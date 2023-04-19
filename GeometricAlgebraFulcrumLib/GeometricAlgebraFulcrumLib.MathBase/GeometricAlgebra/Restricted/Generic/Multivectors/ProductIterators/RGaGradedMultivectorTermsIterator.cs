using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.ProductIterators
{
    public sealed class RGaGradedMultivectorTermsIterator<T>
    {
        public static RGaGradedMultivectorTermsIterator<T> Create(RGaGradedMultivector<T> mv1, RGaGradedMultivector<T> mv2)
        {
            return new(
                mv1.Processor,
                mv1,
                mv2
            );
        }
        

        private readonly RGaMultivectorTermsIterator<T> _termsIterator;


        public IScalarProcessor<T> ScalarProcessor 
            => Multivector1.ScalarProcessor;

        public RGaProcessor<T> Processor 
            => Multivector1.Processor;
        
        public RGaGradedMultivector<T> Multivector1 { get; }

        public RGaGradedMultivector<T> Multivector2 { get; }

        public IEnumerable<RGaKVector<T>> KVectors1 
            => Multivector1.KVectors;
        
        public IEnumerable<RGaKVector<T>> KVectors2 
            => Multivector2.KVectors;


        private RGaGradedMultivectorTermsIterator(RGaProcessor<T> processor, RGaGradedMultivector<T> multivector1, RGaGradedMultivector<T> multivector2)
        {
            var mv1 = processor.CreateZeroScalar();

            _termsIterator = RGaMultivectorTermsIterator<T>.Create(mv1, mv1);

            Multivector1 = multivector1;
            Multivector2 = multivector2;
        }


        private IEnumerable<RGaKVector<T>> GetOpKVectors2(int grade1)
        {
            return KVectors2
                .Where(kVector =>
                    grade1 + kVector.Grade <= 64
                );
        }

        private IEnumerable<RGaKVector<T>> GetESpKVector2(int grade1)
        {
            return KVectors2.Where(s => s.Grade == grade1);
        }

        private IEnumerable<RGaKVector<T>> GetELcpKVectors2(int grade1)
        {
            return KVectors2.Where(kVector =>
                kVector.Grade >= grade1
            );
        }

        private IEnumerable<RGaKVector<T>> GetERcpKVectors2(int grade1)
        {
            return KVectors2.Where(kVector =>
                grade1 >= kVector.Grade
            );
        }


        public RGaKVector<T> GetOpKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            var grade = kVector1.Grade + kVector2.Grade;

            if (grade > 64)
                return Processor.CreateZeroScalar();

            var kVector = Processor.CreateComposer();

            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            kVector.AddTerms(
                _termsIterator.GetOpIdScalarRecords()
            );

            return kVector.GetKVector(grade);
        }

        public T GetESpScalar(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            return ScalarProcessor.Add(
                _termsIterator.GetESpScalars()
            );
        }

        public RGaKVector<T> GetELcpKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            if (kVector2.Grade < kVector1.Grade)
                return Processor.CreateZeroScalar();

            var grade = kVector2.Grade - kVector1.Grade;

            var kVector = Processor.CreateComposer();

            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            kVector.AddTerms(
                _termsIterator.GetELcpIdScalarRecords()
            );

            return kVector.GetKVector(grade);
        }

        public RGaKVector<T> GetERcpKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            if (kVector1.Grade < kVector2.Grade)
                return Processor.CreateZeroScalar();

            var grade = kVector1.Grade - kVector2.Grade;

            var kVector = Processor.CreateComposer();

            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            kVector.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return kVector.GetKVector(grade);
        }

        public RGaKVector<T> GetEHipKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            if (kVector1.Grade < 1 || kVector2.Grade < 1)
                return Processor.CreateZeroScalar();

            var grade = Math.Abs(kVector1.Grade - kVector2.Grade);

            var kVector = Processor.CreateComposer();

            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            kVector.AddTerms(
                _termsIterator.GetERcpIdScalarRecords()
            );

            return kVector.GetKVector(grade);
        }

        public RGaKVector<T> GetEFdpKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
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


        public T GetSpScalar(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            return ScalarProcessor.Add(
                _termsIterator.GetSpScalars()
            );
        }

        public RGaKVector<T> GetLcpKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            if (kVector2.Grade < kVector1.Grade)
                return Processor.CreateZeroScalar();

            var grade = kVector2.Grade - kVector1.Grade;

            var kVector = Processor.CreateComposer();

            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            kVector.AddTerms(
                _termsIterator.GetLcpIdScalarRecords()
            );

            return kVector.GetKVector(grade);
        }

        public RGaKVector<T> GetRcpKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            if (kVector1.Grade < kVector2.Grade)
                return Processor.CreateZeroScalar();

            var grade = kVector1.Grade - kVector2.Grade;

            var kVector = Processor.CreateComposer();

            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            kVector.AddTerms(
                _termsIterator.GetRcpIdScalarRecords()
            );

            return kVector.GetKVector(grade);
        }

        public RGaKVector<T> GetHipKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
        {
            if (kVector1.Grade < 1 || kVector2.Grade < 1)
                return Processor.CreateZeroScalar();

            var grade = Math.Abs(kVector1.Grade - kVector2.Grade);

            var kVector = Processor.CreateComposer();

            _termsIterator.Multivector1 = kVector1;
            _termsIterator.Multivector2 = kVector2;

            kVector.AddTerms(
                _termsIterator.GetRcpIdScalarRecords()
            );

            return kVector.GetKVector(grade);
        }

        public RGaKVector<T> GetFdpKVector(RGaKVector<T> kVector1, RGaKVector<T> kVector2)
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


        public IEnumerable<RGaKVector<T>> GetOpKVectors()
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

        public IEnumerable<RGaKVector<T>> GetELcpKVectors()
        {
            foreach (var kVector1 in KVectors1)
            {
                var grade1 = kVector1.Grade;

                foreach (var kVector2 in GetELcpKVectors2(grade1))
                    yield return GetELcpKVector(kVector1, kVector2);
            }
        }

        public IEnumerable<RGaKVector<T>> GetERcpKVectors()
        {
            foreach (var kVector1 in KVectors1)
            {
                var grade1 = kVector1.Grade;

                foreach (var kVector2 in GetERcpKVectors2(grade1))
                    yield return GetERcpKVector(kVector1, kVector2);
            }
        }

        public IEnumerable<RGaKVector<T>> GetEHipKVectors()
        {
            foreach (var kVector1 in KVectors1.Where(s => s.Grade > 0))
                foreach (var kVector2 in KVectors2.Where(s => s.Grade > 0))
                    yield return GetEHipKVector(kVector1, kVector2);
        }

        public IEnumerable<RGaKVector<T>> GetEFdpKVectors()
        {
            foreach (var kVector1 in KVectors1)
                foreach (var kVector2 in KVectors2)
                    yield return GetEFdpKVector(kVector1, kVector2);
        }


        public IEnumerable<RGaKVector<T>> GetLcpKVectors()
        {
            foreach (var kVector1 in KVectors1)
            {
                var grade1 = kVector1.Grade;

                foreach (var kVector2 in GetELcpKVectors2(grade1))
                    yield return GetLcpKVector(kVector1, kVector2);
            }
        }

        public IEnumerable<RGaKVector<T>> GetRcpKVectors()
        {
            foreach (var kVector1 in KVectors1)
            {
                var grade1 = kVector1.Grade;

                foreach (var kVector2 in GetERcpKVectors2(grade1))
                    yield return GetRcpKVector(kVector1, kVector2);
            }
        }

        public IEnumerable<RGaKVector<T>> GetHipKVectors()
        {
            foreach (var kVector1 in KVectors1.Where(s => s.Grade > 0))
                foreach (var kVector2 in KVectors2.Where(s => s.Grade > 0))
                    yield return GetHipKVector(kVector1, kVector2);
        }

        public IEnumerable<RGaKVector<T>> GetFdpKVectors()
        {
            foreach (var kVector1 in KVectors1)
                foreach (var kVector2 in KVectors2)
                    yield return GetFdpKVector(kVector1, kVector2);
        }
    }
}