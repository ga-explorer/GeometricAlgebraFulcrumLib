using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes.Curves;

public sealed class GrLatticeCurveLocalFrame3D : 
    IFloat64Path3DLocalFrame
{
    public GrLatticeCurve3D ParentCurve { get; }

    public GrLatticeCurveList3D ParentCurveList 
        => ParentCurve.ParentList;
        
    public int Index { get; internal set; } = -1;

    public HashSet<int> LatticeIndexSet { get; }
        = new HashSet<int>();

    private LinFloat64Vector3D _point;
    public LinFloat64Vector3D Point
    {
        get => _point;
        internal set
        {
            if (ParentCurve.IsReady)
                throw new InvalidOperationException();

            _point = value;
        }
    }
        
    public int VSpaceDimensions 
        => 3;

    public double Item1 
        => _point.X;
        
    public double Item2 
        => _point.Y;

    public double Item3 
        => _point.Z;
        
    public double X 
        => _point.X;
        
    public double Y 
        => _point.Y;
        
    public double Z 
        => _point.Z;
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };

    public Triplet<double> PointTriplet 
        => new Triplet<double>(Point.X, Point.Y, Point.Z);

    public double ScalingFactor 
        => 1;

    public LinFloat64Vector3D Tangent { get; }

    public LinFloat64Normal3D Normal1 { get; }
        = new LinFloat64Normal3D();
        
    public LinFloat64Normal3D Normal2 { get; }
        = new LinFloat64Normal3D();

    public double TimeValue { get; private set; }

    public Color Color { get; set; }


    public GrLatticeCurveLocalFrame3D(GrLatticeCurve3D parentCurve, int uIndex, ITriplet<double> pointTriplet)
    {
        ParentCurve = parentCurve;

        _point = LinFloat64Vector3D.Create(pointTriplet.Item1, pointTriplet.Item2, pointTriplet.Item3);

        LatticeIndexSet.Add(uIndex);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _point.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasPoint(double x, double y, double z)
    {
        return Point.X == x && 
               Point.Y == y && 
               Point.Z == z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasPoint(ITriplet<double> pointTriplet)
    {
        return Point.X == pointTriplet.Item1 && 
               Point.Y == pointTriplet.Item2 && 
               Point.Z == pointTriplet.Item3;
    }
        
    internal double ComputeTextureU()
    {
        return LatticeIndexSet.Average(indexU => ParentCurve.GetLatticeTextureU(indexU));
    }

    //internal GrLatticeSurfaceLocalFrame3D ComputeLocalFrame()
    //{
    //    // TODO: this method is not correct around singular points

    //    var tangentU = Tuple3D.Zero;
    //    var tangentV = Tuple3D.Zero;

    //    foreach (var (indexU, indexV) in LatticeIndexSet)
    //    {
    //        tangentU += ParentCurve.GetTangentU(indexU, indexV);
    //        tangentV += ParentCurve.GetTangentV(indexU, indexV);
    //    }

    //    var normal = tangentU.VectorCross(tangentV).ToUnitVector();
    //    tangentU = tangentU.ToUnitVector();
    //    tangentV = tangentV.ToUnitVector();

    //    LocalFrame = new GrLatticeSurfaceLocalFrame3D(tangentU, tangentV, normal);

    //    return LocalFrame;
    //}

}