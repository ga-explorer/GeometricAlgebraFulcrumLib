using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public abstract class LinFloat64RotationBase :
        LinFloat64ReflectionBase
    {
        public override bool SwapsHandedness 
            => false;
    
        public abstract LinFloat64RotationBase GetVectorRotationInverse();

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
        {
            return GetVectorRotationInverse();
        }

        public abstract LinFloat64VectorToVectorRotationSequence ToVectorToVectorRotationSequence();

        //public abstract SimpleRotationSequence ToSimpleVectorRotationSequence();
    }
}