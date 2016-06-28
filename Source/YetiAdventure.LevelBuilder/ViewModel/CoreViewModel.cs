using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.LevelBuilder.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class CoreViewModel : INotifyPropertyChanged
    {

        private bool _isLoading;
        
        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string prop = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(prop);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion
    }
}
