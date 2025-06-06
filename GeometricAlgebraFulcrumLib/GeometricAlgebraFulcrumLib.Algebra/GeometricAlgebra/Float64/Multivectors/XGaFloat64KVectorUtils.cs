﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64KVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearBlade(this XGaFloat64KVector kv, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return kv
            .Gp(kv.Reverse())
            .GetKVectorParts()
            .All(kv1 => 
                kv1.Grade == 0 || kv1.IsNearZero(zeroEpsilon)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector GetPart(this XGaFloat64KVector mv, Func<IndexSet, bool> filterFunc)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector GetPart(this XGaFloat64KVector mv, Func<double, bool> filterFunc)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector GetPart(this XGaFloat64KVector mv, Func<IndexSet, double, bool> filterFunc)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector RemoveSmallTerms(this XGaFloat64KVector mv, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s,
            XGaFloat64Vector v => v.RemoveSmallTerms(zeroEpsilon),
            XGaFloat64Bivector bv => bv.RemoveSmallTerms(zeroEpsilon),
            XGaFloat64HigherKVector kv => kv.RemoveSmallTerms(zeroEpsilon),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector MapScalars(this XGaFloat64KVector mv, Func<double, double> scalarMapping)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.MapScalar(scalarMapping),
            XGaFloat64Vector v => v.MapScalars(scalarMapping),
            XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector MapScalars(this XGaFloat64KVector mv, Func<IndexSet, double, double> scalarMapping)
    {
        return mv switch
        {
            XGaFloat64Scalar s => s.MapScalar(scalarMapping),
            XGaFloat64Vector v => v.MapScalars(scalarMapping),
            XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
            XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Negative(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Negative(),
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Reverse(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector => mv,
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector GradeInvolution(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1,
            XGaFloat64HigherKVector mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector CliffordConjugate(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Conjugate(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar => mv,
            XGaFloat64Vector mv1 => mv1.Conjugate(),
            XGaFloat64Bivector mv1 => mv1.Conjugate(),
            XGaFloat64HigherKVector mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Times(this XGaFloat64KVector mv, double scalarValue)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Times(scalarValue),
            XGaFloat64Vector mv1 => mv1.Times(scalarValue),
            XGaFloat64Bivector mv1 => mv1.Times(scalarValue),
            XGaFloat64HigherKVector mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Divide(this XGaFloat64KVector mv, double scalarValue)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.Divide(scalarValue),
            XGaFloat64Vector mv1 => mv1.Divide(scalarValue),
            XGaFloat64Bivector mv1 => mv1.Divide(scalarValue),
            XGaFloat64HigherKVector mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector DivideByENorm(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByENorm(),
            XGaFloat64Vector mv1 => mv1.DivideByENorm(),
            XGaFloat64Bivector mv1 => mv1.DivideByENorm(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector DivideByENormSquared(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByENormSquared(),
            XGaFloat64Vector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64Bivector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector DivideByNorm(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByNorm(),
            XGaFloat64Vector mv1 => mv1.DivideByNorm(),
            XGaFloat64Bivector mv1 => mv1.DivideByNorm(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector DivideByNormSquared(this XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByNormSquared(),
            XGaFloat64Vector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64Bivector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector EInverse(this XGaFloat64KVector kVector)
    {
        return kVector switch
        {
            XGaFloat64Scalar s => s.EInverse(),
            XGaFloat64Vector v => v.EInverse(),
            XGaFloat64Bivector bv => bv.EInverse(),
            XGaFloat64HigherKVector kv => kv.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Inverse(this XGaFloat64KVector kVector)
    {
        return kVector switch
        {
            XGaFloat64Scalar s => s.Inverse(),
            XGaFloat64Vector v => v.Inverse(),
            XGaFloat64Bivector bv => bv.Inverse(),
            XGaFloat64HigherKVector kv => kv.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector PseudoInverse(this XGaFloat64KVector kVector)
    {
        return kVector switch
        {
            XGaFloat64Scalar s => s.PseudoInverse(),
            XGaFloat64Vector v => v.PseudoInverse(),
            XGaFloat64Bivector bv => bv.PseudoInverse(),
            XGaFloat64HigherKVector kv => kv.PseudoInverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector EDual(this XGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalarEInverse(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector EDual(this XGaFloat64KVector kVector, XGaFloat64KVector blade)
    {
        return kVector.ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Dual(this XGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalarInverse(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Dual(this XGaFloat64KVector kVector, XGaFloat64KVector blade)
    {
        return kVector.Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector EUnDual(this XGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalar(vSpaceDimensions);

        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector EUnDual(this XGaFloat64KVector kVector, XGaFloat64KVector blade)
    {
        return kVector.ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector UnDual(this XGaFloat64KVector kVector, int vSpaceDimensions)
    {
        var blade =
            kVector.Processor.PseudoScalar(vSpaceDimensions);

        return kVector.Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector UnDual(this XGaFloat64KVector kVector, XGaFloat64KVector blade)
    {
        return kVector.Lcp(blade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Op(this IEnumerable<XGaFloat64Vector> mvList, XGaFloat64Processor processor)
    {
        var blade = (XGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }
        
    public static XGaFloat64KVector Op(this XGaFloat64Processor processor, IEnumerable<XGaFloat64Vector> mvList)
    {
        var blade = (XGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsZero)
                return processor.ScalarZero;

            blade = newBlade;
        }

        return blade;
    }
    
    public static XGaFloat64KVector SpanToBlade(this IEnumerable<XGaFloat64Vector> mvList, XGaFloat64Processor processor)
    {
        var blade = (XGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsNearZero())
                continue;

            blade = newBlade;
        }

        return blade;
    }

    public static XGaFloat64KVector SpanToBlade(this XGaFloat64Processor processor, IEnumerable<XGaFloat64Vector> mvList)
    {
        var blade = (XGaFloat64KVector) processor.ScalarOne;

        foreach (var vector in mvList)
        {
            var newBlade = blade.Op(vector);

            if (newBlade.IsNearZero())
                continue;

            blade = newBlade;
        }

        return blade;
    }

}