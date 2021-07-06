using GeometricAlgebraLib.CodeComposer.Composers;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.SymbolicExpressions;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    internal abstract class GaLibraryPartComposerBase : 
        GaClcCodePartComposerBase
    {
        internal IGaMultivectorProcessor<ISymbolicExpressionAtomic> MultivectorProcessor
            => DenseKVectorsLibraryComposer.MultivectorProcessor;

        internal int VSpaceDimension 
            => MultivectorProcessor.BasisSet.VSpaceDimension;

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
