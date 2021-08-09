using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

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


        private IEnumerable<KeyValuePair<ulong, T>> GetOpIdScalarPairs2(ulong id1)
        {
            return Storage2.GetIdScalarPairs().Where(pair => GaBasisUtils.IsNonZeroOp(id1, pair.Key));
        }

        private IEnumerable<KeyValuePair<ulong, T>> GetELcpIdScalarPairs2(ulong id1)
        {
            return Storage2.GetIdScalarPairs().Where(pair => GaBasisUtils.IsNonZeroELcp(id1, pair.Key));
        }

        private IEnumerable<KeyValuePair<ulong, T>> GetERcpIdScalarPairs2(ulong id1)
        {
            return Storage2.GetIdScalarPairs().Where(pair => GaBasisUtils.IsNonZeroERcp(id1, pair.Key));
        }

        private IEnumerable<KeyValuePair<ulong, T>> GetEHipIdScalarPairs2(ulong id1)
        {
            return Storage2.GetIdScalarPairs().Where(pair => GaBasisUtils.IsNonZeroEHip(id1, pair.Key));
        }

        private IEnumerable<KeyValuePair<ulong, T>> GetEFdpIdScalarPairs2(ulong id1)
        {
            return Storage2.GetIdScalarPairs().Where(pair => GaBasisUtils.IsNonZeroEFdp(id1, pair.Key));
        }

        private IEnumerable<KeyValuePair<ulong, T>> GetECpIdScalarPairs2(ulong id1)
        {
            return Storage2.GetIdScalarPairs().Where(pair => GaBasisUtils.IsNonZeroECp(id1, pair.Key));
        }

        private IEnumerable<KeyValuePair<ulong, T>> GetEAcpIdScalarPairs2(ulong id1)
        {
            return Storage2.GetIdScalarPairs().Where(pair => GaBasisUtils.IsNonZeroEAcp(id1, pair.Key));
        }


        public IEnumerable<KeyValuePair<ulong, T>> GetOpIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetOpIdScalarPairs2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetEGpIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in Storage2.GetIdScalarPairs())
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetESpIdScalarPairs()
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarPairs())
            {
                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = GaBasisUtils.IsNegativeEGp(id)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return new KeyValuePair<ulong, T>(0, scalar);
            }
        }
        
        public IEnumerable<T> GetESpScalars()
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarPairs())
            {
                if (!Storage2.TryGetTermScalar(id, out var scalar2)) 
                    continue;

                var scalar = GaBasisUtils.IsNegativeEGp(id)
                    ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                    : ScalarProcessor.Times(scalar1, scalar2);

                yield return scalar;
            }
        }
        
        public IEnumerable<KeyValuePair<ulong, T>> GetELcpIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetELcpIdScalarPairs2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<KeyValuePair<ulong, T>> GetERcpIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetERcpIdScalarPairs2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<KeyValuePair<ulong, T>> GetEHipIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetEHipIdScalarPairs2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<KeyValuePair<ulong, T>> GetEFdpIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetEFdpIdScalarPairs2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<KeyValuePair<ulong, T>> GetECpIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetECpIdScalarPairs2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }
        
        public IEnumerable<KeyValuePair<ulong, T>> GetEAcpIdScalarPairs()
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetEAcpIdScalarPairs2(id1))
                {
                    var id = id1 ^ id2;
                    var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }


        public IEnumerable<KeyValuePair<ulong, T>> GetGpIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in Storage2.GetIdScalarPairs())
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetSpIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarPairs())
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

                yield return new KeyValuePair<ulong, T>(0, scalar);
            }
        }

        public IEnumerable<T> GetSpScalars(GaSignature basesSignature)
        {
            foreach (var (id, scalar1) in Storage1.GetIdScalarPairs())
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

        public IEnumerable<KeyValuePair<ulong, T>> GetLcpIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetELcpIdScalarPairs2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetRcpIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetERcpIdScalarPairs2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetHipIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetEHipIdScalarPairs2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetFdpIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetEFdpIdScalarPairs2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetCpIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetECpIdScalarPairs2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetAcpIdScalarPairs(GaSignature basesSignature)
        {
            foreach (var (id1, scalar1) in Storage1.GetIdScalarPairs())
            {
                foreach (var (id2, scalar2) in GetEAcpIdScalarPairs2(id1))
                {
                    var basisSignature = 
                        basesSignature.GpSignature(id1, id2);

                    if (basisSignature == 0)
                        continue;

                    var id = id1 ^ id2;
                    var scalar = basisSignature < 0
                        ? ScalarProcessor.NegativeTimes(scalar1, scalar2)
                        : ScalarProcessor.Times(scalar1, scalar2);

                    yield return new KeyValuePair<ulong, T>(id, scalar);
                }    
            }
        }
    }
}