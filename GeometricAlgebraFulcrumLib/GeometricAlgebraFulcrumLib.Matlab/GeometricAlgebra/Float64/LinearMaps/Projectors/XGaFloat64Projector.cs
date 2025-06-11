using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Projectors;

public sealed class XGaFloat64Projector :
    XGaFloat64OutermorphismBase,
    IXGaFloat64Projector
{
    public override XGaFloat64Processor Processor 
        => Blade.Processor;
        
    public XGaFloat64KVector Blade { get; }

    public XGaFloat64KVector BladePseudoInverse { get; }


    
    internal XGaFloat64Projector(XGaFloat64KVector blade, bool useBladeInverse)
    {
        Blade = blade;
        BladePseudoInverse = useBladeInverse ? blade.PseudoInverse() : blade;
    }


    
    public override bool IsValid()
    {
        return true;
    }
        
    
    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        var id = index.ToUnitIndexSet();

        return OmMapBasisBlade(id).GetVectorPart();

    }
        
    
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var id = IndexSet.CreatePair(index1, index2);

        return OmMapBasisBlade(id).GetBivectorPart();
    }

    
    public override XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        return OmMap(
            Processor.KVectorTerm(id, 1d)
        );
    }
        
    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        return vector.Fdp(BladePseudoInverse).Gp(Blade).GetVectorPart();
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        return bivector.Fdp(BladePseudoInverse).Gp(Blade).GetBivectorPart();
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        return kVector.Fdp(BladePseudoInverse).Gp(Blade).GetHigherKVectorPart(kVector.Grade);
    }
        
    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        return multivector.Fdp(BladePseudoInverse).Gp(Blade);
    }
        
    
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateLinUnilinearMap(
            index => 
                OmMapBasisVector(index).ToLinVector()
        );
    }

    
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return Processor
            .GetBasisVectorIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    id,
                    OmMap(Processor.VectorTerm(id))
                )
            ).Where(r => !r.Value.IsZero);
    }
        
    
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(
        int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(
                    id,
                    OmMapBasisBlade(id)
                )
            ).Where(r => !r.Value.IsZero);
    }
}