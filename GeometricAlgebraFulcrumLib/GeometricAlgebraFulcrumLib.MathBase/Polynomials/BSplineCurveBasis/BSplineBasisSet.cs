using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using OxyPlot;
using OxyPlot.Series;
using PdfExporter = OxyPlot.SkiaSharp.PdfExporter;

namespace GeometricAlgebraFulcrumLib.MathBase.Polynomials.BSplineCurveBasis
{
    public class BSplineBasisSet :
        IBSplineBasisSet
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BSplineBasisSet Create(BSplineKnotVector knotVector, int degree)
        {
            return new BSplineBasisSet(knotVector, degree);
        }


        public int Degree { get; }

        public BSplineKnotVector KnotVector { get; }

        public int BasisCount 
            => KnotVector.GetBasisCount(Degree);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BSplineBasisSet(BSplineKnotVector knotVector, int degree)
        {
            if (degree < 0 || degree > knotVector.MaxDegree)
                throw new ArgumentOutOfRangeException(nameof(degree));

            Degree = degree;
            KnotVector = knotVector;
        }


        private double GetValue(int degree, int index, double parameterValue)
        {
            if (degree == 0)
                return KnotVector.Boxcar(index, parameterValue);

            var (ti, ti1) = KnotVector.GetKnotValueRange(index, index + 1);
            var (tin, tin1) = KnotVector.GetKnotValueRange(index + degree, index + degree + 1);

            var vi = GetValue(degree - 1, index, parameterValue);
            var vi1 = GetValue(degree - 1, index + 1, parameterValue);

            var a1 = vi * (parameterValue - ti);
            var a2 = tin - ti;

            var b1 = vi1 * (tin1 - parameterValue);
            var b2 = tin1 - ti1;

            var a = a1.IsZero() || a2.IsZero() ? 0d : a1 / a2;
            var b = b1.IsZero() || b2.IsZero() ? 0d : b1 / b2;

            return a + b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index, double parameterValue)
        {
            return GetValue(Degree, index, parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index, double parameterValue, double termScalar)
        {
            return termScalar * GetValue(index, parameterValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(double parameterValue, params double[] termScalarsList)
        {
            return termScalarsList.Select(
                (scalar, index) => GetValue(index, parameterValue, scalar)
            ).Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<double> GetValues(double parameterValue)
        {
            return Enumerable
                .Range(0, Degree + 1)
                .Select(index => GetValue(index, parameterValue))
                .ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Interval GetSupportInterval(int index)
        {
            return Float64Interval.Create(
                KnotVector[index],
                KnotVector[index + Degree + 1], 
                false, 
                false
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineBasisPairProductSet CreatePairProductSet()
        {
            return BSplineBasisPairProductSet.Create(this);
        }

        public void PlotBasisSet(string filePath, float width = 1024, float height = 768)
        {
            var pm = new PlotModel
            {
                Title = $"B-Spline Basis of Degree {Degree}",
                Background = OxyColor.FromRgb(255,255,255)
            };

            var a = KnotVector.FirstValue;
            var b = KnotVector.LastValue;
            var d = (b - a) * 0.1;
            a -= d;
            b += d;

            for (var index = 0; index < BasisCount; index++)
                pm.Series.Add(new FunctionSeries(
                    t => GetValue(index, t), 
                    a, b, (int) width
                ));
            
            PdfExporter.Export(pm, filePath, width, height);
            //PngExporter.Export(pm, filePath, width, height, 300);
        }

        public void PlotBasisSetPairProducts(string filePath, float width = 1024, float height = 768)
        {
            var pm = new PlotModel
            {
                Title = $"B-Spline Basis of Degree {Degree}",
                Background = OxyColor.FromRgb(255,255,255)
            };

            var a = KnotVector.FirstValue;
            var b = KnotVector.LastValue;
            var d = (b - a) * 0.1;
            a -= d;
            b += d;

            for (var index1 = 0; index1 < BasisCount; index1++)
            {
                for (var index2 = 0; index2 <= index1; index2++)
                {
                    pm.Series.Add(new FunctionSeries(
                        t => GetValue(index1, t) * GetValue(index2, t),
                        a, b, (int) width
                    ));
                }
            }
            
            PdfExporter.Export(pm, filePath, width, height);
        }
    }
}