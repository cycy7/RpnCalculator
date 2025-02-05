namespace RpnCalculatorDomain
{
    public class RpnCalculatorException : Exception
    {
        public RpnExceptionType ExceptionType { get; }

        public RpnCalculatorException(RpnExceptionType exceptionType, string message)
            : base(message)
        {
            ExceptionType = exceptionType;
        }

        public RpnCalculatorException(RpnExceptionType exceptionType, string message, Exception innerException)
        : base(message, innerException)
        {
            ExceptionType = exceptionType;
        }

    }
}