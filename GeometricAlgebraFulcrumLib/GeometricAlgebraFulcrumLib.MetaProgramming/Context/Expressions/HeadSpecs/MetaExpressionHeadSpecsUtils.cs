using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public static class MetaExpressionHeadSpecsUtils
{
    public static IMetaExpressionFunction CreateFunction(this IMetaExpressionHeadSpecsFunction headSpecs, MetaContext context)
    {
        return new MetaExpressionFunction(context, headSpecs);
    }

    public static IMetaExpressionFunction CreateFunction(this IMetaExpressionHeadSpecsFunction headSpecs, MetaContext context, IMetaExpression argument1)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs,
            new[]
            {
                argument1
            }
        );
    }

    public static IMetaExpressionFunction CreateFunction(this IMetaExpressionHeadSpecsFunction headSpecs, MetaContext context, IMetaExpression argument1, IMetaExpression argument2)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs,
            new[]
            {
                argument1,
                argument2
            }
        );
    }

    public static IMetaExpressionFunction CreateFunction(this IMetaExpressionHeadSpecsFunction headSpecs, MetaContext context, IMetaExpression argument1, IMetaExpression argument2, IMetaExpression argument3)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs,
            new[]
            {
                argument1,
                argument2,
                argument3
            }
        );
    }

    public static IMetaExpressionFunction CreateFunction(this IMetaExpressionHeadSpecsFunction headSpecs, MetaContext context, params IMetaExpression[] argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs,
            argumentsList
        );
    }

    public static IMetaExpressionFunction CreateFunction(this IMetaExpressionHeadSpecsFunction headSpecs, MetaContext context, IEnumerable<IMetaExpression> argumentsList)
    {
        return new MetaExpressionFunction(
            context,
            headSpecs,
            argumentsList
        );
    }

}