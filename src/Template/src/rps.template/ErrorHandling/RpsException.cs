namespace rps.template.ErrorHandling
{
    using System;

    public class RpsException : Exception
    {
        public RpsException(string message) : base(message)
        {   
        }
    }
}