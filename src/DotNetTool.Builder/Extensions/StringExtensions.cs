using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Argument.Check;


namespace DotNetTool.Builder.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsNotAnyOf(this string source, params string[] notContainStrings)
        {
            return !notContainStrings.Any(source.Contains);
        }

        public static bool ContainsAnyOf(this string source, params string[] notContainStrings)
        {
            return notContainStrings.Any(source.Contains);
        }

        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };
        }

        public static string FirstCharToLower(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToLower() + input.Substring(1)
            };
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNotNullOrEmpty(this string source)
        {
            return !source.IsNullOrEmpty();
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1820:TestForEmptyStringsUsingStringLength",
            Justification = "We want to check exactly only string.empty")]
        public static bool IsEmpty(this string source)
        {
            return source == string.Empty;
        }

        public static bool IsNotEmpty(this string source)
        {
            return !source.IsEmpty();
        }

        public static bool StartWith(this string source, string value)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => value);

            return source.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EndWith(this string source, string value)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => value);

            var result = source.EndsWith(value, StringComparison.OrdinalIgnoreCase);

            return result;
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool IsNotNullOrWhiteSpace(this string source)
        {
            return source.IsNullOrWhiteSpace().IsFalse();
        }

        public static bool IsWhitespace(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            return string.IsNullOrWhiteSpace(source);
        }

        public static bool ToBool(this string value)
        {
            Throw.IfNull(() => value);

            bool boolValue;

            if (bool.TryParse(value, out boolValue))
            {
                return boolValue;
            }

            var exceptionText = string.Format(CultureInfo.InvariantCulture, "Value {0} cannot be converted to a boolean value.", value);

            throw new ArgumentException(exceptionText, nameof(value));
        }

        public static bool ToBoolOrDefault(this string value)
        {
            if (value == null)
            {
                return false;
            }

            bool boolValue;

            return bool.TryParse(value, out boolValue) && boolValue;
        }

        public static bool IsValid(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return true;
        }

        public static bool IsNotValid(this string source)
        {
            return !source.IsValid();
        }

        public static DateTime ToDateTime(this string source)
        {
            Throw.IfNullOrWhiteSpace(() => source);

            return source.ToDateTime(DateTimeFormatInfo.InvariantInfo);
        }

        public static DateTime ToDateTime(this string source, DateTimeFormatInfo dateTimeFormatInfo)
        {
            Throw.IfNullOrWhiteSpace(() => source);
            Throw.IfNull(() => dateTimeFormatInfo);

            return source.ToDateTime(DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "It is as expected.")]
        public static DateTime ToDateTime(this string source, DateTimeFormatInfo dateTimeFormatInfo, DateTimeStyles dateTimeStyles)
        {
            Throw.IfNullOrWhiteSpace(() => source);

            DateTime dateTime;
            var result = DateTime.TryParse(source, dateTimeFormatInfo, dateTimeStyles, out dateTime);

            if (!result)
            {
                throw new InvalidOperationException(string.Format(dateTimeFormatInfo, "Can not parse string: {0} to type DateTime", source));
            }

            return dateTime;
        }

        public static string FormatInvariantCulture(this string source, params object[] formatArguments)
        {
            Throw.IfNullOrWhiteSpace(() => source);
            Throw.IfNull(() => formatArguments);

            var formatedString = string.Format(CultureInfo.InvariantCulture, source, formatArguments);

            return formatedString;
        }

        public static IEnumerable<string> SplitBlocks(this string value, int expectedBlocks, int blockLength)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            if (value.Length.NotEqualsTo(expectedBlocks.MultiplyBy(blockLength)))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Can not split string in expected blocks, because of string length: '{0}' which is not equals as the product of expected blocks: '{1}' and bloock length: '{2}'",
                        value.Length,
                        expectedBlocks,
                        blockLength),
                    nameof(value));
            }

            for (var i = 0; i < expectedBlocks; i++)
            {
                yield return value.Substring(i.MultiplyBy(blockLength), blockLength);
            }
        }

        public static IEnumerable<string> Split(this string value, int blockLength)
        {
            if (value.IsNotValid())
            {
                yield break;
            }

            if (blockLength.IsLessOrEqual(default))
            {
                throw new ArgumentException("The length of a block must not be 0 or smaller");
            }

            var expectedBlocks = value.Length.DivideBy(blockLength.ToDouble()).Ceiling();

            for (var i = 0; i < expectedBlocks; i++)
            {
                yield return value.SubstringUpTo(i.MultiplyBy(blockLength), blockLength);
            }
        }

        public static string SubstringUpTo(this string value, int length)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            return value.SubstringUpTo(0, length);
        }

        public static string SubstringUpTo(this string value, int startIndex, int length)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var stringLength = value.Length;
            var maxLength = startIndex.Plus(length).IsLessOrEqual(stringLength) ? length : stringLength - startIndex;

            return value.Substring(startIndex, maxLength);
        }

        public static string NormalizeTo(this string hexCode, int normalizedLength, string placeHolder)
        {
            Throw.IfNullOrWhiteSpace(() => hexCode);

            if (normalizedLength.IsLessOrEqual(default))
            {
                throw new ArgumentException("The length of a block must not be 0 or smaller");
            }

            Throw.IfNullOrWhiteSpace(() => placeHolder);

            return placeHolder.Repeat(normalizedLength.Minus(hexCode.Length)).ConcatWith(hexCode);
        }

        public static string Repeat(this string source, int count)
        {
            Throw.IfNull(() => source);
            Throw.IfLessThan(() => count, 0);

            return Enumerable.Repeat(source, count).Flatten();
        }

        public static string ConcatWith(this string value, params string[] values)
        {
            Throw.IfNull(() => values);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(value);
            values.ForEach(s => stringBuilder.Append(s));

            return stringBuilder.ToString();
        }

        public static bool IsValidHexString(this string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var blocks = value.Split(8);

            return blocks.All(IsHexCodeInternal);
        }

        public static bool IsNotValidHexString(this string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            return !value.IsValidHexString();
        }

        public static bool IsValidBinaryString(this string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            return value.All(character => character.EqualsTo('0') || character.EqualsTo('1'));
        }

        public static bool IsNotValidBinaryString(this string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            return !value.IsValidBinaryString();
        }

        public static Type FromAssembly(this string fullqualifiedTypeName, string fullQualifiedAssemblyName)
        {
            Throw.IfNullOrWhiteSpace(() => fullqualifiedTypeName);
            Throw.IfNullOrWhiteSpace(() => fullQualifiedAssemblyName);

            return Type.GetType(fullqualifiedTypeName + ", " + fullQualifiedAssemblyName);
        }

        public static bool EqualsToIgnoringCase(this string value1, string value2)
        {
            Throw.IfNull(() => value1);
            Throw.IfNull(() => value2);

            return value1.ToUpperInvariant().EqualsTo(value2.ToUpperInvariant());
        }

        public static bool IsNotValidIpAdress(this string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            return value.IsValidIpAdress().IsFalse();
        }

        public static IPAddress ToIpAddress(this string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            IPAddress address;

            if (IPAddress.TryParse(value, out address))
            {
                return address;
            }

            return null;
        }

        public static bool DoesNotContain(this string value, string notExpected)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            return !value.Contains(notExpected);
        }

        private static bool IsValidIpAdress(this string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            return value.ToIpAddress().IsNotNull();
        }

        private static bool IsHexCodeInternal(this string value)
        {
            int result;

            return int.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
        }
    }
}
