namespace SuperHeroes.Core.Exceptions;

/// <summary>
/// Represents a read exception for 3rd party integrations
/// </summary>
public class IntegrationReadException : Exception
{
    public IntegrationReadException(string msg) : base(msg)
    {
    }
}
