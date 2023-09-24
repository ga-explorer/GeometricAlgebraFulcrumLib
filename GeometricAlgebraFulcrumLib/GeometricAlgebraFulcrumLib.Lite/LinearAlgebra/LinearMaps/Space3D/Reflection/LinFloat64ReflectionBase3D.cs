using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Reflection
{
    public abstract class LinFloat64ReflectionBase3D :
        ILinFloat64UnilinearMap3D
    {
        public int VSpaceDimensions 
            => 3;

        public abstract bool SwapsHandedness { get; }

        public abstract bool IsValid();

        public abstract bool IsIdentity();

        public abstract bool IsNearIdentity(double epsilon = 1e-12d);

        public abstract Float64Vector3D MapBasisVector(int basisIndex);
        
        public abstract Float64Vector3D MapVector(IFloat64Vector3D vector);
        
        public abstract LinFloat64ReflectionBase3D GetReflectionLinearMapInverse();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64UnilinearMap3D GetInverseMap()
        {
            return GetReflectionLinearMapInverse();
        }

        public abstract LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence();
    }
}