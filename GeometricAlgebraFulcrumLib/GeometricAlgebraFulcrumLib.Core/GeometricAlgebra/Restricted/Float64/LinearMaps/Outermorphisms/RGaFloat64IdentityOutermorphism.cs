using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public sealed class RGaFloat64IdentityOutermorphism : 
    IRGaFloat64Automorphism
{
    public RGaFloat64Processor Processor { get; }

    public RGaMetric Metric 
        => Processor;
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64IdentityOutermorphism(RGaFloat64Processor metric)
    {
        Processor = metric;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64Outermorphism GetOmAdjoint()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector OmMapBasisVector(int index)
    {
        return Processor.VectorTerm(
                
            index
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        return Processor.BivectorTerm(
                
            index1, 
            index2,
            1d
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector OmMapBasisBlade(ulong id)
    {
        return Processor.KVectorTerm(
                
            id, 
            1d
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector OmMap(RGaFloat64Vector vector)
    {
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
    {
        return bivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
    {
        return kVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector OmMap(RGaFloat64KVector kVector)
    {
        return kVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
    {
        return multivector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(
                    id, 
                    Processor.KVectorTerm(id, 1d)
                )
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return Processor
            .GetBasisVectorIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaFloat64Vector>(
                    id, 
                    Processor.VectorTerm(id.FirstOneBitPosition())
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateIdentityLinUnilinearMap();
    }
}