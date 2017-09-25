using System;

namespace CarStore.Exceptions
{
    public class CarNotFoundException : BaseException
    {
        public CarNotFoundException(string message) : base(message)
        {
            
        }

        public CarNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
