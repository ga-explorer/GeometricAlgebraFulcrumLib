using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Optimization.GradientDescent
{
    public class AdamOptimizer
    {
        private const double Beta1 = 0.9d;
        private const double Beta2 = 0.999d;
        private const double Epsilon = 0.1d;

        private Matrix<double> _m;
        private Matrix<double> _v;

        public double StepSize { get; }


        public AdamOptimizer(int size, double stepSize)
        {
            _m = Matrix<double>.Build.Dense(size, size);
            _v = Matrix<double>.Build.Dense(size, size);

            StepSize = stepSize;
        }

        public void UpdateWeights(ref Matrix<double> weights, Matrix<double> gradient, int iteration)
        {
            _m = Beta1 * _m + (1 - Beta1) * gradient;
            _v = Beta2 * _v + (1 - Beta2) * gradient.PointwisePower(2);

            var mHat = _m / (1 - Math.Pow(Beta1, iteration));
            var vHat = _v / (1 - Math.Pow(Beta2, iteration));

            weights -= StepSize * mHat.PointwiseDivide(vHat.PointwiseSqrt().Add(Epsilon));
        }
    }
}
