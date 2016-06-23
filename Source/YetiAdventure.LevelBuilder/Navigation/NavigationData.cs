using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YetiAdventure.LevelBuilder.Interfaces;

namespace YetiAdventure.LevelBuilder.Navigation
{
    public class NavigationData<T> : INavigationData where T : Page
    {

        public NavigationData(object dataContext) 
        {
            DataContext = dataContext;
            PageType = typeof(T);
        }

        public Type PageType { get; private set; } 
        public object DataContext { get; private set; }
    }


}
