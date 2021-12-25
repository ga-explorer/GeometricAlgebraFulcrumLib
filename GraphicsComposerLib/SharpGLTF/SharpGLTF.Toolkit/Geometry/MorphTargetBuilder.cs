﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

using SharpGLTF.Geometry.VertexTypes;

namespace SharpGLTF.Geometry
{
    public interface IPrimitiveMorphTargetReader
    {
        /// <summary>
        /// Gets the collection of vertex indices that have morph target deltas.
        /// </summary>
        /// <returns>A collection of vertex indices.</returns>
        IReadOnlyCollection<int> GetTargetIndices();

        /// <summary>
        /// Gets the vertex for the given <paramref name="vertexIndex"/> morphed by the current morph target (if any).
        /// </summary>
        /// <param name="vertexIndex">The index of the vertex.</param>
        /// <returns>If the given index has a morphed vertex, it will return it, else ir will return the base vertex.</returns>
        IVertexGeometry GetVertex(int vertexIndex);

        /// <summary>
        /// Gets the <see cref="VertexGeometryDelta"/> of a given vertex for a given morph target.
        /// </summary>
        /// <param name="vertexIndex">The index of the vertex.</param>
        /// <returns>A Vertex delta (Morphed vertex minus base vertex).</returns>
        VertexGeometryDelta GetVertexDelta(int vertexIndex);
    }

