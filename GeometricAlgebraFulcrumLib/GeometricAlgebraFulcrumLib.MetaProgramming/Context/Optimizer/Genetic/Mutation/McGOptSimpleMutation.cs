using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using MathNet.Numerics.LinearAlgebra.Double;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Mutation;

public sealed class McGOptSimpleMutation :
    McGOptMutation
{
    public override string Name 
        => "simple mutation";
    

    internal McGOptSimpleMutation()
    {
    }

    
    public Pair<Matrix> GetInvertibleMap(McGOptParameters parameters, int n, int maxOpCount = 10)
    {
        var arrayComposer = Float64InvertibleArrayComposer.Create(n);

        arrayComposer.ApplyRandomScalingOperations(parameters.RandomGenerator, maxOpCount);

        var matrix = arrayComposer.GetMatrix();
        var matrixInverse = arrayComposer.GetMatrixInverse();

        return new Pair<Matrix>(matrix, matrixInverse);
    }


    public static void MutateLinearPairInPlace(McGOptParameters parameters, MetaContext newContext)
    {
        var variableList =
            newContext
                .GetParameterVariables()
                .Cast<IMetaExpressionVariable>()
                .Concat(newContext.GetIntermediateVariables())
                .ToImmutableArray();

        var n = variableList.Length;

        if (n < 2)
            throw new InvalidOperationException();

        //var (matrix, matrixInv) = GetInvertibleMap(parameters, n, 10);

        var (i1, i2) = parameters.RandomGenerator.GetIndexPair(n, true);
        
        
        var a1 = variableList[i1];
        var a2 = variableList[i2];

        var computationOrder = 0;
        if (a1 is IMetaExpressionVariableComputed c1 && c1.ComputationOrder > computationOrder)
            computationOrder = c1.ComputationOrder;

        if (a2 is IMetaExpressionVariableComputed c2 && c2.ComputationOrder > computationOrder)
            computationOrder = c2.ComputationOrder;

        var computedVariablesList = newContext.GetComputedVariables().ToList();

        var b1 = newContext.GetOrDefineComputedVariable(
            (x, y) => newContext.MetaExpressionProcessor.Add(x, y).ScalarValue,
            a1,
            a2
        );
        
        var b2 = newContext.GetOrDefineComputedVariable(
            (x, y) => newContext.MetaExpressionProcessor.Subtract(x, y).ScalarValue,
            a1,
            a2
        );

        foreach (var c in computedVariablesList.Where(c => c.ComputationOrder > computationOrder)) 
            c.SetComputationOrder(c.ComputationOrder + 2);
        
        b1.SetComputationOrder(computationOrder + 1);
        b2.SetComputationOrder(computationOrder + 2);

        if (variableList.All(v => v.InternalName != b1.InternalName))
            computedVariablesList.Add(b1);

        if (variableList.All(v => v.InternalName != b2.InternalName))
            computedVariablesList.Add(b2);
        
        //// For Debugging Only
        //{
        //    var computedVariablesDictionary = new Dictionary<string, IMetaExpressionVariableComputed>();

        //    foreach (var computedVariable in computedVariablesList)
        //    {
        //        if (computedVariablesDictionary.ContainsKey(computedVariable.InternalName))
        //        {
        //            Console.WriteLine(computedVariablesDictionary[computedVariable.InternalName].RhsExpressionText);
        //            Console.WriteLine(computedVariable.RhsExpressionText);
        //            Console.WriteLine();
        //        }

        //        computedVariablesDictionary.Add(
        //            computedVariable.InternalName,
        //            computedVariable
        //        );
        //    }
        //}

        var expr1 = newContext.MetaExpressionProcessor.Add(b1, b2) / 2;
        var expr2 = newContext.MetaExpressionProcessor.Subtract(b1, b2) / 2;

        foreach (var v in a1.DirectDependingVariables)
            if (v.ComputationOrder > computationOrder + 2)
                v.ReplaceRhsVariable(a1.InternalName, expr1.ScalarValue);
        
        foreach (var v in a2.DirectDependingVariables)
            if (v.ComputationOrder > computationOrder + 2)
                v.ReplaceRhsVariable(a2.InternalName, expr2.ScalarValue);

        newContext.ResetComputedVariables(
            computedVariablesList.OrderBy(x => x.ComputationOrder)
        );

        McOptDependencyUpdate.Process(newContext);

        newContext.OptimizeContext();
    }


    private static void MutateInPlace(McGOptParameters parameters, MetaContext newContext)
    {
        var intermediateVariableList =
            newContext.GetIntermediateVariables().ToList();

        var n = intermediateVariableList.Count;

        if (n < 2)
            throw new InvalidOperationException();

        // Select a random intermediate variable and remove it
        var index = parameters.GetRandomIndex(n);
        var intermediateVariable = intermediateVariableList[index];

        //newContext.RemoveIntermediateVariable(intermediateVariable);

        // Initialize a list of intermediate variables with the selected one
        var intermediateVariableSet = new HashSet<IMetaExpressionVariableComputed>()
        {
            intermediateVariable
        };

        // Add all intermediate variables that directly depend on the selected one
        // to the list
        intermediateVariableSet.AddRange(
            intermediateVariableSet.SelectMany(
                v => v.DirectDependingIntermediateVariables
            ).ToImmutableArray()
        );

        //intermediateVariableSet.AddRange(
        //    intermediateVariableSet.SelectMany(
        //        v => v.DirectDependingIntermediateVariables
        //    ).ToImmutableArray()
        //);

        // Remove all selected intermediate variables in the list from the context
        foreach (var ivar in intermediateVariableSet)
            newContext.RemoveIntermediateVariable(ivar);
    }

    public override MetaContext ApplyMutation(McGOptParameters parameters, MetaContext context)
    {
        var newContext = context.GetContextCopy();
        
        //Console.WriteLine(newContext.ToString());

        //var t1 = DateTime.Now;

        MutateInPlace(parameters, newContext);

        //Console.WriteLine(DateTime.Now - t1);

        newContext.OptimizeContext();
        
        //Console.WriteLine(newContext.ToString());

        return newContext;
    }
}