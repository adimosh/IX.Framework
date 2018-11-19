// <copyright file="ExpressionParsingService.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using IX.Math.Extensibility;
using IX.Math.Extraction;
using IX.Math.Generators;
using IX.StandardExtensions;
using IX.StandardExtensions.ComponentModel;
using IX.System.Collections.Generic;

namespace IX.Math
{
    /// <summary>
    /// A service that is able to parse strings containing mathematical expressions and solve them.
    /// </summary>
    public sealed class ExpressionParsingService : DisposableBase, IExpressionParsingService
    {
        private MathDefinition workingDefinition;

        private List<Assembly> assembliesToRegister;

        private Dictionary<string, Type> nonaryFunctions;
        private Dictionary<string, Type> unaryFunctions;
        private Dictionary<string, Type> binaryFunctions;
        private Dictionary<string, Type> ternaryFunctions;

#pragma warning disable IDISP002 // Dispose member. - It is
#pragma warning disable IDISP006 // Implement IDisposable. - It is
        private LevelDictionary<Type, IConstantsExtractor> constantExtractors;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParsingService"/> class with a standard math definition object.
        /// </summary>
        public ExpressionParsingService()
            : this(new MathDefinition
            {
                Parentheses = new Tuple<string, string>("(", ")"),
                SpecialSymbolIndicators = new Tuple<string, string>("[", "]"),
                StringIndicator = "\"",
                ParameterSeparator = ",",
                AddSymbol = "+",
                AndSymbol = "&",
                DivideSymbol = "/",
                NotEqualsSymbol = "!=",
                EqualsSymbol = "=",
                MultiplySymbol = "*",
                NotSymbol = "!",
                OrSymbol = "|",
                PowerSymbol = "^",
                SubtractSymbol = "-",
                XorSymbol = "#",
                GreaterThanOrEqualSymbol = ">=",
                GreaterThanSymbol = ">",
                LessThanOrEqualSymbol = "<=",
                LessThanSymbol = "<",
                RightShiftSymbol = ">>",
                LeftShiftSymbol = "<<",
                OperatorPrecedenceStyle = OperatorPrecedenceStyle.Mathematical,
            })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParsingService"/> class with a specified math definition object.
        /// </summary>
        /// <param name="definition">The math definition to use.</param>
        public ExpressionParsingService(MathDefinition definition)
        {
            this.workingDefinition = definition;

            this.assembliesToRegister = new List<Assembly>
            {
                typeof(ExpressionParsingService).GetTypeInfo().Assembly,
            };
        }

        /// <summary>
        /// Interprets the mathematical expression and returns a container that can be invoked for solving using specific mathematical types.
        /// </summary>
        /// <param name="expression">The expression to interpret.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="ComputedExpression"/> that represents a compilable form of the original expression, if the expression itself makes sense.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is either null, empty or whitespace-only.</exception>
        public ComputedExpression Interpret(string expression, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            this.ThrowIfCurrentObjectDisposed();

            if (this.nonaryFunctions == null ||
                this.unaryFunctions == null ||
                this.binaryFunctions == null ||
                this.ternaryFunctions == null)
            {
                this.InitializeFunctionsDictionary();
            }

            if (this.constantExtractors == null)
            {
                this.InitializeExtractorsDictionary();
            }

            ComputedExpression result;
            using (var workingSet = new WorkingExpressionSet(
                expression,
                this.workingDefinition.DeepClone(),
                this.assembliesToRegister,
                this.nonaryFunctions,
                this.unaryFunctions,
                this.binaryFunctions,
                this.ternaryFunctions,
                this.constantExtractors,
                cancellationToken))
            {
                Tuple<Nodes.NodeBase, Registration.IParameterRegistry> body = ExpressionGenerator.CreateBody(workingSet);

                if (!workingSet.Success)
                {
                    result = new ComputedExpression(expression, null, false, null, null);
                }
                else
                {
                    result = new ComputedExpression(expression, body.Item1, true, body.Item2, this.workingDefinition.AutoConvertStringFormatSpecifier);
                }

                Interlocked.MemoryBarrier();
            }

            return result;
        }

