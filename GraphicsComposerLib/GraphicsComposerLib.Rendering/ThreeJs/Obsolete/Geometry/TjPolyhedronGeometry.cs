using System.Diagnostics.CodeAnalysis;
using NumericalGeometryLib.BasicMath.Tuples;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.ThreeJs.Obsolete.Geometry
{
    /// <summary>
    /// A polyhedron is a solid in three dimensions with flat faces. This
    /// class will take an array of vertices, project them onto a sphere,
    /// and then divide them up to the desired level of detail. This class
    /// is used by DodecahedronGeometry, IcosahedronGeometry, OctahedronGeometry,
    /// and TetrahedronGeometry to generate their respective geometries.
    /// https://threejs.org/docs/#api/en/geometries/PolyhedronGeometry
    /// </summary>
    public class TjPolyhedronGeometry :
        TjPolyhedronGeometryBase
    {
        public override string JavaScriptClassName 
            => "PolyhedronGeometry";

        public IReadOnlyList<ITuple3D> VertexPositions { get; }

        public IReadOnlyList<int> FaceIndices { get; }


        public TjPolyhedronGeometry([NotNull] IReadOnlyList<ITuple3D> vertexPositions, [NotNull] IReadOnlyList<int> faceIndices)
        {
            VertexPositions = vertexPositions;
            FaceIndices = faceIndices;
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
        {
            base.UpdateConstructorAttributes(attributesDictionary);

            attributesDictionary
                .SetNumbersArrayValue("vertices", VertexPositions, "// ", "[]")
                .SetNumbersArrayValue("indices", FaceIndices, "[]");
        }
    }
}