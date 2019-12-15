namespace Trumpf.Hmi.Uif.ErrorHandling
{
    using System;

    public class UifException : Exception
    {
        public UifException(string message) : base(message)
        {   
        }
    }
}