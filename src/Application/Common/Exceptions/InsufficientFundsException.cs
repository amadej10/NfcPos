using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfcPos.Application.Common.Exceptions;
public class InsufficientFundsException : Exception
{
    public InsufficientFundsException()
        : base()
    {
    }

    public InsufficientFundsException(string message)
        : base(message)
    {
    }

    public InsufficientFundsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public InsufficientFundsException(string id, decimal payAmount, decimal newBalance)
        : base($"User with id: \"{id}\" has insufficient funds. Payment of this amount: \"{payAmount}\" would result in this balance:  \"{newBalance}\".")
    {
    }
}
