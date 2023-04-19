using System.Collections.Generic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib
{
    internal abstract class GaFuLLibraryPartComposerBase : 
        GaFuLCodePartComposerBase
    {
        internal XGaProcessor<IMetaExpressionAtomic> GeometricProcessor
            => DenseKVectorsLibraryComposer.GeometricProcessor;

        internal XGaProcessor<IMetaExpressionAtomic> EuclideanProcessor 
            => DenseKVectorsLibraryComposer.EuclideanProcessor;

        public int VSpaceDimensions 
            => DenseKVectorsLibraryComposer.VSpaceDimensions;
        
        public int GradesCount 
            => VSpaceDimensions + 1;

        public IEnumerable<int> Grades 
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
