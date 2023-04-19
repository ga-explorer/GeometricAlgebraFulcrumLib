using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Reflectors
{
    public abstract class RGaReflectorBase<T> : 
        RGaVersorBase<T>, 
        IRGaReflector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected RGaReflectorBase(RGaProcessor<T> processor)
            : base(processor)
        {
        }


        public abstract IRGaReflector<T> GetReflectorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaVersor<T> GetVersorInverse()
        {
            return GetReflectorInverse();
        }
    }
}