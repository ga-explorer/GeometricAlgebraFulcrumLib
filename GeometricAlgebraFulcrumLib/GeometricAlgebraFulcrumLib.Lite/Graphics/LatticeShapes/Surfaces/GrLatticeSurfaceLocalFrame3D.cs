using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.LatticeShapes.Surfaces;

public sealed class GrLatticeSurfaceLocalFrame3D : 
    IGraphicsVertex3D
{
    public GrLatticeSurface3D ParentSurface { get; }

    public GrLatticeSurfaceList3D ParentSurfaceList 
        => ParentSurface.ParentList;
        
    public int Index { get; internal set; } = -1;

    public HashSet<Pair<int>> LatticeIndexSet { get; }
        = new HashSet<Pair<int>>();

    private Float64Vector3D _point;
    public Float64Vector3D Point
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

    public double Item1 => _point.X;
        
    public double Item2 => _point.Y;

    public double Item3 => _point.Z;
        
    public Float64Scalar X => _point.X;
        
    public Float64Scalar Y => _point.Y;
        
    public Float64Scalar Z => _point.Z;

    public Triplet<double> PointTriplet 
        => new Triplet<double>(Point.X, Point.Y, Point.Z);

    public Normal3D Normal { get; }
        = new Normal3D();
        
    public Pair<double> ParameterValue { get; set; }

    public Color Color { get; set; }

    public bool HasParameterValue 
        => true;

    public bool HasNormal 
        => true;

    public bool HasColor 
        => true;
        
    public GraphicsVertexDataKind3D DataKind 
        => GraphicsVertexDataKind3D.NormalTextureColorData;


    internal GrLatticeSurfaceLocalFrame3D(GrLatticeSurface3D parentSurface, Pair<int> uvIndex, ITriplet<double> pointTriplet)
    {
        ParentSurface = parentSurface;

        _point = Float64Vector3D.Create(pointTriplet.Item1, pointTriplet.Item2, pointTriplet.Item3);

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
    public bool HasPoint(ITriplet<double> pointTriplet)
    {
        return Point.X == pointTriplet.Item1 && 
               Point.Y == pointTriplet.Item2 && 
               Point.Z == pointTriplet.Item3;
    }
        
    internal Float64Vector2D ComputeTextureUv()
    {
        var textureUv = Float64Vector2D.Zero;

        foreach (var (indexU, indexV) in LatticeIndexSet)
        {
            textureUv += ParentSurface.GetLatticeTextureUv(indexU, indexV);
        }
            
        textureUv /= LatticeIndexSet.Count;

        ParameterValue = new Pair<double>(textureUv.Item1, textureUv.Item2);

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