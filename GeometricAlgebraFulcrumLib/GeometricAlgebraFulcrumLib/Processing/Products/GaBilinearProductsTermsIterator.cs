using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.Products
{
    public sealed class GaBilinearProductsTermsIterator<T> 
        : IGaBilinearProductsTermsIterator<T>
    {
        public static GaBilinearProductsTermsIterator<T> Create()
        {
            return new(null, null);
        }

        public static GaBilinearProductsTermsIterator<T> Create(IGaMultivector<T> mv1, IGaMultivector<T> mv2)
        {
            return new(mv1.Storage, mv2.Storage);
        }

        public static GaBilinearProductsTermsIterator<T> Create(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return new(mv1, mv2);
        }


        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage1.ScalarProcessor;

        public IGaMultivectorStorage<T> Storage1 { get; set; }

        public IGaMultivectorStorage<T> Storage2 { get; set; }


        private GaBilinearProductsTermsIterator(IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2) 
        {
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