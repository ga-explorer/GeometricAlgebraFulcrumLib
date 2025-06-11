using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public abstract class XGaFloat64OutermorphismBase :
    IXGaFloat64Outermorphism
{
    public abstract XGaFloat64Processor Processor { get; }

    public XGaMetric Metric 
        => Processor;
        

    public abstract bool IsValid();

    
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return GetOmAdjoint();
    }
        
    
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return OmMapBasisBlade(id);
    }
        
    
    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        return OmMap(multivector);
    }
        
    public abstract IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions);
        
    public abstract IXGaFloat64Outermorphism GetOmAdjoint();
        
    public abstract XGaFloat64Vector OmMapBasisVector(int index);
        
    public abstract XGaFloat64Bivector OmMapBasisBivector(int index1, int index2);
        
    public abstract XGaFloat64KVector OmMapBasisBlade(IndexSet id);
        
    public abstract XGaFloat64Vector OmMap(XGaFloat64Vector vector);
        
    public abstract XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector);
        
    public abstract XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector);
        
    
    public XGaFloat64KVector OmMap(XGaFloat64KVector kVector)
    {
        return kVector switch
        {
            XGaFloat64Scalar s => s,
            XGaFloat64Vector v => OmMap(v),
            XGaFloat64Bivector bv => OmMap(bv),
            XGaFloat64HigherKVector kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }

    public abstract XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector);

    
    public virtual IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange(index => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    index.ToUnitIndexSet(), 
                    OmMapBasisVector(index)
                )
            );
    }

    
    public virtual LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary = vSpaceDimensions.GetRange(
            index =>
                new KeyValuePair<int, XGaFloat64Vector>(
                    index, 
                    OmMapBasisVector(index)
                )
            ).Where(p => !p.Value.IsZero)
        .ToDictionary(
            p => p.Key,
            p => p.Value.ToLinVector()
        );

        return indexVectorDictionary.ToLinUnilinearMap();
    }
}