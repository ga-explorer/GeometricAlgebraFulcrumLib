using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context;

public static class MetaContextUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngouriMathMetaExpressionEvaluator CreateAngouriMathEvaluator(this MetaContext context)
    {
        return new AngouriMathMetaExpressionEvaluator(context);
    }
    
    /// <summary>
    /// Get the indices of all intermediate variables that depend on a given one,
    /// including itself, inside this list of computed variables
    /// </summary>
    /// <param name="varList"></param>
    /// <param name="varIndex"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    public static SortedSet<int> GetIntermediateDependencyIndexSet(this BijectiveList<string, IMetaExpressionVariableComputed> varList, int varIndex, int depth)
    {
        var varIndexSet = new SortedSet<int> { varIndex };
        
        for (var i = 0; i < depth; i++)
        {
            var indexList =
                varIndexSet.SelectMany(j =>
                    varList[j]
                        .DirectDependingIntermediateVariables
                        .Select(v => 
                            varList.GetIndexByKey(v.InternalName)
                        )
                ).ToImmutableArray();

            varIndexSet.AddRange(indexList);
        }

        return varIndexSet;
    }
    
    /// <summary>
    /// Get the indices of all intermediate variables that depend on a given one,
    /// including itself, inside this list of computed variables
    /// </summary>
    /// <param name="varList"></param>
    /// <param name="varIndexList"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    public static SortedSet<int> GetIntermediateDependencyIndexSet(this BijectiveList<string, IMetaExpressionVariableComputed> varList, IEnumerable<int> varIndexList, int depth)
    {
        var varIndexSet = new SortedSet<int>(varIndexList);
        
        for (var i = 0; i < depth; i++)
        {
            var indexList =
                varIndexSet.SelectMany(j =>
                    varList[j]
                        .DirectDependingIntermediateVariables
                        .Select(v => 
                            varList.GetIndexByKey(v.InternalName)
                        )
                ).ToImmutableArray();

            varIndexSet.AddRange(indexList);
        }

        return varIndexSet;
    }

}