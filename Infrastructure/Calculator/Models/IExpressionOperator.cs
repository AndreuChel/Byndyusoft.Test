using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Const;

namespace Calculator.Models
{
    public interface IExpressionOperator : IExpressionOperation
    {
        OperationType OperationType { get; }
    }

    public interface IExpressionOperator<T> : IExpressionOperator
    { 
        T Calculate (T operand1, T operand2 = default(T));
    }

    public interface ICanCahngeOperationType
    {
        void SetOperationType(OperationType newType);
    }

}
