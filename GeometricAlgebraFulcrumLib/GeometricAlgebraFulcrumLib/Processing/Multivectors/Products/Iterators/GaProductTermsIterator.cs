using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Iterators
{
    public sealed class GaProductTermsIterator<T> 
        : IGaProductTermsIterator<T>
    {
        public static GaProductTermsIterator<T> Create(IScalarProcessor<T> scalarProcessor)
        {
            return new GaProductTermsIterator<T>(scalarProcessor, null, null);
        }

        public static GaProductTermsIterator<T> Create(IScalarProcessor<T> scalarProcessor, GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            return new GaProductTermsIterator<T>(scalarProcessor, mv1.MultivectorStorage, mv2.MultivectorStorage);
        }

        public static GaProductTermsIterator<T> Create(IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return new GaProductTermsIterator<T>(scalarProcessor, mv1, mv2);
        }


        public IScalarProcessor<T> ScalarProcessor { get; }

        public IGaMultivectorStorage<T> Storage1 { get; set; }

        public IGaMultivectorStorage<T> Storage2 { get; set; }


        private GaProductTermsIterator([NotNull] IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        {
            ScalarProcessor = scalarProcessor;
            Storage1 = storage1;
            Storage2 = storage2;
        }


        private IEnumerable<IndexScalarRecord<T>> GetOpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroOp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetELcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroELcp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetERcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroERcp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetEHipIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroEHip(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetEFdpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroEFdp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetECpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroECp(id1, pair.Index));
        }

        private IEnumerable<IndexScalarRecord<T>> GetEAcpIdScalarRecords2(ulong id1)
        {
            return Storage2.GetIdScalarRecords().Where(pair => GaBasisBladeProductUtils.IsNonZeroEAcp(id1, pair.Index));
        }


        public IEnumerable<IndexScalarRecord<T>> GetOpIdScalarRecords()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarRecords())
            {
                foreach (var (id2, scalar2) in GetOpIdScalarRecords2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
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
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
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

                var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id)
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

                var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id)
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
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
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
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
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
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
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
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
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
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
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
                    var scalar = GaBasisBladeProductUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }


        public IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(IGaSignature basesSignature)
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

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(IGaSignature basesSignature)
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

                yield return new IndexScalarRecord<T>(0, scalar);
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

        public IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(IGaSignature basesSignature)
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

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(IGaSignature basesSignature)
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

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(IGaSignature basesSignature)
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

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(IGaSignature basesSignature)
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

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(IGaSignature basesSignature)
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

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }

        public IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(IGaSignature basesSignature)
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

                    yield return new IndexScalarRecord<T>(id, scalar);
                }    
            }
        }
    }
}