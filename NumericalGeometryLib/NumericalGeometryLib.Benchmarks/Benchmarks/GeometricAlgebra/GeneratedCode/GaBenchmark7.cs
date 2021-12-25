using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicOperations;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using NumericalGeometryLib.Random;

namespace NumericalGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra.GeneratedCode
{
    public class GaBenchmark7
    {
        public sealed class InternalComputerValues
        {
            public double D00 { get; internal set; }

            public double D10 { get; internal set; }
            public double D11 { get; internal set; }
            public double D12 { get; internal set; }

            public double D20 { get; internal set; }
            public double D21 { get; internal set; }
            public double D22 { get; internal set; }

            public int Region1 { get; internal set; }
            public int Region2 { get; internal set; }

            public double T0 { get; internal set; }
            public double T1 { get; internal set; }
            public double T2 { get; internal set; }

            public bool Stage2Flag { get; internal set; }

            public bool HasIntersection { get; internal set; }


            internal InternalComputerValues()
            {
            }


            private double GetMaxAbs(double v1, double v2)
                => Math.Max(v1, Math.Abs(v2));

            public double GetMaxAbsDifference(InternalComputerValues values)
            {
                var maxDiff = 0.0d;

                //maxDiff = GetMaxAbs(maxDiff, D00 - values.D00);

                //maxDiff = GetMaxAbs(maxDiff, D10 - values.D10);
                //maxDiff = GetMaxAbs(maxDiff, D11 - values.D11);
                //maxDiff = GetMaxAbs(maxDiff, D12 - values.D12);

                //maxDiff = GetMaxAbs(maxDiff, D20 - values.D20);
                //maxDiff = GetMaxAbs(maxDiff, D21 - values.D21);
                //maxDiff = GetMaxAbs(maxDiff, D22 - values.D22);

                maxDiff = GetMaxAbs(maxDiff, T0 - values.T0);
                maxDiff = GetMaxAbs(maxDiff, T1 - values.T1);
                maxDiff = GetMaxAbs(maxDiff, T2 - values.T2);

                return maxDiff;
            }
        }


        private double _d00;
        private double _d10, _d11, _d12;
        private double _d20, _d21, _d22;
        private int _region1, _region2;
        private double _t0, _t1, _t2;


        private readonly List<Tuple2D> _pointsList 
            = new List<Tuple2D>();

        private readonly List<LineSegment2D> _shadowSurfacesList
            = new List<LineSegment2D>();

        private readonly List<LineSegment2D> _lineSegmentsList
            = new List<LineSegment2D>();


        public ITuple2D Point { get; set; }

        public ILineSegment2D ShadowSurface { get; set; }

        public ILineSegment2D LineSegment { get; set; }

        public bool HasIntersection { get; private set; }


        public InternalComputerValues[] ComputationResultsGa;
        public InternalComputerValues[] ComputationResultsVa;


