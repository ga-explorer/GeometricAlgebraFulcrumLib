using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Circles;

public sealed class Float64CirclePath3D :
    Float64ArcLengthPath3D,
    IFloat64CirclePath3D
{
    private readonly Float64AxisAlignedCirclePath3D _baseCircle;
    private readonly SquareMatrix3 _baseCircleRotation;

    public int RotationCount { get; }

    public double Radius { get; }

    public LinFloat64Vector3D Center { get; }

    public LinFloat64Vector3D UnitNormal { get; }


    public Float64CirclePath3D(ILinFloat64Vector3D center, ILinFloat64Vector3D unitNormal, double radius, int rotationCount = 1)
        : base(Float64ScalarRange.ZeroToOne, true)
    {
        if (radius < 0)
            throw new ArgumentException(nameof(radius));

        if (rotationCount == 0 || rotationCount > 100 || rotationCount < -100)
            throw new ArgumentException(nameof(rotationCount));

        RotationCount = Math.Abs(rotationCount);
        Center = center.ToLinVector3D();
        UnitNormal = unitNormal.ToLinVector3D();
        Radius = radius;

        var axis = unitNormal.SelectNearestBasisVector();

        if (axis.IsNegative())
            rotationCount = -rotationCount;

        if (axis.IsXAxis())
            _baseCircle = new Float64YzCirclePath3D(radius, rotationCount);

        else if (axis.IsYAxis())
            _baseCircle = new Float64ZxCirclePath3D(radius, rotationCount);

        else
            _baseCircle = new Float64XyCirclePath3D(radius, rotationCount);

        _baseCircleRotation = SquareMatrix3.CreateAxisToVectorRotationMatrix3D(axis, unitNormal);

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Radius.IsValid() &&
               Center.IsValid() &&
               UnitNormal.IsValid() &&
               UnitNormal.IsNearUnitVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return _baseCircleRotation * _baseCircle.GetValue(t) + Center;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return _baseCircleRotation * _baseCircle.GetDerivative1Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return _baseCircleRotation * _baseCircle.GetDerivative2Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        var frame = _baseCircle.GetFrame(t);

        return Float64Path3DLocalFrame.Create(
            t,
            _baseCircleRotation * frame.Point + Center,
            _baseCircleRotation * frame.Tangent,
            _baseCircleRotation * frame.Normal1,
            _baseCircleRotation * frame.Normal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return Math.Abs(Math.Tau * Radius * RotationCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double t)
    {
        return t.ClampPeriodic(1d) * GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        var maxLength = GetLength();

        return length.ClampPeriodic(maxLength) / maxLength;
    }
}