using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes.Surfaces;

public sealed class GrLatticeSurfaceLocalFrame3D : 
    IGraphicsVertex3D
{
    public GrLatticeSurface3D ParentSurface { get; }

    public GrLatticeSurfaceList3D ParentSurfaceList 
        => ParentSurface.ParentList;
        
    public int Index { get; internal set; } = -1;

    public HashSet<Pair<int>> LatticeIndexSet { get; }
        = new HashSet<Pair<int>>();

    private LinFloat64Vector3D _point;
    public LinFloat64Vector3D Point
    {
        get => _point;
        internal set
        {
            if (ParentSurface.IsReady)
                throw new InvalidOperationException();

            _point = value;
        }
    }
        
    public int VSpaceDimensions 
        => 3;

    public Float64Scalar Item1 => _point.X;
        
    public Float64Scalar Item2 => _point.Y;

    public Float64Scalar Item3 => _point.Z;
        
    public Float64Scalar X => _point.X;
        
    public Float64Scalar Y => _point.Y;
        
    public Float64Scalar Z => _point.Z;

    public Triplet<Float64Scalar> PointTriplet 
        => new Triplet<Float64Scalar>(Point.X, Point.Y, Point.Z);

    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();
        
    public LinFloat64Vector2D ParameterValue { get; set; }

    public Color Color { get; set; }

    public bool HasParameterValue 
        => true;

    public bool HasNormal 
        => true;

    public bool HasColor 
        => true;
        
    public GraphicsVertexDataKind3D DataKind 
        => GraphicsVertexDataKind3D.NormalTextureColorData;


    internal GrLatticeSurfaceLocalFrame3D(GrLatticeSurface3D parentSurface, Pair<int> uvIndex, ITriplet<Float64Scalar> pointTriplet)
    {
        ParentSurface = parentSurface;

        _point = LinFloat64Vector3D.Create(pointTriplet.Item1, pointTriplet.Item2, pointTriplet.Item3);

        LatticeIndexSet.Add(uvIndex);
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
    public bool HasPoint(ITriplet<Float64Scalar> pointTriplet)
    {
        return Point.X == pointTriplet.Item1 && 
               Point.Y == pointTriplet.Item2 && 
               Point.Z == pointTriplet.Item3;
    }
        
    internal LinFloat64Vector2D ComputeTextureUv()
    {
        var textureUv = LinFloat64Vector2D.Zero;

        foreach (var (indexU, indexV) in LatticeIndexSet)
        {
            textureUv += ParentSurface.GetLatticeTextureUv(indexU, indexV);
        }
            
        textureUv /= LatticeIndexSet.Count;

        ParameterValue = textureUv;

        return textureUv;
    }

    //internal GrSurfaceLocalFrame3D ComputeLocalFrame()
    //{
    //    // TODO: this method is not correct around singular points

    //    var tangentU = Tuple3D.Zero;
    //    var tangentV = Tuple3D.Zero;

    //    foreach (var (indexU, indexV) in LatticeIndexSet)
    //    {
    //        tangentU += ParentSurface.GetLatticeTangentU(indexU, indexV);
    //        tangentV += ParentSurface.GetLatticeTangentV(indexU, indexV);
    //    }

    //    var normal = tangentU.VectorCross(tangentV).ToUnitVector();
    //    tangentU = tangentU.ToUnitVector();
    //    tangentV = tangentV.ToUnitVector();

    //    LocalFrame = new GrSurfaceLocalFrame3D(tangentU, tangentV, normal);

    //    return LocalFrame;
    //}

}