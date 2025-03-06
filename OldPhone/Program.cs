using System;

namespace OldPhonePad;

internal static class Program
{
    private static readonly IOldPhoneKeypad OldPhoneKeypad = new OldPhoneKeypad();

    public static void Main(string[] args)
    {
        Console.WriteLine("   Warmly Welcome,I am Khun Maung Ngwe & you can use old phone keypad now!  \n <>----------------------------------------------------------------------<>\n\n" +
            "       eg:please give T9 input like \'222 2 22#\', output will return \"CAB\" \n\n");
        while (true)
        {           
            Console.Write("Enter T9 input or 'exit' to quit : ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
                break;

            try
            {
                string output = OldPhonePad(input);
                Console.WriteLine($"OldPhoneKeypad(\"{input}\") => output: \"{output}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public static string OldPhonePad(string input)
    {
        return OldPhoneKeypad.ConvertKeypadInput(input);
    }
}
