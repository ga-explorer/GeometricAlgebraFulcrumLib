using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors
{
    public partial class XGaFloat64Processor
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureVersorsSequence IdentityVersor()
        {
            return XGaFloat64PureVersorsSequence.CreateIdentity(this);
        }

        
        public XGaFloat64LinearMapOutermorphism LinearMapOutermorphismFromColumns(double[,] array)
        {
            var vectorsCount = array.GetLength(1);

            var basisVectorImagesDictionary = 
                new Dictionary<int, LinFloat64Vector>();

            for (var i = 0; i < vectorsCount; i++)
            {
                var vector = array.ColumnToLinVector(i);

                if (!vector.IsZero)
                    basisVectorImagesDictionary.Add(i, vector);
            }

            return basisVectorImagesDictionary
                .ToLinUnilinearMap()
                .ToOutermorphism(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64LinearMapOutermorphism ClarkeRotationOutermorphism(int vectorsCount)
        {
            return LinearMapOutermorphismFromColumns(
                Float64ArrayUtils.CreateClarkeRotationArray(vectorsCount)
            );

            //var clarkeMapArray =
            //    Float64ArrayUtils.CreateClarkeRotationArray(vectorsCount);

            //var basisVectorImagesDictionary = 
            //    new Dictionary<int, LinFloat64Vector>();

            //for (var i = 0; i < vectorsCount; i++)
            //    basisVectorImagesDictionary.Add(
            //        i, 
            //        clarkeMapArray.ColumnToLinVector(i)
            //    );

            //return basisVectorImagesDictionary
            //    .ToLinUnilinearMap()
            //    .ToOutermorphism(this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64LinearMapOutermorphism ToOutermorphism(LinFloat64UnilinearMap linearMap)
        {
            return new XGaFloat64LinearMapOutermorphism(this, linearMap);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetFinalMappingArray(IEnumerable<IXGaFloat64Outermorphism> omSeq, int rowsCount)
        {
            return omSeq.OmMap(
                CreateFreeFrameOfBasis(rowsCount)
            ).GetArray(rowsCount);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureRotor ComplexEigenPairToPureRotor(double realValue, double imagValue, XGaFloat64Vector realVector, XGaFloat64Vector imagVector)
        {
            //var scalar = scalarProcessor.Add(
            //    scalarProcessor.Times(realValue, realValue),
            //    scalarProcessor.Times(imagValue, imagValue)
            //);

            var angle = LinFloat64PolarAngle.CreateFromVector(realValue, imagValue);

            return realVector.Op(imagVector).ToPureRotor(angle);

            //Console.WriteLine($"Eigen value real part: {realValue.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value imag part: {imagValue.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value length: {scalar.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine($"Eigen value angle: {angle.GetLaTeXDisplayEquation()}");
            //Console.WriteLine();

            //Console.WriteLine("Eigen vector real part:");
            //Console.WriteLine(realVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            //Console.WriteLine();

            //Console.WriteLine("Eigen vector imag part:");
            //Console.WriteLine(imagVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            //Console.WriteLine();

            //Console.WriteLine("Blade:");
            //Console.WriteLine(blade.ToLaTeXEquationsArray("B", @"\mu"));
            //Console.WriteLine();

            //Console.WriteLine("Final rotor:");
            //Console.WriteLine(rotor.ToLaTeXEquationsArray("R", @"\mu"));
            //Console.WriteLine();

            //Console.WriteLine($"Is simple rotor? {rotor.IsSimpleRotor()}");
            //Console.WriteLine();

            //Console.WriteLine();

            //return rotor;
        }


        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureRotor IdentityRotor()
        {
            return XGaFloat64PureRotor.Create(
                1d,
                BivectorZero
            );
        }

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor IdentityScalingRotor()
        {
            return XGaFloat64PureScalingRotor.Create(
                1d,
                BivectorZero
            );
        }

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor IdentityScalingRotor(double scalingFactor)
        {
            return XGaFloat64PureScalingRotor.Create(
                scalingFactor,
                BivectorZero
            );
        }


        /// <summary>
        /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureRotor GivensRotor(int i, int j, LinFloat64PolarAngle rotationAngle)
        {
            Debug.Assert(i >= 0 && j != i);

            var (cosHalfAngle, sinHalfAngle) =
                rotationAngle.HalfPolarAngle();

            return XGaFloat64PureRotor.Create(
                cosHalfAngle,
                BivectorTerm(i, j, sinHalfAngle)
            );
        }

        /// <summary>
        /// Construct a scaled rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor GivensScalingRotor(int i, int j, LinFloat64PolarAngle rotationAngle, double scalingFactor)
        {
            Debug.Assert(i >= 0 && j != i);

            var (cosHalfAngle, sinHalfAngle) =
                rotationAngle.HalfPolarAngle();

            var s = scalingFactor.Sqrt();
            var scalarPart = s * cosHalfAngle;
            var bivectorPart = s * BivectorTerm(i, j, sinHalfAngle);

            return XGaFloat64PureScalingRotor.Create(scalarPart, bivectorPart);
        }
        
        /// <summary>
        /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor GivensScalingRotor(int i, int j, LinFloat64Angle rotationAngle)
        {
            Debug.Assert(j != i);

            var halfRotationAngle = 0.5 * rotationAngle.RadiansValue;
            var (sinHalfAngle, cosHalfAngle) = halfRotationAngle.SinCos();

            var bivectorPart = BivectorTerm(
                new Pair<int>(i, j),
                i < j ? sinHalfAngle : -sinHalfAngle
            );

            //var bivectorPart = this.CreateBivectorTerm(i, j, sinHalfAngle);

            return XGaFloat64PureScalingRotor.Create(
                cosHalfAngle + bivectorPart,
                cosHalfAngle - bivectorPart
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 2D Euclidean space directly using its scalar components
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor PureScalingRotor2D(float r0, float r12)
        {
            return XGaFloat64PureScalingRotor.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .GetSimpleMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor PureScalingRotor2D(double r0, double r12)
        {
            return XGaFloat64PureScalingRotor.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .GetSimpleMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor PureScalingRotor3D(float r0, float r12, float r13, float r23)
        {
            return XGaFloat64PureScalingRotor.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .SetBivectorTerm(0, 2, r13)
                    .SetBivectorTerm(1, 2, r23)
                    .GetSimpleMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor PureScalingRotor3D(double r0, double r12, double r13, double r23)
        {
            return XGaFloat64PureScalingRotor.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .SetBivectorTerm(0, 2, r13)
                    .SetBivectorTerm(1, 2, r23)
                    .GetSimpleMultivector()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64EuclideanScalingRotor2D EuclideanScalingRotor2D(double scalar0, double scalar12)
        {
            return XGaFloat64EuclideanScalingRotor2D.Create(
                this,
                scalar0,
                scalar12
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64EuclideanScalingRotorSquared2D EuclideanScalingRotorSquared2D(double scalar0, double scalar12)
        {
            return XGaFloat64EuclideanScalingRotorSquared2D.Create(this, scalar0, scalar12);
        }
        

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor IdentityPureScalingRotor()
        {
            var multivector =
                Scalar(1);

            return XGaFloat64PureScalingRotor.Create(
                multivector,
                multivector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor IdentityPureScalingRotor(double scalingFactor)
        {
            if (scalingFactor <= 0)
                throw new ArgumentOutOfRangeException(nameof(scalingFactor));

            var multivector =
                Scalar(Math.Sqrt(scalingFactor));

            return XGaFloat64PureScalingRotor.Create(
                multivector,
                multivector
            );
        }

        /// <summary>
        /// Create one rotor from the parametric family of pure rotors taking
        /// sourceVector to targetVector in 3D Euclidean space
        /// </summary>
        /// <param name="sourceVector"></param>
        /// <param name="targetVector"></param>
        /// <param name="angleTheta"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor EuclideanParametricPureScalingRotor3D(ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, LinFloat64Angle angleTheta)
        {
            //var this = BasisBladeSet.CreateEuclidean(3);

            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                BasisBlade((IndexSet)7).ToKVector().EInverse();

            // Compute the smallest angle between source and target vectors
            var cosAngle0 =
                sourceVector.VectorESp(targetVector);

            // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
            var rotorSBlade =
                targetVector.VectorSubtract(sourceVector).ToXGaFloat64Vector().EGp(pseudoScalarInverse).GetBivectorPart();

            var rotorS =
                rotorSBlade.ToEuclideanPureScalingRotor(angleTheta);

            // Define parametric 2-blade of rotation
            // The actual plane of rotation is made by rotating the plane containing
            // sourceVector and targetVector by angle theta in the plane orthogonal to
            // targetVector - sourceVector using rotor S
            var rotorBlade =
                rotorS.OmMap(targetVector.ToXGaFloat64Vector().Op(sourceVector.ToXGaFloat64Vector())).GetBivectorPart();

            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0 - 1) / (2 - angleTheta.Sin().Square() * (cosAngle0 + 1))).ArcCos();

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

            // Return the final rotor taking v1 into v2
            return rotorBlade.ToEuclideanPureScalingRotor(rotorAngle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor EuclideanParametricPureScalingRotor3D(ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, LinFloat64Angle angleTheta, bool assumeUnitVectors = false)
        {
            if (assumeUnitVectors)
                EuclideanParametricPureScalingRotor3D(sourceVector, targetVector, angleTheta);

            var (sourceVectorUnit, sourceVectorLength) =
                sourceVector.GetUnitVectorENormTuple();

            var (targetVectorUnit, targetVectorLength) =
                targetVector.GetUnitVectorENormTuple();

            var scalingFactor = targetVectorLength / sourceVectorLength;

            return EuclideanParametricPureScalingRotor3D(sourceVectorUnit, targetVectorUnit, angleTheta)
                .CreatePureScalingRotor(scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor EuclideanParametricPureScalingRotor3D(ILinFloat64Vector3D sourceVector, ILinFloat64Vector3D targetVector, LinFloat64Angle angleTheta, double scalingFactor)
        {
            return EuclideanParametricPureScalingRotor3D(sourceVector, targetVector, angleTheta)
                .CreatePureScalingRotor(scalingFactor);
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceBasisVectorIndex"></param>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor EuclideanPureScalingRotor(int sourceBasisVectorIndex, XGaFloat64Vector targetVector, bool assumeUnitVector = false)
        {
            var ek = BasisVector(sourceBasisVectorIndex).ToKVector();
            var vk = targetVector.GetTermScalarByIndex(sourceBasisVectorIndex);
            var vk1 = 1 + vk;

            var scalarPart = (vk1 / 2).Sqrt();
            var bivectorPart = (targetVector - vk * ek).Op(ek) / (2 * vk1).Sqrt();

            return XGaFloat64PureScalingRotor.Create(
                scalarPart + bivectorPart,
                scalarPart - bivectorPart
            );
        }

    }
}
