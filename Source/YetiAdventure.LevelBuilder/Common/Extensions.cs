using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.LevelBuilder.Common
{
    public static class Extensions
    {
        public static TService GetService<TService>(this IServiceProvider container) where TService : class
        {
            return container.GetService(typeof(TService)) as TService;
        }

        /// <summary>
        /// converts <see cref="Icon"/> to a <see cref="BitmapImage"/>
        /// </summary>
        /// <param name="icon">an <see cref="Icon"/></param>
        /// <returns>a <see cref="BitmapImage"/></returns>
        public static BitmapImage GetImageSource(this Icon icon)
        {
            if (icon == null || icon.Raw == null) return null;
            using (var stream = new MemoryStream(icon.Raw))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }
    }
}
