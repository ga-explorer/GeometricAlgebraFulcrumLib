using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms
{
    public sealed class XGaFloat64IdentityOutermorphism : 
        IXGaFloat64Automorphism
    {
        public XGaFloat64Processor Processor { get; }

        public XGaMetric Metric
            => Processor;
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64IdentityOutermorphism(XGaFloat64Processor metric)
        {
            Processor = metric;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaFloat64Outermorphism GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector OmMapBasisVector(int index)
        {
            return Processor.CreateVector(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
        {
            return Processor.CreateBivector(
                index1, 
                index2,
                1d
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
        {
            return Processor.CreateKVector(id, 1d);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector OmMap(XGaFloat64Vector vector)
        {
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
        {
            return bivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
        {
            return kVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector OmMap(XGaFloat64KVector kVector)
        {
            return kVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
        {
            return multivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaFloat64UnilinearMap GetAdjoint()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapBasisBlade(IIndexSet id)
        {
            return OmMapBasisBlade(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
        {
            return OmMap(multivector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaFloat64Multivector>(
                        id, 
                        Processor.CreateKVector(id, 1d)
                    )
                );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return Processor
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                        id, 
                        Processor.CreateVector(id.FirstIndex)
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