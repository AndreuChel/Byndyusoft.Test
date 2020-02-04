using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public abstract class ExpressionCalculatorException : Exception
    {
        public ExpressionCalculatorException(string message) : base(message) { }
    }

    public class InvalidExpressionException : ExpressionCalculatorException
    {
        public InvalidExpressionException(string message) : base(message) { }
    }

    public class InvalidResolverConfigureException : ExpressionCalculatorException
    {
        public InvalidResolverConfigureException(string message) : base(message) { }
    }

    public class EmptyResolverException : ExpressionCalculatorException
    {
        public EmptyResolverException() : base("") { }
    }

    public class UnsupportedElementTypeException : ExpressionCalculatorException
    {
        public UnsupportedElementTypeException(string message) : base(message) { }
    }

}
