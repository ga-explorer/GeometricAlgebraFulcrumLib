using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Projectors
{
    public sealed class XGaFloat64Projector :
        XGaFloat64OutermorphismBase,
        IXGaFloat64Projector
    {
        public override XGaFloat64Processor Processor 
            => Blade.Processor;
        
        public XGaFloat64KVector Blade { get; }

        public XGaFloat64KVector BladePseudoInverse { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Projector(XGaFloat64KVector blade)
        {
            Blade = blade;
            BladePseudoInverse = blade.PseudoInverse();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaFloat64Outermorphism GetOmAdjoint()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector OmMapBasisVector(int index)
        {
            var id = index.IndexToIndexSet();

            return OmMapBasisBlade(id).GetVectorPart();

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
        {
            var id = IndexSetUtils.IndexPairToIndexSet(index1, index2);

            return OmMapBasisBlade(id).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
        {
            return OmMap(
                Processor.CreateKVector(id, 1d)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
        {
            return vector.Lcp(BladePseudoInverse).Lcp(Blade).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
        {
            return bivector.Lcp(BladePseudoInverse).Lcp(Blade).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade).GetHigherKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector OmMap(XGaFloat64KVector kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
        {
            return multivector.Lcp(BladePseudoInverse).Lcp(Blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            return vSpaceDimensions.CreateLinUnilinearMap(
                index => 
                    OmMapBasisVector(index).VectorToLinVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return Processor
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                        id,
                        OmMap(Processor.CreateVector(id))
                    )
                ).Where(r => !r.Value.IsZero);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(
            int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaFloat64Multivector>(
                        id,
                        OmMapBasisBlade(id)
                    )
                ).Where(r => !r.Value.IsZero);
        }
    }
}