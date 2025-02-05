using System.Linq.Expressions;
using System.Xml.Linq;

namespace RpnCalculatorDomain
{
    public class RpnCalculator
    {

        public decimal Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new RpnCalculatorException(RpnExceptionType.InvalidExpression, "Expression cannot be empty.");

            var stack = new Stack<decimal>();
            var elements = input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var element in elements)
            {
                if (element.Length > 29)
                {
                    throw new RpnCalculatorException(RpnExceptionType.OutOfRange, $"Number too large: {element}.");
                }
                if (decimal.TryParse(element, out decimal number))
                {
                    stack.Push(number);
                }
                else if (IsOperator(element))
                {
                    if (stack.Count < 2)
                        throw new RpnCalculatorException(RpnExceptionType.InvalidExpression, "Invalid expression: insufficient operands.");

                    decimal rightOperand = stack.Pop();
                    decimal leftOperand = stack.Pop();
                    stack.Push(ApplyOperator(leftOperand, rightOperand, element));
                }
                else
                {
                    throw new RpnCalculatorException(RpnExceptionType.InvalidExpression, $"Invalid character in input: {element}."); 
                }
            }
            if (stack.Count != 1)
                throw new RpnCalculatorException(RpnExceptionType.InvalidExpression, "Invalid expression: too many operands left.");

            return stack.Pop();
        }

        private bool IsOperator(string element)
        {
            string[] operators = { "+", "-", "*", "/", "%"};
            return operators.Contains(element);
        }

        private decimal ApplyOperator(decimal leftoperand, decimal rightoperand, string Operator)
        {
            decimal Result = decimal.MinusOne;
            if (Operator == "+") Result = leftoperand + rightoperand;
            if (Operator == "-") Result = leftoperand - rightoperand;
            if (Operator == "*") Result =  leftoperand * rightoperand;
            if (Operator == "/")
            {
                if (rightoperand == 0) throw new RpnCalculatorException(RpnExceptionType.DivisionByZero,"Division by zero is not allowed.");
                Result = leftoperand / rightoperand;
            }
            if (Operator == "%")
            {
                if (rightoperand == 0) throw new RpnCalculatorException(RpnExceptionType.DivisionByZero,"Division by zero is not allowed.");
                Result = leftoperand % rightoperand;
            }
            return Result;
        
        }
    }
}
