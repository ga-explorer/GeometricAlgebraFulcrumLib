using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;

public sealed record E3DLinePlaneIntersectionRecord<T>
{
    public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

    /// <summary>
    /// Signed distance from the line's point 1 to the plane
    /// </summary>
    public T LinePoint1Distance { get; init; }

    /// <summary>
    /// Signed distance from the line's point 2 to the plane
    /// </summary>
    public T LinePoint2Distance { get; init; }

    /// <summary>
    /// Signed distance from the plane's 1-2 side to the line
    /// </summary>
    public T PlaneLine12Distance { get; init; }

    /// <summary>
    /// Signed distance from the plane's 2-3 side to the line
    /// </summary>
    public T PlaneLine23Distance { get; init; }

    /// <summary>
    /// Signed distance from the plane's 3-1 side to the line
    /// </summary>
    public T PlaneLine31Distance { get; init; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal E3DLinePlaneIntersectionRecord([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }

    
    /// <summary>
    /// Parameter value of line at intersection point
    /// </summary>
    public T GetLineParameterValue12()
    {
        return ScalarProcessor.Divide(
            LinePoint1Distance,
            ScalarProcessor.Subtract(LinePoint1Distance, LinePoint2Distance)
        );
    }
    
    /// <summary>
    /// Parameter value of line at intersection point
    /// </summary>
    public T GetLineParameterValue21()
    {
        return ScalarProcessor.Divide(
            LinePoint2Distance,
            ScalarProcessor.Subtract(LinePoint2Distance, LinePoint1Distance)
        );
    }

    /// <summary>
    /// 1st Parameter value of plane at intersection point
    /// </summary>
    public T GetPlaneParameterValue12()
    {
        return ScalarProcessor.Divide(
            PlaneLine12Distance,
            ScalarProcessor.Add(
                PlaneLine12Distance, 
                PlaneLine23Distance, 
                PlaneLine31Distance
            )
        );
    }
    
    /// <summary>
    /// 2nd Parameter value of plane at intersection point
    /// </summary>
    public T GetPlaneParameterValue21()
    {
        return ScalarProcessor.Divide(
            ScalarProcessor.Negative(PlaneLine12Distance),
            ScalarProcessor.Add(
                PlaneLine12Distance, 
                PlaneLine23Distance, 
                PlaneLine31Distance
            )
        );
    }

    /// <summary>
    /// 2nd Parameter value of plane at intersection point
    /// </summary>
    public T GetPlaneParameterValue23()
    {
        return ScalarProcessor.Divide(
            PlaneLine23Distance,
            ScalarProcessor.Add(
                PlaneLine12Distance, 
                PlaneLine23Distance, 
                PlaneLine31Distance
            )
        );
    }
    
    /// <summary>
    /// 2nd Parameter value of plane at intersection point
    /// </summary>
    public T GetPlaneParameterValue32()
    {
        return ScalarProcessor.Divide(
            ScalarProcessor.Negative(PlaneLine23Distance),
            ScalarProcessor.Add(
                PlaneLine12Distance, 
                PlaneLine23Distance, 
                PlaneLine31Distance
            )
        );
    }

    /// <summary>
    /// 2nd Parameter value of plane at intersection point
    /// </summary>
    public T GetPlaneParameterValue31()
    {
        return ScalarProcessor.Divide(
            PlaneLine31Distance,
            ScalarProcessor.Add(
                PlaneLine12Distance, 
                PlaneLine23Distance, 
                PlaneLine31Distance
            )
        );
    }
    
    /// <summary>
    /// 2nd Parameter value of plane at intersection point
    /// </summary>
    public T GetPlaneParameterValue13()
    {
        return ScalarProcessor.Divide(
            ScalarProcessor.Negative(PlaneLine31Distance),
            ScalarProcessor.Add(
                PlaneLine12Distance, 
                PlaneLine23Distance, 
                PlaneLine31Distance
            )
        );
    }

    /// <summary>
    /// Intersection of a line and a line exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasLinePlaneIntersection()
    {
        var lineParameterValue = GetLineParameterValue12();

        return ScalarProcessor.IsFiniteNumber(lineParameterValue);
    }

    /// <summary>
    /// Intersection of a line and a ray exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasLineBeamIntersection()
    {
        var lineParameterValue = GetLineParameterValue12();

        return ScalarProcessor.IsFiniteNumber(lineParameterValue) &&
               ScalarProcessor.AllSameSign(
                   PlaneLine12Distance, 
                   PlaneLine31Distance
               );
    }

    /// <summary>
    /// Intersection of a line and a segment exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasLineSegmentIntersection()
    {
        var lineParameterValue = GetLineParameterValue12();

        return ScalarProcessor.IsFiniteNumber(lineParameterValue) &&
               ScalarProcessor.AllSameSign(
                   PlaneLine12Distance, 
                   PlaneLine23Distance, 
                   PlaneLine31Distance
               );
    }

    /// <summary>
    /// Intersection of a ray and a line exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasRayPlaneIntersection()
    {
        var lineParameterValue = GetLineParameterValue12();

        return ScalarProcessor.IsFiniteNumber(lineParameterValue) &&
               ScalarProcessor.IsPositive(lineParameterValue);
    }

    /// <summary>
    /// Intersection of a ray and a ray exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasRayBeamIntersection()
    {
        var lineParameterValue = GetLineParameterValue12();

        return ScalarProcessor.IsFiniteNumber(lineParameterValue) &&
               ScalarProcessor.IsPositive(lineParameterValue) &&
               ScalarProcessor.AllSameSign(
                   PlaneLine12Distance, 
                   PlaneLine31Distance
               );
    }

    /// <summary>
    /// Intersection of a ray and a segment exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasRaySegmentIntersection()
    {
        var lineParameterValue = GetLineParameterValue12();

        return ScalarProcessor.IsFiniteNumber(lineParameterValue) &&
               ScalarProcessor.IsPositive(lineParameterValue) &&
               ScalarProcessor.AllSameSign(
                   PlaneLine12Distance, 
                   PlaneLine23Distance, 
                   PlaneLine31Distance
               );
    }

    /// <summary>
    /// Intersection of a segment and a line exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSegmentPlaneIntersection()
    {
        return ScalarProcessor.HaveOppositeSign(
                   LinePoint1Distance, 
                   LinePoint2Distance
               );
    }

    /// <summary>
    /// Intersection of a line segment and a plane beam exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSegmentBeamIntersection()
    {
        return ScalarProcessor.HaveOppositeSign(
                   LinePoint1Distance, 
                   LinePoint2Distance
               ) &&
               ScalarProcessor.AllSameSign(
                   PlaneLine12Distance, 
                   PlaneLine31Distance
               );
    }

    /// <summary>
    /// Intersection of a line segment and a plane segment exists
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSegmentSegmentIntersection()
    {
        return ScalarProcessor.HaveOppositeSign(
                   LinePoint1Distance, 
                   LinePoint2Distance
               ) &&
               ScalarProcessor.AllSameSign(
                   PlaneLine12Distance, 
                   PlaneLine23Distance, 
                   PlaneLine31Distance
               );
    }
}