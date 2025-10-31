using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsmini
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Text;

    /// <summary>
    /// Provides utility methods for parsing compiler errors and applying simple fixes to source code.
    /// </summary>
    public static class CodeFixer
    {
        // Regex to match the standard C# error format: (line,column): error CSxxxx: ... expected
        private const string ErrorLocationPattern = @"\((\d+),(\d+)\)";
        // Regex to extract the token that was expected (e.g., the ';' in '; expected')
        private const string ExpectedTokenPattern = @":\s*(.+)\s+expected";

        /// <summary>
        /// Attempts to parse a C# compiler error message and insert the expected character
        /// into the source code at the specified line and column.
        /// </summary>
        /// <param name="errorMessage">The error message string, e.g., "test.cs(30,34): error CS1002: ; expected"</param>
        /// <param name="sourceCode">The raw string content of the source code.</param>
        /// <returns>The modified source code string, or the original source code if parsing or insertion fails.</returns>
        public static string InsertExpectedChar(string errorMessage, string sourceCode)
        {
            Console.WriteLine($"Attempting to fix error: {errorMessage}");

            // 1. Extract Line and Column
            var locationMatch = Regex.Match(errorMessage, ErrorLocationPattern);
            if (!locationMatch.Success || locationMatch.Groups.Count < 3)
            {
                Console.WriteLine("Error: Failed to parse line and column location.");
                return sourceCode;
            }

            // Compiler messages are 1-based, convert to 0-based for list indexing
            if (!int.TryParse(locationMatch.Groups[1].Value, out int line) ||
                !int.TryParse(locationMatch.Groups[2].Value, out int column))
            {
                Console.WriteLine("Error: Line/column values are not valid integers.");
                return sourceCode;
            }

            // Adjust to 0-based indices
            int lineIndex = line - 1;
            // Column is the insertion point, which often means inserting AT that index (0-based)
            int columnIndex = column - 1;


            // 2. Extract Expected Token
            string expectedToken = "";
            var tokenMatch = Regex.Match(errorMessage, ExpectedTokenPattern, RegexOptions.IgnoreCase);

            if (tokenMatch.Success && tokenMatch.Groups.Count > 1)
            {
                // Capture the token, remove surrounding quotes if present, and trim whitespace
                expectedToken = tokenMatch.Groups[1].Value.Trim().Replace("'", "").Replace("\"", "");
            }

            if (string.IsNullOrEmpty(expectedToken))
            {
                Console.WriteLine("Error: Failed to extract the expected token.");
                return sourceCode;
            }

            Console.WriteLine($"Parsed Fix: Insert '{expectedToken}' at Line {line}, Column {column}.");


            // 3. Apply Fix to Source Code

            // Split the source code into individual lines
            var lines = new List<string>(sourceCode.Replace("\r\n", "\n").Split('\n'));

            if (lineIndex < 0 || lineIndex >= lines.Count)
            {
                Console.WriteLine($"Error: Line index ({lineIndex}) is out of bounds for {lines.Count} lines.");
                return sourceCode;
            }

            string targetLine = lines[lineIndex];

            if (columnIndex < 0 || columnIndex > targetLine.Length)
            {
                // If the column index is slightly beyond the end of the line (e.g., inserting a brace), adjust it.
                if (columnIndex == targetLine.Length)
                {
                    columnIndex = targetLine.Length;
                }
                else
                {
                    Console.WriteLine($"Error: Column index ({columnIndex}) is out of bounds for line length {targetLine.Length}.");
                    return sourceCode;
                }
            }

            // Insert the expected token into the target line string
            string fixedLine = targetLine.Insert(columnIndex, expectedToken.Split(' ')[2]);

            // Replace the original line with the fixed line
            lines[lineIndex] = fixedLine;

            // Join the lines back together with newlines
            return string.Join(Environment.NewLine, lines);
        }
    }
}
