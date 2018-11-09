using System;
using System.Runtime.Serialization;

namespace ByteDev.Hibp
{
    [Serializable]
    public class HibpClientException : Exception
    {
        public HibpClientException()
        {
        }

        public HibpClientException(string message) : base(message)
        {
        }

        public HibpClientException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HibpClientException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}