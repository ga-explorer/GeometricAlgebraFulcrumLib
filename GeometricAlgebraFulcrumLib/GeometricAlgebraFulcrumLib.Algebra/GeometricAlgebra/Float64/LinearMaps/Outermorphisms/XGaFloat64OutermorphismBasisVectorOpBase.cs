using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public abstract class XGaFloat64OutermorphismBasisVectorOpBase
    : XGaFloat64OutermorphismBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        if (index1 == index2) return Processor.BivectorZero;

        var v1 = OmMapBasisVector(index1);
        if (v1.IsZero) return Processor.BivectorZero;

        var v2 = OmMapBasisVector(index2);

        return index1 < index2 ? v1.Op(v2) : v2.Op(v1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        if (id.IsEmptySet)
            return Processor.ScalarOne;

        return id.IsBasisVector() 
            ? OmMapBasisVector(id.FirstIndex)
            : id.Select(OmMapBasisVector).Op(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        var composer = Processor.CreateVectorComposer();

        foreach (var (id, scalar) in vector.IdScalarPairs)
            composer.AddKVectorScaled(
                OmMapBasisVector(id.FirstIndex),
                scalar
            );
            
        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        var composer = Processor.CreateBivectorComposer();

        foreach (var (id, scalar) in bivector.IdScalarPairs)
            composer.AddKVectorScaled(
                OmMapBasisBivector(id.FirstIndex, id.LastIndex),
                scalar
            );
            
        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        var composer = Processor.CreateKVectorComposer(kVector.Grade);

        foreach (var (id, scalar) in kVector.IdScalarPairs)
            composer.AddKVectorScaled(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        foreach (var (id, scalar) in multivector.IdScalarPairs)
            composer.AddMultivectorScaled(
                OmMapBasisBlade(id),
                scalar
            );
            
        return composer.GetSimpleMultivector();
    }
    
}