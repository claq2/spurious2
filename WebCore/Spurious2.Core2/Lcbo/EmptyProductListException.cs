namespace Spurious2.Core2.Lcbo;

public class EmptyProductListException : Exception
{
    public EmptyProductListException()
    {
    }

    public EmptyProductListException(string message) : base(message)
    {
    }

    public EmptyProductListException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