        [Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        //[Params(5, 6, 7, 8)]
        public int OperationsCountLog2 { get; set; } = 10;

        public int OperationsCount => 1 << OperationsCountLog2;


        private InternalComputerValues GetComputerValues(bool stage2Flag)
        {
            return new InternalComputerValues()
            {
                D00 = _d00,
                D10 = _d10,
                D20 = _d20,
                D11 = _d11,
                D21 = _d21,
                D12 = _d12,
                D22 = _d22,
                Region1 = _region1,
                Region2 = _region2,
                T0 = _t0,
                T1 = _t1,
                T2 = _t2,
                Stage2Flag = stage2Flag,
                HasIntersection = HasIntersection
            };
        }


        [GlobalSetup]
        public void Setup()
        {
            var randGen = new System.Random(10);

            var boundingBox = BoundingBox2D.Create(-100, -100, 100, 100);

            for (var i = 0; i < OperationsCount; i++)
            {
                var p0 = randGen.GetPointInside(boundingBox);
                var sp1 = randGen.GetPointInside(boundingBox);
                var sp2 = randGen.GetPointInside(boundingBox);
                var p1 = randGen.GetPointInside(boundingBox);
                var p2 = randGen.GetPointInside(boundingBox);

                _pointsList.Add(p0);
                _shadowSurfacesList.Add(LineSegment2D.Create(sp1, sp2));
                _lineSegmentsList.Add(LineSegment2D.Create(p1, p2));
            }

            ComputationResultsGa = new InternalComputerValues[OperationsCount];
            ComputationResultsVa = new InternalComputerValues[OperationsCount];
        }

        private InternalComputerValues ComputeGa()
        {
            HasIntersection = false;

            _region1 = 0;
            _region2 = 0;

            //Begin GMac Macro Code Generation, 2018-08-07T00:06:37.1751449+02:00
            //Macro: cemsim.hga4d.GetSegmentShadowAreaIntersection2D
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 60 sub-expressions, 0 generated temps, 60 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 10 total.
            //Computations: 1.14285714285714 average, 80 total.
            //Memory Reads: 1.78571428571429 average, 125 total.
            //Memory Writes: 70 total.
            //
            //Macro Binding Data: 
            //   result.d00 = variable: _d00
            //   result.d10 = variable: _d10
            //   result.d20 = variable: _d20
            //   result.d11 = variable: _d11
            //   result.d21 = variable: _d21
            //   result.d12 = variable: _d12
            //   result.d22 = variable: _d22
            //   result.t0 = variable: _t0
            //   result.t1 = variable: _t1
            //   result.t2 = variable: _t2
            //   sp1.#e1# = variable: LineSegment.Point1X
            //   sp1.#e2# = variable: LineSegment.Point1Y
            //   sp2.#e1# = variable: LineSegment.Point2X
            //   sp2.#e2# = variable: LineSegment.Point2Y
            //   p0.#e1# = variable: Point.X
            //   p0.#e2# = variable: Point.Y
            //   p1.#e1# = variable: ShadowLineSegment.Point1X
            //   p1.#e2# = variable: ShadowLineSegment.Point1Y
            //   p2.#e1# = variable: ShadowLineSegment.Point2X
            //   p2.#e2# = variable: ShadowLineSegment.Point2Y

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;
            double tmp9;
            double tmp10;
            double tmp11;
            double tmp12;

            //Sub-expression: LLDI00B7 = Times[-1,LLDI0012,LLDI0013]
            tmp0 = -1 * ShadowSurface.Point1Y * ShadowSurface.Point2X;

            //Sub-expression: LLDI00B8 = Times[LLDI0011,LLDI0014]
            tmp1 = ShadowSurface.Point1X * ShadowSurface.Point2Y;

            //Sub-expression: LLDI00B9 = Plus[LLDI00B7,LLDI00B8]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI00BA = Times[-1,LLDI00B9]
            tmp0 = -tmp0;

            //Sub-expression: LLDI00BB = Times[-1,LLDI0013]
            tmp1 = -ShadowSurface.Point2X;

            //Sub-expression: LLDI00BC = Plus[LLDI0011,LLDI00BB]
            tmp1 = ShadowSurface.Point1X + tmp1;

            //Sub-expression: LLDI00BD = Times[LLDI0010,LLDI00BC]
            tmp2 = Point.Y * tmp1;

            //Sub-expression: LLDI00BE = Plus[LLDI00BA,LLDI00BD]
            tmp2 = tmp0 + tmp2;

            //Sub-expression: LLDI00BF = Times[-1,LLDI0014]
            tmp3 = -ShadowSurface.Point2Y;

            //Sub-expression: LLDI00C0 = Plus[LLDI0012,LLDI00BF]
            tmp3 = ShadowSurface.Point1Y + tmp3;

            //Sub-expression: LLDI00C1 = Times[-1,LLDI000F,LLDI00C0]
            tmp4 = -1 * Point.X * tmp3;

            //Output: LLDI0001 = Plus[LLDI00BE,LLDI00C1]
            _d00 = tmp2 + tmp4;

            //Sub-expression: LLDI00C2 = Times[LLDI000C,LLDI00BC]
            tmp2 = LineSegment.Point1Y * tmp1;

            //Sub-expression: LLDI00C3 = Plus[LLDI00BA,LLDI00C2]
            tmp2 = tmp0 + tmp2;

            //Sub-expression: LLDI00C4 = Times[-1,LLDI000B,LLDI00C0]
            tmp4 = -1 * LineSegment.Point1X * tmp3;

            //Output: LLDI0002 = Plus[LLDI00C3,LLDI00C4]
            _d10 = tmp2 + tmp4;

            //Sub-expression: LLDI00C5 = Times[LLDI000E,LLDI00BC]
            tmp1 = LineSegment.Point2Y * tmp1;

            //Sub-expression: LLDI00C6 = Plus[LLDI00BA,LLDI00C5]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI00C7 = Times[-1,LLDI000D,LLDI00C0]
            tmp1 = -1 * LineSegment.Point2X * tmp3;

            //Output: LLDI0003 = Plus[LLDI00C6,LLDI00C7]
            _d20 = tmp0 + tmp1;


            if (_d00 >= 0)
            {
                if (_d10 < 0) _region1 |= 1;
                if (_d20 < 0) _region2 |= 1;
            }
            else
            {
                if (_d10 >= 0) _region1 |= 1;
                if (_d20 >= 0) _region2 |= 1;
            }

            //Line segment is fully outside shadow area (region 1)
            if (_region1 == 0 && _region2 == 0)
                return GetComputerValues(false);


            //Sub-expression: LLDI00C8 = Times[-1,LLDI0010,LLDI0011]
            tmp3 = -1 * Point.Y * ShadowSurface.Point1X;

            //Sub-expression: LLDI00C9 = Times[LLDI000F,LLDI0012]
            tmp5 = Point.X * ShadowSurface.Point1Y;

            //Sub-expression: LLDI00CA = Plus[LLDI00C8,LLDI00C9]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI00CB = Times[-1,LLDI00CA]
            tmp3 = -tmp3;

            //Sub-expression: LLDI00CC = Times[-1,LLDI0011]
            tmp5 = -ShadowSurface.Point1X;

            //Sub-expression: LLDI00CD = Plus[LLDI000F,LLDI00CC]
            tmp5 = Point.X + tmp5;

            //Sub-expression: LLDI00CE = Times[LLDI000C,LLDI00CD]
            tmp6 = LineSegment.Point1Y * tmp5;

            //Sub-expression: LLDI00CF = Plus[LLDI00CB,LLDI00CE]
            tmp6 = tmp3 + tmp6;

            //Sub-expression: LLDI00D0 = Times[-1,LLDI0012]
            tmp7 = -ShadowSurface.Point1Y;

            //Sub-expression: LLDI00D1 = Plus[LLDI0010,LLDI00D0]
            tmp7 = Point.Y + tmp7;

            //Sub-expression: LLDI00D2 = Times[-1,LLDI000B,LLDI00D1]
            tmp8 = -1 * LineSegment.Point1X * tmp7;

            //Output: LLDI0004 = Plus[LLDI00CF,LLDI00D2]
            _d11 = tmp6 + tmp8;

            //Sub-expression: LLDI00D3 = Times[LLDI000E,LLDI00CD]
            tmp5 = LineSegment.Point2Y * tmp5;

            //Sub-expression: LLDI00D4 = Plus[LLDI00CB,LLDI00D3]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI00D5 = Times[-1,LLDI000D,LLDI00D1]
            tmp5 = -1 * LineSegment.Point2X * tmp7;

            //Output: LLDI0005 = Plus[LLDI00D4,LLDI00D5]
            _d21 = tmp3 + tmp5;


            if (_d00 >= 0)
            {
                if (_d11 < 0) _region1 |= 2;
                if (_d21 < 0) _region2 |= 2;
            }
            else
            {
                if (_d11 >= 0) _region1 |= 2;
                if (_d21 >= 0) _region2 |= 2;
            }

            //Line segment is fully outside shadow area (region 1)
            if ((_region1 & 2) != 0 && (_region2 & 2) != 0)
                return GetComputerValues(false);


            //Sub-expression: LLDI00D6 = Times[LLDI0010,LLDI0013]
            tmp7 = Point.Y * ShadowSurface.Point2X;

            //Sub-expression: LLDI00D7 = Times[-1,LLDI000F,LLDI0014]
            tmp9 = -1 * Point.X * ShadowSurface.Point2Y;

            //Sub-expression: LLDI00D8 = Plus[LLDI00D6,LLDI00D7]
            tmp7 = tmp7 + tmp9;

            //Sub-expression: LLDI00D9 = Times[-1,LLDI00D8]
            tmp7 = -tmp7;

            //Sub-expression: LLDI00DA = Times[-1,LLDI000F]
            tmp9 = -Point.X;

            //Sub-expression: LLDI00DB = Plus[LLDI00DA,LLDI0013]
            tmp9 = tmp9 + ShadowSurface.Point2X;

            //Sub-expression: LLDI00DC = Times[LLDI000C,LLDI00DB]
            tmp10 = LineSegment.Point1Y * tmp9;

            //Sub-expression: LLDI00DD = Plus[LLDI00D9,LLDI00DC]
            tmp10 = tmp7 + tmp10;

            //Sub-expression: LLDI00DE = Times[-1,LLDI0010]
            tmp11 = -Point.Y;

            //Sub-expression: LLDI00DF = Plus[LLDI00DE,LLDI0014]
            tmp11 = tmp11 + ShadowSurface.Point2Y;

            //Sub-expression: LLDI00E0 = Times[-1,LLDI000B,LLDI00DF]
            tmp12 = -1 * LineSegment.Point1X * tmp11;

            //Output: LLDI0006 = Plus[LLDI00DD,LLDI00E0]
            _d12 = tmp10 + tmp12;

            //Sub-expression: LLDI00E1 = Times[LLDI000E,LLDI00DB]
            tmp9 = LineSegment.Point2Y * tmp9;

            //Sub-expression: LLDI00E2 = Plus[LLDI00D9,LLDI00E1]
            tmp7 = tmp7 + tmp9;

            //Sub-expression: LLDI00E3 = Times[-1,LLDI000D,LLDI00DF]
            tmp9 = -1 * LineSegment.Point2X * tmp11;

            //Output: LLDI0007 = Plus[LLDI00E2,LLDI00E3]
            _d22 = tmp7 + tmp9;


            if (_d00 >= 0)
            {
                if (_d12 < 0) _region1 |= 4;
                if (_d22 < 0) _region2 |= 4;
            }
            else
            {
                if (_d12 >= 0) _region1 |= 4;
                if (_d22 >= 0) _region2 |= 4;
            }

            //Line segment is fully outside shadow area (region 1)
            if ((_region1 & 4) != 0 && (_region2 & 4) != 0)
                return GetComputerValues(false);


            //Line segment is fully inside shadow area (region 1)
            if (_region1 == 1 && _region2 == 1)
            {
                HasIntersection = true;

                return GetComputerValues(false);
            }

            //Sub-expression: LLDI00E4 = Plus[LLDI00C3,LLDI00C4]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI00E5 = Plus[LLDI00C6,LLDI00C7]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI00E6 = Times[-1,LLDI00E5]
            tmp0 = -tmp0;

            //Sub-expression: LLDI00E7 = Plus[LLDI00E4,LLDI00E6]
            tmp0 = tmp2 + tmp0;

            //Sub-expression: LLDI00E8 = Power[LLDI00E7,-1]
            tmp0 = 1 / tmp0;

            //Output: LLDI0008 = Times[LLDI00E4,LLDI00E8]
            _t0 = tmp2 * tmp0;

            //Sub-expression: LLDI00E9 = Plus[LLDI00CF,LLDI00D2]
            tmp0 = tmp6 + tmp8;

            //Sub-expression: LLDI00EA = Plus[LLDI00D4,LLDI00D5]
            tmp1 = tmp3 + tmp5;

            //Sub-expression: LLDI00EB = Times[-1,LLDI00EA]
            tmp1 = -tmp1;

            //Sub-expression: LLDI00EC = Plus[LLDI00E9,LLDI00EB]
            tmp1 = tmp0 + tmp1;

            //Sub-expression: LLDI00ED = Power[LLDI00EC,-1]
            tmp1 = 1 / tmp1;

            //Output: LLDI0009 = Times[LLDI00E9,LLDI00ED]
            _t1 = tmp0 * tmp1;

            //Sub-expression: LLDI00EE = Plus[LLDI00DD,LLDI00E0]
            tmp0 = tmp10 + tmp12;

            //Sub-expression: LLDI00EF = Plus[LLDI00E2,LLDI00E3]
            tmp1 = tmp7 + tmp9;

            //Sub-expression: LLDI00F0 = Times[-1,LLDI00EF]
            tmp1 = -tmp1;

            //Sub-expression: LLDI00F1 = Plus[LLDI00EE,LLDI00F0]
            tmp1 = tmp0 + tmp1;

            //Sub-expression: LLDI00F2 = Power[LLDI00F1,-1]
            tmp1 = 1 / tmp1;

            //Output: LLDI000A = Times[LLDI00EE,LLDI00F2]
            _t2 = tmp0 * tmp1;


            //Finish GMac Macro Code Generation, 2018-08-07T00:06:37.1981321+02:00

            return GetComputerValues(true);
        }

        [Benchmark(Baseline = true)]
        public InternalComputerValues[] ComputeUsingGa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                Point = _pointsList[i];
                ShadowSurface = _shadowSurfacesList[i];
                LineSegment = _lineSegmentsList[i];

                ComputationResultsGa[i] = ComputeGa();
            }

