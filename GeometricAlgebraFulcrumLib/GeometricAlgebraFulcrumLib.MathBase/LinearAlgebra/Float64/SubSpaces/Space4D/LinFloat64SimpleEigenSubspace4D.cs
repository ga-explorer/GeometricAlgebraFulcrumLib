using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.SubSpaces.Space4D
{
    public sealed class LinFloat64SimpleEigenSubspace4D :
        ILinFloat64Subspace4D
    {
        public int VSpaceDimensions
            => 4;

        public int SubspaceDimensions
            => Subspace.SubspaceDimensions;

        public IEnumerable<Float64Vector4D> BasisVectors
            => Subspace.BasisVectors;

        public ILinFloat64Subspace4D Subspace { get; }

        public Complex EigenValue { get; }


        public LinFloat64SimpleEigenSubspace4D(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
        {
            EigenValue = eigenValue;

            if (eigenValue.IsNearOne())
            {
                var u =
                    eigenVector.Imaginary().ToTuple4D();

                // Eigen value is real one
                Subspace = LinFloat64LineSubspace4D.CreateFromVector(u);

                return;
            }

            var eigenValueRealPart = eigenValue.Real;
            var eigenValueImagPart = eigenValue.Imaginary;

            if (eigenValueImagPart.IsNearZero())
            {
                if (eigenValueRealPart.IsNearZero())
                {
                    // Eigen value is near zero
                    Subspace = LinFloat64NullSubspace4D.Instance;

                    // Make sure the eigen vector is near zero
                    Debug.Assert(
                        eigenVector.L2Norm().IsNearZero()
                    );
                }
                else
                {
                    var u =
                        eigenVector.Real().ToTuple4D();

                    // Eigen value is real
                    Subspace = LinFloat64LineSubspace4D.CreateFromVector(u);

                    Debug.Assert(
                        !eigenVector.Real().IsNearZero()
                    );
                }
            }
            else
            {
                // Eigen value is complex
                var u = eigenVector.Imaginary().ToTuple4D();
                var v = eigenVector.Real().ToTuple4D();

                Subspace = LinFloat64PlaneSubspace4D.CreateFromVectors(u, v);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(IFloat64Tuple4D vector, double epsilon = 1E-12D)
        {
            return Subspace.NearContains(vector, epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(ILinFloat64Subspace4D subspace, double epsilon = 1E-12)
        {
            return subspace.VSpaceDimensions <= VSpaceDimensions &&
                   subspace.BasisVectors.All(v => NearContains(v, epsilon));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotation4D GetVectorToVectorRotation()
        {
            if (Subspace is not LinFloat64PlaneSubspace4D planeSubspace)
                throw new InvalidOperationException();

            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            ).RadiansToAngle();

            return LinFloat64VectorToVectorRotation4D.Create(
                planeSubspace.BasisVector1,
                planeSubspace.BasisVector2,
                angle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64VectorToVectorRotation4D> GetVectorToVectorRotationMaps()
        {
            if (Subspace is LinFloat64PlaneSubspace4D planeSubspace)
            {
                var angle = Math.Atan2(
                    EigenValue.Imaginary,
                    EigenValue.Real
                );

                yield return LinFloat64VectorToVectorRotation4D.Create(
                    planeSubspace.BasisVector1,
                    planeSubspace.BasisVector2,
                    angle
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64HyperPlaneNormalReflection4D> GetHyperPlaneNormalReflectionMaps()
        {
            if (Subspace is LinFloat64PlaneSubspace4D planeSubspace)
            {
                var angle = Math.Atan2(
                    EigenValue.Imaginary,
                    EigenValue.Real
                );

                var (r1, r2) =
                    LinFloat64VectorToVectorRotation4D.Create(
                        planeSubspace.BasisVector1,
                        planeSubspace.BasisVector2,
                        angle
                    ).GetHyperPlaneReflectionPair();

                yield return r1;
                yield return r2;
            }
            else if (Subspace is LinFloat64LineSubspace4D lineSubspace)
            {
                if (EigenValue.IsNearMinusOne())
                    yield return LinFloat64HyperPlaneNormalReflection4D.Create(lineSubspace.BasisVector);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64VectorDirectionalScaling4D> GetVectorDirectionalScalingMaps()
        {
            if (Subspace is LinFloat64PlaneSubspace4D planeSubspace)
            {
                var angle = Math.Atan2(
                    EigenValue.Imaginary,
                    EigenValue.Real
                );

                var (r1, r2) =
                    LinFloat64VectorToVectorRotation4D.Create(
                        planeSubspace.BasisVector1,
                        planeSubspace.BasisVector2,
                        angle
                    ).GetHyperPlaneReflectionPair();

                yield return r1.ToVectorDirectionalScaling();
                yield return r2.ToVectorDirectionalScaling();
            }
            else if (Subspace is LinFloat64LineSubspace4D lineSubspace)
            {
                var r1 =
                    lineSubspace.BasisVector.CreateDirectionalScaling4D(EigenValue.Real);

                yield return r1.ToVectorDirectionalScaling();
            }
        }


        public override string ToString()
        {
            var composer = new StringBuilder();

            composer.AppendLine($"Dimensions: {SubspaceDimensions}");
            composer.AppendLine($"Eigen Value: {EigenValue}");

            if (SubspaceDimensions == 2)
            {
                var angle =
                    Math.Atan2(EigenValue.Imaginary, EigenValue.Real).RadiansToDegrees();

                composer.AppendLine($"Rotation Angle: {angle:F3} degrees");
            }

            var j = 1;
            foreach (var vector in BasisVectors)
            {
                composer.AppendLine($"Basis Vector {j}: {vector}");

                j++;
            }

            return composer.ToString();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}