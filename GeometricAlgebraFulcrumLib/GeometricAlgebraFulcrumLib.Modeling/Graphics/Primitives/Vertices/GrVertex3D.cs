using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;

public sealed class GrVertex3D 
    : IGraphicsVertex3D
{
    public int Index { get; }
        
    public int VSpaceDimensions 
        => 3;

    public LinFloat64Vector3D Point { get; }

    public LinFloat64Vector2D ParameterValue 
        => LinFloat64Vector2D.Zero;

    public LinFloat64Normal3D Normal
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

    public double X 
        => Point.X;

    public double Y 
        => Point.Y;

    public double Z 
        => Point.Z;
    
    public double this[int index] 
        => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => Float64Scalar.Zero
        };

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
        Point = LinFloat64Vector3D.Create(x, y, z);
    }

    public GrVertex3D(int index, ITriplet<double> point)
    {
        Index = index;
        Point = point.ToLinVector3D();
    }

    public GrVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
    {
        Index = index;
        Point = vertex.Point;
    }

}