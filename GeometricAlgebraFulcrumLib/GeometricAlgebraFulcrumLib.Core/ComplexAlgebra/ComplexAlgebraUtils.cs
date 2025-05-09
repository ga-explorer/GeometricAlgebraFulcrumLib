using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.ComplexAlgebra;

public static class ComplexAlgebraUtils
{
    public static ComplexNumber<T> CreateComplexNumberZero<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new ComplexNumber<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.ZeroValue
        );
    }

    public static ComplexNumber<T> CreateComplexNumberOne<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new ComplexNumber<T>(
            scalarProcessor,
            scalarProcessor.OneValue,
            scalarProcessor.ZeroValue
        );
    }
    
    public static ComplexNumber<T> CreateComplexNumberMinusOne<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new ComplexNumber<T>(
            scalarProcessor,
            scalarProcessor.MinusOneValue,
            scalarProcessor.ZeroValue
        );
    }
    
    public static ComplexNumber<T> CreateComplexNumberI<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new ComplexNumber<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.OneValue
        );
    }
    
    public static ComplexNumber<T> CreateComplexNumberMinusI<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new ComplexNumber<T>(
            scalarProcessor,
            scalarProcessor.ZeroValue,
            scalarProcessor.MinusOneValue
        );
    }
    

    public static ComplexNumber<T> CreateComplexNumberReal<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            scalarProcessor.ValueFromNumber(scalar), 
            scalarProcessor.ZeroValue
        );
    }
    
    public static ComplexNumber<T> CreateComplexNumberReal<T>(this IScalarProcessor<T> scalarProcessor, Scalar<T> scalar)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            scalar.ScalarValue, 
            scalarProcessor.ZeroValue
        );
    }

    public static ComplexNumber<T> CreateComplexNumberReal<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalar)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            scalar.ScalarValue, 
            scalarProcessor.ZeroValue
        );
    }

    public static ComplexNumber<T> CreateComplexNumberReal<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return new ComplexNumber<T>(scalarProcessor, scalar, scalarProcessor.ZeroValue);
    }


    public static ComplexNumber<T> CreateComplexNumberImaginary<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            scalarProcessor.ZeroValue,
            scalarProcessor.ValueFromNumber(scalar)
        );
    }

    public static ComplexNumber<T> CreateComplexNumberImaginary<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> scalar)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            scalarProcessor.ZeroValue,
            scalar.ScalarValue
        );
    }

    public static ComplexNumber<T> CreateComplexNumberImaginary<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            scalarProcessor.ZeroValue,
            scalar
        );
    }
    

    public static ComplexNumber<T> CreateComplexNumber<T>(this IScalarProcessor<T> scalarProcessor, double real, double imaginary)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            scalarProcessor.ValueFromNumber(real), 
            scalarProcessor.ValueFromNumber(imaginary)
        );
    }

    public static ComplexNumber<T> CreateComplexNumber<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> real, IScalar<T> imaginary)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            real.ScalarValue, 
            imaginary.ScalarValue
        );
    }

    public static ComplexNumber<T> CreateComplexNumber<T>(this IScalarProcessor<T> scalarProcessor, T real, T imaginary)
    {
        return new ComplexNumber<T>(
            scalarProcessor, 
            real, 
            imaginary
        );
    }
    
    
    public static ComplexNumber<T> CreateComplexNumberUnitPolar<T>(this IScalarProcessor<T> scalarProcessor, double argument)
    {
        var real = scalarProcessor.Cos(scalarProcessor.ValueFromNumber(argument));
        var imaginary = scalarProcessor.Sin(scalarProcessor.ValueFromNumber(argument));

        return new ComplexNumber<T>(real, imaginary);
    }

    public static ComplexNumber<T> CreateComplexNumberUnitPolar<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> argument)
    {
        var real = scalarProcessor.Cos(argument.ScalarValue);
        var imaginary = scalarProcessor.Sin(argument.ScalarValue);

        return new ComplexNumber<T>(real, imaginary);
    }

    public static ComplexNumber<T> CreateComplexNumberUnitPolar<T>(this IScalarProcessor<T> scalarProcessor, T argument)
    {
        var real = scalarProcessor.Cos(argument);
        var imaginary = scalarProcessor.Sin(argument);

        return new ComplexNumber<T>(real, imaginary);
    }
    

    public static ComplexNumber<T> CreateComplexNumberPolar<T>(this IScalarProcessor<T> scalarProcessor, double modulus, double argument)
    {
        return scalarProcessor.CreateComplexNumberPolar(
            scalarProcessor.ValueFromNumber(modulus),
            scalarProcessor.ValueFromNumber(argument)
        );
    }
    
    public static ComplexNumber<T> CreateComplexNumberPolar<T>(this IScalarProcessor<T> scalarProcessor, Scalar<T> modulus, Scalar<T> argument)
    {
        return scalarProcessor.CreateComplexNumberPolar(
            modulus.ScalarValue,
            argument.ScalarValue
        );
    }

    public static ComplexNumber<T> CreateComplexNumberPolar<T>(this IScalarProcessor<T> scalarProcessor, IScalar<T> modulus, IScalar<T> argument)
    {
        return scalarProcessor.CreateComplexNumberPolar(
            modulus.ScalarValue,
            argument.ScalarValue
        );
    }

    public static ComplexNumber<T> CreateComplexNumberPolar<T>(this IScalarProcessor<T> scalarProcessor, T modulus, T argument)
    {
        Debug.Assert(modulus != null, nameof(modulus) + " != null");

        var real = modulus * scalarProcessor.Cos(argument);
        var imaginary = modulus * scalarProcessor.Sin(argument);

        return new ComplexNumber<T>(real, imaginary);
    }

    
    public static ComplexNumber<T> Determinant<T>(ComplexNumber<T> a11, ComplexNumber<T> a21, ComplexNumber<T> a12, ComplexNumber<T> a22)
    {
        return a11 * a22 - a12 * a21;
    }

    public static ComplexNumber<T> Determinant<T>(this IScalarProcessor<T> scalarProcessor, ComplexNumber<T> a11, ComplexNumber<T> a21, ComplexNumber<T> a12, ComplexNumber<T> a22)
    {
        return a11 * a22 - a12 * a21;
    }
    
    public static Pair<ComplexNumber<T>> SolveLinear2D<T>(ComplexNumber<T> a1, ComplexNumber<T> b1, ComplexNumber<T> c1, ComplexNumber<T> a2, ComplexNumber<T> b2, ComplexNumber<T> c2)
    {
        var det1 = Determinant(c1, c2, b1, b2);
        var det2 = Determinant(a1, a2, c1, c2);
        var det0 = Determinant(a1, a2, b1, b2);

        if (det0.IsZero())
            throw new DivideByZeroException();

        return new Pair<ComplexNumber<T>>(
            det1 / det0,
            det2 / det0
        );
    }

    public static Pair<ComplexNumber<T>> SolveLinear2D<T>(this IScalarProcessor<T> scalarProcessor, ComplexNumber<T> a1, ComplexNumber<T> b1, ComplexNumber<T> c1, ComplexNumber<T> a2, ComplexNumber<T> b2, ComplexNumber<T> c2)
    {
        var det1 = Determinant(c1, c2, b1, b2);
        var det2 = Determinant(a1, a2, c1, c2);
        var det0 = Determinant(a1, a2, b1, b2);

        if (det0.IsZero())
            throw new DivideByZeroException();

        return new Pair<ComplexNumber<T>>(
            det1 / det0,
            det2 / det0
        );
    }
}