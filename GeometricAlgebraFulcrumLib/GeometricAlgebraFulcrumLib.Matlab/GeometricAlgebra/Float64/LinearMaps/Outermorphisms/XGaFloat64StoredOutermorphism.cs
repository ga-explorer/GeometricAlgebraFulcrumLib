using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public sealed class XGaFloat64StoredOutermorphism :
    XGaFloat64OutermorphismBase
{
    private readonly IReadOnlyDictionary<IndexSet, XGaFloat64KVector> _basisMapDictionary;

    public override XGaFloat64Processor Processor { get; }


    internal XGaFloat64StoredOutermorphism(IReadOnlyDictionary<IndexSet, XGaFloat64KVector> basisMapDictionary, XGaFloat64Processor processor)
    {
        _basisMapDictionary = basisMapDictionary;
        Processor = processor;
    }

    
    
    public override bool IsValid()
    {
        return _basisMapDictionary.Values.All(
            d => d.IsValid()
        );
    }

    
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return _basisMapDictionary
            .Where(p => 
                p.Key.VSpaceDimensions() <= vSpaceDimensions
            ).Select(p => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(p.Key, p.Value)
            );
    }

    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        var id = index.ToUnitIndexSet();

        return _basisMapDictionary.TryGetValue(id, out var kVector)
            ? kVector.GetVectorPart()
            : Processor.VectorZero;
    }

    
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var id = IndexSet.CreatePair(index1, index2);

        return _basisMapDictionary.TryGetValue(id, out var kVector)
            ? kVector.GetBivectorPart()
            : Processor.BivectorZero;
    }

    
    public override XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        return _basisMapDictionary.TryGetValue(id, out var kVector)
            ? kVector.GetVectorPart()
            : Processor.KVectorZero(id.Grade());
    }

    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        var composer = Processor.CreateVectorComposer();

        foreach (var (id, scalar) in vector.ToTuples())
            composer.AddKVectorScaled(OmMapBasisBlade(id), scalar);

        return composer.GetVector();
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateBivectorComposer();

        foreach (var (id, scalar) in bivector.ToTuples())
            composer.AddKVectorScaled(OmMapBasisBlade(id), scalar);

        return composer.GetBivector();
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateKVectorComposer(kVector.Grade);

        foreach (var (id, scalar) in kVector.ToTuples())
            composer.AddKVectorScaled(OmMapBasisBlade(id), scalar);

        return composer.GetHigherKVector();
    }
    
    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector.ToTuples())
            composer.AddMultivectorScaled(OmMapBasisBlade(id), scalar);

        return composer.GetSimpleMultivector();
    }

    
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange(
            index =>
                new KeyValuePair<int, XGaFloat64Vector>(
                    index, 
                    OmMapBasisVector(index)
                )
            ).Where(p => !p.Value.IsZero)
            .Select(p => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    p.Key.ToUnitIndexSet(),
                    p.Value
                )
            );
    }
    
}