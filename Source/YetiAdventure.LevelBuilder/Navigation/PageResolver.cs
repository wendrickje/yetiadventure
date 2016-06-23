using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace YetiAdventure.LevelBuilder.Navigation
{
    public class PageResolver : MarkupExtension
    {
        public Type PageType { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            var page = Activator.CreateInstance(PageType) as Page;
            return page;
        }
    }
}
