using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.BasisMaps;

public static class RGaFloat64UnilinearBasisMapUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade Map(this IRGaFloat64UnilinearBasisMap basisMap, RGaBasisBlade basisBlade)
    {
        return basisMap.MapBasisBlade(basisBlade.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade Map(this IRGaFloat64UnilinearBasisMap basisMap, RGaSignedBasisBlade basisBlade)
    {
        return basisMap.MapBasisBlade(basisBlade.Id).Times(
            basisBlade.Sign.ToFloat64()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade Map(this IRGaFloat64UnilinearBasisMap basisMap, IRGaSignedBasisBlade basisBlade)
    {
        return basisMap.MapBasisBlade(basisBlade.Id).Times(
            basisBlade.Sign.ToFloat64()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade Map(this IRGaFloat64UnilinearBasisMap basisMap, RGaFloat64ScaledBasisBlade basisBlade)
    {
        return basisMap.MapBasisBlade(basisBlade.Id).Times(
            basisBlade.Scalar
        );
    }
    
    public static RGaFloat64Multivector Map(this IRGaFloat64UnilinearBasisMap basisMap, RGaFloat64Multivector multivector)
    {
        var composer = basisMap.Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mappedBasisBlade = basisMap.MapBasisBlade(id);

            if (mappedBasisBlade.IsZero)
                continue;

            composer.AddTerm(
                mappedBasisBlade.BasisBlade.Id, 
                mappedBasisBlade.Scalar, 
                scalar
            );
        }

        return composer.GetSimpleMultivector();
    }
    
    public static RGaFloat64Vector OmMap(this IRGaFloat64UnilinearBasisMap basisMap, RGaFloat64Vector multivector)
    {
        var composer = basisMap.Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mappedBasisBlade = basisMap.MapBasisBlade(id);

            if (mappedBasisBlade.IsZero)
                continue;

            composer.AddTerm(
                mappedBasisBlade.BasisBlade.Id, 
                mappedBasisBlade.Scalar, 
                scalar
            );
        }

        return composer.GetVectorPart();
    }
    
    public static RGaFloat64Bivector OmMap(this IRGaFloat64UnilinearBasisMap basisMap, RGaFloat64Bivector multivector)
    {
        var composer = basisMap.Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mappedBasisBlade = basisMap.MapBasisBlade(id);

            if (mappedBasisBlade.IsZero)
                continue;

            composer.AddTerm(
                mappedBasisBlade.BasisBlade.Id, 
                mappedBasisBlade.Scalar, 
                scalar
            );
        }

        return composer.GetBivectorPart();
    }
    
    public static RGaFloat64HigherKVector OmMap(this IRGaFloat64UnilinearBasisMap basisMap, RGaFloat64HigherKVector multivector)
    {
        var composer = basisMap.Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mappedBasisBlade = basisMap.MapBasisBlade(id);

            if (mappedBasisBlade.IsZero)
                continue;

            composer.AddTerm(
                mappedBasisBlade.BasisBlade.Id, 
                mappedBasisBlade.Scalar, 
                scalar
            );
        }

        return composer.GetHigherKVectorPart(multivector.Grade);
    }
    
    public static RGaFloat64KVector OmMap(this IRGaFloat64UnilinearBasisMap basisMap, RGaFloat64KVector multivector)
    {
        var composer = basisMap.Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
        {
            var mappedBasisBlade = basisMap.MapBasisBlade(id);

            if (mappedBasisBlade.IsZero)
                continue;

            composer.AddTerm(
                mappedBasisBlade.BasisBlade.Id, 
                mappedBasisBlade.Scalar, 
                scalar
            );
        }

        return composer.GetKVectorPart(multivector.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(this IRGaFloat64UnilinearBasisMap basisMap, int vSpaceDimensions)
    {
        var processor = basisMap.Processor;

        return basisMap
            .GetMappedBasisBlades(vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(
                    p.Key, 
                    processor.KVectorTerm(p.Value.Id, p.Value.Scalar.ScalarValue)
                )
            );
    }
    
    public static double[,] GetMultivectorMapArray(this IRGaFloat64UnilinearBasisMap basisMap, int rowCount, int colCount)
    {
        //TODO: Review this
        var vSpaceDimensions = Math.Min(rowCount, colCount);
        
        var mapArray = new double[rowCount, colCount];
        
        foreach (var (id, basisBlade) in basisMap.GetMappedBasisBlades(vSpaceDimensions))
            mapArray[id, basisBlade.Id] = basisBlade.Scalar.ScalarValue;

        return mapArray;
    }

}