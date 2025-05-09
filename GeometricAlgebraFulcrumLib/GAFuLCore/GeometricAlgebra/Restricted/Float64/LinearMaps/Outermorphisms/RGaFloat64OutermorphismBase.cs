using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public abstract class RGaFloat64OutermorphismBase :
    IRGaFloat64Outermorphism
{
    public abstract RGaFloat64Processor Processor { get; }

    public RGaMetric Metric
        => Processor;
        

    public abstract bool IsValid();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64UnilinearMap GetAdjoint()
    {
        return GetOmAdjoint();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector MapBasisBlade(ulong id)
    {
        return OmMapBasisBlade(id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Map(RGaFloat64Multivector multivector)
    {
        return OmMap(multivector);
    }
        
    public abstract IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions);
        
    public abstract IRGaFloat64Outermorphism GetOmAdjoint();
        
    public abstract RGaFloat64Vector OmMapBasisVector(int index);
        
    public abstract RGaFloat64Bivector OmMapBasisBivector(int index1, int index2);
        
    public abstract RGaFloat64KVector OmMapBasisBlade(ulong id);
        
    public abstract RGaFloat64Vector OmMap(RGaFloat64Vector vector);
        
    public abstract RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector);
        
    public abstract RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector OmMap(RGaFloat64KVector kVector)
    {
        return kVector switch
        {
            RGaFloat64Scalar s => s,
            RGaFloat64Vector v => OmMap(v),
            RGaFloat64Bivector bv => OmMap(bv),
            RGaFloat64HigherKVector kv => OmMap(kv),
            _ => throw new InvalidOperationException()
        };
    }
        
    public abstract RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector);
        
    public abstract IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions);

    public virtual LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary = vSpaceDimensions.GetRange(
                index =>
                    new KeyValuePair<int, RGaFloat64Vector>(
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