//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IX.StandardExtensions
{
    using System.Reflection;


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IX.StandardExtensions.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to An invalid set of arguments was specified. Argument names: {0}.
        /// </summary>
        internal static string AnInvalidSetOfArgumentsWasSpecifiedArgumentNames
        {
            get
            {
                return ResourceManager.GetString("AnInvalidSetOfArgumentsWasSpecifiedArgumentNames", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The argument {0} is not a positive number..
        /// </summary>
        internal static string ErrorArgumentNotPositive
        {
            get
            {
                return ResourceManager.GetString("ErrorArgumentNotPositive", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The argument {0} is not a positive integer..
        /// </summary>
        internal static string ErrorArgumentNotPositiveInteger
        {
            get
            {
                return ResourceManager.GetString("ErrorArgumentNotPositiveInteger", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The string argument {0} is null (Nothing in Visual Basic) or empty..
        /// </summary>
        internal static string ErrorArgumentNullOrEmpty
        {
            get
            {
                return ResourceManager.GetString("ErrorArgumentNullOrEmpty", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The string argument {0} is null (Nothing in Visual Basic), empty or whitespace-only..
        /// </summary>
        internal static string ErrorArgumentNullOrWhitespace
        {
            get
            {
                return ResourceManager.GetString("ErrorArgumentNullOrWhitespace", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The length should be small enough to not result in an operation outside of the bounds of the array..
        /// </summary>
        internal static string ErrorLengthGoesPastArrayLimits
        {
            get
            {
                return ResourceManager.GetString("ErrorLengthGoesPastArrayLimits", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The length should be a positive integer..
        /// </summary>
        internal static string ErrorLengthMustBeAPositiveInteger
        {
            get
            {
                return ResourceManager.GetString("ErrorLengthMustBeAPositiveInteger", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to This member is not implemented by design..
        /// </summary>
        internal static string ErrorNotImplementedByDesign
        {
            get
            {
                return ResourceManager.GetString("ErrorNotImplementedByDesign", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The source index must point to a location within the array..
        /// </summary>
        internal static string ErrorSourceIndexMustPointToALocationWithinTheArray
        {
            get
            {
                return ResourceManager.GetString("ErrorSourceIndexMustPointToALocationWithinTheArray", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The operation that has made a call to this method should stop retrying, as a critical error, a state change or unrecoverable environment conditions guarantee failure on subsequent retries..
        /// </summary>
        internal static string ErrorStopRetrying
        {
            get
            {
                return ResourceManager.GetString("ErrorStopRetrying", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The supplied boxed or polymorphic argument is of a wrong type..
        /// </summary>
        internal static string ErrorWrongArgumentType
        {
            get
            {
                return ResourceManager.GetString("ErrorWrongArgumentType", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The input sequence contains more than one element..
        /// </summary>
        internal static string SingleOrDefaultMultipleElements
        {
            get
            {
                return ResourceManager.GetString("SingleOrDefaultMultipleElements", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The provided arguments do not form a valid range of values. Arguments: {0}.
        /// </summary>
        internal static string TheProvidedArgumentsDoNotFormAValidRangeOfValuesArguments
        {
            get
            {
                return ResourceManager.GetString("TheProvidedArgumentsDoNotFormAValidRangeOfValuesArguments", resourceCulture);
            }
        }
    }
}
