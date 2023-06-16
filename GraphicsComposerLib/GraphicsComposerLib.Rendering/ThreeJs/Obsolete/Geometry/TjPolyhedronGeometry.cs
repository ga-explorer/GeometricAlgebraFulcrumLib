using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
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

        public IReadOnlyList<IFloat64Tuple3D> VertexPositions { get; }

        public IReadOnlyList<int> FaceIndices { get; }


        public TjPolyhedronGeometry(IReadOnlyList<IFloat64Tuple3D> vertexPositions, IReadOnlyList<int> faceIndices)
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