using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

/// <summary>
/// https://en.wikipedia.org/wiki/Conformal_geometric_algebra
/// </summary>
/// <typeparam name="T"></typeparam>
public class XGaConformalProcessor<T> :
    XGaProcessor<T>
{
    public Multivectors.XGaVector<T> En { get; }

    public Multivectors.XGaVector<T> Ep { get; }

    public Multivectors.XGaVector<T> Eo { get; }

    public Multivectors.XGaVector<T> Ei { get; }
    
    public XGaMusicalAutomorphism<T> MusicalAutomorphism { get; }


    internal XGaConformalProcessor(IScalarProcessor<T> scalarProcessor)
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

        MusicalAutomorphism = XGaMusicalAutomorphism<T>.Create(this);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidHGaPoint(Multivectors.XGaVector<T> hgaPoint)
    {
        var sn = hgaPoint[0];
        var sp = hgaPoint[1];

        return sn.Equals(sp);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidPGaPoint(Multivectors.XGaKVector<T> pgaPoint, int vSpaceDimensions)
    {
        var hgaPoint = 
            PGaDual(pgaPoint, vSpaceDimensions);

        return hgaPoint.Grade == 1 && 
               IsValidHGaPoint(
                   hgaPoint.GetVectorPart()
                );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValidIpnsPoint(Multivectors.XGaVector<T> ipnsPoint)
    {
        var sn = ipnsPoint[0];
        var sp = ipnsPoint[1];

        return sn.Equals(sp);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaKVector<T> PGaDual(Multivectors.XGaKVector<T> mv, int vSpaceDimensions)
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
    public Multivectors.XGaMultivector<T> PGaDual(Multivectors.XGaMultivector<T> mv, int vSpaceDimensions)
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
    public Multivectors.XGaVector<T> EncodeEGaVector(double x, double y)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeEGaVector(T x, T y)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeEGaVector(double x, double y, double z)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeEGaVector(T x, T y, T z)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .SetVectorTerm(4, z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeEGaVector(LinFloat64Vector2D egaVector)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, egaVector.X.ScalarValue)
            .SetVectorTerm(3, egaVector.Y.ScalarValue)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeEGaVector(LinFloat64Vector3D egaVector)
    {
        return this
            .CreateComposer()
            .SetVectorTerm(2, egaVector.X.ScalarValue)
            .SetVectorTerm(3, egaVector.Y.ScalarValue)
            .SetVectorTerm(4, egaVector.Z.ScalarValue)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeHGaPoint(double x, double y)
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
    public Multivectors.XGaVector<T> EncodeHGaPoint(T x, T y)
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
    public Multivectors.XGaVector<T> EncodeHGaPoint(Scalar<T> x, Scalar<T> y)
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
            .SetVectorTerm(2, x.ScalarValue)
            .SetVectorTerm(3, y.ScalarValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeHGaPoint(double x, double y, double z)
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
    public Multivectors.XGaVector<T> EncodeHGaPoint(T x, T y, T z)
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
    public Multivectors.XGaVector<T> EncodeHGaPoint(LinFloat64Vector2D egaVector)
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
    public Multivectors.XGaVector<T> EncodeHGaPoint(LinFloat64Vector3D egaVector)
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
    public Multivectors.XGaKVector<T> EncodePGaPoint(double x, double y)
    {
        return PGaDual(
            EncodeHGaPoint(x, y), 
            4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaKVector<T> EncodePGaPoint(T x, T y)
    {
        return PGaDual(
            EncodeHGaPoint(x, y), 
            4
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaKVector<T> EncodePGaPoint(double x, double y, double z)
    {
        return PGaDual(
            EncodeHGaPoint(x, y, z), 
            5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaKVector<T> EncodePGaPoint(T x, T y, T z)
    {
        return PGaDual(
            EncodeHGaPoint(x, y, z), 
            5
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaKVector<T> EncodePGaPoint(LinFloat64Vector2D egaPoint)
    {
        return PGaDual(
            EncodeHGaPoint(egaPoint), 
            4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaKVector<T> EncodePGaPoint(LinFloat64Vector3D egaPoint)
    {
        return PGaDual(
            EncodeHGaPoint(egaPoint), 
            5
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeIpnsPoint(double x, double y)
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
    public Multivectors.XGaVector<T> EncodeIpnsPoint(T x, T y)
    {
        var x2 = ScalarProcessor.Add(
            ScalarProcessor.Square(x), 
            ScalarProcessor.Square(y)
        ).ScalarValue;

        var sn = ScalarProcessor.Divide(
            ScalarProcessor.Add(ScalarProcessor.OneValue, x2).ScalarValue, 
            ScalarProcessor.TwoValue
        );

        var sp = ScalarProcessor.Divide(
            ScalarProcessor.Subtract(ScalarProcessor.OneValue, x2).ScalarValue, 
            ScalarProcessor.TwoValue
        );

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, x)
            .SetVectorTerm(3, y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeIpnsPoint(double x, double y, double z)
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
    public Multivectors.XGaVector<T> EncodeIpnsPoint(T x, T y, T z)
    {
        var x2 = ScalarProcessor.Add(
            ScalarProcessor.Square(x), 
            ScalarProcessor.Square(y), 
            ScalarProcessor.Square(z)
        ).ScalarValue;

        var sn = ScalarProcessor.Divide(
            ScalarProcessor.Add(ScalarProcessor.OneValue, x2).ScalarValue, 
            ScalarProcessor.TwoValue
        );

        var sp = ScalarProcessor.Divide(
            ScalarProcessor.Subtract(ScalarProcessor.OneValue, x2).ScalarValue, 
            ScalarProcessor.TwoValue
        );

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
    public Multivectors.XGaVector<T> EncodeIpnsPoint(LinFloat64Vector2D egaPoint)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            ).ScalarValue;

        var x2 = egaPoint.VectorENormSquared().ScalarValue;
        var sn = ScalarProcessor.Times(scalarHalf, 1 + x2);
        var sp = ScalarProcessor.Times(scalarHalf, 1 - x2);

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, egaPoint.X.ScalarValue)
            .SetVectorTerm(3, egaPoint.Y.ScalarValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> EncodeIpnsPoint(LinFloat64Vector3D egaPoint)
    {
        var scalarHalf = 
            ScalarProcessor.Divide(
                ScalarProcessor.OneValue, 
                ScalarProcessor.TwoValue
            ).ScalarValue;

        var x2 = egaPoint.VectorENormSquared();
        var sn = ScalarProcessor.Times(scalarHalf, 1 + x2);
        var sp = ScalarProcessor.Times(scalarHalf, 1 - x2);

        return this
            .CreateComposer()
            .SetVectorTerm(0, sn)
            .SetVectorTerm(1, sp)
            .SetVectorTerm(2, egaPoint.X.ScalarValue)
            .SetVectorTerm(3, egaPoint.Y.ScalarValue)
            .SetVectorTerm(4, egaPoint.Z.ScalarValue)
            .GetVector();
    }

    
    public Multivectors.XGaVector<T> DecodeEGaVector(Multivectors.XGaVector<T> egaVector)
    {
        var composer = EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in egaVector.IdScalarPairs)
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
    
    public Multivectors.XGaVector<T> DecodeEGaVector(Multivectors.XGaVector<T> egaVector, T scalingFactor)
    {
        var composer = EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in egaVector.IdScalarPairs)
        {
            var index = id.FirstIndex;

            if (index is 0 or 1) continue;

            composer.SetTerm(
            (index - 2).ToUnitIndexSet(), 
                ScalarProcessor.Times(scalar, scalingFactor)
            );
        }

        return composer.GetVector();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> DecodeHGaPoint(Multivectors.XGaVector<T> hgaPoint)
    {
        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return DecodeEGaVector(
            hgaPoint, 
            (hgaPoint[0] + hgaPoint[1]).Inverse().ScalarValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> DecodePGaPoint(Multivectors.XGaKVector<T> pgaPoint, int vSpaceDimensions)
    {
        if (pgaPoint.Grade != vSpaceDimensions - 2)
            throw new InvalidOperationException();

        var hgaPoint = 
            PGaDual(pgaPoint, vSpaceDimensions).GetVectorPart();
        
        return DecodeHGaPoint(hgaPoint);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Multivectors.XGaVector<T> DecodeIpnsPoint(Multivectors.XGaVector<T> ipnsPoint)
    {
        Debug.Assert(IsValidIpnsPoint(ipnsPoint));

        return DecodeEGaVector(
            ipnsPoint, 
            (ipnsPoint[0] + ipnsPoint[1]).Inverse().ScalarValue
        );
    }


    public Multivectors.XGaKVector<T> PGaRp(Multivectors.XGaKVector<T> mv1, Multivectors.XGaKVector<T> mv2, int vSpaceDimensions)
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

    public Multivectors.XGaMultivector<T> PGaRp(Multivectors.XGaMultivector<T> mv1, XGaMultivector<T> mv2, int vSpaceDimensions)
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