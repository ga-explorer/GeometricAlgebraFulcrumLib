using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Functions;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Mutation;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Reproduction;
using GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Selection;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian;

public class CGpParameters
{
    private static Random RandomGenerator { get; set; } 
        //= new Random(100);
        = new Random(DateTimeOffset.Now.Millisecond);


    public static void ResetRandomGenerator()
    {
        RandomGenerator = new Random(DateTimeOffset.Now.Millisecond);
    }

    public static double GetRandomDouble()
    {
        return RandomGenerator.NextDouble();
    }
    
    public static int GetRandomIndex(int maxPlusOne)
    {
        Debug.Assert(maxPlusOne > 0);

        return maxPlusOne == 1 
            ? 0 
            : RandomGenerator.Next(maxPlusOne);
    }
    
    public static int GetRandomInteger(int value1, int value2)
    {
        if (value1 == value2) return value1;

        return value1 < value2
            ? RandomGenerator.Next(value2 - value1 + 1) + value1
            : RandomGenerator.Next(value1 - value2 + 1) + value2;
    }

    public static IEnumerable<int> GetRandomUniqueIndexList(int selectionSize, int totalSize)
    {
        return RandomGenerator.GetUniqueIndices(selectionSize, totalSize);
    }
    
    public static IEnumerable<int> GetRandomUniqueIndexList(int totalSize)
    {
        return RandomGenerator.GetUniqueIndices(totalSize);
    }


    //public static CGpParameters Create(CGpDataSet dataSet, int inputSize, int outputSize, int nodeCount, int arity)
    //{
    //    return new CGpParameters(
    //        dataSet, 
    //        inputSize, 
    //        outputSize, 
    //        nodeCount, 
    //        arity
    //    );
    //}
    
    
    /// <summary>
    /// Number of scalar inputs of computational graph
    /// </summary>
    public int InputSize { get; }

    /// <summary>
    /// Number of scalar outputs of computational graph
    /// </summary>
    public int OutputSize { get; }
    
    /// <summary>
    /// Number of function nodes per column of Cartesian grid
    /// </summary>
    public int GridRows { get; }

    /// <summary>
    /// Number of function nodes per row of Cartesian grid
    /// </summary>
    public int GridColumns { get; }

    /// <summary>
    /// Total number of function nodes of Cartesian grid
    /// </summary>
    public int MaxNodeCount 
        => GridRows * GridColumns;

