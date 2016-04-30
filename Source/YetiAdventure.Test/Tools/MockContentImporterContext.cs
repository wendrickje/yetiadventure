using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Test.Tools
{

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
}
