using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelPackageFileProcessor
{
    class CompiledPackage
    {

        public CompiledPackage(byte[] compiled)
        {
            _compiled = compiled;
        }

        private byte[] _compiled;
        public byte[] Compiled {
            get { return (byte[])_compiled.Clone(); } 
        }

    }
}
