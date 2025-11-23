using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.Optimization;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1)
        {
            return mv1.Negative();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(int mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(uint mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(long mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(ulong mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(float mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(double mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return mv1.Add(mv2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(int mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(uint mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(long mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(ulong mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(float mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(double mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return mv1.Subtract(mv2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.Processor.ScalarZero;

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(IntegerSign mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero)
                return mv2.Processor.ScalarZero;

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(int mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(uint mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(long mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(ulong mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(float mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(double mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, XGaFloat64Scalar mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator *(XGaFloat64Scalar mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, XGaFloat64Scalar mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }


        public abstract XGaFloat64Multivector Add(XGaFloat64Multivector mv2);

        public abstract XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Times(double scalarValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Divide(double scalarValue);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.Divide(scalarValue),
        //        XGaFloat64Vector mv1 => mv1.Divide(scalarValue),
        //        XGaFloat64Bivector mv1 => mv1.Divide(scalarValue),
        //        XGaFloat64HigherKVector mv1 => mv1.Divide(scalarValue),
        //        XGaFloat64GradedMultivector mv1 => mv1.Divide(scalarValue),
        //        XGaFloat64UniformMultivector mv1 => mv1.Divide(scalarValue),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Negative();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.Negative(),
        //        XGaFloat64Vector mv1 => mv1.Negative(),
        //        XGaFloat64Bivector mv1 => mv1.Negative(),
        //        XGaFloat64HigherKVector mv1 => mv1.Negative(),
        //        XGaFloat64GradedMultivector mv1 => mv1.Negative(),
        //        XGaFloat64UniformMultivector mv1 => mv1.Negative(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Reverse();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar => this,
        //        XGaFloat64Vector => this,
        //        XGaFloat64Bivector mv1 => mv1.Negative(),
        //        XGaFloat64HigherKVector mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
        //        XGaFloat64GradedMultivector mv1 => mv1.Reverse(),
        //        XGaFloat64UniformMultivector mv1 => mv1.Reverse(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector GradeInvolution();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar => this,
        //        XGaFloat64Vector mv1 => mv1.Negative(),
        //        XGaFloat64Bivector mv1 => mv1,
        //        XGaFloat64HigherKVector mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
        //        XGaFloat64GradedMultivector mv1 => mv1.GradeInvolution(),
        //        XGaFloat64UniformMultivector mv1 => mv1.GradeInvolution(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}
        
        /// <summary>
        /// The Hitzer–Sangwine grade involution only negates the selected grades of the multivector
        /// See: Multivector and multivector matrix inverses in real Clifford algebras
        /// </summary>
        /// <param name="gradeSet"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector GradeInvolution(IndexSet gradeSet);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector CliffordConjugate();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar => this,
        //        XGaFloat64Vector mv1 => mv1.Negative(),
        //        XGaFloat64Bivector mv1 => mv1.Negative(),
        //        XGaFloat64HigherKVector mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
        //        XGaFloat64GradedMultivector mv1 => mv1.CliffordConjugate(),
        //        XGaFloat64UniformMultivector mv1 => mv1.CliffordConjugate(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Conjugate();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar => this,
        //        XGaFloat64Vector mv1 => mv1.Conjugate(),
        //        XGaFloat64Bivector mv1 => mv1.Conjugate(),
        //        XGaFloat64HigherKVector mv1 => mv1.Conjugate(),
        //        XGaFloat64GradedMultivector mv1 => mv1.Conjugate(),
        //        XGaFloat64UniformMultivector mv1 => mv1.Conjugate(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector DivideByENorm();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.DivideByENorm(),
        //        XGaFloat64Vector mv1 => mv1.DivideByENorm(),
        //        XGaFloat64Bivector mv1 => mv1.DivideByENorm(),
        //        XGaFloat64HigherKVector mv1 => mv1.DivideByENorm(),
        //        XGaFloat64GradedMultivector mv1 => mv1.DivideByENorm(),
        //        XGaFloat64UniformMultivector mv1 => mv1.DivideByENorm(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector DivideByENormSquared();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.DivideByENormSquared(),
        //        XGaFloat64Vector mv1 => mv1.DivideByENormSquared(),
        //        XGaFloat64Bivector mv1 => mv1.DivideByENormSquared(),
        //        XGaFloat64HigherKVector mv1 => mv1.DivideByENormSquared(),
        //        XGaFloat64GradedMultivector mv1 => mv1.DivideByENormSquared(),
        //        XGaFloat64UniformMultivector mv1 => mv1.DivideByENormSquared(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector DivideByNorm();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.DivideByNorm(),
        //        XGaFloat64Vector mv1 => mv1.DivideByNorm(),
        //        XGaFloat64Bivector mv1 => mv1.DivideByNorm(),
        //        XGaFloat64HigherKVector mv1 => mv1.DivideByNorm(),
        //        XGaFloat64GradedMultivector mv1 => mv1.DivideByNorm(),
        //        XGaFloat64UniformMultivector mv1 => mv1.DivideByNorm(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector DivideByNormSquared();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.DivideByNormSquared(),
        //        XGaFloat64Vector mv1 => mv1.DivideByNormSquared(),
        //        XGaFloat64Bivector mv1 => mv1.DivideByNormSquared(),
        //        XGaFloat64HigherKVector mv1 => mv1.DivideByNormSquared(),
        //        XGaFloat64GradedMultivector mv1 => mv1.DivideByNormSquared(),
        //        XGaFloat64UniformMultivector mv1 => mv1.DivideByNormSquared(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        /// <summary>
        /// Inverse of this multivector assuming it's a Euclidean versor
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector EVersorInverse();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.EInverse(),
        //        XGaFloat64Vector mv1 => mv1.EInverse(),
        //        XGaFloat64Bivector mv1 => mv1.EInverse(),
        //        XGaFloat64HigherKVector mv1 => mv1.EInverse(),
        //        XGaFloat64GradedMultivector mv1 => mv1.EInverse(),
        //        XGaFloat64UniformMultivector mv1 => mv1.EInverse(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        /// <summary>
        /// Inverse of this multivector assuming it's a versor
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector VersorInverse();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.Inverse(),
        //        XGaFloat64Vector mv1 => mv1.Inverse(),
        //        XGaFloat64Bivector mv1 => mv1.Inverse(),
        //        XGaFloat64HigherKVector mv1 => mv1.Inverse(),
        //        XGaFloat64GradedMultivector mv1 => mv1.Inverse(),
        //        XGaFloat64UniformMultivector mv1 => mv1.Inverse(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        /// <summary>
        /// Pseudo-inverse of this multivector assuming it's a versor
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector VersorPseudoInverse();
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.PseudoInverse(),
        //        XGaFloat64Vector mv1 => mv1.PseudoInverse(),
        //        XGaFloat64Bivector mv1 => mv1.PseudoInverse(),
        //        XGaFloat64HigherKVector mv1 => mv1.PseudoInverse(),
        //        XGaFloat64GradedMultivector mv1 => mv1.PseudoInverse(),
        //        XGaFloat64UniformMultivector mv1 => mv1.PseudoInverse(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector EDual(int vSpaceDimensions);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.EDual(vSpaceDimensions),
        //        XGaFloat64Vector mv1 => mv1.EDual(vSpaceDimensions),
        //        XGaFloat64Bivector mv1 => mv1.EDual(vSpaceDimensions),
        //        XGaFloat64HigherKVector mv1 => mv1.EDual(vSpaceDimensions),
        //        XGaFloat64GradedMultivector mv1 => mv1.EDual(vSpaceDimensions),
        //        XGaFloat64UniformMultivector mv1 => mv1.EDual(vSpaceDimensions),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector EDual(XGaFloat64KVector blade);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.EDual(blade),
        //        XGaFloat64Vector mv1 => mv1.EDual(blade),
        //        XGaFloat64Bivector mv1 => mv1.EDual(blade),
        //        XGaFloat64HigherKVector mv1 => mv1.EDual(blade),
        //        XGaFloat64GradedMultivector mv1 => mv1.EDual(blade),
        //        XGaFloat64UniformMultivector mv1 => mv1.EDual(blade),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Dual(int vSpaceDimensions);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.Dual(vSpaceDimensions),
        //        XGaFloat64Vector mv1 => mv1.Dual(vSpaceDimensions),
        //        XGaFloat64Bivector mv1 => mv1.Dual(vSpaceDimensions),
        //        XGaFloat64HigherKVector mv1 => mv1.Dual(vSpaceDimensions),
        //        XGaFloat64GradedMultivector mv1 => mv1.Dual(vSpaceDimensions),
        //        XGaFloat64UniformMultivector mv1 => mv1.Dual(vSpaceDimensions),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Dual(XGaFloat64KVector blade);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.Dual(blade),
        //        XGaFloat64Vector mv1 => mv1.Dual(blade),
        //        XGaFloat64Bivector mv1 => mv1.Dual(blade),
        //        XGaFloat64HigherKVector mv1 => mv1.Dual(blade),
        //        XGaFloat64GradedMultivector mv1 => mv1.Dual(blade),
        //        XGaFloat64UniformMultivector mv1 => mv1.Dual(blade),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector EUnDual(int vSpaceDimensions);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.EUnDual(vSpaceDimensions),
        //        XGaFloat64Vector mv1 => mv1.EUnDual(vSpaceDimensions),
        //        XGaFloat64Bivector mv1 => mv1.EUnDual(vSpaceDimensions),
        //        XGaFloat64HigherKVector mv1 => mv1.EUnDual(vSpaceDimensions),
        //        XGaFloat64GradedMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
        //        XGaFloat64UniformMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector EUnDual(XGaFloat64KVector blade);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.EUnDual(blade),
        //        XGaFloat64Vector mv1 => mv1.EUnDual(blade),
        //        XGaFloat64Bivector mv1 => mv1.EUnDual(blade),
        //        XGaFloat64HigherKVector mv1 => mv1.EUnDual(blade),
        //        XGaFloat64GradedMultivector mv1 => mv1.EUnDual(blade),
        //        XGaFloat64UniformMultivector mv1 => mv1.EUnDual(blade),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector UnDual(int vSpaceDimensions);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.UnDual(vSpaceDimensions),
        //        XGaFloat64Vector mv1 => mv1.UnDual(vSpaceDimensions),
        //        XGaFloat64Bivector mv1 => mv1.UnDual(vSpaceDimensions),
        //        XGaFloat64HigherKVector mv1 => mv1.UnDual(vSpaceDimensions),
        //        XGaFloat64GradedMultivector mv1 => mv1.UnDual(vSpaceDimensions),
        //        XGaFloat64UniformMultivector mv1 => mv1.UnDual(vSpaceDimensions),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector UnDual(XGaFloat64KVector blade);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar mv1 => mv1.UnDual(blade),
        //        XGaFloat64Vector mv1 => mv1.UnDual(blade),
        //        XGaFloat64Bivector mv1 => mv1.UnDual(blade),
        //        XGaFloat64HigherKVector mv1 => mv1.UnDual(blade),
        //        XGaFloat64GradedMultivector mv1 => mv1.UnDual(blade),
        //        XGaFloat64UniformMultivector mv1 => mv1.UnDual(blade),
        //        _ => throw new InvalidOperationException()
        //    };
        //}


        public XGaFloat64Multivector IntPower(int n)
        {
            if (n < 0) 
                throw new ArgumentOutOfRangeException(nameof(n));

            if (n == 0) 
                return Processor.ScalarOne;

            var mv = this;
            while (n > 1)
            {
                mv = Gp(mv);
                n--;
            }

            return mv;
        }

        public XGaFloat64Multivector ExpByTaylor(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (IsNearZero(tolerance))
                return Processor.ScalarOne;

            var termScalar = 1d;
            XGaFloat64Multivector termPower = Processor.ScalarOne;
            XGaFloat64Multivector termSum = Processor.ScalarOne;

            var n = 1;
            while (n <= 100)
            {
                termScalar /= n;
                termPower = Gp(termPower);

                var term = termPower.Times(termScalar);

                termSum += term;

                if (termPower.ENorm() > tolerance && term.ENorm() < tolerance) 
                    return termSum.RemoveSmallTerms(tolerance);

                n++;
            }

            throw new MaximumIterationsException("Value did not converge");
        }
        
        public XGaFloat64Multivector SinByTaylor(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (IsNearZero(tolerance))
                return Processor.ScalarZero;

            var x2 = Gp(this);
            var termScalar = 1d;
            var termPower = this;
            var termSum = this;

            var n = 1;
            while (n <= 100)
            {
                termScalar /= -(2 * n) * (2 * n + 1);
                termPower = x2.Gp(termPower);

                var term = termPower.Times(termScalar);

                termSum += term;

                if (termPower.ENorm() > tolerance && term.ENorm() < tolerance) 
                    return termSum.RemoveSmallTerms(tolerance);

                n++;
            }

            throw new MaximumIterationsException("Value did not converge");
        }
        
        public XGaFloat64Multivector CosByTaylor(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (IsNearZero(tolerance))
                return Processor.ScalarZero;

            var x2 = Gp(this);
            var termScalar = 1d;
            XGaFloat64Multivector termPower = Processor.ScalarOne;
            XGaFloat64Multivector termSum = Processor.ScalarOne;

            var n = 1;
            while (n <= 100)
            {
                termScalar /= -(2 * n) * (2 * n + 1);
                termPower = x2.Gp(termPower);

                var term = termPower.Times(termScalar);

                termSum += term;

                if (termPower.ENorm() > tolerance && term.ENorm() < tolerance) 
                    return termSum.RemoveSmallTerms(tolerance);

                n++;
            }

            throw new MaximumIterationsException("Value did not converge");
        }
        
        public XGaFloat64Multivector SinhByTaylor(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (IsNearZero(tolerance))
                return Processor.ScalarZero;

            var x2 = Gp(this);
            var termScalar = 1d;
            var termPower = this;
            var termSum = this;

            var n = 1;
            while (n <= 100)
            {
                termScalar /= (2 * n) * (2 * n - 1);
                termPower = x2.Gp(termPower);

                var term = termPower.Times(termScalar);

                termSum += term;

                if (termPower.ENorm() > tolerance && term.ENorm() < tolerance) 
                    return termSum.RemoveSmallTerms(tolerance);

                n++;
            }

            throw new MaximumIterationsException("Value did not converge");
        }

        public XGaFloat64Multivector CoshByTaylor(double tolerance = Float64Utils.DefaultTolerance)
        {
            if (IsNearZero(tolerance))
                return Processor.ScalarZero;

            var x2 = Gp(this);
            var termScalar = 1d;
            XGaFloat64Multivector termPower = Processor.ScalarOne;
            XGaFloat64Multivector termSum = Processor.ScalarOne;

            var n = 1;
            while (n <= 100)
            {
                termScalar /= (2 * n) * (2 * n - 1);
                termPower = x2.Gp(termPower);

                var term = termPower.Times(termScalar);

                termSum += term;

                if (termPower.ENorm() > tolerance && term.ENorm() < tolerance) 
                    return termSum.RemoveSmallTerms(tolerance);

                n++;
            }

            throw new MaximumIterationsException("Value did not converge");
        }

        public XGaFloat64Multivector InverseByMatrix(double tolerance = Float64Utils.DefaultTolerance)
        {
            // TODO: For sparse multivectors try to compactify the basis vectors by ignoring the ones not used in this multivector to reduce dimensions of matrix
            return Processor.Multivector(
                LeftGpMapMatrix(VSpaceDimensions).FirstColumnOfInverse().ToArray()
            ).RemoveSmallTerms(tolerance);
        }

        public XGaFloat64Multivector InverseByNeumann(double tolerance = Float64Utils.DefaultTolerance)
        {
            var a = Scalar();

            if (a.IsNearZero(tolerance))
                throw new InvalidOperationException();

            var mvB = 1 - Divide(a);

            if (mvB.Norm() >= 1)
                throw new InvalidOperationException();

            XGaFloat64Multivector termPower = Processor.ScalarOne;
            XGaFloat64Multivector termSum = Processor.ScalarOne;

            var n = 1;
            while (n <= 100)
            {
                termPower = mvB.Gp(termPower);

                termSum += termPower;

                if (termPower.ENorm() < tolerance)
                    return termSum.Divide(a).RemoveSmallTerms(tolerance);

                n++;
            }

            throw new MaximumIterationsException("Value did not converge");
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double ENormSquared()
        {
            if (IsZero)
                return Float64Scalar.Zero;

            var scalarList =
                Scalars.Select(s => s * s);

            return scalarList.Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double NormSquared()
        {
            if (IsZero)
                return Float64Scalar.Zero;

            var scalarList =
                IdScalarPairs.Select(p => 
                    Metric.Signature(p.Key) * p.Value * p.Value
                );

            return scalarList.Sum();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double ENorm()
        {
            return ENormSquared().Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual double Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

    }
}
