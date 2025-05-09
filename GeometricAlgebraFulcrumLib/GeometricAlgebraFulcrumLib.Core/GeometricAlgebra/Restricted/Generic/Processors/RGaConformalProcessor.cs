using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
//using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;

/// <summary>
/// https://en.wikipedia.org/wiki/Conformal_geometric_algebra
/// </summary>
/// <typeparam name="T"></typeparam>
public class RGaConformalProcessor<T> :
    RGaProcessor<T>
{
    public RGaVector<T> En { get; }

    public RGaVector<T> Ep { get; }

    public RGaVector<T> Eo { get; }

    public RGaVector<T> Ei { get; }

    public RGaMusicalAutomorphism<T> MusicalAutomorphism { get; }


    internal RGaConformalProcessor(IScalarProcessor<T> scalarProcessor)
        : base(scalarProcessor, 1, 0)
    {
        var scalarOne = ScalarProcessor.OneValue;
        var scalarMinusOne = ScalarProcessor.MinusOneValue;
        var scalarHalf = ScalarProcessor.Inverse(ScalarProcessor.TwoValue);
        
        En = this
            .CreateComposer()
            .SetVectorTerm(0, ScalarProcessor.OneValue)
            .GetVector();
        
        Ep = this
            .CreateComposer()
            .SetVectorTerm(1, ScalarProcessor.OneValue)
            .GetVector();

        Eo = this
            .CreateComposer()
            .SetVectorTerm(0, scalarHalf)
            .SetVectorTerm(1, scalarHalf)
            .GetVector();

        Ei = this
            .CreateComposer()
            .SetVectorTerm(0, scalarOne)
            .SetVectorTerm(1, scalarMinusOne)
            .GetVector();

        MusicalAutomorphism = RGaMusicalAutomorphism<T>.Create(this);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaPoint(RGaVector<T> hgaPoint)
    {
        var sn = hgaPoint[0];
        var sp = hgaPoint[1];

        return sn.Equals(sp);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaPoint(RGaKVector<T> pgaPoint, int vSpaceDimensions)
    {
        var hgaPoint = 
            PGaDual(pgaPoint, vSpaceDimensions);

        return hgaPoint.Grade == 1 && 
               IsValidHGaPoint(
                   hgaPoint.GetVectorPart()
                );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidIpnsPoint(RGaVector<T> ipnsPoint)
    {
        var sn = ipnsPoint[0];
        var sp = ipnsPoint[1];

        return sn.Equals(sp);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> PGaDual(RGaKVector<T> mv, int vSpaceDimensions)
    {
        //Debug.Assert(
        //    IsValidPGaElement(mv)
        //);

        var icInv = 
            this.PseudoScalarInverse(vSpaceDimensions);

        return MusicalAutomorphism.OmMap(
            mv.Op(Ei).Lcp(icInv)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> PGaDual(RGaMultivector<T> mv, int vSpaceDimensions)
    {
        //Debug.Assert(
        //    IsValidPGaElement(mv)
        //);

        var icInv = 
            this.PseudoScalarInverse(vSpaceDimensions);

        return MusicalAutomorphism.OmMap(
            mv.Op(Ei).Lcp(icInv)
            // Also can be mv.Op(Ei).Lcp(Ic)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeEGaVector(double x, double y)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeEGaVector(T x, T y)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeEGaVector(double x, double y, double z)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeEGaVector(T x, T y, T z)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeEGaVector(LinVector2D<T> egaVector)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, egaVector.X.ScalarValue)
            .SetVectorTerm(3, egaVector.Y.ScalarValue)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeEGaVector(LinVector3D<T> egaVector)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, egaVector.X.ScalarValue)
            .SetVectorTerm(3, egaVector.Y.ScalarValue)
            .SetVectorTerm(4, egaVector.Z.ScalarValue)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeHGaPoint(double x, double y)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            );

        return this
            .CreateComposer()
            .SetVectorTerm(0, scalarHalf)
            .SetVectorTerm(1, scalarHalf)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeHGaPoint(T x, T y)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            );

        return this
            .CreateComposer()
            .SetVectorTerm(0, scalarHalf)
            .SetVectorTerm(1, scalarHalf)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeHGaPoint(double x, double y, double z)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            );

        return this
            .CreateComposer()
            .SetVectorTerm(0, scalarHalf)
            .SetVectorTerm(1, scalarHalf)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeHGaPoint(T x, T y, T z)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            );

        return this
            .CreateComposer()
            .SetVectorTerm(0, scalarHalf)
            .SetVectorTerm(1, scalarHalf)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeHGaPoint(LinVector2D<T> egaVector)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            );

        return this
            .CreateComposer()
            .SetVectorTerm(0, scalarHalf)
            .SetVectorTerm(1, scalarHalf)
            .SetVectorTerm(2, egaVector.X.ScalarValue)
            .SetVectorTerm(3, egaVector.Y.ScalarValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeHGaPoint(LinVector3D<T> egaVector)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            );

        return this
            .CreateComposer()
            .SetVectorTerm(0, scalarHalf)
            .SetVectorTerm(1, scalarHalf)
            .SetVectorTerm(2, egaVector.X.ScalarValue)
            .SetVectorTerm(3, egaVector.Y.ScalarValue)
            .SetVectorTerm(4, egaVector.Z.ScalarValue)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> EncodePGaPoint(double x, double y)
    {
        return PGaDual(
            EncodeHGaPoint(x, y), 
            4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> EncodePGaPoint(T x, T y)
    {
        return PGaDual(
            EncodeHGaPoint(x, y), 
            4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> EncodePGaPoint(double x, double y, double z)
    {
        return PGaDual(
            EncodeHGaPoint(x, y, z), 
            5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> EncodePGaPoint(T x, T y, T z)
    {
        return PGaDual(
            EncodeHGaPoint(x, y, z), 
            5
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> EncodePGaPoint(LinVector2D<T> egaPoint)
    {
        return PGaDual(
            EncodeHGaPoint(egaPoint), 
            4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> EncodePGaPoint(LinVector3D<T> egaPoint)
    {
        return PGaDual(
            EncodeHGaPoint(egaPoint), 
            5
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeIpnsPoint(double x, double y)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            ).ScalarValue;

        var x2 = x * x + y * y;
        var sn = ScalarProcessor.Times(scalarHalf, 1 + x2);
        var sp = ScalarProcessor.Times(scalarHalf, 1 - x2);

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeIpnsPoint(T x, T y)
    {
        var x2 = ScalarProcessor.Add(
            ScalarProcessor.Square(x), 
            ScalarProcessor.Square(y)
        );

        var sn = (ScalarProcessor.One + x2) / ScalarProcessor.Two;
        var sp = (ScalarProcessor.One - x2) / ScalarProcessor.Two;

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeIpnsPoint(double x, double y, double z)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            ).ScalarValue;

        var x2 = x * x + y * y + z * z;
        var sn = ScalarProcessor.Times(scalarHalf, 1 + x2);
        var sp = ScalarProcessor.Times(scalarHalf, 1 - x2);

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeIpnsPoint(T x, T y, T z)
    {
        var x2 = ScalarProcessor.Add(
            ScalarProcessor.Square(x), 
            ScalarProcessor.Square(y), 
            ScalarProcessor.Square(z)
        );

        var sn = (ScalarProcessor.One + x2) / ScalarProcessor.Two;
        var sp = (ScalarProcessor.One - x2) / ScalarProcessor.Two;

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeIpnsPoint(LinVector2D<T> egaPoint)
    {
        var x2 = egaPoint.VectorENormSquared();
        var sn = (ScalarProcessor.One + x2) / ScalarProcessor.Two;
        var sp = (ScalarProcessor.One - x2) / ScalarProcessor.Two;

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, egaPoint.X.ScalarValue)
            .SetVectorTerm(3, egaPoint.Y.ScalarValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EncodeIpnsPoint(LinVector3D<T> egaPoint)
    {
        var x2 = egaPoint.VectorENormSquared();
        var sn = (ScalarProcessor.One + x2) / ScalarProcessor.Two;
        var sp = (ScalarProcessor.One - x2) / ScalarProcessor.Two;

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, egaPoint.X.ScalarValue)
            .SetVectorTerm(3, egaPoint.Y.ScalarValue)
            .SetVectorTerm(4, egaPoint.Z.ScalarValue)
            .GetVector();
    }

    
    public RGaVector<T> DecodeEGaVector(RGaVector<T> egaVector)
    {
        var composer = EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in egaVector.IdScalarPairs)
        {
            var index = id.FirstOneBitPosition();

            if (index is 0 or 1) continue;

            composer.SetTerm(
                (index - 2).BasisVectorIndexToId(), 
                scalar
            );
        }

        return composer.GetVector();
    }
    
    public RGaVector<T> DecodeEGaVector(RGaVector<T> egaVector, T scalingFactor)
    {
        var composer = EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in egaVector.IdScalarPairs)
        {
            var index = id.FirstOneBitPosition();

            if (index is 0 or 1) continue;

            composer.SetTerm(
            (index - 2).BasisVectorIndexToId(), 
                ScalarProcessor.Times(scalar, scalingFactor)
            );
        }

        return composer.GetVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> DecodeHGaPoint(RGaVector<T> hgaPoint)
    {
        Debug.Assert(IsValidHGaPoint(hgaPoint));

        return DecodeEGaVector(
            hgaPoint, 
            (hgaPoint[0] + hgaPoint[1]).Inverse().ScalarValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> DecodePGaPoint(RGaKVector<T> pgaPoint, int vSpaceDimensions)
    {
        if (pgaPoint.Grade != vSpaceDimensions - 2)
            throw new InvalidOperationException();

        var hgaPoint = 
            PGaDual(pgaPoint, vSpaceDimensions).GetVectorPart();
        
        return DecodeHGaPoint(hgaPoint);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> DecodeIpnsPoint(RGaVector<T> ipnsPoint)
    {
        Debug.Assert(IsValidIpnsPoint(ipnsPoint));

        return DecodeEGaVector(
            ipnsPoint, 
            (ipnsPoint[0] + ipnsPoint[1]).Inverse().ScalarValue
        );
    }


    public RGaKVector<T> PGaRp(RGaKVector<T> mv1, RGaKVector<T> mv2, int vSpaceDimensions)
    {
        var icInv = 
            this.PseudoScalarInverse(vSpaceDimensions);

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

    public RGaMultivector<T> PGaRp(RGaMultivector<T> mv1, RGaMultivector<T> mv2, int vSpaceDimensions)
    {
        var icInv = 
            this.PseudoScalarInverse(vSpaceDimensions);

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
}