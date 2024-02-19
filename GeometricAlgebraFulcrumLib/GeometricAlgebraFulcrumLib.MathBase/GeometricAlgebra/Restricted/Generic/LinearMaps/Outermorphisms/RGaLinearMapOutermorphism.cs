using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

public sealed class RGaLinearMapOutermorphism<T> 
    : RGaOutermorphismBase<T>
{
    public override RGaProcessor<T> Processor { get; }
        
    public LinUnilinearMap<T> LinearMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaLinearMapOutermorphism(RGaProcessor<T> processor, LinUnilinearMap<T> linearMap)
    {
        Processor = processor;
        LinearMap = linearMap;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return LinearMap.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaOutermorphism<T> GetOmAdjoint()
    {
        return new RGaLinearMapOutermorphism<T>(
            Processor, 
            LinearMap.GetAdjoint()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaVector<T> OmMapBasisVector(int index)
    {
        return LinearMap.MapBasisVector(index).ToRGaVector(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        if (index1 < 0 || index1 > index2)
            throw new InvalidOperationException();

        return OmMapBasisVector(index1).Op(OmMapBasisVector(index2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaKVector<T> OmMapBasisBlade(ulong id)
    {
        if (id == 0UL)
            return Processor.CreateOneScalar();

        return id.IsBasisVector() 
            ? OmMapBasisVector(id.FirstOneBitPosition())
            : id.PatternToPositions().Select(OmMapBasisVector).Op();
    }
        

    public override RGaVector<T> OmMap(RGaVector<T> vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisVector(id.FirstOneBitPosition()),
                scalar
            );
            
        return composer.GetVector();
    }

    public override RGaBivector<T> OmMap(RGaBivector<T> bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBivector(id.FirstOneBitPosition(), id.LastOneBitPosition()),
                scalar
            );
            
        return composer.GetBivector();
    }
        
    public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in kVector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetHigherKVector(kVector.Grade);
    }
        
    public override RGaMultivector<T> OmMap(RGaMultivector<T> multivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in multivector.IdScalarPairs)
            composer.AddMultivector(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<RGaIdVectorRecord<T>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return LinearMap
            .GetMappedBasisVectors(vSpaceDimensions)
            .Select(r => 
                new RGaIdVectorRecord<T>(
                    r.Key.BasisVectorIndexToId(), 
                    r.Value.ToRGaVector(Processor)
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        return LinearMap.GetSubMap(vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaMultivector<T>>(
                    id, 
                    OmMapBasisBlade(id)
                )
            );
    }
}