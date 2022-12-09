using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Collections;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives.Lines
{
    /// <summary>
    /// This class represents a set of lines based on a fixed set of points
    /// </summary>
    public sealed class GrLineGeometry3D
        : IGraphicsLineGeometry3D
    {
        public static GrLineGeometry3D Create(IReadOnlyList<IFloat64Tuple3D> pointsList)
        {
            if (pointsList.Count < 2)
                throw new ArgumentException();

            return new GrLineGeometry3D(pointsList);
        }

        public static GrLineGeometry3D Create(IReadOnlyList<IFloat64Tuple3D> pointsList, List<int> indicesList)
        {
            if (pointsList.Count < 2)
                throw new ArgumentException();

            return new GrLineGeometry3D(pointsList, indicesList);
        }

        public static GrLineGeometry3D Create(params IFloat64Tuple3D[] pointsList)
        {
            if (pointsList.Length < 2)
                throw new ArgumentException();

            return new GrLineGeometry3D(pointsList);
        }

        public static GrLineGeometry3D Create(IEnumerable<IFloat64Tuple3D> pointsList)
        {
            var pointsArray = pointsList.ToArray();

            if (pointsArray.Length < 2)
                throw new ArgumentException();

            return new GrLineGeometry3D(pointsArray);
        }

        public static GrLineGeometry3D Create(IEnumerable<IFloat64Tuple3D> pointsList, IEnumerable<int> indicesList)
        {
            var pointsArray = pointsList.ToArray();

            if (pointsArray.Length < 2)
                throw new ArgumentException();

            return new GrLineGeometry3D(pointsArray, indicesList.ToList());
        }
        
        public static GrLineGeometry3D Create(params ILineSegment3D[] lineSegmentsList)
        {
            var pointsList = new DistinctTuplesList3D();
            var vertexIndicesList = new List<int>();

            foreach (var lineSegment in lineSegmentsList)
            {
                var vertexIndex1 = pointsList.GetOrAddTupleIndex(
                    lineSegment.GetPoint1()
                );

                var vertexIndex2 = pointsList.GetOrAddTupleIndex(
                    lineSegment.GetPoint2()
                );

                vertexIndicesList.Add(vertexIndex1);
                vertexIndicesList.Add(vertexIndex2);
            }

            return new GrLineGeometry3D(pointsList, vertexIndicesList);
        }

        public static GrLineGeometry3D Create(IEnumerable<ILineSegment3D> lineSegmentsList)
        {
            var pointsList = new DistinctTuplesList3D();
            var vertexIndicesList = new List<int>();

            foreach (var lineSegment in lineSegmentsList)
            {
                var vertexIndex1 = pointsList.GetOrAddTupleIndex(
                    lineSegment.GetPoint1()
                );

                var vertexIndex2 = pointsList.GetOrAddTupleIndex(
                    lineSegment.GetPoint2()
                );

                vertexIndicesList.Add(vertexIndex1);
                vertexIndicesList.Add(vertexIndex2);
            }

            return new GrLineGeometry3D(pointsList, vertexIndicesList);
        }


        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.Lines;

        public int VertexCount 
            => _vertexPoints.Count;

        private readonly IReadOnlyList<IFloat64Tuple3D> _vertexPoints;
        public IEnumerable<IFloat64Tuple3D> GeometryPoints 
            => _vertexPoints;

        public IEnumerable<IGraphicsVertex3D> GeometryVertices
            => _vertexPoints.Select((p, i) => new GrVertex3D(i, p));

        private readonly List<int> _vertexIndicesList;
        public IEnumerable<int> GeometryIndices
            => _vertexIndicesList;

        public int Count
            => _vertexIndicesList.Count >> 1;

        public ILineSegment3D this[int index] 
            => LineSegment3D.Create(
                _vertexPoints[_vertexIndicesList[2 * index]],
                _vertexPoints[_vertexIndicesList[2 * index + 1]]
            );

        public IEnumerable<Pair<IFloat64Tuple3D>> LineVertexPoints
        {
            get
            {
                for (var i = 0; i < _vertexPoints.Count; i += 2)
                    yield return new Pair<IFloat64Tuple3D>(
                        _vertexPoints[_vertexIndicesList[i]],
                        _vertexPoints[_vertexIndicesList[i + 1]]
                    );
            }
        }

        public IEnumerable<Pair<int>> LineVertexIndices
        {
            get
            {
                for (var i = 0; i < _vertexPoints.Count; i += 2)
                    yield return new Pair<int>(
                        _vertexIndicesList[i], 
                        _vertexIndicesList[i + 1]
                    );
            }
        }


        private GrLineGeometry3D(IReadOnlyList<IFloat64Tuple3D> pointsList)
        {
            if (pointsList.Count < 2)
                throw new InvalidOperationException();

            _vertexPoints = pointsList;
            _vertexIndicesList = new List<int>();
        }

        private GrLineGeometry3D(IReadOnlyList<IFloat64Tuple3D> pointsList, List<int> vertexIndicesList)
        {
            if (pointsList.Count < 2 || vertexIndicesList.Count % 2 != 0)
                throw new InvalidOperationException();

            _vertexPoints = pointsList;
            _vertexIndicesList = vertexIndicesList;
        }

        
        public IFloat64Tuple3D GetGeometryPoint(int index)
        {
            return _vertexPoints[index];
        }


        public GrLineGeometry3D ClearLines()
        {
            _vertexIndicesList.Clear();

            return this;
        }

        public GrLineGeometry3D RemoveLine(int lineIndex)
        {
            if (lineIndex < 0 || 2 * lineIndex >= _vertexPoints.Count)
                return this;

            _vertexIndicesList.RemoveAt(2 * lineIndex);
            _vertexIndicesList.RemoveAt(2 * lineIndex);

            return this;
        }


        public GrLineGeometry3D AppendLine(int vertexIndex1, int vertexIndex2)
        {
            vertexIndex1 = vertexIndex1.Mod(_vertexPoints.Count);
            vertexIndex2 = vertexIndex2.Mod(_vertexPoints.Count);

            if (vertexIndex1 == vertexIndex2)
                throw new InvalidOperationException();

            _vertexIndicesList.Add(vertexIndex1);
            _vertexIndicesList.Add(vertexIndex2);

            return this;
        }

        public GrLineGeometry3D PrependLine(int vertexIndex1, int vertexIndex2)
        {
            vertexIndex1 = vertexIndex1.Mod(_vertexPoints.Count);
            vertexIndex2 = vertexIndex2.Mod(_vertexPoints.Count);

            if (vertexIndex1 == vertexIndex2)
                throw new InvalidOperationException();

            _vertexIndicesList.Insert(0, vertexIndex2);
            _vertexIndicesList.Insert(0, vertexIndex1);

            return this;
        }

        public GrLineGeometry3D InsertLine(int lineIndex, int vertexIndex1, int vertexIndex2)
        {
            if (lineIndex < 0 || 2 * lineIndex >= _vertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(lineIndex));

            vertexIndex1 = vertexIndex1.Mod(_vertexPoints.Count);
            vertexIndex2 = vertexIndex2.Mod(_vertexPoints.Count);

            if (vertexIndex1 == vertexIndex2)
                throw new InvalidOperationException();

            var i = lineIndex * 2;
            _vertexIndicesList.Insert(i, vertexIndex2);
            _vertexIndicesList.Insert(i, vertexIndex1);

            return this;
        }


        public GrLineGeometry3D AppendLines(IEnumerable<int> vertexIndicesList)
        {
            var firstIndexFlag = true;
            var vertexIndex1 = 0;

            foreach (var vertexIndex in vertexIndicesList)
            {
                if (firstIndexFlag)
                {
                    vertexIndex1 = vertexIndex.Mod(_vertexPoints.Count);
                }
                else
                {
                    var vertexIndex2 = vertexIndex.Mod(_vertexPoints.Count);

                    if (vertexIndex1 == vertexIndex2)
                        throw new InvalidOperationException();

                    _vertexIndicesList.Add(vertexIndex1);
                    _vertexIndicesList.Add(vertexIndex2);
                }

                firstIndexFlag = !firstIndexFlag;
            }

            return this;
        }
        
        public GrLineGeometry3D PrependLines(IEnumerable<int> vertexIndicesList)
        {
            var firstIndexFlag = true;
            var vertexIndex1 = 0;

            var i = 0;
            foreach (var vertexIndex in vertexIndicesList)
            {
                if (firstIndexFlag)
                {
                    vertexIndex1 = vertexIndex.Mod(_vertexPoints.Count);
                }
                else
                {
                    var vertexIndex2 = vertexIndex.Mod(_vertexPoints.Count);

                    if (vertexIndex1 == vertexIndex2)
                        throw new InvalidOperationException();

                    _vertexIndicesList.Insert(i, vertexIndex1);
                    _vertexIndicesList.Insert(i + 1, vertexIndex2);
                    i += 2;
                }

                firstIndexFlag = !firstIndexFlag;
            }

            return this;
        }
        
        public GrLineGeometry3D InsertLines(int lineIndex, IEnumerable<int> vertexIndicesList)
        {
            if (lineIndex < 0 || 2 * lineIndex >= _vertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(lineIndex));

            var firstIndexFlag = true;
            var vertexIndex1 = 0;

            var i = 2 * lineIndex;
            foreach (var vertexIndex in vertexIndicesList)
            {
                if (firstIndexFlag)
                {
                    vertexIndex1 = vertexIndex.Mod(_vertexPoints.Count);
                }
                else
                {
                    var vertexIndex2 = vertexIndex.Mod(_vertexPoints.Count);

                    if (vertexIndex1 == vertexIndex2)
                        throw new InvalidOperationException();

                    _vertexIndicesList.Insert(i, vertexIndex1);
                    _vertexIndicesList.Insert(i + 1, vertexIndex2);
                    i += 2;
                }

                firstIndexFlag = !firstIndexFlag;
            }

            return this;
        }


        public GrLineGeometry3D AppendLines(IEnumerable<Pair<int>> vertexIndicesList)
        {
            foreach (var vertexIndexPair in vertexIndicesList)
            {
                var vertexIndex1 = vertexIndexPair.Item1.Mod(_vertexPoints.Count);
                var vertexIndex2 = vertexIndexPair.Item2.Mod(_vertexPoints.Count);

                if (vertexIndex1 == vertexIndex2)
                    throw new InvalidOperationException();

                _vertexIndicesList.Add(vertexIndex1);
                _vertexIndicesList.Add(vertexIndex2);
            }

            return this;
        }

        public GrLineGeometry3D PrependLines(IEnumerable<Pair<int>> vertexIndicesList)
        {
            var index = 0;
            foreach (var vertexIndexPair in vertexIndicesList)
            {
                var vertexIndex1 = vertexIndexPair.Item1.Mod(_vertexPoints.Count);
                var vertexIndex2 = vertexIndexPair.Item2.Mod(_vertexPoints.Count);

                if (vertexIndex1 == vertexIndex2)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1, vertexIndex2);
                index += 2;
            }

            return this;
        }

        public GrLineGeometry3D InsertLines(int lineIndex, IEnumerable<Pair<int>> vertexIndicesList)
        {
            if (lineIndex < 0 || 2 * lineIndex >= _vertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(lineIndex));

            var index = 2 * lineIndex;
            foreach (var vertexIndexPair in vertexIndicesList)
            {
                var vertexIndex1 = vertexIndexPair.Item1.Mod(_vertexPoints.Count);
                var vertexIndex2 = vertexIndexPair.Item2.Mod(_vertexPoints.Count);

                if (vertexIndex1 == vertexIndex2)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1, vertexIndex2);
                index += 2;
            }

            return this;
        }
        

        public GrLineGeometry3D AppendPolyLine(IReadOnlyList<int> vertexIndicesList, bool closedPolyline)
        {
            var firstVertexIndex = vertexIndicesList[0].Mod(_vertexPoints.Count);
            var vertexIndex1 = firstVertexIndex;
            var vertexIndices = vertexIndicesList.Skip(1).Select(i => i.Mod(_vertexPoints.Count));

            foreach (var vertexIndex2 in vertexIndices)
            {
                if (vertexIndex1 == vertexIndex2)
                    throw new InvalidOperationException();

                _vertexIndicesList.Add(vertexIndex1);
                _vertexIndicesList.Add(vertexIndex2);

                vertexIndex1 = vertexIndex2;
            }

            if (closedPolyline)
            {
                if (vertexIndex1 == firstVertexIndex)
                    throw new InvalidOperationException();

                _vertexIndicesList.Add(vertexIndex1);
                _vertexIndicesList.Add(firstVertexIndex);
            }

            return this;
        }

        public GrLineGeometry3D PrependPolyLine(IReadOnlyList<int> vertexIndicesList, bool closedPolyline)
        {
            var firstVertexIndex = vertexIndicesList[0].Mod(_vertexPoints.Count);
            var vertexIndex1 = firstVertexIndex;
            var vertexIndices = vertexIndicesList.Skip(1).Select(i => i.Mod(_vertexPoints.Count));

            var index = 0;
            foreach (var vertexIndex2 in vertexIndices)
            {
                if (vertexIndex1 == vertexIndex2)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1,vertexIndex2);
                index += 2;

                vertexIndex1 = vertexIndex2;
            }

            if (closedPolyline)
            {
                if (vertexIndex1 == firstVertexIndex)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1, firstVertexIndex);
            }

            return this;
        }

        public GrLineGeometry3D InsertPolyLine(int lineIndex, IReadOnlyList<int> vertexIndicesList, bool closedPolyline)
        {
            if (lineIndex < 0 || 2 * lineIndex >= _vertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(lineIndex));

            var firstVertexIndex = vertexIndicesList[0].Mod(_vertexPoints.Count);
            var vertexIndex1 = firstVertexIndex;
            var vertexIndices = vertexIndicesList.Skip(1).Select(i => i.Mod(_vertexPoints.Count));

            var index = 2 * lineIndex;
            foreach (var vertexIndex2 in vertexIndices)
            {
                if (vertexIndex1 == vertexIndex2)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1,vertexIndex2);
                index += 2;

                vertexIndex1 = vertexIndex2;
            }

            if (closedPolyline)
            {
                if (vertexIndex1 == firstVertexIndex)
                    throw new InvalidOperationException();

                _vertexIndicesList.Insert(index, vertexIndex1);
                _vertexIndicesList.Insert(index + 1, firstVertexIndex);
            }

            return this;
        }


        public GrLineGeometry3D AppendPolyLine(int firstVertexIndex, int vertexCount, bool closedPolyline)
        {
            if (vertexCount < 2 && vertexCount > -2)
                throw new InvalidOperationException();

            var step = vertexCount > 0 ? 1 : -1;
            if (vertexCount < 0)
                vertexCount = -vertexCount;

            firstVertexIndex = firstVertexIndex.Mod(_vertexPoints.Count);
            var vertexIndex1 = firstVertexIndex;

            for (var i = 1; i < vertexCount; i++)
            {
                var vertexIndex2 = (vertexIndex1 + step).Mod(_vertexPoints.Count);

                _vertexIndicesList.Add(vertexIndex1);
                _vertexIndicesList.Add(vertexIndex2);

                vertexIndex1 = vertexIndex2;
                
                if (vertexIndex1 == firstVertexIndex)
                    break;
            
            }

            if (closedPolyline && vertexIndex1 != firstVertexIndex)
            {
                _vertexIndicesList.Add(vertexIndex1);
                _vertexIndicesList.Add(firstVertexIndex);
            }

            return this;
        }

        public GrLineGeometry3D PrependPolyLine(int firstVertexIndex, int vertexCount, bool closedPolyline)
        {
            if (vertexCount < 2 && vertexCount > -2)
                throw new InvalidOperationException();

            var step = vertexCount > 0 ? 1 : -1;
            if (vertexCount < 0)
                vertexCount = -vertexCount;

            firstVertexIndex = firstVertexIndex.Mod(_vertexPoints.Count);
            var vertexIndex1 = firstVertexIndex;

            var index = 0;
            for (var i = 1; i < vertexCount; i++)
            {
                var vertexIndex2 = (vertexIndex1 + step).Mod(_vertexPoints.Count);

                _vertexIndicesList.Insert(index, vertexIndex2);
                _vertexIndicesList.Insert(index + 1, vertexIndex1);
                index += 2;

                vertexIndex1 = vertexIndex2;
                
                if (vertexIndex1 == firstVertexIndex)
                    break;
            }

            if (closedPolyline && vertexIndex1 != firstVertexIndex)
            {
                _vertexIndicesList.Insert(index, firstVertexIndex);
                _vertexIndicesList.Insert(index + 1, vertexIndex1);
            }

            return this;
        }

        public GrLineGeometry3D InsertPolyLine(int lineIndex, int firstVertexIndex, int vertexCount, bool closedPolyline)
        {
            if (lineIndex < 0 || 2 * lineIndex >= _vertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(lineIndex));

            if (vertexCount < 2 && vertexCount > -2)
                throw new InvalidOperationException();

            var step = vertexCount > 0 ? 1 : -1;
            if (vertexCount < 0)
                vertexCount = -vertexCount;

            firstVertexIndex = firstVertexIndex.Mod(_vertexPoints.Count);
            var vertexIndex1 = firstVertexIndex;

            var index = 2 * lineIndex;
            for (var i = 1; i < vertexCount; i++)
            {
                var vertexIndex2 = (vertexIndex1 + step).Mod(_vertexPoints.Count);

                _vertexIndicesList.Insert(index, vertexIndex2);
                _vertexIndicesList.Insert(index + 1, vertexIndex1);
                index += 2;

                vertexIndex1 = vertexIndex2;
                
                if (vertexIndex1 == firstVertexIndex)
                    break;
            
            }

            if (closedPolyline && vertexIndex1 != firstVertexIndex)
            {
                _vertexIndicesList.Insert(index, firstVertexIndex);
                _vertexIndicesList.Insert(index + 1, vertexIndex1);
            }

            return this;
        }


        public IEnumerator<ILineSegment3D> GetEnumerator()
        {
            for (var i = 0; i < _vertexPoints.Count; i += 2)
            {
                var point1 = _vertexPoints[_vertexIndicesList[i]];
                var point2 = _vertexPoints[_vertexIndicesList[i + 1]];

                yield return LineSegment3D.Create(point1, point2);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}