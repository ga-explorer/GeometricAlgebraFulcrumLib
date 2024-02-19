using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

public static class XGaFloat64MultivectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64RandomComposer CreateXGaRandomComposer(this XGaFloat64Processor processor, int vSpaceDimensions)
    {
        return new XGaFloat64RandomComposer(processor, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64RandomComposer CreateXGaRandomComposer(this XGaFloat64Processor processor, int vSpaceDimensions, int seed)
    {
        return new XGaFloat64RandomComposer(processor, vSpaceDimensions, seed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64RandomComposer CreateXGaRandomComposer(this XGaFloat64Processor processor, int vSpaceDimensions, System.Random randomGenerator)
    {
        return new XGaFloat64RandomComposer(processor, vSpaceDimensions, randomGenerator);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetVSpaceDimensions(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Max(mv => mv.VSpaceDimensions);
    }

    public static double[] KVectorToArray(this XGaFloat64KVector kVector, int vSpaceDimensions)
    {
        if (vSpaceDimensions < kVector.VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var kvSpaceDimensions =
            (int)vSpaceDimensions.GetBinomialCoefficient(kVector.Grade);

        var array = new double[kvSpaceDimensions];

        foreach (var (index, scalar) in kVector.GetKVectorArrayItems())
            array[index] = scalar;

        return array;
    }

    public static double[] MultivectorToArray(this XGaFloat64Multivector kVector, int vSpaceDimensions)
    {
        if (vSpaceDimensions > 31 || vSpaceDimensions < kVector.VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var gaSpaceDimensions =
            1 << vSpaceDimensions;

        var array = new double[gaSpaceDimensions];

        foreach (var (index, scalar) in kVector.GetMultivectorArrayItems())
            array[index] = scalar;

        return array;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector GetPart(this XGaFloat64Multivector mv, Func<IIndexSet, bool> filterFunc)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            XGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
            XGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector GetPart(this XGaFloat64Multivector mv, Func<double, bool> filterFunc)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            XGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
            XGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector GetPart(this XGaFloat64Multivector mv, Func<IIndexSet, double, bool> filterFunc)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            XGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
            XGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector RemoveSmallTerms(this XGaFloat64Multivector mv, double epsilon = 1e-12)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s,
            XGaFloat64Vector v => v.RemoveSmallTerms(epsilon),
            XGaFloat64Bivector bv => bv.RemoveSmallTerms(epsilon),
            XGaFloat64HigherKVector kv => kv.RemoveSmallTerms(epsilon),
            XGaFloat64GradedMultivector mv1 => mv1.RemoveSmallTerms(epsilon),
            XGaFloat64UniformMultivector mv1 => mv1.RemoveSmallTerms(epsilon),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<XGaFloat64Scalar, XGaFloat64Bivector> GetScalarBivectorParts(this XGaFloat64Multivector mv)
    {
        return new Tuple<XGaFloat64Scalar, XGaFloat64Bivector>(
            mv.GetScalarPart(),
            mv.GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<XGaFloat64Multivector, XGaFloat64Multivector> GetEvenOddParts(this XGaFloat64Multivector mv)
    {
        return new Tuple<XGaFloat64Multivector, XGaFloat64Multivector>(
            mv.GetEvenPart(),
            mv.GetOddPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<XGaFloat64Multivector, XGaFloat64Multivector> GetEvenOddParts(this XGaFloat64Multivector mv, int maxGrade)
    {
        return new Tuple<XGaFloat64Multivector, XGaFloat64Multivector>(
            mv.GetEvenPart(maxGrade),
            mv.GetOddPart(maxGrade)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector MapScalars(this XGaFloat64Multivector mv, Func<double, double> scalarMapping)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.MapScalar(scalarMapping),
            XGaFloat64Vector v => v.MapScalars(scalarMapping),
            XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            XGaFloat64GradedMultivector mv1 => mv1.MapScalars(scalarMapping),

            _ => mv.Processor
                .CreateComposer()
                .AddTerms(
                    mv.IdScalarPairs.Select(
                        term => new KeyValuePair<IIndexSet, double>(
                            term.Key,
                            scalarMapping(term.Value)
                        )
                    )
                ).GetSimpleMultivector()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector MapScalars(this XGaFloat64Multivector mv, Func<IIndexSet, double, double> scalarMapping)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.MapScalar(scalarMapping),
            XGaFloat64Vector v => v.MapScalars(scalarMapping),
            XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            XGaFloat64GradedMultivector mv1 => mv1.MapScalars(scalarMapping),

            _ => mv.Processor
                .CreateComposer()
                .AddTerms(
                    mv.IdScalarPairs.Select(
                        term => new KeyValuePair<IIndexSet, double>(
                            term.Key,
                            scalarMapping(term.Key, term.Value)
                        )
                    )
                ).GetSimpleMultivector()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector MapBasisBlades(this XGaFloat64Multivector mv, Func<IIndexSet, IIndexSet> basisMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
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
    public static XGaFloat64Multivector MapBasisBlades(this XGaFloat64Multivector mv, Func<IIndexSet, double, IIndexSet> basisMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
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
    public static XGaFloat64Multivector MapTerms(this XGaFloat64Multivector mv, Func<IIndexSet, double, KeyValuePair<IIndexSet, double>> termMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term =>
                    termMapping(term.Key, term.Value)
            );

        return mv.Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Negative(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Negative(),
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Negative(),
            XGaFloat64GradedMultivector mv1 => mv1.Negative(),
            XGaFloat64UniformMultivector mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Reverse(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector => mv,
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            XGaFloat64GradedMultivector mv1 => mv1.Reverse(),
            XGaFloat64UniformMultivector mv1 => mv1.Reverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector GradeInvolution(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1,
            XGaFloat64HigherKVector mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            XGaFloat64GradedMultivector mv1 => mv1.GradeInvolution(),
            XGaFloat64UniformMultivector mv1 => mv1.GradeInvolution(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector CliffordConjugate(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            XGaFloat64GradedMultivector mv1 => mv1.CliffordConjugate(),
            XGaFloat64UniformMultivector mv1 => mv1.CliffordConjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Conjugate(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector mv1 => mv1.Conjugate(),
            XGaFloat64Bivector mv1 => mv1.Conjugate(),
            XGaFloat64HigherKVector mv1 => mv1.Conjugate(),
            XGaFloat64GradedMultivector mv1 => mv1.Conjugate(),
            XGaFloat64UniformMultivector mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Times(this XGaFloat64Multivector mv, double scalarValue)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Times(scalarValue),
            XGaFloat64Vector mv1 => mv1.Times(scalarValue),
            XGaFloat64Bivector mv1 => mv1.Times(scalarValue),
            XGaFloat64HigherKVector mv1 => mv1.Times(scalarValue),
            XGaFloat64GradedMultivector mv1 => mv1.Times(scalarValue),
            XGaFloat64UniformMultivector mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Divide(this XGaFloat64Multivector mv, double scalarValue)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Divide(scalarValue),
            XGaFloat64Vector mv1 => mv1.Divide(scalarValue),
            XGaFloat64Bivector mv1 => mv1.Divide(scalarValue),
            XGaFloat64HigherKVector mv1 => mv1.Divide(scalarValue),
            XGaFloat64GradedMultivector mv1 => mv1.Divide(scalarValue),
            XGaFloat64UniformMultivector mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector DivideByENorm(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByENorm(),
            XGaFloat64Vector mv1 => mv1.DivideByENorm(),
            XGaFloat64Bivector mv1 => mv1.DivideByENorm(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByENorm(),
            XGaFloat64GradedMultivector mv1 => mv1.DivideByENorm(),
            XGaFloat64UniformMultivector mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector DivideByENormSquared(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByENormSquared(),
            XGaFloat64Vector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64Bivector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64GradedMultivector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64UniformMultivector mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector DivideByNorm(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByNorm(),
            XGaFloat64Vector mv1 => mv1.DivideByNorm(),
            XGaFloat64Bivector mv1 => mv1.DivideByNorm(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByNorm(),
            XGaFloat64GradedMultivector mv1 => mv1.DivideByNorm(),
            XGaFloat64UniformMultivector mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector DivideByNormSquared(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByNormSquared(),
            XGaFloat64Vector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64Bivector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64GradedMultivector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64UniformMultivector mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector EInverse(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.EInverse(),
            XGaFloat64Vector mv1 => mv1.EInverse(),
            XGaFloat64Bivector mv1 => mv1.EInverse(),
            XGaFloat64HigherKVector mv1 => mv1.EInverse(),
            XGaFloat64GradedMultivector mv1 => mv1.EInverse(),
            XGaFloat64UniformMultivector mv1 => mv1.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Inverse(this XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Inverse(),
            XGaFloat64Vector mv1 => mv1.Inverse(),
            XGaFloat64Bivector mv1 => mv1.Inverse(),
            XGaFloat64HigherKVector mv1 => mv1.Inverse(),
            XGaFloat64GradedMultivector mv1 => mv1.Inverse(),
            XGaFloat64UniformMultivector mv1 => mv1.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector EDual(this XGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.EDual(vSpaceDimensions),
            XGaFloat64Vector mv1 => mv1.EDual(vSpaceDimensions),
            XGaFloat64Bivector mv1 => mv1.EDual(vSpaceDimensions),
            XGaFloat64HigherKVector mv1 => mv1.EDual(vSpaceDimensions),
            XGaFloat64GradedMultivector mv1 => mv1.EDual(vSpaceDimensions),
            XGaFloat64UniformMultivector mv1 => mv1.EDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector EDual(this XGaFloat64Multivector mv, XGaFloat64KVector blade)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.EDual(blade),
            XGaFloat64Vector mv1 => mv1.EDual(blade),
            XGaFloat64Bivector mv1 => mv1.EDual(blade),
            XGaFloat64HigherKVector mv1 => mv1.EDual(blade),
            XGaFloat64GradedMultivector mv1 => mv1.EDual(blade),
            XGaFloat64UniformMultivector mv1 => mv1.EDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Dual(this XGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Dual(vSpaceDimensions),
            XGaFloat64Vector mv1 => mv1.Dual(vSpaceDimensions),
            XGaFloat64Bivector mv1 => mv1.Dual(vSpaceDimensions),
            XGaFloat64HigherKVector mv1 => mv1.Dual(vSpaceDimensions),
            XGaFloat64GradedMultivector mv1 => mv1.Dual(vSpaceDimensions),
            XGaFloat64UniformMultivector mv1 => mv1.Dual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Dual(this XGaFloat64Multivector mv, XGaFloat64KVector blade)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Dual(blade),
            XGaFloat64Vector mv1 => mv1.Dual(blade),
            XGaFloat64Bivector mv1 => mv1.Dual(blade),
            XGaFloat64HigherKVector mv1 => mv1.Dual(blade),
            XGaFloat64GradedMultivector mv1 => mv1.Dual(blade),
            XGaFloat64UniformMultivector mv1 => mv1.Dual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector EUnDual(this XGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaFloat64Vector mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaFloat64Bivector mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaFloat64HigherKVector mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaFloat64GradedMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
            XGaFloat64UniformMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector EUnDual(this XGaFloat64Multivector mv, XGaFloat64KVector blade)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.EUnDual(blade),
            XGaFloat64Vector mv1 => mv1.EUnDual(blade),
            XGaFloat64Bivector mv1 => mv1.EUnDual(blade),
            XGaFloat64HigherKVector mv1 => mv1.EUnDual(blade),
            XGaFloat64GradedMultivector mv1 => mv1.EUnDual(blade),
            XGaFloat64UniformMultivector mv1 => mv1.EUnDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector UnDual(this XGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.UnDual(vSpaceDimensions),
            XGaFloat64Vector mv1 => mv1.UnDual(vSpaceDimensions),
            XGaFloat64Bivector mv1 => mv1.UnDual(vSpaceDimensions),
            XGaFloat64HigherKVector mv1 => mv1.UnDual(vSpaceDimensions),
            XGaFloat64GradedMultivector mv1 => mv1.UnDual(vSpaceDimensions),
            XGaFloat64UniformMultivector mv1 => mv1.UnDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector UnDual(this XGaFloat64Multivector mv, XGaFloat64KVector blade)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.UnDual(blade),
            XGaFloat64Vector mv1 => mv1.UnDual(blade),
            XGaFloat64Bivector mv1 => mv1.UnDual(blade),
            XGaFloat64HigherKVector mv1 => mv1.UnDual(blade),
            XGaFloat64GradedMultivector mv1 => mv1.UnDual(blade),
            XGaFloat64UniformMultivector mv1 => mv1.UnDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Op(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Op(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector EGp(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.EGp(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Gp(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Gp(mv)
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D VectorPartToVector2D(this XGaFloat64Multivector mv)
    {
        return Float64Vector2D.Create(mv[0], mv[1]);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D VectorPartToVector3D(this XGaFloat64Multivector mv)
    {
        return Float64Vector3D.Create(mv[0], mv[1], mv[2]);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector4D VectorPartToVector4D(this XGaFloat64Multivector mv)
    {
        return Float64Vector4D.Create(mv[0], mv[1], mv[2], mv[3]);
    }

        
    public static double[,] ScalarPlusBivectorToArray2D(this XGaFloat64Multivector mv)
    {
        var array = mv.GetBivectorPart().BivectorToArray2D();
        var scalar = mv.Scalar();
        var metric = mv.Metric;

        var arraySize = array.GetLength(0);
        for (var i = 0; i < arraySize; i++)
        {
            var signature = metric.Signature(i);

            if (signature.IsZero) continue;

            array[i, i] = signature.IsPositive
                ? scalar
                : -scalar;
        }
        
        return array;
    }

    public static double[,] ScalarPlusBivectorToArray2D(this XGaFloat64Multivector mv, int arraySize)
    {
        var array = mv.GetBivectorPart().BivectorToArray2D(arraySize);
        var scalar = mv.Scalar();
        var metric = mv.Metric;
            
        for (var i = 0; i < arraySize; i++)
        {
            var signature = metric.Signature(i);

            if (signature.IsZero) continue;

            array[i, i] = signature.IsPositive
                ? scalar
                : -scalar;
        }
        
        return array;
    }
}