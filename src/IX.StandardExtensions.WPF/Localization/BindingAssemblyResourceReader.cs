// <copyright file="BindingAssemblyResourceReader.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using IX.StandardExtensions.ComponentModel;
using JetBrains.Annotations;

namespace IX.StandardExtensions.WPF.Localization
{
    /// <summary>
    /// A string resource reader.
    /// </summary>
    [PublicAPI]
    public class BindingAssemblyResourceReader : NotifyPropertyChangedBase
    {
        /// <summary>
        /// The resource managers.
        /// </summary>
        private readonly Dictionary<Tuple<string, string>, ResourceManager> resourceManagers;

        /// <summary>
        /// The culture.
        /// </summary>
        private CultureInfo culture;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingAssemblyResourceReader"/> class.
        /// </summary>
        public BindingAssemblyResourceReader()
        {
            this.resourceManagers = new Dictionary<Tuple<string, string>, ResourceManager>();
        }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public CultureInfo Culture
        {
            get => this.culture;

            set
            {
                if (this.culture == value)
                {
                    return;
                }

                this.culture = value;

                this.RaisePropertyChanged(nameof(this.Localization));
            }
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <value>Current instance.</value>
        public BindingAssemblyResourceReader Localization => this;

        /// <summary>
        /// Registers the used resources.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <exception cref="InvalidOperationException">
        /// Could not crete resource manager, or registration already exists.
        /// </exception>
        public void RegisterUsedResources(Assembly assembly, string resourcePath)
        {
            var registration = new Tuple<string, string>(assembly.FullName, resourcePath);

            if (this.resourceManagers.ContainsKey(registration))
            {
                throw new InvalidOperationException();
            }

            ResourceManager manager;
            try
            {
                manager = new ResourceManager(resourcePath, assembly);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resources.ErrorSourceRegistrationCouldNotBeCreated, ex);
            }

            this.resourceManagers.Add(registration, manager);
        }

        /// <summary>
        /// Gets the localized resource.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The localized resource value.</returns>
        public string GetLocalizedResource(string key, CultureInfo culture)
        {
            foreach (ResourceManager man in this.resourceManagers.Values)
            {
                var entry = man.GetString(key, culture);

                if (!string.IsNullOrEmpty(entry))
                {
                    return entry;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the localized resource.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The localized resource value.</returns>
        public string GetLocalizedResource(string key)
        {
            foreach (ResourceManager man in this.resourceManagers.Values)
            {
                var entry = man.GetString(key, this.culture);

                if (!string.IsNullOrEmpty(entry))
                {
                    return entry;
                }
            }

            return null;
        }
    }
}