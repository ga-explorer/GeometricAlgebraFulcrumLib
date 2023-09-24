using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms
{
    public sealed class RGaIdentityOutermorphism<T> : 
        IRGaAutomorphism<T>
    {
        public RGaProcessor<T> Processor { get; }

        public RGaMetric Metric 
            => Processor;

        public IScalarProcessor<T> ScalarProcessor 
            => Processor.ScalarProcessor;
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaIdentityOutermorphism(RGaProcessor<T> processor)
        {
            Processor = processor;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaOutermorphism<T> GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> OmMapBasisVector(int index)
        {
            return Processor.CreateTermVector(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> OmMapBasisBivector(int index1, int index2)
        {
            return Processor.CreateTermBivector(
                index1, 
                index2,
                ScalarProcessor.ScalarOne
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> OmMapBasisBlade(ulong id)
        {
            return Processor.CreateTermKVector(
                id, 
                ScalarProcessor.ScalarOne
            );
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> OmMap(RGaVector<T> vector)
        {
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> OmMap(RGaBivector<T> bivector)
        {
            return bivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
        {
            return kVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> OmMap(RGaKVector<T> kVector)
        {
            return kVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> OmMap(RGaMultivector<T> multivector)
        {
            return multivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaUnilinearMap<T> GetAdjoint()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Map(RGaMultivector<T> multivector)
        {
            return OmMap(multivector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaMultivector<T>>(
                        id, 
                        Processor.CreateTermKVector(id, ScalarProcessor.ScalarOne)
                    )
                );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdVectorRecord<T>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return Processor
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => 
                    new RGaIdVectorRecord<T>(
                        id, 
                        Processor.CreateTermVector(id.FirstOneBitPosition())
                    )
                );
        }

        public LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }
    }
}