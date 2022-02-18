namespace Cut.Data
{
    [System.Serializable]
    public class NoMoreSpaceException : System.Exception
    {
        public NoMoreSpaceException() { }
        public NoMoreSpaceException(string message) : base(message) { }
        public NoMoreSpaceException(string message, System.Exception inner) : base(message, inner) { }
        protected NoMoreSpaceException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}