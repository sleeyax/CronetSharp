using System;
using System.IO;
using CronetSharp.Cronet.Bin;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    /// <summary>
    /// Inherit from this class to load the cronet DLL before tests
    /// </summary>
    public class SetupCronet
    {
        [SetUp]
        public void Setup()
        {
            var localPath = typeof(SetupCronet).Assembly.EscapedCodeBase.Replace("file:///", "");
            var dir = Path.GetDirectoryName(localPath);
            var platform = Environment.Is64BitProcess ? "Win64" : "Win32";
            var path = Path.Combine(dir, "Cronet", "Bin", platform);
            new CronetLoader().Load(path);
        }
    }
}