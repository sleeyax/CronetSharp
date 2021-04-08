using System;
using System.Collections.Generic;
using System.IO;
using CronetSharp.Cronet;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;

namespace CronetSharp.Tests
{
    [TestFixture]
    public class CronetEngineTest : SetupCronet
    {
        private string _userAgent;

        [SetUp]
        public void Setup()
        {
            base.Setup();
            _userAgent = "cronetenginetests";
        }
        
        [Test]
        public void TestCanStartEngine()
        {
            using var engineParams = new CronetEngineParams
            {
                UserAgent = _userAgent
            };
            using var engine = new CronetEngine(engineParams);
            var result = engine.Start();
            Assert.AreEqual(EngineResult.SUCCESS, result);
        }

        [Test]
        public void TestCanStartTwoEngines()
        {
            using var engineParams1 = new CronetEngineParams();
            using var engine1 = new CronetEngine(engineParams1);
            using var engineParams2 = new CronetEngineParams();
            using var engine2 = new CronetEngine(engineParams2);
            Assert.AreEqual(EngineResult.SUCCESS, engine1.Start());
            Assert.AreEqual(EngineResult.SUCCESS, engine2.Start());
        }

        [Test]
        public void TestGetVersionString()
        {
            using var engine = new CronetEngine();
            string version = engine.Version;
            Assert.IsNotNull(version);
            Assert.AreEqual(true, version.Contains("."));
        }

        [Test]
        public void TestEngineDoesntStartWhenCacheStoragePathInvalid()
        {
            using var engineParams = new CronetEngineParams
            {
                HttpCacheMode = HttpCacheMode.Disk,
                CheckResultEnabled = false
            };
            using var engine = new CronetEngine(engineParams);
            var result = engine.Start();
            Assert.AreEqual(EngineResult.ILLEGAL_ARGUMENT_STORAGE_PATH_MUST_EXIST, result);
            engineParams.StoragePath = "invalidPath";
            result = engine.Start();
            Assert.AreEqual(EngineResult.ILLEGAL_ARGUMENT_STORAGE_PATH_MUST_EXIST, result);
            engineParams.StoragePath = Helpers.CreateTemporaryDirectory();
            result = engine.Start();
            Assert.AreEqual(EngineResult.SUCCESS, result);
            result = engine.Start();
            Assert.AreEqual(EngineResult.ILLEGAL_STATE_ENGINE_ALREADY_STARTED, result);
            
            // second engine shouldn't be able to use the same storage path while in use by the first
            using var engine2 = new CronetEngine(engineParams);
            result = engine2.Start();
            Assert.AreEqual(EngineResult.ILLEGAL_STATE_STORAGE_PATH_IN_USE, result);
            
            // when the first engine is destroyed, the second engine should now be able to use the storage path
            result = engine.Shutdown();
            Assert.AreEqual(EngineResult.SUCCESS, result);
            result = engine2.Start();
            Assert.AreEqual(EngineResult.SUCCESS, result);
        }

        [Test]
        public void TestCantStartWithInvalidPublicKeyPins()
        {
            using var engineParams = new CronetEngineParams
            {
                CheckResultEnabled = false
            };
            using var engine = new CronetEngine(engineParams);
            using var publicKeyPins = new PublicKeyPins();
            
            // detect invalid public key pins
            engineParams.AddPublicKeyPins(publicKeyPins);
            var result = engine.Start();
            Assert.AreEqual(EngineResult.NULL_POINTER_HOSTNAME, result);
            engineParams.ClearPublicKeyPins();
            
            // detect long hostnames
            publicKeyPins.Host = new String('a', 256);
            engineParams.AddPublicKeyPins(publicKeyPins);
            result = engine.Start();
            Assert.AreEqual(EngineResult.ILLEGAL_ARGUMENT_INVALID_HOSTNAME, result);
            engineParams.ClearPublicKeyPins();

            // detect invalid hostnames
            publicKeyPins.Host = "invalid:host/name";
            engineParams.AddPublicKeyPins(publicKeyPins);
            result = engine.Start();
            Assert.AreEqual(EngineResult.ILLEGAL_ARGUMENT_INVALID_HOSTNAME, result);
            engineParams.ClearPublicKeyPins();
            
            // set valid hostname but detect missing pins
            publicKeyPins.Host = "valid.host.name";
            engineParams.AddPublicKeyPins(publicKeyPins);
            result = engine.Start();
            Assert.AreEqual(EngineResult.NULL_POINTER_SHA256_PINS, result);
            engineParams.ClearPublicKeyPins();
            
            // detect invalid pin
            publicKeyPins.Pins = new[] {"invalid_sha256"};
            engineParams.AddPublicKeyPins(publicKeyPins);
            result = engine.Start();
            Assert.AreEqual(EngineResult.ILLEGAL_ARGUMENT_INVALID_PIN, result);
            engineParams.ClearPublicKeyPins();
        }

        [Test]
        public void TestPublicKeyPins()
        {
            using var engineParams = new CronetEngineParams
            {
                CheckResultEnabled = false,
                PublicKeyPins = new []
                {
                    new PublicKeyPins("valid.host.name", new []{"sha256/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA="})
                }
            };
            using var engine = new CronetEngine(engineParams);
            var result = engine.Start();
            Assert.AreEqual(EngineResult.SUCCESS, result);
        }

        [Test]
        public void TestNetLog()
        {
            using var engineParams = new CronetEngineParams
            {
                ExperimentalOptions = "{ \"QUIC\" : {\"max_server_configs_stored_in_properties\" : 8} }"
            };
            using var engine = new CronetEngine(engineParams);
            string netLogFile = Path.Combine(Helpers.CreateTemporaryDirectory(), "netlog.json");
            
            // shouldn't be able to start netlog when engine is not running
            Assert.IsFalse(engine.StartNetLogToFile(netLogFile, true));
            engine.StopNetLog();

            // netlog should work after starting the engine
            engine.Start();
            Assert.IsTrue(engine.StartNetLogToFile(netLogFile, true));
            engine.StopNetLog();
            
            // test that we can't start/stop netlog twice
            Assert.IsTrue(engine.StartNetLogToFile(netLogFile, true));
            Assert.IsFalse(engine.StartNetLogToFile(netLogFile, true));
            engine.StopNetLog();
            
            // verify that file actually contains netlogs
            var netLogFileContents = File.ReadAllText(netLogFile);
            Assert.AreEqual(true, netLogFileContents.Contains("{\"QUIC\":{\"max_server_configs_stored_in_properties\":8}"));
            engine.StopNetLog();
            
            // test that net log cannot start/stop after engine shutdown.
            engine.Shutdown();
            Assert.IsFalse(engine.StartNetLogToFile(netLogFile, true));
        }

        [Test]
        public void TestInvalidNetLogFile()
        {
            using var engineParams = new CronetEngineParams
            {
                ExperimentalOptions = "{ \"QUIC\" : {\"max_server_configs_stored_in_properties\" : 8} }"
            };
            using var engine = new CronetEngine(engineParams);
            var started = engine.StartNetLogToFile("invalidFile", true);
            Assert.IsFalse(started);
        }
    }
}