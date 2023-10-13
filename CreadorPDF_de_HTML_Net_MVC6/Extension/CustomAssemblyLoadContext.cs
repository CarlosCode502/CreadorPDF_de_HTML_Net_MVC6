using System.Reflection;
using System.Runtime.Loader;//Libreria de la herencia ALContext

namespace CreadorPDF_de_HTML_Net_MVC6.Extension
{
    // min 04:30 aprox
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        //Indicamos a nuestro proyecto que vamos a estar trabajando con una libreria de recurso externo
        //que es la librería .dll

        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            //return base.Load(assemblyName);
            throw new NotImplementedException();
        }
    }
}
