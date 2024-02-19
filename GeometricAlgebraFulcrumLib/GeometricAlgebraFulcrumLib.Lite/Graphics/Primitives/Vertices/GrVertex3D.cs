using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices;

public sealed class GrVertex3D 
    : IGraphicsVertex3D
{
    public int Index { get; }
        
    public int VSpaceDimensions 
        => 3;

    public Float64Vector3D Point { get; }

    public Pair<double> ParameterValue 
        => new Pair<double>(0, 0);

    public Normal3D Normal
        => null;

    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }

    public bool HasParameterValue 
        => false;

    public bool HasNormal 
        => false;

    public bool HasColor 
        => false;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.VoidData;

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

    public bool IsValid() => Point.IsValid();


    public GrVertex3D(int index, double x, double y, double z)
    {
        Index = index;
        Point = Float64Vector3D.Create(x, y, z);
    }

    public GrVertex3D(int index, ITriplet<double> point)
    {
        Index = index;
        Point = point.ToVector3D();
    }

    public GrVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
    {
        Index = index;
        Point = vertex.Point;
    }

}