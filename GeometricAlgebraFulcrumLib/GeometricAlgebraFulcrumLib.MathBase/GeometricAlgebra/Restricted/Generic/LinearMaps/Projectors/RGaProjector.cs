using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Projectors;

public sealed class RGaProjector<T> :
    RGaOutermorphismBase<T>,
    IRGaProjector<T>
{
    public override RGaProcessor<T> Processor 
        => Blade.Processor;
        
    public RGaKVector<T> Blade { get; }

    public RGaKVector<T> BladePseudoInverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaProjector(RGaKVector<T> blade, bool useBladeInverse)
    {
        Blade = blade;
        BladePseudoInverse = useBladeInverse ? blade.PseudoInverse() : blade;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaOutermorphism<T> GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaVector<T> OmMapBasisVector(int index)
    {
        var id = index.BasisVectorIndexToId();

        return OmMapBasisBlade(id).GetVectorPart();

    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        var id = BasisBivectorUtils.IndexPairToBivectorId(index1, index2);

        return OmMapBasisBlade(id).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaKVector<T> OmMapBasisBlade(ulong id)
    {
        return OmMap(
            Processor.CreateTermKVector(
                id, 
                ScalarProcessor.ScalarOne
            )
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaVector<T> OmMap(RGaVector<T> vector)
    {
        return vector.Fdp(BladePseudoInverse).Gp(Blade).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> OmMap(RGaBivector<T> bivector)
    {
        return bivector.Fdp(BladePseudoInverse).Gp(Blade).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
    {
        return kVector.Fdp(BladePseudoInverse).Gp(Blade).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> OmMap(RGaMultivector<T> multivector)
    {
        return multivector.Fdp(BladePseudoInverse).Gp(Blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<RGaIdVectorRecord<T>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return Processor
            .GetBasisVectorIds(vSpaceDimensions)
            .Select(id => 
                new RGaIdVectorRecord<T>(
                    id,
                    OmMap(
                        Processor.CreateTermVector(id, ScalarProcessor.ScalarOne)
                    )
                )
            ).Where(r => !r.Vector.IsZero);
    }

    public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        throw new NotImplementedException();
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
            ).Where(r => !r.Value.IsZero);
    }
}