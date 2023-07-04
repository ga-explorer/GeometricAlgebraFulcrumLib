using System.Collections;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Triangles
{
    public sealed class GrVertexListTriangleGeometry3D :
        IGraphicsTriangleGeometry3D
    {
        private readonly IReadOnlyList<IGraphicsVertex3D> _localFrameList;

        private readonly Dictionary<Triplet<int>, int> _triangleIndexTripletDictionary
            = new Dictionary<Triplet<int>, int>();

        private readonly List<Triplet<int>> _triangleIndexTripletList
            = new List<Triplet<int>>();


        public GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.Triangles;

        public int VertexCount 
            => _localFrameList.Count;

        public IEnumerable<int> GeometryIndices
            => _triangleIndexTripletList.SelectMany(t => t.GetItems());

        public IEnumerable<IGraphicsVertex3D> GeometryVertices
            => _localFrameList;

        public IEnumerable<IFloat64Vector3D> GeometryPoints 
            => _localFrameList;

        public int Count 
            => _triangleIndexTripletList.Count;

        public ITriangle3D this[int index]
        {
            get
            {
                var (index1, index2, index3) = 
                    _triangleIndexTripletList[index];

                return Triangle3D.Create(
                    _localFrameList[index1],
                    _localFrameList[index2],
                    _localFrameList[index3]
                );
            }
        }

        public IEnumerable<Triplet<IFloat64Vector3D>> TriangleVertexPoints
            => _triangleIndexTripletList.Select(t => 
                new Triplet<IFloat64Vector3D>(
                    _localFrameList[t.Item1],
                    _localFrameList[t.Item2],
                    _localFrameList[t.Item3]
                )
            );

        public IEnumerable<Triplet<int>> TriangleVertexIndices
            => _triangleIndexTripletList;

        public IEnumerable<IFloat64Vector3D> VertexNormals 
            => _localFrameList.Select(f => f.Normal);
        
        public IEnumerable<IFloat64Vector2D> VertexTextureUVs
            => _localFrameList.Select(f => f.ParameterValue.ToVector2D());
        
        public IEnumerable<Color> VertexColors 
            => _localFrameList.Select(f => f.Color);
        
        public bool VertexNormalsEnabled { get; set; }
        
        public bool VertexTextureUVsEnabled { get; set; }
        
        public bool VertexColorsEnabled { get; set; }

        public GrVertexNormalComputationMethod NormalComputationMethod
            => GrVertexNormalComputationMethod.None;


        public GrVertexListTriangleGeometry3D(IReadOnlyList<IGraphicsVertex3D> localFrameList)
        {
            if (localFrameList.Count < 3)
                throw new InvalidOperationException();

            _localFrameList = localFrameList;
        }


        public GrVertexListTriangleGeometry3D ClearTriangles()
        {
            _triangleIndexTripletList.Clear();
            _triangleIndexTripletDictionary.Clear();

            return this;
        }

        public bool AddTriangle(int index1, int index2, int index3)
        {
            //TODO: add more constraints on the triangles here if needed

            Triplet<int> indexTriplet;

            if (index1 < index2 && index1 < index3) 
                indexTriplet = new Triplet<int>(index1, index2, index3);

            else if (index2 < index3 && index2 < index1)
                indexTriplet = new Triplet<int>(index2, index3, index1);
            
            else if (index3 < index2 && index3 < index1)
                indexTriplet = new Triplet<int>(index3, index1, index2);

            else 
                return false;

            if (_triangleIndexTripletDictionary.ContainsKey(indexTriplet))
                return false;

            var index = _triangleIndexTripletList.Count;
            _triangleIndexTripletDictionary.Add(indexTriplet, index);
            _triangleIndexTripletList.Add(indexTriplet);

            return true;
        }

        public bool AddTriangle(ITriplet<int> indexTriplet)
        {
            return AddTriangle(indexTriplet.Item1, indexTriplet.Item2, indexTriplet.Item3);
        }

        public GrVertexListTriangleGeometry3D AddTriangles(IEnumerable<ITriplet<int>> indexTripletList)
        {
            foreach (var indexTriplet in indexTripletList)
                AddTriangle(indexTriplet);

            return this;
        }

        public IFloat64Vector3D GetGeometryPoint(int index)
        {
            return _localFrameList[index].Point;
        }

        public Normal3D GetVertexNormal(int index)
        {
            return _localFrameList[index].Normal;
        }

        public void ComputeVertexNormals(bool inverseNormals)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ITriangle3D> GetEnumerator()
        {
            return _triangleIndexTripletList.Select(
                t => Triangle3D.Create(
                    _localFrameList[t.Item1],
                    _localFrameList[t.Item2],
                    _localFrameList[t.Item3]
                )
            ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}