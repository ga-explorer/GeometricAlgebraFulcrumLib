using System;
using BenchmarkDotNet.Attributes;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Structures
{
    [SimpleJob]
    public class ScalarOperationsBenchmarks
    {
        public int Count { get; } = 10000;

        public Random RandomGenerator { get; } = new Random();


        [GlobalSetup]
        public void Setup()
        {

        }
        
        [Benchmark(Baseline = true)]
        public double Baseline()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = +y;
                }
            }

            return s;
        }

        [Benchmark]
        public double Add()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = y + x2;
                }
            }

            return s;
        }

        [Benchmark]
        public double Subtract()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = y - x2;
                }
            }

            return s;
        }

        [Benchmark]
        public double Times()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = y * x2;
                }
            }

            return s;
        }

        [Benchmark]
        public double Divide()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = y / x2;
                }
            }

            return s;
        }

        [Benchmark]
        public double Negative()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = -y;
                }
            }

            return s;
        }

        [Benchmark]
        public double TimesConstant()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = y * -3.675;
                }
            }

            return s;
        }

        [Benchmark]
        public double ReciprocalEstimate()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.ReciprocalEstimate(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ReciprocalSqrtEstimate()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.ReciprocalSqrtEstimate(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Abs()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Abs(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Ceiling()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Ceiling(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Floor()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Floor(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Truncate()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Truncate(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Round()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Round(y, 5);
                }
            }

            return s;
        }

        [Benchmark]
        public double Sign()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Sign(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double IEEERemainder()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.IEEERemainder(y, x2);
                }
            }

            return s;
        }

        [Benchmark]
        public double ScaleB()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.ScaleB(y, 7);
                }
            }

            return s;
        }

        [Benchmark]
        public double Sqrt()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Sqrt(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Cbrt()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Cbrt(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Power()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Pow(y, x2);
                }
            }

            return s;
        }

        [Benchmark]
        public double Exp()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Exp(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Log()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Log(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Log2()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Log2(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Log10()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Log10(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double LogBase()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Log(y, s1);
                }
            }

            return s;
        }

        [Benchmark]
        public double LogBaseConstant()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Log(y, 3.765);
                }
            }

            return s;
        }

        [Benchmark]
        public double ILogB()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.ILogB(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Min()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Min(y, x2);
                }
            }

            return s;
        }

        [Benchmark]
        public double Max()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Max(y, x2);
                }
            }

            return s;
        }

        [Benchmark]
        public double Sin()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Sin(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Cos()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Cos(y);
                }
            }

            return s;
        }
        
        [Benchmark]
        public double SqrtOfOneMinusSinSquared()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Sin(y);
                    s = Math.Sqrt(1 - s * s);
                }
            }

            return s;
        }

        [Benchmark]
        public double SqrtOfOneMinusSinSquaredSigned()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    var sinY = Math.Sin(y);
                    s = Math.Sqrt(1 - sinY * sinY);
                    if (((int)Math.Floor(2 * y / Math.PI) % 4 + 4) % 4 is 1 or 2) s = -s;
                }
            }

            return s;
        }

        //[Benchmark]
        //public double SinCos1()
        //{
        //    var s = 0d;

        //    for (var i = 0; i < Count; i++)
        //    {
        //        var s1 = RandomGenerator.NextDouble();

        //        for (var j = 0; j < Count; j++)
        //        {
        //            var s2 = RandomGenerator.NextDouble();

        //            var x1 = s1 + s2;
        //            var x2 = s1 - s2;

        //            var y = x1 + x2;
        //            var y1 = Math.Sin(y);
        //            var y2 = Math.Cos(y);

        //            s = y1 + y2;
        //        }
        //    }

        //    return s;
        //}

        //[Benchmark]
        //public double SinCos2()
        //{
        //    var s = 0d;

        //    for (var i = 0; i < Count; i++)
        //    {
        //        var s1 = RandomGenerator.NextDouble();

        //        for (var j = 0; j < Count; j++)
        //        {
        //            var s2 = RandomGenerator.NextDouble();

        //            var x1 = s1 + s2;
        //            var x2 = s1 - s2;

        //            var y = x1 + x2;
        //            var y1 = Math.Sin(y);

        //            var y2 = Math.Sign(
        //                Math.PI / 2 - Math.Abs(y)
        //            ) * Math.Sqrt(1 - y1 * y1);

        //            //var y2 = Math.Sqrt(1 - y1 * y1);

        //            //if ((y >= 0 && y <= Math.PI / 2) || (y <= 0 && y >= -Math.PI / 2))
        //            //    y2 = -y2;

        //            s = y1 + y2;
        //        }
        //    }

        //    return s;
        //}

        //[Benchmark]
        //public double SinCos3()
        //{
        //    var s = 0d;

        //    for (var i = 0; i < Count; i++)
        //    {
        //        var s1 = RandomGenerator.NextDouble();

        //        for (var j = 0; j < Count; j++)
        //        {
        //            var s2 = RandomGenerator.NextDouble();

        //            var x1 = s1 + s2;
        //            var x2 = s1 - s2;

        //            var y = x1 + x2;
        //            var (y1, y2) = Math.SinCos(y);

        //            s = y1 + y2;
        //        }
        //    }

        //    return s;
        //}

        [Benchmark]
        public double Tan()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Tan(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ArcCos()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Acos(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ArcSin()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Asin(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ArcTan()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Atan(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ArcTan2()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Atan2(y, x2);
                }
            }

            return s;
        }

        [Benchmark]
        public double Cosh()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Cosh(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Sinh()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Sinh(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Tanh()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Tanh(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ArcCosh()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Acosh(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ArcSinh()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Asinh(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double ArcTanh()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Atanh(y);
                }
            }

            return s;
        }

        [Benchmark]
        public double Clamp()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.Clamp(y, -0.5, 0.5);
                }
            }

            return s;
        }

        [Benchmark]
        public double FusedMultiplyAdd()
        {
            var s = 0d;

            for (var i = 0; i < Count; i++)
            {
                var s1 = RandomGenerator.NextDouble();

                for (var j = 0; j < Count; j++)
                {
                    var s2 = RandomGenerator.NextDouble();

                    var x1 = s1 + s2;
                    var x2 = s1 - s2;
                    var y = x1 + x2;

                    s = Math.FusedMultiplyAdd(y, x1, x2);
                }
            }

            return s;
        }

    }
}
