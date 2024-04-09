using System.Runtime.Serialization;

namespace InfSecLab
{
    [Serializable]
    internal class InvalidOffsetRangeTextBoxException : Exception
    {
        public InvalidOffsetRangeTextBoxException()
        {
        }

        public InvalidOffsetRangeTextBoxException(string? message) : base(message)
        {
        }

        public InvalidOffsetRangeTextBoxException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidOffsetRangeTextBoxException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}