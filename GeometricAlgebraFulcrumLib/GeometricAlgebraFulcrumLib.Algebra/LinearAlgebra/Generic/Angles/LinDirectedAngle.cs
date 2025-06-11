using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;

public sealed record LinDirectedAngle<T> :
    LinAngle<T>
{
    public static LinDirectedAngle<T> Angle0(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle0Radians());

    public static LinDirectedAngle<T> Angle30(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle30Radians());

    public static LinDirectedAngle<T> Angle45(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle45Radians());

    public static LinDirectedAngle<T> Angle60(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle60Radians());

    public static LinDirectedAngle<T> Angle90(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle90Radians());

    public static LinDirectedAngle<T> Angle120(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle120Radians());

    public static LinDirectedAngle<T> Angle135(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle135Radians());

    public static LinDirectedAngle<T> Angle150(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle150Radians());

    public static LinDirectedAngle<T> Angle180(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle180Radians());

    public static LinDirectedAngle<T> Angle225(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle225Radians());

    public static LinDirectedAngle<T> Angle210(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle210Radians());

    public static LinDirectedAngle<T> Angle240(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle240Radians());

    public static LinDirectedAngle<T> Angle270(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle270Radians());

    public static LinDirectedAngle<T> Angle300(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle300Radians());

    public static LinDirectedAngle<T> Angle315(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle315Radians());

    public static LinDirectedAngle<T> Angle330(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle330Radians());

    public static LinDirectedAngle<T> Angle360(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle360Radians());
    
    public static LinDirectedAngle<T> AngleMinus30(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle30Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus45(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle45Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus60(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle60Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus90(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle90Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus120(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle120Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus135(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle135Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus150(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle150Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus180(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle180Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus225(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle225Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus210(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle210Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus240(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle240Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus270(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle270Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus300(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle300Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus315(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle315Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus330(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle330Radians().Negative());

    public static LinDirectedAngle<T> AngleMinus360(IScalarProcessor<T> scalarProcessor)
        => new LinDirectedAngle<T>(scalarProcessor.Angle360Radians().Negative());

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromDegrees(IScalarProcessor<T> scalarProcessor, int angleInDegrees)
    {
        // A full-range angle is in the range [-360, 360] degrees
        var degrees =
            angleInDegrees switch
            {
                < -360 => angleInDegrees % 720 + 360,
                > 360 => angleInDegrees % 360,
                _ => angleInDegrees
            };

        var radians = 
            scalarProcessor.DegreesToRadians(degrees);

        return new LinDirectedAngle<T>(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromDegrees(IScalar<T> angleInDegrees)
    {
        var degrees = angleInDegrees.ToFloat64();

        if (degrees.IsNaNOrInfinite())
            return new LinDirectedAngle<T>(
                angleInDegrees.DegreesToRadians()
            );

        // A full-range angle is in the range [-360, 360] degrees
        degrees = degrees switch
        {
            < -360d => degrees % 720d + 360d,
            > 360d => degrees % 360d,
            _ => degrees
        };
        
        var radians = 
            angleInDegrees.ScalarProcessor.DegreesToRadians(degrees);

        return new LinDirectedAngle<T>(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromDegrees(IScalar<T> angleInDegrees, LinAngleRange range)
    {
        var radians = 
            range.GetDegrees(angleInDegrees).DegreesToRadians();

        return new LinDirectedAngle<T>(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreatePositiveFromDegrees(IScalar<T> angleInDegrees)
    {
        var radians = 
            LinAngleRange.Positive360.GetDegrees(angleInDegrees).DegreesToRadians();

        return new LinDirectedAngle<T>(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateNegativeFromDegrees(IScalar<T> angleInDegrees)
    {
        var radians = 
            LinAngleRange.Negative360.GetDegrees(angleInDegrees).DegreesToRadians();

        return new LinDirectedAngle<T>(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromRadians(IScalar<T> angleInRadians)
    {
        var angleInRadiansNumber = angleInRadians.ToFloat64();

        var radians = 
            angleInRadiansNumber.IsNaNOrInfinite()
                ? angleInRadians.ToScalar()
                : LinAngleRange.Symmetric360.GetRadians(angleInRadiansNumber).ScalarFromNumber(angleInRadians.ScalarProcessor);
        
        return new LinDirectedAngle<T>(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromRadians(IScalar<T> angleInRadians, LinAngleRange range)
    {
        var radians = 
            range.GetRadians(angleInRadians);

        return new LinDirectedAngle<T>(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreatePositiveFromRadians(IScalar<T> angleInRadians)
    {
        var radians = 
            LinAngleRange.Positive360.GetRadians(angleInRadians);

        return new LinDirectedAngle<T>(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateNegativeFromRadians(IScalar<T> angleInRadians)
    {
        var radians = 
            LinAngleRange.Negative360.GetRadians(angleInRadians);

        return new LinDirectedAngle<T>(radians);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromCosSin(IScalar<T> angleCos, IScalar<T> angleSin)
    {
        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromCosSin(IScalar<T> angleCos, IScalar<T> angleSin, LinAngleRange range)
    {
        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromCos(IScalar<T> angleCos)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromCos(IScalar<T> angleCos, LinAngleRange range)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromSin(IScalar<T> angleSin)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromSin(IScalar<T> angleSin, LinAngleRange range)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromTan(IScalar<T> angleTan)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromTan(IScalar<T> angleTan, LinAngleRange range)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }
    
    /// <summary>
    /// This assumes the angle is in the first quadrant
    /// </summary>
    /// <param name="angleTanSquared"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromTanSquared(LinAngle<T> angleTanSquared)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromTanSquared(LinAngle<T> angleTanSquared, LinAngleRange range)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateHalfAngleFromCosSin(IScalar<T> doubleAngleCos, IScalar<T> doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin.IsNegative()) angleCos = -angleCos;

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateHalfAngleFromCosSin(IScalar<T> doubleAngleCos, IScalar<T> doubleAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin.IsNegative()) angleCos = -angleCos;

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateHalfAngleFromCos(IScalar<T> doubleAngleCos)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateHalfAngleFromCos(IScalar<T> doubleAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateHalfAngleFromSin(IScalar<T> doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateHalfAngleFromSin(IScalar<T> doubleAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromCosSin(IScalar<T> halfAngleCos, IScalar<T> halfAngleSin)
    {
        var angleCos = 2 * halfAngleCos.Square() - 1;
        var angleSin = 2 * halfAngleCos.Times(halfAngleSin);

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromCosSin(IScalar<T> halfAngleCos, IScalar<T> halfAngleSin, LinAngleRange range)
    {
        var angleCos = 2 * halfAngleCos.Square() - 1;
        var angleSin = 2 * halfAngleCos.Times(halfAngleSin);

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromCos(IScalar<T> halfAngleCos)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromCos(IScalar<T> halfAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromSin(IScalar<T> halfAngleSin)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromSin(IScalar<T> halfAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromTan(IScalar<T> halfAngleTan)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateDoubleAngleFromTan(IScalar<T> halfAngleTan, LinAngleRange range)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateTripleAngleFromCosSin(IScalar<T> thirdAngleCos, IScalar<T> thirdAngleSin)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateTripleAngleFromCosSin(IScalar<T> thirdAngleCos, IScalar<T> thirdAngleSin, LinAngleRange range)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateTripleAngleFromCos(IScalar<T> thirdAngleCos)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateTripleAngleFromCos(IScalar<T> thirdAngleCos, LinAngleRange range)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateTripleAngleFromSin(IScalar<T> thirdAngleSin)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateTripleAngleFromSin(IScalar<T> thirdAngleSin, LinAngleRange range)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinDirectedAngle<T>(angleCos, angleSin, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromVector(IScalar<T> x, IScalar<T> y)
    {
        var radians = 
            x.ScalarProcessor.VectorToRadians(x.ScalarValue, y.ScalarValue);

        return CreatePositiveFromRadians(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromVector(IScalar<T> x, IScalar<T> y, LinAngleRange range)
    {
        var radians = range.GetRadians(
            x.ScalarProcessor.VectorToRadians(x.ScalarValue, y.ScalarValue)
        );

        return CreatePositiveFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromVector(IPair<Scalar<T>> vector)
    {
        return CreateFromVector(
            vector.Item1, 
            vector.Item2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromVector(IPair<Scalar<T>> vector, LinAngleRange range)
    {
        return CreateFromVector(
            vector.Item1, 
            vector.Item2,
            range
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromUnitVectors(IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return CreateFromCos(v1.VectorESp(v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromUnitVectors(ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return CreateFromCos(v1.VectorESp(v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CreateFromUnitVectors(IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return CreateFromCos(v1.ESp(v2));
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator T(LinDirectedAngle<T> angle)
    //{
    //    return angle.RadiansValue;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator LinDirectedAngle<T>(IScalar<T> angleInRadians)
    //{
    //    return CreateFromDegrees(
    //        angleInRadians
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(LinDirectedAngle<T> angle)
    {
        return CreateFromRadians(-angle.Radians);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator +(LinDirectedAngle<T> angle1, LinAngle<T> angle2)
    {
        return CreateFromRadians(angle1.Radians + angle2.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator +(LinAngle<T> angle1, LinDirectedAngle<T> angle2)
    {
        return CreateFromRadians(angle1.Radians + angle2.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator +(LinDirectedAngle<T> angle1, LinDirectedAngle<T> angle2)
    {
        return CreateFromRadians(angle1.Radians + angle2.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator +(LinDirectedAngle<T> angle1, T angleInRadians2)
    {
        Debug.Assert(angleInRadians2 != null, nameof(angleInRadians2) + " != null");
        
        return CreateFromRadians(
            angle1.Radians + angleInRadians2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator +(LinDirectedAngle<T> angle1, IScalar<T> angleInRadians2)
    {
        return CreateFromRadians(
            angle1.Radians + angleInRadians2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator +(T angleInRadians1, LinDirectedAngle<T> angle2)
    {
        Debug.Assert(angleInRadians1 != null, nameof(angleInRadians1) + " != null");
        
        return CreateFromRadians(
            angleInRadians1 + angle2.Radians
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator +(IScalar<T> angleInRadians1, LinDirectedAngle<T> angle2)
    {
        return CreateFromRadians(
            angleInRadians1 + angle2.Radians
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(LinDirectedAngle<T> angle1, LinAngle<T> angle2)
    {
        return CreateFromRadians(angle1.Radians - angle2.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(LinAngle<T> angle1, LinDirectedAngle<T> angle2)
    {
        return CreateFromRadians(angle1.Radians - angle2.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(LinDirectedAngle<T> angle1, LinDirectedAngle<T> angle2)
    {
        return CreateFromRadians(angle1.Radians - angle2.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(LinDirectedAngle<T> angle1, T angleInRadians2)
    {
        Debug.Assert(angleInRadians2 != null, nameof(angleInRadians2) + " != null");
        
        return CreateFromRadians(angle1.Radians - angleInRadians2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(LinDirectedAngle<T> angle1, IScalar<T> angleInRadians2)
    {
        return CreateFromRadians(
            angle1.Radians - angleInRadians2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(T angleInRadians1, LinDirectedAngle<T> angle2)
    {
        Debug.Assert(angleInRadians1 != null, nameof(angleInRadians1) + " != null");
        
        return CreateFromRadians(angleInRadians1 - angle2.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator -(IScalar<T> angleInRadians1, LinDirectedAngle<T> angle2)
    {
        return CreateFromRadians(
            angleInRadians1 - angle2.Radians
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator *(LinDirectedAngle<T> angle, int number)
    {
        return CreateFromRadians(angle.Radians * number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator *(LinDirectedAngle<T> angle, T number)
    {
        Debug.Assert(number != null, nameof(number) + " != null");
        
        return CreateFromRadians(angle.Radians * number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator *(LinDirectedAngle<T> angle, IScalar<T> number)
    {
        return CreateFromRadians(angle.Radians * number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator *(int number, LinDirectedAngle<T> angle)
    {
        return CreateFromRadians(number * angle.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator *(T number, LinDirectedAngle<T> angle)
    {
        Debug.Assert(number != null, nameof(number) + " != null");
        
        return CreateFromRadians(number * angle.Radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator *(IScalar<T> number, LinDirectedAngle<T> angle)
    {
        return CreateFromRadians(number * angle.Radians);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator /(LinDirectedAngle<T> angle, int number)
    {
        return CreateFromRadians(angle.Radians / number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator /(LinDirectedAngle<T> angle, T number)
    {
        Debug.Assert(number != null, nameof(number) + " != null");
        
        return CreateFromRadians(angle.Radians / number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> operator /(LinDirectedAngle<T> angle, IScalar<T> number)
    {
        return CreateFromRadians(angle.Radians / number);
    }


    public override IScalarProcessor<T> ScalarProcessor { get; }

    public override T CosValue 
        => ScalarProcessor.Cos(RadiansValue).ScalarValue;

    public override T SinValue 
        => ScalarProcessor.Sin(RadiansValue).ScalarValue;
    
    public override T TanValue 
        => ScalarProcessor.Tan(RadiansValue).ScalarValue;
    
    public override T RadiansValue { get; }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinDirectedAngle(IScalarProcessor<T> scalarProcessor, T radians)
    {
        ScalarProcessor = scalarProcessor;
        RadiansValue = radians;

        Debug.Assert(IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinDirectedAngle(IScalar<T> radians)
    {
        ScalarProcessor = radians.ScalarProcessor;
        RadiansValue = radians.ToScalar().ScalarValue;

        Debug.Assert(IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinDirectedAngle(IScalar<T> x, IScalar<T> y)
    {
        ScalarProcessor = x.ScalarProcessor;
        RadiansValue = x.ArcTan2(y).RadiansValue;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinDirectedAngle(IScalar<T> x, IScalar<T> y, LinAngleRange range)
    {
        ScalarProcessor = x.ScalarProcessor;
        RadiansValue = range.GetRadians(x.ArcTan2(y)).ScalarValue;

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        if (!ScalarProcessor.IsValid(RadiansValue)) 
            return false;

        if (!Radians.IsNumber()) 
            return true;

        return Radians >= -ScalarProcessor.PiTimes2 && 
               Radians <= ScalarProcessor.PiTimes2;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinDirectedAngle<T> NegativeAngle()
    {
        return CreateFromRadians(-Radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinDirectedAngle<T> OppositeAngle()
    {
        return CreateFromRadians(Radians + ScalarProcessor.Pi);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> AngleAdd(T angle2)
    {
        Debug.Assert(angle2 != null, nameof(angle2) + " != null");

        return CreateFromRadians(Radians + angle2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinDirectedAngle<T> AngleSubtract(T angle2)
    {
        Debug.Assert(angle2 != null, nameof(angle2) + " != null");
        
        return CreateFromRadians(Radians - angle2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinDirectedAngle<T> AngleAdd(IScalar<T> angle2)
    {
        return CreateFromRadians(Radians + angle2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinDirectedAngle<T> AngleSubtract(IScalar<T> angle2)
    {
        return CreateFromRadians(Radians - angle2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinDirectedAngle<T> AngleTimes(T scalingFactor)
    {
        Debug.Assert(scalingFactor != null, nameof(scalingFactor) + " != null");
        
        return CreateFromRadians(Radians * scalingFactor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinDirectedAngle<T> AngleDivide(T scalingFactor)
    {
        Debug.Assert(scalingFactor != null, nameof(scalingFactor) + " != null");
        
        return CreateFromRadians(Radians / scalingFactor);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinDirectedAngle<T> MapAngleRadians(ScalarTransformer<T> transformer)
    {
        return ScalarProcessor.CreateDirectedAngleFromRadians(
            transformer.MapScalarValue(RadiansValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"{DegreesValue} degrees";
    }
}
