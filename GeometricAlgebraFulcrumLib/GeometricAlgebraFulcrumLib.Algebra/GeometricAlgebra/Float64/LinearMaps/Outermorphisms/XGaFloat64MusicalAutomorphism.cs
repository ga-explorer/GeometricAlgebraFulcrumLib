using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

/// <summary>
/// See Projective Geometric Algebra as a Sub-algebra of Conformal Geometric algebra
/// https://link.springer.com/article/10.1007/s00006-021-01118-7
/// </summary>
public sealed class XGaFloat64MusicalAutomorphism :
    XGaFloat64OutermorphismBasisVectorOpBase
{
    public static XGaFloat64MusicalAutomorphism Instance { get; }
        = new XGaFloat64MusicalAutomorphism();


    public XGaFloat64ConformalProcessor ConformalProcessor 
        => XGaFloat64ConformalProcessor.Instance;

    public override XGaFloat64Processor Processor 
        => ConformalProcessor;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaFloat64MusicalAutomorphism()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }

    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        return index switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(index)),
            0 => ConformalProcessor.Vector(-1.25, 0.75),
            1 => ConformalProcessor.Vector(-0.75, 1.25),
            _ => ConformalProcessor.VectorTerm(index)
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.RangeToDictionary(
            index => OmMapBasisVector(index).ToLinVector()
        ).ToLinUnilinearMap();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        return vSpaceDimensions
            .GetRange(index => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    index.ToUnitIndexSet(), 
                    OmMapBasisVector(index)
                )
            );
    }

}