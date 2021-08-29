using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public sealed class GaFuLSymbolicContextComputationCodeInfo
    {
        public ISymbolicVariableComputed ComputedVariable { get; internal init; }

        public string ExternalName 
            => ComputedVariable.ExternalName;

        public SteExpression RhsSimpleTextExpression { get; internal init; }

        public GaFuLLanguageServerBase LanguageServer { get; internal init; }

        public bool EnableCodeGeneration { get; set; }
    }
}