// <copyright file="RelayCommand.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Commanding
{
    /// <summary>
    /// A relayed command for the editor.
    /// </summary>
    /// <seealso cref="ICommand" />
    [PublicAPI]
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The execute action.
        /// </summary>
        private readonly Action<object> executeAction;

        /// <summary>
        /// The can execute action.
        /// </summary>
        private readonly Predicate<object> canExecuteAction;

        /// <summary>
        /// <see langword="true"/> if the command is waiting for an action, <see langword="false"/> if it is idle.
        /// </summary>
        private bool isWaitingForAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="executeAction">The execute action.</param>
        public RelayCommand(Action<object> executeAction)
        {
            this.executeAction = executeAction;
            this.canExecuteAction = (state) => true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="executeAction">The execute action.</param>
        /// <param name="canExecuteAction">The can execute action.</param>
        public RelayCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            this.executeAction = executeAction;
            this.canExecuteAction = canExecuteAction;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gets a value indicating whether this relay command is in design mode.
        /// </summary>
        /// <value><see langword="true" /> if this relay command is in design mode; otherwise, <see langword="false"/>.</value>
        [global::System.ComponentModel.Browsable(false)]
        public bool IsInDesignMode => DesignMode.IsInDesignMode;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter) => !this.isWaitingForAction && this.canExecuteAction(parameter);

#pragma warning disable HAA0603 // Delegate allocation from a method group - This is acceptable in this case
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            this.isWaitingForAction = true;

            this.TriggerCanExecuteChanged();

            try
            {
                this.executeAction(parameter);
            }
            finally
            {
                this.isWaitingForAction = false;

                (Dispatcher.CurrentDispatcher ?? global::System.Windows.Application.Current.Dispatcher).Invoke(this.TriggerCanExecuteChanged, DispatcherPriority.ApplicationIdle);
            }
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group

        /// <summary>
        /// Triggers the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void TriggerCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}