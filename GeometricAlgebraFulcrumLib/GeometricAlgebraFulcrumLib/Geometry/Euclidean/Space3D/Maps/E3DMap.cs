using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;

public abstract class E3DMap<T>
{
    public abstract IScalarProcessor<T> ScalarProcessor { get; }


    public abstract E3DVector<T> Map(E3DVector<T> vector);

    public abstract E3DPoint<T> Map(E3DPoint<T> point);

    public abstract E3DMap<T> GetInverse();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLine<T> Map(E3DLine<T> line)
    {
        return line switch
        {
            E3DLineSegment<T> lineSegment => Map(lineSegment),
            E3DLineTangent<T> lineTangent => Map(lineTangent),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLineSegment<T> Map(E3DLineSegment<T> lineSegment)
    {
        return new E3DLineSegment<T>(
            Map(lineSegment.Point1),
            Map(lineSegment.Point2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DLineTangent<T> Map(E3DLineTangent<T> lineTangent)
    {
        return new E3DLineTangent<T>(
            Map(lineTangent.Origin),
            Map(lineTangent.Direction)
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DPlane<T> Map(E3DPlane<T> plane)
    {
        return plane switch
        {
            E3DPlaneSegment<T> planeSegment => Map(planeSegment),
            E3DPlaneTangent<T> planeTangent => Map(planeTangent),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DPlaneSegment<T> Map(E3DPlaneSegment<T> planeSegment)
    {
        return new E3DPlaneSegment<T>(
            Map(planeSegment.Point1),
            Map(planeSegment.Point2),
            Map(planeSegment.Point3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public E3DPlaneTangent<T> Map(E3DPlaneTangent<T> planeTangent)
    {
        return new E3DPlaneTangent<T>(
            Map(planeTangent.Origin),
            Map(planeTangent.Direction12),
            Map(planeTangent.Direction13)
        );
    }

    public virtual E3DMapAffine<T> ToAffine()
    {
        return new E3DMapAffine<T>(
            Map(ScalarProcessor.CreateE3DPointZero()),
            Map(ScalarProcessor.CreateE3DVectorXAxis()),
            Map(ScalarProcessor.CreateE3DVectorYAxis()),
            Map(ScalarProcessor.CreateE3DVectorZAxis())
        );
    }
}