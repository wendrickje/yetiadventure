using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YetiAdventure.LevelBuilder.Converters
{
    /// <summary>
    /// inverse boolean to visibility converter
    /// </summary>
    /// <seealso cref="YetiAdventure.LevelBuilder.Converters.ConverterBase" />
    public class InverseBooleanToVisibilityConverter : ConverterBase
    {
        /// <summary>
        /// Conversion logic
        /// </summary>
        /// <param name="value">The value being converted</param>
        /// <param name="targetType">Property type</param>
        /// <param name="parameter">Converter parameter</param>
        /// <param name="culture">Current culture</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override object DoConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //when true return collapsed
            //when false return visible
            var val = (bool)value;
            return val ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
