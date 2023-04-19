using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D
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

        public SquareMatrix3 ToSquareMatrix3()
        {
            //Construct matrix columns
            var c1 = MapVector(new Float64Tuple2D(1, 0));
            var c2 = MapVector(new Float64Tuple2D(0, 1));
            var c3 = MapPoint(new Float64Tuple2D(0, 0));

            return new SquareMatrix3()
            {
                Scalar00 = c1.X,
                Scalar01 = c2.X,
                Scalar02 = c3.X,
                Scalar10 = c1.Y,
                Scalar11 = c2.Y,
                Scalar12 = c3.Y,
                Scalar22 = 1.0
            };
        }

        public double[,] ToArray2D()
        {
            throw new NotImplementedException();
        }

        public Float64Tuple2D MapPoint(IFloat64Tuple2D point)
        {
            return _affineMapsList.Aggregate(
                point.ToTuple2D(), 
                (current, linearMap) => linearMap.MapPoint(current)
            );
        }

        public Float64Tuple2D MapVector(IFloat64Tuple2D vector)
        {
            return _affineMapsList.Aggregate(
                vector.ToTuple2D(), 
                (current, linearMap) => linearMap.MapVector(current)
            );
        }

        public Float64Tuple2D MapNormal(IFloat64Tuple2D normal)
        {
            return _affineMapsList.Aggregate(
                normal.ToTuple2D(), 
                (current, linearMap) => linearMap.MapNormal(current)
            );
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

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
