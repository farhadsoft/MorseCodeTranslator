using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

#pragma warning disable S2368

namespace MorseCodeTranslator
{
    public static class Translator
    {
        public static string TranslateToMorse(string message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            StringBuilder result = new StringBuilder();
            WriteMorse(MorseCodes.CodeTable, message, result);

            return result.ToString();
        }

        public static string TranslateToText(string morseMessage)
        {
            if (morseMessage is null)
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            StringBuilder result = new StringBuilder();
            WriteText(MorseCodes.CodeTable, morseMessage, result);

            return result.ToString();
        }

        public static void WriteMorse(char[][] codeTable, string message, StringBuilder morseMessageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (codeTable is null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (morseMessageBuilder is null)
            {
                throw new ArgumentNullException(nameof(morseMessageBuilder));
            }

            foreach (var c in message)
            {
                int i = 0;
                if (char.IsLetter(c))
                {
                    while (char.ToUpper(c, CultureInfo.CurrentCulture) != codeTable[i][0])
                    {
                        i++;
                    }

                    if (i != message.Length && morseMessageBuilder.Length > 0)
                    {
                        morseMessageBuilder.Append(' ');
                    }

                    for (int j = 1; j < codeTable[i].Length; j++)
                    {
                        morseMessageBuilder.Append(codeTable[i][j]);
                    }
                }
            }

            if (dot != '.')
            {
                morseMessageBuilder.Replace('.', dot);
            }

            if (dash != '-')
            {
                morseMessageBuilder.Replace('-', dash);
            }

            if (separator != ' ')
            {
                morseMessageBuilder.Replace(' ', separator);
            }
        }

        public static void WriteText(char[][] codeTable, string morseMessage, StringBuilder messageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (codeTable is null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            if (morseMessage is null)
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            if (messageBuilder is null)
            {
                throw new ArgumentNullException(nameof(messageBuilder));
            }

            if (separator != ' ')
            {
                morseMessage = morseMessage.Replace(separator, ' ');
            }

            if (dot != '.')
            {
                morseMessage = morseMessage.Replace(dot, '.');
            }

            if (dash != '-')
            {
                morseMessage = morseMessage.Replace(dash, '-');
            }

            if (Regex.IsMatch(morseMessage, @"^[. -]+$"))
            {
                var morseCode = MorseCodes.CodeTable;
                string[] codes = morseMessage.Split(' ');
                for (int i = 0; i < codes.Length; i++)
                {
                    int j = 0;
                    string morse = new string(morseCode[j]);
                    while (morse[1..] != codes[i])
                    {
                        j++;
                        morse = new string(morseCode[j]);
                    }

                    messageBuilder.Append(morseCode[j][0]);
                }
            }
        }
    }
}
