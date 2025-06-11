using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Spaces.Conformal;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using XGaFloat64KVector = GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors.XGaFloat64KVector;
using XGaFloat64Vector = GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors.XGaFloat64Vector;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

/// <summary>
/// https://en.wikipedia.org/wiki/Conformal_geometric_algebra
/// </summary>
public class XGaFloat64ConformalProcessor :
    XGaFloat64Processor
{
    public static XGaFloat64ConformalProcessor Instance { get; }
        = new XGaFloat64ConformalProcessor();


    public XGaFloat64Vector En { get; }

    public XGaFloat64Vector Ep { get; }
    
    public XGaFloat64Vector Eo { get; }

    public XGaFloat64Vector Ei { get; }
    
    public XGaFloat64MusicalAutomorphism MusicalAutomorphism 
        => XGaFloat64MusicalAutomorphism.Instance;
    

    private XGaFloat64ConformalProcessor()
        : base(1, 0)
    {
        En = CreateVectorComposer()
            .SetVectorTerm(0, 1)
            .GetVector();
        
        Ep = CreateVectorComposer()
            .SetVectorTerm(1, 1)
            .GetVector();

        Eo = CreateVectorComposer()
            .SetVectorTerm(0, 0.5d)
            .SetVectorTerm(1, 0.5d)
            .GetVector();

        Ei = CreateVectorComposer()
            .SetVectorTerm(0, 1d)
            .SetVectorTerm(1, -1d)
            .GetVector();
    }


    
    public bool IsValidHGaPoint(XGaFloat64Vector hgaPoint)
    {
        var sn = hgaPoint[0];
        var sp = hgaPoint[1];

        return sn.IsNearEqual(sp);
    }
    
    
    public bool IsValidPGaPoint(XGaFloat64KVector pgaPoint, int vSpaceDimensions)
    {
        var hgaPoint = 
            PGaDual(pgaPoint, vSpaceDimensions);

        return hgaPoint.Grade == 1 && 
               IsValidHGaPoint(
                   hgaPoint.GetVectorPart()
                );
    }

    
    public bool IsValidIpnsPoint(XGaFloat64Vector ipnsPoint)
    {
        var sn = ipnsPoint[0];
        var sp = ipnsPoint[1];

        return sn.IsNearEqual(sp);
    }

    
    
    public XGaFloat64KVector PGaDual(XGaFloat64KVector mv, int vSpaceDimensions)
    {
        //Debug.Assert(
        //    IsValidPGaElement(mv)
        //);

        var icInv = 
            PseudoScalarInverse(vSpaceDimensions);

        return MusicalAutomorphism.OmMap(
            mv.Op(Ei).Lcp(icInv)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    
    public XGaFloat64Multivector PGaDual(XGaFloat64Multivector mv, int vSpaceDimensions)
    {
        //Debug.Assert(
        //    IsValidPGaElement(mv)
        //);

        var icInv = 
            PseudoScalarInverse(vSpaceDimensions);

        return MusicalAutomorphism.OmMap(
            mv.Op(Ei).Lcp(icInv)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }


    
    public XGaFloat64Vector EncodeEGaVector(double x, double y)
    {
        return CreateVectorComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }
    
    
    public XGaFloat64Vector EncodeEGaVector(double x, double y, double z)
    {
        return CreateVectorComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }
    
    
    public XGaFloat64Vector EncodeEGaVector(LinFloat64Vector2D egaVector)
    {
        return CreateVectorComposer()
            .SetVectorTerm(2, egaVector.X)
            .SetVectorTerm(3, egaVector.Y)
            .GetVector();
    }
    
    
    public XGaFloat64Vector EncodeEGaVector(LinFloat64Vector3D egaVector)
    {
        return CreateVectorComposer()
            .SetVectorTerm(2, egaVector.X)
            .SetVectorTerm(3, egaVector.Y)
            .SetVectorTerm(4, egaVector.Z)
            .GetVector();
    }


    
    public XGaFloat64Vector EncodeHGaPoint(double x, double y)
    {
        return CreateVectorComposer()
            .SetVectorTerm(0, 0.5)
            .SetVectorTerm(1, 0.5)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }
    
    
    public XGaFloat64Vector EncodeHGaPoint(double x, double y, double z)
    {
        return CreateVectorComposer()
            .SetVectorTerm(0, 0.5)
            .SetVectorTerm(1, 0.5)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }
    
    
    public XGaFloat64Vector EncodeHGaPoint(LinFloat64Vector2D egaVector)
    {
        return CreateVectorComposer()
            .SetVectorTerm(0, 0.5)
            .SetVectorTerm(1, 0.5)
            .SetVectorTerm(2, egaVector.X)
            .SetVectorTerm(3, egaVector.Y)
            .GetVector();
    }

    
    public XGaFloat64Vector EncodeHGaPoint(LinFloat64Vector3D egaVector)
    {
        return CreateVectorComposer()
            .SetVectorTerm(0, 0.5)
            .SetVectorTerm(1, 0.5)
            .SetVectorTerm(2, egaVector.X)
            .SetVectorTerm(3, egaVector.Y)
            .SetVectorTerm(4, egaVector.Z)
            .GetVector();
    }


    
    public XGaFloat64KVector EncodePGaPoint(double x, double y)
    {
        return PGaDual(
            EncodeHGaPoint(x, y), 
            4
        );
    }
    
    
    public XGaFloat64KVector EncodePGaPoint(double x, double y, double z)
    {
        return PGaDual(
            EncodeHGaPoint(x, y, z), 
            5
        );
    }

    
    public XGaFloat64KVector EncodePGaPoint(LinFloat64Vector2D egaPoint)
    {
        return PGaDual(
            EncodeHGaPoint(egaPoint), 
            4
        );
    }
    
    
    public XGaFloat64KVector EncodePGaPoint(LinFloat64Vector3D egaPoint)
    {
        return PGaDual(
            EncodeHGaPoint(egaPoint), 
            5
        );
    }

    
    
    public XGaFloat64Vector EncodeIpnsPoint(double x, double y)
    {
        var x2 = x * x + y * y;
        var sn = 0.5 * (1 + x2);
        var sp = 0.5 * (1 - x2);

        return CreateVectorComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }

    
    public XGaFloat64Vector EncodeIpnsPoint(double x, double y, double z)
    {
        var x2 = x * x + y * y + z * z;
        var sn = 0.5 * (1 + x2);
        var sp = 0.5 * (1 - x2);

        return CreateVectorComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }
    
    
    public XGaFloat64Vector EncodeIpnsPoint(LinFloat64Vector2D egaPoint)
    {
        var x2 = egaPoint.VectorENormSquared();
        var sn = 0.5 * (1 + x2);
        var sp = 0.5 * (1 - x2);

        return CreateVectorComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, egaPoint.X)
            .SetVectorTerm(3, egaPoint.Y)
            .GetVector();
    }

    
    public XGaFloat64Vector EncodeIpnsPoint(LinFloat64Vector3D egaPoint)
    {
        var x2 = egaPoint.VectorENormSquared();
        var sn = 0.5 * (1 + x2);
        var sp = 0.5 * (1 - x2);
        
        return CreateVectorComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, egaPoint.X)
            .SetVectorTerm(3, egaPoint.Y)
            .SetVectorTerm(4, egaPoint.Z)
            .GetVector();
    }

    
    
    public LinFloat64Vector2D DecodeEGaVectorAsVector2D(XGaFloat64Vector egaVector)
    {
        return LinFloat64Vector2D.Create(
            egaVector[2],
            egaVector[3]
        );
    }
    
    
    public LinFloat64Vector2D DecodeEGaVectorAsVector2D(XGaFloat64Vector egaVector, double scalingFactor)
    {
        return LinFloat64Vector2D.Create(
            egaVector[2] * scalingFactor,
            egaVector[3] * scalingFactor
        );
    }

    
    public LinFloat64Vector3D DecodeEGaVectorAsVector3D(XGaFloat64Vector egaVector)
    {
        return LinFloat64Vector3D.Create(
            egaVector[2],
            egaVector[3],
            egaVector[4]
        );
    }
    
    
    public LinFloat64Vector3D DecodeEGaVectorAsVector3D(XGaFloat64Vector egaVector, double scalingFactor)
    {
        return LinFloat64Vector3D.Create(
            egaVector[2] * scalingFactor,
            egaVector[3] * scalingFactor,
            egaVector[4] * scalingFactor
        );
    }
    
    public LinFloat64Vector DecodeEGaVectorAsVector(XGaFloat64Vector egaVector)
    {
        var composer = LinFloat64VectorComposer.Create();

        foreach (var (id, scalar) in egaVector.IdScalarTuples)
        {
            var index = id.FirstIndex;

            if (index is 0 or 1) continue;

            composer.SetTerm(
                index - 2, 
                scalar
            );
        }

        return composer.GetVector();
    }

    public LinFloat64Vector DecodeEGaVectorAsVector(XGaFloat64Vector egaVector, double scalingFactor)
    {
        var composer = LinFloat64VectorComposer.Create();

        foreach (var (id, scalar) in egaVector.IdScalarTuples)
        {
            var index = id.FirstIndex;

            if (index is 0 or 1) continue;

            composer.SetTerm(
                index - 2, 
                scalar * scalingFactor
            );
        }

        return composer.GetVector();
    }

    public XGaFloat64Vector DecodeEGaVector(XGaFloat64Vector egaVector)
    {
        var composer = XGaFloat64EuclideanProcessor.Instance.CreateVectorComposer();

        foreach (var (id, scalar) in egaVector.IdScalarTuples)
        {
            var index = id.FirstIndex;

            if (index is 0 or 1) continue;

            composer.SetTerm(
                (index - 2).ToUnitIndexSet(), 
                scalar
            );
        }

        return composer.GetVector();
    }
    
    public XGaFloat64Vector DecodeEGaVector(XGaFloat64Vector egaVector, double scalingFactor)
    {
        var composer = XGaFloat64EuclideanProcessor.Instance.CreateVectorComposer();

        foreach (var (id, scalar) in egaVector.IdScalarTuples)
        {
            var index = id.FirstIndex;

            if (index is 0 or 1) continue;

            composer.SetTerm(
            (index - 2).ToUnitIndexSet(), 
                scalar * scalingFactor
            );
        }

        return composer.GetVector();
    }

    
    
    public LinFloat64Vector2D DecodeHGaPointAsVector2D(XGaFloat64Vector hgaPoint)
    {
        Debug.Assert(IsValidHGaPoint(hgaPoint));

        return DecodeEGaVectorAsVector2D(
            hgaPoint, 
            1d / (hgaPoint[0] + hgaPoint[1])
        );
    }

    
    public LinFloat64Vector3D DecodeHGaPointAsVector3D(XGaFloat64Vector hgaPoint)
    {
        Debug.Assert(IsValidHGaPoint(hgaPoint));

        return DecodeEGaVectorAsVector3D(
            hgaPoint, 
            1d / (hgaPoint[0] + hgaPoint[1])
        );
    }
    
    
    public XGaFloat64Vector DecodeHGaPoint(XGaFloat64Vector hgaPoint)
    {
        Debug.Assert(IsValidHGaPoint(hgaPoint));

        return DecodeEGaVector(
            hgaPoint, 
            1d / (hgaPoint[0] + hgaPoint[1])
        );
    }

    
    
    public LinFloat64Vector2D DecodePGaPointAsVector2D(XGaFloat64KVector pgaPoint)
    {
        if (pgaPoint.Grade != 2)
            throw new InvalidOperationException();

        var hgaPoint = 
            PGaDual(pgaPoint, 4).GetVectorPart();

        return DecodeHGaPointAsVector2D(hgaPoint);
    }
    
    
    public LinFloat64Vector3D DecodePGaPointAsVector3D(XGaFloat64KVector pgaPoint)
    {
        if (pgaPoint.Grade != 3)
            throw new InvalidOperationException();

        var hgaPoint = 
            PGaDual(pgaPoint, 5).GetVectorPart();
        
        return DecodeHGaPointAsVector3D(hgaPoint);
    }
    
    
    public XGaFloat64Vector DecodePGaPoint(XGaFloat64KVector pgaPoint, int vSpaceDimensions)
    {
        if (pgaPoint.Grade != vSpaceDimensions - 2)
            throw new InvalidOperationException();

        var hgaPoint = 
            PGaDual(pgaPoint, vSpaceDimensions).GetVectorPart();
        
        return DecodeHGaPoint(hgaPoint);
    }

    
    
    public LinFloat64Vector2D DecodeIpnsPointAsVector2D(XGaFloat64Vector ipnsPoint)
    {
        Debug.Assert(IsValidIpnsPoint(ipnsPoint));

        return DecodeEGaVectorAsVector2D(
            ipnsPoint, 
            1d / (ipnsPoint[0] + ipnsPoint[1])
        );
    }

    
    public LinFloat64Vector3D DecodeIpnsPointAsVector3D(XGaFloat64Vector ipnsPoint)
    {
        Debug.Assert(IsValidIpnsPoint(ipnsPoint));

        return DecodeEGaVectorAsVector3D(
            ipnsPoint, 
            1d / (ipnsPoint[0] + ipnsPoint[1])
        );
    }
    
    
    public XGaFloat64Vector DecodeIpnsPoint(XGaFloat64Vector ipnsPoint)
    {
        Debug.Assert(IsValidIpnsPoint(ipnsPoint));

        return DecodeEGaVector(
            ipnsPoint, 
            1d / (ipnsPoint[0] + ipnsPoint[1])
        );
    }


    public XGaFloat64KVector PGaRp(XGaFloat64KVector mv1, XGaFloat64KVector mv2, int vSpaceDimensions)
    {
        var icInv = 
            PseudoScalarInverse(vSpaceDimensions);

        var mv1Dual = MusicalAutomorphism.OmMap(
            mv1.Op(Ei).Lcp(icInv)
        );
        
        var mv2Dual = MusicalAutomorphism.OmMap(
            mv2.Op(Ei).Lcp(icInv)
        );

        return MusicalAutomorphism.OmMap(
            mv1Dual.Op(mv2Dual).Op(Ei).Lcp(icInv)
        );
    }

    public XGaFloat64Multivector PGaRp(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2, int vSpaceDimensions)
    {
        var icInv = 
            PseudoScalarInverse(vSpaceDimensions);

        var mv1Dual = MusicalAutomorphism.OmMap(
            mv1.Op(Ei).Lcp(icInv)
        );
        
        var mv2Dual = MusicalAutomorphism.OmMap(
            mv2.Op(Ei).Lcp(icInv)
        );

        return MusicalAutomorphism.OmMap(
            (XGaFloat64KVector)mv1Dual.Op(mv2Dual).Op(Ei).Lcp(icInv)
        );
    }


    
    public XGaFloat64ConformalSpace CreateSpace(int vSpaceDimensions)
    {
        return new XGaFloat64ConformalSpace(vSpaceDimensions);
    }

}