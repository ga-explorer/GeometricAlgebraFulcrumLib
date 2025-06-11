using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Cost;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Mutation;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Reproduction;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Selection;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;

public class McGOptParameters
{
    public Random RandomGenerator { get; set; }
        //= new Random(100);
        = new Random(DateTimeOffset.Now.Millisecond);
    
    public McGOptEvolutionaryStrategy EvolutionaryStrategy { get; set; }

    private int _mu;
    /// <summary>
    /// Number of parent chromosomes to be selected for reproduction
    /// </summary>
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
    /// <summary>
    /// Number of child chromosomes to be generated from 'mu' selected parents
    /// </summary>
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

    /// <summary>
    /// Number of candidate chromosomes
    /// </summary>
    public int CandidateChromosomeCount 
        => EvolutionaryStrategy switch
        {
            McGOptEvolutionaryStrategy.MuPlusLambda => Mu + Lambda,

            McGOptEvolutionaryStrategy.Lambda => Lambda,
            
            _ => throw new InvalidOperationException(
                $"Error: the evolutionary strategy '{EvolutionaryStrategy}' is not known.\nTerminating CGP-Library."
            )
        };

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

    private int _progressUpdateFrequency;
    /// <summary>
    /// Frequency of reporting algorithm progress to console
    /// </summary>
    public int ProgressUpdateFrequency
    {
        get => _progressUpdateFrequency;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _progressUpdateFrequency = value;
        }
    }
    
    private double _targetCost;
    /// <summary>
    /// Target cost threshold to stop searching if reached
    /// </summary>
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

    private int _generationCount;
    /// <summary>
    /// Max number of generations to stop searching
    /// </summary>
    public int GenerationCount
    {
        get => _generationCount;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _generationCount = value;
        }
    }

    public McGOptMutation MutationType { get; set; }

    public McGOptCostFunction CostFunction { get; set; }

    public McGOptSelectionScheme SelectionScheme { get; set; }

    public McGOptReproductionScheme ReproductionScheme { get; set; }

    public string CodeFilePath { get; set; } = string.Empty;


    public McGOptParameters()
    {
        Mu = 1;
        Lambda = 4;
        TargetCost = 0;
        GenerationCount = 1000;
        EvolutionaryStrategy = McGOptEvolutionaryStrategy.MuPlusLambda;
        MutationRate = 0.1;
        ProgressUpdateFrequency = 1;

        MutationType = McGOptMutation.Simple;
        CostFunction = McGOptCostFunction.ComputationsCount;
        SelectionScheme = McGOptSelectionScheme.SortByCost;
        ReproductionScheme = McGOptReproductionScheme.MutateRandomParent;
    }

    
    public void ResetRandomGenerator()
    {
        RandomGenerator = new Random(DateTimeOffset.Now.Millisecond);
    }
    
    public void ResetRandomGenerator(int seed)
    {
        RandomGenerator = new Random(seed);
    }

    public double GetRandomDouble()
    {
        return RandomGenerator.NextDouble();
    }

    public int GetRandomIndex(int maxPlusOne)
    {
        Debug.Assert(maxPlusOne > 0);

        return maxPlusOne == 1
            ? 0
            : RandomGenerator.Next(maxPlusOne);
    }

    public int GetRandomInteger(int value1, int value2)
    {
        if (value1 == value2) return value1;

        return value1 < value2
            ? RandomGenerator.Next(value2 - value1 + 1) + value1
            : RandomGenerator.Next(value1 - value2 + 1) + value2;
    }

    public IEnumerable<int> GetRandomUniqueIndexList(int selectionSize, int totalSize)
    {
        return RandomGenerator.GetDistinctIndices(selectionSize, totalSize);
    }

    public IEnumerable<int> GetRandomUniqueIndexList(int totalSize)
    {
        return RandomGenerator.GetDistinctIndices(totalSize);
    }

    public void PrintParameters()
    {
        Console.WriteLine("-----------------------------------------------------------\n");
        Console.WriteLine("                       Parameters                          \n");
        Console.WriteLine("-----------------------------------------------------------\n");
        Console.WriteLine("Evolutionary Strategy:\t\t\t({0}{1}{2})-ES\n", Mu, EvolutionaryStrategy, Lambda);
        Console.WriteLine("Mutation Type:\t\t\t\t{0}\n", MutationType.Name);
        Console.WriteLine("Mutation rate:\t\t\t\t{0}\n", MutationRate);
        Console.WriteLine("Cost Function:\t\t\t{0}\n", CostFunction.Name);
        Console.WriteLine("Selection scheme:\t\t\t{0}\n", SelectionScheme.Name);
        Console.WriteLine("Reproduction scheme:\t\t\t{0}\n", ReproductionScheme.Name);
        Console.WriteLine("Update frequency:\t\t\t{0}\n", ProgressUpdateFrequency);
        Console.WriteLine("-----------------------------------------------------------\n\n");
    }


    public bool CanMutate()
    {
        return RandomGenerator.NextDouble() < MutationRate;
    }
}