using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicOperations;
using NumericalGeometryLib.BasicShapes.Triangles;
using NumericalGeometryLib.BasicShapes.Triangles.Immutable;
using NumericalGeometryLib.Borders.Space3D.Immutable;
using NumericalGeometryLib.Random;

namespace NumericalGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra.GeneratedCode
{
    public class GaBenchmark8
    {
        public sealed class InternalComputerValues
        {
            public double D00 { get; internal set; }

            public double D10 { get; internal set; }
            public double D11 { get; internal set; }
            public double D12 { get; internal set; }
            public double D13 { get; internal set; }

            public double D20 { get; internal set; }
            public double D21 { get; internal set; }
            public double D22 { get; internal set; }
            public double D23 { get; internal set; }

            public double D30 { get; internal set; }
            public double D31 { get; internal set; }
            public double D32 { get; internal set; }
            public double D33 { get; internal set; }

            public int Region1 { get; internal set; }
            public int Region2 { get; internal set; }
            public int Region3 { get; internal set; }

            public double T120 { get; internal set; }
            public double T230 { get; internal set; }
            public double T310 { get; internal set; }

            public double T121 { get; internal set; }
            public double T231 { get; internal set; }
            public double T311 { get; internal set; }

            public double T122 { get; internal set; }
            public double T232 { get; internal set; }
            public double T312 { get; internal set; }

            public double T123 { get; internal set; }
            public double T233 { get; internal set; }
            public double T313 { get; internal set; }

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

                maxDiff = GetMaxAbs(maxDiff, T120 - values.T120);
                maxDiff = GetMaxAbs(maxDiff, T230 - values.T230);
                maxDiff = GetMaxAbs(maxDiff, T310 - values.T310);

                maxDiff = GetMaxAbs(maxDiff, T121 - values.T121);
                maxDiff = GetMaxAbs(maxDiff, T231 - values.T231);
                maxDiff = GetMaxAbs(maxDiff, T311 - values.T311);

                maxDiff = GetMaxAbs(maxDiff, T122 - values.T122);
                maxDiff = GetMaxAbs(maxDiff, T232 - values.T232);
                maxDiff = GetMaxAbs(maxDiff, T312 - values.T312);

                maxDiff = GetMaxAbs(maxDiff, T123 - values.T123);
                maxDiff = GetMaxAbs(maxDiff, T233 - values.T233);
                maxDiff = GetMaxAbs(maxDiff, T313 - values.T313);

                return maxDiff;
            }
        }


        private double _d00;
        private double _d10, _d11, _d12, _d13;
        private double _d20, _d21, _d22, _d23;
        private double _d30, _d31, _d32, _d33;
        private int _region1, _region2, _region3;
        private double _t120, _t230, _t310;
        private double _t121, _t231, _t311;
        private double _t122, _t232, _t312;
        private double _t123, _t233, _t313;


        private readonly List<Float64Tuple3D> _pointsList
            = new List<Float64Tuple3D>();

        private readonly List<Triangle3D> _shadowSurfacesList
            = new List<Triangle3D>();

        private readonly List<Triangle3D> _trianglesList
            = new List<Triangle3D>();


        public IFloat64Tuple3D Point { get; set; }

        public ITriangle3D ShadowSurface { get; set; }

