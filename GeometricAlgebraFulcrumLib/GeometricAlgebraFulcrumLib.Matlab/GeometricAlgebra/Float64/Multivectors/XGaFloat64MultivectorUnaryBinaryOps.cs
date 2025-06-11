using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1)
        {
            return mv1.Negative();
        }


        
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator +(int mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator +(uint mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator +(long mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator +(ulong mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator +(float mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Add(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator +(double mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Add(
                mv2.Processor.Scalar(mv1)
            );
        }

        
        public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return mv1.Add(mv2);
        }


        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator -(int mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator -(uint mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator -(long mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator -(ulong mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator -(float mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Subtract(
                mv1.Processor.Scalar(mv2)
            );
        }

        
        public static XGaFloat64Multivector operator -(double mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Processor.Scalar(mv1).Subtract(mv2);
        }

        
        public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return mv1.Subtract(mv2);
        }


        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return mv1.Processor.ScalarZero;

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        
        public static XGaFloat64Multivector operator *(IntegerSign mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero)
                return mv2.Processor.ScalarZero;

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Times(mv2);
        }

        
        public static XGaFloat64Multivector operator *(int mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Times(mv2);
        }

        
        public static XGaFloat64Multivector operator *(uint mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Times(mv2);
        }

        
        public static XGaFloat64Multivector operator *(long mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Times(mv2);
        }

        
        public static XGaFloat64Multivector operator *(ulong mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Times(mv2);
        }

        
        public static XGaFloat64Multivector operator *(float mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        
        public static XGaFloat64Multivector operator *(double mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1);
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, XGaFloat64Scalar mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        
        public static XGaFloat64Multivector operator *(XGaFloat64Scalar mv1, XGaFloat64Multivector mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }


        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, int mv2)
        {
            return mv1.Divide(mv2);
        }

        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, uint mv2)
        {
            return mv1.Divide(mv2);
        }

        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, long mv2)
        {
            return mv1.Divide(mv2);
        }

        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, ulong mv2)
        {
            return mv1.Divide(mv2);
        }

        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, float mv2)
        {
            return mv1.Divide(mv2);
        }

        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, double mv2)
        {
            return mv1.Divide(mv2);
        }

        
        public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, XGaFloat64Scalar mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }


        public abstract XGaFloat64Multivector Add(XGaFloat64Multivector mv2);

        public abstract XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2);


        
        public XGaFloat64Multivector Times(double scalarValue)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Times(scalarValue),
                XGaFloat64Vector mv1 => mv1.Times(scalarValue),
                XGaFloat64Bivector mv1 => mv1.Times(scalarValue),
                XGaFloat64HigherKVector mv1 => mv1.Times(scalarValue),
                XGaFloat64GradedMultivector mv1 => mv1.Times(scalarValue),
                XGaFloat64UniformMultivector mv1 => mv1.Times(scalarValue),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Divide(double scalarValue)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Divide(scalarValue),
                XGaFloat64Vector mv1 => mv1.Divide(scalarValue),
                XGaFloat64Bivector mv1 => mv1.Divide(scalarValue),
                XGaFloat64HigherKVector mv1 => mv1.Divide(scalarValue),
                XGaFloat64GradedMultivector mv1 => mv1.Divide(scalarValue),
                XGaFloat64UniformMultivector mv1 => mv1.Divide(scalarValue),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Negative()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Negative(),
                XGaFloat64Vector mv1 => mv1.Negative(),
                XGaFloat64Bivector mv1 => mv1.Negative(),
                XGaFloat64HigherKVector mv1 => mv1.Negative(),
                XGaFloat64GradedMultivector mv1 => mv1.Negative(),
                XGaFloat64UniformMultivector mv1 => mv1.Negative(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Reverse()
        {
            return this switch
            {
                XGaFloat64Scalar => this,
                XGaFloat64Vector => this,
                XGaFloat64Bivector mv1 => mv1.Negative(),
                XGaFloat64HigherKVector mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
                XGaFloat64GradedMultivector mv1 => mv1.Reverse(),
                XGaFloat64UniformMultivector mv1 => mv1.Reverse(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector GradeInvolution()
        {
            return this switch
            {
                XGaFloat64Scalar => this,
                XGaFloat64Vector mv1 => mv1.Negative(),
                XGaFloat64Bivector mv1 => mv1,
                XGaFloat64HigherKVector mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
                XGaFloat64GradedMultivector mv1 => mv1.GradeInvolution(),
                XGaFloat64UniformMultivector mv1 => mv1.GradeInvolution(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector CliffordConjugate()
        {
            return this switch
            {
                XGaFloat64Scalar => this,
                XGaFloat64Vector mv1 => mv1.Negative(),
                XGaFloat64Bivector mv1 => mv1.Negative(),
                XGaFloat64HigherKVector mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
                XGaFloat64GradedMultivector mv1 => mv1.CliffordConjugate(),
                XGaFloat64UniformMultivector mv1 => mv1.CliffordConjugate(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Conjugate()
        {
            return this switch
            {
                XGaFloat64Scalar => this,
                XGaFloat64Vector mv1 => mv1.Conjugate(),
                XGaFloat64Bivector mv1 => mv1.Conjugate(),
                XGaFloat64HigherKVector mv1 => mv1.Conjugate(),
                XGaFloat64GradedMultivector mv1 => mv1.Conjugate(),
                XGaFloat64UniformMultivector mv1 => mv1.Conjugate(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector DivideByENorm()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.DivideByENorm(),
                XGaFloat64Vector mv1 => mv1.DivideByENorm(),
                XGaFloat64Bivector mv1 => mv1.DivideByENorm(),
                XGaFloat64HigherKVector mv1 => mv1.DivideByENorm(),
                XGaFloat64GradedMultivector mv1 => mv1.DivideByENorm(),
                XGaFloat64UniformMultivector mv1 => mv1.DivideByENorm(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector DivideByENormSquared()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.DivideByENormSquared(),
                XGaFloat64Vector mv1 => mv1.DivideByENormSquared(),
                XGaFloat64Bivector mv1 => mv1.DivideByENormSquared(),
                XGaFloat64HigherKVector mv1 => mv1.DivideByENormSquared(),
                XGaFloat64GradedMultivector mv1 => mv1.DivideByENormSquared(),
                XGaFloat64UniformMultivector mv1 => mv1.DivideByENormSquared(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector DivideByNorm()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.DivideByNorm(),
                XGaFloat64Vector mv1 => mv1.DivideByNorm(),
                XGaFloat64Bivector mv1 => mv1.DivideByNorm(),
                XGaFloat64HigherKVector mv1 => mv1.DivideByNorm(),
                XGaFloat64GradedMultivector mv1 => mv1.DivideByNorm(),
                XGaFloat64UniformMultivector mv1 => mv1.DivideByNorm(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector DivideByNormSquared()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.DivideByNormSquared(),
                XGaFloat64Vector mv1 => mv1.DivideByNormSquared(),
                XGaFloat64Bivector mv1 => mv1.DivideByNormSquared(),
                XGaFloat64HigherKVector mv1 => mv1.DivideByNormSquared(),
                XGaFloat64GradedMultivector mv1 => mv1.DivideByNormSquared(),
                XGaFloat64UniformMultivector mv1 => mv1.DivideByNormSquared(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector EInverse()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EInverse(),
                XGaFloat64Vector mv1 => mv1.EInverse(),
                XGaFloat64Bivector mv1 => mv1.EInverse(),
                XGaFloat64HigherKVector mv1 => mv1.EInverse(),
                XGaFloat64GradedMultivector mv1 => mv1.EInverse(),
                XGaFloat64UniformMultivector mv1 => mv1.EInverse(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Inverse()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Inverse(),
                XGaFloat64Vector mv1 => mv1.Inverse(),
                XGaFloat64Bivector mv1 => mv1.Inverse(),
                XGaFloat64HigherKVector mv1 => mv1.Inverse(),
                XGaFloat64GradedMultivector mv1 => mv1.Inverse(),
                XGaFloat64UniformMultivector mv1 => mv1.Inverse(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector PseudoInverse()
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.PseudoInverse(),
                XGaFloat64Vector mv1 => mv1.PseudoInverse(),
                XGaFloat64Bivector mv1 => mv1.PseudoInverse(),
                XGaFloat64HigherKVector mv1 => mv1.PseudoInverse(),
                XGaFloat64GradedMultivector mv1 => mv1.PseudoInverse(),
                XGaFloat64UniformMultivector mv1 => mv1.PseudoInverse(),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector EDual(int vSpaceDimensions)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EDual(vSpaceDimensions),
                XGaFloat64Vector mv1 => mv1.EDual(vSpaceDimensions),
                XGaFloat64Bivector mv1 => mv1.EDual(vSpaceDimensions),
                XGaFloat64HigherKVector mv1 => mv1.EDual(vSpaceDimensions),
                XGaFloat64GradedMultivector mv1 => mv1.EDual(vSpaceDimensions),
                XGaFloat64UniformMultivector mv1 => mv1.EDual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector EDual(XGaFloat64KVector blade)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EDual(blade),
                XGaFloat64Vector mv1 => mv1.EDual(blade),
                XGaFloat64Bivector mv1 => mv1.EDual(blade),
                XGaFloat64HigherKVector mv1 => mv1.EDual(blade),
                XGaFloat64GradedMultivector mv1 => mv1.EDual(blade),
                XGaFloat64UniformMultivector mv1 => mv1.EDual(blade),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Dual(int vSpaceDimensions)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Dual(vSpaceDimensions),
                XGaFloat64Vector mv1 => mv1.Dual(vSpaceDimensions),
                XGaFloat64Bivector mv1 => mv1.Dual(vSpaceDimensions),
                XGaFloat64HigherKVector mv1 => mv1.Dual(vSpaceDimensions),
                XGaFloat64GradedMultivector mv1 => mv1.Dual(vSpaceDimensions),
                XGaFloat64UniformMultivector mv1 => mv1.Dual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Dual(XGaFloat64KVector blade)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Dual(blade),
                XGaFloat64Vector mv1 => mv1.Dual(blade),
                XGaFloat64Bivector mv1 => mv1.Dual(blade),
                XGaFloat64HigherKVector mv1 => mv1.Dual(blade),
                XGaFloat64GradedMultivector mv1 => mv1.Dual(blade),
                XGaFloat64UniformMultivector mv1 => mv1.Dual(blade),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector EUnDual(int vSpaceDimensions)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EUnDual(vSpaceDimensions),
                XGaFloat64Vector mv1 => mv1.EUnDual(vSpaceDimensions),
                XGaFloat64Bivector mv1 => mv1.EUnDual(vSpaceDimensions),
                XGaFloat64HigherKVector mv1 => mv1.EUnDual(vSpaceDimensions),
                XGaFloat64GradedMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
                XGaFloat64UniformMultivector mv1 => mv1.EUnDual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector EUnDual(XGaFloat64KVector blade)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EUnDual(blade),
                XGaFloat64Vector mv1 => mv1.EUnDual(blade),
                XGaFloat64Bivector mv1 => mv1.EUnDual(blade),
                XGaFloat64HigherKVector mv1 => mv1.EUnDual(blade),
                XGaFloat64GradedMultivector mv1 => mv1.EUnDual(blade),
                XGaFloat64UniformMultivector mv1 => mv1.EUnDual(blade),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector UnDual(int vSpaceDimensions)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.UnDual(vSpaceDimensions),
                XGaFloat64Vector mv1 => mv1.UnDual(vSpaceDimensions),
                XGaFloat64Bivector mv1 => mv1.UnDual(vSpaceDimensions),
                XGaFloat64HigherKVector mv1 => mv1.UnDual(vSpaceDimensions),
                XGaFloat64GradedMultivector mv1 => mv1.UnDual(vSpaceDimensions),
                XGaFloat64UniformMultivector mv1 => mv1.UnDual(vSpaceDimensions),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector UnDual(XGaFloat64KVector blade)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.UnDual(blade),
                XGaFloat64Vector mv1 => mv1.UnDual(blade),
                XGaFloat64Bivector mv1 => mv1.UnDual(blade),
                XGaFloat64HigherKVector mv1 => mv1.UnDual(blade),
                XGaFloat64GradedMultivector mv1 => mv1.UnDual(blade),
                XGaFloat64UniformMultivector mv1 => mv1.UnDual(blade),
                _ => throw new InvalidOperationException()
            };
        }


        
        public virtual double ENormSquared()
        {
            if (IsZero)
                return 0d;

            var scalarList =
                Scalars.Select(s => s * s);

            return scalarList.Sum();
        }

        
        public virtual double NormSquared()
        {
            if (IsZero)
                return 0d;

            var scalarList =
                IdScalarPairs.Select(p => 
                    Metric.Signature(p.Key) * p.Value * p.Value
                );

            return scalarList.Sum();
        }
        
        
        public virtual double ENorm()
        {
            return ENormSquared().Sqrt();
        }

        
        public virtual double Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

    }
}
