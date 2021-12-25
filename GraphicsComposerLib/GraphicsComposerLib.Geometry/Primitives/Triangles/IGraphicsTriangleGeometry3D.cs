using System.Collections.Generic;
using System.Drawing;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes.Triangles;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives.Triangles
{
    public interface IGraphicsTriangleGeometry3D 
        : IGraphicsPrimitiveGeometry3D<ITriangle3D>
    {
        IEnumerable<Triplet<ITuple3D>> TriangleVertexPoints { get; }

        IEnumerable<Triplet<int>> TriangleVertexIndices { get; }

        IEnumerable<ITuple3D> VertexNormals { get; }

        IEnumerable<ITuple2D> VertexTextureUVs { get; }

        IEnumerable<Color> VertexColors { get; }

        bool VertexNormalsEnabled { get; }

        bool VertexTextureUVsEnabled { get; }

        bool VertexColorsEnabled { get; }

        GrNormal3D GetVertexNormal(int index);

        GrVertexNormalComputationMethod NormalComputationMethod { get; }

        void ComputeVertexNormals(bool inverseNormals);
    }
}