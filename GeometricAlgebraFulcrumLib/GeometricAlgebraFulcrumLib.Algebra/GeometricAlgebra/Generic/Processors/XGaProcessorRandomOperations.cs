using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors
{
    public partial class XGaProcessor<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaRandomComposer<T> CreateXGaRandomComposer(int vSpaceDimensions)
        {
            return new XGaRandomComposer<T>(this, vSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaRandomComposer<T> CreateXGaRandomComposer(int vSpaceDimensions, int seed)
        {
            return new XGaRandomComposer<T>(this, vSpaceDimensions, seed);
        }


    }
}
