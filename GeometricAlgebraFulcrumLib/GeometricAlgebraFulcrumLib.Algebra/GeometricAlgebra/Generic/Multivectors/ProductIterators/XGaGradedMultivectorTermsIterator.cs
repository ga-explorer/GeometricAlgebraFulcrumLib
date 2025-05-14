using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.ProductIterators;

public sealed class XGaGradedMultivectorTermsIterator<T>
{
    public static XGaGradedMultivectorTermsIterator<T> Create(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
    {
        return new(
            mv1.Processor,
            mv1,
            mv2
        );
    }
        

    private readonly XGaMultivectorTermsIterator<T> _termsIterator;


    public IScalarProcessor<T> ScalarProcessor 
        => Multivector1.ScalarProcessor;

    public XGaProcessor<T> Processor 
        => Multivector1.Processor;
        
    public XGaGradedMultivector<T> Multivector1 { get; }

    public XGaGradedMultivector<T> Multivector2 { get; }

    public IEnumerable<XGaKVector<T>> KVectors1 
        => Multivector1.KVectors;
        
    public IEnumerable<XGaKVector<T>> KVectors2 
        => Multivector2.KVectors;


    private XGaGradedMultivectorTermsIterator(XGaProcessor<T> processor, XGaGradedMultivector<T> multivector1, XGaGradedMultivector<T> multivector2)
    {
        var mv1 = processor.ScalarZero;

        _termsIterator = XGaMultivectorTermsIterator<T>.Create(mv1, mv1);

        Multivector1 = multivector1;
        Multivector2 = multivector2;
    }


    private IEnumerable<XGaKVector<T>> GetOpKVectors2(int grade1)
    {
        return KVectors2
            .Where(kVector =>
                grade1 + kVector.Grade <= 64
            );
    }

    private IEnumerable<XGaKVector<T>> GetESpKVector2(int grade1)
    {
        return KVectors2.Where(s => s.Grade == grade1);
    }

    private IEnumerable<XGaKVector<T>> GetELcpKVectors2(int grade1)
    {
        return KVectors2.Where(kVector =>
            kVector.Grade >= grade1
        );
    }

    private IEnumerable<XGaKVector<T>> GetERcpKVectors2(int grade1)
    {
        return KVectors2.Where(kVector =>
            grade1 >= kVector.Grade
        );
    }


    public XGaKVector<T> GetOpKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        var grade = kVector1.Grade + kVector2.Grade;

        if (grade > 64)
            return Processor.ScalarZero;

        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetOpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }

    public T GetESpScalar(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        return ScalarProcessor.Add(
            _termsIterator.GetESpScalars()
        ).ScalarValue;
    }

    public XGaKVector<T> GetELcpKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        if (kVector2.Grade < kVector1.Grade)
            return Processor.ScalarZero;

