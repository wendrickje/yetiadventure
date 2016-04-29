using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Levels
{
    public class LevelPackage 
    {
        public LevelPackage()
        {
        }

        public LevelPackage(string name, List<int[][]> levelLayouts, object imageSource)
        {
            Name = name;
            LevelLayouts = levelLayouts;
            ImageSource = imageSource;
        }

        public string Name { get; private set; }

        public object ImageSource { get; set; }

        public List<int[][]> LevelLayouts { get; set; }

        


    }
}
