**/Loader/Loader.csproj** will be used to load assemblies and output some information, in this case Methods and their return types

**/LibToLoad/LibToLoad.csproj** is a normal dll that will be reflected over

**/Loader/AssemblyResolver.cs** has been adapted from https://samcragg.wordpress.com/2017/06/30/resolving-assemblies-in-net-core/

**/Loader/PackageCompilationRuntimesAssemblyResolver** is a very crude loader that allows loading of `System.Data.Client.dll`
