using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePad
{
    public class OldPhoneKeypad : IOldPhoneKeypad
    {
        public string ConvertKeypadInput(string input)
        {
            if (!input.EndsWith("#"))
                throw new InvalidOperationException("Send key was not detected in input sequence.");

            var sb = new StringBuilder();
            char lastChar = '\0';
            int pressCount = 0;

            int sendIndex = input.IndexOf('#');
            string processedInput = sendIndex >= 0 ? input.Substring(0, sendIndex) : input;

            foreach (var currentChar in processedInput)
            {
                if (currentChar == ' ')
                {
                    if (lastChar != '\0')
                    {
                        sb.Append(GetCharacterFromKey(lastChar, pressCount));
                        lastChar = '\0';
                        pressCount = 0;
                    }
                }
                else if (currentChar == '*')
                {
                    if (lastChar != '\0')
                    {
                        sb.Append(GetCharacterFromKey(lastChar, pressCount));
                        lastChar = '\0';
                        pressCount = 0;
                    }

                    if (sb.Length > 0)
                    {
                        sb.Length--;
                    }
                }
                else if (char.IsDigit(currentChar))
                {
                    if (lastChar == currentChar)
                    {
                        pressCount++;
                    }
                    else
                    {
                        if (lastChar != '\0')
                        {
                            sb.Append(GetCharacterFromKey(lastChar, pressCount));
                        }

                        lastChar = currentChar;
                        pressCount = 1;
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Invalid character '{currentChar}' in input.");
                }
            }

            if (lastChar != '\0')
            {
                sb.Append(GetCharacterFromKey(lastChar, pressCount));
            }

            return sb.ToString();
        }

        private static char GetCharacterFromKey(char key, int count)
        {
            var keyStr = key.ToString();
            if (!Keypad.ContainsKey(keyStr))
                throw new InvalidOperationException($"Key '{key}' is not supported.");

            var keyChars = Keypad[keyStr];
            int index = (count - 1) % keyChars.Length;
            return keyChars[index];
        }

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