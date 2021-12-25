using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public abstract class ScaledRotorBase<T> : 
        VersorBase<T>, 
        IScaledRotor<T>
    {
        protected ScaledRotorBase([NotNull] IGeometricAlgebraProcessor<T> processor)
            : base(processor)
        {
        }


        public abstract T GetScalingFactor();

        public abstract IScaledRotor<T> GetScaledRotorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return GetScaledRotorInverse();
        }
    }
}