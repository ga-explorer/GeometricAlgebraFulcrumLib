using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms
{
    public abstract class RGaOutermorphismBase<T> :
        IRGaOutermorphism<T>
    {
        public abstract RGaProcessor<T> Processor { get; }

        public RGaMetric Metric 
            => Processor;

        public IScalarProcessor<T> ScalarProcessor 
            => Processor.ScalarProcessor;


        public abstract bool IsValid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaUnilinearMap<T> GetAdjoint()
        {
            return GetOmAdjoint();
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
        
        public abstract IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions);
        
        public abstract IRGaOutermorphism<T> GetOmAdjoint();
        
        public abstract RGaVector<T> OmMapBasisVector(int index);
        
        public abstract RGaBivector<T> OmMapBasisBivector(int index1, int index2);
        
        public abstract RGaKVector<T> OmMapBasisBlade(ulong id);
        
        public abstract RGaVector<T> OmMap(RGaVector<T> vector);
        
        public abstract RGaBivector<T> OmMap(RGaBivector<T> bivector);
        
        public abstract RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector);
        
        public abstract RGaKVector<T> OmMap(RGaKVector<T> kVector);
        
        public abstract RGaMultivector<T> OmMap(RGaMultivector<T> multivector);
        
        public abstract IEnumerable<RGaIdVectorRecord<T>> GetOmMappedBasisVectors(int vSpaceDimensions);
        
        public abstract LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions);
    }
}