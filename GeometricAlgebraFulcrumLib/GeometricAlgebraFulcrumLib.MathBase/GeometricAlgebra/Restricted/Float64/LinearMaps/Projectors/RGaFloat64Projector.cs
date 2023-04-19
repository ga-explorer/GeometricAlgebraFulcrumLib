using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Projectors
{
    public sealed class RGaFloat64Projector :
        RGaFloat64OutermorphismBase,
        IRGaFloat64Projector
    {
        public override RGaFloat64Processor Processor 
            => Blade.Processor;
        
        public RGaFloat64KVector Blade { get; }

        public RGaFloat64KVector BladePseudoInverse { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64Projector(RGaFloat64KVector blade)
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
        public override IRGaFloat64Outermorphism GetOmAdjoint()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMapBasisVector(int index)
        {
            var id = index.BasisVectorIndexToId();

            return OmMapBasisBlade(id).GetVectorPart();

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
        {
            var id = BasisBivectorUtils.IndexPairToBivectorId(index1, index2);

            return OmMapBasisBlade(id).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMapBasisBlade(ulong id)
        {
            return OmMap(
                Processor.CreateKVector(id, 1d)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector vector)
        {
            return vector.Lcp(BladePseudoInverse).Lcp(Blade).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
        {
            return bivector.Lcp(BladePseudoInverse).Lcp(Blade).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade).GetHigherKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMap(RGaFloat64KVector kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
        {
            return multivector.Lcp(BladePseudoInverse).Lcp(Blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return Processor
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaFloat64Vector>(
                        id,
                        OmMap(
                            Processor.CreateVector(id, 1d)
                        )
                    )
                ).Where(r => !r.Value.IsZero);
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaFloat64Multivector>(
                        id,
                        OmMapBasisBlade(id)
                    )
                ).Where(r => !r.Value.IsZero);
        }
    }
}