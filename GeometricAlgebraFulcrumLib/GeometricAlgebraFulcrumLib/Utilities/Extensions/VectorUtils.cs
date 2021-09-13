using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class VectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> RotateUsing<T>(this Vector<T> vector, IRotor<T> rotor)
        {
            var processor = vector.GeometricProcessor;

            return new Vector<T>(
                processor,
                rotor.OmMapVector(vector.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> ProjectUsing<T>(this Vector<T> vector, GeoSubspace<T> subspace)
        {
            var processor = vector.GeometricProcessor;

            return new Vector<T>(
                processor,
                subspace.Project(vector.VectorStorage).GetVectorPart()
            );
        }
    }
}