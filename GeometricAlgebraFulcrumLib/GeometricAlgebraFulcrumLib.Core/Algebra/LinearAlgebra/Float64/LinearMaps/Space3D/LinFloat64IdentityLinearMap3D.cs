using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;

public sealed class LinFloat64IdentityLinearMap3D :
    LinFloat64Rotation3D,
    ILinFloat64DirectionalScalingLinearMap3D
{
    public static LinFloat64IdentityLinearMap3D Instance { get; }
        = new LinFloat64IdentityLinearMap3D();


    public double ScalingFactor
        => 1d;

    public LinFloat64Vector3D ScalingVector
        => LinFloat64Vector3D.E1;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64IdentityLinearMap3D()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double epsilon = 1e-12d)
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D MapBasisVector(int axisIndex)
    {
        return LinFloat64Vector3D.BasisVectors[axisIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D x)
    {
        return x.ToLinVector3D();
    }

    public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Rotation3D GetInverseRotation()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling3D.Create(
            1d,
            LinFloat64Vector3D.E1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Quaternion GetQuaternion()
    {
        return LinFloat64Quaternion.Identity;
    }
}