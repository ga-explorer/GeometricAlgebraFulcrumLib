using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;


/// <summary>
/// In this record only the angle cos and sin are stored, the radians
/// value is computed on demand
/// </summary>
public sealed record LinPolarAngle<T> :
    LinAngle<T>
{
    public static LinPolarAngle<T> Angle0(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 0);

    public static LinPolarAngle<T> Angle30(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 30);

    public static LinPolarAngle<T> Angle45(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 45);

    public static LinPolarAngle<T> Angle60(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 60);

    public static LinPolarAngle<T> Angle90(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 90);

    public static LinPolarAngle<T> Angle120(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 120);

    public static LinPolarAngle<T> Angle135(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 135);

    public static LinPolarAngle<T> Angle150(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 150);

    public static LinPolarAngle<T> Angle180(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 180);

    public static LinPolarAngle<T> Angle210(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 210);

    public static LinPolarAngle<T> Angle225(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 225);

    public static LinPolarAngle<T> Angle240(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 240);

    public static LinPolarAngle<T> Angle270(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 270);

    public static LinPolarAngle<T> Angle300(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 300);

    public static LinPolarAngle<T> Angle315(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 315);

    public static LinPolarAngle<T> Angle330(IScalarProcessor<T> scalarProcessor) 
        => CreateFromDegrees(scalarProcessor, 330);

    public static LinPolarAngle<T> Angle360(IScalarProcessor<T> scalarProcessor)
        => CreateFromDegrees(scalarProcessor, 360);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromDegrees(IScalarProcessor<T> scalarProcessor, int angleInDegrees)
    {
        var angleInRadians = scalarProcessor.DegreesToRadians(angleInDegrees);

        return CreateFromRadians(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromDegrees(IScalar<T> angleInDegrees)
    {
        var angleInRadians = angleInDegrees.DegreesToRadians();

        return CreateFromRadians(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromRadians(IScalar<T> angleInRadians)
    {
        var cosValue = angleInRadians.Cos();
        var sinValue = angleInRadians.Sin();

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromCosSin(IScalar<T> angleCos, IScalar<T> angleSin)
    {
        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromCos(IScalar<T> angleCos)
    {
        var angleSin = angleCos.CosToRadiansSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromSin(IScalar<T> angleSin)
    {
        var angleCos = angleSin.SinToRadiansCos();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    /// <summary>
    /// This assumes the angle is in the first two quadrant
    /// </summary>
    /// <param name="angleTan"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromTan(IScalar<T> angleTan)
    {
        var (angleCos, angleSin) = angleTan.TanToRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    /// <summary>
    /// This assumes the angle is in the first quadrant
    /// </summary>
    /// <param name="angleTanSquared"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromTanSquared(LinAngle<T> angleTanSquared)
    {
        var (angleCos, angleSin) = angleTanSquared.TanSquaredToRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateHalfAngleFromCosSin(IScalar<T> doubleAngleCos, IScalar<T> doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        if (doubleAngleSin.IsNegative()) angleCos = -angleCos;

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateHalfAngleFromCos(IScalar<T> doubleAngleCos)
    {
        var (angleCos, angleSin) = doubleAngleCos.CosToHalfRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateHalfAngleFromSin(IScalar<T> doubleAngleSin)
    {
        var (angleCos, angleSin) = doubleAngleSin.SinToHalfRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateDoubleAngleFromCosSin(IScalar<T> halfAngleCos, IScalar<T> halfAngleSin)
    {
        var angleCos = halfAngleCos.Square().Times(2) - 1;
        var angleSin = 2 * halfAngleCos.Times(halfAngleSin);

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateDoubleAngleFromCos(IScalar<T> halfAngleCos)
    {
        var (angleCos, angleSin) = halfAngleCos.CosToDoubleRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateDoubleAngleFromSin(IScalar<T> halfAngleSin)
    {
        var (angleCos, angleSin) = halfAngleSin.SinToDoubleRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateDoubleAngleFromTan(IScalar<T> halfAngleTan)
    {
        var (angleCos, angleSin) = halfAngleTan.TanToDoubleRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateTripleAngleFromCosSin(IScalar<T> thirdAngleCos, IScalar<T> thirdAngleSin)
    {
        var angleCos = thirdAngleCos.CosToTripleRadiansCos();
        var angleSin = thirdAngleSin.SinToTripleRadiansSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateTripleAngleFromCos(IScalar<T> thirdAngleCos)
    {
        var (angleCos, angleSin) = thirdAngleCos.CosToTripleRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateTripleAngleFromSin(IScalar<T> thirdAngleSin)
    {
        var (angleCos, angleSin) = thirdAngleSin.SinToTripleRadiansCosSin();

        return new LinPolarAngle<T>(angleCos, angleSin);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromVector(IScalar<T> x, IScalar<T> y)
    {
        var r = (x.Square() + y.Square()).Sqrt();

        //Debug.Assert(r.IsValid() && r.IsFinite());

        return r.IsZero() 
            ? Angle0(x.ScalarProcessor) 
            : new LinPolarAngle<T>(x / r, y / r);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromVector(IPair<Scalar<T>> vector)
    {
        return CreateFromVector(
            vector.Item1, 
            vector.Item2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromUnitVectors(IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return CreateFromCos(v1.VectorESp(v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromUnitVectors(ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return CreateFromCos(v1.VectorESp(v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CreateFromUnitVectors(IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return CreateFromCos(v1.ESp(v2));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(LinPolarAngle<T> a1)
    {
        return new LinPolarAngle<T>(a1.Cos(), -a1.Sin());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator +(T a1, LinPolarAngle<T> a2)
    {
        var scalarProcessor = a2.ScalarProcessor;

        var a1Cos = scalarProcessor.Cos(a1);
        var a1Sin = scalarProcessor.Sin(a1);

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos - a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator +(IScalar<T> a1, LinPolarAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos - a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator +(LinPolarAngle<T> a1, T a2)
    {
        var scalarProcessor = a1.ScalarProcessor;

        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = scalarProcessor.Cos(a2);
        var a2Sin = scalarProcessor.Sin(a2);

        var cosValue = a1Cos * a2Cos - a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator +(LinPolarAngle<T> a1, IScalar<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos - a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator +(LinPolarAngle<T> a1, LinAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos - a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator +(LinAngle<T> a1, LinPolarAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos - a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator +(LinPolarAngle<T> a1, LinPolarAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos - a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos + a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(T a1, LinPolarAngle<T> a2)
    {
        var scalarProcessor = a2.ScalarProcessor;

        var a1Cos = scalarProcessor.Cos(a1);
        var a1Sin = scalarProcessor.Sin(a1);

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos + a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(IScalar<T> a1, LinPolarAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos + a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(LinPolarAngle<T> a1, T a2)
    {
        var scalarProcessor = a1.ScalarProcessor;

        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = scalarProcessor.Cos(a2);
        var a2Sin = scalarProcessor.Sin(a2);

        var cosValue = a1Cos * a2Cos + a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(LinPolarAngle<T> a1, IScalar<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos + a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(LinPolarAngle<T> a1, LinAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos + a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(LinAngle<T> a1, LinPolarAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos + a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator -(LinPolarAngle<T> a1, LinPolarAngle<T> a2)
    {
        var a1Cos = a1.Cos();
        var a1Sin = a1.Sin();

        var a2Cos = a2.Cos();
        var a2Sin = a2.Sin();

        var cosValue = a1Cos * a2Cos + a1Sin * a2Sin;
        var sinValue = a1Sin * a2Cos - a1Cos * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, int a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, uint a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, long a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, ulong a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, float a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, double a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, T a2)
    {
        Debug.Assert(a2 != null, nameof(a2) + " != null");

        return (a1.Radians * a2).RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, Scalar<T> a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(LinPolarAngle<T> a1, IScalar<T> a2)
    {
        return (a1.Radians * a2).RadiansToPolarAngle();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(int a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(uint a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(long a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(ulong a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(float a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(double a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(T a1, LinPolarAngle<T> a2)
    {
        Debug.Assert(a1 != null, nameof(a1) + " != null");

        return (a1 * a2.Radians).RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(Scalar<T> a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator *(IScalar<T> a1, LinPolarAngle<T> a2)
    {
        return (a1 * a2.Radians).RadiansToPolarAngle();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, int a2)
    {
        return (a1.Radians / a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, uint a2)
    {
        return (a1.Radians / a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, long a2)
    {
        return (a1.Radians / a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, ulong a2)
    {
        return (a1.Radians / a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, float a2)
    {
        return (a1.Radians / a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, double a2)
    {
        return (a1.Radians / a2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, T a2)
    {
        Debug.Assert(a2 != null, nameof(a2) + " != null");

        return (a1.Radians / a2).RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> operator /(LinPolarAngle<T> a1, IScalar<T> a2)
    {
        return (a1.Radians / a2).RadiansToPolarAngle();
    }


    public override IScalarProcessor<T> ScalarProcessor { get; }

    public override T CosValue { get; }

    public override T SinValue { get; }

    public override T TanValue
        => ScalarProcessor.Divide(SinValue, CosValue).ScalarValue;

    public override T RadiansValue
    {
        get
        {
            var radians = ScalarProcessor.VectorToRadians(CosValue, SinValue);

            return (radians < 0 ? radians + ScalarProcessor.PiTimes2 : radians).ScalarValue;
        }
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinPolarAngle(IScalar<T> radians)
    {
        ScalarProcessor = radians.ScalarProcessor;
        CosValue = radians.Cos().ScalarValue;
        SinValue = radians.Sin().ScalarValue;

        Debug.Assert(IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinPolarAngle(IScalarProcessor<T> scalarProcessor, T radians)
    {
        ScalarProcessor = scalarProcessor;
        CosValue = scalarProcessor.Cos(radians).ScalarValue;
        SinValue = scalarProcessor.Sin(radians).ScalarValue;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinPolarAngle(IScalar<T> cosValue, IScalar<T> sinValue)
    {
        ScalarProcessor = cosValue.ScalarProcessor;
        CosValue = cosValue.ScalarValue;
        SinValue = sinValue.ScalarValue;

        Debug.Assert(IsValid());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinPolarAngle(IScalarProcessor<T> scalarProcessor, T cosValue, T sinValue)
    {
        ScalarProcessor = scalarProcessor;
        CosValue = cosValue;
        SinValue = sinValue;

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        if (!ScalarProcessor.IsValid(CosValue)) return false;
        if (!ScalarProcessor.IsValid(SinValue)) return false;

        var number = (Cos().Square() + Sin().Square()).ToFloat64();

        return double.IsNaN(number) || number.IsNearOne();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> NegativeAngle()
    {
        return new LinPolarAngle<T>(Cos(), -Sin());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> OppositeAngle()
    {
        return new LinPolarAngle<T>(-Cos(), -Sin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> AngleAdd(IScalar<T> angle2)
    {
        var a2Cos = angle2.Cos();
        var a2Sin = angle2.Sin();

        var cosValue = Cos() * a2Cos - Sin() * a2Sin;
        var sinValue = Sin() * a2Cos + Cos() * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> AngleSubtract(IScalar<T> angle2)
    {
        var a2Cos = angle2.Cos();
        var a2Sin = angle2.Sin();

        var cosValue = Cos() * a2Cos + Sin() * a2Sin;
        var sinValue = Sin() * a2Cos - Cos() * a2Sin;

        return new LinPolarAngle<T>(cosValue, sinValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> AngleTimes(T scalingFactor)
    {
        Debug.Assert(scalingFactor != null, nameof(scalingFactor) + " != null");
        
        return CreateFromRadians(Radians * scalingFactor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> AngleDivide(T scalingFactor)
    {
        Debug.Assert(scalingFactor != null, nameof(scalingFactor) + " != null");
        
        return CreateFromRadians(Radians / scalingFactor);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"{DegreesValue:G5} degrees";
    }
}
