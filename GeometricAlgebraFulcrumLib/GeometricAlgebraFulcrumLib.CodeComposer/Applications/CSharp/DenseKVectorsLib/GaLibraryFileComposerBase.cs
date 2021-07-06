using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    public abstract class GaLibraryFileComposerBase : 
        GaClcCodeFileComposerBase
    {
        internal IGaMultivectorProcessor<ISymbolicExpressionAtomic> MultivectorProcessor { get; }

        internal int VSpaceDimension 
            => MultivectorProcessor.BasisSet.VSpaceDimension;

        internal string CurrentNamespace { get; }

        internal GaLibraryComposer DenseKVectorsLibraryComposer 
            => (GaLibraryComposer) LibraryComposer;


        internal GaLibraryFileComposerBase(GaLibraryComposer libGen)
            : base(libGen)
        {
            MultivectorProcessor = libGen.MultivectorProcessor;
            CurrentNamespace = libGen.CurrentNamespace;
        }


        internal void GenerateKVectorFileStartCode()
        {
            TextComposer.AppendLine(
                Templates["kvector_file_start"].GenerateUsing(CurrentNamespace)
            );

            TextComposer.IncreaseIndentation();
            TextComposer.IncreaseIndentation();
        }

        internal void GenerateKVectorFileFinishCode()
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
