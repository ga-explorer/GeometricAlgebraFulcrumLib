using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra;

public static class XGaRandomUtils
{
    
    public static IndexSet GetBasisVectorId(this Random randomGenerator, int vSpaceDimensions)
    {
        return randomGenerator.GetInt32(0, vSpaceDimensions).ToUnitIndexSet();
    }

    
    public static IndexSet[] GetDistinctBasisVectorIDs(this Random randomGenerator, int vSpaceDimensions, int count)
    {
        return randomGenerator
            .GetDistinctIndices(count, vSpaceDimensions)
            .Select(i => i.ToUnitIndexSet())
            .ToArray();
    }


    
    public static XGaFloat64Vector GetXGaVector(this Random randomGenerator, XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var count = randomGenerator.GetInt32(0, vSpaceDimensions);

        return processor.Vector(
            randomGenerator
                .GetDistinctBasisVectorIDs(vSpaceDimensions, count)
                .ToDictionary(
                    id => id, 
                    _ => randomGenerator.GetFloat64()
                )
            );
    }


    
    public static XGaFloat64Bivector GetXGaBivectorBlade(this Random randomGenerator, XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var v1 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v2 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);

        return v1.Op(v2);
    }
    
    
    
    public static XGaFloat64KVector GetXGaTrivectorBlade(this Random randomGenerator, XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var v1 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v2 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v3 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);

        return v1.Op(v2).Op(v3);
    }
    
}