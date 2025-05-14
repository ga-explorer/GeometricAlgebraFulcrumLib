using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context;

public static class MetaContextMergeUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMetaExpressionAtomic BreakMerge(this IMetaExpressionAtomic scalar)
    {
        if (scalar is MetaExpressionVariableComputed computedVariable)
            computedVariable.DisableMerge();

        return scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<IMetaExpressionAtomic> BreakMerge(this Scalar<IMetaExpressionAtomic> scalar)
    {
        scalar.ScalarValue.BreakMerge();

        return scalar;
    }
    
    public static IScalar<IMetaExpressionAtomic> BreakMerge(this IScalar<IMetaExpressionAtomic> scalar)
    {
        scalar.ScalarValue.BreakMerge();

        return scalar;
    }

    public static LinDirectedAngle<IMetaExpressionAtomic> BreakMerge(this LinDirectedAngle<IMetaExpressionAtomic> angle)
    {
        angle.RadiansValue.BreakMerge();

        return angle;
    }

    public static LinPolarAngle<IMetaExpressionAtomic> BreakMerge(this LinPolarAngle<IMetaExpressionAtomic> angle)
    {
        angle.CosValue.BreakMerge();
        angle.SinValue.BreakMerge();

        return angle;
    }

    public static ComplexNumber<IMetaExpressionAtomic> BreakMerge(this ComplexNumber<IMetaExpressionAtomic> number)
    {
        number.Real.BreakMerge();
        number.Imaginary.BreakMerge();

        return number;
    }
    
    public static LinVector2D<IMetaExpressionAtomic> BreakMerge(this LinVector2D<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();

        return vector;
    }

    public static ILinVector2D<IMetaExpressionAtomic> BreakMerge(this ILinVector2D<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();

        return vector;
    }
    
    public static LinVector3D<IMetaExpressionAtomic> BreakMerge(this LinVector3D<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();

        return vector;
    }

    public static ILinVector3D<IMetaExpressionAtomic> BreakMerge(this ILinVector3D<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();

        return vector;
    }
    
    public static LinVector4D<IMetaExpressionAtomic> BreakMerge(this LinVector4D<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();
        vector.Item4.BreakMerge();

        return vector;
    }

    public static ILinVector4D<IMetaExpressionAtomic> BreakMerge(this ILinVector4D<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();
        vector.Item4.BreakMerge();

        return vector;
    }
    
    public static IPair<IMetaExpressionAtomic> BreakMerge(this IPair<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();

        return vector;
    }

    public static IPair<Scalar<IMetaExpressionAtomic>> BreakMerge(this IPair<Scalar<IMetaExpressionAtomic>> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();

        return vector;
    }

    public static ITriplet<IMetaExpressionAtomic> BreakMerge(this ITriplet<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();

        return vector;
    }

    public static ITriplet<Scalar<IMetaExpressionAtomic>> BreakMerge(this ITriplet<Scalar<IMetaExpressionAtomic>> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();

        return vector;
    }

    public static IQuad<IMetaExpressionAtomic> BreakMerge(this IQuad<IMetaExpressionAtomic> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();
        vector.Item4.BreakMerge();

        return vector;
    }

    public static IQuad<Scalar<IMetaExpressionAtomic>> BreakMerge(this IQuad<Scalar<IMetaExpressionAtomic>> vector)
    {
        vector.Item1.BreakMerge();
        vector.Item2.BreakMerge();
        vector.Item3.BreakMerge();
        vector.Item4.BreakMerge();

        return vector;
    }

    public static void BreakMerge(this IEnumerable<IMetaExpressionAtomic> scalarsList)
    {
        foreach (var scalar in scalarsList)
            scalar.BreakMerge();
    }

    public static void BreakMerge(this IEnumerable<Scalar<IMetaExpressionAtomic>> scalarsList)
    {
        foreach (var scalar in scalarsList)
            scalar.BreakMerge();
    }
    
    public static XGaScalar<IMetaExpressionAtomic> BreakMerge(this XGaScalar<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }
    
    public static XGaVector<IMetaExpressionAtomic> BreakMerge(this XGaVector<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }
    
    public static XGaBivector<IMetaExpressionAtomic> BreakMerge(this XGaBivector<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }
    
    public static XGaHigherKVector<IMetaExpressionAtomic> BreakMerge(this XGaHigherKVector<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }

    public static XGaKVector<IMetaExpressionAtomic> BreakMerge(this XGaKVector<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }
    
    public static XGaGradedMultivector<IMetaExpressionAtomic> BreakMerge(this XGaGradedMultivector<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }
    
    public static XGaUniformMultivector<IMetaExpressionAtomic> BreakMerge(this XGaUniformMultivector<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }

    public static XGaMultivector<IMetaExpressionAtomic> BreakMerge(this XGaMultivector<IMetaExpressionAtomic> multivector)
    {
        var scalars =
            multivector
                .Scalars
                .Where(s => s.IsComputedVariable);

        foreach (var scalar in scalars)
            scalar.BreakMerge();

        return multivector;
    }

    public static CGaBlade<IMetaExpressionAtomic> BreakMerge(this CGaBlade<IMetaExpressionAtomic> kVector)
    {
        kVector.InternalKVector.BreakMerge();

        return kVector;
    }
    
    public static CGaVersor<IMetaExpressionAtomic> BreakMerge(this CGaVersor<IMetaExpressionAtomic> kVector)
    {
        kVector.InternalMultivector.BreakMerge();
        kVector.InternalMultivectorInverse.BreakMerge();

        return kVector;
    }

    public static IMetaExpressionAtomic[,] BreakMerge(this IMetaExpressionAtomic[,] array)
    {
        var rowsCount = array.GetLength(0);
        var colsCount = array.GetLength(1);

        for (var j = 0; j < colsCount; j++)
        for (var i = 0; i < rowsCount; i++)
        {
            var scalar = array[i, j];

            if (scalar.IsComputedVariable)
                scalar.BreakMerge();
        }

        return array;
    }


    public static MetaContext EnableMerge(this MetaContext context)
    {
        context.MergeExpressions = true;

        return context;
    }
    
    public static Scalar<IMetaExpressionAtomic> BreakMerge(this MetaContext context, Scalar<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static LinPolarAngle<IMetaExpressionAtomic> BreakMerge(this MetaContext context, LinPolarAngle<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static LinDirectedAngle<IMetaExpressionAtomic> BreakMerge(this MetaContext context, LinDirectedAngle<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }

    public static XGaScalar<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaScalar<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static XGaVector<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaVector<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }

    public static XGaBivector<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaBivector<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }

    public static XGaHigherKVector<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaHigherKVector<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static XGaKVector<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaKVector<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static XGaGradedMultivector<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaGradedMultivector<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static XGaUniformMultivector<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaUniformMultivector<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static XGaMultivector<IMetaExpressionAtomic> BreakMerge(this MetaContext context, XGaMultivector<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static CGaBlade<IMetaExpressionAtomic> BreakMerge(this MetaContext context, CGaBlade<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }
    
    public static CGaVersor<IMetaExpressionAtomic> BreakMerge(this MetaContext context, CGaVersor<IMetaExpressionAtomic> rhsExpression)
    {
        return rhsExpression.BreakMerge();
    }

}