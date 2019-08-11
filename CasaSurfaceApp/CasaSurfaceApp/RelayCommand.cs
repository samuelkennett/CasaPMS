using System;
using System.Windows.Input;

namespace CasaSurfaceApp
{
    /// <summary>
    /// A basic Command that runs an action. Citation: https://github.com/angelsix/youtube/blob/develop/WPF/03-TreeViewsSimpleViewModel/WpfTreeView/Directory/ViewModels/Base/RelayCommand.cs
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members
        /// <summary>
        /// Action to run
        /// </summary>
        private Action mAction;

        #endregion

        #region Public Events

        /// <summary>
        /// The event that is fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Commands Methods

        /// <summary>
        /// relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter){
            return true;
        }

        /// <summary>
        /// Executes the commands action
        /// </summary>
        /// <param name="parameters"></param>
        public void Execute(object parameters)
        {
            mAction();
        }

        #endregion

    }
}
