﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DataStructuresLib.BitManipulation;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    public class ComposedMap3D :
        IAffineMap3D,
        IReadOnlyList<IAffineMap3D>
    {
        private readonly List<IAffineMap3D> _affineMapsList 
            = new List<IAffineMap3D>();


        public int Count 
            => _affineMapsList.Count;

        public IAffineMap3D this[int index] 
            => _affineMapsList[index];


        public ComposedMap3D()
        {
            _affineMapsList.Add(IdentityMap3D.DefaultMap);
        }


        public ComposedMap3D ResetToIdentity()
        {
            _affineMapsList.Clear();
            _affineMapsList.Add(IdentityMap3D.DefaultMap);

            return this;
        }

        public ComposedMap3D AppendAffineMap(IAffineMap3D affineMap)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3D)
                _affineMapsList.Clear();

            _affineMapsList.Add(affineMap);

            return this;
        }

        public ComposedMap3D AppendAffineMaps(IEnumerable<IAffineMap3D> affineMapsList)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3D)
                _affineMapsList.Clear();

            _affineMapsList.AddRange(affineMapsList);

            return this;
        }

        public ComposedMap3D PrependAffineMap(IAffineMap3D affineMap)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3D)
                _affineMapsList.Clear();

            _affineMapsList.Insert(0, affineMap);

            return this;
        }

        public ComposedMap3D PrependAffineMaps(IEnumerable<IAffineMap3D> affineMapsList)
        {
            if (_affineMapsList.Count == 1 && _affineMapsList[0] is IdentityMap3D)
                _affineMapsList.Clear();

            _affineMapsList.InsertRange(0, affineMapsList);

            return this;
        }

        public bool SwapsHandedness 
            => _affineMapsList.Count(m => m.SwapsHandedness).IsOdd();

        public SquareMatrix4 ToSquareMatrix4()
        {
            //Construct matrix columns
            var c0 = MapVector(new Tuple3D(1, 0, 0));
            var c1 = MapVector(new Tuple3D(0, 1, 0));
            var c2 = MapVector(new Tuple3D(0, 0, 1));
            var c3 = MapPoint(new Tuple3D(0, 0, 0));

            return new SquareMatrix4()
            {
                [0] = c0.X, [4] = c1.X, [8]  = c2.X, [12] = c3.X,
                [1] = c0.Y, [5] = c1.Y, [9] = c2.Y, [13] = c3.Y,
                [2] = c0.Z, [6] = c1.Z, [10] = c2.Z, [14] = c3.Z, [15] = 1d
            };
        }

        public Matrix4x4 ToMatrix4x4()
        {
            //Construct matrix columns
            var c0 = MapVector(Tuple3D.E1);
            var c1 = MapVector(Tuple3D.E2);
            var c2 = MapVector(Tuple3D.E3);
            var c3 = MapPoint(Tuple3D.Zero);

            return new Matrix4x4(
                (float) c0.X, (float) c1.X, (float) c2.X, (float) c3.X,
                (float) c0.Y, (float) c1.Y, (float) c2.Y, (float) c3.Y,
                (float) c0.Z, (float) c1.Z, (float) c2.Z, (float) c3.Z,
                0f, 0f, 0f, 1.0f
            );
        }

        public double[,] ToArray2D()
        {
            var c0 = MapVector(Tuple3D.E1);
            var c1 = MapVector(Tuple3D.E2);
            var c2 = MapVector(Tuple3D.E3);
            var c3 = MapPoint(Tuple3D.Zero);

            var array = new double[4, 4];

            array[0, 0] = c0.X; array[0, 1] = c1.X; array[0, 2] = c2.X; array[0, 3] = c3.X;
            array[1, 0] = c0.Y; array[1, 1] = c1.Y; array[1, 2] = c2.Y; array[1, 3] = c3.Y;
            array[2, 0] = c0.Z; array[2, 1] = c1.Z; array[2, 2] = c2.Z; array[2, 3] = c3.Z;
            array[3, 3] = 1d;

            return array;
        }

        public Tuple3D MapPoint(ITuple3D point)
        {
            return _affineMapsList.Aggregate(
                point.ToTuple3D(), 
                (current, linearMap) => linearMap.MapPoint(current)
            );
        }

        public Tuple3D MapVector(ITuple3D vector)
        {
            return _affineMapsList.Aggregate(
                vector.ToTuple3D(), 
                (current, linearMap) => linearMap.MapVector(current)
            );
        }

        public Tuple3D MapNormal(ITuple3D normal)
        {
            return _affineMapsList.Aggregate(
                normal.ToTuple3D(), 
                (current, linearMap) => linearMap.MapNormal(current)
            );
        }

        public IAffineMap3D InverseMap()
        {
            var invMap = new ComposedMap3D();

            foreach (var map in _affineMapsList)
                invMap.PrependAffineMap(map.InverseMap());

            return invMap;
        }

        public IEnumerator<IAffineMap3D> GetEnumerator()
        {
            return _affineMapsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
