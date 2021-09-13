using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Frames;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class RotorUtils
    {
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> MapFreeFrame<T>(this IRotor<T> rotor, GeoFreeFrame<T> frame)
        {
            return new GeoFreeFrame<T>(
                frame.GeometricProcessor,
                frame.FrameKind,
                frame.Select(rotor.OmMapVector)
            );
        }
    }
}