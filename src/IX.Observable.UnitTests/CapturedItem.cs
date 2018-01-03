// <copyright file="CapturedItem.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.Undoable;

namespace IX.Observable.UnitTests
{
    /// <summary>
    /// A test fixture for testing undo/redo stuff.
    /// </summary>
    /// <seealso cref="IX.Undoable.EditableItemBase" />
    public class CapturedItem : EditableItemBase
    {
        private string testProperty;

        /// <summary>
        /// Gets or sets the test property.
        /// </summary>
        /// <value>The test property.</value>
        public string TestProperty
        {
            get => this.testProperty;

            set
            {
                if (this.testProperty != value)
                {
                    this.AdvertisePropertyChange(nameof(this.TestProperty), this.testProperty, value);

                    this.testProperty = value;

                    this.RaisePropertyChanged(nameof(this.TestProperty));
                }
            }
        }

        /// <summary>
        /// Called when a list of state changes must be executed.
        /// </summary>
        /// <param name="stateChanges">The state changes to execute.</param>
        /// <exception cref="InvalidOperationException">
        /// Undo/Redo advertised a state change that is not for the only property, some state is leaking.
        /// or
        /// Undo/Redo advertised a state change that is of a different type than property, some state is leaking.
        /// </exception>
        protected override void DoChanges(StateChange[] stateChanges)
        {
            foreach (StateChange stateChange in stateChanges)
            {
                if (stateChange is PropertyStateChange<string> psts)
                {
                    if (psts.PropertyName != nameof(this.TestProperty))
                    {
                        throw new InvalidOperationException("Undo/Redo advertised a state change that is not for the only property, some state is leaking.");
                    }

                    this.testProperty = psts.NewValue;

                    this.RaisePropertyChanged(nameof(this.TestProperty));
                }
                else
                {
                    throw new InvalidOperationException("Undo/Redo advertised a state change that is of a different type than property, some state is leaking.");
                }
            }
        }

        /// <summary>
        /// Called when a list of state changes are canceled and must be reverted.
        /// </summary>
        /// <param name="stateChanges">The state changes to revert.</param>
        /// <exception cref="InvalidOperationException">
        /// Undo/Redo advertised a state change that is not for the only property, some state is leaking.
        /// or
        /// Undo/Redo advertised a state change that is of a different type than property, some state is leaking.
        /// </exception>
        protected override void RevertChanges(StateChange[] stateChanges)
        {
            foreach (StateChange stateChange in stateChanges)
            {
                if (stateChange is PropertyStateChange<string> psts)
                {
                    if (psts.PropertyName != nameof(this.TestProperty))
                    {
                        throw new InvalidOperationException("Undo/Redo advertised a state change that is not for the only property, some state is leaking.");
                    }

                    this.testProperty = psts.OldValue;

                    this.RaisePropertyChanged(nameof(this.TestProperty));
                }
                else
                {
                    throw new InvalidOperationException("Undo/Redo advertised a state change that is of a different type than property, some state is leaking.");
                }
            }
        }
    }
}