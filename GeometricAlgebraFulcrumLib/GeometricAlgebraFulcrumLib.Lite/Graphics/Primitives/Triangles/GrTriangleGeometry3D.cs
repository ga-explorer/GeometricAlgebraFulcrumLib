using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Triangles
{
    public sealed class GrTriangleGeometry3D 
        : GrTriangleGeometryBase3D
    {
        public static GrTriangleGeometry3D Create(IReadOnlyList<IFloat64Vector3D> pointsList)
        {
            //if ((pointsList.Count % 3) != 0)
            //    throw new ArgumentException();

            return new GrTriangleGeometry3D(pointsList);
        }

        public static GrTriangleGeometry3D Create(IReadOnlyList<IFloat64Vector3D> pointsList, List<int> indicesList)
        {
            //if ((pointsList.Count % 3) != 0)
            //    throw new ArgumentException();

            return new GrTriangleGeometry3D(pointsList, indicesList);
        }

        public static GrTriangleGeometry3D Create(params IFloat64Vector3D[] pointsList)
        {
            //if ((pointsList.Length % 3) != 0)
            //    throw new ArgumentException();

            return new GrTriangleGeometry3D(pointsList);
        }

        public static GrTriangleGeometry3D Create(IEnumerable<IFloat64Vector3D> pointsList)
        {
            var pointsArray = pointsList.ToArray();

            //if ((pointsArray.Length % 3) != 0)
            //    throw new ArgumentException();

            return new GrTriangleGeometry3D(pointsArray);
        }

        public static GrTriangleGeometry3D Create(IEnumerable<IFloat64Vector3D> pointsList, IEnumerable<int> indicesList)
        {
            var pointsArray = pointsList.ToArray();

            //if ((pointsArray.Length % 3) != 0)
            //    throw new ArgumentException();

            return new GrTriangleGeometry3D(pointsArray, indicesList.ToList());
        }

        public static GrTriangleGeometry3D Create(params ITriangle3D[] trianglesList)
        {
            var pointsList = new DistinctTuplesList3D();
            var vertexIndicesList = new List<int>();

            foreach (var triangle in trianglesList)
            {
                var vertexIndex1 = pointsList.GetOrAddTupleIndex(
                    triangle.GetPoint1()
                );

                var vertexIndex2 = pointsList.GetOrAddTupleIndex(
                    triangle.GetPoint2()
                );

                var vertexIndex3 = pointsList.GetOrAddTupleIndex(
                    triangle.GetPoint3()
                );

                vertexIndicesList.Add(vertexIndex1);
                vertexIndicesList.Add(vertexIndex2);
                vertexIndicesList.Add(vertexIndex3);
            }

            return new GrTriangleGeometry3D(pointsList, vertexIndicesList);
        }
        
        public static GrTriangleGeometry3D Create(IEnumerable<ITriangle3D> trianglesList, bool reversePoints)
        {
            var pointsList = new DistinctTuplesList3D();
            var vertexIndicesList = new List<int>();

            foreach (var triangle in trianglesList)
            {
                var vertexIndex1 = pointsList.GetOrAddTupleIndex(
                    triangle.GetPoint1()
                );

                var vertexIndex2 = pointsList.GetOrAddTupleIndex(
                    triangle.GetPoint2()
                );

                var vertexIndex3 = pointsList.GetOrAddTupleIndex(
                    triangle.GetPoint3()
                );

                if (reversePoints)
                {
                    vertexIndicesList.Add(vertexIndex3);
                    vertexIndicesList.Add(vertexIndex2);
                    vertexIndicesList.Add(vertexIndex1);
                }
                else
                {
                    vertexIndicesList.Add(vertexIndex1);
                    vertexIndicesList.Add(vertexIndex2);
                    vertexIndicesList.Add(vertexIndex3);
                }
            }

            return new GrTriangleGeometry3D(pointsList, vertexIndicesList);
        }

        public static GrTriangleGeometry3D Create(IReadOnlyList<IGraphicsSurfaceLocalFrame3D> verticesList, GraphicsVertexDataKind3D vertexDataKind)
        {
            //if ((verticesList.Count % 3) != 0)
            //    throw new ArgumentException();

            var geometry = new GrTriangleGeometry3D(verticesList);

            if (vertexDataKind.HasColor)
                geometry.SetVertexColors(
                    verticesList.Select(v => v.Color).ToArray()
                );

            if (vertexDataKind.HasTextureUv)
                geometry.SetVertexUVs(
                    verticesList.Select(v => (IFloat64Vector2D) v.ParameterValue.ToVector2D()).ToArray()
                );

            if (vertexDataKind.HasNormal)
                geometry.SetVertexNormals(
                    verticesList.Select(v => v.Normal).ToArray()
                );

            return geometry;
        }


        public override GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.Triangles;

        private readonly List<int> _vertexIndicesList;
        public override IEnumerable<int> GeometryIndices
            => _vertexIndicesList;

        public override int Count
            => _vertexIndicesList.Count / 3;

        public override ITriangle3D this[int index] 
            => Triangle3D.Create(
                GetGeometryPoint(_vertexIndicesList[3 * index]),
                GetGeometryPoint(_vertexIndicesList[3 * index + 1]),
                GetGeometryPoint(_vertexIndicesList[3 * index + 2])
            );

        public override IEnumerable<Triplet<IFloat64Vector3D>> TriangleVertexPoints
        {
            get
            {
                var pointsList = new List<Triplet<IFloat64Vector3D>>(Count);

                for (var i = 0; i < _vertexIndicesList.Count; i += 3)
                    pointsList.Add(new Triplet<IFloat64Vector3D>(
                        GetGeometryPoint(_vertexIndicesList[i]),
                        GetGeometryPoint(_vertexIndicesList[i + 1]),
                        GetGeometryPoint(_vertexIndicesList[i + 2])
                    ));

                return pointsList;
            }
        }

        public override IEnumerable<Triplet<int>> TriangleVertexIndices
        {
            get
            {
                var pointsList = new List<Triplet<int>>(Count);

                for (var i = 0; i < _vertexIndicesList.Count; i += 3)
                    pointsList.Add(new Triplet<int>(
                        _vertexIndicesList[i], 
                        _vertexIndicesList[i + 1], 
                        _vertexIndicesList[i + 2]
                    ));

                return pointsList;
            }
        }


        private GrTriangleGeometry3D(IReadOnlyList<IFloat64Vector3D> pointsList)
            : base(pointsList)
        {
            _vertexIndicesList = new List<int>();
        }
        
        private GrTriangleGeometry3D(IReadOnlyList<IFloat64Vector3D> pointsList, List<int> vertexIndicesList)
            : base(pointsList)
        {
            _vertexIndicesList = vertexIndicesList;
        }
        
        
        public GrTriangleGeometry3D ClearTriangles()
        {
            _vertexIndicesList.Clear();

            return this;
        }

        public GrTriangleGeometry3D RemoveTriangle(int triangleIndex)
        {
            if (triangleIndex < 0 || 3 * triangleIndex >= VertexCount)
                return this;

            _vertexIndicesList.RemoveAt(2 * triangleIndex);
            _vertexIndicesList.RemoveAt(2 * triangleIndex);
            _vertexIndicesList.RemoveAt(2 * triangleIndex);

            return this;
        }


        public GrTriangleGeometry3D AppendTriangle(int vertexIndex1, int vertexIndex2, int vertexIndex3)
        {
            vertexIndex1 = vertexIndex1.Mod(VertexCount);
            vertexIndex2 = vertexIndex2.Mod(VertexCount);
            vertexIndex3 = vertexIndex3.Mod(VertexCount);

            if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                throw new InvalidOperationException();

            _vertexIndicesList.Add(vertexIndex1);
            _vertexIndicesList.Add(vertexIndex2);
            _vertexIndicesList.Add(vertexIndex3);

            return this;
        }

        public GrTriangleGeometry3D PrependTriangle(int vertexIndex1, int vertexIndex2, int vertexIndex3)
        {
            vertexIndex1 = vertexIndex1.Mod(VertexCount);
            vertexIndex2 = vertexIndex2.Mod(VertexCount);
            vertexIndex3 = vertexIndex3.Mod(VertexCount);

            if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                throw new InvalidOperationException();

            _vertexIndicesList.Insert(0, vertexIndex1);
            _vertexIndicesList.Insert(1, vertexIndex2);
            _vertexIndicesList.Insert(2, vertexIndex3);

            return this;
        }

        public GrTriangleGeometry3D InsertTriangle(int triangleIndex, int vertexIndex1, int vertexIndex2, int vertexIndex3)
        {
            if (triangleIndex < 0 || 3 * triangleIndex >= VertexCount)
                throw new IndexOutOfRangeException(nameof(triangleIndex));

            vertexIndex1 = vertexIndex1.Mod(VertexCount);
            vertexIndex2 = vertexIndex2.Mod(VertexCount);
            vertexIndex3 = vertexIndex3.Mod(VertexCount);

            if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                throw new InvalidOperationException();

            var i = 3 * triangleIndex;
            _vertexIndicesList.Insert(i, vertexIndex1);
            _vertexIndicesList.Insert(i + 1, vertexIndex2);
            _vertexIndicesList.Insert(i + 2, vertexIndex3);

            return this;
        }


        public GrTriangleGeometry3D AppendTriangles(IEnumerable<int> vertexIndicesList)
        {
            var triangleVertexIndex = 0;
            var vertexIndex1 = 0;
            var vertexIndex2 = 1;

            foreach (var vertexIndex in vertexIndicesList)
            {
                if (triangleVertexIndex == 0)
                {
                    vertexIndex1 = vertexIndex.Mod(VertexCount);
                }
                else if (triangleVertexIndex == 1)
                {
                    vertexIndex2 = vertexIndex.Mod(VertexCount);
                }
                else
                {
                    var vertexIndex3 = vertexIndex.Mod(VertexCount);

                    if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                        throw new InvalidOperationException();

                    _vertexIndicesList.Add(vertexIndex1);
                    _vertexIndicesList.Add(vertexIndex2);
                    _vertexIndicesList.Add(vertexIndex3);
                }

                triangleVertexIndex = (triangleVertexIndex + 1) % 3;
            }

            return this;
        }
        
        public GrTriangleGeometry3D PrependTriangles(IEnumerable<int> vertexIndicesList)
        {
            var triangleVertexIndex = 0;
            var vertexIndex1 = 0;
            var vertexIndex2 = 1;

            var index = 0;
            foreach (var vertexIndex in vertexIndicesList)
            {
                if (triangleVertexIndex == 0)
                {
                    vertexIndex1 = vertexIndex.Mod(VertexCount);
                }
                else if (triangleVertexIndex == 1)
                {
                    vertexIndex2 = vertexIndex.Mod(VertexCount);
                }
                else
                {
                    var vertexIndex3 = vertexIndex.Mod(VertexCount);

                    if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                        throw new InvalidOperationException();

                    _vertexIndicesList.Insert(index, vertexIndex1);
                    _vertexIndicesList.Insert(index + 1, vertexIndex2);
                    _vertexIndicesList.Insert(index + 2, vertexIndex3);
                    index += 3;
                }

                triangleVertexIndex = (triangleVertexIndex + 1) % 3;
            }

            return this;
        }
        
        public GrTriangleGeometry3D InsertTriangles(int triangleIndex, IEnumerable<int> vertexIndicesList)
        {
            if (triangleIndex < 0 || 3 * triangleIndex >= VertexCount)
                throw new IndexOutOfRangeException(nameof(triangleIndex));

            var triangleVertexIndex = 0;
            var vertexIndex1 = 0;
            var vertexIndex2 = 1;

            var index = 3 * triangleIndex;
            foreach (var vertexIndex in vertexIndicesList)
            {
                if (triangleVertexIndex == 0)
                {
                    vertexIndex1 = vertexIndex.Mod(VertexCount);
                }
                else if (triangleVertexIndex == 1)
                {
                    vertexIndex2 = vertexIndex.Mod(VertexCount);
                }
                else
                {
                    var vertexIndex3 = vertexIndex.Mod(VertexCount);

                    if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                        throw new InvalidOperationException();

                    _vertexIndicesList.Insert(index, vertexIndex1);
                    _vertexIndicesList.Insert(index + 1, vertexIndex2);
                    _vertexIndicesList.Insert(index + 2, vertexIndex3);
                    index += 3;
                }

                triangleVertexIndex = (triangleVertexIndex + 1) % 3;
            }

            return this;
        }


        public GrTriangleGeometry3D AppendTriangles(IEnumerable<ITriplet<int>> vertexIndicesList)
        {
            foreach (var vertexIndexPair in vertexIndicesList)
            {
                var vertexIndex1 = vertexIndexPair.Item1.Mod(VertexCount);
                var vertexIndex2 = vertexIndexPair.Item2.Mod(VertexCount);
                var vertexIndex3 = vertexIndexPair.Item3.Mod(VertexCount);

                if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                    throw new InvalidOperationException();

                _vertexIndicesList.Add(vertexIndex1);
                _vertexIndicesList.Add(vertexIndex2);
                _vertexIndicesList.Add(vertexIndex3);
            }

            return this;
        }

        public GrTriangleGeometry3D PrependTriangles(IEnumerable<ITriplet<int>> vertexIndicesList)
        {
            var index = 0;
            foreach (var vertexIndexPair in vertexIndicesList)
            {
                var vertexIndex1 = vertexIndexPair.Item1.Mod(VertexCount);
                var vertexIndex2 = vertexIndexPair.Item2.Mod(VertexCount);
                var vertexIndex3 = vertexIndexPair.Item3.Mod(VertexCount);

                if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1, vertexIndex2);
                _vertexIndicesList.Insert(index + 2, vertexIndex3);
                index += 3;
            }

            return this;
        }

        public GrTriangleGeometry3D InsertTriangles(int triangleIndex, IEnumerable<ITriplet<int>> vertexIndicesList)
        {
            if (triangleIndex < 0 || 3 * triangleIndex >= VertexCount)
                throw new IndexOutOfRangeException(nameof(triangleIndex));

            var index = 3 * triangleIndex;
            foreach (var vertexIndexPair in vertexIndicesList)
            {
                var vertexIndex1 = vertexIndexPair.Item1.Mod(VertexCount);
                var vertexIndex2 = vertexIndexPair.Item2.Mod(VertexCount);
                var vertexIndex3 = vertexIndexPair.Item3.Mod(VertexCount);

                if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex1 == vertexIndex3)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1, vertexIndex2);
                _vertexIndicesList.Insert(index + 2, vertexIndex3);
                index += 3;
            }

            return this;
        }
        
        
        public override IEnumerator<ITriangle3D> GetEnumerator()
        {
            for (var i = 0; i < VertexCount; i += 3)
            {
                var point1 = GetGeometryPoint(_vertexIndicesList[i]);
                var point2 = GetGeometryPoint(_vertexIndicesList[i + 1]);
                var point3 = GetGeometryPoint(_vertexIndicesList[i + 2]);

                yield return Triangle3D.Create(point1, point2, point3);
            }
        }
    }
}