    /// <summary>
    /// Represents the vertex deltas of a specific morph target.
    /// <see cref="PrimitiveBuilder{TMaterial, TvG, TvM, TvS}._UseMorphTarget(int)"/>
    /// </summary>
    /// <typeparam name="TvG">The vertex fragment type with Position, Normal and Tangent.</typeparam>
    sealed class PrimitiveMorphTargetBuilder<TvG> : IPrimitiveMorphTargetReader
        where TvG : struct, IVertexGeometry
    {
        #region lifecycle

        internal PrimitiveMorphTargetBuilder(Func<int, TvG> baseVertexFunc)
        {
            this._BaseVertexFunc = baseVertexFunc;
            this._MorphVertices = new Dictionary<int, TvG>();
        }

        internal PrimitiveMorphTargetBuilder(Func<int, TvG> baseVertexFunc, PrimitiveMorphTargetBuilder<TvG> other)
        {
            this._BaseVertexFunc = baseVertexFunc;
            this._MorphVertices = new Dictionary<int, TvG>(other._MorphVertices);
        }

        #endregion

        #region data

        private readonly Func<int, TvG> _BaseVertexFunc;

        private readonly Dictionary<int, TvG> _MorphVertices;

        #endregion

        #region API

        public IReadOnlyCollection<int> GetTargetIndices()
        {
            return _MorphVertices.Keys;
        }

        public VertexGeometryDelta GetVertexDelta(int vertexIndex)
        {
            if (_MorphVertices.TryGetValue(vertexIndex, out TvG value))
            {
                return value.Subtract(_BaseVertexFunc(vertexIndex));
            }

            return default;
        }

        public void SetVertexDelta(int vertexIndex, VertexGeometryDelta value)
        {
            if (object.Equals(value, default(VertexGeometryDelta)))
            {
                _RemoveVertex(vertexIndex);
                return;
            }

            var vertex = _BaseVertexFunc(vertexIndex);
            vertex.Add(value);

            _SetVertex(vertexIndex, vertex);
        }

        IVertexGeometry IPrimitiveMorphTargetReader.GetVertex(int vertexIndex)
        {
            return _MorphVertices.TryGetValue(vertexIndex, out TvG value) ? value : _BaseVertexFunc(vertexIndex);
        }

        public TvG GetVertex(int vertexIndex)
        {
            return _MorphVertices.TryGetValue(vertexIndex, out TvG value) ? value : _BaseVertexFunc(vertexIndex);
        }

        public void SetVertex(int vertexIndex, TvG value)
        {
            if (object.Equals(value, _BaseVertexFunc(vertexIndex)))
            {
                _RemoveVertex(vertexIndex);
                return;
            }

            _SetVertex(vertexIndex, value);
        }

        private void _SetVertex(int vertexIndex, TvG value)
        {
            _MorphVertices[vertexIndex] = value;
        }

        private void _RemoveVertex(int vertexIndex)
        {
            _MorphVertices.Remove(vertexIndex);
        }

        #endregion

        #region internals

        internal void TransformVertices(Func<TvG, TvG> vertexFunc)
        {
            foreach (var vidx in _MorphVertices.Keys)
            {
                var g = GetVertex(vidx);

                g = vertexFunc(g);

                SetVertex(vidx, g);
            }
        }

        internal void SetMorphTargets(IPrimitiveMorphTargetReader other, IReadOnlyDictionary<int, int> vertexMap, Func<IVertexGeometry, TvG> vertexFunc)
        {
            Guard.NotNull(vertexFunc, nameof(vertexFunc));

            var indices = other.GetTargetIndices();

            foreach (var srcVidx in indices)
            {
                var g = vertexFunc(other.GetVertex(srcVidx));

                var dstVidx = srcVidx;

                if (vertexMap != null)
                {
                    if (!vertexMap.TryGetValue(srcVidx, out dstVidx)) dstVidx = -1;
                }

                if (dstVidx >= 0) this.SetVertex(dstVidx, g);
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents the vertex deltas of a specific morph target.
    /// <see cref="IMeshBuilder{TMaterial}.UseMorphTarget(int)"/>
    /// </summary>
    public interface IMorphTargetBuilder
    {
        IReadOnlyCollection<Vector3> Positions { get; }
        IReadOnlyCollection<IVertexGeometry> Vertices { get; }
        IReadOnlyList<IVertexGeometry> GetVertices(Vector3 position);

        /// <summary>
        /// Sets an absolute morph target.
        /// </summary>
        /// <param name="meshVertex">The base mesh vertex to morph.</param>
        /// <param name="morphVertex">The morphed vertex.</param>
        void SetVertex(IVertexGeometry meshVertex, IVertexGeometry morphVertex);

        /// <summary>
        /// Sets a relative morph target
        /// </summary>
        /// <param name="meshVertex">The base mesh vertex to morph.</param>
        /// <param name="delta">The offset from <paramref name="meshVertex"/> to morph.</param>
        void SetVertexDelta(IVertexGeometry meshVertex, VertexGeometryDelta delta);

        /// <summary>
        /// Sets a relative morph target to all base mesh vertices matching <paramref name="meshPosition"/>.
        /// </summary>
        /// <param name="meshPosition">The base vertex position.</param>
        /// <param name="delta">The offset to apply to each matching vertex found.</param>
        void SetVertexDelta(Vector3 meshPosition, VertexGeometryDelta delta);
    }

    /// <summary>
    /// Represents the vertex deltas of a specific morph target.
    /// <see cref="MeshBuilder{TMaterial, TvG, TvM, TvS}.UseMorphTarget(int)"/>
    /// </summary>
    /// <typeparam name="TMaterial">The material type used by the base mesh.</typeparam>
    /// <typeparam name="TvG">The vertex geometry type used by the base mesh.</typeparam>
    /// <typeparam name="TvS">The vertex skinning type used by the base mesh.</typeparam>
    /// <typeparam name="TvM">The vertex material type used by the base mesh.</typeparam>
    /// <remarks>
    /// Morph targets are stored separately on each <see cref="PrimitiveBuilder{TMaterial, TvG, TvM, TvS}"/>,
    /// so connecting vertices between two primitives might be duplicated. This means that when we set
    /// a displaced vertex, we must be sure we do so for all instances we can find.
    /// </remarks>
    public sealed class MorphTargetBuilder<TMaterial, TvG, TvS, TvM> : IMorphTargetBuilder
            where TvG : struct, IVertexGeometry
            where TvM : struct, IVertexMaterial
            where TvS : struct, IVertexSkinning
    {
        #region lifecycle

        internal MorphTargetBuilder(MeshBuilder<TMaterial, TvG, TvM, TvS> mesh, int morphTargetIndex)
        {
            _Mesh = mesh;
            _MorphTargetIndex = morphTargetIndex;

            foreach (var prim in _Mesh.Primitives)
            {
                for (int vidx = 0; vidx < prim.Vertices.Count; ++vidx)
                {
                    var key = prim.Vertices[vidx].Geometry;

                    if (!_Vertices.TryGetValue(key, out List<(PrimitiveBuilder<TMaterial, TvG, TvM, TvS>, int)> val))
                    {
                        _Vertices[key] = val = new List<(PrimitiveBuilder<TMaterial, TvG, TvM, TvS>, int)>();
                    }

                    val.Add((prim, vidx));

                    if (!_Positions.TryGetValue(key.GetPosition(), out List<TvG> geos))
                    {
                        _Positions[key.GetPosition()] = geos = new List<TvG>();
                    }

                    geos.Add(key);
                }
            }
        }

        #endregion

        #region data

        private readonly MeshBuilder<TMaterial, TvG, TvM, TvS> _Mesh;
        private readonly int _MorphTargetIndex;

        private readonly Dictionary<TvG, List<(PrimitiveBuilder<TMaterial, TvG, TvM, TvS>, int)>> _Vertices = new Dictionary<TvG, List<(PrimitiveBuilder<TMaterial, TvG, TvM, TvS>, int)>>();

        private readonly Dictionary<Vector3, List<TvG>> _Positions = new Dictionary<Vector3, List<TvG>>();

        #endregion

        #region properties

        public IReadOnlyCollection<Vector3> Positions => _Positions.Keys;

        public IReadOnlyCollection<TvG> Vertices => _Vertices.Keys;

        #endregion

        #region API

        public IReadOnlyList<TvG> GetVertices(Vector3 position)
        {
            return _Positions.TryGetValue(position, out List<TvG> geos) ? (IReadOnlyList<TvG>)geos : Array.Empty<TvG>();
        }

        public void SetVertexDelta(Vector3 key, VertexGeometryDelta delta)
        {
            if (_Positions.TryGetValue(key, out List<TvG> geos))
            {
                foreach (var g in geos) SetVertexDelta(g, delta);
            }
        }

        public void SetVertex(TvG meshVertex, TvG morphVertex)
        {
            if (_Vertices.TryGetValue(meshVertex, out List<(PrimitiveBuilder<TMaterial, TvG, TvM, TvS>, int)> val))
            {
                foreach (var entry in val)
                {
                    entry.Item1
                        ._UseMorphTarget(_MorphTargetIndex)
                        .SetVertex(entry.Item2, morphVertex);
                }
            }
        }

        public void SetVertexDelta(TvG meshVertex, VertexGeometryDelta delta)
        {
            if (_Vertices.TryGetValue(meshVertex, out List<(PrimitiveBuilder<TMaterial, TvG, TvM, TvS>, int)> val))
            {
                foreach (var entry in val)
                {
                    entry.Item1
                        ._UseMorphTarget(_MorphTargetIndex)
                        .SetVertexDelta(entry.Item2, delta);
                }
            }
        }

        #endregion

        #region IMorphTargetBuilder

        IReadOnlyCollection<IVertexGeometry> IMorphTargetBuilder.Vertices => (IReadOnlyList<IVertexGeometry>)(IReadOnlyCollection<TvG>)_Vertices.Keys;

        IReadOnlyList<IVertexGeometry> IMorphTargetBuilder.GetVertices(Vector3 position)
        {
            return _Positions.TryGetValue(position, out List<TvG> geos)
                ? (IReadOnlyList<IVertexGeometry>)geos
                : Array.Empty<IVertexGeometry>();
        }

        void IMorphTargetBuilder.SetVertex(IVertexGeometry meshVertex, IVertexGeometry morphVertex)
        {
            SetVertex(meshVertex.ConvertToGeometry<TvG>(), morphVertex.ConvertToGeometry<TvG>());
        }

        void IMorphTargetBuilder.SetVertexDelta(IVertexGeometry meshVertex, VertexGeometryDelta delta)
        {
            SetVertexDelta(meshVertex.ConvertToGeometry<TvG>(), delta);
        }

        #endregion
    }
}