            return ComputationResultsGa;
        }


        private InternalComputerValues ComputeVa()
        {
            HasIntersection = false;

            _region1 = 0;
            _region2 = 0;

            var sp1 = LineSegment.GetPoint1();
            var sp2 = LineSegment.GetPoint2();

            var p0 = Point;

            var p1 = ShadowSurface.GetPoint1();
            var p2 = ShadowSurface.GetPoint2();

            _d00 = p0.GetSignedDistanceToLineVa(ShadowSurface);

            _d10 = sp1.GetSignedDistanceToLineVa(p1, p2);
            _d20 = sp2.GetSignedDistanceToLineVa(p1, p2);


            if (_d00 >= 0)
            {
                if (_d10 < 0) _region1 |= 1;
                if (_d20 < 0) _region2 |= 1;
            }
            else
            {
                if (_d10 >= 0) _region1 |= 1;
                if (_d20 >= 0) _region2 |= 1;
            }

            //Line segment is fully outside shadow area (region 1)
            if (_region1 == 0 && _region2 == 0)
                return GetComputerValues(false);


            _d11 = sp1.GetSignedDistanceToLineVa(p0, p1);
            _d21 = sp2.GetSignedDistanceToLineVa(p0, p1);


            if (_d00 >= 0)
            {
                if (_d11 < 0) _region1 |= 2;
                if (_d21 < 0) _region2 |= 2;
            }
            else
            {
                if (_d11 >= 0) _region1 |= 2;
                if (_d21 >= 0) _region2 |= 2;
            }

            //Line segment is fully outside shadow area (region 1)
            if ((_region1 & 2) != 0 && (_region2 & 2) != 0)
                return GetComputerValues(false);


            _d12 = sp1.GetSignedDistanceToLineVa(p2, p0);
            _d22 = sp2.GetSignedDistanceToLineVa(p2, p0);


            if (_d00 >= 0)
            {
                if (_d12 < 0) _region1 |= 4;
                if (_d22 < 0) _region2 |= 4;
            }
            else
            {
                if (_d12 >= 0) _region1 |= 4;
                if (_d22 >= 0) _region2 |= 4;
            }

            //Line segment is fully outside shadow area (region 1)
            if ((_region1 & 4) != 0 && (_region2 & 4) != 0)
                return GetComputerValues(false);


            //Line segment is fully inside shadow area (region 1)
            if (_region1 == 1 && _region2 == 1)
            {
                HasIntersection = true;

                return GetComputerValues(false);
            }

            _t0 = _d10 / (_d10 - _d20);
            _t1 = _d11 / (_d11 - _d21);
            _t2 = _d12 / (_d12 - _d22);

            return GetComputerValues(true);
        }

