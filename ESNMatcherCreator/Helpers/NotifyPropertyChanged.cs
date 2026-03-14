using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ESNMatcherCreator.Helpers
{
    /// <summary>
    /// Класс реализации интерфейса NotifyPropertyChanged
    /// </summary>
    public class NotifyPropertyChanged: INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Событие PropertyChangedEventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
    }
}
