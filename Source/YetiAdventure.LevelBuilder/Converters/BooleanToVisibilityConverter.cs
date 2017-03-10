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
    /// boolean to visibility converter
    /// </summary>
    /// <seealso cref="YetiAdventure.LevelBuilder.Converters.ConverterBase" />
    public class BooleanToVisibilityConverter : ConverterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter"/> class.
        /// </summary>
        public BooleanToVisibilityConverter()
        {

        }

        private readonly System.Windows.Controls.BooleanToVisibilityConverter _internalConverter = new System.Windows.Controls.BooleanToVisibilityConverter();

        /// <summary>
        /// Conversion logic
        /// </summary>
        /// <param name="value">The value being converted</param>
        /// <param name="targetType">Property type</param>
        /// <param name="parameter">Converter parameter</param>
        /// <param name="culture">Current culture</param>
        /// <returns></returns>
        public override object DoConvert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return _internalConverter.Convert(value, targetType, parameter, culture);
        }

        #region Overrides of ConverterBase

        /// <summary>
        /// Optional ConvertBack method. By default returns value;
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _internalConverter.ConvertBack(value, targetType, parameter, culture);
        }

        #endregion
    }
}
