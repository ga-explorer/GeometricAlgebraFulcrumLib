using System.Collections.Generic;
using System.Linq;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Maps.Space2D
{
    public sealed class ComposedMap2D : IAffineMap2D
    {
        private readonly List<IAffineMap2D> _affineMapsList
            = new List<IAffineMap2D>();


        public ComposedMap2D()
        {
            _affineMapsList.Add(new IdentityMap2D());
        }

        public ComposedMap2D ResetToIdentity()
        {
            _affineMapsList.Clear();
            _affineMapsList.Add(new IdentityMap2D());

            return this;
        }

        public ComposedMap2D ResetToTranslation(double directionX, double directionY)
        {
            _affineMapsList.Clear();
            _affineMapsList.Add(new TranslationMap2D(directionX, directionY));

            return this;
        }

        //TODO: Complete the remaining maps

        public ComposedMap2D AppendMap(IAffineMap2D affineMap)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
                _affineMapsList.Clear();

            _affineMapsList.Add(affineMap);

            return this;
        }

        public ComposedMap2D AppendMaps(IEnumerable<IAffineMap2D> affineMapsList)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
                _affineMapsList.Clear();

            _affineMapsList.AddRange(affineMapsList);

            return this;
        }

        public ComposedMap2D PrependMap(IAffineMap2D affineMap)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
                _affineMapsList.Clear();

            _affineMapsList.Insert(0, affineMap);

            return this;
        }

        public ComposedMap2D PrependMaps(IEnumerable<IAffineMap2D> affineMapsList)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap2D)
                _affineMapsList.Clear();

            _affineMapsList.InsertRange(0, affineMapsList);

            return this;
        }

        public AffineMapMatrix3X3 ToMatrix()
        {
            //Construct matrix columns
            var c1 = MapVector(new Tuple2D(1, 0));
            var c2 = MapVector(new Tuple2D(0, 1));
            var c3 = MapPoint(new Tuple2D(0, 0));

            return new AffineMapMatrix3X3()
            {
                [0] = c1.X,
                [3] = c2.X,
                [6] = c3.X,
                [1] = c1.Y,
                [4] = c2.Y,
                [7] = c3.Y,
                [8] = 1.0
            };
        }

        public double[,] ToArray2D()
        {
            throw new System.NotImplementedException();
        }

        public ITuple2D MapPoint(ITuple2D point)
        {
            var p = point;

            foreach (var linearMap in _affineMapsList)
                p = linearMap.MapPoint(p);

            return p;
        }

        public ITuple2D MapVector(ITuple2D vector)
        {
            var v = vector;

            foreach (var linearMap in _affineMapsList)
                v = linearMap.MapVector(v);

            return v;
        }

        public ITuple2D MapNormal(ITuple2D normal)
        {
            var v = normal;

            foreach (var linearMap in _affineMapsList)
                v = linearMap.MapNormal(v);

            return v;
        }

        public IAffineMap2D InverseMap()
        {
            var invMap = new ComposedMap2D();

            invMap.AppendMaps(
                _affineMapsList
                .Select(m => m.InverseMap())
                .Reverse()
            );

            return invMap;
        }
    }
}
