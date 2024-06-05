using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

public sealed class LinFloat64IdentityLinearMap :
    LinFloat64SimpleRotation,
    ILinFloat64DirectionalScalingLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64IdentityLinearMap Create(int dimensions)
    {
        return new LinFloat64IdentityLinearMap(dimensions);
    }


    public override int VSpaceDimensions { get; }

    public double ScalingFactor
        => 1d;

    public LinFloat64Vector ScalingVector { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64IdentityLinearMap(int dimensions)
    {
        if (dimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(dimensions));

        VSpaceDimensions = dimensions;
        ScalingVector = 0.CreateLinVector();
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
    public override LinFloat64Vector MapBasisVector(int axisIndex)
    {
        return axisIndex.CreateLinVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector x)
    {
        return x;
    }

    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64SimpleRotation GetInverseSimpleRotation()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            1d,
            0.CreateLinVector()
        );
    }
}