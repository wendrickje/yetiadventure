using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LevelPackageFileProcessor
{
    public class LevelPackage
    {

        public LevelPackage(string file)
        {
            File = file;
        }

        public string File { get; private set; }

        public LevelManager.LevelManager LevelManager { get; set; }

        public object ImageSource { get; set; }

        public List<int[][]> LevelLayouts { get; set; }

    }
}
