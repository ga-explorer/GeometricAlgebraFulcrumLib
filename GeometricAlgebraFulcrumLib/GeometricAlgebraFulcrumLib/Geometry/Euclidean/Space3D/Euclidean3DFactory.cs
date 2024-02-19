using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Maps;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D.Objects;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D;

public static class Euclidean3DFactory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVector<T>(this IScalarProcessor<T> scalarProcessor, T x, T y, T z, bool assumeUnitVector = false)
    {
        return new E3DVector<T>(scalarProcessor, x, y, z, assumeUnitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorZero<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, zero, zero, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorXAxis<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;
        var one = scalarProcessor.ScalarOne;

        return new E3DVector<T>(scalarProcessor, one, zero, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorXAxis<T>(this IScalarProcessor<T> scalarProcessor, T x)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, x, zero, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorYAxis<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;
        var one = scalarProcessor.ScalarOne;

        return new E3DVector<T>(scalarProcessor, zero, one, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorYAxis<T>(this IScalarProcessor<T> scalarProcessor, T y)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DVector<T>(scalarProcessor, zero, y, zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorZAxis<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;
        var one = scalarProcessor.ScalarOne;

        return new E3DVector<T>(scalarProcessor, zero, zero, one);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DVector<T> CreateE3DVectorZAxis<T>(this IScalarProcessor<T> scalarProcessor, T z)
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
    public static E3DPoint<T> CreateE3DPoint<T>(this IScalarProcessor<T> scalarProcessor, T x, T y, T z)
    {
        return new E3DPoint<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DPoint<T> CreateE3DPointZero<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var zero = scalarProcessor.ScalarZero;

        return new E3DPoint<T>(scalarProcessor, zero, zero, zero);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DLineTangent<T> CreateE3DLineTangent<T>(this IScalarProcessor<T> scalarProcessor, E3DPoint<T> origin, E3DVector<T> direction)
    {
        return new E3DLineTangent<T>(origin, direction);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DLineSegment<T> CreateE3DLineSegment<T>(this IScalarProcessor<T> scalarProcessor, E3DPoint<T> point1, E3DPoint<T> point2)
    {
        return new E3DLineSegment<T>(point1, point2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DPlaneSegment<T> CreateE3DPlaneSegment<T>(this IScalarProcessor<T> scalarProcessor, E3DPoint<T> point1, E3DPoint<T> point2, E3DPoint<T> point3)
    {
        return new E3DPlaneSegment<T>(point1, point2, point3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapIdentity<T> CreateE3DMapIdentity<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new E3DMapIdentity<T>(scalarProcessor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapTranslate<T> CreateE3DMapTranslate<T>(this IScalarProcessor<T> scalarProcessor, E3DVector<T> direction)
    {
        return new E3DMapTranslate<T>(direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapSequence<T> CreateE3DMapSequence<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new E3DMapSequence<T>(scalarProcessor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapSequence<T> CreateE3DMapSequence<T>(this IScalarProcessor<T> scalarProcessor, params E3DMap<T>[] mapList)
    {
        var map = new E3DMapSequence<T>(scalarProcessor);

        foreach (var subMap in mapList)
            map.Append(subMap);

        return map;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapSequence<T> CreateE3DMapSequence<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<E3DMap<T>> mapList)
    {
        var map = new E3DMapSequence<T>(scalarProcessor);

        foreach (var subMap in mapList)
            map.Append(subMap);

        return map;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapReflect<T> CreateE3DMapReflect<T>(this IScalarProcessor<T> scalarProcessor, E3DPlane<T> plane)
    {
        return new E3DMapReflect<T>(plane.Point1, plane.Normal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapReflect<T> CreateE3DMapReflect<T>(this IScalarProcessor<T> scalarProcessor, E3DPoint<T> origin, E3DVector<T> normal)
    {
        return new E3DMapReflect<T>(origin, normal);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapScale<T> CreateE3DMapScale<T>(this IScalarProcessor<T> scalarProcessor, T factor, E3DPoint<T> origin, E3DVector<T> direction)
    {
        return new E3DMapScale<T>(factor, origin, direction);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapScaleUniform<T> CreateE3DMapScaleUniform<T>(this IScalarProcessor<T> scalarProcessor, T factor, E3DPoint<T> origin)
    {
        return new E3DMapScaleUniform<T>(factor, origin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static E3DMapRotate<T> CreateE3DMapRotate<T>(this IScalarProcessor<T> scalarProcessor, PlanarAngle<T> angle, E3DPoint<T> origin, E3DVector<T> direction)
    {
        return new E3DMapRotate<T>(angle, origin, direction);
    }

}