    private int _maxLevelsBack;
    /// <summary>
    /// Maximum number of previous columns (> 0) that in input to a node can connect to
    /// </summary>
    public int MaxLevelsBack
    {
        get => _maxLevelsBack;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _maxLevelsBack = value;
        }
    }
    
    /// <summary>
    /// Maximum number of following columns (>= 0) that in input to a node can connect to
    /// </summary>
    private int _maxLevelsForward;
    public int MaxLevelsForward
    {
        get => _maxLevelsForward;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _maxLevelsForward = value;
        }
    }

    /// <summary>
    /// Maximum number of inputs per node
    /// </summary>
    public int MaxArity { get; }
    
    /// <summary>
    /// The set of functions for the algorithm
    /// </summary>
    public CGpFunctionSet FunctionSet { get; }


    private int _mu;
    public int Mu
    {
        get => _mu;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _mu = value;
        }
    }

    private int _lambda;
    public int Lambda
    {
        get => _lambda;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _lambda = value;
        }
    }

    public CGpEvolutionaryStrategy EvolutionaryStrategy { get; set; }

    private double _mutationRate;
    public double MutationRate
    {
        get => _mutationRate;
        set
        {
            if (value is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _mutationRate = value;
        }
    }

    private double _recurrentConnectionProbability;
    public double RecurrentConnectionProbability
    {
        get => _recurrentConnectionProbability;
        set
        {
            if (value is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _recurrentConnectionProbability = value;
        }
    }

    public bool RecurrentConnectionsAllowed 
        => _recurrentConnectionProbability > 0;

    private double _nodeInputWeightRange;
    public double NodeInputWeightRange
    {
        get => _nodeInputWeightRange;
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            _nodeInputWeightRange = Math.Abs(value);
        }
    }

    private double _parametricNodeInputWeightRatio;
    public double ParametricNodeInputWeightRatio
    {
        get => _parametricNodeInputWeightRatio;
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (value is < 0 or > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            _parametricNodeInputWeightRatio = value;
        }
    }

    private double _targetCost;
    public double TargetCost
    {
        get => _targetCost;
        set
        {
            if (double.IsNaN(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            _targetCost = value;
        }
    }

    private int _updateFrequency;
    public int UpdateFrequency
    {
        get => _updateFrequency;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _updateFrequency = value;
        }
    }
    
    private int _threadCount;
    public int ThreadCount
    {
        get => _threadCount;
        set
        {
            if (value <= 0) 
                throw new ArgumentOutOfRangeException(nameof(value));

            _threadCount = value;
        }
    }

    public bool ShortcutConnections { get; set; }
    
    public CGpMutation MutationType { get; set; } 
    
    public CGpSelectionScheme SelectionScheme { get; set; }

    public CGpReproductionScheme ReproductionScheme { get; set; }


    public CGpParameters(CGpFunctionSet functionSet, int inputSize, int outputSize, int gridColumns, int maxArity)
        : this(functionSet, inputSize, outputSize, 1, gridColumns, maxArity)
    {
    }

    public CGpParameters(CGpFunctionSet functionSet, int inputSize, int outputSize, int gridRows, int gridColumns, int maxArity)
    {
        if (inputSize < 1)
            throw new ArgumentException(nameof(inputSize));

        if (outputSize < 1)
            throw new ArgumentException(nameof(outputSize));
        
        if (gridRows < 1)
            throw new ArgumentException(nameof(gridRows));
        
        if (gridColumns < 1)
            throw new ArgumentException(nameof(gridColumns));

        if (maxArity < 1)
            throw new ArgumentException(nameof(maxArity));

        FunctionSet = functionSet;

        InputSize = inputSize;
        OutputSize = outputSize;
        GridRows = gridRows;
        GridColumns = gridColumns;
        MaxLevelsBack = gridColumns;
        MaxLevelsForward = gridColumns;
        MaxArity = maxArity;

        Mu = 1;
        Lambda = 4;
        EvolutionaryStrategy = CGpEvolutionaryStrategy.MuPlusLambda;
        MutationRate = 0.05;
        RecurrentConnectionProbability = 0.0;
        //ConnectionWeightRange = 0;
        ShortcutConnections = true;
        TargetCost = 0;
        UpdateFrequency = 1;
        ThreadCount = 1;

        MutationType = CGpMutation.Probabilistic;
        //CostFunction = CGpRegressionCostFunction.Rms;
        SelectionScheme = CGpSelectionScheme.ByFittest;
        ReproductionScheme = CGpReproductionScheme.MutateRandomParent;
    }


    public int GetNodeIndex(int row, int column)
    {
        if (row < 0 || row >= GridRows)
            throw new ArgumentOutOfRangeException(nameof(row));

        if (column < 0 || column >= GridColumns)
            throw new ArgumentOutOfRangeException(nameof(column));

        return GridRows * column + row;
    }
    
    public int GetNodeGridRow(int index)
    {
        if (index < 0 || index >= MaxNodeCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        return index % GridRows;
    }

    public int GetNodeGridColumn(int index)
    {
        if (index < 0 || index >= MaxNodeCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        return (index - index % GridRows) / GridRows;
    }

    public Pair<int> GetNodeGridPosition(int index)
    {
        if (index < 0 || index >= MaxNodeCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        var row = index % GridRows;
        var column = (index - row) / GridRows;

        return new Pair<int>(row, column);
    }
    
    public Pair<int> GetNodeIndexRangeFromColumnRange(int col1, int col2)
    {
        Debug.Assert(col1 <= col2);

        var index1 = col1 * GridRows;
        var index2 = col2 * GridRows + GridRows - 1;

        return new Pair<int>(index1, index2);
    }

    public Pair<int> GetNodeInputNodesIndexRange(int nodeIndex, bool allowRecurrentConnections)
    {
        if (allowRecurrentConnections)
        {
            // Recurrent connections start at any node in the same or later column

            var col1 = GetNodeGridColumn(nodeIndex);
            var col2 = Math.Min(col1 + MaxLevelsForward, GridColumns - 1);

            return GetNodeIndexRangeFromColumnRange(col1, col2);
        }
        else
        {
            // Non-Recurrent connections start at any node in a previous column

            // This node is in the first column, it has no previous columns
            if (nodeIndex < GridRows)
                return new Pair<int>(-1, -1);

            var col2 = GetNodeGridColumn(nodeIndex) - 1;
            var col1 = Math.Max(col2 - MaxLevelsBack + 1, 0);

            return GetNodeIndexRangeFromColumnRange(col1, col2);
        }
    }
    
    public int GetNodeMaxInputNodesCount(int nodeIndex, bool allowRecurrentConnections)
    {
        var (index1, index2) = 
            GetNodeInputNodesIndexRange(nodeIndex, allowRecurrentConnections);
        
        return index1 < 0 
            ? 0 
            : index2 - index1 + 1;
    }

    public int GetNodeMaxInputSourcesCount(int nodeIndex, bool allowRecurrentConnections)
    {
        return InputSize + 
               GetNodeMaxInputNodesCount(nodeIndex, allowRecurrentConnections);
    }


    public void PrintParameters()
    {
        Console.WriteLine("-----------------------------------------------------------\n");
        Console.WriteLine("                       Parameters                          \n");
        Console.WriteLine("-----------------------------------------------------------\n");
        Console.WriteLine("Evolutionary Strategy:\t\t\t({0}{1}{2})-ES\n", Mu, EvolutionaryStrategy, Lambda);
        Console.WriteLine("Inputs:\t\t\t\t\t{0}\n", InputSize);
        Console.WriteLine("Nodes:\t\t\t\t\t{0}\n", MaxNodeCount);
        Console.WriteLine("Outputs:\t\t\t\t{0}\n", OutputSize);
        Console.WriteLine("Node Arity:\t\t\t\t{0}\n", MaxArity);
        //Console.WriteLine("Connection weights range:\t\t+/- {0}\n", ConnectionWeightRange);
        Console.WriteLine("Mutation Type:\t\t\t\t{0}\n", MutationType.Name);
        Console.WriteLine("Mutation rate:\t\t\t\t{0}\n", MutationRate);
        Console.WriteLine("Recurrent Connection Probability:\t{0}\n", RecurrentConnectionProbability);
        Console.WriteLine("Shortcut Connections:\t\t\t{0}\n", ShortcutConnections);
        //Console.WriteLine("Cost Function:\t\t\t{0}\n", CostFunction.Name);
        Console.WriteLine("Target Cost:\t\t\t\t{0}\n", TargetCost);
        Console.WriteLine("Selection scheme:\t\t\t{0}\n", SelectionScheme.Name);
        Console.WriteLine("Reproduction scheme:\t\t\t{0}\n", ReproductionScheme.Name);
        Console.WriteLine("Update frequency:\t\t\t{0}\n", UpdateFrequency);
        Console.WriteLine("Threads:\t\t\t{0}\n", ThreadCount);
        //PrintFunctionSet();
        Console.WriteLine("-----------------------------------------------------------\n\n");
    }


    public bool CanMutate()
    {
        return RandomGenerator.NextDouble() < MutationRate;
    }

    //public int GetRandomFunctionIndex(int maxInputSourceCount)
    //{
    //    if (maxInputSourceCount < 0)
    //        throw new InvalidOperationException();

    //    var indexList = 
    //        FunctionSet.Count
    //            .GetRange()
    //            .Where(i => FunctionSet[i].MinInputSize <= maxInputSourceCount)
    //            .ToImmutableArray();

    //    return indexList.Length switch
    //    {
    //        < 1 => throw new InvalidOperationException(),
    //        1 => indexList[0],
    //        _ => indexList[RandomGenerator.Next(indexList.Length)]
    //    };
    //}

    //public int GetRandomNodeInputIndex(int nodeCount, int nodeIndex)
    //{
    //    return RandomGenerator.NextDouble() < RecurrentConnectionProbability
    //        /* pick any previous nodes or the node itself */
    //        ? RandomGenerator.Next(nodeCount - nodeIndex) + nodeIndex + InputSize

    //        /* pick any previous node including inputs */
    //        : RandomGenerator.Next(InputSize + nodeIndex);
    //}

    //public int GetRandomNodeInputIndexNonRecurrent(int nodeIndex)
    //{
    //    return RandomGenerator.Next(InputSize + nodeIndex);
    //}

    public int GetRandomNodeInputIndex(int nodeIndex)
    {
        var allowRecurrentConnections =
            RecurrentConnectionsAllowed &&
            RandomGenerator.NextDouble() < RecurrentConnectionProbability;

        if (allowRecurrentConnections)
        {
            var (index1, index2) = 
                GetNodeInputNodesIndexRange(nodeIndex, true);

            return GetRandomInteger(index1, index2) + InputSize;
        }
        else
        {
            var (index1, index2) = 
                GetNodeInputNodesIndexRange(nodeIndex, false);

            if (index1 < 0)
                return GetRandomIndex(InputSize);

            var index = GetRandomIndex(InputSize + index2 - index1 + 1);

            return index < InputSize 
                ? index 
                : index + index1;
        }
    }

    public CGpNodeInputWeight CreateNodeInputWeight(double midValue = 1d)
    {
        var valueRange =
            NodeInputWeightRange > 0d &&
            RandomGenerator.NextDouble() < ParametricNodeInputWeightRatio
                ? NodeInputWeightRange
                : 0;

        return new CGpNodeInputWeight(midValue, valueRange);
    }
}