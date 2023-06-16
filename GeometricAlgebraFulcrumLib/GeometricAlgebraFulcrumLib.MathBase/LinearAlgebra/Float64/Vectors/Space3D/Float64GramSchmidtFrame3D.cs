using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public class Float64GramSchmidtFrame3D
    {
        public static Float64GramSchmidtFrame3D Create(Float64Vector3D v1, Float64Vector3D v2, Float64Vector3D v3)
        {
            var array = new double[,]
            {
                { v1.X, v2.X, v3.X },
                { v1.Y, v2.Y, v3.Y },
                { v1.Z, v2.Z, v3.Z }
            };

            var matrix = Matrix<double>.Build.DenseOfArray(array);

            var qr = matrix.QR();

            var n1 = qr.R[0, 0];
            var n2 = qr.R[1, 1];
            var n3 = qr.R[2, 2];

            var e1 = n1 >= 0
                ? Float64Vector3D.Create(qr.Q[0, 0], qr.Q[1, 0], qr.Q[2, 0])
                : Float64Vector3D.Create(-qr.Q[0, 0], -qr.Q[1, 0], -qr.Q[2, 0]);

            var e2 = n2 >= 0
                ? Float64Vector3D.Create(qr.Q[0, 1], qr.Q[1, 1], qr.Q[2, 1])
                : Float64Vector3D.Create(-qr.Q[0, 1], -qr.Q[1, 1], -qr.Q[2, 1]);

            var e3 = n3 >= 0
                ? Float64Vector3D.Create(qr.Q[0, 2], qr.Q[1, 2], qr.Q[2, 2])
                : Float64Vector3D.Create(-qr.Q[0, 2], -qr.Q[1, 2], -qr.Q[2, 2]);

            return new Float64GramSchmidtFrame3D(
                n1.Abs(), n2.Abs(), n3.Abs(),
                e1, e2, e3
            );
        }


        public double Direction1Norm { get; private set; }

        public double Direction2Norm { get; private set; }

        public double Direction3Norm { get; private set; }

        public Float64Vector3D UnitDirection1 { get; }

        public Float64Vector3D UnitDirection2 { get; }

        public Float64Vector3D UnitDirection3 { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64GramSchmidtFrame3D(double direction1Norm, double direction2Norm, double direction3Norm, Float64Vector3D unitDirection1, Float64Vector3D unitDirection2, Float64Vector3D unitDirection3)
        {
            Direction1Norm = direction1Norm;
            Direction2Norm = direction2Norm;
            Direction3Norm = direction3Norm;

            UnitDirection1 = unitDirection1;
            UnitDirection2 = unitDirection2;
            UnitDirection3 = unitDirection3;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64GramSchmidtFrame3D CleanNorms(double epsilon = 1e-12)
        {
            if (Direction1Norm.IsNearZero(epsilon))
                Direction1Norm = 0d;

            if (Direction2Norm.IsNearZero(epsilon))
                Direction2Norm = 0d;

            if (Direction3Norm.IsNearZero(epsilon))
                Direction3Norm = 0d;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDirection1()
        {
            return Direction1Norm * UnitDirection1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDirection2()
        {
            return Direction2Norm * UnitDirection2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDirection3()
        {
            return Direction3Norm * UnitDirection3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetCurvature12()
        {
            return Direction2Norm / Direction1Norm;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetCurvature23()
        {
            return Direction3Norm / Direction2Norm;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D GetDarbouxBlade12()
        {
            return GetCurvature12() * UnitDirection1.Op(UnitDirection2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D GetDarbouxBlade23()
        {
            return GetCurvature23() * UnitDirection2.Op(UnitDirection3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D GetDarbouxBivector()
        {
            return GetDarbouxBlade12() + GetDarbouxBlade23();
        }
    }
}