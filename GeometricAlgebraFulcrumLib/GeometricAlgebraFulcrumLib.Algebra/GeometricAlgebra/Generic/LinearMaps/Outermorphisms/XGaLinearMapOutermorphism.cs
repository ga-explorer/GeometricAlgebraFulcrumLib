using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;

public sealed class XGaLinearMapOutermorphism<T> 
    : XGaOutermorphismBase<T>
{
    public override XGaProcessor<T> Processor { get; }
        
    public LinUnilinearMap<T> LinearMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaLinearMapOutermorphism(XGaProcessor<T> processor, LinUnilinearMap<T> linearMap)
    {
        Processor = processor;
        LinearMap = linearMap;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return LinearMap.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaOutermorphism<T> GetOmAdjoint()
    {
        return new XGaLinearMapOutermorphism<T>(
            Processor, 
            LinearMap.GetAdjoint()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMapBasisVector(int index)
    {
        return LinearMap.MapBasisVector(index).ToXGaVector(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 > index2)
            throw new InvalidOperationException();

        return OmMapBasisVector(index1).Op(OmMapBasisVector(index2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> OmMapBasisBlade(IndexSet id)
    {
        if (id.IsEmptySet)
            return Processor.ScalarOne;

        return id.IsUnitSet 
            ? OmMapBasisVector(id.FirstIndex)
            : id.Select(OmMapBasisVector).Op(Processor);
    }
        

    public override XGaVector<T> OmMap(XGaVector<T> vector)
    {
        var composer = Processor.CreateVectorComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddKVectorScaled(
                OmMapBasisVector(id.FirstIndex),
                scalar
            );
            
        return composer.GetVector();
    }

    public override XGaBivector<T> OmMap(XGaBivector<T> bivector)
    {
        var composer = Processor.CreateBivectorComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddKVectorScaled(
                OmMapBasisBivector(id.FirstIndex, id.LastIndex),
                scalar
            );
            
        return composer.GetBivector();
    }
        
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        var composer = Processor.CreateKVectorComposer(kVector.Grade);

        foreach (var (id, scalar) in kVector.IdScalarPairs)
            composer.AddKVectorScaled(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetHigherKVector();
    }
        
    public override XGaMultivector<T> OmMap(XGaMultivector<T> multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector.IdScalarPairs)
            composer.AddKVectorScaled(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return LinearMap
            .GetMappedBasisVectors(vSpaceDimensions)
            .Select(r => 
                new KeyValuePair<IndexSet, XGaVector<T>>(
                    r.Key.ToUnitIndexSet(), 
                    r.Value.ToXGaVector(Processor)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        return LinearMap;
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
            );
    }
}