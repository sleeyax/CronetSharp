namespace CronetSharp.Cronet.Bin
{
    public interface ILoader
    {
       /// <summary>
       /// Load the cronet dll into the current process
       /// </summary>
       /// <param name="path">relative directory to the dll</param>
        void Load(string path);
    }
}