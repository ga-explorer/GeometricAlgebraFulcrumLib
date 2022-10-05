using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D;

public static class Euclidean3DFactory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngle<T>(this IScalarAlgebraProcessor<T> scalarProcessor, PlanarAngle angle)
    {
        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.GetScalarFromNumber(angle.Radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360 => (angleInDegrees % 720) + 360,
            > 360 => angleInDegrees % 360,
            _ => angleInDegrees
        };

        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarAlgebraProcessor<T> scalarProcessor, long angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360L => (angleInDegrees % 720L) + 360L,
            > 360L => angleInDegrees % 360L,
            _ => angleInDegrees
        };

        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarAlgebraProcessor<T> scalarProcessor, float angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360f => (angleInDegrees % 720f) + 360f,
            > 360f => angleInDegrees % 360f,
            _ => angleInDegrees
        };

        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarAlgebraProcessor<T> scalarProcessor, double angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360d => (angleInDegrees % 720d) + 360d,
            > 360d => angleInDegrees % 360d,
            _ => angleInDegrees
        };

        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromDegrees<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T angleInDegrees)
    {
        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.DegreesToRadians(angleInDegrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromRadians<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T angleInRadians)
    {
        return new PlanarAngle<T>(
            scalarProcessor,
            angleInRadians
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ITuple2D v1, ITuple2D v2)
    {
        var angleInRadians = Math.Acos(v1.VectorDot(v2));

        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.GetScalarFromNumber(angleInRadians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ITuple3D v1, ITuple3D v2)
    {
        var angleInRadians = Math.Acos(v1.VectorDot(v2));

        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.GetScalarFromNumber(angleInRadians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PlanarAngle<T> CreatePlanarAngleFromUnitVectors<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ITuple4D v1, ITuple4D v2)
    {
        var angleInRadians = Math.Acos(v1.VectorDot(v2));

        return new PlanarAngle<T>(
            scalarProcessor,
            scalarProcessor.GetScalarFromNumber(angleInRadians)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T x, T y, T z, bool assumeUnitVector = false)
    {
        return new E3DVector<T>(scalarProcessor, x, y, z, assumeUnitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, zero, zero, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorXAxis<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;
        var one = scalarProcessor.ScalarOne;

        return new E3DVector<T>(scalarProcessor, one, zero, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorXAxis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T x)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, x, zero, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorYAxis<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;
        var one = scalarProcessor.ScalarOne;

        return new E3DVector<T>(scalarProcessor, zero, one, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorYAxis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T y)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, zero, y, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorZAxis<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;
        var one = scalarProcessor.ScalarOne;

        return new E3DVector<T>(scalarProcessor, zero, zero, one);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorZAxis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T z)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, zero, zero, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarXy<T>(this PlanarAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = angle.Cos();
        var y = angle.Sin();
        var z = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarXy<T>(this PlanarAngle<T> angle, T length)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = length * angle.Cos();
        var y = length * angle.Sin();
        var z = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarYx<T>(this PlanarAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = angle.Sin();
        var y = angle.Cos();
        var z = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarYx<T>(this PlanarAngle<T> angle, T length)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = length * angle.Sin();
        var y = length * angle.Cos();
        var z = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarYz<T>(this PlanarAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = scalarProcessor.ScalarZero;
        var y = angle.Cos();
        var z = angle.Sin();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarYz<T>(this PlanarAngle<T> angle, T length)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = scalarProcessor.ScalarZero;
        var y = length * angle.Cos();
        var z = length * angle.Sin();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarZy<T>(this PlanarAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = scalarProcessor.ScalarZero;
        var y = angle.Sin();
        var z = angle.Cos();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarZy<T>(this PlanarAngle<T> angle, T length)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = scalarProcessor.ScalarZero;
        var y = length * angle.Sin();
        var z = length * angle.Cos();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarZx<T>(this PlanarAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = angle.Sin();
        var y = scalarProcessor.ScalarZero;
        var z = angle.Cos();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarZx<T>(this PlanarAngle<T> angle, T length)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = length * angle.Sin();
        var y = scalarProcessor.ScalarZero;
        var z = length * angle.Cos();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarXz<T>(this PlanarAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = angle.Cos();
        var y = scalarProcessor.ScalarZero;
        var z = angle.Sin();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorPolarXz<T>(this PlanarAngle<T> angle, T length)
    {
        var scalarProcessor = angle.ScalarProcessor;

        var x = length * angle.Cos();
        var y = scalarProcessor.ScalarZero;
        var z = length * angle.Sin();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorSphericalXyz<T>(this PlanarAngle<T> angleXy, PlanarAngle<T> angleZ)
    {
        var scalarProcessor = angleXy.ScalarProcessor;

        var angleZSin = angleZ.Sin();
        var x = angleXy.Cos() * angleZSin;
        var y = angleXy.Sin() * angleZSin;
        var z = angleZ.Cos();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorSphericalXyz<T>(this PlanarAngle<T> angleXy, PlanarAngle<T> angleZ, T length)
    {
        var scalarProcessor = angleXy.ScalarProcessor;

        var angleZSin = angleZ.Sin();
        var x = length * angleXy.Cos() * angleZSin;
        var y = length * angleXy.Sin() * angleZSin;
        var z = length * angleZ.Cos();

        return new E3DVector<T>(scalarProcessor, x, y, z);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DPoint<T> CreateE3DPoint<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T x, T y, T z)
    {
        return new E3DPoint<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DPoint<T> CreateE3DPointZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DPoint<T>(scalarProcessor, zero, zero, zero);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DLineTangent<T> CreateE3DLineTangent<T>(this IScalarAlgebraProcessor<T> scalarProcessor, E3DPoint<T> origin, E3DVector<T> direction)
    {
        return new E3DLineTangent<T>(origin, direction);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DLineSegment<T> CreateE3DLineSegment<T>(this IScalarAlgebraProcessor<T> scalarProcessor, E3DPoint<T> point1, E3DPoint<T> point2)
    {
        return new E3DLineSegment<T>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DPlaneSegment<T> CreateE3DPlaneSegment<T>(this IScalarAlgebraProcessor<T> scalarProcessor, E3DPoint<T> point1, E3DPoint<T> point2, E3DPoint<T> point3)
    {
        return new E3DPlaneSegment<T>(point1, point2, point3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapIdentity<T> CreateE3DMapIdentity<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
    {
        return new E3DMapIdentity<T>(scalarProcessor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapTranslate<T> CreateE3DMapTranslate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, E3DVector<T> direction)
    {
        return new E3DMapTranslate<T>(direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapSequence<T> CreateE3DMapSequence<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
    {
        return new E3DMapSequence<T>(scalarProcessor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapSequence<T> CreateE3DMapSequence<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params E3DMap<T>[] mapList)
    {
        var map = new E3DMapSequence<T>(scalarProcessor);

        foreach (var subMap in mapList)
            map.Append(subMap);

        return map;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapSequence<T> CreateE3DMapSequence<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<E3DMap<T>> mapList)
    {
        var map = new E3DMapSequence<T>(scalarProcessor);

        foreach (var subMap in mapList)
            map.Append(subMap);

        return map;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapReflect<T> CreateE3DMapReflect<T>(this IScalarAlgebraProcessor<T> scalarProcessor, E3DPlane<T> plane)
    {
        return new E3DMapReflect<T>(plane.Point1, plane.Normal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapReflect<T> CreateE3DMapReflect<T>(this IScalarAlgebraProcessor<T> scalarProcessor, E3DPoint<T> origin, E3DVector<T> normal)
    {
        return new E3DMapReflect<T>(origin, normal);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapScale<T> CreateE3DMapScale<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T factor, E3DPoint<T> origin, E3DVector<T> direction)
    {
        return new E3DMapScale<T>(factor, origin, direction);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapScaleUniform<T> CreateE3DMapScaleUniform<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T factor, E3DPoint<T> origin)
    {
        return new E3DMapScaleUniform<T>(factor, origin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapRotate<T> CreateE3DMapRotate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, PlanarAngle<T> angle, E3DPoint<T> origin, E3DVector<T> direction)
    {
        return new E3DMapRotate<T>(angle, origin, direction);
    }

}