using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Reflectors
{
    public abstract class ReflectorBase<T> : 
        VersorBase<T>, 
        IReflector<T>
    {
        protected ReflectorBase([NotNull] IGeometricAlgebraProcessor<T> processor)
            : base(processor)
        {
        }


        public abstract IReflector<T> GetReflectorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return GetReflectorInverse();
        }
    }
}