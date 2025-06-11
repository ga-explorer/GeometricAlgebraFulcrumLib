using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public sealed class XGaFloat64IdentityOutermorphism : 
    IXGaFloat64Automorphism
{
    public XGaFloat64Processor Processor { get; }

    public XGaMetric Metric
        => Processor;
        

    
    public XGaFloat64IdentityOutermorphism(XGaFloat64Processor metric)
    {
        Processor = metric;
    }

    
    
    public IXGaFloat64Outermorphism GetOmAdjoint()
    {
        return this;
    }

    
    public XGaFloat64Vector OmMapBasisVector(int index)
    {
        return Processor.VectorTerm(index);
    }
        
    
    public XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        return Processor.BivectorTerm(
            index1, 
            index2,
            1d
        );
    }
        
    
    public XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        return Processor.KVectorTerm(id, 1d);
    }
    

    
    public XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        return vector;
    }

    
    public XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        return bivector;
    }

    
    public XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        return kVector;
    }

    
    public XGaFloat64KVector OmMap(XGaFloat64KVector kVector)
    {
        return kVector;
    }

    
    public XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        return multivector;
    }


    
    public bool IsValid()
    {
        return true;
    }

    
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
    }
        
    
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return OmMapBasisBlade(id);
    }
        
    
    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        return OmMap(multivector);
    }


    
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(
                    id, 
                    Processor.KVectorTerm(id, 1d)
                )
            );
    }
    
    
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return Processor
            .GetBasisVectorIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    id, 
                    Processor.VectorTerm(id.FirstIndex)
                )
            );
    }
        
    
    public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateIdentityLinUnilinearMap();
    }
}