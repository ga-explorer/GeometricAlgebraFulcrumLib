namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.DSLException;

public class AstGeneratorException : CompilerException
{
    public AstGeneratorException(string message)
        : base(message)
    {
    }

    public AstGeneratorException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}