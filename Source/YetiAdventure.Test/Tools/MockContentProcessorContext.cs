using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Test.Tools
{

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
}
