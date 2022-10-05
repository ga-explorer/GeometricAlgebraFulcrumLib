using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Variables;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Composers
{
    public sealed class GaFuLMetaContextComputationCodeInfo
    {
        public IMetaExpressionVariableComputed ComputedVariable { get; internal init; }

        public string ExternalName 
            => ComputedVariable.ExternalName;

        public SteExpression RhsSimpleTextExpression { get; internal init; }

        public GaFuLLanguageServerBase LanguageServer { get; internal init; }

        public bool EnableCodeGeneration { get; set; }
    }
}