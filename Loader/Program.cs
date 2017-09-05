using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentBinDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var otherBinPath = Path.Combine(currentBinDirectory, "..", "..", "..", "..", "LibToLoad", "bin", "Debug", "netstandard1.6");

            var otherDll = Path.Combine(otherBinPath, "LibToLoad.dll");
            var exists = File.Exists(otherDll);
            
            using (var dynamicContext = new AssemblyResolver(otherDll))
            {
                PrintTypes(dynamicContext.Assembly);
            }

            return;
        }

        private static void PrintTypes(Assembly assembly)
        {
            foreach (TypeInfo type in assembly.DefinedTypes)
            {
                Console.WriteLine(type.Name);

                foreach (var methodInfo in type.DeclaredMethods)
                {
                    Console.WriteLine("    {0} -> {1}", methodInfo.Name, methodInfo.ReturnType.Name);
                }
            }
        }
    }
}
