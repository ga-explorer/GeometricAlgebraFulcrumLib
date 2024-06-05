using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;

public sealed class XGaFloat64LinearMapOutermorphism 
    : XGaFloat64OutermorphismBase
{
    public override XGaFloat64Processor Processor { get; }
        
    public LinFloat64UnilinearMap LinearMap { get; }


    internal XGaFloat64LinearMapOutermorphism(XGaFloat64Processor metric, LinFloat64UnilinearMap linearMap)
    {
        Processor = metric;
        LinearMap = linearMap;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return LinearMap.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        return new XGaFloat64LinearMapOutermorphism(
            Processor, 
            LinearMap.GetAdjoint()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        return LinearMap.MapBasisVector(index).ToXGaFloat64Vector(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 > index2)
            throw new InvalidOperationException();

        return OmMapBasisVector(index1).Op(OmMapBasisVector(index2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
    {
        if (id.IsEmptySet)
            return Processor.ScalarOne;

        return id.IsSingleIndexSet 
            ? OmMapBasisVector(id.FirstIndex)
            : id.Select(OmMapBasisVector).Op(Processor);
    }
        

    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisVector(id.FirstIndex),
                scalar
            );
            
        return composer.GetVector();
    }

    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBivector(id.FirstIndex, id.LastIndex),
                scalar
            );
            
        return composer.GetBivector();
    }
        
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in kVector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetHigherKVector(kVector.Grade);
    }
        
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in multivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return LinearMap.GetSubMap(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(
        int vSpaceDimensions)
    {
        return LinearMap
            .GetMappedBasisVectors(vSpaceDimensions)
            .Select(r => 
                new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                    r.Key.IndexToIndexSet(), 
                    r.Value.ToXGaFloat64Vector(Processor)
                )
            );
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
            );
    }
}