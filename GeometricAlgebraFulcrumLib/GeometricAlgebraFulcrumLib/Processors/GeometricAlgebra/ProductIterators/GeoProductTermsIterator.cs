using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.ProductIterators
{
    public sealed class MultivectorStorageTermsIterator<T> 
        : IMultivectorStorageTermsIterator<T>
    {
        public static MultivectorStorageTermsIterator<T> Create(IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new MultivectorStorageTermsIterator<T>(scalarProcessor, null, null);
        }

        public static MultivectorStorageTermsIterator<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            return new MultivectorStorageTermsIterator<T>(scalarProcessor, mv1.MultivectorStorage, mv2.MultivectorStorage);
        }

        public static MultivectorStorageTermsIterator<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return new MultivectorStorageTermsIterator<T>(scalarProcessor, mv1, mv2);
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public IMultivectorStorage<T> Storage1 { get; set; }

        public IMultivectorStorage<T> Storage2 { get; set; }


        private MultivectorStorageTermsIterator([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> storage1, IMultivectorStorage<T> storage2)
        {
            ScalarProcessor = scalarProcessor;
            Storage1 = storage1;
            Storage2 = storage2;
        }


        private IEnumerable<IndexScalarRecord<T>> GetOpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => BasisBladeProductUtils.IsNonZeroOp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetELcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => BasisBladeProductUtils.IsNonZeroELcp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetERcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => BasisBladeProductUtils.IsNonZeroERcp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetEHipIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => BasisBladeProductUtils.IsNonZeroEHip(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetEFdpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => BasisBladeProductUtils.IsNonZeroEFdp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetECpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => BasisBladeProductUtils.IsNonZeroECp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetEAcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => BasisBladeProductUtils.IsNonZeroEAcp(id1, pair.Index));
        }


        public IEnumerable<IndexScalarRecord<T>> GetOpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetOpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetEGpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in Storage2.GetIdScalarRecords())
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetESpIdScalarRecords()
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = BasisBladeProductUtils.EGpSquaredIsNegative(id)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new IndexScalarRecord<T>(0, scalar);
            }
        }
        
        public IEnumerable<T> GetESpScalars()
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = BasisBladeProductUtils.EGpSquaredIsNegative(id)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return scalar;
            }
        }
        
        public IEnumerable<IndexScalarRecord<T>> GetELcpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<IndexScalarRecord<T>> GetERcpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<IndexScalarRecord<T>> GetEHipIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<IndexScalarRecord<T>> GetEFdpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<IndexScalarRecord<T>> GetECpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<IndexScalarRecord<T>> GetEAcpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = BasisBladeProductUtils.EGpIsNegative(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }


        public IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in Storage2.GetIdScalarRecords())
                {
                    var basisSignature = 
                        basisSet.GpSign(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                var basisSignature = 
                    basisSet.GetBasisBladeSignature(id);

                if (basisSignature == 0)
                    continue;

                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = basisSignature < 0
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new IndexScalarRecord<T>(0, scalar);
            }
        }

        public IEnumerable<T> GetSpScalars(BasisBladeSet basisSet)
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                var basisSignature = 
                    basisSet.GetBasisBladeSignature(id);

                if (basisSignature == 0)
                    continue;

                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = basisSignature < 0
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return scalar;
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basisSet.GpSign(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basisSet.GpSign(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basisSet.GpSign(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basisSet.GpSign(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basisSet.GpSign(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(BasisBladeSet basisSet)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basisSet.GpSign(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }
    }
}