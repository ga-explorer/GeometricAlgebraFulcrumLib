using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators
{
    public sealed class GaProductTermsIterator<T> 
        : IGaProductTermsIterator<T>
    {
        public static GaProductTermsIterator<T> Create(IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaProductTermsIterator<T>(scalarProcessor, null, null);
        }

        public static GaProductTermsIterator<T> Create(IGaScalarProcessor<T> scalarProcessor, GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            return new GaProductTermsIterator<T>(scalarProcessor, mv1.MultivectorStorage, mv2.MultivectorStorage);
        }

        public static GaProductTermsIterator<T> Create(IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return new GaProductTermsIterator<T>(scalarProcessor, mv1, mv2);
        }


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGaStorageMultivector<T> Storage1 { get; set; }

        public IGaStorageMultivector<T> Storage2 { get; set; }


        private GaProductTermsIterator([NotNull] IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> storage1, IGaStorageMultivector<T> storage2)
        {
            ScalarProcessor = scalarProcessor;
            Storage1 = storage1;
            Storage2 = storage2;
        }


        private IEnumerable<GaRecordKeyValue<T>> GetOpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroOp(id1, pair.Key));
        }

        private IEnumerable<GaRecordKeyValue<T>> GetELcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroELcp(id1, pair.Key));
        }

        private IEnumerable<GaRecordKeyValue<T>> GetERcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroERcp(id1, pair.Key));
        }

        private IEnumerable<GaRecordKeyValue<T>> GetEHipIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroEHip(id1, pair.Key));
        }

        private IEnumerable<GaRecordKeyValue<T>> GetEFdpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroEFdp(id1, pair.Key));
        }

        private IEnumerable<GaRecordKeyValue<T>> GetECpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroECp(id1, pair.Key));
        }

        private IEnumerable<GaRecordKeyValue<T>> GetEAcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroEAcp(id1, pair.Key));
        }


        public IEnumerable<GaRecordKeyValue<T>> GetOpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetOpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetEGpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in Storage2.GetIdScalarRecords())
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetESpIdScalarRecords()
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new GaRecordKeyValue<T>(0, scalar);
            }
        }
        
        public IEnumerable<T> GetESpScalars()
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return scalar;
            }
        }
        
        public IEnumerable<GaRecordKeyValue<T>> GetELcpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<GaRecordKeyValue<T>> GetERcpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<GaRecordKeyValue<T>> GetEHipIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<GaRecordKeyValue<T>> GetEFdpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<GaRecordKeyValue<T>> GetECpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<GaRecordKeyValue<T>> GetEAcpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }


        public IEnumerable<GaRecordKeyValue<T>> GetGpIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in Storage2.GetIdScalarRecords())
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetSpIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                var basisSignature = 
                    basesSignature.GetBasisBladeSignature(id);

                if (basisSignature == 0)
                    continue;

                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = basisSignature < 0
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new GaRecordKeyValue<T>(0, scalar);
            }
        }

        public IEnumerable<T> GetSpScalars(IGaSignature basesSignature)
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarRecords())
            {
                var basisSignature = 
                    basesSignature.GetBasisBladeSignature(id);

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

        public IEnumerable<GaRecordKeyValue<T>> GetLcpIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetELcpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetRcpIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetERcpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetHipIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEHipIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetFdpIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEFdpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetCpIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetECpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<GaRecordKeyValue<T>> GetAcpIdScalarRecords(IGaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetEAcpIdScalarRecords2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new GaRecordKeyValue<T>(id, scalar);
                }    
            }
        }
    }
}