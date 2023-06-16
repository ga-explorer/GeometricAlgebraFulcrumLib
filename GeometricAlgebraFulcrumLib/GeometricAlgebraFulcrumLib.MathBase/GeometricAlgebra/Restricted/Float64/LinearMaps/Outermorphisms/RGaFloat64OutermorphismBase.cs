using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms
{
    public abstract class RGaFloat64OutermorphismBase :
        IRGaFloat64Outermorphism
    {
        public abstract RGaFloat64Processor Processor { get; }

        public RGaMetric Metric
            => Processor;
        

        public abstract bool IsValid();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaFloat64UnilinearMap GetAdjoint()
        {
            return GetOmAdjoint();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector Map(RGaFloat64Multivector multivector)
        {
            return OmMap(multivector);
        }
        
        public abstract IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions);
        
        public abstract IRGaFloat64Outermorphism GetOmAdjoint();
        
        public abstract RGaFloat64Vector OmMapBasisVector(int index);
        
        public abstract RGaFloat64Bivector OmMapBasisBivector(int index1, int index2);
        
        public abstract RGaFloat64KVector OmMapBasisBlade(ulong id);
        
        public abstract RGaFloat64Vector OmMap(RGaFloat64Vector vector);
        
        public abstract RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector);
        
        public abstract RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector);
        
        public abstract RGaFloat64KVector OmMap(RGaFloat64KVector kVector);
        
        public abstract RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector);
        
        public abstract IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions);
        
        public abstract LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions);
    }
}