using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms
{
    public sealed class RGaFloat64IdentityOutermorphism : 
        IRGaFloat64Automorphism
    {
        public RGaFloat64Processor Processor { get; }

        public RGaMetric Metric 
            => Processor;
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64IdentityOutermorphism(RGaFloat64Processor metric)
        {
            Processor = metric;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaFloat64Outermorphism GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector OmMapBasisVector(int index)
        {
            return Processor.CreateVector(
                
                index
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
        {
            return Processor.CreateBivector(
                
                index1, 
                index2,
                1d
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector OmMapBasisBlade(ulong id)
        {
            return Processor.CreateKVector(
                
                id, 
                1d
            );
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector OmMap(RGaFloat64Vector vector)
        {
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
        {
            return bivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
        {
            return kVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector OmMap(RGaFloat64KVector kVector)
        {
            return kVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
        {
            return multivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaFloat64UnilinearMap GetAdjoint()
        {
            return this;
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaFloat64Multivector>(
                        id, 
                        Processor.CreateKVector(id, 1d)
                    )
                );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return Processor
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaFloat64Vector>(
                        id, 
                        Processor.CreateVector(id.FirstOneBitPosition())
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            return vSpaceDimensions.CreateIdentityLinUnilinearMap();
        }
    }
}