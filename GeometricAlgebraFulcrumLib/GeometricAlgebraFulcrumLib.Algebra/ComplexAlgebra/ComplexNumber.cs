using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra
{
    public sealed class ComplexNumber<T> : 
        ILinVector2D<T>,
        IReadOnlyList<Scalar<T>>
    {
        public static ComplexNumber<T> Zero(IScalarProcessor<T> scalarProcessor)
        {
            return new ComplexNumber<T>(
                scalarProcessor.Zero, 
                scalarProcessor.Zero
            );
        }

        public static ComplexNumber<T> One(IScalarProcessor<T> scalarProcessor)
        {
            return new ComplexNumber<T>(
                scalarProcessor, 
                scalarProcessor.OneValue, 
                scalarProcessor.ZeroValue
            );
        }


        public static ComplexNumber<T> operator +(ComplexNumber<T> c)
        {
            return c;
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c)
        {
            return new ComplexNumber<T>(
                c.Real.Negative(),
                c.Imaginary.Negative()
            );
        }
        

        public static ComplexNumber<T> operator +(int c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator +(uint c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(long c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator +(ulong c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(float c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator +(double c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(T c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator +(Scalar<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.ScalarValue, c2.Real.ScalarValue),
                c2.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, int c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, uint c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, long c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, ulong c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, float c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, double c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, T c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, Scalar<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2.ScalarValue),
                c1.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator +(ComplexNumber<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Add(c1.Real.ScalarValue, c2.Real.ScalarValue),
                scalarProcessor.Add(c1.Imaginary.ScalarValue, c2.Imaginary.ScalarValue)
            );
        }
        

        public static ComplexNumber<T> operator -(int c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }
        
        public static ComplexNumber<T> operator -(uint c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator -(long c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }
        
        public static ComplexNumber<T> operator -(ulong c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator -(float c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }
        
        public static ComplexNumber<T> operator -(double c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator -(T c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }
        
        public static ComplexNumber<T> operator -(Scalar<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.ScalarValue, c2.Real.ScalarValue),
                scalarProcessor.Negative(c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, int c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, uint c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, long c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, ulong c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, float c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, double c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }

        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, T c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2),
                c1.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, Scalar<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2.ScalarValue),
                c1.Imaginary
            );
        }
        
        public static ComplexNumber<T> operator -(ComplexNumber<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Subtract(c1.Real.ScalarValue, c2.Real.ScalarValue),
                scalarProcessor.Subtract(c1.Imaginary.ScalarValue, c2.Imaginary.ScalarValue)
            );
        }
        
        
        public static ComplexNumber<T> operator *(int c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.Times(c1, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator *(uint c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.Times(c1, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator *(long c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.Times(c1, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator *(ulong c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.Times(c1, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator *(float c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.Times(c1, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator *(double c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.Times(c1, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator *(T c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.Times(c1, c2.Imaginary.ScalarValue)
            );
        }
        
        public static ComplexNumber<T> operator *(Scalar<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.ScalarValue, c2.Real.ScalarValue),
                scalarProcessor.Times(c1.ScalarValue, c2.Imaginary.ScalarValue)
            );
        }
        
        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, int c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, uint c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, long c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, ulong c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, float c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, double c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, T c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, Scalar<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2.ScalarValue),
                scalarProcessor.Times(c1.Imaginary.ScalarValue, c2.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator *(ComplexNumber<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.Real.ScalarValue, c2.Real.ScalarValue) -
                    scalarProcessor.Times(c1.Imaginary.ScalarValue, c2.Imaginary.ScalarValue),
                scalarProcessor.Times(c1.Real.ScalarValue, c2.Imaginary.ScalarValue) +
                    scalarProcessor.Times(c1.Imaginary.ScalarValue, c2.Real.ScalarValue)
            );
        }
        
        
        public static ComplexNumber<T> operator /(int c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            var s = scalarProcessor.Divide(
                c1, 
                c2.MagnitudeSquaredValue
            ).ScalarValue;

            return new ComplexNumber<T>(
                scalarProcessor.Times(s, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(s, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(uint c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            var s = scalarProcessor.Divide(
                c1, 
                c2.MagnitudeSquaredValue
            ).ScalarValue;

            return new ComplexNumber<T>(
                scalarProcessor.Times(s, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(s, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(long c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            var s = scalarProcessor.Divide(
                c1, 
                c2.MagnitudeSquaredValue
            ).ScalarValue;

            return new ComplexNumber<T>(
                scalarProcessor.Times(s, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(s, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(ulong c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            var s = scalarProcessor.Divide(
                c1, 
                c2.MagnitudeSquaredValue
            ).ScalarValue;

            return new ComplexNumber<T>(
                scalarProcessor.Times(s, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(s, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(float c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            var s = scalarProcessor.Divide(
                c1, 
                c2.MagnitudeSquaredValue
            ).ScalarValue;

            return new ComplexNumber<T>(
                scalarProcessor.Times(s, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(s, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(double c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            var s = scalarProcessor.Divide(
                c1, 
                c2.MagnitudeSquaredValue
            ).ScalarValue;

            return new ComplexNumber<T>(
                scalarProcessor.Times(s, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(s, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(T c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(c1, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(Scalar<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c2.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Times(c1.ScalarValue, c2.Real.ScalarValue),
                scalarProcessor.NegativeTimes(c1.ScalarValue, c2.Imaginary.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, int c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, uint c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, long c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, ulong c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, float c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, double c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, T c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, Scalar<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            return new ComplexNumber<T>(
                scalarProcessor.Divide(c1.Real.ScalarValue, c2.ScalarValue),
                scalarProcessor.Divide(c1.Imaginary.ScalarValue, c2.ScalarValue)
            );
        }

        public static ComplexNumber<T> operator /(ComplexNumber<T> c1, ComplexNumber<T> c2)
        {
            var scalarProcessor = c1.ScalarProcessor;

            var r2 = c2.MagnitudeSquared;

            return new ComplexNumber<T>(
                (
                    scalarProcessor.Times(c1.Real.ScalarValue, c2.Real.ScalarValue) +
                        scalarProcessor.Times(c1.Imaginary.ScalarValue, c2.Imaginary.ScalarValue)
                    
                ) / r2,
                (
                        scalarProcessor.Times(c1.Imaginary.ScalarValue, c2.Real.ScalarValue) -
                        scalarProcessor.Times(c1.Real.ScalarValue, c2.Imaginary.ScalarValue)
                ) / r2
            );
        }


        public IScalarProcessor<T> ScalarProcessor 
            => Real.ScalarProcessor;
            
        public Scalar<T> Real { get; }

        public T RealValue 
            => Real.ScalarValue;

        public Scalar<T> Imaginary { get; }
        
        public T ImaginaryValue 
            => Imaginary.ScalarValue;
        
        public int VSpaceDimensions 
            => 2;

        public Scalar<T> Item1 
            => Real;

        public Scalar<T> Item2 
            => Imaginary;

        public Scalar<T> X 
            => Real;

        public Scalar<T> Y 
            => Imaginary;

        public Scalar<T> Magnitude
            => MagnitudeSquared.Sqrt();
        
        public T MagnitudeValue
            => Magnitude.ScalarValue;

        public Scalar<T> MagnitudeSquared
            => Real.Square() + Imaginary.Square();
        
        public T MagnitudeSquaredValue
            => MagnitudeSquared.ScalarValue;

        public LinPolarAngle<T> Phase
        {
            get
            {
                var r = MagnitudeSquaredValue;
                return ScalarProcessor.CreatePolarAngle(
                    ScalarProcessor.Divide(RealValue, r).ScalarValue,
                    ScalarProcessor.Divide(ImaginaryValue, r).ScalarValue
                );
            }
        }

        public int Count 
            => 2;

        public Scalar<T> this[int index] 
            => index switch
            {
                0 => Real,
                1 => Imaginary,
                _ =>  throw new IndexOutOfRangeException()
            };
        
        
        internal ComplexNumber(IScalarProcessor<T> scalarProcessor, T real)
        {
            Real = scalarProcessor.ScalarFromValue(real);
            Imaginary = scalarProcessor.ScalarFromValue(scalarProcessor.ZeroValue);
        }

        internal ComplexNumber(IScalarProcessor<T> scalarProcessor, T real, T imaginary)
        {
            Real = scalarProcessor.ScalarFromValue(real);
            Imaginary = scalarProcessor.ScalarFromValue(imaginary);
        }
        
        internal ComplexNumber(Scalar<T> real)
        {
            Real = real;
            Imaginary = real.ScalarProcessor.Zero;
        }

        internal ComplexNumber(Scalar<T> real, Scalar<T> imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        
        public bool IsValid()
        {
            return Real.IsValid() && Imaginary.IsValid();
        }

        public bool IsZero()
        {
            return Real.IsZero() &&
                   Imaginary.IsZero();
        }
        
        public bool IsNearZero()
        {
            return Magnitude.IsNearZero();
        }

        public ComplexNumber<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new ComplexNumber<T>(
                ScalarProcessor,
                scalarMapping(RealValue),
                scalarMapping(ImaginaryValue)
            );
        }
        
        public ComplexNumber<T1> MapScalars<T1>(Func<T, T1> scalarMapping, IScalarProcessor<T1> scalarProcessor)
        {
            return new ComplexNumber<T1>(
                scalarProcessor,
                scalarMapping(RealValue),
                scalarMapping(ImaginaryValue)
            );
        }

        public ComplexNumber<T> Negative()
        {
            return new ComplexNumber<T>(
                ScalarProcessor.Negative(Real.ScalarValue), 
                ScalarProcessor.Negative(Imaginary.ScalarValue)
            );
        }

        public ComplexNumber<T> Conjugate()
        {
            return new ComplexNumber<T>(
                Real, 
                ScalarProcessor.Negative(Imaginary.ScalarValue)
            );
        }
        
        public ComplexNumber<T> Inverse()
        {
            var r2 = MagnitudeSquared;

            return new ComplexNumber<T>(
                ScalarProcessor.Divide(Real.ScalarValue, r2.ScalarValue), 
                ScalarProcessor.NegativeDivide(Imaginary.ScalarValue, r2.ScalarValue)
            );
        }
        
        public ComplexNumber<T> Square()
        {
            return new ComplexNumber<T>(
                Real.Square() - Imaginary.Square(),
                ScalarProcessor.Times(Real.ScalarValue, Imaginary.ScalarValue) + 
                ScalarProcessor.Times(Imaginary.ScalarValue, Real.ScalarValue)
            );
        }

        public ComplexNumber<T> LogE()
        {
            return new ComplexNumber<T>(
                ScalarProcessor.LogE(Magnitude.ScalarValue),
                Phase.Radians
            );
        }

        public IEnumerator<Scalar<T>> GetEnumerator()
        {
            yield return Real;
            yield return Imaginary;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
