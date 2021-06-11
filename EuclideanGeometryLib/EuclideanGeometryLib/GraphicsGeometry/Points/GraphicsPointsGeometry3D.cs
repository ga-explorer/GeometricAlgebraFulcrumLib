using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Points
{
    public sealed class GraphicsPointsGeometry3D
        : IGraphicsPointsGeometry3D
    {
        public static GraphicsPointsGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            return new GraphicsPointsGeometry3D(pointsList);
        }

        public static GraphicsPointsGeometry3D Create(params ITuple3D[] pointsList)
        {
            return new GraphicsPointsGeometry3D(pointsList);
        }

        public static GraphicsPointsGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            return new GraphicsPointsGeometry3D(pointsList.ToArray());
        }


        public IReadOnlyList<ITuple3D> VertexPoints { get; }

        public IEnumerable<IGraphicsVertex3D> Vertices
            => VertexPoints.Select((p, i) => new GraphicsVertex3D(i, p));

        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.Points;

        private readonly List<int> _vertexIndicesList = new List<int>();
        public IReadOnlyList<int> VertexIndices
            => _vertexIndicesList;

        public int Count
            => _vertexIndicesList.Count;

        public ITuple3D this[int index] 
            => VertexPoints[_vertexIndicesList[index]];


        private GraphicsPointsGeometry3D(IReadOnlyList<ITuple3D> pointsList)
        {
            VertexPoints = pointsList;
        }


        public GraphicsPointsGeometry3D ClearPoints()
        {
            _vertexIndicesList.Clear();

            return this;
        }

        public GraphicsPointsGeometry3D RemovePoint(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex >= VertexPoints.Count)
                return this;

            _vertexIndicesList.RemoveAt(pointIndex);

            return this;
        }


        public GraphicsPointsGeometry3D AppendPoint(int vertexIndex)
        {
            vertexIndex = vertexIndex.Mod(VertexPoints.Count);

            _vertexIndicesList.Add(vertexIndex);

            return this;
        }

        public GraphicsPointsGeometry3D PrependPoint(int vertexIndex)
        {
            vertexIndex = vertexIndex.Mod(VertexPoints.Count);

            _vertexIndicesList.Insert(0, vertexIndex);

            return this;
        }

        public GraphicsPointsGeometry3D InsertPoint(int pointIndex, int vertexIndex)
        {
            if (pointIndex < 0 || pointIndex >= VertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(pointIndex));

            vertexIndex = vertexIndex.Mod(VertexPoints.Count);

            _vertexIndicesList.Insert(pointIndex, vertexIndex);

            return this;
        }


        public GraphicsPointsGeometry3D AppendPoints(IEnumerable<int> vertexIndicesList)
        {
            _vertexIndicesList.AddRange(
                vertexIndicesList.Select(i => i.Mod(VertexPoints.Count))
            );

            return this;
        }
        
        public GraphicsPointsGeometry3D AppendPoints(params int[] vertexIndicesList)
        {
            _vertexIndicesList.AddRange(
                vertexIndicesList.Select(i => i.Mod(VertexPoints.Count))
            );

            return this;
        }
        
        public GraphicsPointsGeometry3D PrependPoints(IEnumerable<int> vertexIndicesList)
        {
            _vertexIndicesList.InsertRange(
                0,
                vertexIndicesList.Select(i => i.Mod(VertexPoints.Count))
            );

            return this;
        }
        
        public GraphicsPointsGeometry3D PrependPoints(params int[] vertexIndicesList)
        {
            _vertexIndicesList.InsertRange(
                0,
                vertexIndicesList.Select(i => i.Mod(VertexPoints.Count))
            );

            return this;
        }
        
        public GraphicsPointsGeometry3D InsertPoints(int pointIndex, IEnumerable<int> vertexIndicesList)
        {
            if (pointIndex < 0 || pointIndex >= VertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(pointIndex));

            _vertexIndicesList.InsertRange(
                pointIndex,
                vertexIndicesList.Select(i => i.Mod(VertexPoints.Count))
            );

            return this;
        }
        
        public GraphicsPointsGeometry3D InsertPoints(int pointIndex, params int[] vertexIndicesList)
        {
            if (pointIndex < 0 || pointIndex >= VertexPoints.Count)
                throw new IndexOutOfRangeException(nameof(pointIndex));

            _vertexIndicesList.InsertRange(
                pointIndex,
                vertexIndicesList.Select(i => i.Mod(VertexPoints.Count))
            );

            return this;
        }

        
        public IEnumerator<ITuple3D> GetEnumerator()
        {
            return _vertexIndicesList.Select(
                i => VertexPoints[i]
            ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}