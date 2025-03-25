using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Composers
{
    public sealed class Float64ScalarSignalComposer :
        IReadOnlyList<Float64ScalarSignal>
    {
        private readonly List<Float64ScalarSignal> _scalarList
            = new List<Float64ScalarSignal>();


        public int Count
            => _scalarList.Count;

        public Float64ScalarSignal this[int index]
        {
            get => _scalarList[index];
            set => _scalarList[index] = value;
        }


        public Float64ScalarSignalComposer Clear()
        {
            _scalarList.Clear();

            return this;
        }

        public Float64ScalarSignalComposer RemoveAt(int index)
        {
            _scalarList.RemoveAt(index);

            return this;
        }


        public Float64ScalarSignalComposer AppendSignal(Float64ScalarSignal scalar)
        {
            if (scalar is Float64ScalarListSignal scalarList)
            {
                foreach (var s in scalarList)
                    AppendSignal(s);

                return this;
            }

            _scalarList.Add(scalar);

            return this;
        }

        public Float64ScalarSignalComposer PrependSignal(Float64ScalarSignal scalar)
        {
            if (scalar is Float64ScalarListSignal scalarList)
            {
                foreach (var s in scalarList.Reverse())
                    PrependSignal(s);

                return this;
            }

            _scalarList.Insert(0, scalar);

            return this;
        }

        public Float64ScalarSignalComposer InsertSignal(int index, Float64ScalarSignal scalar)
        {
            if (scalar is Float64ScalarListSignal scalarList)
            {
                foreach (var s in scalarList.Reverse())
                    InsertSignal(index, s);

                return this;
            }

            _scalarList.Insert(index, scalar);

            return this;
        }


        public Float64ScalarSignalComposer AppendSignal(Float64ScalarSignal scalar, double timeLength, int repeatCount = 1)
        {
            return AppendSignal(
                scalar.Repeat(repeatCount, 0, timeLength)
            );
        }

        public Float64ScalarSignalComposer PrependSignal(Float64ScalarSignal scalar, double timeLength, int repeatCount = 1)
        {
            return PrependSignal(
                scalar.Repeat(repeatCount, 0, timeLength)
            );
        }

        public Float64ScalarSignalComposer InsertSignal(int index, Float64ScalarSignal scalar, double timeLength, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                scalar.Repeat(repeatCount, 0, timeLength)
            );
        }


        public Float64ScalarSignalComposer AppendSignal(Float64ScalarSignal scalar, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                scalar.Repeat(repeatCount, 0, timeLength, value1, value2)
            );
        }

        public Float64ScalarSignalComposer PrependSignal(Float64ScalarSignal scalar, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                scalar.Repeat(repeatCount, 0, timeLength, value1, value2)
            );
        }

        public Float64ScalarSignalComposer InsertSignal(int index, Float64ScalarSignal scalar, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                scalar.Repeat(repeatCount, 0, timeLength, value1, value2)
            );
        }


        public Float64ScalarSignalComposer AppendConstant(double timeLength, double value)
        {
            _scalarList.Add(
                Float64ScalarSignal.FiniteConstant(0, timeLength, value)
            );

            return this;
        }

        public Float64ScalarSignalComposer PrependConstant(double timeLength, double value)
        {
            _scalarList.Insert(
                0,
                Float64ScalarSignal.FiniteConstant(0, timeLength, value)
            );

            return this;
        }

        public Float64ScalarSignalComposer InsertConstant(int index, double timeLength, double value)
        {
            _scalarList.Insert(
                index,
                Float64ScalarSignal.FiniteConstant(0, timeLength, value)
            );

            return this;
        }


        public Float64ScalarSignalComposer AppendSharpStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteSharpStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependSharpStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteSharpStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertSharpStep(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteSharpStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendSmoothStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteSmoothStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependSmoothStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteSmoothStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertSmoothStep(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteSmoothStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendSharpRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteSharpRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependSharpRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteSharpRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertSharpRectangle(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteSharpRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendSmoothRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteSmoothRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependSmoothRectangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteSmoothRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertSmoothRectangle(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteSmoothRectangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendRamp(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteRamp(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependRamp(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteRamp(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertRamp(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteRamp(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendFullCos(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependFullCos(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertFullCos(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteCos(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendFullSin(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependFullSin(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertFullSin(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteSin(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendHalfSinStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteHalfSinStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependHalfSinStep(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteHalfSinStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertHalfSinStep(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteHalfSinStep(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendTriangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteTriangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependTriangle(double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteTriangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertTriangle(int index, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteTriangle(),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignalComposer AppendTriangle(double vertexRelativeTime, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return AppendSignal(
                Float64ScalarSignal.FiniteTriangle(vertexRelativeTime),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer PrependTriangle(double vertexRelativeTime, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return PrependSignal(
                Float64ScalarSignal.FiniteTriangle(vertexRelativeTime),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }

        public Float64ScalarSignalComposer InsertTriangle(int index, double vertexRelativeTime, double timeLength, double value1, double value2, int repeatCount = 1)
        {
            return InsertSignal(
                index,
                Float64ScalarSignal.FiniteTriangle(vertexRelativeTime),
                timeLength,
                value1,
                value2,
                repeatCount
            );
        }


        public Float64ScalarSignal ComposeScalar()
        {
            if (_scalarList.Count == 0)
                return Float64ScalarSignal.FiniteZero();

            if (_scalarList.Count == 1)
                return _scalarList[0];

            return _scalarList.Concat();
        }

        public Float64ScalarSignal ComposeScalar(double transitionTimeLength)
        {
            if (_scalarList.Count == 0)
                return Float64ScalarSignal.FiniteZero();

            if (_scalarList.Count == 1)
                return _scalarList[0];

            return _scalarList.ConcatBlend(transitionTimeLength);
        }

        public IEnumerator<Float64ScalarSignal> GetEnumerator()
        {
            return _scalarList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
