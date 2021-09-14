using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public abstract class RotorBase<T> : 
        VersorBase<T>, 
        IRotor<T>
    {
        protected RotorBase([NotNull] IGeometricAlgebraProcessor<T> processor)
            : base(processor)
        {
        }


        public abstract IRotor<T> GetRotorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return GetRotorInverse();
        }
    }
}