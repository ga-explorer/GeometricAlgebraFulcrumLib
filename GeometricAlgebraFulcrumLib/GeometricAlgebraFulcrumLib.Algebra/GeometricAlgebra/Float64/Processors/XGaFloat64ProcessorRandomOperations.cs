using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors
{
    public partial class XGaFloat64Processor
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64RandomComposer CreateXGaRandomComposer(int vSpaceDimensions)
        {
            return new XGaFloat64RandomComposer(this, vSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64RandomComposer CreateXGaRandomComposer(int vSpaceDimensions, int seed)
        {
            return new XGaFloat64RandomComposer(this, vSpaceDimensions, seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64RandomComposer CreateXGaRandomComposer(int vSpaceDimensions, Random randomGenerator)
        {
            return new XGaFloat64RandomComposer(this, vSpaceDimensions, randomGenerator);
        }



    }
}
