using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

public static class RGaFloat64MultivectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64RandomComposer CreateRGaRandomComposer(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        return new RGaFloat64RandomComposer(metric, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64RandomComposer CreateRGaRandomComposer(this RGaFloat64Processor metric, int vSpaceDimensions, int seed)
    {
        return new RGaFloat64RandomComposer(metric, vSpaceDimensions, seed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64RandomComposer CreateRGaRandomComposer(this RGaFloat64Processor metric, int vSpaceDimensions, Random randomGenerator)
    {
        return new RGaFloat64RandomComposer(metric, vSpaceDimensions, randomGenerator);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetVSpaceDimensions(this IEnumerable<RGaFloat64Multivector> mvList)
    {
        return mvList.Max(mv => mv.VSpaceDimensions);
    }

    public static double[] KVectorToArray(this RGaFloat64KVector kVector, int vSpaceDimensions)
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

    public static double[] MultivectorToArray(this RGaFloat64Multivector kVector, int vSpaceDimensions)
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
    public static RGaFloat64Multivector GetPart(this RGaFloat64Multivector mv, Func<ulong, bool> filterFunc)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.GetPart(filterFunc),
            RGaFloat64Vector v => v.GetPart(filterFunc),
            RGaFloat64Bivector bv => bv.GetPart(filterFunc),
            RGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            RGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
            RGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector GetPart(this RGaFloat64Multivector mv, Func<double, bool> filterFunc)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.GetPart(filterFunc),
            RGaFloat64Vector v => v.GetPart(filterFunc),
            RGaFloat64Bivector bv => bv.GetPart(filterFunc),
            RGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            RGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
            RGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector GetPart(this RGaFloat64Multivector mv, Func<ulong, double, bool> filterFunc)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.GetPart(filterFunc),
            RGaFloat64Vector v => v.GetPart(filterFunc),
            RGaFloat64Bivector bv => bv.GetPart(filterFunc),
            RGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            RGaFloat64GradedMultivector mv1 => mv1.GetPart(filterFunc),
            RGaFloat64UniformMultivector mv1 => mv1.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector RemoveSmallTerms(this RGaFloat64Multivector mv, double epsilon = 1e-12)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s,
            RGaFloat64Vector v => v.RemoveSmallTerms(epsilon),
            RGaFloat64Bivector bv => bv.RemoveSmallTerms(epsilon),
            RGaFloat64HigherKVector kv => kv.RemoveSmallTerms(epsilon),
            RGaFloat64GradedMultivector mv1 => mv1.RemoveSmallTerms(epsilon),
            RGaFloat64UniformMultivector mv1 => mv1.RemoveSmallTerms(epsilon),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<RGaFloat64Scalar, RGaFloat64Bivector> GetScalarBivectorParts(this RGaFloat64Multivector mv)
    {
        return new Tuple<RGaFloat64Scalar, RGaFloat64Bivector>(
            mv.GetScalarPart(),
            mv.GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<RGaFloat64Multivector, RGaFloat64Multivector> GetEvenOddParts(this RGaFloat64Multivector mv)
    {
        return new Tuple<RGaFloat64Multivector, RGaFloat64Multivector>(
            mv.GetEvenPart(),
            mv.GetOddPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<RGaFloat64Multivector, RGaFloat64Multivector> GetEvenOddParts(this RGaFloat64Multivector mv, int maxGrade)
    {
        return new Tuple<RGaFloat64Multivector, RGaFloat64Multivector>(
            mv.GetEvenPart(maxGrade),
            mv.GetOddPart(maxGrade)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector MapScalars(this RGaFloat64Multivector mv, Func<double, double> scalarMapping)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.MapScalar(scalarMapping),
            RGaFloat64Vector v => v.MapScalars(scalarMapping),
            RGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            RGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            RGaFloat64GradedMultivector mv1 => mv1.MapScalars(scalarMapping),

            _ => mv.Processor
                .CreateComposer()
                .AddTerms(
                    mv.IdScalarPairs.Select(
                        term => new KeyValuePair<ulong, double>(
                            term.Key,
                            scalarMapping(term.Value)
                        )
                    )
                ).GetSimpleMultivector()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector MapScalars(this RGaFloat64Multivector mv, Func<ulong, double, double> scalarMapping)
    {
        return mv switch
        {
            RGaFloat64Scalar s => s.MapScalar(scalarMapping),
            RGaFloat64Vector v => v.MapScalars(scalarMapping),
            RGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            RGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            RGaFloat64GradedMultivector mv1 => mv1.MapScalars(scalarMapping),

            _ => mv.Processor
                .CreateComposer()
                .AddTerms(
                    mv.IdScalarPairs.Select(
                        term => new KeyValuePair<ulong, double>(
                            term.Key,
                            scalarMapping(term.Key, term.Value)
                        )
                    )
                ).GetSimpleMultivector()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector MapBasisBlades(this RGaFloat64Multivector mv, Func<ulong, ulong> basisMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public static RGaFloat64Multivector MapBasisBlades(this RGaFloat64Multivector mv, Func<ulong, double, ulong> basisMapping)
    {
        var termList =
            mv.IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public static RGaFloat64Multivector MapTerms(this RGaFloat64Multivector mv, Func<ulong, double, KeyValuePair<ulong, double>> termMapping)
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
    public static RGaFloat64Multivector Negative(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Negative(),
            RGaFloat64Vector mv1 => mv1.Negative(),
            RGaFloat64Bivector mv1 => mv1.Negative(),
            RGaFloat64HigherKVector mv1 => mv1.Negative(),
            RGaFloat64GradedMultivector mv1 => mv1.Negative(),
            RGaFloat64UniformMultivector mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Reverse(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector => mv,
            RGaFloat64Bivector mv1 => mv1.Negative(),
            RGaFloat64HigherKVector mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            RGaFloat64GradedMultivector mv1 => mv1.Reverse(),
            RGaFloat64UniformMultivector mv1 => mv1.Reverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector GradeInvolution(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector mv1 => mv1.Negative(),
            RGaFloat64Bivector mv1 => mv1,
            RGaFloat64HigherKVector mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            RGaFloat64GradedMultivector mv1 => mv1.GradeInvolution(),
            RGaFloat64UniformMultivector mv1 => mv1.GradeInvolution(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector CliffordConjugate(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector mv1 => mv1.Negative(),
            RGaFloat64Bivector mv1 => mv1.Negative(),
            RGaFloat64HigherKVector mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            RGaFloat64GradedMultivector mv1 => mv1.CliffordConjugate(),
            RGaFloat64UniformMultivector mv1 => mv1.CliffordConjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Conjugate(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar => mv,
            RGaFloat64Vector mv1 => mv1.Conjugate(),
            RGaFloat64Bivector mv1 => mv1.Conjugate(),
            RGaFloat64HigherKVector mv1 => mv1.Conjugate(),
            RGaFloat64GradedMultivector mv1 => mv1.Conjugate(),
            RGaFloat64UniformMultivector mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Times(this RGaFloat64Multivector mv, double scalarValue)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Times(scalarValue),
            RGaFloat64Vector mv1 => mv1.Times(scalarValue),
            RGaFloat64Bivector mv1 => mv1.Times(scalarValue),
            RGaFloat64HigherKVector mv1 => mv1.Times(scalarValue),
            RGaFloat64GradedMultivector mv1 => mv1.Times(scalarValue),
            RGaFloat64UniformMultivector mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Divide(this RGaFloat64Multivector mv, double scalarValue)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Divide(scalarValue),
            RGaFloat64Vector mv1 => mv1.Divide(scalarValue),
            RGaFloat64Bivector mv1 => mv1.Divide(scalarValue),
            RGaFloat64HigherKVector mv1 => mv1.Divide(scalarValue),
            RGaFloat64GradedMultivector mv1 => mv1.Divide(scalarValue),
            RGaFloat64UniformMultivector mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector DivideByENorm(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByENorm(),
            RGaFloat64Vector mv1 => mv1.DivideByENorm(),
            RGaFloat64Bivector mv1 => mv1.DivideByENorm(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByENorm(),
            RGaFloat64GradedMultivector mv1 => mv1.DivideByENorm(),
            RGaFloat64UniformMultivector mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector DivideByENormSquared(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByENormSquared(),
            RGaFloat64Vector mv1 => mv1.DivideByENormSquared(),
            RGaFloat64Bivector mv1 => mv1.DivideByENormSquared(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByENormSquared(),
            RGaFloat64GradedMultivector mv1 => mv1.DivideByENormSquared(),
            RGaFloat64UniformMultivector mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector DivideByNorm(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByNorm(),
            RGaFloat64Vector mv1 => mv1.DivideByNorm(),
            RGaFloat64Bivector mv1 => mv1.DivideByNorm(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByNorm(),
            RGaFloat64GradedMultivector mv1 => mv1.DivideByNorm(),
            RGaFloat64UniformMultivector mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector DivideByNormSquared(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.DivideByNormSquared(),
            RGaFloat64Vector mv1 => mv1.DivideByNormSquared(),
            RGaFloat64Bivector mv1 => mv1.DivideByNormSquared(),
            RGaFloat64HigherKVector mv1 => mv1.DivideByNormSquared(),
            RGaFloat64GradedMultivector mv1 => mv1.DivideByNormSquared(),
            RGaFloat64UniformMultivector mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector EInverse(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.EInverse(),
            RGaFloat64Vector mv1 => mv1.EInverse(),
            RGaFloat64Bivector mv1 => mv1.EInverse(),
            RGaFloat64HigherKVector mv1 => mv1.EInverse(),
            RGaFloat64GradedMultivector mv1 => mv1.EInverse(),
            RGaFloat64UniformMultivector mv1 => mv1.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Inverse(this RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Inverse(),
            RGaFloat64Vector mv1 => mv1.Inverse(),
            RGaFloat64Bivector mv1 => mv1.Inverse(),
            RGaFloat64HigherKVector mv1 => mv1.Inverse(),
            RGaFloat64GradedMultivector mv1 => mv1.Inverse(),
            RGaFloat64UniformMultivector mv1 => mv1.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector EDual(this RGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.EDual(vSpaceDimensions),
            RGaFloat64Vector mv1 => mv1.EDual(vSpaceDimensions),
            RGaFloat64Bivector mv1 => mv1.EDual(vSpaceDimensions),
            RGaFloat64HigherKVector mv1 => mv1.EDual(vSpaceDimensions),
            RGaFloat64GradedMultivector mv1 => mv1.EDual(vSpaceDimensions),
            RGaFloat64UniformMultivector mv1 => mv1.EDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector EDual(this RGaFloat64Multivector mv, RGaFloat64KVector blade)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.EDual(blade),
            RGaFloat64Vector mv1 => mv1.EDual(blade),
            RGaFloat64Bivector mv1 => mv1.EDual(blade),
            RGaFloat64HigherKVector mv1 => mv1.EDual(blade),
            RGaFloat64GradedMultivector mv1 => mv1.EDual(blade),
            RGaFloat64UniformMultivector mv1 => mv1.EDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Dual(this RGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Dual(vSpaceDimensions),
            RGaFloat64Vector mv1 => mv1.Dual(vSpaceDimensions),
            RGaFloat64Bivector mv1 => mv1.Dual(vSpaceDimensions),
            RGaFloat64HigherKVector mv1 => mv1.Dual(vSpaceDimensions),
            RGaFloat64GradedMultivector mv1 => mv1.Dual(vSpaceDimensions),
            RGaFloat64UniformMultivector mv1 => mv1.Dual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Dual(this RGaFloat64Multivector mv, RGaFloat64KVector blade)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.Dual(blade),
            RGaFloat64Vector mv1 => mv1.Dual(blade),
            RGaFloat64Bivector mv1 => mv1.Dual(blade),
            RGaFloat64HigherKVector mv1 => mv1.Dual(blade),
            RGaFloat64GradedMultivector mv1 => mv1.Dual(blade),
            RGaFloat64UniformMultivector mv1 => mv1.Dual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector EUnDual(this RGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.EUnDual(vSpaceDimensions),
            RGaFloat64Vector mv1 => mv1.EUnDual(vSpaceDimensions),
            RGaFloat64Bivector mv1 => mv1.EUnDual(vSpaceDimensions),
            RGaFloat64HigherKVector mv1 => mv1.EUnDual(vSpaceDimensions),
            RGaFloat64GradedMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
            RGaFloat64UniformMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector EUnDual(this RGaFloat64Multivector mv, RGaFloat64KVector blade)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.EUnDual(blade),
            RGaFloat64Vector mv1 => mv1.EUnDual(blade),
            RGaFloat64Bivector mv1 => mv1.EUnDual(blade),
            RGaFloat64HigherKVector mv1 => mv1.EUnDual(blade),
            RGaFloat64GradedMultivector mv1 => mv1.EUnDual(blade),
            RGaFloat64UniformMultivector mv1 => mv1.EUnDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector UnDual(this RGaFloat64Multivector mv, int vSpaceDimensions)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.UnDual(vSpaceDimensions),
            RGaFloat64Vector mv1 => mv1.UnDual(vSpaceDimensions),
            RGaFloat64Bivector mv1 => mv1.UnDual(vSpaceDimensions),
            RGaFloat64HigherKVector mv1 => mv1.UnDual(vSpaceDimensions),
            RGaFloat64GradedMultivector mv1 => mv1.UnDual(vSpaceDimensions),
            RGaFloat64UniformMultivector mv1 => mv1.UnDual(vSpaceDimensions),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector UnDual(this RGaFloat64Multivector mv, RGaFloat64KVector blade)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => mv1.UnDual(blade),
            RGaFloat64Vector mv1 => mv1.UnDual(blade),
            RGaFloat64Bivector mv1 => mv1.UnDual(blade),
            RGaFloat64HigherKVector mv1 => mv1.UnDual(blade),
            RGaFloat64GradedMultivector mv1 => mv1.UnDual(blade),
            RGaFloat64UniformMultivector mv1 => mv1.UnDual(blade),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Op(this IEnumerable<RGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Op(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector EGp(this IEnumerable<RGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.EGp(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Gp(this IEnumerable<RGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Gp(mv)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetVectorPartAsTuple2D(this RGaFloat64Multivector mv)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)mv.GetBasisBladeScalar(1),
            (Float64Scalar)mv.GetBasisBladeScalar(2));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetVectorPartAsTuple3D(this RGaFloat64Multivector mv)
    {
        return LinFloat64Vector3D.Create(mv[1], mv[2], mv[3]);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Quaternion GetVectorPartAsTuple4D(this RGaFloat64Multivector mv)
    {
        return LinFloat64Quaternion.Create(mv.GetBasisBladeScalar(1),
            mv.GetBasisBladeScalar(2),
            mv.GetBasisBladeScalar(4),
            mv.GetBasisBladeScalar(8));
    }

        
    public static double[,] ScalarPlusBivectorToArray2D(this RGaFloat64Multivector mv, int arraySize)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector[,] GetMapTable(this IReadOnlyList<RGaFloat64Multivector> multivectorList, Func<RGaFloat64Multivector, RGaFloat64Multivector, RGaFloat64Multivector> multivectorMap)
    {
        return GetMapTable(
            multivectorList,
            multivectorList,
            multivectorMap
        );
    }

    public static RGaFloat64Multivector[,] GetMapTable(this IReadOnlyList<RGaFloat64Multivector> multivectorList1, IReadOnlyList<RGaFloat64Multivector> multivectorList2, Func<RGaFloat64Multivector, RGaFloat64Multivector, RGaFloat64Multivector> multivectorMap)
    {
        var rowCount = multivectorList1.Count;
        var colCount = multivectorList2.Count;

        var tableArray = new RGaFloat64Multivector[rowCount, colCount];

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