using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors
{
    public partial class XGaProcessor<T>
    {

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> CreateIdentityRotor()
        {
            return XGaPureRotor<T>.Create(
                ScalarProcessor.OneValue,
                BivectorZero
            );
        }

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreateScaledIdentityRotor()
        {
            return XGaPureScalingRotor<T>.Create(
                ScalarProcessor.OneValue,
                BivectorZero
            );
        }

        /// <summary>
        /// Create an identity rotor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreateScaledIdentityRotor(T scalingFactor)
        {
            return XGaPureScalingRotor<T>.Create(
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
        public XGaPureRotor<T> CreateGivensRotor(int i, int j, LinPolarAngle<T> rotationAngle)
        {
            Debug.Assert(i >= 0 && j != i);

            var (cosHalfAngle, sinHalfAngle) =
                rotationAngle.HalfPolarAngle();

            return XGaPureRotor<T>.Create(
                cosHalfAngle.ScalarValue,
                BivectorTerm(i, j, sinHalfAngle.ScalarValue)
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
        public XGaPureScalingRotor<T> CreateScaledGivensRotor(int i, int j, LinPolarAngle<T> rotationAngle, T scalingFactor)
        {
            Debug.Assert(i >= 0 && j != i);

            var (cosHalfAngle, sinHalfAngle) =
                rotationAngle.HalfPolarAngle();

            var s = ScalarProcessor.Sqrt(scalingFactor);
            var scalarPart = ScalarProcessor.Times(s, cosHalfAngle);
            var bivectorPart = s * BivectorTerm(i, j, sinHalfAngle.ScalarValue);

            return XGaPureScalingRotor<T>.Create(scalarPart.ScalarValue, bivectorPart);
        }

        /// <summary>
        /// Create a scaled pure rotor in 2D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor2D(float r0, float r12)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor2D(double r0, double r12)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor2D(string r0, string r12)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 2D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor2D(T r0, T r12)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor3D(float r0, float r12, float r13, float r23)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .SetBivectorTerm(0, 2, r13)
                    .SetBivectorTerm(1, 2, r23)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor3D(double r0, double r12, double r13, double r23)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .SetBivectorTerm(0, 2, r13)
                    .SetBivectorTerm(1, 2, r23)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor3D(string r0, string r12, string r13, string r23)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .SetBivectorTerm(0, 2, r13)
                    .SetBivectorTerm(1, 2, r23)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor3D(T r0, T r12, T r13, T r23)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .SetBivectorTerm(0, 2, r13)
                    .SetBivectorTerm(1, 2, r23)
                    .GetMultivector()
            );
        }

        /// <summary>
        /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r0"></param>
        /// <param name="r12"></param>
        /// <param name="r13"></param>
        /// <param name="r23"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureScalingRotor<T> CreatePureScalingRotor3D(IScalar<T> r0, IScalar<T> r12, IScalar<T> r13, IScalar<T> r23)
        {
            return XGaPureScalingRotor<T>.Create(
                CreateMultivectorComposer()
                    .SetScalarTerm(r0)
                    .SetBivectorTerm(0, 1, r12)
                    .SetBivectorTerm(0, 2, r13)
                    .SetBivectorTerm(1, 2, r23)
                    .GetMultivector()
            );
        }

        public XGaPureRotor<T> CreateSimpleKirchhoffRotor(int vSpaceDimensions)
        {
            var v1 =
                VectorSymmetricUnit(vSpaceDimensions);

            var v2 = VectorTerm(
                vSpaceDimensions - 1
            );

            return v2.CreatePureRotor(v1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScalingRotor2D<T> CreateEuclideanScalingRotor2D(Scalar<T> scalar0, Scalar<T> scalar12)
        {
            return XGaEuclideanScalingRotor2D<T>.Create(
                this,
                scalar0,
                scalar12
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScalingRotor2D<T> CreateEuclideanScalingRotor2D(T scalar0, T scalar12)
        {
            return XGaEuclideanScalingRotor2D<T>.Create(
                this,
                ScalarProcessor.ScalarFromValue(scalar0),
                ScalarProcessor.ScalarFromValue(scalar12)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScalingRotorSquared2D<T> CreateEuclideanScalingRotorSquared2D(Scalar<T> scalar0, Scalar<T> scalar12)
        {
            return XGaEuclideanScalingRotorSquared2D<T>.Create(
                this,
                scalar0,
                scalar12
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaEuclideanScalingRotorSquared2D<T> CreateEuclideanScalingRotorSquared2D(T scalar0, T scalar12)
        {
            return XGaEuclideanScalingRotorSquared2D<T>.Create(
                this,
                scalar0.ScalarFromValue(ScalarProcessor),
                scalar12.ScalarFromValue(ScalarProcessor)
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[,] GetFinalMappingArray(IEnumerable<IXGaOutermorphism<T>> omSeq, int rowsCount)
        {
            return omSeq.OmMap(
                CreateFreeFrameOfBasis(rowsCount)
            ).GetArray(rowsCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaLinearMapOutermorphism<T> CreateOutermorphism(LinUnilinearMap<T> linearMap)
        {
            return new XGaLinearMapOutermorphism<T>(this, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureVersorsSequence<T> CreateIdentityVersor()
        {
            return XGaPureVersorsSequence<T>.CreateIdentity(this);
        }

        public XGaLinearMapOutermorphism<T> CreateClarkeRotationMap(int vectorsCount)
        {
            var clarkeMapArray =
                ScalarProcessor.CreateClarkeRotationArray(vectorsCount);

            var basisVectorImagesDictionary = 
                new Dictionary<int, LinVector<T>>();

            for (var i = 0; i < vectorsCount; i++)
                basisVectorImagesDictionary.Add(
                    i, 
                    clarkeMapArray.ColumnToLinVector(ScalarProcessor, i)
                );

            return ScalarProcessor.CreateLinUnilinearMap(
                basisVectorImagesDictionary
            ).ToOutermorphism(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureRotor<T> ComplexEigenPairToPureRotor(T realValue, T imagValue, XGaVector<T> realVector, XGaVector<T> imagVector)
        {
            //var scalar = scalarProcessor.Add(
            //    scalarProcessor.Times(realValue, realValue),
            //    scalarProcessor.Times(imagValue, imagValue)
            //);

            var angle = ScalarProcessor.ArcTan2(
                realValue,
                imagValue
            );

            return realVector.Op(imagVector).CreatePureRotor(angle);

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
    }
}
