using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Computers;

public sealed class LineTraversalData3D
{
    private readonly Stack<Float64ScalarRange> _parameterRangeStack
        = new Stack<Float64ScalarRange>();


    public LinFloat64Vector3D Origin { get; }

    public LinFloat64Vector3D Direction { get; }

    public LinFloat64Vector3D DirectionInv { get; }

    public int[] DirectionSign { get; }

    public double ParameterMinValue { get; private set; }

    public double ParameterMaxValue { get; private set; }
        
    public Float64ScalarRange ParameterRange
        => Float64ScalarRange.Create(ParameterMinValue, ParameterMaxValue);

    public bool IsLine 
        => double.IsNegativeInfinity(ParameterMinValue) &&
           double.IsPositiveInfinity(ParameterMaxValue);

    public bool IsRay 
        => !double.IsInfinity(ParameterMinValue) &&
           double.IsPositiveInfinity(ParameterMaxValue);

    public bool IsLineSegment 
        => !double.IsInfinity(ParameterMinValue) &&
           !double.IsInfinity(ParameterMaxValue);

    internal LineTraversalData3D(ILine3D line)
    {
        Origin = line.GetOrigin();
        Direction = line.GetDirection();
        DirectionInv = line.GetDirectionInv();
        DirectionSign = line.GetDirectionSign();
        ParameterMinValue = double.NegativeInfinity;
        ParameterMaxValue = double.PositiveInfinity;
    }

    internal LineTraversalData3D(ILine3D line, double paramMinValue, double paramMaxValue)
    {
        Origin = line.GetOrigin();
        Direction = line.GetDirection();
        DirectionInv = line.GetDirectionInv();
        DirectionSign = line.GetDirectionSign();
        ParameterMinValue = paramMinValue;
        ParameterMaxValue = paramMaxValue;
    }


    internal LineTraversalData3D StoreParameterRange(double newMinValue, double newMaxValue)
    {
        _parameterRangeStack.Push(
            Float64ScalarRange.Create(ParameterMinValue, ParameterMaxValue)
        );

        ParameterMinValue = newMinValue;
        ParameterMaxValue = newMaxValue;

        return this;
    }

    internal LineTraversalData3D RestoreParameterRange()
    {
        var range = _parameterRangeStack.Pop();

        ParameterMinValue = range.MinValue;
        ParameterMaxValue = range.MaxValue;

        return this;
    }

    internal LineTraversalData3D UpdateParameterMaxValue(double value)
    {
        if (ParameterMaxValue > value)
            ParameterMaxValue = value;

        return this;
    }


    public LineSegment3D GetLineSegment()
    {
        return new LineSegment3D(
            Origin.X + ParameterMinValue * Direction.X,
            Origin.Y + ParameterMinValue * Direction.Y,
            Origin.Z + ParameterMinValue * Direction.Z,
            Origin.X + ParameterMaxValue * Direction.X,
            Origin.Y + ParameterMaxValue * Direction.Y,
            Origin.Z + ParameterMaxValue * Direction.Z
        );
    }

    public LinFloat64Vector3D GetMinPoint()
    {
        return LinFloat64Vector3D.Create(Origin.X + ParameterMinValue * Direction.X,
            Origin.Y + ParameterMinValue * Direction.Y,
            Origin.Z + ParameterMinValue * Direction.Z);
    }

    public LinFloat64Vector3D GetMaxPoint()
    {
        return LinFloat64Vector3D.Create(Origin.X + ParameterMaxValue * Direction.X,
            Origin.Y + ParameterMaxValue * Direction.Y,
            Origin.Z + ParameterMaxValue * Direction.Z);
    }
}