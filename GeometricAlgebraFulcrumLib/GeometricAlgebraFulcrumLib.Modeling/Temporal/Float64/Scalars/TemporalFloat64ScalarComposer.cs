using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars.Composed;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars
{
    public sealed class TemporalFloat64ScalarComposer :
        IReadOnlyList<TemporalFloat64Scalar>
    {
        private readonly List<TemporalFloat64Scalar> _scalarList
            = new List<TemporalFloat64Scalar>();


        public int Count
            => _scalarList.Count;

        public TemporalFloat64Scalar this[int index]
        {
            get => _scalarList[index];
            set => _scalarList[index] = value;
        }

        
        public TemporalFloat64ScalarComposer Clear()
        {
            _scalarList.Clear();

            return this;
        }

        public TemporalFloat64ScalarComposer RemoveAt(int index)
        {
            _scalarList.RemoveAt(index);

            return this;
        }


        public TemporalFloat64ScalarComposer AppendScalar(TemporalFloat64Scalar scalar)
        {
            if (scalar is TscList scalarList)
            {
                foreach (var s in scalarList)
                    AppendScalar(s);

                return this;
            }

            _scalarList.Add(scalar);

            return this;
        }
        
        public TemporalFloat64ScalarComposer PrependScalar(TemporalFloat64Scalar scalar)
        {
            if (scalar is TscList scalarList)
            {
                foreach (var s in scalarList.Reverse())
                    PrependScalar(s);

                return this;
            }

            _scalarList.Insert(0, scalar);

            return this;
        }
        
        public TemporalFloat64ScalarComposer InsertScalar(int index, TemporalFloat64Scalar scalar)
        {
            if (scalar is TscList scalarList)
            {
                foreach (var s in scalarList.Reverse())
                    InsertScalar(index, s);

                return this;
            }

            _scalarList.Insert(index, scalar);

            return this;
        }
        
        
        public TemporalFloat64ScalarComposer AppendScalar(TemporalFloat64Scalar scalar, double timeLength, int repeatCount = 1)
        {
            return AppendScalar(
                scalar.Repeat(repeatCount, 0, timeLength)
            );
        }
        
        public TemporalFloat64ScalarComposer PrependScalar(TemporalFloat64Scalar scalar, double timeLength, int repeatCount = 1)
        {
            return PrependScalar(
                scalar.Repeat(repeatCount, 0, timeLength)
            );
        }
        
        public TemporalFloat64ScalarComposer InsertScalar(int index, TemporalFloat64Scalar scalar, double timeLength, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                scalar.Repeat(repeatCount, 0, timeLength)
            );
        }

        
        public TemporalFloat64ScalarComposer AppendScalar(TemporalFloat64Scalar scalar, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                scalar.Repeat(repeatCount, 0, timeLength, value1, value2)
            );
        }
        
        public TemporalFloat64ScalarComposer PrependScalar(TemporalFloat64Scalar scalar, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                scalar.Repeat(repeatCount, 0, timeLength, value1, value2)
            );
        }
        
        public TemporalFloat64ScalarComposer InsertScalar(int index, TemporalFloat64Scalar scalar, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                scalar.Repeat(repeatCount, 0, timeLength, value1, value2)
            );
        }


        public TemporalFloat64ScalarComposer AppendConstant(double timeLength, double value)
        {
            _scalarList.Add(
                TemporalFloat64Scalar.Constant(
                    value,
                    0,
                    timeLength
                )
            );

            return this;
        }

        public TemporalFloat64ScalarComposer PrependConstant(double timeLength, double value)
        {
            _scalarList.Insert(
                0,
                TemporalFloat64Scalar.Constant(
                    value,
                    0,
                    timeLength
                )
            );

            return this;
        }

        public TemporalFloat64ScalarComposer InsertConstant(int index, double timeLength, double value)
        {
            _scalarList.Insert(
                index,
                TemporalFloat64Scalar.Constant(
                    value,
                    0,
                    timeLength
                )
            );

            return this;
        }
        
        
        public TemporalFloat64ScalarComposer AppendSharpStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.SharpStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependSharpStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.SharpStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertSharpStep(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.SharpStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }
        
        
        public TemporalFloat64ScalarComposer AppendSmoothStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.SmoothStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependSmoothStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.SmoothStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertSmoothStep(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.SmoothStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendSharpRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.SharpRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependSharpRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.SharpRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertSharpRectangle(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.SharpRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendSmoothRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.SmoothRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependSmoothRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.SmoothRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertSmoothRectangle(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.SmoothRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendRamp(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.Ramp(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependRamp(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.Ramp(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertRamp(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.Ramp(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendFullCos(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.FullCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependFullCos(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.FullCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertFullCos(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.FullCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendFullSin(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.FullSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependFullSin(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.FullSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertFullSin(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.FullSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendHalfCos(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.HalfCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependHalfCos(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.HalfCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertHalfCos(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.HalfCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendHalfSin(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.HalfSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependHalfSin(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.HalfSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertHalfSin(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.HalfSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendTriangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.Triangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependTriangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.Triangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertTriangle(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.Triangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        
        public TemporalFloat64ScalarComposer AppendTriangle(double vertexRelativeTime, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendScalar(
                TemporalFloat64Scalar.Triangle(vertexRelativeTime),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer PrependTriangle(double vertexRelativeTime, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependScalar(
                TemporalFloat64Scalar.Triangle(vertexRelativeTime),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public TemporalFloat64ScalarComposer InsertTriangle(int index, double vertexRelativeTime, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertScalar(
                index,
                TemporalFloat64Scalar.Triangle(vertexRelativeTime),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }
        
        
        public TemporalFloat64Scalar ComposeScalar()
        {
            if (_scalarList.Count == 0)
                return TemporalFloat64Scalar.Zero();

            if (_scalarList.Count == 1)
                return _scalarList[0];

            return _scalarList.Concat();
        }

        public TemporalFloat64Scalar ComposeScalar(double transitionTimeLength)
        {
            if (_scalarList.Count == 0)
                return TemporalFloat64Scalar.Zero();

            if (_scalarList.Count == 1)
                return _scalarList[0];

            return _scalarList.ConcatBlend(transitionTimeLength);
        }

        public IEnumerator<TemporalFloat64Scalar> GetEnumerator()
        {
            return _scalarList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
