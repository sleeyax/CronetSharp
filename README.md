 # CronetSharp
 C# bindings to interact with cronet (Chromium Network Stack) native C API.
 
 ## Cronet
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