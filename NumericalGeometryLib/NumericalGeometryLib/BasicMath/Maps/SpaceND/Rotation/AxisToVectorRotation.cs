using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;

public sealed class AxisToVectorRotation :
    VectorToVectorRotationLinearMap
{
    public Axis SourceAxis { get; }

    public override Float64Tuple SourceVector { get; }

    public override Float64Tuple TargetOrthogonalVector { get; }

    public override Float64Tuple TargetVector { get; }

    public override double AngleCos { get; }

    public override PlanarAngle Angle
        => AngleCos.ArcCos();


    public AxisToVectorRotation(int uAxisIndex, bool uAxisNegative, Float64Tuple v)
    {
        Debug.Assert(
            v.IsNearUnit()
        );

        var dimensions = v.ScalarArray.Length;
        SourceAxis = new Axis(dimensions, uAxisIndex, uAxisNegative);
        SourceVector = SourceAxis.ToTuple();
        TargetVector = v;

        AngleCos = TargetVector.VectorDot(SourceAxis).Clamp(-1d, 1d);

        Debug.Assert(
            !AngleCos.IsNearMinusOne()
        );

        TargetOrthogonalVector = Float64Tuple.CreateCopy(TargetVector);
        TargetOrthogonalVector.ScalarArray[SourceAxis.Index] -= SourceAxis.IsNegative ? -AngleCos : AngleCos;
        TargetOrthogonalVector.InPlaceScale(1d / (1d + AngleCos));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return 
            SourceAxis.Dimensions == TargetVector.Dimensions &&
            TargetVector.IsNearUnit() &&
            !AngleCos.IsNearMinusOne();
    }
    
    public override Float64Tuple ProjectOnRotationPlane(Float64Tuple vector)
    {
        var v = TargetVector.ScalarArray;
        var x = vector.ScalarArray;

        var uvDot = AngleCos;
        var xuDot = vector.VectorDot(SourceAxis);
        var xvDot = x.VectorDot(v);
        var bivectorNormSquaredInv = 1d / (1d - uvDot * uvDot);

        var uScalar = (xuDot - xvDot * uvDot) * bivectorNormSquaredInv;
        var vScalar = (xvDot - xuDot * uvDot) * bivectorNormSquaredInv;

        var y = new double[Dimensions];

        for (var i = 0; i < Dimensions; i++)
            y[i] = vScalar * v[i];

        y[SourceAxis.Index] += SourceAxis.IsNegative ? -uScalar : uScalar;

        return Float64Tuple.Create(y);
    }

    public override Float64Tuple MapVectorBasis(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0 && basisIndex < Dimensions
        );

        var y = new double[Dimensions];
        y[basisIndex] = 1d;

        var t = TargetOrthogonalVector.ScalarArray;
        var v = TargetVector.ScalarArray;

        var r = y.VectorDot(t);
        var s = basisIndex == SourceAxis.Index ? SourceAxis.IsNegative ? -1d : 1d : 0d;
        var rsPlus = r + s;
        var rsMinus = r - s;

        for (var i = 0; i < Dimensions; i++)
            y[i] -= rsMinus * v[i];

        y[SourceAxis.Index] -= SourceAxis.IsNegative ? -rsPlus : rsPlus;

        return Float64Tuple.Create(y);
    }

    public override Float64Tuple MapVector(Float64Tuple vector)
    {
        Debug.Assert(
            vector.Dimensions == Dimensions
        );
        
        var y = vector.GetScalarArrayCopy();

        var t = TargetOrthogonalVector.ScalarArray;
        var v = TargetVector.ScalarArray;

        var r = y.VectorDot(t);
        var s = SourceAxis.IsNegative ? -y[SourceAxis.Index] : y[SourceAxis.Index];
        var rsPlus = r + s;
        var rsMinus = r - s;

        for (var i = 0; i < Dimensions; i++)
            y[i] -= rsMinus * v[i];

        y[SourceAxis.Index] -= SourceAxis.IsNegative ? -rsPlus : rsPlus;

        return Float64Tuple.Create(y);
    }
    
    public override Float64Tuple MapVectorProjection(Float64Tuple vector)
    {
        var x = vector.ScalarArray;
        var t = TargetOrthogonalVector.ScalarArray;
        var v = TargetVector.ScalarArray;

        var r = x.VectorDot(t);
        var s = SourceAxis.IsNegative ? -x[SourceAxis.Index] : x[SourceAxis.Index];

        var y = new double[Dimensions];

        var uScalar = r / (AngleCos - 1);
        var vScalar = s - uScalar * AngleCos;

        for (var i = 0; i < Dimensions; i++)
            y[i] = vScalar * v[i];

        y[SourceAxis.Index] += SourceAxis.IsNegative ? -uScalar : uScalar;

        return Float64Tuple.Create(y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override SimpleRotationLinearMap GetSimpleVectorRotationInverse()
    {
        return VectorToAxisRotation.Create(
            TargetVector,
            SourceAxis.Index,
            SourceAxis.IsNegative
        );
    }
}