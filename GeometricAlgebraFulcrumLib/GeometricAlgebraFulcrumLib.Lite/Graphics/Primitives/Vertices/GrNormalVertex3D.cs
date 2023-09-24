using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices
{
    public sealed class GrNormalVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }
        
        public int VSpaceDimensions 
            => 3;

        public Float64Vector3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public Pair<double> ParameterValue 
            => new Pair<double>(0, 0);

        public Normal3D Normal { get; }
            = new Normal3D();

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

        public bool HasColor 
            => false;

        public bool HasParameterValue 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.NormalData;

        public bool IsValid() => Point.IsValid() && Normal.IsValid();


        public GrNormalVertex3D(int index, ITriplet<double> point)
        {
            Index = index;
            Point = point.ToVector3D();
        }

        public GrNormalVertex3D(int index, ITriplet<double> point, ITriplet<double> normal)
        {
            Index = index;
            Point = point.ToVector3D();
            Normal.Set(normal);
        }

        public GrNormalVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            Normal.Set(vertex.Normal);
        }


        
        public Float64Vector3D GetDisplacedPoint(double d)
        {
            return Float64Vector3D.Create(Point.X + d * Normal.X,
                Point.Y + d * Normal.Y,
                Point.Z + d * Normal.Z);
        }
    }
}