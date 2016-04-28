using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using LevelPackageFileProcessor;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace YetiAdventure.Test
{
    [TestClass]
    public class LevelProcessorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            byte[] raw;
            string data;
            var filestream = Assembly.GetExecutingAssembly().GetManifestResourceStream("YetiAdventure.Test.testproject.lpfx");
            using (var reader = new StreamReader(filestream))
            {
                data = reader.ReadToEnd();
                raw = Encoding.ASCII.GetBytes(data);
                //raw = Convert.FromBase64String(data);

            }
            var compiledPackage = new CompiledPackage(raw);

            var testfile = "testfile.lpfx";
            using (var writer = new StreamWriter(File.Create(testfile)))
            {

                writer.Write(data);
            }

            var processor = new LevelPackageProcessor();
            var importer = new LevelPackageImporter();


            var actualCompiledPackage = importer.Import(testfile, new MockContentImporterContext());
            //Assert.IsTrue(actual == compiledPackage);
            var actualLevelPackage = processor.Process(actualCompiledPackage, new MockContentProcessorContext());


        }
    }

    public class MockContentProcessorContext : ContentProcessorContext
    {
        public override string BuildConfiguration
        {
            get
            {
                return "";
            }
        }

        public override string IntermediateDirectory
        {
            get
            {
                return "";
            }
        }
        ContentBuildLogger _logger;
        public override ContentBuildLogger Logger
        {
            get
            {
                return _logger ?? (_logger = new MockContentBuildLogger());
            }
        }

        public override string OutputDirectory
        {
            get
            {
                return "";
            }
        }

        public override string OutputFilename
        {
            get
            {
                return "";
            }
        }

        public override OpaqueDataDictionary Parameters
        {
            get
            {
                return new OpaqueDataDictionary();
            }
        }

        public override TargetPlatform TargetPlatform
        {
            get
            {
                return TargetPlatform.Windows;
            }
        }

        public override GraphicsProfile TargetProfile
        {
            get
            {
                return GraphicsProfile.HiDef;
            }
        }

        public override void AddDependency(string filename)
        {
        }

        public override void AddOutputFile(string filename)
        {
        }

        public override TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName)
        {
            var obj = default(TOutput);
            return obj;
        }

        public override ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName)
        {
            var obj = default(ExternalReference<TOutput>);
            return obj;
        }

        public override TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters)
        {
            var obj = default(TOutput);
            return obj;
        }
    }
    public class MockContentImporterContext : ContentImporterContext
    {
        public override string IntermediateDirectory
        {
            get
            {
                return "";
            }
        }
        ContentBuildLogger _logger;
        public override ContentBuildLogger Logger
        {
            get
            {
                return _logger ?? (_logger = new MockContentBuildLogger());
            }
        }

        public override string OutputDirectory
        {
            get
            {
                return "";
            }
        }

        public override void AddDependency(string filename)
        {
        }
    }

    public class MockContentBuildLogger : ContentBuildLogger
    {
        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
        }
    }
}
