namespace CronetSharp.CronetAsm
{
    public interface ILoader
    {
        /// <summary>
        /// Load the cronet dll into the current process
        /// </summary>
        /// <param name="dll"></param>
        void Load(string dll = "cronet.dll");
    }
}