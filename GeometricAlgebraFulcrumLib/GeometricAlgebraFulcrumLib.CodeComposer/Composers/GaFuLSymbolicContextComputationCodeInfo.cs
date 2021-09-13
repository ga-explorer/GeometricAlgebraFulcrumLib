using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra.Variables;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;

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