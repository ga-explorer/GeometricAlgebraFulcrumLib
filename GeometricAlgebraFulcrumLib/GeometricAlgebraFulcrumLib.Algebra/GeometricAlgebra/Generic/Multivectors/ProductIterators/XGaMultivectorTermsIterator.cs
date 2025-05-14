using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.ProductIterators;

public sealed class XGaMultivectorTermsIterator<T>
    : IXGaMultivectorTermsIterator<T>
{
    public static XGaMultivectorTermsIterator<T> Create(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
    {
        return new XGaMultivectorTermsIterator<T>(mv1, mv2);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Multivector1.ScalarProcessor;

    public XGaProcessor<T> Processor 
        => Multivector1.Processor;

    public XGaMultivector<T> Multivector1 { get; set; }

    public XGaMultivector<T> Multivector2 { get; set; }


    private XGaMultivectorTermsIterator(XGaMultivector<T> storage1, XGaMultivector<T> storage2)
    {
        Multivector1 = storage1;
        Multivector2 = storage2;
    }


    private IEnumerable<KeyValuePair<IndexSet, T>> GetOpIdScalarRecords2(IndexSet id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.OpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<IndexSet, T>> GetELcpIdScalarRecords2(IndexSet id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.ELcpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<IndexSet, T>> GetERcpIdScalarRecords2(IndexSet id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.ERcpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<IndexSet, T>> GetEHipIdScalarRecords2(IndexSet id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.EHipIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<IndexSet, T>> GetEFdpIdScalarRecords2(IndexSet id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.EFdpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<IndexSet, T>> GetECpIdScalarRecords2(IndexSet id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.ECpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<IndexSet, T>> GetEAcpIdScalarRecords2(IndexSet id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.EAcpIsNonZero(pair.Key));
    }


    public IEnumerable<KeyValuePair<IndexSet, T>> GetOpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetOpIdScalarRecords2(id1))
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEGpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in Multivector2.IdScalarPairs)
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetESpIdScalarRecords()
    {
        foreach (var (id, scalar1) in Multivector1.IdScalarPairs)
        {
            if (!Multivector2.TryGetBasisBladeScalarValue(id, out var scalar2))
                continue;

            var scalar = id.EGpSquaredIsNegative()
                ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                : ScalarProcessor.Times(scalar1, scalar2);

            yield return new KeyValuePair<IndexSet, T>(IndexSet.EmptySet, scalar.ScalarValue);
        }
    }

    public IEnumerable<T> GetESpScalars()
    {
        foreach (var (id, scalar1) in Multivector1.IdScalarPairs)
        {
            if (!Multivector2.TryGetBasisBladeScalarValue(id, out var scalar2))
                continue;

            var scalar = id.EGpSquaredIsNegative()
                ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                : ScalarProcessor.Times(scalar1, scalar2);

            yield return scalar.ScalarValue;
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetELcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetERcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEHipIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEFdpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetECpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetEAcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
            {
                var id = id1.SetMerge(id2);
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }


    public IEnumerable<KeyValuePair<IndexSet, T>> GetGpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in Multivector2.IdScalarPairs)
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1.SetMerge(id2);
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetSpIdScalarRecords()
    {
        foreach (var (id, scalar1) in Multivector1.IdScalarPairs)
        {
            var basisSignature =
                Processor.Signature(id);

            if (basisSignature.IsZero)
                continue;

            if (!Multivector2.TryGetBasisBladeScalarValue(id, out var scalar2))
                continue;

            var scalar = basisSignature.IsNegative
                ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                : ScalarProcessor.Times(scalar1, scalar2);

            yield return new KeyValuePair<IndexSet, T>(IndexSet.EmptySet, scalar.ScalarValue);
        }
    }

    public IEnumerable<T> GetSpScalars()
    {
        foreach (var (id, scalar1) in Multivector1.IdScalarPairs)
        {
            var basisSignature =
                Processor.Signature(id);

            if (basisSignature.IsZero)
                continue;

            if (!Multivector2.TryGetBasisBladeScalarValue(id, out var scalar2))
                continue;

            var scalar = basisSignature.IsNegative
                ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                : ScalarProcessor.Times(scalar1, scalar2);

            yield return scalar.ScalarValue;
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetLcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1.SetMerge(id2);
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetRcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1.SetMerge(id2);
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetHipIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1.SetMerge(id2);
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetFdpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1.SetMerge(id2);
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetCpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1.SetMerge(id2);
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }

    public IEnumerable<KeyValuePair<IndexSet, T>> GetAcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1.SetMerge(id2);
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<IndexSet, T>(id, scalar.ScalarValue);
            }
        }
    }
}