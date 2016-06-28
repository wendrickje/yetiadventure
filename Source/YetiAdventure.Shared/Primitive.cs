using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Shared
{
    public class Primitive
    {
        private Guid _guid;

        /// <summary>
        /// Primitive ID
        /// /// </summary>
        public Guid Guid
        {
            get
            {
                return _guid == null || _guid == Guid.Empty ? (_guid = Guid.NewGuid()) : _guid;
            }
        }
    }
}
