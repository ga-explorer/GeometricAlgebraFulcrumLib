using System.Collections.Generic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    public abstract class GaFuLLibrarySymbolicContextFileComposerBase
        : GaFuLSymbolicContextCodeFileComposerBase, IGeometricAlgebraSpace
    {
        internal IGeometricAlgebraProcessor<ISymbolicExpressionAtomic> GeometricProcessor
            => DenseKVectorsLibraryComposer.GeometricProcessor;

        internal IGeometricAlgebraProcessor<ISymbolicExpressionAtomic> EuclideanProcessor 
            => DenseKVectorsLibraryComposer.EuclideanProcessor;

        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

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

        internal GaFuLLibraryComposer DenseKVectorsLibraryComposer 
            => (GaFuLLibraryComposer) CodeComposer;


        internal GaFuLLibrarySymbolicContextFileComposerBase(GaFuLLibraryComposer libGen)
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
