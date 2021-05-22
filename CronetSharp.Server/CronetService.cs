using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CronetSharp.Cronet;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace CronetSharp.Server
{
    internal class CronetService : WebSocketBehavior
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public CronetService()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        
        protected override async void OnMessage(MessageEventArgs e)
        {
            var request = JsonConvert.DeserializeObject<CronetRequest>(e.Data, _serializerSettings);

            if (request == null)
            {
                SendErrorResponse(0, "Expected valid Request object");
                return;
            }
            
            using var engineParameters = request.EngineParams;
            using var engine = CronetEngine.CreateAndStart(engineParameters);
            
            using var bodyStream = new MemoryStream();
            
            var taskCompletionSource = new TaskCompletionSource<CronetResponse>();

            void SetResponse(UrlRequest _, UrlResponseInfo info)
            {
                try
                {
                    var response = new CronetResponse
                    {
                        Id = request.Id,
                        Error = null,
                        Body = Encoding.UTF8.GetString(bodyStream.ToArray()),
                        BodyRaw = bodyStream.ToArray(),
                        StatusCode = info.HttpStatusCode,
                        StatusCodeText = info.HttpStatusCodeText,
                        Headers = info.Headers,
                    };
                    taskCompletionSource.TrySetResult(response);
                }
                catch (Exception ex)
                {
                    taskCompletionSource.TrySetException(ex);
                }
            }

            using var urlRequestCallback = new UrlRequestCallback
            {
                OnRedirectReceived = (req, info, arg3) =>
                {
                    if (request.FollowRedirects) req.FollowRedirect();
                    else req.Cancel();
                },
                OnResponseStarted = (req, info) =>
                {
                    req.Read(ByteBuffer.Allocate(102400));
                },
                OnReadCompleted = (req, info, byteBuffer, bytesRead) =>
                {
                    var bodyBytes = byteBuffer.GetData();
                    bodyStream.Write(bodyBytes, 0, (int) bytesRead);
                    byteBuffer.Clear();
                    req.Read(byteBuffer);
                },
                OnSucceeded = SetResponse,
                OnCancelled = SetResponse,
                OnFailed = (req, info, error) =>
                {
                    if (error.ErrorCode == ErrorCode.TimedOut || error.ErrorCode == ErrorCode.ConnectionTimedOut)
                        taskCompletionSource.TrySetResult(new CronetResponse {Id = request.Id, Error = "Request timed out"});
                    else if (error.CronetErrorMessage == "net::ERR_PROXY_CONNECTION_FAILED")
                        taskCompletionSource.TrySetResult(new CronetResponse {Id = request.Id, Error = "Proxy authentication required"});
                    else
                        taskCompletionSource.TrySetException(error);
                }
            };

            using var urlRequestParams = new UrlRequestParams
            {
                HttpMethod = request.Method,
                Headers = request.Headers,
                AllowDirectExecutor = true,
                DisableCache = request.UrlRequestParameters?.DisableCache ?? true,
                Priority = request.UrlRequestParameters?.RequestPriority ?? RequestPriority.Medium
            };
            if (request.Body != null)
            {
                urlRequestParams.UploadDataProvider = UploadDataProvider.Create(request.Body);
                urlRequestParams.UploadDataProviderExecutor = new Executor();
            }
            
            using var executor = Executors.NewSingleThreadExecutor();

            using var urlRequest = engine.NewUrlRequest(request.Url, urlRequestCallback, executor, urlRequestParams);
            urlRequest.Start();

            try
            {
                var response = await taskCompletionSource.Task;
                SendResponse(response);
            }
            catch (CronetException ex)
            {
                SendErrorResponse(request.Id, $"Cronet exception occured with code {ex.ErrorCode} and message '{ex.CronetErrorMessage}'");
            }
            catch (Exception ex)
            {
                SendErrorResponse(request.Id, $"Unexpected error: {ex.Message}");
            }
            finally
            {
                var engineResult = engine.Shutdown();
                if (engineResult != EngineResult.SUCCESS)
                    throw new Exception("Failed to shutdown cronet engine. Result: " + engineResult);
            }
        }

        private void SendResponse(CronetResponse response)
        {
            Send(JsonConvert.SerializeObject(response, Formatting.None, _serializerSettings));
        }

        private void SendErrorResponse(int id, string error)
        {
            SendResponse(new CronetResponse{Id= id, Error = error});
        }
    }
}