using RpnCalculatorDomain;

namespace RpnCalculatorConsole;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Welcome to Reverse Polish Notation calculator console app!");
        Console.WriteLine("Enter the expression you want to evaluate or type Q/q to quit.");
        var rpnCalculator = new RpnCalculator();

        while (true)
        {
            string input = Console.ReadLine();
            if (input.ToUpper() == "Q")
                break;
            try
            {
                var result = rpnCalculator.Process(input);
                Console.WriteLine($"Te result of processing your input {input} with RPN is {result}");
            }
            catch (RpnCalculatorException exception)
            {
                Console.WriteLine($"Processing your input returned an exception {exception.Message}");
            }
        }
    }
}

