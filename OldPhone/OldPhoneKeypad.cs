using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePad
{
        //summary:
        //  Implements conversion functionality for old phone keypad input sequences.
    public class OldPhoneKeypad : IOldPhoneKeypad
    {
        // Summary:
        //   This method converts a string representing multi-tap phone keypad input into the corresponding text output.
        //   The input simulates a traditional phone keypad where each digit (2-9) corresponds to a set of letters.
        //   Consecutive presses of the same key cycle through the letters associated with that key.
        //   Spaces are used to separate presses of the same key when entering different characters.
        //   '*' acts as a backspace to remove the last character entered.
        //   The input must end with '#' to indicate the end of input.
        
        // Parameters:
        //   input - The input string containing digits (0-9), spaces, '*', and ending with '#'.
        //           The sequence before the first '#' must consist only of digits, spaces, and '*'.
        //           The input must end with '#' to be valid.
        
        // Returns:
        //   A string representing the text output after processing the keypad input.
        //
        // Exceptions:
        //   InvalidOperationException - Thrown if:
        //     - The input does not end with '#'.
        //     - The sequence before the first '#' contains characters other than digits (0-9), spaces, and '*'.
        public string ConvertKeypadInput(string input)
        {
            if (!input.EndsWith("#"))
                throw new InvalidOperationException("Input must end with send key '#'.");

            StringBuilder outputBuilder = new StringBuilder();
            char? currentKey = null;
            int keyPressCount = 0;

            int sendKeyIndex = input.IndexOf('#');
            string processedInput = sendKeyIndex >= 0 ?
                input.Substring(0, sendKeyIndex) :
                input;

            foreach (char currentChar in processedInput)
            {
                if (currentChar == ' ')
                {
                    if (currentKey.HasValue)
                    {
                        AppendCurrentCharacter(outputBuilder, currentKey.Value, keyPressCount);
                        currentKey = null;
                        keyPressCount = 0;
                    }
                }
                else if (currentChar == '*')
                {
                    if (currentKey.HasValue)
                    {
                        AppendCurrentCharacter(outputBuilder, currentKey.Value, keyPressCount);
                        currentKey = null;
                        keyPressCount = 0;
                    }

                    if (outputBuilder.Length > 0)
                    {
                        outputBuilder.Length--;
                    }
                }
                else if (char.IsDigit(currentChar))
                {
                    if (currentKey == currentChar)
                    {
                        keyPressCount++;
                    }
                    else
                    {
                        if (currentKey.HasValue)
                        {
                            AppendCurrentCharacter(outputBuilder, currentKey.Value, keyPressCount);
                        }
                        currentKey = currentChar;
                        keyPressCount = 1;
                    }
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Invalid character '{currentChar}' in input. Only digits 0-9, *, #, and spaces are allowed.");
                }
            }

            if (currentKey.HasValue)
            {
                AppendCurrentCharacter(outputBuilder, currentKey.Value, keyPressCount);
            }

            return outputBuilder.ToString();
        }

        //summary:
        //  Maps a key and press count to its corresponding character.
        //  param name "key" is Numeric key character.
        //  param name "count" is Number of presses.

        //returns
        //  Mapped character
        //  exception "InvalidOperationException" is Throws for unsupported keys.
        private static char GetCharacterFromKey(char key, int count)
        {
            string keyString = key.ToString();

            if (!Keypad.ContainsKey(keyString))
                throw new InvalidOperationException($"Key '{key}' is not supported by the keypad.");

            char[] keyCharacters = Keypad[keyString];
            int characterIndex = (count - 1) % keyCharacters.Length;
            return keyCharacters[characterIndex];
        }

        //summary:
        //  Helper method to safely append characters to the output buffer.
        private static void AppendCurrentCharacter(StringBuilder outputBuilder, char key, int count)
        {
            char character = GetCharacterFromKey(key, count);
            outputBuilder.Append(character);
        }

       //summary:
       //  T9 keypad character mapping following standard phone keypad layout.

        private static readonly Dictionary<string, char[]> Keypad = new()
        {
            { "1", new[] { '&', '\'', '(' } },      
            { "2", new[] { 'A', 'B', 'C' } },       
            { "3", new[] { 'D', 'E', 'F' } },       
            { "4", new[] { 'G', 'H', 'I' } },       
            { "5", new[] { 'J', 'K', 'L' } },       
            { "6", new[] { 'M', 'N', 'O' } },       
            { "7", new[] { 'P', 'Q', 'R', 'S' } },  
            { "8", new[] { 'T', 'U', 'V' } },      
            { "9", new[] { 'W', 'X', 'Y', 'Z' } }, 
            { "0", new[] { ' ' } }                  
        };
    }
}