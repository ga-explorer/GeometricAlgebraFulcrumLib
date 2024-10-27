using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian;

public class CGpNode
{
    internal static CGpNode Create(CGpChromosome chromosome, int nodeIndex)
    {
        var cgpParameters = chromosome.Context.Parameters;

        var node = new CGpNode(chromosome, nodeIndex);

        node.SetRandomFunction();

        for (var i = 0; i < cgpParameters.MaxArity; i++)
        {
            node.SetRandomInputIndex(i);
            //node.InputIndices[i] = cgpParameters.GetRandomNodeInputIndex(nodeIndex);

            node.Weights[i] = cgpParameters.CreateNodeInputWeight();
        }

        return node;
    }
    
    internal static CGpNode CreateUnitWeightsNonRecurrent(CGpChromosome chromosome, int nodeIndex)
    {
        var cgpParameters = chromosome.Context.Parameters;

        var node = new CGpNode(chromosome, nodeIndex);

        node.SetRandomFunction();

        for (var i = 0; i < cgpParameters.MaxArity; i++)
        {
            node.SetRandomInputIndex(i);
            //node.InputIndices[i] = cgpParameters.GetRandomNodeInputIndex(nodeIndex);

            node.Weights[i] = CGpNodeInputWeight.One;
        }

        return node;
    }
    

    public CGpChromosome Chromosome { get; }

    public CGpContext Context 
        => Chromosome.Context;
    
    public CGpParameters Parameters 
        => Chromosome.Context.Parameters;

    public int Index { get; }

    public int GridRow 
        => Parameters.GetNodeGridRow(Index);

    public int GridColumn 
        => Parameters.GetNodeGridColumn(Index);

    public Pair<int> GridPosition 
        => Parameters.GetNodeGridPosition(Index);

    public int FunctionIndex { get; private set; }
    
    public CGpFloat64Function Function 
        => Parameters.FunctionSet[FunctionIndex];

    public string FunctionName 
        => Function.Name;

    public bool IsParametric 
        => Weights.Any(w => w.IsParametric);

    public int[] InputIndices { get; }

    public CGpNodeInputWeight[] Weights { get; }

    public bool IsActive { get; internal set; }

    public double OutputValue { get; private set; }

    public int ActualArity { get; private set; }

    
    private CGpNode(CGpChromosome chromosome, int index)
    {
        if (index < 0 || index >= chromosome.Context.Parameters.MaxNodeCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        Chromosome = chromosome;
        Index = index;

        InputIndices = new int[Parameters.MaxArity];
        Weights = new CGpNodeInputWeight[Parameters.MaxArity];
        FunctionIndex = -1;
        IsActive = true;
        OutputValue = 0;
    }

    
    public int GetMaxInputNodesCount(bool allowRecurrentConnections)
    {
        return Parameters.GetNodeMaxInputNodesCount(Index, allowRecurrentConnections);
    }
    
    public int GetMaxInputSourcesCount(bool allowRecurrentConnections)
    {
        return Parameters.GetNodeMaxInputSourcesCount(Index, allowRecurrentConnections);
    }

    public void SetRandomFunction()
    {
        Parameters.FunctionSet.GetRandomFunction(
            Parameters.MaxArity, 
            out var functionIndex, 
            out var arity
        );

        FunctionIndex = functionIndex;
        ActualArity = arity;
    }

    public void SetRandomInputIndex(int inputItemIndex)
    {
        InputIndices[inputItemIndex] = Parameters.GetRandomNodeInputIndex(Index);
    }

    //public void SetRandomInputIndices(bool allowRecurrentConnections)
    //{
    //    var totalSize = GetMaxInputSourcesCount(allowRecurrentConnections);

    //    var minInputSize = Function.MinInputSize;
        
    //    var maxInputSize = 
    //        Function.MaxInputSize < 0
    //            ? Parameters.MaxArity
    //            : Math.Min(Function.MaxInputSize, Parameters.MaxArity);

    //    if (minInputSize > maxInputSize)
    //        throw new InvalidOperationException();

    //    ActualArity = 
    //        CGpParameters.GetRandomInteger(
    //            minInputSize, 
    //            maxInputSize
    //        );

    //    var indexList =
    //        CGpParameters.GetRandomUniqueIndexList(
    //            ActualArity, 
    //            totalSize
    //        );

    //    var i = 0;
    //    foreach (var index in indexList)
    //        InputIndices[i++] = index;

    //    while (i < InputIndices.Length) 
    //        InputIndices[i++] = -1;
    //}

    public void ResetOutputValue()
    {
        OutputValue = 0d;
    }

    public void SetOutputValue(double outputValue)
    {
        if (double.IsNaN(outputValue))
            OutputValue = 0d;
        
        else if (double.IsNegativeInfinity(outputValue))
            OutputValue = double.MinValue;

        else if (double.IsPositiveInfinity(outputValue))
            OutputValue = double.MaxValue;

        else
            OutputValue = outputValue;
    }

    public void ExecuteNode(IReadOnlyList<double> chromosomeInputValues)
    {
        var inputs = new double[ActualArity];
        var weights = new double[ActualArity];
        
        // for each of the node's inputs
        for (var j = 0; j < inputs.Length; j++)
        {
            // gather the nodes input locations
            var nodeInputIndex = InputIndices[j];

            // Store the actual value of the node's input
            inputs[j] = 
                nodeInputIndex < Parameters.InputSize
                    ? chromosomeInputValues[nodeInputIndex]
                    : Chromosome.GetNodeOutputValue(nodeInputIndex - Parameters.InputSize);

            weights[j] = Weights[j].Value;
        }

        // Set the final output value of the node
        SetOutputValue( 
            Function.GetOutput(inputs, weights)
        );
    }

    public CGpNode GetCopy(CGpChromosome chromosome, int index)
    {
        var nodeDest = new CGpNode(chromosome, index)
        {
            FunctionIndex = FunctionIndex,
            ActualArity = ActualArity,
            IsActive = IsActive
        };

        for (var i = 0; i < Parameters.MaxArity; i++)
        {
            nodeDest.InputIndices[i] = InputIndices[i];
            nodeDest.Weights[i] = Weights[i].GetCopy();
        }

        return nodeDest;
    }

    public void CopyToNode(CGpNode nodeDest)
    {
        nodeDest.FunctionIndex = FunctionIndex;
        nodeDest.ActualArity = ActualArity;
        nodeDest.IsActive = IsActive;
        
        for (var i = 0; i < Parameters.MaxArity; i++)
        {
            nodeDest.InputIndices[i] = InputIndices[i];
            nodeDest.Weights[i] = Weights[i].GetCopy();
        }
    }
}