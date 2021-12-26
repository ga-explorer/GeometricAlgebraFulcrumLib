using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;

namespace NumericalGeometryLib.GeometricAlgebra
{
    public static class GeometricAlgebraUtils
    {
        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is 1
        /// the original basis blade Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsPositiveOfGrade(this uint grade)
        {
            return (grade & 1) == 0;
        }

        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsNegativeOfGrade(this uint grade)
        {
            return (grade & 1) != 0;
        }
        
        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GradeInvolutionSignOfGrade(this uint grade)
        {
            return (grade & 1) != 0 ? -1 : 1;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsPositiveOfGrade(this uint grade)
        {
            return grade % 4 < 2;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsNegativeOfGrade(this uint grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReverseSignOfGrade(this uint grade)
        {
            return (grade % 4 > 1) ? -1 : 1;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsPositiveOfGrade(this uint grade)
        {
            return (grade % 4) is 0 or 3;

            //return ((grade * (grade + 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsNegativeOfGrade(this uint grade)
        {
            return (grade % 4) is 1 or 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CliffordConjugateSignOfGrade(this uint grade)
        {
            return ((grade % 4) is 1 or 2) ? -1 : 1;
        }


        public static int GetMinScalarMagnitudeIndex(this double[] scalarsArray)
        {
            var minValue = scalarsArray[0].Abs();
            var minValueIndex = 0;

            if (minValue == 0d) return minValueIndex;

            for (var index = 1; index < scalarsArray.Length; index++)
            {
                var absNumber = scalarsArray[index].Abs();

                if (absNumber >= minValue) continue;

                minValue = absNumber;
                minValueIndex = index;

                if (minValue == 0d) return minValueIndex;
            }

            return minValueIndex;
        }

        public static GaMultivector VectorUnitNormal(this GaMultivector vector)
        {
            var basisSet = vector.BasisSet;
            var scalarsArray = 
                vector.VectorPartAsArray();

            var minValueIndex = 
                scalarsArray.GetMinScalarMagnitudeIndex();

            var minValue = 
                scalarsArray[minValueIndex];

            var sum = 0d;
            for (var index = 0; index < scalarsArray.Length; index++)
            {
                if (index == minValueIndex) continue;

                var signature = basisSet.GetBasisVectorSignature(index);

                if (signature == 1)
                {
                    sum += scalarsArray[index];
                    scalarsArray[index] = minValue;
                }
                else if (signature == -1)
                {
                    sum -= scalarsArray[index];
                    scalarsArray[index] = -minValue;
                }
            }

            scalarsArray[minValueIndex] = -sum;

            var v = basisSet.CreateVector(scalarsArray);
            return v / v.Norm();
        }

        public static GaMultivector VectorEUnitNormal(this GaMultivector vector)
        {
            var basisSet = vector.BasisSet;
            var scalarsArray = 
                vector.VectorPartAsArray();

            var minValueIndex = 
                scalarsArray.GetMinScalarMagnitudeIndex();

            var minValue = 
                scalarsArray[minValueIndex];

            var sum = 0d;
            for (var index = 0; index < scalarsArray.Length; index++)
            {
                if (index == minValueIndex) continue;

                sum += scalarsArray[index];
                scalarsArray[index] = minValue;
            }

            scalarsArray[minValueIndex] = -sum;

            var v = basisSet.CreateVector(scalarsArray);
            return v / v.ENorm();
        }
    }
}