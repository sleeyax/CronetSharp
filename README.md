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
var myUrlRequestCallback = new UrlRequestCallback()
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
};

// Create an Executor object to manage network tasks
var executor = Executors.NewSingleThreadExecutor();

// Create and configure a UrlRequest object
UrlRequest.Builder requestBuilder = cronetEngine.NewUrlRequestBuilder("https://example.com", myUrlRequestCallback, executor);
UrlRequest request = requestBuilder.Build();
request.Start();

// Let's do some other C# stuff here ...
Console.WriteLine("Request sent! Please wait...");
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
Below is a list of functionalities that exist in cronet but were not added to this library.
 * ClientContext (to pass the current object context to underlying native structures in order to retrieve it later, as seen in the Java implementation for Android)
 * Metrics (only really useful for logging purposes)
 * Stream handler (implementation of the java.net.HttpURLConnection API)
 
 ## Cronet
 Please see Google's [build instructions](https://chromium.googlesource.com/chromium/src/+/master/components/cronet/build_instructions.md) in order to build cronet from source.
 
 ### Proxies
 As of the time of writing, cronet [doesn't officially support proxies](https://bugs.chromium.org/p/chromium/issues/detail?id=1122749&q=component%3AInternals%3ENetwork%3ELibrary%20proxy&can=2) yet.
 However, you'll notice that this wrapper comes with methods for proxy handling out of the box. 
 Either modify the chromium source code yourself to add this proxy support or wait for it to be added by Google developers.
 
 ### Build 
 Execute the following to create a release build, containing all dependencies in 1 single library:
```
$ cd chromium/src
$ gn gen out/Cronet --args="chrome_pgo_phase = 0 is_debug=false is_component_build=false is_official_build=true target_cpu=\"x64\""
$ autoninja -C out/Cronet cronet_package
```

The final compiled library can be found in `out\Cronet\cronet` or just `out\Cronet` depending on the build arguments that were specified.

#### Build arguments
List of options that you can use to modify the output of the command above.
##### Target Architecture
* For ARMv8 64-bit: `target_cpu="arm64"`
* For x86 32-bit: `target_cpu="x86"`
* For x86 64-bit: `target_cpu="x64"`

##### Release modes
* Create debug build: `is_debug=true`
* Create release build: `is_debug=false`

##### Dependencies
* Bundle native components: `is_component_build=false`
* Separate native components: `is_component_build=true`

## License
Currently I do not wish to license this code, which means all rights are reserved. 
You do not have the right to to use, modify, share modifications, or share the software including its compiled form without prior written consent from the author.
