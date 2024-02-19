using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.ProductIterators;

public sealed class RGaMultivectorTermsIterator<T>
    : IRGaMultivectorTermsIterator<T>
{
    public static RGaMultivectorTermsIterator<T> Create(RGaMultivector<T> mv1, RGaMultivector<T> mv2)
    {
        return new RGaMultivectorTermsIterator<T>(mv1, mv2);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Multivector1.ScalarProcessor;

    public RGaProcessor<T> Processor 
        => Multivector1.Processor;

    public RGaMultivector<T> Multivector1 { get; set; }

    public RGaMultivector<T> Multivector2 { get; set; }


    private RGaMultivectorTermsIterator(RGaMultivector<T> storage1, RGaMultivector<T> storage2)
    {
        Multivector1 = storage1;
        Multivector2 = storage2;
    }


    private IEnumerable<KeyValuePair<ulong, T>> GetOpIdScalarRecords2(ulong id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.OpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<ulong, T>> GetELcpIdScalarRecords2(ulong id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.ELcpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<ulong, T>> GetERcpIdScalarRecords2(ulong id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.ERcpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<ulong, T>> GetEHipIdScalarRecords2(ulong id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.EHipIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<ulong, T>> GetEFdpIdScalarRecords2(ulong id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.EFdpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<ulong, T>> GetECpIdScalarRecords2(ulong id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.ECpIsNonZero(pair.Key));
    }

    private IEnumerable<KeyValuePair<ulong, T>> GetEAcpIdScalarRecords2(ulong id1)
    {
        return Multivector2.IdScalarPairs.Where(pair => id1.EAcpIsNonZero(pair.Key));
    }


    public IEnumerable<KeyValuePair<ulong, T>> GetOpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetOpIdScalarRecords2(id1))
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetEGpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in Multivector2.IdScalarPairs)
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetESpIdScalarRecords()
    {
        foreach (var (id, scalar1) in Multivector1.IdScalarPairs)
        {
            if (!Multivector2.TryGetBasisBladeScalarValue(id, out var scalar2))
                continue;

            var scalar = id.EGpSquaredIsNegative()
                ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                : ScalarProcessor.Times(scalar1, scalar2);

            yield return new KeyValuePair<ulong, T>(0, scalar);
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

            yield return scalar;
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetELcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetERcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetEHipIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetEFdpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetECpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetEAcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
            {
                var id = id1 ^ id2;
                var scalar = id1.EGpIsNegative(id2)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }


    public IEnumerable<KeyValuePair<ulong, T>> GetGpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in Multivector2.IdScalarPairs)
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1 ^ id2;
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetSpIdScalarRecords()
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

            yield return new KeyValuePair<ulong, T>(0, scalar);
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

            yield return scalar;
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetLcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1 ^ id2;
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetRcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1 ^ id2;
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetHipIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1 ^ id2;
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetFdpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1 ^ id2;
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetCpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1 ^ id2;
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }

    public IEnumerable<KeyValuePair<ulong, T>> GetAcpIdScalarRecords()
    {
        foreach (var (id1, scalar1) in Multivector1.IdScalarPairs)
        {
            foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
            {
                var basisSignature =
                    Processor.GpSign(id1, id2);

                if (basisSignature.IsZero)
                    continue;

                var id = id1 ^ id2;
                var scalar = basisSignature.IsNegative
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(id, scalar);
            }
        }
    }
}