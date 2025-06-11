using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Projectors;

public sealed class XGaProjector<T> :
    XGaOutermorphismBase<T>,
    IXGaProjector<T>
{
    public override XGaProcessor<T> Processor 
        => Blade.Processor;
        
    public XGaKVector<T> Blade { get; }

    public XGaKVector<T> BladePseudoInverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaProjector(XGaKVector<T> blade, bool useBladeInverse)
    {
        Blade = blade;
        BladePseudoInverse = useBladeInverse ? blade.PseudoInverse() : blade;
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
        var id = index.ToUnitIndexSet();

        return OmMapBasisBlade(id).GetVectorPart();

    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        var id = IndexSet.CreatePair(index1, index2);

        return OmMapBasisBlade(id).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> OmMapBasisBlade(IndexSet id)
    {
        return OmMap(
            Processor.KVectorTerm(
                id, 
                ScalarProcessor.OneValue
            )
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMap(XGaVector<T> vector)
    {
        return vector.Fdp(BladePseudoInverse).Gp(Blade).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> bivector)
    {
        return bivector.Fdp(BladePseudoInverse).Gp(Blade).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        return kVector.Fdp(BladePseudoInverse).Gp(Blade).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> multivector)
    {
        return multivector.Fdp(BladePseudoInverse).Gp(Blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return Processor
            .GetBasisVectorIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaVector<T>>(
                    id,
                    OmMap(
                        Processor.VectorTerm(id, ScalarProcessor.OneValue)
                    )
                )
            ).Where(r => !r.Value.IsZero);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(
        int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(
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