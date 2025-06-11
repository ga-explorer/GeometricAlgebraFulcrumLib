using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

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


    
    private XGaFloat64MusicalAutomorphism()
    {
    }


    
    public override bool IsValid()
    {
        return true;
    }

    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        throw new NotImplementedException();
    }

    
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
    
    
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.RangeToDictionary(
            index => OmMapBasisVector(index).ToLinVector()
        ).ToLinUnilinearMap();
    }
    
    
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