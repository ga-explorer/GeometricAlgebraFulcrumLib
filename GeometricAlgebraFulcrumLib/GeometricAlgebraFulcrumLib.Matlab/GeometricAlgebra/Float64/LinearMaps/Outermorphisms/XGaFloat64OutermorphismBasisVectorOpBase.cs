using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public abstract class XGaFloat64OutermorphismBasisVectorOpBase
    : XGaFloat64OutermorphismBase
{
    
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
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
    
    
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        if (index1 == index2) return Processor.BivectorZero;

        var v1 = OmMapBasisVector(index1);
        if (v1.IsZero) return Processor.BivectorZero;

        var v2 = OmMapBasisVector(index2);

        return index1 < index2 ? v1.Op(v2) : v2.Op(v1);
    }

    
    public override XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        if (id.IsEmptySet)
            return Processor.ScalarOne;

        return id.IsBasisVector() 
            ? OmMapBasisVector(id.FirstIndex)
            : id.Select(OmMapBasisVector).Op(Processor);
    }

    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        var composer = Processor.CreateVectorComposer();

        foreach (var (id, scalar) in vector.IdScalarTuples)
            composer.AddKVectorScaled(
                OmMapBasisVector(id.FirstIndex),
                scalar
            );
            
        return composer.GetVector();
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateBivectorComposer();

        foreach (var (id, scalar) in bivector.IdScalarTuples)
            composer.AddKVectorScaled(
                OmMapBasisBivector(id.FirstIndex, id.LastIndex),
                scalar
            );
            
        return composer.GetBivector();
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateKVectorComposer(kVector.Grade);

        foreach (var (id, scalar) in kVector.IdScalarTuples)
            composer.AddKVectorScaled(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetHigherKVector();
    }

    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector.IdScalarTuples)
            composer.AddMultivectorScaled(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetMultivector();
    }
    
}