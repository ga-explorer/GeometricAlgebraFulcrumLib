using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.CodeComposer.LanguageServers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Composers
{
    public sealed class GaClcComputationCodeInfo
    {
        public ISymbolicVariableComputed ComputedVariable { get; internal init; }

        public string ExternalName 
            => ComputedVariable.ExternalName;

        public SteExpression RhsSimpleTextExpression { get; internal init; }

        public GaClcLanguageServer LanguageServer { get; internal init; }

        public bool EnableCodeGeneration { get; set; }
    }
}