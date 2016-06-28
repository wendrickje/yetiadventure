using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.LevelBuilder.Common
{
    public class IconImageSourceProvider : MarkupExtension
    {
        public IconType Icon { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var source = YetiAdventure.Shared.Icons.Icon.Icons[Icon];
            return source.GetImageSource();
        }
    }
}
