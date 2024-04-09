using System.Runtime.Serialization;

namespace InfSecLab
{
    [Serializable]
    internal class InvalidInputKeyTextBoxException : Exception
    {
        public InvalidInputKeyTextBoxException()
        {
        }

        public InvalidInputKeyTextBoxException(string? message) : base(message)
        {
        }

        public InvalidInputKeyTextBoxException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidInputKeyTextBoxException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}