        [Benchmark]
        public InternalComputerValues[] ComputeUsingVa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                Point = _pointsList[i];
                ShadowSurface = _shadowSurfacesList[i];
                LineSegment = _lineSegmentsList[i];

                ComputationResultsVa[i] = ComputeVa();
            }

            return ComputationResultsGa;
        }


        public double Validate(int operationsCountLog2 = 10)
        {
            OperationsCountLog2 = operationsCountLog2;

            Setup();

            ComputeUsingGa();
            ComputeUsingVa();

            var maxDiff = 0.0d;
            for (var i = 0; i < OperationsCount; i++)
            {
                var resultGa = ComputationResultsGa[i];
                var resultVa = ComputationResultsVa[i];

                if (resultVa.HasIntersection != resultGa.HasIntersection)
                    throw new InvalidOperationException();

                if (resultVa.Stage2Flag != resultGa.Stage2Flag)
                    throw new InvalidOperationException();

                if (resultVa.Region1 != resultGa.Region1)
                    throw new InvalidOperationException();

                if (resultVa.Region2 != resultGa.Region2)
                    throw new InvalidOperationException();

                var diff = resultGa.GetMaxAbsDifference(resultVa);

                if (maxDiff < diff) maxDiff = diff;
            }

            return maxDiff;
        }
    }
}