using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Combinations;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using TextComposerLib.Text;

namespace NumericalGeometryLib.GeometricAlgebra.EuclideanND
{
    public class EgaKVector :
        IGeometricElement,
        IReadOnlyList<double>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector CreateScalar(int dimensions, double scalar = 0d)
        {
            var kVector = new EgaKVector(dimensions, 0)
            {
                [0] = scalar
            };

            return kVector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector CreateVector(int dimensions)
        {
            return new EgaKVector(dimensions, 1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector CreateVector(params double[] scalarArray)
        {
            return new EgaKVector(scalarArray.Length, 1, scalarArray);
        }

        public static EgaKVector CreateVector(IReadOnlyList<double> scalarList)
        {
            var kVector = new EgaKVector(scalarList.Count, 1);

            for (var i = 0; i < kVector.Count; i++)
                kVector[i] = scalarList[i];

            return kVector;
        }
        
        public static EgaKVector CreateBivector(int dimensions, IReadOnlyList<double> scalarList)
        {
            var kVector = new EgaKVector(dimensions, 2);

            for (var i = 0; i < kVector.Count; i++)
                kVector[i] = scalarList[i];

            return kVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector CreateBivector(int dimensions)
        {
            return new EgaKVector(dimensions, 2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector CreateKVector(int dimensions, int grade)
        {
            return new EgaKVector(dimensions, grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector operator -(EgaKVector kVector)
        {
            var scalarArray = kVector.ScalarArray.Select(s => -s).ToArray();

            return new EgaKVector(
                kVector.Dimensions,
                kVector.Grade,
                scalarArray
            );
        }
        
        public static EgaKVector operator +(EgaKVector kVector1, EgaKVector kVector2)
        {
            if (kVector1.Dimensions != kVector2.Dimensions || kVector1.Grade != kVector2.Grade)
                throw new InvalidOperationException();

            var scalarArray = new double[kVector1.Count];

            for (var i = 0; i < scalarArray.Length; i++)
                scalarArray[i] = kVector1.ScalarArray[i] + kVector2.ScalarArray[i];

            return new EgaKVector(
                kVector1.Dimensions,
                kVector1.Grade,
                scalarArray
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector operator +(EgaKVector kVector1, double kVector2)
        {
            if (kVector1.Grade != 0)
                throw new InvalidOperationException();

            var scalarArray = new double[1];

            scalarArray[0] = kVector1.ScalarArray[0] + kVector2;

            return new EgaKVector(
                kVector1.Dimensions,
                kVector1.Grade,
                scalarArray
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector operator +(double kVector1, EgaKVector kVector2)
        {
            if (kVector2.Grade != 0)
                throw new InvalidOperationException();

            var scalarArray = new double[1];

            scalarArray[0] = kVector1 + kVector2.ScalarArray[0];

            return new EgaKVector(
                kVector2.Dimensions,
                kVector2.Grade,
                scalarArray
            );
        }

        public static EgaKVector operator -(EgaKVector kVector1, EgaKVector kVector2)
        {
            if (kVector1.Dimensions != kVector2.Dimensions || kVector1.Grade != kVector2.Grade)
                throw new InvalidOperationException();

            var scalarArray = new double[kVector1.Count];

            for (var i = 0; i < scalarArray.Length; i++)
                scalarArray[i] = kVector1.ScalarArray[i] - kVector2.ScalarArray[i];

            return new EgaKVector(
                kVector1.Dimensions,
                kVector1.Grade,
                scalarArray
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector operator -(EgaKVector kVector1, double kVector2)
        {
            if (kVector1.Grade != 0)
                throw new InvalidOperationException();

            var scalarArray = new double[1];

            scalarArray[0] = kVector1.ScalarArray[0] - kVector2;

            return new EgaKVector(
                kVector1.Dimensions,
                kVector1.Grade,
                scalarArray
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EgaKVector operator -(double kVector1, EgaKVector kVector2)
        {
            if (kVector2.Grade != 0)
                throw new InvalidOperationException();

            var scalarArray = new double[1];

            scalarArray[0] = kVector1 - kVector2.ScalarArray[0];

            return new EgaKVector(
                kVector2.Dimensions,
                kVector2.Grade,
                scalarArray
            );
        }

        public static EgaKVector operator *(EgaKVector kVector1, double kVector2)
        {
            Debug.Assert(kVector2.IsValid());

            var scalarArray = new double[kVector1.Count];

            for (var i = 0; i < scalarArray.Length; i++)
                scalarArray[i] = kVector1.ScalarArray[i] * kVector2;

            return new EgaKVector(
                kVector1.Dimensions,
                kVector1.Grade,
                scalarArray
            );
        }
        
        public static EgaKVector operator *(double kVector1, EgaKVector kVector2)
        {
            Debug.Assert(kVector1.IsValid());

            var scalarArray = new double[kVector2.Count];

            for (var i = 0; i < scalarArray.Length; i++)
                scalarArray[i] = kVector1 * kVector2.ScalarArray[i];

            return new EgaKVector(
                kVector2.Dimensions,
                kVector2.Grade,
                scalarArray
            );
        }
        
        public static EgaKVector operator /(EgaKVector kVector1, double kVector2)
        {
            Debug.Assert(kVector2.IsValid());

            kVector2 = 1d / kVector2;

            var scalarArray = new double[kVector1.Count];

            for (var i = 0; i < scalarArray.Length; i++)
                scalarArray[i] = kVector1.ScalarArray[i] * kVector2;

            return new EgaKVector(
                kVector1.Dimensions,
                kVector1.Grade,
                scalarArray
            );
        }


        public int Dimensions { get; }

        public int Grade { get; }

        public double[] ScalarArray { get; private set; }
        
        public int Count 
            => ScalarArray.Length;

        public double this[int index]
        {
            get => ScalarArray[index];
            set
            {
                Debug.Assert(value.IsNotNaN());

                ScalarArray[index] = value;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EgaKVector(int dimensions, int grade)
        {
            if (dimensions is < 2 or > 63)
                throw new ArgumentOutOfRangeException(nameof(dimensions));

            if (grade < 0 || grade > dimensions)
                throw new ArgumentOutOfRangeException(nameof(grade));

            var scalarArraySize = dimensions.GetBinomialCoefficient(grade);

            Dimensions = dimensions;
            Grade = grade;
            ScalarArray = new double[scalarArraySize];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EgaKVector(int dimensions, int grade, double[] scalarArray)
        {
            if (dimensions is < 2 or > 63)
                throw new ArgumentOutOfRangeException(nameof(dimensions));

            if (grade < 0 || grade > dimensions)
                throw new ArgumentOutOfRangeException(nameof(grade));

            var scalarArraySize = dimensions.GetBinomialCoefficient(grade);

            if ((ulong) scalarArray.Length != scalarArraySize)
                throw new ArgumentException(nameof(scalarArray));

            Dimensions = dimensions;
            Grade = grade;
            ScalarArray = scalarArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ScalarArray.All(s => s.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EgaKVector Clear()
        {
            ScalarArray = new double[ScalarArray.Length];

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EgaKVector SetScalarArray(double[] scalarArray)
        {
            if (scalarArray.Length != ScalarArray.Length)
                throw new ArgumentException();

            Debug.Assert(
                scalarArray.All(s => s.IsValid())
            );

            ScalarArray = scalarArray;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple ToTuple()
        {
            return ScalarArray.CreateTuple();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NormSquared()
        {
            return ScalarArray.Sum(s => s * s);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Norm()
        {
            return ScalarArray.Sum(s => s * s).Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EgaKVector Reverse()
        {
            return BasisBladeUtils.ReverseIsPositiveOfGrade((uint) Grade) 
                ? this : -this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EgaKVector GradeInvolution()
        {
            return BasisBladeUtils.GradeInvolutionIsPositiveOfGrade((uint) Grade) 
                ? this : -this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EgaKVector CliffordConjugate()
        {
            return BasisBladeUtils.CliffordConjugateIsPositiveOfGrade((uint) Grade)
                ? this : -this;
        }
        
        public double Sp(EgaKVector kVector2)
        {
            if (Dimensions != kVector2.Dimensions)
                throw new InvalidOperationException();

            if (Grade != kVector2.Grade)
                return 0d;

            var grade1 = (uint) Grade;
            var spScalar = 0d;
            for (var index1 = 0UL; index1 < (ulong) ScalarArray.Length; index1++)
            {
                var id1 = BasisBladeUtils.BasisBladeGradeIndexToId(grade1, index1);
                var scalar1 = ScalarArray[index1];
                var scalar2 = kVector2.ScalarArray[index1];
                var scalar = scalar1 * scalar2;

                if (BasisBladeProductUtils.EGpSign(id1, id1) < 0)
                    scalar = -scalar;

                spScalar += scalar;
            }

            return spScalar;
        }

        public EgaKVector Op(EgaKVector kVector2)
        {
            if (Dimensions != kVector2.Dimensions)
                throw new InvalidOperationException();

            var grade = Grade + kVector2.Grade;

            if (grade > Dimensions)
                return CreateScalar(Dimensions, 0d);

            if (Grade == 0)
                return ScalarArray[0] * kVector2;

            if (kVector2.Grade == 0)
                return this * kVector2.ScalarArray[0];

            var scalarArraySize = Dimensions.GetBinomialCoefficient(grade);
            var scalarArray = new double[scalarArraySize];

            var grade1 = (uint) Grade;
            var grade2 = (uint) kVector2.Grade;

            for (var index1 = 0UL; index1 < (ulong) ScalarArray.Length; index1++)
            {
                var id1 = BasisBladeUtils.BasisBladeGradeIndexToId(grade1, index1);
                var scalar1 = ScalarArray[index1];

                for (var index2 = 0UL; index2 < (ulong) kVector2.ScalarArray.Length; index2++)
                {
                    var id2 = BasisBladeUtils.BasisBladeGradeIndexToId(grade2, index2);
                    
                    if (!BasisBladeProductUtils.IsNonZeroOp(id1, id2))
                        continue;

                    var scalar2 = kVector2.ScalarArray[index2];

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalar1 * scalar2;

                    if (BasisBladeProductUtils.EGpSign(id1, id2) < 0)
                        scalar = -scalar;

                    scalarArray[index] += scalar;
                }
            }

            return new EgaKVector(
                Dimensions,
                grade,
                scalarArray
            );
        }

        public EgaKVector Lcp(EgaKVector kVector2)
        {
            if (Dimensions != kVector2.Dimensions)
                throw new InvalidOperationException();

            var grade = kVector2.Grade - Grade;

            if (grade < 0)
                return CreateScalar(Dimensions, 0d);

            if (Grade == 0)
                return ScalarArray[0] * kVector2;
            
            var scalarArraySize = Dimensions.GetBinomialCoefficient(grade);
            var scalarArray = new double[scalarArraySize];

            var grade1 = (uint) Grade;
            var grade2 = (uint) kVector2.Grade;

            for (var index1 = 0UL; index1 < (ulong) ScalarArray.Length; index1++)
            {
                var id1 = BasisBladeUtils.BasisBladeGradeIndexToId(grade1, index1);
                var scalar1 = ScalarArray[index1];

                for (var index2 = 0UL; index2 < (ulong) kVector2.ScalarArray.Length; index2++)
                {
                    var id2 = BasisBladeUtils.BasisBladeGradeIndexToId(grade2, index2);
                    
                    if (!BasisBladeProductUtils.IsNonZeroELcp(id1, id2))
                        continue;

                    var scalar2 = kVector2.ScalarArray[index2];

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalar1 * scalar2;

                    if (BasisBladeProductUtils.EGpSign(id1, id2) < 0)
                        scalar = -scalar;

                    scalarArray[index] += scalar;
                }
            }

            return new EgaKVector(
                Dimensions,
                grade,
                scalarArray
            );
        }
        
        public EgaKVector Rcp(EgaKVector kVector2)
        {
            if (Dimensions != kVector2.Dimensions)
                throw new InvalidOperationException();

            var grade = Grade - kVector2.Grade;

            if (grade < 0)
                return CreateScalar(Dimensions, 0d);

            if (Grade == 0)
                return ScalarArray[0] * kVector2;
            
            var scalarArraySize = Dimensions.GetBinomialCoefficient(grade);
            var scalarArray = new double[scalarArraySize];

            var grade1 = (uint) Grade;
            var grade2 = (uint) kVector2.Grade;

            for (var index1 = 0UL; index1 < (ulong) kVector2.ScalarArray.Length; index1++)
            {
                var id1 = BasisBladeUtils.BasisBladeGradeIndexToId(grade1, index1);
                var scalar1 = ScalarArray[index1];

                for (var index2 = 0UL; index2 < (ulong) ScalarArray.Length; index2++)
                {
                    var id2 = BasisBladeUtils.BasisBladeGradeIndexToId(grade2, index2);
                    
                    if (!BasisBladeProductUtils.IsNonZeroERcp(id1, id2))
                        continue;

                    var scalar2 = kVector2.ScalarArray[index2];

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalar1 * scalar2;

                    if (BasisBladeProductUtils.EGpSign(id1, id2) < 0)
                        scalar = -scalar;

                    scalarArray[index] += scalar;
                }
            }

            return new EgaKVector(
                Dimensions,
                grade,
                scalarArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>) ScalarArray).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return ScalarArray
                .Select(d => d.ToString("F6"))
                .Concatenate(", ", "(", ")");
        }
    }
}
