using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public sealed class XGaFloat64LinearMapOutermorphism 
    : XGaFloat64OutermorphismBasisVectorOpBase
{
    public override XGaFloat64Processor Processor { get; }
        
    public LinFloat64UnilinearMap LinearMap { get; }


    internal XGaFloat64LinearMapOutermorphism(XGaFloat64Processor metric, LinFloat64UnilinearMap linearMap)
    {
        Processor = metric;
        LinearMap = linearMap;
    }

        
    
    public override bool IsValid()
    {
        return LinearMap.IsValid();
    }

    
    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        return new XGaFloat64LinearMapOutermorphism(
            Processor, 
            LinearMap.GetAdjoint()
        );
    }

    
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        return LinearMap.MapBasisVector(index).ToXGaFloat64Vector(Processor);
    }
        
    //
    //public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    //{
    //    if (index1 < 0 || index1 > index2)
    //        throw new InvalidOperationException();

    //    return OmMapBasisVector(index1).Op(OmMapBasisVector(index2));
    //}

    //
    //public override XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    //{
    //    if (id.IsEmptySet)
    //        return Processor.ScalarOne;

    //    return id.IsUnitSet 
    //        ? OmMapBasisVector(id.FirstIndex)
    //        : id.Select(OmMapBasisVector).Op(Processor);
    //}
        

    //public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    //{
    //    var composer = Processor.CreateVectorComposer();

    //    foreach (var (id, scalar) in vector.IdScalarPairs)
    //        composer.AddKVectorScaled(
    //            OmMapBasisVector(id.FirstIndex),
    //            scalar
    //        );
            
    //    return composer.GetVector();
    //}

    //public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    //{
    //    var composer = Processor.CreateBivectorComposer();

    //    foreach (var (id, scalar) in bivector.IdScalarPairs)
    //        composer.AddKVectorScaled(
    //            OmMapBasisBivector(id.FirstIndex, id.LastIndex),
    //            scalar
    //        );
            
    //    return composer.GetBivector();
    //}
        
    //public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    //{
    //    var composer = Processor.CreateKVectorComposer(kVector.Grade);

    //    foreach (var (id, scalar) in kVector.IdScalarPairs)
    //        composer.AddKVectorScaled(
    //            OmMapBasisBlade(id),
    //            scalar
    //        );
            
    //    return composer.GetHigherKVector();
    //}
        
    //public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    //{
    //    var composer = Processor.CreateMultivectorComposer();

    //    foreach (var (id, scalar) in multivector.IdScalarPairs)
    //        composer.AddMultivectorScaled(
    //            OmMapBasisBlade(id),
    //            scalar
    //        );
            
    //    return composer.GetMultivector();
    //}

        
    
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return LinearMap.GetSubMap(vSpaceDimensions);
    }

    //
    //public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    //{
    //    return LinearMap
    //        .GetMappedBasisVectors(vSpaceDimensions)
    //        .Select(r => 
    //            new KeyValuePair<IndexSet, XGaFloat64Vector>(
    //                r.Key.ToUnitIndexSet(), 
    //                r.Value.ToXGaFloat64Vector(Processor)
    //            )
    //        );
    //}

    //
    //public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    //{
    //    return Processor
    //        .GetBasisBladeIds(vSpaceDimensions)
    //        .Select(id => 
    //            new KeyValuePair<IndexSet, XGaFloat64Multivector>(
    //                id, 
    //                OmMapBasisBlade(id)
    //            )
    //        );
    //}
}