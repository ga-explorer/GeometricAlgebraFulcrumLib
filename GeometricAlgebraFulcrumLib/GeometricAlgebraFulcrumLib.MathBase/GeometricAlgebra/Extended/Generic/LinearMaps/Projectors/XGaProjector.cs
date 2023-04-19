using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Projectors
{
    public sealed class XGaProjector<T> :
        XGaOutermorphismBase<T>,
        IXGaProjector<T>
    {
        public override XGaProcessor<T> Processor 
            => Blade.Processor;
        
        public XGaKVector<T> Blade { get; }

        public XGaKVector<T> BladePseudoInverse { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaProjector(XGaKVector<T> blade)
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
        public override IXGaOutermorphism<T> GetOmAdjoint()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMapBasisVector(int index)
        {
            var id = index.IndexToIndexSet();

            return OmMapBasisBlade(id).GetVectorPart();

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMapBasisBivector(int index1, int index2)
        {
            var id = IndexSetUtils.IndexPairToIndexSet(index1, index2);

            return OmMapBasisBlade(id).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> OmMapBasisBlade(IIndexSet id)
        {
            return OmMap(
                Processor.CreateKVector(
                    id, 
                    ScalarProcessor.ScalarOne
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMap(XGaVector<T> vector)
        {
            return vector.Lcp(BladePseudoInverse).Lcp(Blade).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMap(XGaBivector<T> bivector)
        {
            return bivector.Lcp(BladePseudoInverse).Lcp(Blade).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade).GetHigherKVectorPart(kVector.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> OmMap(XGaKVector<T> kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> OmMap(XGaMultivector<T> multivector)
        {
            return multivector.Lcp(BladePseudoInverse).Lcp(Blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<IIndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return Processor
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaVector<T>>(
                        id,
                        OmMap(
                            Processor.CreateVector(id, ScalarProcessor.ScalarOne)
                        )
                    )
                ).Where(r => !r.Value.IsZero);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(
            int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaMultivector<T>>(
                        id,
                        OmMapBasisBlade(id)
                    )
                ).Where(r => !r.Value.IsZero);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            return ScalarProcessor.CreateLinUnilinearMap(
                vSpaceDimensions,
                index => OmMapBasisVector(index).ToLinVector()
            );
        }

    }
}