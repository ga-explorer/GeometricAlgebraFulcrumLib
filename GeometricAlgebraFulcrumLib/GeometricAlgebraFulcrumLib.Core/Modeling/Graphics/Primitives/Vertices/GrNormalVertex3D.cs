using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Vertices;

public sealed class GrNormalVertex3D 
    : IGraphicsVertex3D
{
    public int Index { get; }
        
    public int VSpaceDimensions 
        => 3;

    public LinFloat64Vector3D Point { get; }

    public Color Color
    {
        get => Color.Black;
        set => throw new InvalidOperationException();
    }

    public Pair<Float64Scalar> ParameterValue 
        => new Pair<Float64Scalar>(0, 0);

    public LinFloat64Normal3D Normal { get; }
        = new LinFloat64Normal3D();

    public Float64Scalar X 
        => Point.X;

    public Float64Scalar Y 
        => Point.Y;

    public Float64Scalar Z 
        => Point.Z;

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;

    public bool HasColor 
        => false;

    public bool HasParameterValue 
        => false;

    public bool HasNormal 
        => true;

    public GraphicsVertexDataKind3D DataKind
        => GraphicsVertexDataKind3D.NormalData;

    public bool IsValid() => Point.IsValid() && Normal.IsValid();


    public GrNormalVertex3D(int index, ITriplet<Float64Scalar> point)
    {
        Index = index;
        Point = point.ToLinVector3D();
    }

    public GrNormalVertex3D(int index, ITriplet<Float64Scalar> point, ITriplet<Float64Scalar> normal)
    {
        Index = index;
        Point = point.ToLinVector3D();
        Normal.Set(normal);
    }

    public GrNormalVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
    {
        Index = index;
        Point = vertex.Point;
        Normal.Set(vertex.Normal);
    }


        
    public LinFloat64Vector3D GetDisplacedPoint(double d)
    {
        return LinFloat64Vector3D.Create(Point.X + d * Normal.X,
            Point.Y + d * Normal.Y,
            Point.Z + d * Normal.Z);
    }
}