// <copyright file="ViewModelBase.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// A base class for view models.
    /// </summary>
    /// <seealso cref="NotifyPropertyChangedBase" />
    /// <seealso cref="IDisposable" />
    public abstract class ViewModelBase : NotifyPropertyChangedBase, INotifyDataErrorInfo
    {
        private static readonly string[] EmptyStringArray = new string[0];

        private ConcurrentDictionary<string, List<string>> entityErrors = new ConcurrentDictionary<string, List<string>>();
        private object validatorLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        protected ViewModelBase()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected ViewModelBase(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets a value indicating whether the entity has validation errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        public bool HasErrors => this.entityErrors.Values.Any(p => p.Count > 0);

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire view model.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="string.Empty" />, to retrieve entity-level errors.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (this.entityErrors.TryGetValue(propertyName, out List<string> propertyErrors))
            {
                return propertyErrors.ToArray();
            }

            return EmptyStringArray;
        }

        /// <summary>
        /// Validates this object asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        public Task ValidateAsync() => Task.Run(() => this.Validate());

        /// <summary>
        /// Validates this object.
        /// </summary>
        public void Validate()
        {
            lock (this.validatorLock)
            {
                var initialHasErrors = this.HasErrors;

                // We validate the object
                var validationResults = new List<ValidationResult>();
                if (Validator.TryValidateObject(this, new ValidationContext(this, null), validationResults, true))
                {
                    this.entityErrors.Clear();
                }
                else
                {
#pragma warning disable IDE0008 // Use explicit type - heavy use of LINQ makes this impractical

                    // Remove those properties which pass validation
                    foreach (var kv in this.entityErrors.ToArray())
                    {
                        if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                        {
                            this.entityErrors.TryRemove(kv.Key, out List<string> ignored);
                            this.RaiseErrorsChanged(kv.Key);
                        }
                    }

                    // Those properties that currently don't pass validation should have their validation results saved as messages.
                    foreach (var property in from r in validationResults
                                             from m in r.MemberNames
                                             group r by m into g
                                             select g)
                    {
                        string[] messages = property.Select(r => r.ErrorMessage).ToArray();

                        List<string> errorList = this.entityErrors.GetOrAdd(property.Key, new List<string>(messages.Length));
                        errorList.Clear();
                        errorList.AddRange(messages);
                        this.RaiseErrorsChanged(property.Key);
                    }
#pragma warning restore IDE0008 // Use explicit type
                }

                // Raise property changed for HasErrors, if necessary
                if (this.HasErrors != initialHasErrors)
                {
                    this.RaisePropertyChanged(nameof(this.HasErrors));
                }
            }
        }

        /// <summary>
        /// Raises the property changed event, followed by with validation.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChangedWithValidation(string propertyName)
        {
            this.RaisePropertyChanged(propertyName);

            this.FireAndForget(this.Validate);
        }

        /// <summary>
        /// Raises the errors changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property for which the errors have been changed.</param>
        protected void RaiseErrorsChanged(string propertyName) => this.Invoke(
                (state) =>
                {
                    var arguments = ((ViewModelBase, string))state;
                    arguments.Item1.ErrorsChanged?.Invoke(arguments.Item1, new DataErrorsChangedEventArgs(arguments.Item2));
                },
                (this, propertyName));
    }
}