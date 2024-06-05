namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Operator;

/// <summary>
/// This interface is used for defining language operators
/// </summary>
public interface ILanguageOperator
{
    string OperatorName { get; }

    ILanguageOperator DuplicateOperator();
}