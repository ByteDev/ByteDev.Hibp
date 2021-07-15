using System;
using System.Runtime.Serialization;

namespace ByteDev.Hibp
{
    /// <summary>
    /// Represents an exception from the HIBP client.
    /// </summary>
    [Serializable]
    public class HibpClientException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Hibp.HibpClientException" /> class.
        /// </summary>
        public HibpClientException() : base("Error occured within the HIBP client.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Hibp.HibpClientException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public HibpClientException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Hibp.HibpClientException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>       
        public HibpClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Hibp.HibpClientException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected HibpClientException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}