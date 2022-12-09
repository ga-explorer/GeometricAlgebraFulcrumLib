using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;

public sealed class OrthogonalVectorToVectorRotation :
    VectorToVectorRotationLinearMap
{
    public override Float64Tuple SourceVector { get; }

    public override Float64Tuple TargetOrthogonalVector
        => TargetVector;

    public override Float64Tuple TargetVector { get; }

    public override double AngleCos
        => 0d;

    public override PlanarAngle Angle
        => PlanarAngle.Angle90;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public OrthogonalVectorToVectorRotation(Float64Tuple sourceVector, Float64Tuple targetVector)
    {
        Debug.Assert(
            sourceVector.Dimensions == targetVector.Dimensions &&
            sourceVector.IsNearUnit() &&
            targetVector.IsNearUnit() &&
            targetVector.IsNearOrthogonalTo(sourceVector)
        );

        SourceVector = sourceVector;
        TargetVector = targetVector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            SourceVector.Dimensions == TargetVector.Dimensions &&
            SourceVector.IsNearUnit() &&
            TargetVector.IsNearUnit() &&
            TargetVector.IsNearOrthogonalTo(SourceVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double epsilon = 1e-12d)
    {
        return false;
    }
    
    public override Float64Tuple ProjectOnRotationPlane(Float64Tuple vector)
    {
        var u = SourceVector.ScalarArray;
        var v = TargetVector.ScalarArray;
        var x = vector.ScalarArray;

        var (xuDot, xvDot) = x.VectorDot(u, v);

        var y = new double[Dimensions];

        for (var i = 0; i < Dimensions; i++)
            y[i] = xuDot * u[i] + xvDot * v[i];

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVectorBasis(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0 && basisIndex < Dimensions
        );

        var y = new double[Dimensions];
        y[basisIndex] = 1d;

        var u = SourceVector.ScalarArray;
        var v = TargetVector.ScalarArray;

        var r = v[basisIndex];
        var s = u[basisIndex];
        var rsPlus = r + s;
        var rsMinus = r - s;

        for (var i = 0; i < Dimensions; i++)
            y[i] -= rsPlus * u[i] + rsMinus * v[i];
        
        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );
        
        var y = vector.GetScalarArrayCopy();

        var u = SourceVector.ScalarArray;
        var v = TargetVector.ScalarArray;

        var (r, s) = y.VectorDot(v, u);
        var rsPlus = r + s;
        var rsMinus = r - s;

        for (var i = 0; i < Dimensions; i++)
            y[i] -= rsPlus * u[i] + rsMinus * v[i];

        return Float64Tuple.Create(y);
    }
    
    public override Float64Tuple MapVectorProjection(Float64Tuple vector)
    {
        var x = vector.ScalarArray;
        var u = SourceVector.ScalarArray;
        var t = TargetOrthogonalVector.ScalarArray;
        var v = TargetVector.ScalarArray;

        var (r, s) = x.VectorDot(t, u);

        var y = new double[Dimensions];

        for (var i = 0; i < Dimensions; i++)
            y[i] = s * v[i] - r * u[i];

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override SimpleRotationLinearMap GetSimpleVectorRotationInverse()
    {
        return new OrthogonalVectorToVectorRotation(
            TargetVector,
            SourceVector
        );
    }
}