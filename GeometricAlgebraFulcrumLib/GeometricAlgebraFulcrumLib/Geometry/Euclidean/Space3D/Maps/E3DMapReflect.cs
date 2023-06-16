using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps
{
    public sealed class E3DMapReflect<T> :
        E3DMap<T>
    {
        public override IScalarProcessor<T> ScalarProcessor 
            => Origin.ScalarProcessor;

        public E3DPoint<T> Origin { get; }

        public E3DVector<T> UnitNormal { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E3DMapReflect(E3DPoint<T> point, E3DVector<T> normal)
        {
            Origin = point;
            UnitNormal = normal.GetUnitVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DVector<T> Map(E3DVector<T> vector)
        {
            return vector - 2 * UnitNormal.Dot(vector) * UnitNormal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DPoint<T> Map(E3DPoint<T> point)
        {
            return point + Map(point - Origin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override E3DMap<T> GetInverse()
        {
            return this;
        }
    }
}