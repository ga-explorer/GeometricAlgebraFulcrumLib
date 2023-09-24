using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public sealed class RGaFloat64ComputedOutermorphism :
    RGaFloat64OutermorphismBase
{
    public Func<ulong, RGaFloat64KVector> BasisMapFunc { get; }

    public override RGaFloat64Processor Processor { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64ComputedOutermorphism(Func<ulong, RGaFloat64KVector> basisMapFunc, RGaFloat64Processor processor)
    {
        BasisMapFunc = basisMapFunc;
        Processor = processor;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<ulong, RGaFloat64Multivector>(id, BasisMapFunc(id))
            ).Where(p => !p.Value.IsZero);
    }

    public override IRGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMapBasisVector(int index)
    {
        var id = index.BasisVectorIndexToId();

        return BasisMapFunc(id).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var id = BasisBivectorUtils.IndexPairToBivectorId(index1, index2);

        return BasisMapFunc(id).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector OmMapBasisBlade(ulong id)
    {
        return BasisMapFunc(id).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMap(RGaFloat64Vector vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in kVector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetHigherKVector(kVector.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange(
                index =>
                    new KeyValuePair<int, RGaFloat64Vector>(
                        index, 
                        OmMapBasisVector(index)
                    )
            ).Where(p => !p.Value.IsZero)
            .Select(p => 
                new KeyValuePair<ulong, RGaFloat64Vector>(
                    p.Key.BasisVectorIndexToId(),
                    p.Value
                )
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
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