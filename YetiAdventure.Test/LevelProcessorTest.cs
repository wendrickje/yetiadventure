using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using LevelPackageFileProcessor;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using LevelPackageFileReader;
using YetiAdventure.Levels;
using YetiAdventure.Test.Tools;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace YetiAdventure.Test
{
    [TestClass]
    public class LevelProcessorTest
    {
        [TestMethod]
        public void Test_LevelProcessor_LevelImporter_LevelReader()
        {
            var assetName = "testproject";
            var ext = ".lpfx";
            var testfile = assetName + ext;
            var raw = File.ReadAllBytes(testfile);

            var compiledPackage = new CompiledPackage(raw);
            var levelpackage = new LevelPackage();

            //do importer
            var importer = new LevelPackageImporter();
            var actualCompiledPackage = importer.Import(testfile, new MockContentImporterContext());
            //Assert.IsTrue(actualCompiledPackage == compiledPackage);

            //do processor
            var processor = new LevelPackageProcessor();
            var actualLevelPackage = processor.Process(actualCompiledPackage, new MockContentProcessorContext());
            //Assert.IsTrue(actualLevelPackage == levelpackage);


            //do reader
            var reader = new LevelPackageContentReader();
            var graphicDeviceManager = new MockGraphicsDeviceManager();
            var contentManager = new MockContentManager();
            Action<IDisposable> action = (disposable) =>
            {

            };
            var pcr = new PrivateObject(typeof(ContentReader), contentManager, new MemoryStream(raw), graphicDeviceManager.GraphicsDevice, assetName, 1, action);

            var contentReader = pcr.Target as ContentReader;
            var readOutputPackage = reader.ReadPublic(contentReader, levelpackage);
            //Assert.IsTrue(actualLevelPackage == readOutputPackage);

        }




    }
}
