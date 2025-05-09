using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarProcessorAngleUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle0Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle30Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(30);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle45Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(45);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle60Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(60);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle90Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.PiOver2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle120Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(120);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle135Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(135);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle150Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(150);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle180Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Pi;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle210Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(210);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle225Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(225);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle240Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(240);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle270Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(270);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle300Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(300);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle315Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(315);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle330Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(330);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Angle360Radians<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.DegreesToRadians(360);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cos45<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.One.Divide( 
            scalarProcessor.Sqrt(scalarProcessor.TwoValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sin45<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.One.Divide( 
            scalarProcessor.Sqrt(scalarProcessor.TwoValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cos30<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(3).Sqrt() / scalarProcessor.Two;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sin30<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Divide(
            scalarProcessor.OneValue, 
            scalarProcessor.TwoValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cos60<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Divide(
            scalarProcessor.OneValue, 
            scalarProcessor.TwoValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sin60<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(3).Sqrt() / scalarProcessor.Two;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, int angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360 => angleInDegrees % 720 + 360,
            > 360 => angleInDegrees % 360,
            _ => angleInDegrees
        };

        return scalarProcessor.DegreeToRadianFactor.Times(angleInDegrees);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, uint angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            > 360 => angleInDegrees % 360u,
            _ => angleInDegrees
        };

        return scalarProcessor.DegreeToRadianFactor.Times(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, long angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360L => angleInDegrees % 720L + 360L,
            > 360L => angleInDegrees % 360L,
            _ => angleInDegrees
        };

        return scalarProcessor.DegreeToRadianFactor.Times(angleInDegrees);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, ulong angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            > 360L => angleInDegrees % 360UL,
            _ => angleInDegrees
        };

        return scalarProcessor.DegreeToRadianFactor.Times(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, float angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360f => angleInDegrees % 720f + 360f,
            > 360f => angleInDegrees % 360f,
            _ => angleInDegrees
        };

        return scalarProcessor.DegreeToRadianFactor.Times(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, double angleInDegrees)
    {
        angleInDegrees = angleInDegrees switch
        {
            < -360d => angleInDegrees % 720d + 360d,
            > 360d => angleInDegrees % 360d,
            _ => angleInDegrees
        };

        return scalarProcessor.DegreeToRadianFactor.Times(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalarProcessor<T> scalarProcessor, T angleInDegrees)
    {
        return scalarProcessor.Times(
            scalarProcessor.DegreeToRadianFactorValue,
            angleInDegrees
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RadiansToDegrees<T>(this IScalarProcessor<T> scalarProcessor, float angleInRadians)
    {
        return scalarProcessor.RadianToDegreeFactor.Times(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RadiansToDegrees<T>(this IScalarProcessor<T> scalarProcessor, double angleInRadians)
    {
        return scalarProcessor.RadianToDegreeFactor.Times(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RadiansToDegrees<T>(this IScalarProcessor<T> scalarProcessor, T angleInRadians)
    {
        return scalarProcessor.Times(
            scalarProcessor.RadianToDegreeFactorValue,
            angleInRadians
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, uint denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, uint numerator, T denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, long denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, long numerator, T denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, ulong denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, ulong numerator, T denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatio<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
    {
        return scalarProcessor.Rational(numerator, denominator) * scalarProcessor.Pi;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, T numerator, uint denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, uint numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, T numerator, long denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, long numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, T numerator, ulong denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, ulong numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioCos<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Cos();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, T numerator, uint denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, uint numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, T numerator, long denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, long numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, T numerator, ulong denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, ulong numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> PiRatioSin<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).Sin();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcCos<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return LinPolarAngle<T>.CreateFromCos(
            scalarProcessor.Positive(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcSin<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return LinPolarAngle<T>.CreateFromSin(
            scalarProcessor.Positive(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return LinPolarAngle<T>.CreateFromSin(
            scalarProcessor.Positive(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalarProcessor<T> scalarProcessor, T scalarX, T scalarY)
    {
        return LinPolarAngle<T>.CreateFromVector(
            scalarProcessor.Positive(scalarX),
            scalarProcessor.Positive(scalarY)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, uint denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, uint numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, long denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, long numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, ulong denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, ulong numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> PiRatioToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToPolarAngle();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, int numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, int denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, int numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, uint denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, uint numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, long denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, long numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, ulong denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, ulong numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> PiRatioToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T numerator, T denominator)
    {
        return scalarProcessor.PiRatio(numerator, denominator).RadiansToDirectedAngle();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, int degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, uint degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, long degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, ulong degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, float degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, double degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, string degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromText(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T degrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromValue(degrees)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this int degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this uint degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this long degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this ulong degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this float degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this double degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this string degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromText(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this T degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromValue(degrees)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, float radians)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, double radians)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, string radians)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromText(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this IScalarProcessor<T> scalarProcessor, T radians)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(radians)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this float radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this double radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this string radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromText(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this T radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinPolarAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(radians)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, int degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, uint degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, long degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, ulong degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, float degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, double degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, string degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromText(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T degrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromValue(degrees)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this int degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this uint degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this long degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this ulong degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this float degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this double degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromNumber(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this string degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromText(degrees)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this T degrees, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(
            scalarProcessor.ScalarFromValue(degrees)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, float radians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, double radians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, string radians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromText(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T radians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(radians)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this float radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this double radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this string radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromText(radians)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this T radians, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(radians)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, float radians, LinAngleRange range)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians),
            range
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, double radians, LinAngleRange range)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians),
            range
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, string radians, LinAngleRange range)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromText(radians),
            range
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalarProcessor<T> scalarProcessor, T radians, LinAngleRange range)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(radians),
            range
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this float radians, LinAngleRange range, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians),
            range
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this double radians, LinAngleRange range, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromNumber(radians),
            range
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this string radians, LinAngleRange range, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromText(radians),
            range
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this T radians, LinAngleRange range, IScalarProcessor<T> scalarProcessor)
    {
        return LinDirectedAngle<T>.CreateFromRadians(
            scalarProcessor.ScalarFromValue(radians),
            range
        );
    }
}