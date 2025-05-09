using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;

public static class XGaFloat64MultivectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer CreateComposer(this XGaFloat64Processor processor, double scalarValue)
    {
        return new XGaFloat64MultivectorComposer(processor).SetScalarTerm(scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer CreateComposer(this XGaFloat64Processor processor)
    {
        return new XGaFloat64MultivectorComposer(processor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer ToComposer(this XGaFloat64Scalar scalar)
    {
        return new XGaFloat64MultivectorComposer(scalar.Processor).SetScalar(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer ToComposer(this XGaFloat64Multivector mv)
    {
        return new XGaFloat64MultivectorComposer(mv.Processor).SetMultivector(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer NegativeToComposer(this XGaFloat64Scalar scalar)
    {
        return new XGaFloat64MultivectorComposer(scalar.Processor).SetScalarNegative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer NegativeToComposer(this XGaFloat64Multivector mv)
    {
        return new XGaFloat64MultivectorComposer(mv.Processor).SetMultivectorNegative(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer ToComposer(this XGaFloat64Scalar scalar, double scalingFactor)
    {
        return new XGaFloat64MultivectorComposer(scalar.Processor).SetScalar(scalar, scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer ToComposer(this XGaFloat64Multivector mv, double scalingFactor)
    {
        return new XGaFloat64MultivectorComposer(mv.Processor).SetMultivector(mv, scalingFactor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComposer AddEGpTerm(this Float64ScalarComposer composer, XGaFloat64Processor processor, IndexSet id, double scalar1, double scalar2)
    {
        var term = processor.EGpSign(id, id);
        var scalar = term.IsPositive
            ? scalar1 * scalar2
            : -(scalar1 * scalar2);

        return composer.AddScalarValue(scalar);
    }

    public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
            return composer;

        var processor = mv1.Processor;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddEGpTerm(processor, id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddEGpTerm(processor, id, scalar1, scalar2);
            }
        }

        return composer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv1.TryGetKVector(mv2.Grade, out var kVector1)
            ? composer.AddESpTerms(kVector1, mv2)
            : composer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv2.TryGetKVector(mv1.Grade, out var kVector2)
            ? composer.AddESpTerms(mv1, kVector2)
            : composer;
    }

    public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.KVectorCount <= mv2.KVectorCount)
        {
            foreach (var kVector1 in mv1.KVectors)
            {
                var grade = kVector1.Grade;

                if (!mv2.TryGetKVector(grade, out var kVector2))
                    continue;

                composer.AddESpTerms(kVector1, kVector2);
            }
        }
        else
        {
            foreach (var kVector2 in mv2.KVectors)
            {
                var grade = kVector2.Grade;

                if (!mv1.TryGetKVector(grade, out var kVector1))
                    continue;

                composer.AddESpTerms(kVector1, kVector2);
            }
        }

        return composer;
    }

    public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        var processor = mv1.Processor;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddEGpTerm(processor, id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddEGpTerm(processor, id, scalar1, scalar2);
            }
        }

        return composer;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComposer AddGpTerm(this Float64ScalarComposer composer, XGaFloat64Processor processor, IndexSet id, double scalar1, double scalar2)
    {
        var term = processor.GpSign(id, id);

        if (term.IsZero)
            return composer;

        var scalar = term.IsPositive
            ? scalar1 * scalar2
            : -(scalar1 * scalar2);

        return composer.AddScalarValue(scalar);
    }

    public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
            return composer;

        var processor = mv1.Processor;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddGpTerm(processor, id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddGpTerm(processor, id, scalar1, scalar2);
            }
        }

        return composer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv1.TryGetKVector(mv2.Grade, out var kVector1)
            ? composer.AddSpTerms(kVector1, mv2)
            : composer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv2.TryGetKVector(mv1.Grade, out var kVector2)
            ? composer.AddSpTerms(mv1, kVector2)
            : composer;
    }

    public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.KVectorCount <= mv2.KVectorCount)
        {
            foreach (var kVector1 in mv1.KVectors)
            {
                var grade = kVector1.Grade;

                if (!mv2.TryGetKVector(grade, out var kVector2))
                    continue;

                composer.AddSpTerms(kVector1, kVector2);
            }
        }
        else
        {
            foreach (var kVector2 in mv2.KVectors)
            {
                var grade = kVector2.Grade;

                if (!mv1.TryGetKVector(grade, out var kVector1))
                    continue;

                composer.AddSpTerms(kVector1, kVector2);
            }
        }

        return composer;
    }

    public static Float64ScalarComposer AddSpTerms(this Float64ScalarComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        var processor = mv1.Processor;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddGpTerm(processor, id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddGpTerm(processor, id, scalar1, scalar2);
            }
        }

        return composer;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar GetXGaFloat64Scalar(this Float64ScalarComposer composer, XGaFloat64Processor processor)
    {
        return processor.Scalar(composer.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector GetXGaFloat64Vector(this Float64ScalarComposer composer, XGaFloat64Processor processor, int index)
    {
        return processor.VectorTerm(index, composer.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector GetXGaFloat64Bivector(this Float64ScalarComposer composer, XGaFloat64Processor processor, int index1, int index2)
    {
        return processor.BivectorTerm(index1, index2, composer.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector GetXGaFloat64HigherKVector(this Float64ScalarComposer composer, XGaFloat64Processor processor, IndexSet id)
    {
        return processor.HigherKVectorTerm(id, composer.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector GetXGaFloat64KVector(this Float64ScalarComposer composer, XGaFloat64Processor processor, IndexSet id)
    {
        return processor.KVectorTerm(id, composer.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector GetXGaFloat64GradedMultivector(this Float64ScalarComposer composer, XGaFloat64Processor processor, IndexSet id)
    {
        return processor.Multivector(id, composer.ScalarValue);
    }


    private static XGaFloat64MultivectorComposer AddEuclideanProductTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var term1 in mv1.IdScalarPairs)
        foreach (var term2 in mv2.IdScalarPairs)
            if (filterFunc(term1.Key, term2.Key))
                composer.AddEGpTerm(term1, term2);

        return composer;
    }

    private static XGaFloat64MultivectorComposer AddMetricProductTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, Func<IndexSet, IndexSet, bool> filterFunc)
    {
        Debug.Assert(
            composer.Metric.HasSameSignature(mv1.Metric) &&
            composer.Metric.HasSameSignature(mv2.Metric)
        );

        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var term1 in mv1.IdScalarPairs)
        foreach (var term2 in mv2.IdScalarPairs)
            if (filterFunc(term1.Key, term2.Key))
                composer.AddGpTerm(term1, term2);

        return composer;
    }


    public static XGaFloat64MultivectorComposer AddESpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddEGpTerm(id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddEGpTerm(id, scalar1, scalar2);
            }
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddELcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade > mv2.Grade)
            return composer;

        if (mv1.Grade == mv2.Grade)
            return composer.AddESpTerms(mv1, mv2);

        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ELcpIsNonZero
        );
    }

    public static XGaFloat64MultivectorComposer AddERcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade < mv2.Grade)
            return composer;

        if (mv1.Grade == mv2.Grade)
            return composer.AddESpTerms(mv1, mv2);

        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ERcpIsNonZero
        );
    }

    public static XGaFloat64MultivectorComposer AddEFdpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade == mv2.Grade)
            return composer.AddESpTerms(mv1, mv2);

        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EFdpIsNonZero
        );
    }

    public static XGaFloat64MultivectorComposer AddEHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade == 0 || mv2.Grade == 0)
            return composer;

        if (mv1.Grade == mv2.Grade)
            return composer.AddESpTerms(mv1, mv2);

        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EFdpIsNonZero
        );
    }


    public static XGaFloat64MultivectorComposer AddSpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade != mv2.Grade || mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddGpTerm(id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddGpTerm(id, scalar1, scalar2);
            }
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddLcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade > mv2.Grade)
            return composer;

        if (mv1.Grade == mv2.Grade)
            return composer.AddSpTerms(mv1, mv2);

        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ELcpIsNonZero
        );
    }

    public static XGaFloat64MultivectorComposer AddRcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade < mv2.Grade)
            return composer;

        if (mv1.Grade == mv2.Grade)
            return composer.AddSpTerms(mv1, mv2);

        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ERcpIsNonZero
        );
    }

    public static XGaFloat64MultivectorComposer AddFdpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade == mv2.Grade)
            return composer.AddSpTerms(mv1, mv2);

        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EFdpIsNonZero
        );
    }

    public static XGaFloat64MultivectorComposer AddHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.Grade == 0 || mv2.Grade == 0)
            return composer;

        if (mv1.Grade == mv2.Grade)
            return composer.AddSpTerms(mv1, mv2);

        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EFdpIsNonZero
        );
    }


    public static XGaFloat64MultivectorComposer AddESpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv1.TryGetKVector(mv2.Grade, out var kVector1)
            ? composer.AddESpTerms(kVector1, mv2)
            : composer;
    }

    public static XGaFloat64MultivectorComposer AddELcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            if (kVector1.Grade <= mv2.Grade)
                composer.AddELcpTerms(kVector1, mv2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddERcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            if (kVector1.Grade >= mv2.Grade)
                composer.AddERcpTerms(kVector1, mv2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddEHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero || mv2.Grade == 0)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            if (kVector1.Grade > 0)
                composer.AddEFdpTerms(kVector1, mv2);
        }

        return composer;
    }


    public static XGaFloat64MultivectorComposer AddSpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv1.TryGetKVector(mv2.Grade, out var kVector1)
            ? composer.AddSpTerms(kVector1, mv2)
            : composer;
    }

    public static XGaFloat64MultivectorComposer AddLcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            if (kVector1.Grade <= mv2.Grade)
                composer.AddLcpTerms(kVector1, mv2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddRcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            if (kVector1.Grade >= mv2.Grade)
                composer.AddRcpTerms(kVector1, mv2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero || mv2.IsZero || mv2.Grade == 0)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            if (kVector1.Grade > 0)
                composer.AddFdpTerms(kVector1, mv2);
        }

        return composer;
    }


    public static XGaFloat64MultivectorComposer AddESpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv2.TryGetKVector(mv1.Grade, out var kVector2)
            ? composer.AddESpTerms(mv1, kVector2)
            : composer;
    }

    public static XGaFloat64MultivectorComposer AddELcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector2 in mv2.KVectors)
        {
            if (mv1.Grade <= kVector2.Grade)
                composer.AddELcpTerms(mv1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddERcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector2 in mv2.KVectors)
        {
            if (mv1.Grade >= kVector2.Grade)
                composer.AddERcpTerms(mv1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddEHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero || mv1.Grade == 0)
            return composer;

        foreach (var kVector2 in mv2.KVectors)
        {
            if (kVector2.Grade > 0)
                composer.AddEFdpTerms(mv1, kVector2);
        }

        return composer;
    }


    public static XGaFloat64MultivectorComposer AddSpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        return mv2.TryGetKVector(mv1.Grade, out var kVector2)
            ? composer.AddSpTerms(mv1, kVector2)
            : composer;
    }

    public static XGaFloat64MultivectorComposer AddLcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector2 in mv2.KVectors)
        {
            if (mv1.Grade <= kVector2.Grade)
                composer.AddLcpTerms(mv1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddRcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector2 in mv2.KVectors)
        {
            if (mv1.Grade >= kVector2.Grade)
                composer.AddRcpTerms(mv1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64KVector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero || mv1.Grade == 0)
            return composer;

        foreach (var kVector2 in mv2.KVectors)
        {
            if (kVector2.Grade > 0)
                composer.AddFdpTerms(mv1, kVector2);
        }

        return composer;
    }


    public static XGaFloat64MultivectorComposer AddESpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.KVectorCount <= mv2.KVectorCount)
        {
            foreach (var kVector1 in mv1.KVectors)
            {
                var grade = kVector1.Grade;

                if (!mv2.TryGetKVector(grade, out var kVector2))
                    continue;

                composer.AddESpTerms(kVector1, kVector2);
            }
        }
        else
        {
            foreach (var kVector2 in mv2.KVectors)
            {
                var grade = kVector2.Grade;

                if (!mv1.TryGetKVector(grade, out var kVector1))
                    continue;

                composer.AddESpTerms(kVector1, kVector2);
            }
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddELcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            var grade1 = kVector1.Grade;
            var kVectorList2 =
                mv2.KVectors.Where(kv => grade1 <= kv.Grade);

            foreach (var kVector2 in kVectorList2)
                composer.AddELcpTerms(kVector1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddERcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            var grade1 = kVector1.Grade;
            var kVectorList2 =
                mv2.KVectors.Where(kv => grade1 >= kv.Grade);

            foreach (var kVector2 in kVectorList2)
                composer.AddERcpTerms(kVector1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddEHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        var kVectorList1 =
            mv1.KVectors.Where(kv => kv.Grade > 0);

        foreach (var kVector1 in kVectorList1)
        {
            var kVectorList2 =
                mv2.KVectors.Where(kv => kv.Grade > 0);

            foreach (var kVector2 in kVectorList2)
                composer.AddEFdpTerms(kVector1, kVector2);
        }

        return composer;
    }


    public static XGaFloat64MultivectorComposer AddSpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.KVectorCount <= mv2.KVectorCount)
        {
            foreach (var kVector1 in mv1.KVectors)
            {
                var grade = kVector1.Grade;

                if (!mv2.TryGetKVector(grade, out var kVector2))
                    continue;

                composer.AddSpTerms(kVector1, kVector2);
            }
        }
        else
        {
            foreach (var kVector2 in mv2.KVectors)
            {
                var grade = kVector2.Grade;

                if (!mv1.TryGetKVector(grade, out var kVector1))
                    continue;

                composer.AddSpTerms(kVector1, kVector2);
            }
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddLcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            var grade1 = kVector1.Grade;
            var kVectorList2 =
                mv2.KVectors.Where(kv => grade1 <= kv.Grade);

            foreach (var kVector2 in kVectorList2)
                composer.AddLcpTerms(kVector1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddRcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var kVector1 in mv1.KVectors)
        {
            var grade1 = kVector1.Grade;
            var kVectorList2 =
                mv2.KVectors.Where(kv => grade1 >= kv.Grade);

            foreach (var kVector2 in kVectorList2)
                composer.AddRcpTerms(kVector1, kVector2);
        }

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        var kVectorList1 =
            mv1.KVectors.Where(kv => kv.Grade > 0);

        foreach (var kVector1 in kVectorList1)
        {
            var kVectorList2 =
                mv2.KVectors.Where(kv => kv.Grade > 0);

            foreach (var kVector2 in kVectorList2)
                composer.AddFdpTerms(kVector1, kVector2);
        }

        return composer;
    }


    public static XGaFloat64MultivectorComposer AddEGpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var term1 in mv1.IdScalarPairs)
        foreach (var term2 in mv2.IdScalarPairs)
            composer.AddEGpTerm(term1, term2);

        return composer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddOpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.OpIsNonZero
        );
    }

    public static XGaFloat64MultivectorComposer AddESpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        if (mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddEGpTerm(id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddEGpTerm(id, scalar1, scalar2);
            }
        }

        return composer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddELcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ELcpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddERcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ERcpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddEFdpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EFdpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddEHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EHipIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddEAcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EAcpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddECpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddEuclideanProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ECpIsNonZero
        );
    }


    public static XGaFloat64MultivectorComposer AddGpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        Debug.Assert(
            composer.Metric.HasSameSignature(mv1.Metric) &&
            composer.Metric.HasSameSignature(mv2.Metric)
        );

        if (mv1.IsZero || mv2.IsZero)
            return composer;

        foreach (var term1 in mv1.IdScalarPairs)
        foreach (var term2 in mv2.IdScalarPairs)
            composer.AddGpTerm(term1, term2);

        return composer;
    }

    public static XGaFloat64MultivectorComposer AddSpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        Debug.Assert(
            composer.Metric.HasSameSignature(mv1.Metric) &&
            composer.Metric.HasSameSignature(mv2.Metric)
        );

        if (mv1.IsZero || mv2.IsZero)
            return composer;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1.IdScalarPairs)
            {
                if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
                    continue;

                composer.AddGpTerm(id, scalar1, scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2.IdScalarPairs)
            {
                if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
                    continue;

                composer.AddGpTerm(id, scalar1, scalar2);
            }
        }

        return composer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddLcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ELcpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddRcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ERcpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddFdpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EFdpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddHipTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EHipIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddAcpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.EAcpIsNonZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64MultivectorComposer AddCpTerms(this XGaFloat64MultivectorComposer composer, XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return composer.AddMetricProductTerms(
            mv1,
            mv2,
            BasisBladeProductUtils.ECpIsNonZero
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector Multivector(this XGaFloat64Processor processor, IReadOnlyDictionary<IndexSet, double> termList)
    {
        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector Multivector(this XGaFloat64Processor processor, IReadOnlyDictionary<int, XGaFloat64KVector> gradeKVectorDictionary)
    {
        if (gradeKVectorDictionary.Count == 0 && gradeKVectorDictionary is not EmptyDictionary<int, XGaFloat64KVector>)
            return processor.MultivectorZero;

        if (gradeKVectorDictionary.Count == 1 && gradeKVectorDictionary is not SingleItemDictionary<int, XGaFloat64KVector>)
            return gradeKVectorDictionary.Values.First().ToMultivector();

        return new XGaFloat64GradedMultivector(processor, gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector Multivector(this XGaFloat64Processor processor, IEnumerable<KeyValuePair<IndexSet, double>> termList)
    {
        return processor
            .CreateComposer()
            .AddTerms(termList)
            .GetMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Multivector2D(this XGaFloat64Processor processor, double scalar, double vectorScalar0, double vectorScalar1, double bivectorScalar)
    {
        return processor
            .CreateComposer()
            .SetTerm(0, scalar)
            .SetTerm(1, vectorScalar0)
            .SetTerm(2, vectorScalar1)
            .SetTerm(3, bivectorScalar)
            .GetMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector Multivector(this XGaFloat64Processor processor, IndexSet id)
    {
        var grade = id.Count;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
            grade,
            processor.KVectorTerm(id, 1d)
        );

        return new XGaFloat64GradedMultivector(
            processor,
            gradeKVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector Multivector(this XGaFloat64Processor processor, IndexSet id, double scalar)
    {
        var grade = id.Count;

        if (scalar.IsZero())
            return processor.MultivectorZero;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
            grade,
            processor.KVectorTerm(id, scalar)
        );

        return new XGaFloat64GradedMultivector(
            processor,

            gradeKVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector Multivector(this XGaFloat64Processor processor, KeyValuePair<IndexSet, double> basisScalarPair)
    {
        var (id, scalar) = basisScalarPair;
        var grade = id.Count;

        if (scalar.IsZero())
            return processor.MultivectorZero;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
            grade,
            processor.KVectorTerm(basisScalarPair)
        );

        return new XGaFloat64GradedMultivector(
            processor,

            gradeKVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector ToMultivector(this XGaFloat64Vector kVector)
    {
        if (kVector.IsZero)
            return kVector.Processor.MultivectorZero;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
            1,
            kVector
        );

        return new XGaFloat64GradedMultivector(
            kVector.Processor,
            gradeKVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector ToMultivector(this XGaFloat64Bivector kVector)
    {
        if (kVector.IsZero)
            return kVector.Processor.MultivectorZero;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
            2,
            kVector
        );

        return new XGaFloat64GradedMultivector(kVector.Processor, gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector ToMultivector(this XGaFloat64HigherKVector kVector)
    {
        if (kVector.IsZero)
            return kVector.Processor.MultivectorZero;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
            kVector.Grade,
            kVector
        );

        return new XGaFloat64GradedMultivector(kVector.Processor, gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector ToMultivector(this XGaFloat64KVector kVector)
    {
        if (kVector.IsZero)
            return kVector.Processor.MultivectorZero;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaFloat64KVector>(
            kVector.Grade,
            kVector
        );

        return new XGaFloat64GradedMultivector(kVector.Processor, gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector ToMultivector(this XGaFloat64GradedMultivector multivector)
    {
        return multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector ToMultivector(this XGaFloat64UniformMultivector multivector)
    {
        return multivector.ToComposer().GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector ToMultivector(this XGaFloat64Multivector multivector)
    {
        return multivector switch
        {
            XGaFloat64KVector kVector => kVector.ToMultivector(),
            XGaFloat64GradedMultivector mv => mv,
            XGaFloat64UniformMultivector mv => mv.ToMultivector(),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector ToUniformMultivector(this XGaFloat64Vector kVector)
    {
        return kVector.ToComposer().GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector ToUniformMultivector(this XGaFloat64Bivector kVector)
    {
        return kVector.ToComposer().GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector ToUniformMultivector(this XGaFloat64HigherKVector kVector)
    {
        return kVector.ToComposer().GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector ToUniformMultivector(this XGaFloat64KVector kVector)
    {
        return kVector.ToComposer().GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector ToMUniformMultivector(this XGaFloat64UniformMultivector multivector)
    {
        return multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector ToUniformMultivector(this XGaFloat64GradedMultivector multivector)
    {
        return multivector.ToComposer().GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector ToUniformMultivector(this XGaFloat64Multivector multivector)
    {
        return multivector switch
        {
            XGaFloat64KVector kVector => kVector.ToUniformMultivector(),
            XGaFloat64UniformMultivector mv => mv,
            XGaFloat64GradedMultivector mv => mv.ToUniformMultivector(),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector UniformMultivector(this XGaFloat64Processor processor, IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IndexSet, double>)
            return processor.UniformMultivectorZero;

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IndexSet, double>)
            return processor.UniformMultivector(basisScalarDictionary.First());

        return new XGaFloat64UniformMultivector(
            processor,

            basisScalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector UniformMultivector(this XGaFloat64Processor processor, IndexSet basisBlade)
    {
        return new XGaFloat64UniformMultivector(processor,

            new SingleItemDictionary<IndexSet, double>(
                basisBlade,
                1d
            ));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector UniformMultivector(this XGaFloat64Processor processor, IndexSet basisBlade, double scalar)
    {
        if (scalar.IsZero())
            return new XGaFloat64UniformMultivector(processor);

        return new XGaFloat64UniformMultivector(processor,

            new SingleItemDictionary<IndexSet, double>(
                basisBlade,
                scalar
            ));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector UniformMultivector(this XGaFloat64Processor processor, KeyValuePair<IndexSet, double> basisScalarPair)
    {
        return processor.UniformMultivector(

            basisScalarPair.Key,
            basisScalarPair.Value
        );
    }

}