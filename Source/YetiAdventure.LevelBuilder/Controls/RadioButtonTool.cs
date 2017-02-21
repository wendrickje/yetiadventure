using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YetiAdventure.LevelBuilder.Controls
{
    /// <summary>
    /// custom radio button
    /// </summary>
    /// <seealso cref="System.Windows.Controls.RadioButton" />
    public class RadioButtonTool : RadioButton
    {
        /// <summary>
        /// Initializes the <see cref="RadioButtonTool"/> class.
        /// </summary>
        static RadioButtonTool()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButtonTool), new FrameworkPropertyMetadata(typeof(RadioButtonTool)));
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }




    }
}
