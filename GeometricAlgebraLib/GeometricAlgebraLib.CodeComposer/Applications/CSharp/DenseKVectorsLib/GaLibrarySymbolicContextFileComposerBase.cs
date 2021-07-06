using GeometricAlgebraLib.CodeComposer.Composers;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Processing.SymbolicExpressions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    public abstract class GaLibrarySymbolicContextFileComposerBase
        : GaClcSymbolicContextCodeFileComposerBase
    {
        internal IGaScalarProcessor<ISymbolicExpressionAtomic> ScalarProcessor
            => DenseKVectorsLibraryComposer.MultivectorProcessor.ScalarProcessor;

        internal IGaMultivectorProcessor<ISymbolicExpressionAtomic> MultivectorProcessor
            => DenseKVectorsLibraryComposer.MultivectorProcessor;

        internal int VSpaceDimension 
            => VSpaceDimension;

        internal string CurrentNamespace 
            => DenseKVectorsLibraryComposer.CurrentNamespace;

        internal GaLibraryComposer DenseKVectorsLibraryComposer 
            => (GaLibraryComposer) LibraryComposer;


        internal GaLibrarySymbolicContextFileComposerBase(GaLibraryComposer libGen)
            : base(libGen)
        {
        }
        

        internal void GenerateBladeFileStartCode()
        {
            TextComposer.AppendLine(
                Templates["kvector_file_start"].GenerateUsing(CurrentNamespace)
            );

            TextComposer.IncreaseIndentation();
            TextComposer.IncreaseIndentation();
        }

        internal void GenerateBladeFileFinishCode()
        {
            TextComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("}")
                .DecreaseIndentation()
                .AppendLineAtNewLine("}");
        }

        internal void GenerateOutermorphismFileStartCode()
        {
            TextComposer.AppendLine(
                Templates["om_file_start"],
                "frame", CurrentNamespace,
                "grade", VSpaceDimension
            );

            TextComposer.IncreaseIndentation();
            TextComposer.IncreaseIndentation();
        }

        internal void GenerateOutermorphismFileFinishCode()
        {
            TextComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("}")
                .DecreaseIndentation()
                .AppendLineAtNewLine("}");
        }

        internal void GenerateBeginRegion(string regionText)
        {
            TextComposer.AppendLineAtNewLine(@"#region " + regionText);
        }

        internal void GenerateEndRegion()
        {
            TextComposer.AppendLineAtNewLine(@"#endregion");
        }
    }
}
