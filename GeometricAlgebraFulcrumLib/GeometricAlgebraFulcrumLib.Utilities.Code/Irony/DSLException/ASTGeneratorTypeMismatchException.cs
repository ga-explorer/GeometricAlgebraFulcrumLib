namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.DSLException;

public class AstGeneratorTypeMismatchException : AstGeneratorException
{
    public AstGeneratorTypeMismatchException(string message)
        : base(message)
    {
    }

    public AstGeneratorTypeMismatchException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}