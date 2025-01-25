using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Vector3DRandomUtils
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetLinVector3D(this Random random)
    {
        return new LinFloat64SphericalVector3D(
            random.GetPolarAngleFromArcRatio(0.5),
            random.GetPolarAngle(),
            random.NextDouble()
        ).ToLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetUnitLinVector3D(this Random random)
    {
        return new LinFloat64SphericalUnitVector3D(
            random.GetPolarAngleFromArcRatio(0.5),
            random.GetPolarAngle()
        ).ToLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinFloat64Vector3D> GetOrthonormalLinVector3DPair(this Random random)
    {
        var v1 = random.GetUnitLinVector3D();
        var v2 = random.GetLinVector3D().RejectOnUnitVector(v1).ToUnitLinVector3D();

        return new Pair<LinFloat64Vector3D>(v1, v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D GetLinBivector3D(this Random random)
    {
        return LinFloat64Bivector3D.Create(
            random.NextDouble(),
            random.NextDouble(),
            random.NextDouble()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D GetLinTrivector3D(this Random random)
    {
        return LinFloat64Trivector3D.Create(
            random.NextDouble()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Quaternion GetLinQuaternion(this Random random)
    {
        return LinFloat64Quaternion.Create(random.NextDouble(), random.NextDouble(), random.NextDouble(), random.NextDouble());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetLinearCombination(this Random random, ITriplet<Float64Scalar> vector1, ITriplet<Float64Scalar> vector2)
    {
        return vector1.VectorTimes(random.NextDouble()) +
               vector2.VectorTimes(random.NextDouble());
    }

}