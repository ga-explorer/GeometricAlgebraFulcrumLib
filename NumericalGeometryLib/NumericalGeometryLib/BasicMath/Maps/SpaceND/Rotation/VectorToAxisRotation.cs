using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;

public sealed class VectorToAxisRotation :
    VectorToVectorRotationLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorToAxisRotation Create(Float64Tuple u, Axis vAxis)
    {
        return new VectorToAxisRotation(
            u,
            vAxis.Index,
            vAxis.IsNegative
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorToAxisRotation Create(Float64Tuple u, int vAxisIndex, bool vAxisNegative)
    {
        return new VectorToAxisRotation(
            u,
            vAxisIndex,
            vAxisNegative
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorToAxisRotation CreateToPositiveAxis(Float64Tuple u, int vAxisIndex)
    {
        return new VectorToAxisRotation(
            u,
            vAxisIndex,
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorToAxisRotation CreateToNegativeAxis(Float64Tuple u, int vAxisIndex)
    {
        return new VectorToAxisRotation(
            u,
            vAxisIndex,
            true
        );
    }


    public Axis TargetAxis { get; }

    public override Float64Tuple SourceVector { get; }

    public override Float64Tuple TargetOrthogonalVector { get; }

    public override Float64Tuple TargetVector { get; }

    public override double AngleCos { get; }

    public override PlanarAngle Angle
        => AngleCos.ArcCos();


    private VectorToAxisRotation(Float64Tuple sourceVector, int targetAxisIndex, bool targetAxisNegative)
    {
        Debug.Assert(
            sourceVector.IsNearUnit()
        );

        var dimensions = sourceVector.ScalarArray.Length;
        SourceVector = sourceVector;
        TargetAxis = new Axis(dimensions, targetAxisIndex, targetAxisNegative);
        TargetVector = TargetAxis.ToTuple();

        AngleCos = SourceVector.VectorDot(TargetAxis).Clamp(-1d, 1d);

        Debug.Assert(
            !AngleCos.IsNearMinusOne()
        );

        //TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);

        TargetOrthogonalVector = -AngleCos * SourceVector;
        TargetOrthogonalVector.ScalarArray[TargetAxis.Index] += TargetAxis.IsNegative ? -1d : 1d;
        TargetOrthogonalVector.InPlaceScale(1d / (1d + AngleCos));
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            SourceVector.Dimensions == TargetAxis.Dimensions &&
            SourceVector.IsNearUnit() &&
            !AngleCos.IsNearMinusOne();
    }
    
    public override Float64Tuple ProjectOnRotationPlane(Float64Tuple vector)
    {
        var u = SourceVector.ScalarArray;
        var x = vector.ScalarArray;

        var uvDot = AngleCos;
        var xuDot = x.VectorDot(u);
        var xvDot = vector.VectorDot(TargetAxis);
        var bivectorNormSquaredInv = 1d / (1d - uvDot * uvDot);

        var uScalar = (xuDot - xvDot * uvDot) * bivectorNormSquaredInv;
        var vScalar = (xvDot - xuDot * uvDot) * bivectorNormSquaredInv;

        var y = new double[Dimensions];

        for (var i = 0; i < Dimensions; i++)
            y[i] = uScalar * u[i];

        y[TargetAxis.Index] += TargetAxis.IsNegative ? -vScalar : vScalar;

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
        var t = TargetOrthogonalVector.ScalarArray;

        var r = t[basisIndex];
        var s = u[basisIndex];
        var rsPlus = r + s;
        var rsMinus = r - s;
        
        for (var i = 0; i < Dimensions; i++)
            y[i] -= rsPlus * u[i];

        y[TargetAxis.Index] -= 
            TargetAxis.IsNegative ? -rsMinus : rsMinus;

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );
            
        //var r = vector.VectorDot(TargetOrthogonalVector);
        //var s = vector.VectorDot(SourceVector);

        //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

        var y = vector.GetScalarArrayCopy();

        var u = SourceVector.ScalarArray;
        var t = TargetOrthogonalVector.ScalarArray;

        var (r, s) = y.VectorDot(t, u);
        var rsPlus = r + s;
        var rsMinus = r - s;
        
        for (var i = 0; i < Dimensions; i++)
            y[i] -= rsPlus * u[i];

        y[TargetAxis.Index] -= 
            TargetAxis.IsNegative ? -rsMinus : rsMinus;

        return Float64Tuple.Create(y);
    }
    
    public override Float64Tuple MapVectorProjection(Float64Tuple vector)
    {
        var x = vector.ScalarArray;
        var u = SourceVector.ScalarArray;
        var t = TargetOrthogonalVector.ScalarArray;

        var (r, s) = x.VectorDot(t, u);

        var y = new double[Dimensions];

        var uScalar = r / (AngleCos - 1d);
        var vScalar = s - uScalar * AngleCos;

        for (var i = 0; i < Dimensions; i++)
            y[i] = uScalar * u[i];

        y[TargetAxis.Index] += TargetAxis.IsNegative ? -vScalar : vScalar;

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override SimpleRotationLinearMap GetSimpleVectorRotationInverse()
    {
        return new AxisToVectorRotation(
            TargetAxis.Index,
            TargetAxis.IsNegative,
            SourceVector
        );
    }
}