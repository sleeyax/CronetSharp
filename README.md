 # CronetSharp
 C# bindings and library to interact with the cronet (Chromium Network Stack) native C API.
 
 ## Usage
 This library implements cronet functions in a similar way as the [android library](https://developer.android.com/guide/topics/connectivity/cronet).
 If you're already familiar with that, it should be easy for you to convert existing code.
 
 The example below is an exact conversion from the [original android example](https://developer.android.com/guide/topics/connectivity/cronet/start#create) to C# using CronetSharp: 
 ```c#
// Load cronet dll
// You can create your own DLL loader by inheriting from 'CronetLoader' (recommended) or by implementing 'ILoader' (advanced) 
ILoader loader = new CronetLoader();
loader.Load("/absolute/path/to/dir/containing/cronet/dll");

// Create and configure an instance of CronetEngine
CronetEngine.Builder myBuilder = new CronetEngine.Builder();
CronetEngine cronetEngine = myBuilder.BuildAndStart();

// Provide an implementation of the request callback
var myUrlRequestCallback = new UrlRequestCallback(new UrlRequestCallbackHandler
{
    OnRedirectReceived = (req, info, arg3) =>
    {
        // You should call the req.followRedirect() method to continue
        // processing the request.
        req.FollowRedirect();
    },
    OnResponseStarted = (req, info) =>
    {
        // You should call the req.read() method before the request can be
        // further processed. The following instruction provides a ByteBuffer object
        // with a capacity of 102400 bytes to the read() method.
        req.Read(ByteBuffer.Allocate(102400));
    },
    OnReadCompleted = (req, info, byteBuffer, bytesRead) =>
    {
        // You should keep reading the request until there's no more data.
        req.Read(ByteBuffer.Allocate(102400));
    },
    OnSucceeded = (req, info) => { },
    OnFailed = (req, info, error) => { },
    OnCancelled = (req, info) => { }
});

// Create an Executor object to manage network tasks
var executor = new Executor();

// Create and configure a UrlRequest object
UrlRequest.Builder requestBuilder = cronetEngine.NewUrlRequestBuilder("https://example.com", myUrlRequestCallback, executor);
UrlRequest request = requestBuilder.Build();
request.Start();

// Our c# code  continues here ...
Console.ReadKey();
// Cleanup & Free memory
executor.Dispose();
request.Dispose();
cronetEngine.Shutdown();
cronetEngine.Dispose();
 ```

 Please note that this library is built in an explicit way. 
 You'll have to clean up unused resources in unmanaged memory yourself by calling `.Dispose()` on objects or by wrapping them in `using` blocks.
 Also don't forget to `.Start()` your Cronet Engine if you're not using any of the built-in factories or helper methods that do this for you. 
 
 ### Csharpified
 The typical 'builder pattern' that's overused by Google here can be simplified using C# object initializers.
 For example the following code:
```c#
UrlRequest.Builder requestBuilder = cronetEngine.NewUrlRequestBuilder("https://example.com", myUrlRequestCallback, executor);
UrlRequest request = requestBuilder
    .AddHeader("user-agent", "mycustomuseragent")
    .DisableCache()
    .SetPriority(RequestPriority.Highest)
    .Build();
```
Can also be written as:
```c#
UrlRequest request = cronetEngine.NewUrlRequest("https://example.com", myUrlRequestCallback, executor, new UrlRequestParams
{
    Headers = new [] { new HttpHeader("user-agent", "mycustomuseragent") },
    DisableCache = true,
    Priority = RequestPriority.Highest,
});
```
 
 ## Missing Features
Below is a list of functionalities that exist in cronet but were not added to this library (yet).
 * ClientContext (Cronet_ClientContext)
 * Metrics
 
 ## Cronet
 Please see Google's [build instructions](https://chromium.googlesource.com/chromium/src/+/master/components/cronet/build_instructions.md) in order to build cronet from source.
 
 ### Build
 Always execute any of the commands below under `chromium/src`.
  
 **debug**
 ```
$ gn gen out/Debug --args="is_debug=true target_cpu=\"x86\""
$ ninja -C out\Debug cronet
```
 **release**
 ```
$ gn gen out/Release --args="is_debug=false is_component_build=false target_cpu=\"x86\""
$ ninja -C out\Release cronet
 ```  

#### Build options
* For ARMv8 64-bit: `target_cpu="arm64"`
* For x86 32-bit: `target_cpu="x86"`
* For x86 64-bit: `target_cpu="x64"`