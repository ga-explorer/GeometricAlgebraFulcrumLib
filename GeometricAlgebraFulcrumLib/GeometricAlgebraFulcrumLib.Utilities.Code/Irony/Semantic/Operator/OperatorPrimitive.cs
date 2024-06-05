namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Operator;

/// <summary>
/// This class represents a primitive language operator
/// </summary>
public class OperatorPrimitive : ILanguageOperator
{
    private readonly string _operatorName;

    /// <summary>
    /// The operator symbol
    /// </summary>
    public string OperatorSymbolString { get; }

    /// <summary>
    /// The operator name
    /// </summary>
    string ILanguageOperator.OperatorName => _operatorName;


    protected OperatorPrimitive(string opName, string opSymbolString)
    {
        _operatorName = opName;
        OperatorSymbolString = opSymbolString;
    }

    protected OperatorPrimitive(string opName)
        : this(opName, "")
    {
    }


    public ILanguageOperator DuplicateOperator()
    {
        return this;
    }


    public override string ToString()
    {
        return 
            string.IsNullOrEmpty(OperatorSymbolString) ? 
                _operatorName : 
                OperatorSymbolString;
    }


    public static OperatorPrimitive Create(string opName, string opSymbolString)
    {
        return new OperatorPrimitive(opName, opSymbolString);
    }

    public static OperatorPrimitive Create(string opName)
    {
        return new OperatorPrimitive(opName);
    }
}