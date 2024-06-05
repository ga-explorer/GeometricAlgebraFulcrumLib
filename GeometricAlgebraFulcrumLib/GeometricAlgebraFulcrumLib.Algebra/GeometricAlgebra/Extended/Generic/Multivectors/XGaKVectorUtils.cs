using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

public static class XGaKVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearBlade<T>(this XGaKVector<T> kv)
    {
        return kv
            .Gp(kv.Reverse())
            .GetKVectorParts()
            .All(kv1 => 
                kv1.Grade == 0 || kv1.IsNearZero()
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> GetPart<T>(this XGaKVector<T> mv, Func<IIndexSet, bool> filterFunc)
    {
        return mv switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> GetPart<T>(this XGaKVector<T> mv, Func<T, bool> filterFunc)
    {
        return mv switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> GetPart<T>(this XGaKVector<T> mv, Func<IIndexSet, T, bool> filterFunc)
    {
        return mv switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> MapScalars<T>(this XGaKVector<T> mv, Func<T, T> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(scalarMapping),
            XGaVector<T> v => v.MapScalars(scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T2> MapScalars<T1, T2>(this XGaKVector<T1> mv, XGaProcessor<T2> processor, Func<T1, T2> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T1> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector MapScalars<T>(this XGaKVector<T> mv, XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> MapScalars<T>(this XGaKVector<T> mv, Func<IIndexSet, T, T> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(scalarMapping),
            XGaVector<T> v => v.MapScalars(scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T2> MapScalars<T1, T2>(this XGaKVector<T1> mv, XGaProcessor<T2> processor, Func<IIndexSet, T1, T2> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T1> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector MapScalars<T>(this XGaKVector<T> mv, XGaFloat64Processor processor, Func<IIndexSet, T, double> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Negative<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Negative(),
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Reverse<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> => mv,
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> GradeInvolution<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1,
            XGaHigherKVector<T> mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> CliffordConjugate<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Conjugate<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> mv1 => mv1.Conjugate(),
            XGaBivector<T> mv1 => mv1.Conjugate(),
            XGaHigherKVector<T> mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Times<T>(this XGaKVector<T> mv, T scalarValue)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
      
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Times<T>(this XGaKVector<T> mv, Scalar<T> scalar)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Times<T>(this XGaKVector<T> mv, IScalar<T> scalar)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Divide<T>(this XGaKVector<T> mv, T scalarValue)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Divide<T>(this XGaKVector<T> mv, Scalar<T> scalar)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Divide<T>(this XGaKVector<T> mv, IScalar<T> scalar)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> DivideByENorm<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENorm(),
            XGaVector<T> mv1 => mv1.DivideByENorm(),
            XGaBivector<T> mv1 => mv1.DivideByENorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> DivideByENormSquared<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENormSquared(),
            XGaVector<T> mv1 => mv1.DivideByENormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByENormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> DivideByNorm<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNorm(),
            XGaVector<T> mv1 => mv1.DivideByNorm(),
            XGaBivector<T> mv1 => mv1.DivideByNorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> DivideByNormSquared<T>(this XGaKVector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNormSquared(),
            XGaVector<T> mv1 => mv1.DivideByNormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByNormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> EInverse<T>(this XGaKVector<T> kVector)
    {
        return kVector switch
        {
            XGaScalar<T> s => s.EInverse(),
            XGaVector<T> v => v.EInverse(),
            XGaBivector<T> bv => bv.EInverse(),
            XGaHigherKVector<T> kv => kv.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Inverse<T>(this XGaKVector<T> kVector)
    {
        return kVector switch
        {
            XGaScalar<T> s => s.Inverse(),
            XGaVector<T> v => v.Inverse(),
            XGaBivector<T> bv => bv.Inverse(),
            XGaHigherKVector<T> kv => kv.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> PseudoInverse<T>(this XGaKVector<T> kVector)
    {
        return kVector switch
        {
            XGaScalar<T> s => s.PseudoInverse(),
            XGaVector<T> v => v.PseudoInverse(),
            XGaBivector<T> bv => bv.PseudoInverse(),
            XGaHigherKVector<T> kv => kv.PseudoInverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> EDual<T>(this XGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalarEInverse(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> EDual<T>(this XGaKVector<T> kVector, XGaKVector<T> blade)
    {
        return kVector.ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Dual<T>(this XGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalarInverse(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Dual<T>(this XGaKVector<T> kVector, XGaKVector<T> blade)
    {
        return kVector.Lcp(Inverse(blade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> EUnDual<T>(this XGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalar(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> EUnDual<T>(this XGaKVector<T> kVector, XGaKVector<T> blade)
    {
        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> UnDual<T>(this XGaKVector<T> kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalar(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> UnDual<T>(this XGaKVector<T> kVector, XGaKVector<T> blade)
    {
        return kVector.Lcp(blade);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaKVector<T> Op<T>(this IEnumerable<XGaVector<T>> mvList)
    //{
    //    return mvList.Skip(1).Aggregate(
    //        (XGaKVector<T>)mvList.First(),
    //        (current, mv) => current.Op(mv)
    //    );
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Op<T>(this IEnumerable<XGaVector<T>> mvList, XGaProcessor<T> processor)
    {
        var blade = (XGaKVector<T>) processor.ScalarOne;
        
        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Op<T>(this XGaProcessor<T> processor, IEnumerable<XGaVector<T>> mvList)
    {
        var blade = (XGaKVector<T>) processor.ScalarOne;
        
        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }

    public static XGaKVector<T> SpanToBlade<T>(this IEnumerable<XGaVector<T>> mvList, XGaProcessor<T> processor)
    {
        var blade = (XGaKVector<T>) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsNearZero())
                continue;

            blade = newBlade;
        }

        return blade;
    }

    public static XGaKVector<T> SpanToBlade<T>(this XGaProcessor<T> processor, IEnumerable<XGaVector<T>> mvList)
    {
        var blade = (XGaKVector<T>) processor.ScalarOne;

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