        public ITriangle3D Triangle { get; set; }

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
                D11 = _d11,
                D12 = _d12,
                D13 = _d13,
                D20 = _d20,
                D21 = _d21,
                D22 = _d22,
                D23 = _d23,
                D30 = _d30,
                D31 = _d31,
                D32 = _d32,
                D33 = _d33,
                Region1 = _region1,
                Region2 = _region2,
                Region3 = _region3,
                T120 = _t120,
                T230 = _t230,
                T310 = _t310,
                T121 = _t121,
                T231 = _t231,
                T311 = _t311,
                T122 = _t122,
                T232 = _t232,
                T312 = _t312,
                T123 = _t123,
                T233 = _t233,
                T313 = _t313,
                Stage2Flag = stage2Flag,
                HasIntersection = HasIntersection
            };
        }


        [GlobalSetup]
        public void Setup()
        {
            var randGen = new System.Random(10);

            var boundingBox = BoundingBox3D.CreateFromPoints(-100, -100, -100, 100, 100, 100);

            for (var i = 0; i < OperationsCount; i++)
            {
                var p0 = randGen.GetPointInside(boundingBox);

                var sp1 = randGen.GetPointInside(boundingBox);
                var sp2 = randGen.GetPointInside(boundingBox);
                var sp3 = randGen.GetPointInside(boundingBox);

                var p1 = randGen.GetPointInside(boundingBox);
                var p2 = randGen.GetPointInside(boundingBox);
                var p3 = randGen.GetPointInside(boundingBox);

                _pointsList.Add(p0);
                _shadowSurfacesList.Add(Triangle3D.Create(sp1, sp2, sp3));
                _trianglesList.Add(Triangle3D.Create(p1, p2, p3));
            }

            ComputationResultsGa = new InternalComputerValues[OperationsCount];
            ComputationResultsVa = new InternalComputerValues[OperationsCount];
        }

        private void ResetInternalVariables()
        {
            _d00 = 0;

            _d10 = 0;
            _d11 = 0;
            _d12 = 0;
            _d13 = 0;

            _d20 = 0;
            _d21 = 0;
            _d22 = 0;
            _d23 = 0;

            _d30 = 0;
            _d31 = 0;
            _d32 = 0;
            _d33 = 0;

            _region1 = 0;
            _region2 = 0;
            _region3 = 0;

            _t120 = 0;
            _t230 = 0;
            _t310 = 0;

            _t121 = 0;
            _t231 = 0;
            _t311 = 0;

            _t122 = 0;
            _t232 = 0;
            _t312 = 0;

            _t123 = 0;
            _t233 = 0;
            _t313 = 0;

            HasIntersection = false;
        }

        private void ComputeTValues()
        {
            //Parameter values of triangle line sides' (1-2, 2-3, and 3-1)
            //intersections with plane p1-p2-p3
            _t120 = _d10 / (_d10 - _d20);
            _t230 = _d20 / (_d20 - _d30);
            _t310 = _d30 / (_d30 - _d10);

            //Parameter values of triangle line sides' (1-2, 2-3, and 3-1)
            //intersections with plane p0-p2-p3
            _t121 = _d11 / (_d11 - _d21);
            _t231 = _d21 / (_d21 - _d31);
            _t311 = _d31 / (_d31 - _d11);

            //Parameter values of triangle line sides' (1-2, 2-3, and 3-1)
            //intersections with plane p1-p0-p3
            _t122 = _d12 / (_d12 - _d22);
            _t232 = _d22 / (_d22 - _d32);
            _t312 = _d32 / (_d32 - _d12);

            //Parameter values of triangle line sides' (1-2, 2-3, and 3-1)
            //intersections with plane p1-p2-p0
            _t123 = _d13 / (_d13 - _d23);
            _t233 = _d23 / (_d23 - _d33);
            _t313 = _d33 / (_d33 - _d13);
        }


        private InternalComputerValues ComputeGa()
        {
            ResetInternalVariables();

            //Begin GMac Macro Code Generation, 2018-10-11T15:17:36.9703119+02:00
            //Macro: cemsim.hga4d.GetTriangleShadowVolumeIntersection3D
            //Input Variables: 21 used, 0 not used, 21 total.
            //Temp Variables: 199 sub-expressions, 0 generated temps, 199 total.
            //Target Temp Variables: 8 total.
            //Output Variables: 13 total.
            //Computations: 1.25471698113208 average, 266 total.
            //Memory Reads: 1.91509433962264 average, 406 total.
            //Memory Writes: 212 total.
            //
            //Macro Binding Data: 
            //   result.d00 = variable: _d00
            //   result.d10 = variable: _d10
            //   result.d20 = variable: _d20
            //   result.d30 = variable: _d30
            //   result.d11 = variable: _d11
            //   result.d21 = variable: _d21
            //   result.d31 = variable: _d31
            //   result.d12 = variable: _d12
            //   result.d22 = variable: _d22
            //   result.d32 = variable: _d32
            //   result.d13 = variable: _d13
            //   result.d23 = variable: _d23
            //   result.d33 = variable: _d33
            //   sp1.#e1# = variable: Triangle.Point1X
            //   sp1.#e2# = variable: Triangle.Point1Y
            //   sp1.#e3# = variable: Triangle.Point1Z
            //   sp2.#e1# = variable: Triangle.Point2X
            //   sp2.#e2# = variable: Triangle.Point2Y
            //   sp2.#e3# = variable: Triangle.Point2Z
            //   sp3.#e1# = variable: Triangle.Point3X
            //   sp3.#e2# = variable: Triangle.Point3Y
            //   sp3.#e3# = variable: Triangle.Point3Z
            //   p0.#e1# = variable: Point.ItemX
            //   p0.#e2# = variable: Point.ItemY
            //   p0.#e3# = variable: Point.ItemZ
            //   p1.#e1# = variable: ShadowTriangle.Point1X
            //   p1.#e2# = variable: ShadowTriangle.Point1Y
            //   p1.#e3# = variable: ShadowTriangle.Point1Z
            //   p2.#e1# = variable: ShadowTriangle.Point2X
            //   p2.#e2# = variable: ShadowTriangle.Point2Y
            //   p2.#e3# = variable: ShadowTriangle.Point2Z
            //   p3.#e1# = variable: ShadowTriangle.Point3X
            //   p3.#e2# = variable: ShadowTriangle.Point3Y
            //   p3.#e3# = variable: ShadowTriangle.Point3Z

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;

            //Sub-expression: LLDI0194 = Times[-1,LLDI001B,LLDI001D]
            tmp0 = -1 * ShadowSurface.Point1Y * ShadowSurface.Point2X;

            //Sub-expression: LLDI0195 = Times[LLDI001A,LLDI001E]
            tmp1 = ShadowSurface.Point1X * ShadowSurface.Point2Y;

            //Sub-expression: LLDI0196 = Plus[LLDI0194,LLDI0195]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0197 = Times[LLDI0022,LLDI0196]
            tmp1 = ShadowSurface.Point3Z * tmp0;

            //Sub-expression: LLDI0198 = Times[-1,LLDI001C,LLDI001D]
            tmp2 = -1 * ShadowSurface.Point1Z * ShadowSurface.Point2X;

            //Sub-expression: LLDI0199 = Times[LLDI001A,LLDI001F]
            tmp3 = ShadowSurface.Point1X * ShadowSurface.Point2Z;

            //Sub-expression: LLDI019A = Plus[LLDI0198,LLDI0199]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI019B = Times[-1,LLDI0021,LLDI019A]
            tmp3 = -1 * ShadowSurface.Point3Y * tmp2;

            //Sub-expression: LLDI019C = Plus[LLDI0197,LLDI019B]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI019D = Times[-1,LLDI001C,LLDI001E]
            tmp3 = -1 * ShadowSurface.Point1Z * ShadowSurface.Point2Y;

            //Sub-expression: LLDI019E = Times[LLDI001B,LLDI001F]
            tmp4 = ShadowSurface.Point1Y * ShadowSurface.Point2Z;

            //Sub-expression: LLDI019F = Plus[LLDI019D,LLDI019E]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI01A0 = Times[LLDI0020,LLDI019F]
            tmp4 = ShadowSurface.Point3X * tmp3;

            //Sub-expression: LLDI01A1 = Plus[LLDI019C,LLDI01A0]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI01A2 = Times[-1,LLDI001D]
            tmp4 = -ShadowSurface.Point2X;

            //Sub-expression: LLDI01A3 = Plus[LLDI001A,LLDI01A2]
            tmp4 = ShadowSurface.Point1X + tmp4;

            //Sub-expression: LLDI01A4 = Times[-1,LLDI0021,LLDI01A3]
            tmp5 = -1 * ShadowSurface.Point3Y * tmp4;

            //Sub-expression: LLDI01A5 = Plus[LLDI0196,LLDI01A4]
            tmp0 = tmp0 + tmp5;

            //Sub-expression: LLDI01A6 = Times[-1,LLDI001E]
            tmp5 = -ShadowSurface.Point2Y;

            //Sub-expression: LLDI01A7 = Plus[LLDI001B,LLDI01A6]
            tmp5 = ShadowSurface.Point1Y + tmp5;

            //Sub-expression: LLDI01A8 = Times[LLDI0020,LLDI01A7]
            tmp6 = ShadowSurface.Point3X * tmp5;

            //Sub-expression: LLDI01A9 = Plus[LLDI01A5,LLDI01A8]
            tmp0 = tmp0 + tmp6;

            //Sub-expression: LLDI01AA = Times[-1,LLDI0019,LLDI01A9]
            tmp6 = -1 * Point.Z * tmp0;

            //Sub-expression: LLDI01AB = Plus[LLDI01A1,LLDI01AA]
            tmp6 = tmp1 + tmp6;

            //Sub-expression: LLDI01AC = Times[-1,LLDI0022,LLDI01A3]
            tmp4 = -1 * ShadowSurface.Point3Z * tmp4;

            //Sub-expression: LLDI01AD = Plus[LLDI019A,LLDI01AC]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI01AE = Times[-1,LLDI001F]
            tmp4 = -ShadowSurface.Point2Z;

            //Sub-expression: LLDI01AF = Plus[LLDI001C,LLDI01AE]
            tmp4 = ShadowSurface.Point1Z + tmp4;

            //Sub-expression: LLDI01B0 = Times[LLDI0020,LLDI01AF]
            tmp7 = ShadowSurface.Point3X * tmp4;

            //Sub-expression: LLDI01B1 = Plus[LLDI01AD,LLDI01B0]
            tmp2 = tmp2 + tmp7;

            //Sub-expression: LLDI01B2 = Times[LLDI0018,LLDI01B1]
            tmp7 = Point.Y * tmp2;

            //Sub-expression: LLDI01B3 = Plus[LLDI01AB,LLDI01B2]
            tmp6 = tmp6 + tmp7;

            //Sub-expression: LLDI01B4 = Times[-1,LLDI0022,LLDI01A7]
            tmp5 = -1 * ShadowSurface.Point3Z * tmp5;

            //Sub-expression: LLDI01B5 = Plus[LLDI019F,LLDI01B4]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI01B6 = Times[LLDI0021,LLDI01AF]
            tmp4 = ShadowSurface.Point3Y * tmp4;

            //Sub-expression: LLDI01B7 = Plus[LLDI01B5,LLDI01B6]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI01B8 = Times[-1,LLDI0017,LLDI01B7]
            tmp4 = -1 * Point.X * tmp3;

            //Output: LLDI0001 = Plus[LLDI01B3,LLDI01B8]
            _d00 = tmp6 + tmp4;

            //Sub-expression: LLDI01B9 = Times[-1,LLDI0010,LLDI01A9]
            tmp4 = -1 * Triangle.Point1Z * tmp0;

            //Sub-expression: LLDI01BA = Plus[LLDI01A1,LLDI01B9]
            tmp4 = tmp1 + tmp4;

            //Sub-expression: LLDI01BB = Times[LLDI000F,LLDI01B1]
            tmp5 = Triangle.Point1Y * tmp2;

            //Sub-expression: LLDI01BC = Plus[LLDI01BA,LLDI01BB]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI01BD = Times[-1,LLDI000E,LLDI01B7]
            tmp5 = -1 * Triangle.Point1X * tmp3;

            //Output: LLDI0002 = Plus[LLDI01BC,LLDI01BD]
            _d10 = tmp4 + tmp5;

            //Sub-expression: LLDI01BE = Times[-1,LLDI0013,LLDI01A9]
            tmp4 = -1 * Triangle.Point2Z * tmp0;

            //Sub-expression: LLDI01BF = Plus[LLDI01A1,LLDI01BE]
            tmp4 = tmp1 + tmp4;

            //Sub-expression: LLDI01C0 = Times[LLDI0012,LLDI01B1]
            tmp5 = Triangle.Point2Y * tmp2;

            //Sub-expression: LLDI01C1 = Plus[LLDI01BF,LLDI01C0]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI01C2 = Times[-1,LLDI0011,LLDI01B7]
            tmp5 = -1 * Triangle.Point2X * tmp3;

            //Output: LLDI0003 = Plus[LLDI01C1,LLDI01C2]
            _d20 = tmp4 + tmp5;

            //Sub-expression: LLDI01C3 = Times[-1,LLDI0016,LLDI01A9]
            tmp0 = -1 * Triangle.Point3Z * tmp0;

            //Sub-expression: LLDI01C4 = Plus[LLDI01A1,LLDI01C3]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI01C5 = Times[LLDI0015,LLDI01B1]
            tmp1 = Triangle.Point3Y * tmp2;

            //Sub-expression: LLDI01C6 = Plus[LLDI01C4,LLDI01C5]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI01C7 = Times[-1,LLDI0014,LLDI01B7]
            tmp1 = -1 * Triangle.Point3X * tmp3;

            //Output: LLDI0004 = Plus[LLDI01C6,LLDI01C7]
            _d30 = tmp0 + tmp1;


            if (_d00 >= 0)
            {
                if (_d10 < 0) _region1 |= 1;
                if (_d20 < 0) _region2 |= 1;
                if (_d30 < 0) _region3 |= 1;
            }
            else
            {
                if (_d10 >= 0) _region1 |= 1;
                if (_d20 >= 0) _region2 |= 1;
                if (_d30 >= 0) _region3 |= 1;
            }

            //Triangle is fully outside shadow area (region 1)
            if (_region1 == 0 && _region2 == 0 && _region3 == 0)
                return GetComputerValues(false);


            //Sub-expression: LLDI01FA = Times[LLDI0018,LLDI0020]
            tmp0 = Point.Y * ShadowSurface.Point3X;

            //Sub-expression: LLDI01FB = Times[-1,LLDI0017,LLDI0021]
            tmp1 = -1 * Point.X * ShadowSurface.Point3Y;

            //Sub-expression: LLDI01FC = Plus[LLDI01FA,LLDI01FB]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI01FD = Times[LLDI001C,LLDI01FC]
            tmp1 = ShadowSurface.Point1Z * tmp0;

            //Sub-expression: LLDI01FE = Times[LLDI0019,LLDI0020]
            tmp2 = Point.Z * ShadowSurface.Point3X;

            //Sub-expression: LLDI01FF = Times[-1,LLDI0017,LLDI0022]
            tmp3 = -1 * Point.X * ShadowSurface.Point3Z;

            //Sub-expression: LLDI0200 = Plus[LLDI01FE,LLDI01FF]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0201 = Times[-1,LLDI001B,LLDI0200]
            tmp3 = -1 * ShadowSurface.Point1Y * tmp2;

            //Sub-expression: LLDI0202 = Plus[LLDI01FD,LLDI0201]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0203 = Times[LLDI0019,LLDI0021]
            tmp3 = Point.Z * ShadowSurface.Point3Y;

            //Sub-expression: LLDI0204 = Times[-1,LLDI0018,LLDI0022]
            tmp4 = -1 * Point.Y * ShadowSurface.Point3Z;

            //Sub-expression: LLDI0205 = Plus[LLDI0203,LLDI0204]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0206 = Times[LLDI001A,LLDI0205]
            tmp4 = ShadowSurface.Point1X * tmp3;

            //Sub-expression: LLDI0207 = Plus[LLDI0202,LLDI0206]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0208 = Times[-1,LLDI0017]
            tmp4 = -Point.X;

            //Sub-expression: LLDI0209 = Plus[LLDI0208,LLDI0020]
            tmp4 = tmp4 + ShadowSurface.Point3X;

            //Sub-expression: LLDI020A = Times[-1,LLDI001B,LLDI0209]
            tmp5 = -1 * ShadowSurface.Point1Y * tmp4;

            //Sub-expression: LLDI020B = Plus[LLDI01FC,LLDI020A]
            tmp0 = tmp0 + tmp5;

            //Sub-expression: LLDI020C = Times[-1,LLDI0018]
            tmp5 = -Point.Y;

            //Sub-expression: LLDI020D = Plus[LLDI020C,LLDI0021]
            tmp5 = tmp5 + ShadowSurface.Point3Y;

            //Sub-expression: LLDI020E = Times[LLDI001A,LLDI020D]
            tmp6 = ShadowSurface.Point1X * tmp5;

            //Sub-expression: LLDI020F = Plus[LLDI020B,LLDI020E]
            tmp0 = tmp0 + tmp6;

            //Sub-expression: LLDI0210 = Times[-1,LLDI0010,LLDI020F]
            tmp6 = -1 * Triangle.Point1Z * tmp0;

            //Sub-expression: LLDI0211 = Plus[LLDI0207,LLDI0210]
            tmp6 = tmp1 + tmp6;

            //Sub-expression: LLDI0212 = Times[-1,LLDI001C,LLDI0209]
            tmp4 = -1 * ShadowSurface.Point1Z * tmp4;

            //Sub-expression: LLDI0213 = Plus[LLDI0200,LLDI0212]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI0214 = Times[-1,LLDI0019]
            tmp4 = -Point.Z;

            //Sub-expression: LLDI0215 = Plus[LLDI0214,LLDI0022]
            tmp4 = tmp4 + ShadowSurface.Point3Z;

            //Sub-expression: LLDI0216 = Times[LLDI001A,LLDI0215]
            tmp7 = ShadowSurface.Point1X * tmp4;

            //Sub-expression: LLDI0217 = Plus[LLDI0213,LLDI0216]
            tmp2 = tmp2 + tmp7;

            //Sub-expression: LLDI0218 = Times[LLDI000F,LLDI0217]
            tmp7 = Triangle.Point1Y * tmp2;

            //Sub-expression: LLDI0219 = Plus[LLDI0211,LLDI0218]
            tmp6 = tmp6 + tmp7;

            //Sub-expression: LLDI021A = Times[-1,LLDI001C,LLDI020D]
            tmp5 = -1 * ShadowSurface.Point1Z * tmp5;

            //Sub-expression: LLDI021B = Plus[LLDI0205,LLDI021A]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI021C = Times[LLDI001B,LLDI0215]
            tmp4 = ShadowSurface.Point1Y * tmp4;

            //Sub-expression: LLDI021D = Plus[LLDI021B,LLDI021C]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI021E = Times[-1,LLDI000E,LLDI021D]
            tmp4 = -1 * Triangle.Point1X * tmp3;

            //Output: LLDI0008 = Plus[LLDI0219,LLDI021E]
            _d12 = tmp6 + tmp4;

            //Sub-expression: LLDI021F = Times[-1,LLDI0013,LLDI020F]
            tmp4 = -1 * Triangle.Point2Z * tmp0;

            //Sub-expression: LLDI0220 = Plus[LLDI0207,LLDI021F]
            tmp4 = tmp1 + tmp4;

            //Sub-expression: LLDI0221 = Times[LLDI0012,LLDI0217]
            tmp5 = Triangle.Point2Y * tmp2;

            //Sub-expression: LLDI0222 = Plus[LLDI0220,LLDI0221]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI0223 = Times[-1,LLDI0011,LLDI021D]
            tmp5 = -1 * Triangle.Point2X * tmp3;

            //Output: LLDI0009 = Plus[LLDI0222,LLDI0223]
            _d22 = tmp4 + tmp5;

            //Sub-expression: LLDI0224 = Times[-1,LLDI0016,LLDI020F]
            tmp0 = -1 * Triangle.Point3Z * tmp0;

            //Sub-expression: LLDI0225 = Plus[LLDI0207,LLDI0224]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI0226 = Times[LLDI0015,LLDI0217]
            tmp1 = Triangle.Point3Y * tmp2;

            //Sub-expression: LLDI0227 = Plus[LLDI0225,LLDI0226]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0228 = Times[-1,LLDI0014,LLDI021D]
            tmp1 = -1 * Triangle.Point3X * tmp3;

            //Output: LLDI000A = Plus[LLDI0227,LLDI0228]
            _d32 = tmp0 + tmp1;


            if (_d00 >= 0)
            {
                if (_d12 < 0) _region1 |= 4;
                if (_d22 < 0) _region2 |= 4;
                if (_d32 < 0) _region3 |= 4;
            }
            else
            {
                if (_d12 >= 0) _region1 |= 4;
                if (_d22 >= 0) _region2 |= 4;
                if (_d32 >= 0) _region3 |= 4;
            }

            //Triangle is fully outside shadow area (region 1)
            if ((_region1 & 4) != 0 && (_region2 & 4) != 0 && (_region3 & 4) != 0)
                return GetComputerValues(false);


            //Sub-expression: LLDI01C8 = Times[-1,LLDI001E,LLDI0020]
            tmp0 = -1 * ShadowSurface.Point2Y * ShadowSurface.Point3X;

            //Sub-expression: LLDI01C9 = Times[LLDI001D,LLDI0021]
            tmp1 = ShadowSurface.Point2X * ShadowSurface.Point3Y;

            //Sub-expression: LLDI01CA = Plus[LLDI01C8,LLDI01C9]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI01CB = Times[LLDI0019,LLDI01CA]
            tmp1 = Point.Z * tmp0;

            //Sub-expression: LLDI01CC = Times[-1,LLDI001F,LLDI0020]
            tmp2 = -1 * ShadowSurface.Point2Z * ShadowSurface.Point3X;

            //Sub-expression: LLDI01CD = Times[LLDI001D,LLDI0022]
            tmp3 = ShadowSurface.Point2X * ShadowSurface.Point3Z;

            //Sub-expression: LLDI01CE = Plus[LLDI01CC,LLDI01CD]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI01CF = Times[-1,LLDI0018,LLDI01CE]
            tmp3 = -1 * Point.Y * tmp2;

            //Sub-expression: LLDI01D0 = Plus[LLDI01CB,LLDI01CF]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI01D1 = Times[-1,LLDI001F,LLDI0021]
            tmp3 = -1 * ShadowSurface.Point2Z * ShadowSurface.Point3Y;

            //Sub-expression: LLDI01D2 = Times[LLDI001E,LLDI0022]
            tmp4 = ShadowSurface.Point2Y * ShadowSurface.Point3Z;

            //Sub-expression: LLDI01D3 = Plus[LLDI01D1,LLDI01D2]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI01D4 = Times[LLDI0017,LLDI01D3]
            tmp4 = Point.X * tmp3;

            //Sub-expression: LLDI01D5 = Plus[LLDI01D0,LLDI01D4]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI01D6 = Times[-1,LLDI0020]
            tmp4 = -ShadowSurface.Point3X;

            //Sub-expression: LLDI01D7 = Plus[LLDI001D,LLDI01D6]
            tmp4 = ShadowSurface.Point2X + tmp4;

            //Sub-expression: LLDI01D8 = Times[-1,LLDI0018,LLDI01D7]
            tmp5 = -1 * Point.Y * tmp4;

            //Sub-expression: LLDI01D9 = Plus[LLDI01CA,LLDI01D8]
            tmp0 = tmp0 + tmp5;

            //Sub-expression: LLDI01DA = Times[-1,LLDI0021]
            tmp5 = -ShadowSurface.Point3Y;

            //Sub-expression: LLDI01DB = Plus[LLDI001E,LLDI01DA]
            tmp5 = ShadowSurface.Point2Y + tmp5;

            //Sub-expression: LLDI01DC = Times[LLDI0017,LLDI01DB]
            tmp6 = Point.X * tmp5;

            //Sub-expression: LLDI01DD = Plus[LLDI01D9,LLDI01DC]
            tmp0 = tmp0 + tmp6;

            //Sub-expression: LLDI01DE = Times[-1,LLDI0010,LLDI01DD]
            tmp6 = -1 * Triangle.Point1Z * tmp0;

            //Sub-expression: LLDI01DF = Plus[LLDI01D5,LLDI01DE]
            tmp6 = tmp1 + tmp6;

            //Sub-expression: LLDI01E0 = Times[-1,LLDI0019,LLDI01D7]
            tmp4 = -1 * Point.Z * tmp4;

            //Sub-expression: LLDI01E1 = Plus[LLDI01CE,LLDI01E0]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI01E2 = Times[-1,LLDI0022]
            tmp4 = -ShadowSurface.Point3Z;

            //Sub-expression: LLDI01E3 = Plus[LLDI001F,LLDI01E2]
            tmp4 = ShadowSurface.Point2Z + tmp4;

            //Sub-expression: LLDI01E4 = Times[LLDI0017,LLDI01E3]
            tmp7 = Point.X * tmp4;

            //Sub-expression: LLDI01E5 = Plus[LLDI01E1,LLDI01E4]
            tmp2 = tmp2 + tmp7;

            //Sub-expression: LLDI01E6 = Times[LLDI000F,LLDI01E5]
            tmp7 = Triangle.Point1Y * tmp2;

            //Sub-expression: LLDI01E7 = Plus[LLDI01DF,LLDI01E6]
            tmp6 = tmp6 + tmp7;

            //Sub-expression: LLDI01E8 = Times[-1,LLDI0019,LLDI01DB]
            tmp5 = -1 * Point.Z * tmp5;

            //Sub-expression: LLDI01E9 = Plus[LLDI01D3,LLDI01E8]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI01EA = Times[LLDI0018,LLDI01E3]
            tmp4 = Point.Y * tmp4;

            //Sub-expression: LLDI01EB = Plus[LLDI01E9,LLDI01EA]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI01EC = Times[-1,LLDI000E,LLDI01EB]
            tmp4 = -1 * Triangle.Point1X * tmp3;

            //Sub-expression: LLDI01ED = Plus[LLDI01E7,LLDI01EC]
            tmp4 = tmp6 + tmp4;

            //Output: LLDI0005 = Times[-1,LLDI01ED]
            _d11 = -tmp4;

            //Sub-expression: LLDI01EE = Times[-1,LLDI0013,LLDI01DD]
            tmp4 = -1 * Triangle.Point2Z * tmp0;

            //Sub-expression: LLDI01EF = Plus[LLDI01D5,LLDI01EE]
            tmp4 = tmp1 + tmp4;

            //Sub-expression: LLDI01F0 = Times[LLDI0012,LLDI01E5]
            tmp5 = Triangle.Point2Y * tmp2;

            //Sub-expression: LLDI01F1 = Plus[LLDI01EF,LLDI01F0]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI01F2 = Times[-1,LLDI0011,LLDI01EB]
            tmp5 = -1 * Triangle.Point2X * tmp3;

            //Sub-expression: LLDI01F3 = Plus[LLDI01F1,LLDI01F2]
            tmp4 = tmp4 + tmp5;

            //Output: LLDI0006 = Times[-1,LLDI01F3]
            _d21 = -tmp4;

            //Sub-expression: LLDI01F4 = Times[-1,LLDI0016,LLDI01DD]
            tmp0 = -1 * Triangle.Point3Z * tmp0;

            //Sub-expression: LLDI01F5 = Plus[LLDI01D5,LLDI01F4]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI01F6 = Times[LLDI0015,LLDI01E5]
            tmp1 = Triangle.Point3Y * tmp2;

            //Sub-expression: LLDI01F7 = Plus[LLDI01F5,LLDI01F6]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI01F8 = Times[-1,LLDI0014,LLDI01EB]
            tmp1 = -1 * Triangle.Point3X * tmp3;

            //Sub-expression: LLDI01F9 = Plus[LLDI01F7,LLDI01F8]
            tmp0 = tmp0 + tmp1;

            //Output: LLDI0007 = Times[-1,LLDI01F9]
            _d31 = -tmp0;


            if (_d00 >= 0)
            {
                if (_d11 < 0) _region1 |= 2;
                if (_d21 < 0) _region2 |= 2;
                if (_d31 < 0) _region3 |= 2;
            }
            else
            {
                if (_d11 >= 0) _region1 |= 2;
                if (_d21 >= 0) _region2 |= 2;
                if (_d31 >= 0) _region3 |= 2;
            }

            //Line segment is fully outside shadow area (region 1)
            if ((_region1 & 2) != 0 && (_region2 & 2) != 0 && (_region3 & 2) != 0)
                return GetComputerValues(false);


            //Sub-expression: LLDI0229 = Times[-1,LLDI0018,LLDI001A]
            tmp0 = -1 * Point.Y * ShadowSurface.Point1X;

            //Sub-expression: LLDI022A = Times[LLDI0017,LLDI001B]
            tmp1 = Point.X * ShadowSurface.Point1Y;

            //Sub-expression: LLDI022B = Plus[LLDI0229,LLDI022A]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI022C = Times[LLDI001F,LLDI022B]
            tmp1 = ShadowSurface.Point2Z * tmp0;

            //Sub-expression: LLDI022D = Times[-1,LLDI0019,LLDI001A]
            tmp2 = -1 * Point.Z * ShadowSurface.Point1X;

            //Sub-expression: LLDI022E = Times[LLDI0017,LLDI001C]
            tmp3 = Point.X * ShadowSurface.Point1Z;

            //Sub-expression: LLDI022F = Plus[LLDI022D,LLDI022E]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0230 = Times[-1,LLDI001E,LLDI022F]
            tmp3 = -1 * ShadowSurface.Point2Y * tmp2;

            //Sub-expression: LLDI0231 = Plus[LLDI022C,LLDI0230]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0232 = Times[-1,LLDI0019,LLDI001B]
            tmp3 = -1 * Point.Z * ShadowSurface.Point1Y;

            //Sub-expression: LLDI0233 = Times[LLDI0018,LLDI001C]
            tmp4 = Point.Y * ShadowSurface.Point1Z;

            //Sub-expression: LLDI0234 = Plus[LLDI0232,LLDI0233]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0235 = Times[LLDI001D,LLDI0234]
            tmp4 = ShadowSurface.Point2X * tmp3;

            //Sub-expression: LLDI0236 = Plus[LLDI0231,LLDI0235]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0237 = Times[-1,LLDI001A]
            tmp4 = -ShadowSurface.Point1X;

            //Sub-expression: LLDI0238 = Plus[LLDI0017,LLDI0237]
            tmp4 = Point.X + tmp4;

            //Sub-expression: LLDI0239 = Times[-1,LLDI001E,LLDI0238]
            tmp5 = -1 * ShadowSurface.Point2Y * tmp4;

            //Sub-expression: LLDI023A = Plus[LLDI022B,LLDI0239]
            tmp0 = tmp0 + tmp5;

            //Sub-expression: LLDI023B = Times[-1,LLDI001B]
            tmp5 = -ShadowSurface.Point1Y;

            //Sub-expression: LLDI023C = Plus[LLDI0018,LLDI023B]
            tmp5 = Point.Y + tmp5;

            //Sub-expression: LLDI023D = Times[LLDI001D,LLDI023C]
            tmp6 = ShadowSurface.Point2X * tmp5;

            //Sub-expression: LLDI023E = Plus[LLDI023A,LLDI023D]
            tmp0 = tmp0 + tmp6;

            //Sub-expression: LLDI023F = Times[-1,LLDI0010,LLDI023E]
            tmp6 = -1 * Triangle.Point1Z * tmp0;

            //Sub-expression: LLDI0240 = Plus[LLDI0236,LLDI023F]
            tmp6 = tmp1 + tmp6;

            //Sub-expression: LLDI0241 = Times[-1,LLDI001F,LLDI0238]
            tmp4 = -1 * ShadowSurface.Point2Z * tmp4;

            //Sub-expression: LLDI0242 = Plus[LLDI022F,LLDI0241]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI0243 = Times[-1,LLDI001C]
            tmp4 = -ShadowSurface.Point1Z;

            //Sub-expression: LLDI0244 = Plus[LLDI0019,LLDI0243]
            tmp4 = Point.Z + tmp4;

            //Sub-expression: LLDI0245 = Times[LLDI001D,LLDI0244]
            tmp7 = ShadowSurface.Point2X * tmp4;

            //Sub-expression: LLDI0246 = Plus[LLDI0242,LLDI0245]
            tmp2 = tmp2 + tmp7;

            //Sub-expression: LLDI0247 = Times[LLDI000F,LLDI0246]
            tmp7 = Triangle.Point1Y * tmp2;

            //Sub-expression: LLDI0248 = Plus[LLDI0240,LLDI0247]
            tmp6 = tmp6 + tmp7;

            //Sub-expression: LLDI0249 = Times[-1,LLDI001F,LLDI023C]
            tmp5 = -1 * ShadowSurface.Point2Z * tmp5;

            //Sub-expression: LLDI024A = Plus[LLDI0234,LLDI0249]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI024B = Times[LLDI001E,LLDI0244]
            tmp4 = ShadowSurface.Point2Y * tmp4;

            //Sub-expression: LLDI024C = Plus[LLDI024A,LLDI024B]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI024D = Times[-1,LLDI000E,LLDI024C]
            tmp4 = -1 * Triangle.Point1X * tmp3;

            //Sub-expression: LLDI024E = Plus[LLDI0248,LLDI024D]
            tmp4 = tmp6 + tmp4;

            //Output: LLDI000B = Times[-1,LLDI024E]
            _d13 = -tmp4;

            //Sub-expression: LLDI024F = Times[-1,LLDI0013,LLDI023E]
            tmp4 = -1 * Triangle.Point2Z * tmp0;

            //Sub-expression: LLDI0250 = Plus[LLDI0236,LLDI024F]
            tmp4 = tmp1 + tmp4;

            //Sub-expression: LLDI0251 = Times[LLDI0012,LLDI0246]
            tmp5 = Triangle.Point2Y * tmp2;

            //Sub-expression: LLDI0252 = Plus[LLDI0250,LLDI0251]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI0253 = Times[-1,LLDI0011,LLDI024C]
            tmp5 = -1 * Triangle.Point2X * tmp3;

            //Sub-expression: LLDI0254 = Plus[LLDI0252,LLDI0253]
            tmp4 = tmp4 + tmp5;

            //Output: LLDI000C = Times[-1,LLDI0254]
            _d23 = -tmp4;

            //Sub-expression: LLDI0255 = Times[-1,LLDI0016,LLDI023E]
            tmp0 = -1 * Triangle.Point3Z * tmp0;

            //Sub-expression: LLDI0256 = Plus[LLDI0236,LLDI0255]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI0257 = Times[LLDI0015,LLDI0246]
            tmp1 = Triangle.Point3Y * tmp2;

            //Sub-expression: LLDI0258 = Plus[LLDI0256,LLDI0257]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0259 = Times[-1,LLDI0014,LLDI024C]
            tmp1 = -1 * Triangle.Point3X * tmp3;

            //Sub-expression: LLDI025A = Plus[LLDI0258,LLDI0259]
            tmp0 = tmp0 + tmp1;

            //Output: LLDI000D = Times[-1,LLDI025A]
            _d33 = -tmp0;


            if (_d00 >= 0)
            {
                if (_d13 < 0) _region1 |= 8;
                if (_d23 < 0) _region2 |= 8;
                if (_d33 < 0) _region3 |= 8;
            }
            else
            {
                if (_d13 >= 0) _region1 |= 8;
                if (_d23 >= 0) _region2 |= 8;
                if (_d33 >= 0) _region3 |= 8;
            }

            //Triangle is fully outside shadow area (region 1)
            if ((_region1 & 8) != 0 && (_region2 & 8) != 0 && (_region3 & 8) != 0)
                return GetComputerValues(false);


            //Finish GMac Macro Code Generation, 2018-10-11T15:17:36.9713111+02:00


            //Triangle is fully inside shadow area (region 1)
            if (_region1 == 1 || _region2 == 1 || _region3 == 1)
            {
                HasIntersection = true;

                ComputeTValues();

                return GetComputerValues(false);
            }


            HasIntersection = true;

            ComputeTValues();

            return GetComputerValues(false);
        }

        [Benchmark(Baseline = true)]
        public InternalComputerValues[] ComputeUsingGa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                Point = _pointsList[i];
                ShadowSurface = _shadowSurfacesList[i];
                Triangle = _trianglesList[i];

                ComputationResultsGa[i] = ComputeGa();
            }

            return ComputationResultsGa;
        }


        private InternalComputerValues ComputeVa()
        {
            ResetInternalVariables();

            var sp1 = Triangle.GetPoint1();
            var sp2 = Triangle.GetPoint2();
            var sp3 = Triangle.GetPoint3();

            var p0 = Point;

            var p1 = ShadowSurface.GetPoint1();
            var p2 = ShadowSurface.GetPoint2();
            var p3 = ShadowSurface.GetPoint3();

            _d00 = p0.GetSignedDistanceToPlaneVa(p1, p2, p3);

            _d10 = sp1.GetSignedDistanceToPlaneVa(p1, p2, p3);
            _d20 = sp2.GetSignedDistanceToPlaneVa(p1, p2, p3);
            _d30 = sp3.GetSignedDistanceToPlaneVa(p1, p2, p3);

            if (_d00 >= 0)
            {
                if (_d10 < 0) _region1 |= 1;
                if (_d20 < 0) _region2 |= 1;
                if (_d30 < 0) _region3 |= 1;
            }
            else
            {
                if (_d10 >= 0) _region1 |= 1;
                if (_d20 >= 0) _region2 |= 1;
                if (_d30 >= 0) _region3 |= 1;
            }

            //Triangle is fully outside shadow area (region 1)
            if (_region1 == 0 && _region2 == 0 && _region3 == 0)
                return GetComputerValues(false);


            _d12 = sp1.GetSignedDistanceToPlaneVa(p3, p0, p1);
            _d22 = sp2.GetSignedDistanceToPlaneVa(p3, p0, p1);
            _d32 = sp3.GetSignedDistanceToPlaneVa(p3, p0, p1);

            if (_d00 >= 0)
            {
                if (_d12 < 0) _region1 |= 4;
                if (_d22 < 0) _region2 |= 4;
                if (_d32 < 0) _region3 |= 4;
            }
            else
            {
                if (_d12 >= 0) _region1 |= 4;
                if (_d22 >= 0) _region2 |= 4;
                if (_d32 >= 0) _region3 |= 4;
            }

            //Triangle is fully outside shadow area (region 1)
            if ((_region1 & 4) != 0 && (_region2 & 4) != 0 && (_region3 & 4) != 0)
                return GetComputerValues(false);


            _d11 = -sp1.GetSignedDistanceToPlaneVa(p2, p3, p0);
            _d21 = -sp2.GetSignedDistanceToPlaneVa(p2, p3, p0);
            _d31 = -sp3.GetSignedDistanceToPlaneVa(p2, p3, p0);

            if (_d00 >= 0)
            {
                if (_d11 < 0) _region1 |= 2;
                if (_d21 < 0) _region2 |= 2;
                if (_d31 < 0) _region3 |= 2;
            }
            else
            {
                if (_d11 >= 0) _region1 |= 2;
                if (_d21 >= 0) _region2 |= 2;
                if (_d31 >= 0) _region3 |= 2;
            }

            //Line segment is fully outside shadow area (region 1)
            if ((_region1 & 2) != 0 && (_region2 & 2) != 0 && (_region3 & 2) != 0)
                return GetComputerValues(false);


            _d13 = -sp1.GetSignedDistanceToPlaneVa(p0, p1, p2);
            _d23 = -sp2.GetSignedDistanceToPlaneVa(p0, p1, p2);
            _d33 = -sp3.GetSignedDistanceToPlaneVa(p0, p1, p2);


            if (_d00 >= 0)
            {
                if (_d13 < 0) _region1 |= 8;
                if (_d23 < 0) _region2 |= 8;
                if (_d33 < 0) _region3 |= 8;
            }
            else
            {
                if (_d13 >= 0) _region1 |= 8;
                if (_d23 >= 0) _region2 |= 8;
                if (_d33 >= 0) _region3 |= 8;
            }

            //Triangle is fully outside shadow area (region 1)
            if ((_region1 & 8) != 0 && (_region2 & 8) != 0 && (_region3 & 8) != 0)
                return GetComputerValues(false);


            //Triangle is fully inside shadow area (region 1)
            if (_region1 == 1 || _region2 == 1 || _region3 == 1)
            {
                HasIntersection = true;

                ComputeTValues();

                return GetComputerValues(false);
            }


            HasIntersection = true;

            ComputeTValues();

            return GetComputerValues(false);
        }

        [Benchmark]
        public InternalComputerValues[] ComputeUsingVa()
        {
            for (var i = 0; i < OperationsCount; i++)
            {
                Point = _pointsList[i];
                ShadowSurface = _shadowSurfacesList[i];
                Triangle = _trianglesList[i];

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

                if (resultVa.Region3 != resultGa.Region3)
                    throw new InvalidOperationException();

                var diff = resultGa.GetMaxAbsDifference(resultVa);

                if (maxDiff < diff) maxDiff = diff;
            }

            return maxDiff;
        }
    }
}