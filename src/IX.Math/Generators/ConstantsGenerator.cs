// <copyright file="ConstantsGenerator.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using IX.Math.Formatters;
using IX.Math.Nodes;
using IX.Math.Nodes.Constants;

namespace IX.Math.Generators
{
    internal static class ConstantsGenerator
    {
        public static string GenerateStringConstant(
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string originalExpression,
            string stringIndicator,
            string content)
        {
            if (reverseConstantsTable.TryGetValue(content, out string key))
            {
                return key;
            }
            else
            {
                var name = GenerateName(constantsTable.Keys, originalExpression);
                constantsTable.Add(name, new StringNode(content.Substring(stringIndicator.Length, content.Length - stringIndicator.Length)));
                reverseConstantsTable.Add(content, name);
                return name;
            }
        }

        public static string CheckAndAdd(
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string originalExpression,
            string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            if (reverseConstantsTable.TryGetValue(content, out string key))
            {
                return key;
            }
            else
            {
                if (ParsingFormatter.ParseNumeric(content, out object n))
                {
                    var name = GenerateName(constantsTable.Keys, originalExpression);
                    constantsTable.Add(name, new NumericNode(n));
                    reverseConstantsTable.Add(content, name);
                    return name;
                }
                else if (ParsingFormatter.ParseByteArray(content, out byte[] ba))
                {
                    var name = GenerateName(constantsTable.Keys, originalExpression);
                    constantsTable.Add(name, new ByteArrayNode(ba));
                    reverseConstantsTable.Add(content, name);
                    return name;
                }
                else if (bool.TryParse(content, out bool b))
                {
                    var name = GenerateName(constantsTable.Keys, originalExpression);
                    constantsTable.Add(name, new BoolNode(b));
                    reverseConstantsTable.Add(content, name);
                    return name;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void GenerateNamedNumericSymbol(
            IDictionary<string, ConstantNodeBase> constantsTable,
            IDictionary<string, string> reverseConstantsTable,
            string name,
            double value,
            params string[] alternateNames)
        {
            if (reverseConstantsTable.TryGetValue(name, out string key))
            {
                return;
            }
            else
            {
                constantsTable.Add(name, new NumericNode(value));
                reverseConstantsTable.Add(value.ToString(), name);

                foreach (var alternateName in alternateNames)
                {
                    reverseConstantsTable.Add(alternateName, name);
                }
            }
        }

        private static string GenerateName(IEnumerable<string> keys, string originalExpression)
        {
            var index = int.Parse(keys.Where(p => p.StartsWith("Const") && p.Length > 5).LastOrDefault()?.Substring(5) ?? "0");

            do
            {
                index++;
            }
            while (originalExpression.Contains($"Const{index}"));

            return $"Const{index}";
        }
    }
}