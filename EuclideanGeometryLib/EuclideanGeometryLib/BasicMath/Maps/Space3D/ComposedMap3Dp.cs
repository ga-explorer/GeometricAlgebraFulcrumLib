using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Maps.Space3D
{
    public class ComposedMap3Dp : IAffineMap3D
    {
        private readonly List<IAffineMap3D> _affineMapsList 
            = new List<IAffineMap3D>();


        public ComposedMap3Dp()
        {
            _affineMapsList.Add(IdentityMap3Dp.Default);
        }

        public ComposedMap3Dp ResetToIdentity()
        {
            _affineMapsList.Clear();
            _affineMapsList.Add(IdentityMap3Dp.Default);

            return this;
        }

        public ComposedMap3Dp AppendAffineMap(IAffineMap3D affineMap)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3Dp)
                _affineMapsList.Clear();

            _affineMapsList.Add(affineMap);

            return this;
        }

        public ComposedMap3Dp AppendAffineMaps(IEnumerable<IAffineMap3D> affineMapsList)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3Dp)
                _affineMapsList.Clear();

            _affineMapsList.AddRange(affineMapsList);

            return this;
        }

        public ComposedMap3Dp PrependAffineMap(IAffineMap3D affineMap)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3Dp)
                _affineMapsList.Clear();

            _affineMapsList.Insert(0, affineMap);

            return this;
        }

        public ComposedMap3Dp PrependAffineMaps(IEnumerable<IAffineMap3D> affineMapsList)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3Dp)
                _affineMapsList.Clear();

            _affineMapsList.InsertRange(0, affineMapsList);

            return this;
        }

        public bool SwapsHandedness { get; }

        public Matrix4X4 ToMatrix()
        {
            //Construct matrix columns
            var c1 = MapVector(new Tuple3D(1, 0, 0));
            var c2 = MapVector(new Tuple3D(0, 1, 0));
            var c3 = MapVector(new Tuple3D(0, 0, 1));
            var c4 = MapPoint(new Tuple3D(0, 0, 0));

            return new Matrix4X4()
            {
                [0] = c1.X, [5] = c2.X, [9]  = c3.X, [13] = c4.X,
                [1] = c1.Y, [6] = c2.Y, [10] = c3.Y, [14] = c4.Y,
                [3] = c1.Z, [7] = c2.Z, [11] = c3.Z, [15] = c4.Z, [16] = 1.0
            };
        }

        public ITuple3D MapPoint(ITuple3D point)
        {
            var p = point;

            foreach (var linearMap in _affineMapsList)
                p = linearMap.MapPoint(p);

            return p;
        }

        public ITuple3D MapVector(ITuple3D vector)
        {
            var v = vector;

            foreach (var linearMap in _affineMapsList)
                v = linearMap.MapVector(v);

            return v;
        }

        public ITuple3D MapNormal(ITuple3D normal)
        {
            var v = normal;

            foreach (var linearMap in _affineMapsList)
                v = linearMap.MapNormal(v);

            return v;
        }

        public IAffineMap3D InverseMap()
        {
            throw new System.NotImplementedException();
        }
    }
}
