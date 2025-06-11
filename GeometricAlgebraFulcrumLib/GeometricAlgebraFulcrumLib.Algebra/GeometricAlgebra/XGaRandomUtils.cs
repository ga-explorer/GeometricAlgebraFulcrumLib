using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;

public static class XGaRandomUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet GetBasisVectorId(this Random randomGenerator, int vSpaceDimensions)
    {
        return randomGenerator.GetInt32(0, vSpaceDimensions).ToUnitIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet[] GetDistinctBasisVectorIDs(this Random randomGenerator, int vSpaceDimensions, int count)
    {
        return randomGenerator
            .GetDistinctIndices(count, vSpaceDimensions)
            .Select(i => i.ToUnitIndexSet())
            .ToArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> GetXGaVector<T>(this Random randomGenerator, XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var sp = processor.ScalarProcessor;
        var count = randomGenerator.GetInt32(0, vSpaceDimensions);

        return processor.Vector(
            randomGenerator
                .GetDistinctBasisVectorIDs(vSpaceDimensions, count)
                .ToDictionary(
                    id => id, 
                    _ => sp.ScalarFromRandom(randomGenerator, -1, 1).ScalarValue
                )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector GetXGaBivectorBlade(this Random randomGenerator, XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var v1 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v2 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);

        return v1.Op(v2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> GetXGaBivectorBlade<T>(this Random randomGenerator, XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var v1 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v2 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);

        return v1.Op(v2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector GetXGaTrivectorBlade(this Random randomGenerator, XGaFloat64Processor processor, int vSpaceDimensions)
    {
        var v1 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v2 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v3 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);

        return v1.Op(v2).Op(v3);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> GetXGaTrivectorBlade<T>(this Random randomGenerator, XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var v1 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v2 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);
        var v3 = randomGenerator.GetXGaVector(processor, vSpaceDimensions);

        return v1.Op(v2).Op(v3);
    }
}