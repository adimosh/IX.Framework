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
using IX.StandardExtensions.Threading;
using JetBrains.Annotations;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    ///     A base class for view models.
    /// </summary>
    /// <seealso cref="NotifyPropertyChangedBase" />
    /// <seealso cref="IDisposable" />
    [PublicAPI]
    public abstract class ViewModelBase : NotifyPropertyChangedBase, INotifyDataErrorInfo
    {
        private static readonly string[] EmptyStringArray = new string[0];
        private readonly Lazy<ConcurrentDictionary<string, List<string>>> entityErrors;
        private readonly object validatorLock;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModelBase" /> class.
        /// </summary>
        protected ViewModelBase()
        {
            this.entityErrors = new Lazy<ConcurrentDictionary<string, List<string>>>();
            this.validatorLock = new object();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModelBase" /> class.
        /// </summary>
        /// <param name="synchronizationContext">The specific synchronization context to use.</param>
        protected ViewModelBase(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
            this.entityErrors = new Lazy<ConcurrentDictionary<string, List<string>>>();
            this.validatorLock = new object();
        }

        /// <summary>
        ///     Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        ///     Gets a value indicating whether the entity has validation errors.
        /// </summary>
        /// <value><see langword="true" /> if this instance has errors; otherwise, <see langword="false" />.</value>
        public bool HasErrors =>
            this.entityErrors.IsValueCreated && this.entityErrors.Value.Values.Any(p => p.Count > 0);

        /// <summary>
        ///     Gets the validation errors for a specified property or for the entire view model.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property to retrieve validation errors for; or null or
        ///     <see cref="string.Empty" />, to retrieve entity-level errors.
        /// </param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (this.entityErrors.Value.TryGetValue(
                propertyName,
                out List<string> propertyErrors))
            {
                return propertyErrors.ToArray();
            }

            return EmptyStringArray;
        }

#pragma warning disable IDE0008 // Use explicit type - heavy use of LINQ makes this impractical
#pragma warning disable HAA0401 // Possible allocation of reference type enumerator - Unavoidable
        /// <summary>
        ///     Validates this object.
        /// </summary>
        public void Validate()
        {
            lock (this.validatorLock)
            {
                var initialHasErrors = this.HasErrors;

                // We validate the object
                var validationResults = new List<ValidationResult>();
                if (Validator.TryValidateObject(
                    this,
                    new ValidationContext(
                        this,
                        null), validationResults,
                    true))
                {
                    if (this.entityErrors.IsValueCreated)
                    {
                        this.entityErrors.Value.Clear();
                    }
                }
                else
                {
                    // Remove those properties which pass validation
                    if (!this.entityErrors.IsValueCreated)
                    {
                        foreach (KeyValuePair<string, List<string>> kv in this.entityErrors.Value.ToArray())
                        {
                            if (AllDifferent(
                                validationResults,
                                kv.Key))
                            {
                                this.entityErrors.Value.TryRemove(
                                    kv.Key,
                                    out _);
                                this.RaiseErrorsChanged(kv.Key);
                            }

                            bool AllDifferent(
                                List<ValidationResult> source,
                                string key)
                            {
                                foreach (ValidationResult r in source)
                                {
                                    foreach (var m in r.MemberNames)
                                    {
                                        if (m == key)
                                        {
                                            return false;
                                        }
                                    }
                                }

                                return true;
                            }
                        }
                    }

                    // Those properties that currently don't pass validation should have their validation results saved as messages.
                    foreach (IGrouping<string, ValidationResult> property in from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g)
                    {
                        string[] messages = property.Select(r => r.ErrorMessage).ToArray();

                        List<string> errorList = this.entityErrors.Value.GetOrAdd(
                            property.Key,
                            new List<string>(messages.Length));
                        errorList.Clear();
                        errorList.AddRange(messages);
                        this.RaiseErrorsChanged(property.Key);
                    }
                }

                // Raise property changed for HasErrors, if necessary
                if (this.HasErrors != initialHasErrors)
                {
                    this.RaisePropertyChanged(nameof(this.HasErrors));
                }
            }
        }
#pragma warning restore HAA0401 // Possible allocation of reference type enumerator
#pragma warning restore IDE0008 // Use explicit type

#pragma warning disable HAA0603 // Delegate allocation from a method group - Expected
        /// <summary>
        ///     Raises the property changed event, followed by with validation.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChangedWithValidation(string propertyName)
        {
            this.RaisePropertyChanged(propertyName);

            Fire.AndForget(this.Validate);
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group

        /// <summary>
        ///     Raises the errors changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property for which the errors have been changed.</param>
        protected void RaiseErrorsChanged(string propertyName) => this.Invoke(
            (
                invoker,
                internalPropertyName) => invoker.ErrorsChanged?.Invoke(
                invoker,
                new DataErrorsChangedEventArgs(internalPropertyName)), this,
            propertyName);
    }
}