namespace DocGen.Common.CustomClases
{
    public class EntityDoNotExistsException : Exception
    {
        public EntityDoNotExistsException() { }

        public EntityDoNotExistsException(string message)
            : base(message) { }
    }
}
