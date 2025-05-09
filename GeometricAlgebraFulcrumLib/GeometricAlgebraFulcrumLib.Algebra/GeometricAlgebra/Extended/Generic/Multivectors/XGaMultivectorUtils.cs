using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

public static class XGaMultivectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaRandomComposer<T> CreateXGaRandomComposer<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        return new XGaRandomComposer<T>(processor, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaRandomComposer<T> CreateXGaRandomComposer<T>(this XGaProcessor<T> processor, int vSpaceDimensions, int seed)
    {
        return new XGaRandomComposer<T>(processor, vSpaceDimensions, seed);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetVSpaceDimensions<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Max(mv => mv.VSpaceDimensions);
    }

    public static T[] KVectorToArray<T>(this XGaKVector<T> kVector, int vSpaceDimensions)
    {
        if (vSpaceDimensions < kVector.VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var kvSpaceDimensions =
            (int)vSpaceDimensions.GetBinomialCoefficient(kVector.Grade);

        var array = kVector.ScalarProcessor.CreateArrayZero1D(kvSpaceDimensions);

        foreach (var (index, scalar) in kVector.GetKVectorArrayItems())
            array[index] = scalar;

        return array;
    }

    public static T[] MultivectorToArray<T>(this XGaMultivector<T> kVector, int vSpaceDimensions)
    {
        if (vSpaceDimensions > 31 || vSpaceDimensions < kVector.VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var gaSpaceDimensions =
            1 << vSpaceDimensions;

        var array = kVector.ScalarProcessor.CreateArrayZero1D(gaSpaceDimensions);

        foreach (var (index, scalar) in kVector.GetMultivectorArrayItems())
            array[index] = scalar;

        return array;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> GetPart<T>(this XGaMultivector<T> mv, Func<IndexSet, bool> filterFunc)
    {
        return mv switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            XGaGradedMultivector<T> mv1 => mv1.GetPart(filterFunc),
            XGaUniformMultivector<T> mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> GetPart<T>(this XGaMultivector<T> mv, Func<T, bool> filterFunc)
    {
        return mv switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            XGaGradedMultivector<T> mv1 => mv1.GetPart(filterFunc),
            XGaUniformMultivector<T> mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> GetPart<T>(this XGaMultivector<T> mv, Func<IndexSet, T, bool> filterFunc)
    {
        return mv switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            XGaGradedMultivector<T> mv1 => mv1.GetPart(filterFunc),
            XGaUniformMultivector<T> mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<XGaScalar<T>, XGaBivector<T>> GetScalarBivectorParts<T>(this XGaMultivector<T> mv)
    {
        return new Tuple<XGaScalar<T>, XGaBivector<T>>(
            mv.GetScalarPart(),
            mv.GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<XGaMultivector<T>, XGaMultivector<T>> GetEvenOddParts<T>(this XGaMultivector<T> mv)
    {
        return new Tuple<XGaMultivector<T>, XGaMultivector<T>>(
            mv.GetEvenPart(),
            mv.GetOddPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<XGaMultivector<T>, XGaMultivector<T>> GetEvenOddParts<T>(this XGaMultivector<T> mv, int maxGrade)
    {
        return new Tuple<XGaMultivector<T>, XGaMultivector<T>>(
            mv.GetEvenPart(maxGrade),
            mv.GetOddPart(maxGrade)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapScalars<T>(this XGaMultivector<T> mv, Func<T, T> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(scalarMapping),
            XGaVector<T> v => v.MapScalars(scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            XGaGradedMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
            XGaUniformMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector MapScalars<T>(this XGaMultivector<T> mv, XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            XGaGradedMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
            XGaUniformMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T2> MapScalars<T1, T2>(this XGaMultivector<T1> mv, XGaProcessor<T2> processor, Func<T1, T2> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T1> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
            XGaGradedMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
            XGaUniformMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapScalars<T>(this XGaMultivector<T> mv, Func<IndexSet, T, T> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(scalarMapping),
            XGaVector<T> v => v.MapScalars(scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
            XGaGradedMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
            XGaUniformMultivector<T> mv1 => mv1.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector MapScalars<T>(this XGaMultivector<T> mv, XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
            XGaGradedMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
            XGaUniformMultivector<T> mv1 => mv1.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T2> MapScalars<T1, T2>(this XGaMultivector<T1> mv, XGaProcessor<T2> processor, Func<IndexSet, T1, T2> scalarMapping)
    {
        return mv switch
        {
            XGaScalar<T1> s => s.MapScalar(processor, scalarMapping),
            XGaVector<T1> v => v.MapScalars(processor, scalarMapping),
            XGaBivector<T1> bv => bv.MapScalars(processor, scalarMapping),
            XGaHigherKVector<T1> kv => kv.MapScalars(processor, scalarMapping),
            XGaGradedMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
            XGaUniformMultivector<T1> mv1 => mv1.MapScalars(processor, scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapBasisBlades<T>(this XGaMultivector<T> mv, Func<IndexSet, IndexSet> basisMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    basisMapping(term.Key),
                    term.Value
                )
            );

        return mv.Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapBasisBlades<T>(this XGaMultivector<T> mv, Func<IndexSet, T, IndexSet> basisMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    basisMapping(term.Key, term.Value),
                    term.Value
                )
            );

        return mv.Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapTerms<T>(this XGaMultivector<T> mv, Func<IndexSet, T, KeyValuePair<IndexSet, T>> termMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term =>
                    termMapping(term.Key, term.Value)
            ).Where(p => !mv.ScalarProcessor.IsZero(p.Value));

        return mv.Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Negative<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Negative(),
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Negative(),
            XGaGradedMultivector<T> mv1 => mv1.Negative(),
            XGaUniformMultivector<T> mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Reverse<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> => mv,
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            XGaGradedMultivector<T> mv1 => mv1.Reverse(),
            XGaUniformMultivector<T> mv1 => mv1.Reverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> GradeInvolution<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1,
            XGaHigherKVector<T> mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            XGaGradedMultivector<T> mv1 => mv1.GradeInvolution(),
            XGaUniformMultivector<T> mv1 => mv1.GradeInvolution(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> CliffordConjugate<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            XGaGradedMultivector<T> mv1 => mv1.CliffordConjugate(),
            XGaUniformMultivector<T> mv1 => mv1.CliffordConjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Conjugate<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> => mv,
            XGaVector<T> mv1 => mv1.Conjugate(),
            XGaBivector<T> mv1 => mv1.Conjugate(),
            XGaHigherKVector<T> mv1 => mv1.Conjugate(),
            XGaGradedMultivector<T> mv1 => mv1.Conjugate(),
            XGaUniformMultivector<T> mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Times<T>(this XGaMultivector<T> mv, T scalarValue)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            XGaGradedMultivector<T> mv1 => mv1.Times(scalarValue),
            XGaUniformMultivector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Divide<T>(this XGaMultivector<T> mv, T scalarValue)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            XGaGradedMultivector<T> mv1 => mv1.Divide(scalarValue),
            XGaUniformMultivector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> DivideByENorm<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENorm(),
            XGaVector<T> mv1 => mv1.DivideByENorm(),
            XGaBivector<T> mv1 => mv1.DivideByENorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENorm(),
            XGaGradedMultivector<T> mv1 => mv1.DivideByENorm(),
            XGaUniformMultivector<T> mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> DivideByENormSquared<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENormSquared(),
            XGaVector<T> mv1 => mv1.DivideByENormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByENormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENormSquared(),
            XGaGradedMultivector<T> mv1 => mv1.DivideByENormSquared(),
            XGaUniformMultivector<T> mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> DivideByNorm<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNorm(),
            XGaVector<T> mv1 => mv1.DivideByNorm(),
            XGaBivector<T> mv1 => mv1.DivideByNorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNorm(),
            XGaGradedMultivector<T> mv1 => mv1.DivideByNorm(),
            XGaUniformMultivector<T> mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> DivideByNormSquared<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNormSquared(),
            XGaVector<T> mv1 => mv1.DivideByNormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByNormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNormSquared(),
            XGaGradedMultivector<T> mv1 => mv1.DivideByNormSquared(),
            XGaUniformMultivector<T> mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> EInverse<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.EInverse(),
            XGaVector<T> mv1 => mv1.EInverse(),
            XGaBivector<T> mv1 => mv1.EInverse(),
            XGaHigherKVector<T> mv1 => mv1.EInverse(),
            XGaGradedMultivector<T> mv1 => mv1.EInverse(),
            XGaUniformMultivector<T> mv1 => mv1.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Inverse<T>(this XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Inverse(),
            XGaVector<T> mv1 => mv1.Inverse(),
            XGaBivector<T> mv1 => mv1.Inverse(),
            XGaHigherKVector<T> mv1 => mv1.Inverse(),
            XGaGradedMultivector<T> mv1 => mv1.Inverse(),
            XGaUniformMultivector<T> mv1 => mv1.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> EDual<T>(this XGaMultivector<T> mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.EDual(vSpaceDimensions),
            XGaVector<T> mv1 => mv1.EDual(vSpaceDimensions),
            XGaBivector<T> mv1 => mv1.EDual(vSpaceDimensions),
            XGaHigherKVector<T> mv1 => mv1.EDual(vSpaceDimensions),
            XGaGradedMultivector<T> mv1 => mv1.EDual(vSpaceDimensions),
            XGaUniformMultivector<T> mv1 => mv1.EDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> EDual<T>(this XGaMultivector<T> mv, XGaKVector<T> blade)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.EDual(blade),
            XGaVector<T> mv1 => mv1.EDual(blade),
            XGaBivector<T> mv1 => mv1.EDual(blade),
            XGaHigherKVector<T> mv1 => mv1.EDual(blade),
            XGaGradedMultivector<T> mv1 => mv1.EDual(blade),
            XGaUniformMultivector<T> mv1 => mv1.EDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Dual<T>(this XGaMultivector<T> mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Dual(vSpaceDimensions),
            XGaVector<T> mv1 => mv1.Dual(vSpaceDimensions),
            XGaBivector<T> mv1 => mv1.Dual(vSpaceDimensions),
            XGaHigherKVector<T> mv1 => mv1.Dual(vSpaceDimensions),
            XGaGradedMultivector<T> mv1 => mv1.Dual(vSpaceDimensions),
            XGaUniformMultivector<T> mv1 => mv1.Dual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Dual<T>(this XGaMultivector<T> mv, XGaKVector<T> blade)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.Dual(blade),
            XGaVector<T> mv1 => mv1.Dual(blade),
            XGaBivector<T> mv1 => mv1.Dual(blade),
            XGaHigherKVector<T> mv1 => mv1.Dual(blade),
            XGaGradedMultivector<T> mv1 => mv1.Dual(blade),
            XGaUniformMultivector<T> mv1 => mv1.Dual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> EUnDual<T>(this XGaMultivector<T> mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaVector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaBivector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaHigherKVector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaGradedMultivector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaUniformMultivector<T> mv1 => mv1.EUnDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> EUnDual<T>(this XGaMultivector<T> mv, XGaKVector<T> blade)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.EUnDual(blade),
            XGaVector<T> mv1 => mv1.EUnDual(blade),
            XGaBivector<T> mv1 => mv1.EUnDual(blade),
            XGaHigherKVector<T> mv1 => mv1.EUnDual(blade),
            XGaGradedMultivector<T> mv1 => mv1.EUnDual(blade),
            XGaUniformMultivector<T> mv1 => mv1.EUnDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> UnDual<T>(this XGaMultivector<T> mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.UnDual(vSpaceDimensions),
            XGaVector<T> mv1 => mv1.UnDual(vSpaceDimensions),
            XGaBivector<T> mv1 => mv1.UnDual(vSpaceDimensions),
            XGaHigherKVector<T> mv1 => mv1.UnDual(vSpaceDimensions),
            XGaGradedMultivector<T> mv1 => mv1.UnDual(vSpaceDimensions),
            XGaUniformMultivector<T> mv1 => mv1.UnDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> UnDual<T>(this XGaMultivector<T> mv, XGaKVector<T> blade)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => mv1.UnDual(blade),
            XGaVector<T> mv1 => mv1.UnDual(blade),
            XGaBivector<T> mv1 => mv1.UnDual(blade),
            XGaHigherKVector<T> mv1 => mv1.UnDual(blade),
            XGaGradedMultivector<T> mv1 => mv1.UnDual(blade),
            XGaUniformMultivector<T> mv1 => mv1.UnDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Op<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Op(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> EGp<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.EGp(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Gp<T>(this IEnumerable<XGaMultivector<T>> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Gp(mv)
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetVectorPartAsVector2D(this XGaMultivector<double> mv)
    {
        return LinFloat64Vector2D.Create(
            (Float64Scalar)mv.Scalar(0).ScalarValue,
            (Float64Scalar)mv.Scalar(1).ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetVectorPartAsVector3D(this XGaMultivector<double> mv)
    {
        return LinFloat64Vector3D.Create(
            mv.Scalar(0).ScalarValue,
            mv.Scalar(1).ScalarValue,
            mv.Scalar(2).ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D GetVectorPartAsVector4D(this XGaMultivector<double> mv)
    {
        return LinFloat64Vector4D.Create(
            mv.Scalar(0).ScalarValue,
            mv.Scalar(1).ScalarValue,
            mv.Scalar(2).ScalarValue,
            mv.Scalar(3).ScalarValue
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] MultivectorToArray1D<T>(this XGaMultivector<T> mv, int arraySize)
    {
        var vSpaceDimensions = mv.VSpaceDimensions;

        if (vSpaceDimensions > 31)
            throw new InvalidOperationException();

        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        if ((ulong) arraySize < gaSpaceDimensions)
            throw new InvalidOperationException();

        var array = mv
            .ScalarProcessor
            .CreateArrayZero1D(arraySize);

        foreach (var (id, scalar) in mv.IdScalarPairs)
            array[id.ToInt32()] = scalar;

        return array;
    }

    public static T[,] ScalarPlusBivectorToArray2D<T>(this XGaMultivector<T> mv)
    {
        var array = mv.GetBivectorPart().BivectorToArray2D();
        var scalar = mv.Scalar().ScalarValue;
        var metric = mv.Metric;
        var scalarProcessor = mv.ScalarProcessor;

        var arraySize = array.GetLength(0);
        for (var i = 0; i < arraySize; i++)
        {
            var signature = metric.Signature(i);

            if (signature.IsZero) continue;

            array[i, i] = signature.IsPositive
                ? scalar
                : scalarProcessor.Negative(scalar).ScalarValue;
        }
        
        return array;
    }

    public static T[,] ScalarPlusBivectorToArray2D<T>(this XGaMultivector<T> mv, int arraySize)
    {
        var array = mv.GetBivectorPart().BivectorToArray2D(arraySize);
        var scalar = mv.Scalar().ScalarValue;
        var metric = mv.Metric;
        var scalarProcessor = mv.ScalarProcessor;

        for (var i = 0; i < arraySize; i++)
        {
            var signature = metric.Signature(i);

            if (signature.IsZero) continue;

            array[i, i] = signature.IsPositive
                ? scalar
                : scalarProcessor.Negative(scalar).ScalarValue;
        }
        
        return array;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T>[,] GetMapTable<T>(this IReadOnlyList<XGaMultivector<T>> multivectorList, Func<XGaMultivector<T>, XGaMultivector<T>, XGaMultivector<T>> multivectorMap)
    {
        return GetMapTable(
            multivectorList,
            multivectorList,
            multivectorMap
        );
    }

    public static XGaMultivector<T>[,] GetMapTable<T>(this IReadOnlyList<XGaMultivector<T>> multivectorList1, IReadOnlyList<XGaMultivector<T>> multivectorList2, Func<XGaMultivector<T>, XGaMultivector<T>, XGaMultivector<T>> multivectorMap)
    {
        var rowCount = multivectorList1.Count;
        var colCount = multivectorList2.Count;

        var tableArray = new XGaMultivector<T>[rowCount, colCount];

        for (var i = 0; i < rowCount; i++)
        {
            var b1 = multivectorList1[i];

            for (var j = 0; j < colCount; j++)
            {
                var b2 = multivectorList2[j];

                tableArray[i, j] = multivectorMap(b1, b2);
            }
        }

        return tableArray;
    }
}