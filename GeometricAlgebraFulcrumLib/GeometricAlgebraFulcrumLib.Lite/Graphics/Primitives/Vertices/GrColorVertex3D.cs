using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices;

public sealed class GrColorVertex3D 
    : IGraphicsVertex3D
{
    public int Index { get; }
        
    public int VSpaceDimensions 
        => 3;

    public Float64Vector3D Point { get; }

    public Color Color { get; set; }
        = Color.Black;

    public Pair<double> ParameterValue 
        => new Pair<double>(0, 0);

    public Normal3D Normal
        => null;

    public bool HasColor 
        => true;

    public bool HasParameterValue 
        => false;

    public bool HasNormal 
        => false;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.ColorData;

    public Float64Scalar X 
        => Point.X;

    public Float64Scalar Y 
        => Point.Y;

    public Float64Scalar Z 
        => Point.Z;

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;

    public double TextureU 
        => 0;

    public bool IsValid() => Point.IsValid();


    public GrColorVertex3D(int index, ITriplet<double> point)
    {
        Index = index;
        Point = point.ToVector3D();
    }

    public GrColorVertex3D(int index, ITriplet<double> point, Color color)
    {
        Index = index;
        Point = point.ToVector3D();
        Color = color;
    }

    public GrColorVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
    {
        Index = index;
        Point = vertex.Point;
        Color = vertex.Color;
    }
}