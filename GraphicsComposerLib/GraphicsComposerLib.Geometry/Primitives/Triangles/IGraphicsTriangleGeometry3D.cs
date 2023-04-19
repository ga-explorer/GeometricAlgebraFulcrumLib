using System.Collections.Generic;

using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Triangles;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives.Triangles
{
    public interface IGraphicsTriangleGeometry3D 
        : IGraphicsPrimitiveGeometry3D<ITriangle3D>
    {
        IEnumerable<Triplet<IFloat64Tuple3D>> TriangleVertexPoints { get; }

        IEnumerable<Triplet<int>> TriangleVertexIndices { get; }

        IEnumerable<IFloat64Tuple3D> VertexNormals { get; }

        IEnumerable<IFloat64Tuple2D> VertexTextureUVs { get; }

        IEnumerable<Color> VertexColors { get; }

        bool VertexNormalsEnabled { get; }

        bool VertexTextureUVsEnabled { get; }

        bool VertexColorsEnabled { get; }

        Normal3D GetVertexNormal(int index);

        GrVertexNormalComputationMethod NormalComputationMethod { get; }

        void ComputeVertexNormals(bool inverseNormals);
    }
}