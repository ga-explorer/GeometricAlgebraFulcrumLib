using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraLib.CodeComposer.LanguageServers;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Variables;

namespace GeometricAlgebraLib.CodeComposer.Composers
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