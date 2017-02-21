using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.LevelBuilder.ViewModel
{
    /// <summary>
    /// base viewmodel
    /// </summary>
    public class CoreViewModel : INotifyPropertyChanged
    {

        private bool _isLoading;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                SetProperty(ref _isLoading, value);
            }
        }


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">The storage.</param>
        /// <param name="value">The value.</param>
        /// <param name="prop">The property.</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string prop = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(prop);
            return true;
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="prop">The property.</param>
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="p">The p.</param>
        protected void OnPropertyChanged(Expression<Func<object>> p)
        {
            var target = p.Body as MemberExpression;
            if (target == null) return;
            OnPropertyChanged(target.Member.Name);
        }


        #endregion
    }
}
