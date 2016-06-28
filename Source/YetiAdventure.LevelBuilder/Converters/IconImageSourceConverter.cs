using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using YetiAdventure.LevelBuilder.Common;
using YetiAdventure.Shared.Icons;

namespace YetiAdventure.LevelBuilder.Converters
{
    public class IconImageSourceConverter : ConverterBase
    {
        public IconImageSourceConverter()
        {

        }

        /// <summary>
        /// takes in an <see cref="IconType"/> and returns a <see cref="BitmapImage"/> source
        /// </summary>
        /// <param name="value">icon type <see cref="IconType"/></param>
        /// <param name="targetType">a <see cref="BitmapImage"/></param>
        /// <param name="parameter">parameter not used</param>
        /// <param name="culture">culture invariant</param>
        /// <returns>a <see cref="BitmapImage"/></returns>
        public override object DoConvert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return value;
            var iconString = value.ToString();
            if (iconString == null) return value;
            var icon = (IconType)Enum.Parse(typeof(IconType), iconString);
            var source = YetiAdventure.Shared.Icons.Icon.Icons[icon];
            return source.GetImageSource();
        } 

    }
}
