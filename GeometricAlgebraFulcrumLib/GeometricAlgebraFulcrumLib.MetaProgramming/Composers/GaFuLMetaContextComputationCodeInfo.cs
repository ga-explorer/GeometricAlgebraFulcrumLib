using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers;

public sealed class GaFuLMetaContextComputationCodeInfo
{
    public IMetaExpressionVariableComputed ComputedVariable { get; internal init; }

    public string ExternalName 
        => ComputedVariable.ExternalName;
    
    public string OutputExternalName 
        => ComputedVariable.OutputExternalName;

    public SteExpression RhsSimpleTextExpression { get; internal init; }

    public GaFuLLanguageServerBase LanguageServer { get; internal init; }

    public bool EnableCodeGeneration { get; set; }
}