        /// <summary>
        /// Returns the prototypes of all registered functions.
        /// </summary>
        /// <returns>All function names, with all possible combinations of input and output data.</returns>
        public string[] GetRegisteredFunctions()
        {
            this.ThrowIfCurrentObjectDisposed();

            if (this.nonaryFunctions == null ||
                this.unaryFunctions == null ||
                this.binaryFunctions == null ||
                this.ternaryFunctions == null)
            {
                this.InitializeFunctionsDictionary();
            }

            var bldr = new List<string>();

            foreach (KeyValuePair<string, Type> function in this.nonaryFunctions)
            {
                bldr.Add($"{function.Key}()");
            }

            foreach (KeyValuePair<string, Type> function in this.unaryFunctions)
            {
                foreach (ConstructorInfo constructor in function.Value.GetTypeInfo().DeclaredConstructors.ToArray())
                {
                    ParameterInfo[] parameters = constructor.GetParameters();

                    if (parameters.Length != 1)
                    {
                        continue;
                    }

                    var parameterName = parameters[0].Name;

                    if (parameterName == null)
                    {
                        continue;
                    }

                    bldr.Add($"{function.Key}({parameterName})");
                }
            }

            foreach (KeyValuePair<string, Type> function in this.binaryFunctions)
            {
                foreach (ConstructorInfo constructor in function.Value.GetTypeInfo().DeclaredConstructors.ToArray())
                {
                    ParameterInfo[] parameters = constructor.GetParameters();

                    if (parameters.Length != 2)
                    {
                        continue;
                    }

                    var parameterNameLeft = parameters[0].Name;
                    var parameterNameRight = parameters[1].Name;

                    if (parameterNameLeft == null || parameterNameRight == null)
                    {
                        continue;
                    }

                    bldr.Add($"{function.Key}({parameterNameLeft}, {parameterNameRight})");
                }
            }

            foreach (KeyValuePair<string, Type> function in this.ternaryFunctions)
            {
                foreach (ConstructorInfo constructor in function.Value.GetTypeInfo().DeclaredConstructors.ToArray())
                {
                    ParameterInfo[] parameters = constructor.GetParameters();

                    if (parameters.Length != 3)
                    {
                        continue;
                    }

                    var parameterNameLeft = parameters[0].Name;
                    var parameterNameMiddle = parameters[1].Name;
                    var parameterNameRight = parameters[2].Name;

                    if (parameterNameLeft == null || parameterNameMiddle == null || parameterNameRight == null)
                    {
                        continue;
                    }

                    bldr.Add($"{function.Key}({parameterNameLeft}, {parameterNameMiddle}, {parameterNameRight})");
                }
            }

            return bldr.ToArray();
        }

        /// <summary>
        /// Registers an assembly to extract compatible functions from.
        /// </summary>
        /// <param name="assembly">The assembly to register.</param>
        public void RegisterFunctionsAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            this.ThrowIfCurrentObjectDisposed();

            if (this.assembliesToRegister.Contains(assembly))
            {
                return;
            }

            this.assembliesToRegister.Add(assembly);

            this.nonaryFunctions?.Clear();
            this.unaryFunctions?.Clear();
            this.binaryFunctions?.Clear();
            this.ternaryFunctions?.Clear();
        }

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            this.nonaryFunctions?.Clear();
            this.unaryFunctions?.Clear();
            this.binaryFunctions?.Clear();
            this.ternaryFunctions?.Clear();
            this.assembliesToRegister?.Clear();

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Disposes in the general (managed and unmanaged) context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            base.DisposeGeneralContext();

            Interlocked.Exchange(ref this.nonaryFunctions, null);
            Interlocked.Exchange(ref this.unaryFunctions, null);
            Interlocked.Exchange(ref this.binaryFunctions, null);
            Interlocked.Exchange(ref this.ternaryFunctions, null);
            Interlocked.Exchange(ref this.assembliesToRegister, null);

            Interlocked.Exchange(ref this.workingDefinition, null);
        }

        private void InitializeFunctionsDictionary()
        {
            Interlocked.Exchange(
                ref this.nonaryFunctions,
                FunctionsDictionaryGenerator.GenerateInternalNonaryFunctionsDictionary(this.assembliesToRegister))?.Clear();

            Interlocked.Exchange(
                ref this.unaryFunctions,
                FunctionsDictionaryGenerator.GenerateInternalUnaryFunctionsDictionary(this.assembliesToRegister))?.Clear();

            Interlocked.Exchange(
                ref this.binaryFunctions,
                FunctionsDictionaryGenerator.GenerateInternalBinaryFunctionsDictionary(this.assembliesToRegister))?.Clear();

            Interlocked.Exchange(
                ref this.ternaryFunctions,
                FunctionsDictionaryGenerator.GenerateInternalTernaryFunctionsDictionary(this.assembliesToRegister))?.Clear();
        }

        private void InitializeExtractorsDictionary()
        {
#pragma warning disable IDISP003 // Dispose previous before re-assigning. - Not necessary, as the initializer checks beforehand
            this.constantExtractors = new LevelDictionary<Type, IConstantsExtractor>
            {
                { typeof(StringExtractor), new StringExtractor(), 1000 },
                { typeof(ScientificFormatNumberExtractor), new ScientificFormatNumberExtractor(), 2000 },
            };
#pragma warning restore IDISP003 // Dispose previous before re-assigning.

            var incrementer = 2001;
            this.assembliesToRegister
                .GetTypesAssignableFrom<IConstantsExtractor>()
                .Where(p => p.IsClass && !p.IsAbstract && !p.IsGenericTypeDefinition && p.HasPublicParameterlessConstructor())
                .Select(p => p.AsType())
                .Where((p, thisL1) => !thisL1.constantExtractors.ContainsKey(p), this)
                .ForEach(
                    (Type p, ref int i, ExpressionParsingService thisL1) =>
                    {
                        if (p.GetAttributeDataByTypeWithoutVersionBinding<ConstantsExtractorAttribute, int>(out var explicitLevel))
                        {
                            thisL1.constantExtractors.Add(p, (IConstantsExtractor)p.Instantiate(), explicitLevel);
                        }
                        else
                        {
                            thisL1.constantExtractors.Add(p, (IConstantsExtractor)p.Instantiate(), Interlocked.Increment(ref i));
                        }
                    },
                    ref incrementer,
                    this);
        }
    }
}