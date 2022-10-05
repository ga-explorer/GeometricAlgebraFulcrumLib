using System.Collections.Generic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib
{
    internal abstract class GaFuLLibraryPartComposerBase : 
        GaFuLCodePartComposerBase, IGeometricAlgebraSpace
    {
        internal IGeometricAlgebraProcessor<IMetaExpressionAtomic> GeometricProcessor
            => DenseKVectorsLibraryComposer.GeometricProcessor;

        internal IGeometricAlgebraProcessor<IMetaExpressionAtomic> EuclideanProcessor 
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


        internal GaFuLLibraryPartComposerBase(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }
    }
}
