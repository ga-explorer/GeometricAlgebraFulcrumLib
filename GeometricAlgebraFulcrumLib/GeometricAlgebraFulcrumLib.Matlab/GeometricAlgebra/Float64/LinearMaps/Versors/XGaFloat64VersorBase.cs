using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Versors;

public abstract class XGaFloat64VersorBase :
    XGaFloat64OutermorphismBase,
    IXGaFloat64Versor
{
    public override XGaFloat64Processor Processor { get; }
        

    
    protected XGaFloat64VersorBase(XGaFloat64Processor metric)
    {
        Processor = metric;
    }

        
    
    public abstract IXGaFloat64Versor GetVersorInverse();

    public abstract XGaFloat64Multivector GetMultivector();
        
    public abstract XGaFloat64Multivector GetMultivectorReverse();
        
    public abstract XGaFloat64Multivector GetMultivectorInverse();
        

    
    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        return GetVersorInverse();
    }

        
    
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        return OmMap(
            Processor.VectorTerm(index)
        );
    }
        
    
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 >= index2)
            throw new InvalidOperationException();

        return OmMap(
            Processor.BivectorTerm(
                index1, 
                index2, 
                1d
            )
        );
    }

    
    public override XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        return OmMap(
            Processor.KVectorTerm(id, 1d)
        );
    }
        
        
    
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateLinUnilinearMap(
            index => 
                OmMapBasisVector(index).ToLinVector()
        );
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
            );
    }
        
    
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(
        int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange()
            .Select(index => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    index.ToUnitIndexSet(), 
                    OmMapBasisVector(index)
                )
            );
    }


}