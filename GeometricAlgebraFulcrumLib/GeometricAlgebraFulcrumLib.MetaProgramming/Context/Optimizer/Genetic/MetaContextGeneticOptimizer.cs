namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;

public sealed class MetaContextGeneticOptimizer :
    MetaContextProcessorBase
{
    public static MetaContext Process(McGOptParameters parameters, MetaContext context)
    {
        var optimizer = new MetaContextGeneticOptimizer(parameters, context);

        optimizer.BeginProcessing();

        return optimizer.BestContext;
    }


    public McGOptChromosome BestChromosome { get; private set; }

    public MetaContext BestContext 
        => BestChromosome?.Context ?? Context;

    public Random RandomGenerator
        => Parameters.RandomGenerator;

    public McGOptParameters Parameters { get; }


    private MetaContextGeneticOptimizer(McGOptParameters parameters, MetaContext context)
        : base(context)
    {
        Parameters = parameters;
        BestChromosome = CreateChromosome();
    }


    public McGOptChromosome CreateChromosome()
    {
        return McGOptChromosome.Create(this, BestContext);
    }
    
    private McGOptChromosome GetBestChromosome(IEnumerable<McGOptChromosome> parentChromosomes, IEnumerable<McGOptChromosome> childChromosomes)
    {
        var best = CreateChromosome();

        var bestChromosome = best;

        foreach (var chromosome in parentChromosomes)
        {
            if (chromosome.Cost <= bestChromosome.Cost)
                bestChromosome = chromosome;
        }

        foreach (var chromosome in childChromosomes)
        {
            if (chromosome.Cost <= bestChromosome.Cost)
                bestChromosome = chromosome;
        }

        bestChromosome.CopyToChromosome(best);

        return best;
    }

    public McGOptChromosome Run(int generationCount)
    {
        // error checking
        if (generationCount < 0)
            throw new InvalidOperationException($"Error: {generationCount} generations is invalid. The number of generations must be >= 0.\n Terminating CGP-Library.");

        // initialise parent chromosomes
        var parentChromosomes = new McGOptChromosome[Parameters.Mu];

        for (var i = 0; i < Parameters.Mu; i++)
            parentChromosomes[i] = CreateChromosome();

        // initialise children chromosomes
        var childChromosomes = new McGOptChromosome[Parameters.Lambda];

        for (var i = 0; i < Parameters.Lambda; i++)
            childChromosomes[i] = CreateChromosome();

        // initialise the best chromosome
        var bestChromosome = CreateChromosome();

        // initialise the candidate chromosomes
        var candidateChromosomes = new McGOptChromosome[Parameters.CandidateChromosomeCount];

        for (var i = 0; i < Parameters.CandidateChromosomeCount; i++)
            candidateChromosomes[i] = CreateChromosome();

        // compute and store cost of parent chromosomes
        for (var i = 0; i < Parameters.Mu; i++)
            parentChromosomes[i].UpdateCost();

        // report progress
        if (Parameters.ProgressUpdateFrequency != 0)
        {
            Console.WriteLine();
            Console.WriteLine("-- Starting CGP --");
            Console.WriteLine("Gen\tCost");
        }

        // for each generation
        int gen;
        for (gen = 0; gen < generationCount; gen++)
        {
            // compute and store cost of population chromosomes
            for (var i = 0; i < Parameters.Lambda; i++)
                childChromosomes[i].UpdateCost();

            // get the best chromosome
            bestChromosome = GetBestChromosome(
                parentChromosomes,
                childChromosomes
            );

            bestChromosome.TrySaveCSharpCode();

            // check termination conditions
            if (bestChromosome.Cost <= Parameters.TargetCost)
            {
                if (Parameters.ProgressUpdateFrequency != 0)
                    Console.WriteLine("{0}\t{1} - Solution Found", gen, bestChromosome.Cost);

                break;
            }

            // report progress
            if (Parameters.ProgressUpdateFrequency != 0 && (gen % Parameters.ProgressUpdateFrequency == 0 || gen >= generationCount - 1))
                Console.WriteLine("{0}\t{1}", gen, bestChromosome.Cost);

            
            // Set the chromosomes which will be used by the selection scheme
            // dependant upon the evolutionary strategy. i.e. '+' all are used
            // by the selection scheme, ',' only the children are.
            if (Parameters.EvolutionaryStrategy == McGOptEvolutionaryStrategy.MuPlusLambda)
            {
                // Note: the children are placed before the parents to
                // ensure 'new blood' is always selected over old if the
                // cost are equal.
                for (var i = 0; i < Parameters.CandidateChromosomeCount; i++)
                {
                    if (i < Parameters.Lambda)
                        childChromosomes[i].CopyToChromosome(candidateChromosomes[i]);
                    else
                        parentChromosomes[i - Parameters.Lambda].CopyToChromosome(candidateChromosomes[i]);
                }
            }
            else if (Parameters.EvolutionaryStrategy == McGOptEvolutionaryStrategy.Lambda)
            {
                for (var i = 0; i < Parameters.CandidateChromosomeCount; i++)
                    childChromosomes[i].CopyToChromosome(candidateChromosomes[i]);
            }

            // select the parents from the candidate chromosomes
            Parameters.SelectionScheme.Execute(
                parentChromosomes,
                candidateChromosomes
            );

            // create the children from the parents
            Parameters.ReproductionScheme.Execute(
                parentChromosomes,
                childChromosomes
            );

            //if (Parameters.CanMutate())
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Performing Linear Pair Mutation");

            //    var chromosomeList = 
            //        parentChromosomes.Concat(childChromosomes);

            //    foreach (var chromosome in chromosomeList)
            //    {
            //        Console.WriteLine($"   Before: {chromosome.Context.ComputationsCount}");

            //        McGOptSimpleMutation.MutateLinearPairInPlace(
            //            Parameters,
            //            chromosome.Context
            //        );

            //        Console.WriteLine($"    After: {chromosome.Context.ComputationsCount}");
            //    }

            //    Console.WriteLine();
            //}
        }

        // deal with formatting for displaying progress
        if (Parameters.ProgressUpdateFrequency != 0) 
            Console.WriteLine();

        bestChromosome.Generation = gen;
        
        return bestChromosome;
    }

    public McGOptResults RepeatRun(int generationCount, int iterationCount)
    {
        var updateFrequency = Parameters.ProgressUpdateFrequency;

        // set the update frequency to generational results
        Parameters.ProgressUpdateFrequency = 0;

        var results = McGOptResults.Create(iterationCount);

        Console.WriteLine("Run\tFitness\t\tGenerations\tActive Nodes");

        // for each run
        for (var i = 0; i < iterationCount; i++)
        {
            // run cgp
            results[i] = Run(generationCount);

            Console.WriteLine(
                "{0}\t{1}\t{2}",
                i,
                results[i].Cost,
                results[i].Generation
            );
        }

        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("MEAN\t{0}\t{1}", results.GetAverageCost(), results.GetAverageGenerations());
        Console.WriteLine("MEDIAN\t{0}\t{1}", results.GetMedianCost(), results.GetMedianGenerations());
        Console.WriteLine("----------------------------------------------------\n");

        // restore the original value for the update frequency
        Parameters.ProgressUpdateFrequency = updateFrequency;

        return results;
    }

    protected override void BeginProcessing()
    {
        BestChromosome = Run(Parameters.GenerationCount);
    }
}