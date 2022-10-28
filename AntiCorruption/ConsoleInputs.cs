using RhitmoPark.Application.Constants;

namespace RhitmoPark.AntiCorruption
{
    public static class ConsoleInputs
    {
        public static string ReadLine()
        {
            try
            {
                var consoleInput = Console.ReadLine();

                if (string.IsNullOrEmpty(consoleInput))
                {
                    throw new Exception(ApplicationErrorMessagesConstants.EmptyConsoleInput);
                }

                return consoleInput;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ReadLine();
            }
        }

        public static int IntReadLine()
        {
            try
            {
                var consoleInput = int.TryParse(Console.ReadLine(), out int parseResult);

                if (!consoleInput)
                {
                    throw new Exception(ApplicationErrorMessagesConstants.ConsoleInputNotInt);
                }

                return parseResult;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return IntReadLine();
            }
        }
    }
}
