using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelPackageFileProcessor
{
    public class CompiledPackage
    {

        public CompiledPackage(byte[] raw)
        {
            _raw = raw;
        }

        private byte[] _raw;
        public byte[] Raw
        {
            get
            {
                return _raw;//(byte[])_raw.Clone();
            } 
        }

    }
}
