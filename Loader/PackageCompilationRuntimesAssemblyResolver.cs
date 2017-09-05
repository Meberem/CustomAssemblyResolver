using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace Loader
{
    public class PackageCompilationRuntimesAssemblyResolver : ICompilationAssemblyResolver
    {
        private readonly string[] _nugetPackageDirectories;

        public PackageCompilationRuntimesAssemblyResolver()
            : this(DefaultNuGetPaths())
        {
        }

        public PackageCompilationRuntimesAssemblyResolver(string path)
            : this(new [] { path })
        {
        }

        public PackageCompilationRuntimesAssemblyResolver(string[] paths)
        {
            _nugetPackageDirectories = paths;
        }

        public bool TryResolveAssemblyPaths(CompilationLibrary library, List<string> assemblies)
        {
            if (_nugetPackageDirectories == null || _nugetPackageDirectories.Length == 0 || !string.Equals(library.Type, "package", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            foreach (var directory in _nugetPackageDirectories)
            {
                string packagePath;

                var fullPath = Path.Combine(directory, library.Name, library.Version, "runtimes", "unix", "lib", "netstandard1.3", $"{library.Name}.dll");
                if (File.Exists(fullPath))
                {
                    assemblies.AddRange(new[] { fullPath });
                    return true;
                }
            }

            return false;        
        }

        private static string[] DefaultNuGetPaths() 
        {
            var osPlatform = RuntimeEnvironment.OperatingSystemPlatform;
            
            string basePath;
            if (osPlatform == Platform.Windows)
            {
                basePath = Environment.GetEnvironmentVariable("USERPROFILE");
            }
            else
            {
                basePath = Environment.GetEnvironmentVariable("HOME");
            }

            if (string.IsNullOrEmpty(basePath))
            {
                return new string[] { string.Empty };
            }

            return new string[] { Path.Combine(basePath, ".nuget", "packages") };
        }
    }
}