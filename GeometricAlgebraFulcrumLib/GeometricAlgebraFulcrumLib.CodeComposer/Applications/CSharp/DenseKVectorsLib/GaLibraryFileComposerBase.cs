using System.Collections.Generic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    public abstract class GaLibraryFileComposerBase : 
        GaClcCodeFileComposerBase, IGaSpace
    {
        internal IGaProcessor<ISymbolicExpressionAtomic> Processor 
            => DenseKVectorsLibraryComposer.Processor;

        internal IGaProcessor<ISymbolicExpressionAtomic> EuclideanProcessor 
            => DenseKVectorsLibraryComposer.EuclideanProcessor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        internal string CurrentNamespace 
            => DenseKVectorsLibraryComposer.CurrentNamespace;

        internal GaLibraryComposer DenseKVectorsLibraryComposer 
            => (GaLibraryComposer) LibraryComposer;


        internal GaLibraryFileComposerBase(GaLibraryComposer libGen)
            : base(libGen)
        {
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
                "signature", CurrentNamespace,
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
