namespace Flozacode.Exceptions
{
    public class CrudFailedException : Exception
    {
		public CrudFailedException(string message) : base(message)
		{
		}

		public CrudFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
