using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64
{
    public static class Float64LinearAlgebraUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle Lerp(this double t, Float64PlanarAngle angle1, Float64PlanarAngle angle2)
        {
            Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

            return ((1.0d - t) * angle1.Degrees + t * angle2.Degrees).DegreesToAngle();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle Lerp(this double t, Float64PlanarAngle angle2)
        {
            Debug.Assert(t.IsValid() && t is >= 0 and <= 1);

            return (t * angle2.Degrees).DegreesToAngle();
        }

    }
}
