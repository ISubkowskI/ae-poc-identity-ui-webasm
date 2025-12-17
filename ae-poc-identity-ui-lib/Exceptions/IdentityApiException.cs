namespace Ae.Poc.Identity.Ui.Exceptions;

public class IdentityApiException : Exception
{
    public IdentityApiException(string message) : base(message)
    {
    }

    public IdentityApiException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
