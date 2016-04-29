using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Test.Tools
{

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
