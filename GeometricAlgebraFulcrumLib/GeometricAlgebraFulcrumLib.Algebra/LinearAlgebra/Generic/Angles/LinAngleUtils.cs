using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;

public static class LinAngleUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return (1 - scalarProcessor.Square(cosAngle)).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        Debug.Assert(cosAngle != null, nameof(cosAngle) + " != null");

        return (1 - scalarProcessor.Square(cosAngle)).Sqrt() / cosAngle;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToHalfRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return ((1 + scalarProcessor.ScalarFromValue(cosAngle)) / 2).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToHalfRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return ((1 - scalarProcessor.ScalarFromValue(cosAngle)) / 2).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToHalfRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        var cos = scalarProcessor.ScalarFromValue(cosAngle);

        return ((1 - cos) / (1 + cos)).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> CosToHalfRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return new Pair<Scalar<T>>(
            ((1 + scalarProcessor.ScalarFromValue(cosAngle)) / 2).Sqrt(),
            ((1 - scalarProcessor.ScalarFromValue(cosAngle)) / 2).Sqrt()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToDoubleRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return 2 * scalarProcessor.Square(cosAngle) - 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToDoubleRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        var sinAngle = (1 - scalarProcessor.Square(cosAngle)).Sqrt();

        Debug.Assert(cosAngle != null, nameof(cosAngle) + " != null");

        return 2 * (cosAngle * sinAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToDoubleRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        Debug.Assert(cosAngle != null, nameof(cosAngle) + " != null");
        
        var sinAngle = (1 - scalarProcessor.Square(cosAngle)).Sqrt();

        var cosDoubleRadians = 2 * scalarProcessor.Square(cosAngle) - 1;
        var sinDoubleRadians = 2 * (cosAngle * sinAngle);

        return sinDoubleRadians / cosDoubleRadians;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> CosToDoubleRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        Debug.Assert(cosAngle != null, nameof(cosAngle) + " != null");

        var sinAngle = (1 - scalarProcessor.Square(cosAngle)).Sqrt();

        var cosDoubleRadians = 2 * scalarProcessor.Square(cosAngle) - 1;
        var sinDoubleRadians = 2 * (cosAngle * sinAngle);

        return new Pair<Scalar<T>>(cosDoubleRadians, sinDoubleRadians);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToTripleRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        Debug.Assert(cosAngle != null, nameof(cosAngle) + " != null");

        return (4 * scalarProcessor.Square(cosAngle) - 3) * cosAngle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToTripleRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return scalarProcessor.CosToRadiansSin(cosAngle).SinToTripleRadiansSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToTripleRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return scalarProcessor.CosToTripleRadiansSin(cosAngle) / 
               scalarProcessor.CosToTripleRadiansCos(cosAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> CosToTripleRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T cosAngle)
    {
        return new Pair<Scalar<T>>(
            scalarProcessor.CosToTripleRadiansCos(cosAngle), 
            scalarProcessor.CosToTripleRadiansSin(cosAngle)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return (1 - scalarProcessor.Square(sinAngle)).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        Debug.Assert(sinAngle != null, nameof(sinAngle) + " != null");

        return sinAngle / (1 - scalarProcessor.Square(sinAngle)).Sqrt();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToHalfRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return scalarProcessor.SinToRadiansCos(sinAngle).CosToHalfRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToHalfRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return scalarProcessor.SinToRadiansCos(sinAngle).CosToHalfRadiansSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToHalfRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return scalarProcessor.SinToRadiansCos(sinAngle).CosToHalfRadiansTan();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> SinToHalfRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return scalarProcessor.SinToRadiansCos(sinAngle).CosToHalfRadiansCosSin();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToDoubleRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        

        return 1 - 2 * scalarProcessor.Square(sinAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToDoubleRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        Debug.Assert(sinAngle != null, nameof(sinAngle) + " != null");

        var cosAngle = (1 - scalarProcessor.Square(sinAngle)).Sqrt();

        return 2 * (sinAngle * cosAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToDoubleRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        Debug.Assert(sinAngle != null, nameof(sinAngle) + " != null");

        var cosAngle = (1 - scalarProcessor.Square(sinAngle)).Sqrt();

        var cosDoubleRadians = 1 - 2 * scalarProcessor.Square(sinAngle);
        var sinDoubleRadians = 2 * (sinAngle * cosAngle);

        return sinDoubleRadians / cosDoubleRadians;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> SinToDoubleRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        Debug.Assert(sinAngle != null, nameof(sinAngle) + " != null");

        var cosAngle = (1 - scalarProcessor.Square(sinAngle)).Sqrt();

        var cosDoubleRadians = 1 - 2 * scalarProcessor.Square(sinAngle);
        var sinDoubleRadians = 2 * (sinAngle * cosAngle);

        return new Pair<Scalar<T>>(cosDoubleRadians, sinDoubleRadians);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToTripleRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return scalarProcessor.SinToRadiansCos(sinAngle).CosToTripleRadiansCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToTripleRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        Debug.Assert(sinAngle != null, nameof(sinAngle) + " != null");

        return (3 - 4 * scalarProcessor.Square(sinAngle)) * sinAngle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToTripleRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return scalarProcessor.SinToTripleRadiansSin(sinAngle) / 
               scalarProcessor.SinToTripleRadiansCos(sinAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> SinToTripleRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T sinAngle)
    {
        return new Pair<Scalar<T>>(
            scalarProcessor.SinToTripleRadiansCos(sinAngle), 
            scalarProcessor.SinToTripleRadiansSin(sinAngle)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T angleTan)
    {
        var angleTanSign = scalarProcessor.Sign(angleTan);
        var angleTanSquared = scalarProcessor.Square(angleTan);
        var d = 1 + angleTanSquared;

        return angleTanSign / d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T angleTan)
    {
        var angleTanSquared = scalarProcessor.Square(angleTan);
        var d = 1 + angleTanSquared;

        return angleTanSquared / d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> TanToRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T angleTan)
    {
        var angleTanSign = scalarProcessor.Sign(angleTan);
        var angleTanSquared = scalarProcessor.Square(angleTan);
        var d = 1 + angleTanSquared;

        var cosValue = angleTanSign / d;
        var sinValue = angleTanSquared / d;

        return new Pair<Scalar<T>>(cosValue, sinValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanSquaredToRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T angleTanSquared)
    {
        return 1 / (1 + scalarProcessor.ScalarFromValue(angleTanSquared));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanSquaredToRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T angleTanSquared)
    {
        Debug.Assert(angleTanSquared != null, nameof(angleTanSquared) + " != null");
        
        return angleTanSquared / (1 + scalarProcessor.ScalarFromValue(angleTanSquared));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> TanSquaredToRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T angleTanSquared)
    {
        Debug.Assert(angleTanSquared != null, nameof(angleTanSquared) + " != null");
        
        var d = 1 + scalarProcessor.ScalarFromValue(angleTanSquared);

        var cosValue = 1 / d;
        var sinValue = angleTanSquared / d;

        return new Pair<Scalar<T>>(cosValue, sinValue);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToDoubleRadiansCos<T>(this IScalarProcessor<T> scalarProcessor, T angleTan)
    {
        var angleTanSquared = scalarProcessor.Square(angleTan);

        return (1 - angleTanSquared) / (1 + angleTanSquared);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToDoubleRadiansSin<T>(this IScalarProcessor<T> scalarProcessor, T angleTan)
    {
        Debug.Assert(angleTan != null, nameof(angleTan) + " != null");
        
        var angleTanSquared = scalarProcessor.Square(angleTan);

        return 2 * (angleTan / (1 + angleTanSquared));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToDoubleRadiansTan<T>(this IScalarProcessor<T> scalarProcessor, T angleTan)
    {
        Debug.Assert(angleTan != null, nameof(angleTan) + " != null");
        
        return 2 * (angleTan / (1 - scalarProcessor.Square(angleTan)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> TanToDoubleRadiansCosSin<T>(this IScalarProcessor<T> scalarProcessor, T angleTan)
    {
        Debug.Assert(angleTan != null, nameof(angleTan) + " != null");
        
        var angleTanSquared = scalarProcessor.Square(angleTan);
        var cosValue = (1 - angleTanSquared) / (1 + angleTanSquared);
        var sinValue = 2 * (angleTan / (1 + angleTanSquared));

        return new Pair<Scalar<T>>(cosValue, sinValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToRadiansSin<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToRadiansSin(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToRadiansTan<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToRadiansTan(cosAngle.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToHalfRadiansCos<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToHalfRadiansCos(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToHalfRadiansSin<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToHalfRadiansSin(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToHalfRadiansTan<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToHalfRadiansTan(cosAngle.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> CosToHalfRadiansCosSin<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToHalfRadiansCosSin(cosAngle.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToDoubleRadiansCos<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToDoubleRadiansCos(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToDoubleRadiansSin<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToDoubleRadiansSin(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToDoubleRadiansTan<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToDoubleRadiansTan(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> CosToDoubleRadiansCosSin<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToDoubleRadiansCosSin(cosAngle.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToTripleRadiansCos<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToTripleRadiansCos(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToTripleRadiansSin<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToTripleRadiansSin(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> CosToTripleRadiansTan<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToTripleRadiansTan(cosAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> CosToTripleRadiansCosSin<T>(this IScalar<T> cosAngle)
    {
        return cosAngle.ScalarProcessor.CosToTripleRadiansCosSin(cosAngle.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToRadiansCos<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToRadiansCos(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToRadiansTan<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToRadiansTan(sinAngle.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToHalfRadiansCos<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToHalfRadiansCos(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToHalfRadiansSin<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToHalfRadiansSin(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToHalfRadiansTan<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToHalfRadiansTan(sinAngle.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> SinToHalfRadiansCosSin<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToHalfRadiansCosSin(sinAngle.ScalarValue);
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToDoubleRadiansCos<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToDoubleRadiansCos(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToDoubleRadiansSin<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToDoubleRadiansSin(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToDoubleRadiansTan<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToDoubleRadiansTan(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> SinToDoubleRadiansCosSin<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToDoubleRadiansCosSin(sinAngle.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToTripleRadiansCos<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToTripleRadiansCos(sinAngle.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToTripleRadiansSin<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToTripleRadiansSin(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SinToTripleRadiansTan<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToTripleRadiansTan(sinAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> SinToTripleRadiansCosSin<T>(this IScalar<T> sinAngle)
    {
        return sinAngle.ScalarProcessor.SinToTripleRadiansCosSin(sinAngle.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToRadiansCos<T>(this IScalar<T> tanAngle)
    {
        return tanAngle.ScalarProcessor.TanToRadiansCos(tanAngle.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToRadiansSin<T>(this IScalar<T> tanAngle)
    {
        return tanAngle.ScalarProcessor.TanToRadiansSin(tanAngle.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> TanToRadiansCosSin<T>(this IScalar<T> tanAngle)
    {
        return tanAngle.ScalarProcessor.TanToRadiansCosSin(tanAngle.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanSquaredToRadiansCos<T>(this IScalar<T> angleTanSquared)
    {
        return angleTanSquared.ScalarProcessor.TanSquaredToRadiansCos(angleTanSquared.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanSquaredToRadiansSin<T>(this IScalar<T> angleTanSquared)
    {
        return angleTanSquared.ScalarProcessor.TanSquaredToRadiansSin(angleTanSquared.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> TanSquaredToRadiansCosSin<T>(this IScalar<T> angleTanSquared)
    {
        return angleTanSquared.ScalarProcessor.TanSquaredToRadiansCosSin(angleTanSquared.ScalarValue);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToDoubleRadiansCos<T>(this IScalar<T> angleTan)
    {
        return angleTan.ScalarProcessor.TanToDoubleRadiansCos(angleTan.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TanToDoubleRadiansSin<T>(this IScalar<T> angleTan)
    {
        return angleTan.ScalarProcessor.TanToDoubleRadiansSin(angleTan.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Scalar<T>> TanToDoubleRadiansCosSin<T>(this IScalar<T> angleTan)
    {
        return angleTan.ScalarProcessor.TanToDoubleRadiansCosSin(angleTan.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetRadians<T>(this LinAngleRange range, IScalar<T> angleInRadians)
    {
        var angleInRadiansNumber = angleInRadians.ToFloat64();

        return angleInRadiansNumber.IsNaNOrInfinite() 
            ? angleInRadians.ToScalar() 
            : range.GetRadians(angleInRadiansNumber).ScalarFromNumber(angleInRadians.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDegrees<T>(this LinAngleRange range, IScalar<T> angleInDegrees)
    {
        var angleInDegreesNumber = angleInDegrees.ToFloat64();

        return angleInDegreesNumber.IsNaNOrInfinite() 
            ? angleInDegrees.ToScalar() 
            : range.GetRadians(angleInDegreesNumber).ScalarFromNumber(angleInDegrees.ScalarProcessor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngleFromRadians<T>(this LinAngleRange range, IScalar<T> angleInRadians)
    {
        var radians = range.GetRadians(angleInRadians);

        return LinDirectedAngle<T>.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngleFromDegrees<T>(this LinAngleRange range, IScalar<T> angleInDegrees)
    {
        var radians = range.GetDegrees(angleInDegrees);

        return LinDirectedAngle<T>.CreateFromDegrees(radians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPolarAngleFromRadians<T>(this LinAngleRange range, IScalar<T> angleInRadians)
    {
        var radians = range.GetRadians(angleInRadians);

        return LinPolarAngle<T>.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPolarAngleFromDegrees<T>(this LinAngleRange range, IScalar<T> angleInDegrees)
    {
        var radians = range.GetDegrees(angleInDegrees);

        return LinPolarAngle<T>.CreateFromDegrees(radians);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPolarAngle<T>(this Random randomGenerator, IScalarProcessor<T> scalarProcessor)
    {
        var radians = randomGenerator.GetNumber(-2, 2) * scalarProcessor.Pi;

        return LinPolarAngle<T>.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPolarAngleInQuadrant<T>(this Random randomGenerator, int quadrantIndex, IScalarProcessor<T> scalarProcessor)
    {
        var radians = (randomGenerator.GetNumber(0, 1) + quadrantIndex % 4) * scalarProcessor.PiOver2;

        return LinPolarAngle<T>.CreateFromRadians(radians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPolarAngleFromArcRatio<T>(this Random randomGenerator, double maxAngleRatio, IScalarProcessor<T> scalarProcessor)
    {
        Debug.Assert(maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(0, maxAngleRatio) * scalarProcessor.PiTimes2).RadiansToPolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPolarAngleFromArcRatio<T>(this Random randomGenerator, double minAngleRatio, double maxAngleRatio, IScalarProcessor<T> scalarProcessor)
    {
        Debug.Assert(minAngleRatio >= -1 && maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(minAngleRatio, maxAngleRatio) * scalarProcessor.PiTimes2).RadiansToPolarAngle();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngle<T>(this Random randomGenerator, IScalarProcessor<T> scalarProcessor)
    {
        return (randomGenerator.GetNumber(-2, 2) * scalarProcessor.Pi).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngleInQuadrant<T>(this Random randomGenerator, int quadrantIndex, IScalarProcessor<T> scalarProcessor)
    {
        return ((randomGenerator.GetNumber(0, 1) + quadrantIndex % 4) * scalarProcessor.PiOver2).RadiansToDirectedAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngleFromRatio<T>(this Random randomGenerator, double maxAngleRatio, IScalarProcessor<T> scalarProcessor)
    {
        Debug.Assert(maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(0, maxAngleRatio) * scalarProcessor.PiTimes2).RadiansToDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngleFromRatio<T>(this Random randomGenerator, double minAngleRatio, double maxAngleRatio, IScalarProcessor<T> scalarProcessor)
    {
        Debug.Assert(minAngleRatio >= -1 && maxAngleRatio <= 1);

        return (randomGenerator.GetNumber(minAngleRatio, maxAngleRatio) * scalarProcessor.PiTimes2).RadiansToDirectedAngle();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinAngle<T> NegativeAngle<T>(this LinAngle<T> angle)
    {
        return angle switch
        {
            LinDirectedAngle<T> directedAngle => 
                directedAngle.NegativeAngle(),

            LinPolarAngle<T> polarAngle => 
                polarAngle.NegativeAngle(),

            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinAngle<T> OppositeAngle<T>(this LinAngle<T> angle)
    {
        return angle switch
        {
            LinDirectedAngle<T> directedAngle => 
                directedAngle.OppositeAngle(),

            LinPolarAngle<T> polarAngle => 
                polarAngle.OppositeAngle(),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinAngle<T> AngleAdd<T>(this LinAngle<T> angle, IScalar<T> angle2)
    {
        return angle switch
        {
            LinDirectedAngle<T> directedAngle => 
                directedAngle.AngleAdd(angle2),

            LinPolarAngle<T> polarAngle => 
                polarAngle.AngleAdd(angle2),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinAngle<T> AngleSubtract<T>(this LinAngle<T> angle, IScalar<T> angle2)
    {
        return angle switch
        {
            LinDirectedAngle<T> directedAngle => 
                directedAngle.AngleSubtract(angle2),

            LinPolarAngle<T> polarAngle => 
                polarAngle.AngleSubtract(angle2),

            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinAngle<T> AngleTimes<T>(this LinAngle<T> angle, T scalingFactor)
    {
        return angle switch
        {
            LinDirectedAngle<T> directedAngle => 
                directedAngle.AngleTimes(scalingFactor),

            LinPolarAngle<T> polarAngle => 
                polarAngle.AngleTimes(scalingFactor),

            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinAngle<T> AngleDivide<T>(this LinAngle<T> angle, T scalingFactor)
    {
        return angle switch
        {
            LinDirectedAngle<T> directedAngle => 
                directedAngle.AngleDivide(scalingFactor),

            LinPolarAngle<T> polarAngle => 
                polarAngle.AngleDivide(scalingFactor),

            _ => throw new InvalidOperationException()
        };
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetPhaseAsDirectedAngle<T>(this IPair<Scalar<T>> vector)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinDirectedAngle<T>.Angle0(vector.GetScalarProcessor()) 
            : LinDirectedAngle<T>.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetPhaseAsDirectedAngle<T>(this IPair<Scalar<T>> vector, LinAngleRange range)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinDirectedAngle<T>.Angle0(vector.GetScalarProcessor()) 
            : LinDirectedAngle<T>.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r,
                range
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPhaseAsPolarAngle<T>(this IPair<Scalar<T>> vector)
    {
        var r = vector.VectorENorm();

        return r.IsZero() 
            ? LinPolarAngle<T>.Angle0(vector.GetScalarProcessor()) 
            : LinPolarAngle<T>.CreateFromCosSin(
                vector.Item1 / r, 
                vector.Item2 / r
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetPhaseAsDirectedAngle<T>(this ComplexNumber<T> complexNumber)
    {
        var r = complexNumber.Magnitude;

        return r.IsZero() 
            ? LinDirectedAngle<T>.Angle0(complexNumber.ScalarProcessor) 
            : LinDirectedAngle<T>.CreateFromCosSin(
                complexNumber.Real / r, 
                complexNumber.Imaginary / r
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetPhaseAsDirectedAngle<T>(this ComplexNumber<T> complexNumber, LinAngleRange range)
    {
        var r = complexNumber.Magnitude;

        return r.IsZero() 
            ? LinDirectedAngle<T>.Angle0(complexNumber.ScalarProcessor) 
            : LinDirectedAngle<T>.CreateFromCosSin(
                complexNumber.Real / r, 
                complexNumber.Imaginary / r,
                range
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetPhaseAsPolarAngle<T>(this ComplexNumber<T> complexNumber)
    {
        var r = complexNumber.Magnitude;

        return r.IsZero() 
            ? LinPolarAngle<T>.Angle0(complexNumber.ScalarProcessor) 
            : LinPolarAngle<T>.CreateFromCosSin(
                complexNumber.Real / r, 
                complexNumber.Imaginary / r
            );
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCosWithUnit<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        Debug.Assert(
            v2.VectorIsNearUnit()
        );

        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
        var t2 = (v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2).Sqrt();

        return t1 / t2;
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCosWithUnit<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1 * v2.Item1 +
            v1.Item2 * v2.Item2 +
            v1.Item3 * v2.Item3;

        var t2 = (
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2 +
            v1.Item3 * v1.Item3
        ).Sqrt();

        return t1 / t2;
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCosWithUnit<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        Debug.Assert(
            v2.IsNearUnit()
        );

        var t1 =
            v1.Item1 * v2.Item1 +
            v1.Item2 * v2.Item2 +
            v1.Item3 * v2.Item3 +
            v1.Item4 * v2.Item4;

        var t2 = (
            v1.Item1 * v1.Item1 +
            v1.Item2 * v1.Item2 +
            v1.Item3 * v1.Item3 +
            v1.Item4 * v1.Item4
        ).Sqrt();

        return t1 / t2;
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetUnitVectorsAngleCos<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetUnitVectorsAngleCos<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3;
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetUnitVectorsAngleCos<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        Debug.Assert(
            v1.IsNearUnitVector() &&
            v2.IsNearUnitVector()
        );

        return v1.Item1 * v2.Item1 +
               v1.Item2 * v2.Item2 +
               v1.Item3 * v2.Item3 +
               v1.Item4 * v2.Item4;
    }

    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCos<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2;
        var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2;
        var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2;

        return t1 / (t2 * t3).Sqrt();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCos<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3;
        var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3;
        var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2 + v2.Item3 * v2.Item3;

        return t1 / (t2 * t3).Sqrt();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCos<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        var t1 = v1.Item1 * v2.Item1 + v1.Item2 * v2.Item2 + v1.Item3 * v2.Item3 + v1.Item4 * v2.Item4;
        var t2 = v1.Item1 * v1.Item1 + v1.Item2 * v1.Item2 + v1.Item3 * v1.Item3 + v1.Item4 * v1.Item4;
        var t3 = v2.Item1 * v2.Item1 + v2.Item2 * v2.Item2 + v2.Item3 * v2.Item3 + v2.Item4 * v2.Item4;

        return t1 / (t2 * t3).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCos<T>(this LinVector<T> vector1, LinVector<T> vector2)
    {
        var uuDot = vector1.ENormSquared();
        var vvDot = vector2.ENormSquared();
        var uvDot = vector1.ESp(vector2);

        var norm = (uuDot * vvDot).Sqrt();

        return norm.IsZero()
            ? vector1.ScalarProcessor.Zero
            : uvDot / norm;
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngleWithUnit<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return v1.GetAngleCosWithUnit(v2).ArcCos();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngleWithUnit<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return v1.GetAngleCosWithUnit(v2).ArcCos();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngleWithUnit<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return v1.GetAngleCosWithUnit(v2).ArcCos();
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Scalar<T> GetAngleCosWithUnit<T>(this LinVector<T> vector1, ILinSignedBasisVector vector2)
    //{
    //    Debug.Assert(
    //        vector2.Sign.IsNotZero
    //    );

    //    var uuDot = vector1.ENormSquared();
    //    var uvDot = vector1.ESp(vector2);

    //    var norm = uuDot.Sqrt();

    //    return norm.IsZero()
    //        ? vector1.ScalarProcessor.Zero
    //        : uvDot / norm;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetAngleCosWithUnit<T>(this LinVector<T> vector1, LinVector<T> vector2)
    {
        //Debug.Assert(
        //    vector2.IsNearUnit()
        //);

        var uuDot = vector1.ENormSquared();
        var uvDot = vector1.ESp(vector2);

        var norm = uuDot.Sqrt();

        return norm.IsZero()
            ? vector1.ScalarProcessor.Zero
            : uvDot / norm;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetUnitVectorsAngleCos<T>(this LinVector<T> vector1, LinVector<T> vector2)
    {
        return vector1.ESp(vector2);
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinPolarAngle<T> GetAngleWithUnit<T>(this LinVector<T> vector1, ILinSignedBasisVector vector2)
    //{
    //    return vector1.GetAngleCosWithUnit(vector2).ArcCos();
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngleWithUnit<T>(this LinVector<T> vector1, LinVector<T> vector2)
    {
        return vector1.GetAngleCosWithUnit(vector2).ArcCos();
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetUnitVectorsAngle<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).ArcCos();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetUnitVectorsAngle<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).ArcCos().RadiansToPolarAngle();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetUnitVectorsAngle<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return v1.GetUnitVectorsAngleCos(v2).ArcCos().RadiansToPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetUnitVectorsAngle<T>(this LinVector<T> vector1, LinVector<T> vector2)
    {
        return vector1.GetUnitVectorsAngleCos(vector2).ArcCos();
    }


    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngle<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        return v1.GetAngleCos(v2).ArcCos();
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngle<T>(this ITriplet<Scalar<T>> v1, ITriplet<Scalar<T>> v2)
    {
        return v1.GetAngleCos(v2).ArcCos();
    }
    
    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngle<T>(this IQuad<Scalar<T>> v1, IQuad<Scalar<T>> v2)
    {
        return v1.GetAngleCos(v2).ArcCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngle<T>(this LinVector<T> vector1, LinVector<T> vector2, bool assumeUnitVectors)
    {
        var v12Sp = vector1.ESp(vector2);

        var angle = assumeUnitVectors
            ? v12Sp
            : v12Sp / (vector1.ENormSquared() * vector2.ENormSquared()).Sqrt();

        return angle.ArcCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngle<T>(this LinVector<T> vector1, LinVector<T> vector2)
    {
        return vector1.GetAngleCos(vector2).ArcCos();
    }


    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngleFromPoints<T>(this IPair<Scalar<T>> p0, IPair<Scalar<T>> p1, IPair<Scalar<T>> p2)
    {
        var v1 = LinVector2D<T>.Create(
            p1.Item1 - p0.Item1,
            p1.Item2 - p0.Item2
        );

        var v2 = LinVector2D<T>.Create(
            p2.Item1 - p0.Item1,
            p2.Item2 - p0.Item2
        );

        return v1.GetAngle(v2);
    }

    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngleFromPoints<T>(this ITriplet<Scalar<T>> p0, ITriplet<Scalar<T>> p1, ITriplet<Scalar<T>> p2)
    {
        var v1 = LinVector3D<T>.Create(
            p1.Item1 - p0.Item1,
            p1.Item2 - p0.Item2,
            p1.Item3 - p0.Item3
        );

        var v2 = LinVector3D<T>.Create(
            p2.Item1 - p0.Item1,
            p2.Item2 - p0.Item2,
            p2.Item3 - p0.Item3
        );

        return v1.GetAngle(v2);
    }
    
    /// <summary>
    /// Find the angle between points (p1, p0, p2); i.e. p0 is the head of the angle
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> GetAngleFromPoints<T>(this IQuad<Scalar<T>> p0, IQuad<Scalar<T>> p1, IQuad<Scalar<T>> p2)
    {
        var v1 = LinVector4D<T>.Create(
            p1.Item1 - p0.Item1,
            p1.Item2 - p0.Item2,
            p1.Item3 - p0.Item3,
            p1.Item4 - p0.Item4
        );

        var v2 = LinVector4D<T>.Create(
            p2.Item1 - p0.Item1,
            p2.Item2 - p0.Item2,
            p2.Item3 - p0.Item3,
            p2.Item4 - p0.Item4
        );

        return v1.GetAngle(v2);
    }

    
    /// <summary>
    /// Find the directed angle between this vector and another
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngle<T>(this IPair<Scalar<T>> v1, IPair<Scalar<T>> v2)
    {
        var angle1 = v1.GetPhaseAsDirectedAngle();
        var angle2 = v2.GetPhaseAsDirectedAngle();

        return angle2.AngleSubtract(angle1.RadiansValue);

        //var angle1 = Math.Atan2(v1.Item2.ScalarValue, v1.Item1.ScalarValue);
        //var angle2 = Math.Atan2(v2.Item2.ScalarValue, v2.Item1.ScalarValue);

        //return (angle2 - angle1).RadiansToDirectedAngle();
    }

    /// <summary>
    /// Find the directed angle from three points ABC
    /// </summary>
    /// <param name="pointA"></param>
    /// <param name="pointB"></param>
    /// <param name="pointC"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> GetDirectedAngleFromPoints<T>(this IPair<Scalar<T>> pointA, IPair<Scalar<T>> pointB, IPair<Scalar<T>> pointC)
    {
        return GetDirectedAngle(
            pointA.VectorSubtract(pointB), 
            pointC.VectorSubtract(pointB)
        );

        //var v1 = pointA.Subtract(pointB).ToUnitComplexNumber();
        //var v2 = pointC.Subtract(pointB).ToUnitComplexNumber();

        //return (v2 / v1).GetPhaseAsPolarAngle().ToDirectedAngle();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CosToPolarAngle<T>(this IScalar<T> angleCos)
    {
        return LinPolarAngle<T>.CreateFromCos(angleCos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> SinToPolarAngle<T>(this IScalar<T> angleSin)
    {
        return LinPolarAngle<T>.CreateFromSin(angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> TanToPolarAngle<T>(this IScalar<T> angleTan)
    {
        return LinPolarAngle<T>.CreateFromTan(angleTan);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> CosToDoublePolarAngle<T>(this IScalar<T> angleCos)
    {
        return LinPolarAngle<T>.CreateDoubleAngleFromCos(angleCos);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> SinToDoublePolarAngle<T>(this IScalar<T> angleSin)
    {
        return LinPolarAngle<T>.CreateDoubleAngleFromSin(angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> TanToDoublePolarAngle<T>(this IScalar<T> angleTan)
    {
        return LinPolarAngle<T>.CreateDoubleAngleFromTan(angleTan);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CosToDirectedAngle<T>(this IScalar<T> angleCos)
    {
        return LinDirectedAngle<T>.CreateFromCos(angleCos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> SinToDirectedAngle<T>(this IScalar<T> angleSin)
    {
        return LinDirectedAngle<T>.CreateFromSin(angleSin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> TanToDirectedAngle<T>(this IScalar<T> angleTan)
    {
        return LinDirectedAngle<T>.CreateFromTan(angleTan);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> CosToDoubleDirectedAngle<T>(this IScalar<T> angleCos)
    {
        return LinDirectedAngle<T>.CreateDoubleAngleFromCos(angleCos);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> SinToDoubleDirectedAngle<T>(this IScalar<T> angleSin)
    {
        return LinDirectedAngle<T>.CreateDoubleAngleFromSin(angleSin);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> TanToDoubleDirectedAngle<T>(this IScalar<T> angleTan)
    {
        return LinDirectedAngle<T>.CreateDoubleAngleFromTan(angleTan);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> DegreesToPolarAngle<T>(this IScalar<T> angleInDegrees)
    {
        return LinPolarAngle<T>.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalar<T> angleInDegrees)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(angleInDegrees);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> DegreesToDirectedAngle<T>(this IScalar<T> angleInDegrees, LinAngleRange range)
    {
        return LinDirectedAngle<T>.CreateFromDegrees(angleInDegrees, range);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> RadiansToPolarAngle<T>(this IScalar<T> angleInRadians)
    {
        return LinPolarAngle<T>.CreateFromRadians(angleInRadians);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalar<T> angleInRadians)
    {
        return LinDirectedAngle<T>.CreateFromRadians(angleInRadians);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> RadiansToDirectedAngle<T>(this IScalar<T> angleInRadians, LinAngleRange range)
    {
        return LinDirectedAngle<T>.CreateFromRadians(angleInRadians, range);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexNumber<T> ToComplexNumber<T>(this LinAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        return new ComplexNumber<T>(
            scalarProcessor,
            angle.CosValue,
            angle.SinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexNumber<T> ToComplexNumber<T>(this LinAngle<T> angle, T modulusValue)
    {
        var scalarProcessor = angle.ScalarProcessor;

        return new ComplexNumber<T>(
            scalarProcessor.Times(modulusValue, angle.CosValue),
            scalarProcessor.Times(modulusValue, angle.SinValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexNumber<T> ToComplexConjugateNumber<T>(this LinAngle<T> angle)
    {
        var scalarProcessor = angle.ScalarProcessor;

        return new ComplexNumber<T>(
            angle.Cos(),
            scalarProcessor.Negative(angle.SinValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexNumber<T> ToComplexConjugateNumber<T>(this LinAngle<T> angle, T modulusValue)
    {
        var scalarProcessor = angle.ScalarProcessor;

        return new ComplexNumber<T>(
            scalarProcessor.Times(modulusValue, angle.CosValue),
            scalarProcessor.NegativeTimes(modulusValue, angle.SinValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinVector2D<T>> RotateBasisFrame2D<T>(this LinAngle<T> angle)
    {
        return new Pair<LinVector2D<T>>(
            angle.Rotate(LinVector2D<T>.E1(angle.ScalarProcessor)),
            angle.Rotate(LinVector2D<T>.E2(angle.ScalarProcessor))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Rotate<T>(this LinAngle<T> angle, int x, int y)
    {
        var angleCos = angle.Cos();
        var angleSin = angle.Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Rotate<T>(this LinAngle<T> angle, T x, T y)
    {
        var angleCos = angle.Cos();
        var angleSin = angle.Sin();

        Debug.Assert(x is not null && y is not null);

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Rotate<T>(this LinAngle<T> angle, IScalar<T> sx, IScalar<T> sy)
    {
        var x = sx.ScalarValue;
        var y = sy.ScalarValue;

        Debug.Assert(x is not null && y is not null);

        var angleCos = angle.Cos();
        var angleSin = angle.Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Rotate<T>(this LinAngle<T> angle, LinUnitBasisVector2D axis)
    {
        var (x, y) = axis.ToVector2D(angle.ScalarProcessor);

        var angleCos = angle.Cos();
        var angleSin = angle.Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Rotate<T>(this LinAngle<T> angle, IPair<Scalar<T>> vector)
    {
        var x = vector.Item1;
        var y = vector.Item2;

        var angleCos = angle.Cos();
        var angleSin = angle.Sin();

        var x1 = x * angleCos - y * angleSin;
        var y1 = x * angleSin + y * angleCos;

        return LinVector2D<T>.Create(x1, y1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinVector2D<T>> Rotate<T>(this LinAngle<T> angle, IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2)
    {
        return new Pair<LinVector2D<T>>(
            angle.Rotate(vector1),
            angle.Rotate(vector2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<LinVector2D<T>> Rotate<T>(this LinAngle<T> angle, IPair<Scalar<T>> vector1, IPair<Scalar<T>> vector2, IPair<Scalar<T>> vector3)
    {
        return new Triplet<LinVector2D<T>>(
            angle.Rotate(vector1),
            angle.Rotate(vector2),
            angle.Rotate(vector3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinVector2D<T>> Rotate<T>(this LinAngle<T> angle, params IPair<Scalar<T>>[] vectorArray)
    {
        return vectorArray
            .Select(angle.Rotate)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector2D<T>> Rotate<T>(this LinAngle<T> angle, IEnumerable<IPair<Scalar<T>>> vectorList)
    {
        return vectorList.Select(angle.Rotate);
    }


}