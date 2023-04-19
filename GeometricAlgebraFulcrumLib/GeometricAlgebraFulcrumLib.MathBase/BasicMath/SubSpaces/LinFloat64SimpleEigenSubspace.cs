using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Scaling;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.SubSpaces
{
    public sealed class LinFloat64SimpleEigenSubspace :
        ILinFloat64Subspace
    {
        public int VSpaceDimensions 
            => Subspace.VSpaceDimensions;
    
        public int SubspaceDimensions 
            => Subspace.SubspaceDimensions;

        public IEnumerable<LinFloat64Vector> BasisVectors 
            => Subspace.BasisVectors;

        public ILinFloat64Subspace Subspace { get; }
    
        public Complex EigenValue { get; }
    

        public LinFloat64SimpleEigenSubspace(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
        {
            EigenValue = eigenValue;

            if (eigenValue.IsNearOne())
            {
                var u = 
                    eigenVector.Imaginary().ToArray().CreateLinVector();

                // Eigen value is real one
                Subspace = LinFloat64LineSubspace.CreateFromVector(u);

                return;
            }

            var eigenValueRealPart = eigenValue.Real;
            var eigenValueImagPart = eigenValue.Imaginary;

            if (eigenValueImagPart.IsNearZero())
            {
                if (eigenValueRealPart.IsNearZero())
                {
                    // Eigen value is near zero
                    Subspace = LinFloat64NullSubspace.Create(eigenVector.Count);

                    // Make sure the eigen vector is near zero
                    Debug.Assert(
                        eigenVector.L2Norm().IsNearZero()
                    );
                }
                else
                {
                    var u = 
                        eigenVector.Real().CreateLinVector();

                    // Eigen value is real
                    Subspace = LinFloat64LineSubspace.CreateFromVector(u);

                    Debug.Assert(
                        !eigenVector.Real().IsNearZero()
                    );
                }
            }
            else
            {
                // Eigen value is complex
                var u = eigenVector.Imaginary().ToArray().CreateLinVector();
                var v = eigenVector.Real().ToArray().CreateLinVector();

                Subspace = LinFloat64PlaneSubspace.CreateFromVectors(u, v);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(LinFloat64Vector vector, double epsilon = 1E-12D)
        {
            return Subspace.NearContains(vector, epsilon);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1E-12)
        {
            return subspace.VSpaceDimensions <= VSpaceDimensions && 
                   subspace.BasisVectors.All(v => NearContains(v, epsilon));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorToVectorRotation GetVectorToVectorRotation()
        {
            if (Subspace is not LinFloat64PlaneSubspace planeSubspace)
                throw new InvalidOperationException();

            var angle = Math.Atan2(
                EigenValue.Imaginary,
                EigenValue.Real
            );

            return LinFloat64VectorToVectorRotation.Create(
                planeSubspace.BasisVector1, 
                planeSubspace.BasisVector2, 
                angle
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64VectorToVectorRotation> GetVectorToVectorRotationMaps()
        {
            if (Subspace is LinFloat64PlaneSubspace planeSubspace)
            {
                var angle = Math.Atan2(
                    EigenValue.Imaginary,
                    EigenValue.Real
                );

                yield return LinFloat64VectorToVectorRotation.Create(
                    planeSubspace.BasisVector1,
                    planeSubspace.BasisVector2,
                    angle
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64HyperPlaneNormalReflection> GetHyperPlaneNormalReflectionMaps()
        {
            if (Subspace is LinFloat64PlaneSubspace planeSubspace)
            {
                var angle = Math.Atan2(
                    EigenValue.Imaginary,
                    EigenValue.Real
                );

                var (r1, r2) = 
                    LinFloat64VectorToVectorRotation.Create(
                        planeSubspace.BasisVector1,
                        planeSubspace.BasisVector2,
                        angle
                    ).GetHyperPlaneReflectionPair();

                yield return r1;
                yield return r2;
            }
            else if (Subspace is LinFloat64LineSubspace lineSubspace)
            {
                if (EigenValue.IsNearMinusOne())
                    yield return LinFloat64HyperPlaneNormalReflection.Create(lineSubspace.BasisVector);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<LinFloat64VectorDirectionalScaling> GetVectorDirectionalScalingMaps()
        {
            if (Subspace is LinFloat64PlaneSubspace planeSubspace)
            {
                var angle = Math.Atan2(
                    EigenValue.Imaginary,
                    EigenValue.Real
                );

                var (r1, r2) = 
                    LinFloat64VectorToVectorRotation.Create(
                        planeSubspace.BasisVector1,
                        planeSubspace.BasisVector2,
                        angle
                    ).GetHyperPlaneReflectionPair();

                yield return r1.ToVectorDirectionalScaling();
                yield return r2.ToVectorDirectionalScaling();
            }
            else if (Subspace is LinFloat64LineSubspace lineSubspace)
            {
                var r1 = 
                    lineSubspace.BasisVector.CreateDirectionalScaling(EigenValue.Real);

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
    }
}