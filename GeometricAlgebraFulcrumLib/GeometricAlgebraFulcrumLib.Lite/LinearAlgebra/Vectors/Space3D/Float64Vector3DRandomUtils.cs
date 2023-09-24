using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

public static class Float64Vector3DRandomUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetVector3D(this System.Random random)
    {
        return new Float64SphericalVector3D(
            random.GetAngle(Float64PlanarAngle.Angle180),
            random.GetAngle(),
            random.NextDouble()
        ).ToVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetUnitVector3D(this System.Random random)
    {
        return new Float64SphericalUnitVector3D(
            random.GetAngle(Float64PlanarAngle.Angle180),
            random.GetAngle()
        ).ToVector3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Float64Vector3D> GetOrthonormalVectorPair3D(this System.Random random)
    {
        var v1 = random.GetUnitVector3D();
        var v2 = random.GetVector3D().RejectOnUnitVector(v1).ToUnitVector();

        return new Pair<Float64Vector3D>(v1, v2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector3D GetBivector3D(this System.Random random)
    {
        return Float64Bivector3D.Create(
            random.NextDouble(),
            random.NextDouble(),
            random.NextDouble()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Trivector3D GetTrivector3D(this System.Random random)
    {
        return Float64Trivector3D.Create(
            random.NextDouble()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Quaternion GetQuaternion(this System.Random random)
    {
        return Float64Quaternion.Create(
            random.NextDouble(),
            random.NextDouble(),
            random.NextDouble(),
            random.NextDouble()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetLinearCombination(this System.Random random, Float64Vector3D vector1, Float64Vector3D vector2)
    {
        return vector1 * random.NextDouble() +
               vector2 * random.NextDouble();
    }

}