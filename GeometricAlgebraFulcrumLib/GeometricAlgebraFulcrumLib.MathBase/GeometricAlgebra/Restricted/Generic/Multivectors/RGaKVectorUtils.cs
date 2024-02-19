using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

public static class RGaKVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearBlade<T>(this RGaKVector<T> kv)
    {
        return kv
            .Gp(kv.Reverse())
            .GetKVectorParts()
            .All(kv1 => 
                kv1.Grade == 0 || kv1.IsNearZero()
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> GetPart<T>(this RGaKVector<T> mv, Func<ulong, bool> filterFunc)
    {
        return mv switch
        {
            RGaScalar<T> s => s.GetPart(filterFunc),
            RGaVector<T> v => v.GetPart(filterFunc),
            RGaBivector<T> bv => bv.GetPart(filterFunc),
            RGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> GetPart<T>(this RGaKVector<T> mv, Func<T, bool> filterFunc)
    {
        return mv switch
        {
            RGaScalar<T> s => s.GetPart(filterFunc),
            RGaVector<T> v => v.GetPart(filterFunc),
            RGaBivector<T> bv => bv.GetPart(filterFunc),
            RGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> GetPart<T>(this RGaKVector<T> mv, Func<ulong, T, bool> filterFunc)
    {
        return mv switch
        {
            RGaScalar<T> s => s.GetPart(filterFunc),
            RGaVector<T> v => v.GetPart(filterFunc),
            RGaBivector<T> bv => bv.GetPart(filterFunc),
            RGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> MapScalars<T>(this RGaKVector<T> mv, Func<T, T> scalarMapping)
    {
        return mv switch
        {
            RGaScalar<T> s => s.MapScalar(scalarMapping),
            RGaVector<T> v => v.MapScalars(scalarMapping),
            RGaBivector<T> bv => bv.MapScalars(scalarMapping),
            RGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T2> MapScalars<T1, T2>(this RGaKVector<T1> mv, RGaProcessor<T2> processor, Func<T1, T2> scalarMapping)
    {
        return mv switch
        {
            RGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
            RGaVector<T1> v => v.MapScalars(processor, scalarMapping),
            RGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
            RGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector MapScalars<T>(this RGaKVector<T> mv, RGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        return mv switch
        {
            RGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            RGaVector<T> v => v.MapScalars(processor, scalarMapping),
            RGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            RGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> MapScalars<T>(this RGaKVector<T> mv, Func<ulong, T, T> scalarMapping)
    {
        return mv switch
        {
            RGaScalar<T> s => s.MapScalar(scalarMapping),
            RGaVector<T> v => v.MapScalars(scalarMapping),
            RGaBivector<T> bv => bv.MapScalars(scalarMapping),
            RGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T2> MapScalars<T1, T2>(this RGaKVector<T1> mv, RGaProcessor<T2> processor, Func<ulong, T1, T2> scalarMapping)
    {
        return mv switch
        {
            RGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
            RGaVector<T1> v => v.MapScalars(processor, scalarMapping),
            RGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
            RGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector MapScalars<T>(this RGaKVector<T> mv, RGaFloat64Processor processor, Func<ulong, T, double> scalarMapping)
    {
        return mv switch
        {
            RGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            RGaVector<T> v => v.MapScalars(processor, scalarMapping),
            RGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            RGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Negative<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => mv1.Negative(),
            RGaVector<T> mv1 => mv1.Negative(),
            RGaBivector<T> mv1 => mv1.Negative(),
            RGaHigherKVector<T> mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Reverse<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> => mv,
            RGaVector<T> => mv,
            RGaBivector<T> mv1 => mv1.Negative(),
            RGaHigherKVector<T> mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> GradeInvolution<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> => mv,
            RGaVector<T> mv1 => mv1.Negative(),
            RGaBivector<T> mv1 => mv1,
            RGaHigherKVector<T> mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> CliffordConjugate<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> => mv,
            RGaVector<T> mv1 => mv1.Negative(),
            RGaBivector<T> mv1 => mv1.Negative(),
            RGaHigherKVector<T> mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Conjugate<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> => mv,
            RGaVector<T> mv1 => mv1.Conjugate(),
            RGaBivector<T> mv1 => mv1.Conjugate(),
            RGaHigherKVector<T> mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Times<T>(this RGaKVector<T> mv, T scalarValue)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => mv1.Times(scalarValue),
            RGaVector<T> mv1 => mv1.Times(scalarValue),
            RGaBivector<T> mv1 => mv1.Times(scalarValue),
            RGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Divide<T>(this RGaKVector<T> mv, T scalarValue)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => mv1.Divide(scalarValue),
            RGaVector<T> mv1 => mv1.Divide(scalarValue),
            RGaBivector<T> mv1 => mv1.Divide(scalarValue),
            RGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> DivideByENorm<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => mv1.DivideByENorm(),
            RGaVector<T> mv1 => mv1.DivideByENorm(),
            RGaBivector<T> mv1 => mv1.DivideByENorm(),
            RGaHigherKVector<T> mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> DivideByENormSquared<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => mv1.DivideByENormSquared(),
            RGaVector<T> mv1 => mv1.DivideByENormSquared(),
            RGaBivector<T> mv1 => mv1.DivideByENormSquared(),
            RGaHigherKVector<T> mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> DivideByNorm<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => mv1.DivideByNorm(),
            RGaVector<T> mv1 => mv1.DivideByNorm(),
            RGaBivector<T> mv1 => mv1.DivideByNorm(),
            RGaHigherKVector<T> mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> DivideByNormSquared<T>(this RGaKVector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => mv1.DivideByNormSquared(),
            RGaVector<T> mv1 => mv1.DivideByNormSquared(),
            RGaBivector<T> mv1 => mv1.DivideByNormSquared(),
            RGaHigherKVector<T> mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> EInverse<T>(this RGaKVector<T> kVector)
    {
        return kVector switch
        {
            RGaScalar<T> s => s.EInverse(),
            RGaVector<T> v => v.EInverse(),
            RGaBivector<T> bv => bv.EInverse(),
            RGaHigherKVector<T> kv => kv.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Inverse<T>(this RGaKVector<T> kVector)
    {
        return kVector switch
        {
            RGaScalar<T> s => s.Inverse(),
            RGaVector<T> v => v.Inverse(),
            RGaBivector<T> bv => bv.Inverse(),
            RGaHigherKVector<T> kv => kv.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> PseudoInverse<T>(this RGaKVector<T> kVector)
    {
        return kVector switch
        {
            RGaScalar<T> s => s.PseudoInverse(),
            RGaVector<T> v => v.PseudoInverse(),
            RGaBivector<T> bv => bv.PseudoInverse(),
            RGaHigherKVector<T> kv => kv.PseudoInverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> EDual<T>(this RGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.CreatePseudoScalarEInverse(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> EDual<T>(this RGaKVector<T> kVector, RGaKVector<T> blade)
    {
        return kVector.ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Dual<T>(this RGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.CreatePseudoScalarInverse(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Dual<T>(this RGaKVector<T> kVector, RGaKVector<T> blade)
    {
        return kVector.Lcp(Inverse(blade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> EUnDual<T>(this RGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.CreatePseudoScalar(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> EUnDual<T>(this RGaKVector<T> kVector, RGaKVector<T> blade)
    {
        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> UnDual<T>(this RGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.CreatePseudoScalar(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> UnDual<T>(this RGaKVector<T> kVector, RGaKVector<T> blade)
    {
        return kVector.Lcp(blade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Op<T>(this IEnumerable<RGaVector<T>> mvList)
    {
        return mvList.Skip(1).Aggregate(
            (RGaKVector<T>)mvList.First(),
            (current, mv) => current.Op(mv)
        );
    }
        
    public static RGaKVector<T> SpanToBlade<T>(this RGaProcessor<T> processor, IEnumerable<RGaVector<T>> mvList)
    {
        var blade = (RGaKVector<T>) processor.CreateOneScalar();

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