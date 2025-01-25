using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

public static class RGaFloat64KVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearBlade(this RGaFloat64KVector kv, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return kv
            .Gp(kv.Reverse())
            .GetKVectorParts()
            .All(kv1 => 
                kv1.Grade == 0 || kv1.IsNearZero(zeroEpsilon)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector GetPart(this RGaFloat64KVector mv, Func<ulong, bool> filterFunc)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.GetPart(filterFunc),
            RGaFloat64Vector v => v.GetPart(filterFunc),
            RGaFloat64Bivector bv => bv.GetPart(filterFunc),
            RGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector GetPart(this RGaFloat64KVector mv, Func<double, bool> filterFunc)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.GetPart(filterFunc),
            RGaFloat64Vector v => v.GetPart(filterFunc),
            RGaFloat64Bivector bv => bv.GetPart(filterFunc),
            RGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector GetPart(this RGaFloat64KVector mv, Func<ulong, double, bool> filterFunc)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.GetPart(filterFunc),
            RGaFloat64Vector v => v.GetPart(filterFunc),
            RGaFloat64Bivector bv => bv.GetPart(filterFunc),
            RGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector RemoveSmallTerms(this RGaFloat64KVector mv, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s,
            RGaFloat64Vector v => v.RemoveSmallTerms(zeroEpsilon),
            RGaFloat64Bivector bv => bv.RemoveSmallTerms(zeroEpsilon),
            RGaFloat64HigherKVector kv => kv.RemoveSmallTerms(zeroEpsilon),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector MapScalars(this RGaFloat64KVector mv, Func<double, double> scalarMapping)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.MapScalar(scalarMapping),
            RGaFloat64Vector v => v.MapScalars(scalarMapping),
            RGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            RGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector MapScalars(this RGaFloat64KVector mv, Func<ulong, double, double> scalarMapping)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.MapScalar(scalarMapping),
            RGaFloat64Vector v => v.MapScalars(scalarMapping),
            RGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            RGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Negative(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Negative(),
            RGaFloat64Vector mv1 => mv1.Negative(),
            RGaFloat64Bivector mv1 => mv1.Negative(),
            RGaFloat64HigherKVector mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Reverse(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector => mv,
            RGaFloat64Bivector mv1 => mv1.Negative(),
            RGaFloat64HigherKVector mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector GradeInvolution(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector mv1 => mv1.Negative(),
            RGaFloat64Bivector mv1 => mv1,
            RGaFloat64HigherKVector mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector CliffordConjugate(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector mv1 => mv1.Negative(),
            RGaFloat64Bivector mv1 => mv1.Negative(),
            RGaFloat64HigherKVector mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Conjugate(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector mv1 => mv1.Conjugate(),
            RGaFloat64Bivector mv1 => mv1.Conjugate(),
            RGaFloat64HigherKVector mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Times(this RGaFloat64KVector mv, double scalarValue)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Times(scalarValue),
            RGaFloat64Vector mv1 => mv1.Times(scalarValue),
            RGaFloat64Bivector mv1 => mv1.Times(scalarValue),
            RGaFloat64HigherKVector mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Divide(this RGaFloat64KVector mv, double scalarValue)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Divide(scalarValue),
            RGaFloat64Vector mv1 => mv1.Divide(scalarValue),
            RGaFloat64Bivector mv1 => mv1.Divide(scalarValue),
            RGaFloat64HigherKVector mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector DivideByENorm(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByENorm(),
            RGaFloat64Vector mv1 => mv1.DivideByENorm(),
            RGaFloat64Bivector mv1 => mv1.DivideByENorm(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector DivideByENormSquared(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByENormSquared(),
            RGaFloat64Vector mv1 => mv1.DivideByENormSquared(),
            RGaFloat64Bivector mv1 => mv1.DivideByENormSquared(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector DivideByNorm(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByNorm(),
            RGaFloat64Vector mv1 => mv1.DivideByNorm(),
            RGaFloat64Bivector mv1 => mv1.DivideByNorm(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector DivideByNormSquared(this RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByNormSquared(),
            RGaFloat64Vector mv1 => mv1.DivideByNormSquared(),
            RGaFloat64Bivector mv1 => mv1.DivideByNormSquared(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector EInverse(this RGaFloat64KVector kVector)
    {
        return kVector switch
        {
            RGaFloat64Scalar s => s.EInverse(),
            RGaFloat64Vector v => v.EInverse(),
            RGaFloat64Bivector bv => bv.EInverse(),
            RGaFloat64HigherKVector kv => kv.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Inverse(this RGaFloat64KVector kVector)
    {
        return kVector switch
        {
            RGaFloat64Scalar s => s.Inverse(),
            RGaFloat64Vector v => v.Inverse(),
            RGaFloat64Bivector bv => bv.Inverse(),
            RGaFloat64HigherKVector kv => kv.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector PseudoInverse(this RGaFloat64KVector kVector)
    {
        return kVector switch
        {
            RGaFloat64Scalar s => s.PseudoInverse(),
            RGaFloat64Vector v => v.PseudoInverse(),
            RGaFloat64Bivector bv => bv.PseudoInverse(),
            RGaFloat64HigherKVector kv => kv.PseudoInverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector EDual(this RGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalarEInverse(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector EDual(this RGaFloat64KVector kVector, RGaFloat64KVector blade)
    {
        return kVector.ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Dual(this RGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalarInverse(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Dual(this RGaFloat64KVector kVector, RGaFloat64KVector blade)
    {
        return kVector.Lcp(Inverse(blade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector EUnDual(this RGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalar(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector EUnDual(this RGaFloat64KVector kVector, RGaFloat64KVector blade)
    {
        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector UnDual(this RGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalar(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector UnDual(this RGaFloat64KVector kVector, RGaFloat64KVector blade)
    {
        return kVector.Lcp(blade);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Op(this IEnumerable<RGaFloat64Vector> mvList, RGaFloat64Processor processor)
    {
        var blade = (RGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }
        
    public static RGaFloat64KVector Op(this RGaFloat64Processor processor, IEnumerable<RGaFloat64Vector> mvList)
    {
        var blade = (RGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }
        
    public static RGaFloat64KVector SpanToBlade(this IEnumerable<RGaFloat64Vector> mvList, RGaFloat64Processor processor)
    {
        var blade = (RGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsNearZero())
                continue;

            blade = newBlade;
        }

        return blade;
    }

    public static RGaFloat64KVector SpanToBlade(this RGaFloat64Processor processor, IEnumerable<RGaFloat64Vector> mvList)
    {
        var blade = (RGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsNearZero())
                continue;

            blade = newBlade;
        }

        return blade;
    }

}