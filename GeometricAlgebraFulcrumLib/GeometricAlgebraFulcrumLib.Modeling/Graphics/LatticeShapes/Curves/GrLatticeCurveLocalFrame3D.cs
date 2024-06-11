using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes.Curves;

public sealed class GrLatticeCurveLocalFrame3D : 
    IParametricCurveLocalFrame3D
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

    public Float64Scalar Item1 
        => _point.X;
        
    public Float64Scalar Item2 
        => _point.Y;

    public Float64Scalar Item3 
        => _point.Z;
        
    public Float64Scalar X 
        => _point.X;
        
    public Float64Scalar Y 
        => _point.Y;
        
    public Float64Scalar Z 
        => _point.Z;

    public bool IsValid()
    {
        return _point.IsValid();
    }

    public Triplet<Float64Scalar> PointTriplet 
        => new Triplet<Float64Scalar>(Point.X, Point.Y, Point.Z);

    public LinFloat64Normal3D Normal1 { get; }
        = new LinFloat64Normal3D();
        
    public LinFloat64Normal3D Normal2 { get; }
        = new LinFloat64Normal3D();

    public LinFloat64Vector3D Tangent { get; }

    public Float64Scalar ParameterValue { get; private set; }

    public Color Color { get; set; }


    public GrLatticeCurveLocalFrame3D(GrLatticeCurve3D parentCurve, int uIndex, ITriplet<Float64Scalar> pointTriplet)
    {
        ParentCurve = parentCurve;

        _point = LinFloat64Vector3D.Create(pointTriplet.Item1, pointTriplet.Item2, pointTriplet.Item3);

        LatticeIndexSet.Add(uIndex);
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