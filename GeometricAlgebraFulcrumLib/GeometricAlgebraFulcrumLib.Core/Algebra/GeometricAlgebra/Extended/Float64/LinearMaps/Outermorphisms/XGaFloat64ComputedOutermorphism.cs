using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;

public sealed class XGaFloat64ComputedOutermorphism :
    XGaFloat64OutermorphismBase
{
    public Func<IIndexSet, XGaFloat64KVector> BasisMapFunc { get; }

    public override XGaFloat64Processor Processor { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64ComputedOutermorphism(Func<IIndexSet, XGaFloat64KVector> basisMapFunc, XGaFloat64Processor processor)
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
    public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IIndexSet, XGaFloat64Multivector>(id, BasisMapFunc(id))
            ).Where(p => !p.Value.IsZero);
    }

    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        var id = index.IndexToIndexSet();

        return BasisMapFunc(id).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var id = IndexSetUtils.IndexPairToIndexSet(index1, index2);

        return BasisMapFunc(id).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
    {
        return BasisMapFunc(id).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in vector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in bivector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in kVector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetHigherKVector(kVector.Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateComposer();

        foreach (var (id, scalar) in multivector)
            composer.AddMultivector(OmMapBasisBlade(id), scalar);

        return composer.GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions.GetRange(
                index =>
                    new KeyValuePair<int, XGaFloat64Vector>(
                        index, 
                        OmMapBasisVector(index)
                    )
            ).Where(p => !p.Value.IsZero)
            .Select(p => 
                new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                    p.Key.IndexToIndexSet(),
                    p.Value
                )
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        var indexVectorDictionary = vSpaceDimensions.GetRange(
                index =>
                    new KeyValuePair<int, XGaFloat64Vector>(
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