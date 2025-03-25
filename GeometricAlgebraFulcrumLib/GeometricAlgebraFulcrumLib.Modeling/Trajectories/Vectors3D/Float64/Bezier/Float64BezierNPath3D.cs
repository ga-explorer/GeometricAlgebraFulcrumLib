using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Bezier;

public sealed class Float64BezierNPath3D :
    Float64Path3D
{
    

    public List<LinFloat64Vector3D> ControlPoints { get; }
        = new List<LinFloat64Vector3D>();

    public int Degree
        => ControlPoints.Count - 1;


    public Float64BezierNPath3D(bool isPeriodic) 
        : base(Float64ScalarRange.ZeroToOne, isPeriodic)
    {
    }

    public Float64BezierNPath3D GetDerivativeCurve()
    {
        var result = new Float64BezierNPath3D(IsPeriodic);

        for (var n = 0; n < Degree; n++)
            result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

        return result;
    }

    public override bool IsValid()
    {
        return ControlPoints.All(p => p.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return t.DeCasteljau(ControlPoints.ToArray());
    }

    public override Float64Path3D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path3D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        throw new NotImplementedException();
    }

    
}