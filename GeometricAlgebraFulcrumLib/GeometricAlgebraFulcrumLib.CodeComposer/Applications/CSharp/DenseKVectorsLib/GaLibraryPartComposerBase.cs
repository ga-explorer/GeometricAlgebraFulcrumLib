using System.Collections.Generic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    internal abstract class GaLibraryPartComposerBase : 
        GaClcCodePartComposerBase, IGaSpace
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


        internal GaLibraryPartComposerBase(GaLibraryComposer libGen)
            : base(libGen)
        {
        }
    }
}
