using System;
using System.Windows.Input;

namespace ESNMatcherCreator.Helpers
{
    /// <summary>
    /// Класс реализации интерфейса Command
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields
        private Action<object> execute;
        private Func<object, bool> canExecute;
        #endregion

        #region Constructions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => execute(parameter);
        #endregion
    }
}