        var grade = kVector2.Grade - kVector1.Grade;

        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetELcpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }

    public XGaKVector<T> GetERcpKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        if (kVector1.Grade < kVector2.Grade)
            return Processor.ScalarZero;

        var grade = kVector1.Grade - kVector2.Grade;

        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetERcpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }

    public XGaKVector<T> GetEHipKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        if (kVector1.Grade < 1 || kVector2.Grade < 1)
            return Processor.ScalarZero;

        var grade = Math.Abs(kVector1.Grade - kVector2.Grade);

        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetERcpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }

    public XGaKVector<T> GetEFdpKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        var grade = Math.Abs(kVector1.Grade - kVector2.Grade);
        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetEFdpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }


    public T GetSpScalar(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        return ScalarProcessor.Add(
            _termsIterator.GetSpScalars()
        ).ScalarValue;
    }

    public XGaKVector<T> GetLcpKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        if (kVector2.Grade < kVector1.Grade)
            return Processor.ScalarZero;

        var grade = kVector2.Grade - kVector1.Grade;

        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetLcpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }

    public XGaKVector<T> GetRcpKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        if (kVector1.Grade < kVector2.Grade)
            return Processor.ScalarZero;

        var grade = kVector1.Grade - kVector2.Grade;

        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetRcpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }

    public XGaKVector<T> GetHipKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        if (kVector1.Grade < 1 || kVector2.Grade < 1)
            return Processor.ScalarZero;

        var grade = Math.Abs(kVector1.Grade - kVector2.Grade);

        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetRcpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }

    public XGaKVector<T> GetFdpKVector(XGaKVector<T> kVector1, XGaKVector<T> kVector2)
    {
        var grade = Math.Abs(kVector1.Grade - kVector2.Grade);
        var kVector = Processor.CreateComposer();

        _termsIterator.Multivector1 = kVector1;
        _termsIterator.Multivector2 = kVector2;

        kVector.AddTerms(
            _termsIterator.GetFdpIdScalarRecords()
        );

        return kVector.GetKVector(grade);
    }


    public IEnumerable<XGaKVector<T>> GetOpKVectors()
    {
        foreach (var kVector1 in KVectors1)
        {
            var grade1 = kVector1.Grade;

            foreach (var kVector2 in GetOpKVectors2(grade1))
                yield return GetOpKVector(kVector1, kVector2);
        }
    }

    public IEnumerable<T> GetESpScalars()
    {
        foreach (var kVector1 in KVectors1)
        {
            var grade1 = kVector1.Grade;

            foreach (var kVector2 in GetESpKVector2(grade1))
                yield return GetESpScalar(kVector1, kVector2);
        }
    }

    public IEnumerable<XGaKVector<T>> GetELcpKVectors()
    {
        foreach (var kVector1 in KVectors1)
        {
            var grade1 = kVector1.Grade;

            foreach (var kVector2 in GetELcpKVectors2(grade1))
                yield return GetELcpKVector(kVector1, kVector2);
        }
    }

    public IEnumerable<XGaKVector<T>> GetERcpKVectors()
    {
        foreach (var kVector1 in KVectors1)
        {
            var grade1 = kVector1.Grade;

            foreach (var kVector2 in GetERcpKVectors2(grade1))
                yield return GetERcpKVector(kVector1, kVector2);
        }
    }

    public IEnumerable<XGaKVector<T>> GetEHipKVectors()
    {
        foreach (var kVector1 in KVectors1.Where(s => s.Grade > 0))
        foreach (var kVector2 in KVectors2.Where(s => s.Grade > 0))
            yield return GetEHipKVector(kVector1, kVector2);
    }

    public IEnumerable<XGaKVector<T>> GetEFdpKVectors()
    {
        foreach (var kVector1 in KVectors1)
        foreach (var kVector2 in KVectors2)
            yield return GetEFdpKVector(kVector1, kVector2);
    }


    public IEnumerable<XGaKVector<T>> GetLcpKVectors()
    {
        foreach (var kVector1 in KVectors1)
        {
            var grade1 = kVector1.Grade;

            foreach (var kVector2 in GetELcpKVectors2(grade1))
                yield return GetLcpKVector(kVector1, kVector2);
        }
    }

    public IEnumerable<XGaKVector<T>> GetRcpKVectors()
    {
        foreach (var kVector1 in KVectors1)
        {
            var grade1 = kVector1.Grade;

            foreach (var kVector2 in GetERcpKVectors2(grade1))
                yield return GetRcpKVector(kVector1, kVector2);
        }
    }

    public IEnumerable<XGaKVector<T>> GetHipKVectors()
    {
        foreach (var kVector1 in KVectors1.Where(s => s.Grade > 0))
        foreach (var kVector2 in KVectors2.Where(s => s.Grade > 0))
            yield return GetHipKVector(kVector1, kVector2);
    }

    public IEnumerable<XGaKVector<T>> GetFdpKVectors()
    {
        foreach (var kVector1 in KVectors1)
        foreach (var kVector2 in KVectors2)
            yield return GetFdpKVector(kVector1, kVector2);
    }
}