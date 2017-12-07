// <copyright file="ExpressionParsingService.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using IX.Math.Generators;
using IX.Math.Nodes.Constants;
using IX.Math.Nodes.Parameters;
using IX.Math.PlatformMitigation;

namespace IX.Math
{
    /// <summary>
    /// A service that is able to parse strings containing mathematical expressions and solve them.
    /// </summary>
    public sealed class ExpressionParsingService : IExpressionParsingService
    {
        private MathDefinition workingDefinition;

        private List<Assembly> assembliesToRegister;

        private Dictionary<string, Type> nonaryFunctions;
        private Dictionary<string, Type> unaryFunctions;
        private Dictionary<string, Type> binaryFunctions;
        private Dictionary<string, Type> ternaryFunctions;

        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParsingService"/> class with a standard math definition object.
        /// </summary>
        public ExpressionParsingService()
            : this(new MathDefinition
            {
                Parantheses = new Tuple<string, string>("(", ")"),
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
        /// Finalizes an instance of the <see cref="ExpressionParsingService"/> class.
        /// </summary>
        ~ExpressionParsingService()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Interprets the mathematical expression and returns a container that can be invoked for solving using specific mathematical types.
        /// </summary>
        /// <param name="expression">The expression to interpret.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>A <see cref="ComputedExpression"/> that represent</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is either null, empty or whitespace-only.</exception>
        public ComputedExpression Interpret(string expression, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            this.ThrowIfDisposed();

            if (this.nonaryFunctions == null ||
                this.unaryFunctions == null ||
                this.binaryFunctions == null ||
                this.ternaryFunctions == null)
            {
                this.InitializeFunctionsDictionary();
            }

            var workingSet = new WorkingExpressionSet(
                expression,
                this.workingDefinition,
                this.assembliesToRegister,
                this.nonaryFunctions,
                this.unaryFunctions,
                this.binaryFunctions,
                this.ternaryFunctions,
                cancellationToken);

            ExpressionGenerator.CreateBody(workingSet);

            if (!workingSet.Success)
            {
                return new ComputedExpression(expression, null, null, false);
            }
            else
            {
                return new ComputedExpression(expression, workingSet.Body, workingSet.ParametersTable.Values.ToArray(), true);
            }
        }

        /// <summary>
        /// Returns the prototypes of all registered functions.
        /// </summary>
        /// <returns>All function names, with all possible combinations of input and output data.</returns>
        public string[] GetRegisteredFunctions()
        {
            this.ThrowIfDisposed();

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
                foreach (ConstructorInfo constructor in function.Value.GetTypeConstructors())
                {
                    ParameterInfo[] parameters = constructor.GetParameters();

                    if (parameters.Length != 1)
                    {
                        continue;
                    }

                    var parameterName = this.GetParameterNode(parameters[0]);

                    if (parameterName == null)
                    {
                        continue;
                    }

                    bldr.Add($"{function.Key}({parameterName})");
                }
            }

            foreach (KeyValuePair<string, Type> function in this.binaryFunctions)
            {
                foreach (ConstructorInfo constructor in function.Value.GetTypeConstructors())
                {
                    ParameterInfo[] parameters = constructor.GetParameters();

                    if (parameters.Length != 2)
                    {
                        continue;
                    }

                    var parameterNameLeft = this.GetParameterNode(parameters[0]);
                    var parameterNameRight = this.GetParameterNode(parameters[1]);

                    if (parameterNameLeft == null || parameterNameRight == null)
                    {
                        continue;
                    }

                    bldr.Add($"{function.Key}({parameterNameLeft}, {parameterNameRight})");
                }
            }

            foreach (KeyValuePair<string, Type> function in this.ternaryFunctions)
            {
                foreach (ConstructorInfo constructor in function.Value.GetTypeConstructors())
                {
                    ParameterInfo[] parameters = constructor.GetParameters();

                    if (parameters.Length != 3)
                    {
                        continue;
                    }

                    var parameterNameLeft = this.GetParameterNode(parameters[0]);
                    var parameterNameMiddle = this.GetParameterNode(parameters[1]);
                    var parameterNameRight = this.GetParameterNode(parameters[2]);

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

            this.ThrowIfDisposed();

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
        /// Disposes of this instance of the <see cref="ExpressionParsingService"/> class.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of this instance of the <see cref="ExpressionParsingService"/> class.
        /// </summary>
        /// <param name="isManaged">Indicates whether or not this came from a <see cref="M:Dispose"/> call, or the destructor.</param>
        private void Dispose(bool isManaged)
        {
            if (isManaged)
            {
                this.nonaryFunctions?.Clear();
                this.unaryFunctions?.Clear();
                this.binaryFunctions?.Clear();
                this.ternaryFunctions?.Clear();
                this.assembliesToRegister?.Clear();
            }

            this.nonaryFunctions = null;
            this.unaryFunctions = null;
            this.binaryFunctions = null;
            this.ternaryFunctions = null;
            this.assembliesToRegister = null;

            this.workingDefinition = null;

            this.disposed = true;
        }

        /// <summary>
        /// Checks whether this instance of the <see cref="ExpressionParsingService"/> is disposed, and throws an exception if so.
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(typeof(ExpressionParsingService).FullName);
            }
        }

        private void InitializeFunctionsDictionary()
        {
            if (this.nonaryFunctions != null)
            {
                this.nonaryFunctions.Clear();
                this.nonaryFunctions = null;
            }

            if (this.unaryFunctions != null)
            {
                this.unaryFunctions.Clear();
                this.unaryFunctions = null;
            }

            if (this.binaryFunctions != null)
            {
                this.binaryFunctions.Clear();
                this.binaryFunctions = null;
            }

            if (this.ternaryFunctions != null)
            {
                this.ternaryFunctions.Clear();
                this.ternaryFunctions = null;
            }

            this.nonaryFunctions = FunctionsDictionaryGenerator.GenerateInternalNonaryFunctionsDictionary(this.assembliesToRegister);
            this.unaryFunctions = FunctionsDictionaryGenerator.GenerateInternalUnaryFunctionsDictionary(this.assembliesToRegister);
            this.binaryFunctions = FunctionsDictionaryGenerator.GenerateInternalBinaryFunctionsDictionary(this.assembliesToRegister);
            this.ternaryFunctions = FunctionsDictionaryGenerator.GenerateInternalTernaryFunctionsDictionary(this.assembliesToRegister);
        }

        private string GetParameterNode(ParameterInfo parameter)
        {
            if (parameter.ParameterType == typeof(BoolParameterNode) ||
                parameter.ParameterType == typeof(BoolNode))
            {
                return "boolean";
            }
            else if (parameter.ParameterType == typeof(NumericParameterNode) ||
                parameter.ParameterType == typeof(NumericNode))
            {
                return "numeric";
            }
            else if (parameter.ParameterType == typeof(StringParameterNode) ||
                parameter.ParameterType == typeof(StringNode))
            {
                return "string";
            }
            else
            {
                return null;
            }
        }
    }
}