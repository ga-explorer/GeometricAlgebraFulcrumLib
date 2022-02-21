using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.GeometricAlgebra.Basis;
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

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector EGp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).EGp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector EGp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.EGp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector EGp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).EGp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector EGp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.EGp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector EGp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).EGp(basisSet.CreateVector(mv2));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Gp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Gp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Gp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.Gp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Gp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Gp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Gp(this GaMultivector mv1, ITuple2D mv2)
        {
            return mv1.Gp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Gp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.Gp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Gp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).Gp(basisSet.CreateVector(mv2));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ESp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).ESp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ESp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.ESp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ESp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).ESp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ESp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.ESp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ESp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).ESp(basisSet.CreateVector(mv2));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Sp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.Sp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Sp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.Sp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).Sp(basisSet.CreateVector(mv2));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Op(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.Op(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this ITuple2D mv1, ITuple2D mv2)
        {
            return BasisBladeSet.Euclidean2D.CreateVector(mv1).Op(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this ITuple3D mv1, ITuple3D mv2)
        {
            return BasisBladeSet.Euclidean3D.CreateVector(mv1).Op(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Op(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this GaMultivector mv1, ITuple2D mv2)
        {
            return mv1.Op(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.Op(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this BasisBladeSet basisSet, ITuple2D mv1, ITuple2D mv2)
        {
            return basisSet.CreateVector(mv1).Op(basisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Op(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).Op(basisSet.CreateVector(mv2));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ELcp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).ELcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ELcp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.ELcp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ELcp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).ELcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ELcp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.ELcp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ELcp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).ELcp(basisSet.CreateVector(mv2));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Lcp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Lcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Lcp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.Lcp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Lcp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Lcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Lcp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.Lcp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Lcp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).Lcp(basisSet.CreateVector(mv2));
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ERcp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).ERcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ERcp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.ERcp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ERcp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).ERcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ERcp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.ERcp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector ERcp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).ERcp(basisSet.CreateVector(mv2));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Rcp(this ITuple3D mv1, GaTerm mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Rcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Rcp(this GaTerm mv1, ITuple3D mv2)
        {
            return mv1.Rcp(mv1.BasisSet.CreateVector(mv2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Rcp(this ITuple3D mv1, GaMultivector mv2)
        {
            return mv2.BasisSet.CreateVector(mv1).Rcp(mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Rcp(this GaMultivector mv1, ITuple3D mv2)
        {
            return mv1.Rcp(mv1.BasisSet.CreateVector(mv2));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector Rcp(this BasisBladeSet basisSet, ITuple3D mv1, ITuple3D mv2)
        {
            return basisSet.CreateVector(mv1).Rcp(basisSet.CreateVector(mv2));
        }
    }
}