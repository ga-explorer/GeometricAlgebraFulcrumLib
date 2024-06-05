using GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.DataSets;
using GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Functions;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian
{
    public class CGpContext
    {
        public static CGpContext Create(CGpParameters cgpParameters)
        {
            return new CGpContext(cgpParameters);
        }

        public static CGpContext Create(CGpFunctionSet functionSet, int inputSize, int outputSize, int nodeRows, int nodeColumns, int arity)
        {
            var cgpParameters = new CGpParameters(
                functionSet, 
                inputSize, 
                outputSize, 
                nodeRows, 
                nodeColumns,
                arity
            );

            return new CGpContext(cgpParameters);
        }
        

        public CGpParameters Parameters { get; }

        //public CGpFunctionSet FunctionSet
        //    => Parameters.FunctionSet;

        public CGpDataSet? DataSet { get; set; }


        private CGpContext(CGpParameters cgpParameters)
        {
            Parameters = cgpParameters;
        }


        public CGpChromosome CreateChromosome()
        {
            return CGpChromosome.Create(this);
        }
        
        public CGpChromosome CreateChromosome(CGpChromosome originalChromosome)
        {
            return CGpChromosome.CreateFromChromosome(originalChromosome);
        }

        private CGpChromosome GetBestChromosome(IEnumerable<CGpChromosome> parentChromosomes, IEnumerable<CGpChromosome> childChromosomes)
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

        //public CGpChromosome OptimizeParameters(CGpChromosome originalChromosome, int generationCount)
        //{
        //    /* error checking */
        //    if (generationCount < 0)
        //        throw new InvalidOperationException($"Error: {generationCount} generations is invalid. The number of generations must be >= 0.\n Terminating CGP-Library.\n");
            
        //    if (DataSet is null)
        //        throw new InvalidOperationException("Data set not present");

        //    if (Parameters.InputSize != DataSet.InputSize)
        //        throw new InvalidOperationException($"Error: The number of inputs specified the dataSet ({DataSet.InputSize}) does not match the number of inputs specified the parameters ({Parameters.InputSize}).\n");

        //    if (Parameters.OutputSize != DataSet.OutputSize)
        //        throw new InvalidOperationException($"Error: The number of outputs specified the dataSet ({DataSet.OutputSize}) does not match the number of outputs specified the parameters ({Parameters.OutputSize}).\n");

        //    /* initialise parent chromosomes */
        //    var parentChromosomes = new CGpChromosome[Parameters.Mu];

        //    for (var i = 0; i < Parameters.Mu; i++) 
        //        parentChromosomes[i] = CreateChromosome(originalChromosome);

        //    /* initialise children chromosomes */
        //    var childChromosomes = new CGpChromosome[Parameters.Lambda];

        //    for (var i = 0; i < Parameters.Lambda; i++)
        //        childChromosomes[i] = CreateChromosome(originalChromosome);

        //    /* initialise best chromosome */
        //    var bestChromosome = CreateChromosome(originalChromosome);

        //    /* determine the number of the Candidate Chromosomes based on the evolutionary Strategy */
        //    var candidateChromosomeCount = 
        //        Parameters.EvolutionaryStrategy switch
        //        {
        //            CGpEvolutionaryStrategy.MuPlusLambda => 
        //                Parameters.Mu + Parameters.Lambda,

        //            CGpEvolutionaryStrategy.Lambda => 
        //                Parameters.Lambda,

        //            _ => throw new InvalidOperationException(
        //                $"Error: the evolutionary strategy '{Parameters.EvolutionaryStrategy}' is not known.\nTerminating CGP-Library."
        //            )
        //        };

        //    /* initialise the candidate chromosomes */
        //    var candidateChromosomes = new CGpChromosome[candidateChromosomeCount];

        //    for (var i = 0; i < candidateChromosomeCount; i++) 
        //        candidateChromosomes[i] = CreateChromosome(originalChromosome);

        //    /* set fitness of the parents */
        //    for (var i = 0; i < Parameters.Mu; i++) 
        //        parentChromosomes[i].UpdateCost(DataSet);

        //    /* show the user what's going on */
        //    if (Parameters.UpdateFrequency != 0)
        //    {
        //        Console.WriteLine("\n-- Starting Parametric Optimization CGP --\n");
        //        Console.WriteLine("Gen\tcost");
        //    }

        //    /* for each generation */
        //    var gen = originalChromosome.Generation;

        //    for (var genIndex = 1; genIndex < generationCount; genIndex++)
        //    {
        //        gen = originalChromosome.Generation + genIndex;

        //        /* set fitness of the children of the population */
        //        for (var i = 0; i < Parameters.Lambda; i++)
        //            childChromosomes[i].UpdateCost(DataSet);

        //        /* get best chromosome */
        //        bestChromosome = GetBestChromosome(
        //            parentChromosomes, 
        //            childChromosomes
        //        );

        //        /* check termination conditions */
        //        if (bestChromosome.Cost <= Parameters.TargetCost)
        //        {
        //            if (Parameters.UpdateFrequency != 0)
        //                Console.WriteLine("{0}\t{1} - Solution Found\n", gen, bestChromosome.Cost);

        //            break;
        //        }

        //        /* display progress to the user at the update frequency specified */
        //        if (Parameters.UpdateFrequency != 0 && (gen % Parameters.UpdateFrequency == 0 || gen >= generationCount - 1))
        //            Console.WriteLine("{0}\t{1}\n", gen, bestChromosome.Cost);

        //        /*
        //            Set the chromosomes which will be used by the selection scheme
        //            dependant upon the evolutionary strategy. i.e. '+' all are used
        //            by the selection scheme, ',' only the children are.
        //        */
        //        if (Parameters.EvolutionaryStrategy == CGpEvolutionaryStrategy.MuPlusLambda)
        //        {
        //            /*
        //                Note: the children are placed before the parents to
        //                ensure 'new blood' is always selected over old if the
        //                fitness are equal.
        //            */
        //            for (var i = 0; i < candidateChromosomeCount; i++)
        //            {

        //                if (i < Parameters.Lambda)
        //                    childChromosomes[i].CopyToChromosome(candidateChromosomes[i]);
        //                else
        //                    parentChromosomes[i - Parameters.Lambda].CopyToChromosome(candidateChromosomes[i]);
        //            }
        //        }
        //        else if (Parameters.EvolutionaryStrategy == CGpEvolutionaryStrategy.Lambda)
        //        {
        //            for (var i = 0; i < candidateChromosomeCount; i++) 
        //                childChromosomes[i].CopyToChromosome(candidateChromosomes[i]);
        //        }

        //        /* select the parents from the candidate chromosomes */
        //        Parameters.SelectionScheme.Execute(
        //            parentChromosomes, 
        //            candidateChromosomes
        //        );

        //        /* create the children from the parents */
        //        Parameters.ReproductionScheme.Execute(
        //            parentChromosomes, 
        //            childChromosomes, 
        //            true
        //        );
        //    }

        //    /* deal with formatting for displaying progress */
        //    if (Parameters.UpdateFrequency != 0)
        //    {
        //        Console.WriteLine("\n");
        //    }

        //    /* copy the best chromosome */
        //    bestChromosome.Generation = gen;
        //    /*copyChromosome(chromosome, bestChromosome);*/

        //    return bestChromosome;
        //}

        public CGpChromosome RunCGp(int generationCount)
        {
            /* error checking */
            if (generationCount < 0)
                throw new InvalidOperationException($"Error: {generationCount} generations is invalid. The number of generations must be >= 0.\n Terminating CGP-Library.\n");

            if (DataSet is null)
                throw new InvalidOperationException("Data set not present");

            if (Parameters.InputSize != DataSet.InputSize)
                throw new InvalidOperationException($"Error: The number of inputs specified the dataSet ({DataSet.InputSize}) does not match the number of inputs specified the parameters ({Parameters.InputSize}).\n");

            if (Parameters.OutputSize != DataSet.OutputSize)
                throw new InvalidOperationException($"Error: The number of outputs specified the dataSet ({DataSet.OutputSize}) does not match the number of outputs specified the parameters ({Parameters.OutputSize}).\n");

            /* initialise parent chromosomes */
            var parentChromosomes = new CGpChromosome[Parameters.Mu];

            for (var i = 0; i < Parameters.Mu; i++) 
                parentChromosomes[i] = CreateChromosome();

            /* initialise children chromosomes */
            var childChromosomes = new CGpChromosome[Parameters.Lambda];

            for (var i = 0; i < Parameters.Lambda; i++)
                childChromosomes[i] = CreateChromosome();

            /* initialise best chromosome */
            var bestChromosome = CreateChromosome();

            /* determine the number of the Candidate Chromosomes based on the evolutionary Strategy */
            var candidateChromosomeCount = 
                Parameters.EvolutionaryStrategy switch
                {
                    CGpEvolutionaryStrategy.MuPlusLambda => 
                        Parameters.Mu + Parameters.Lambda,

                    CGpEvolutionaryStrategy.Lambda => 
                        Parameters.Lambda,

                    _ => throw new InvalidOperationException(
                        $"Error: the evolutionary strategy '{Parameters.EvolutionaryStrategy}' is not known.\nTerminating CGP-Library."
                    )
                };

            /* initialise the candidate chromosomes */
            var candidateChromosomes = new CGpChromosome[candidateChromosomeCount];

            for (var i = 0; i < candidateChromosomeCount; i++) 
                candidateChromosomes[i] = CreateChromosome();

            /* set fitness of the parents */
            for (var i = 0; i < Parameters.Mu; i++) 
                parentChromosomes[i].UpdateCost(DataSet);

            /* show the user what's going on */
            if (Parameters.UpdateFrequency != 0)
            {
                Console.WriteLine("\n-- Starting CGP --\n");
                Console.WriteLine("Gen\tfitness");
            }

            /* for each generation */
            int gen;
            for (gen = 0; gen < generationCount; gen++)
            {
                /* set fitness of the children of the population */
                for (var i = 0; i < Parameters.Lambda; i++)
                    childChromosomes[i].UpdateCost(DataSet);

                /* get best chromosome */
                bestChromosome = GetBestChromosome(
                    parentChromosomes, 
                    childChromosomes
                );

                /* check termination conditions */
                if (bestChromosome.Cost <= Parameters.TargetCost)
                {
                    if (Parameters.UpdateFrequency != 0)
                        Console.WriteLine("{0}\t{1} - Solution Found\n", gen, bestChromosome.Cost);

                    break;
                }

                /* display progress to the user at the update frequency specified */
                if (Parameters.UpdateFrequency != 0 && (gen % Parameters.UpdateFrequency == 0 || gen >= generationCount - 1))
                    Console.WriteLine("{0}\t{1}\n", gen, bestChromosome.Cost);

                /*
                    Set the chromosomes which will be used by the selection scheme
                    dependant upon the evolutionary strategy. i.e. '+' all are used
                    by the selection scheme, ',' only the children are.
                */
                if (Parameters.EvolutionaryStrategy == CGpEvolutionaryStrategy.MuPlusLambda)
                {
                    /*
                        Note: the children are placed before the parents to
                        ensure 'new blood' is always selected over old if the
                        fitness are equal.
                    */
                    for (var i = 0; i < candidateChromosomeCount; i++)
                    {

                        if (i < Parameters.Lambda)
                            childChromosomes[i].CopyToChromosome(candidateChromosomes[i]);
                        else
                            parentChromosomes[i - Parameters.Lambda].CopyToChromosome(candidateChromosomes[i]);
                    }
                }
                else if (Parameters.EvolutionaryStrategy == CGpEvolutionaryStrategy.Lambda)
                {
                    for (var i = 0; i < candidateChromosomeCount; i++) 
                        childChromosomes[i].CopyToChromosome(candidateChromosomes[i]);
                }

                /* select the parents from the candidate chromosomes */
                Parameters.SelectionScheme.Execute(
                    parentChromosomes, 
                    candidateChromosomes
                );

                /* create the children from the parents */
                Parameters.ReproductionScheme.Execute(
                    parentChromosomes, 
                    childChromosomes, 
                    false
                );
            }

            /* deal with formatting for displaying progress */
            if (Parameters.UpdateFrequency != 0)
            {
                Console.WriteLine("\n");
            }

            /* copy the best chromosome */
            bestChromosome.Generation = gen;
            /*copyChromosome(chromosome, bestChromosome);*/

            return bestChromosome;
        }

        public CGpResults RepeatCGp(int generationCount, int iterationCount)
        {
            var updateFrequency = Parameters.UpdateFrequency;
            
            /* set the update frequency to generational results */
            Parameters.UpdateFrequency = 0;

            var results = CGpResults.Create(iterationCount);

            Console.WriteLine("Run\tCost\t\tGenerations\tActive Nodes\n");

            /* for each run */
            for (var i = 0; i < iterationCount; i++)
            {
                /* run cgp */
                results[i] = RunCGp(generationCount);

                Console.WriteLine(
                    "{0}\t{1}\t{2}\t\t{3}\n", 
                    i, 
                    results[i].Cost, 
                    results[i].Generation, 
                    results[i].ActiveNodeCount
                );
            }

            Console.WriteLine("----------------------------------------------------\n");
            Console.WriteLine("MEAN\t{0}\t{1}\t{2}\n", results.GetAverageCost(), results.GetAverageGenerations(), results.GetAverageActiveNodes());
            Console.WriteLine("MEDIAN\t{0}\t{1}\t{2}\n", results.GetMedianCost(), results.GetMedianGenerations(), results.GetMedianActiveNodes());
            Console.WriteLine("----------------------------------------------------\n\n");

            /* restore the original value for the update frequency */
            Parameters.UpdateFrequency = updateFrequency;

            return results;
        }


        //internal static void SelectFittest(CGpChromosome[] parentChromosomes, CGpChromosome[] candidateChromosomes)
        //{
        //    candidateChromosomes.SortByCost();

        //    for (var i = 0; i < parentChromosomes.Length; i++)
        //        candidateChromosomes[i].CopyToChromosome(parentChromosomes[i]);
        //}

        //internal static void MutateRandomParent(CGpChromosome[] parents, CGpChromosome[] children, int numParents, int numChildren)
        //{
        //    /* for each child */
        //    for (var i = 0; i < numChildren; i++)
        //    {
        //        /* set child as clone of random parent */
        //        parents[CGpParameters.GetRandomIndex(numParents)].CopyToChromosome(children[i]);

        //        /* mutate newly cloned child */
        //        children[i].MutateChromosome();
        //    }
        //}

        //internal static double SupervisedLearning(CGpChromosome chromosome, CGpDataSet data)
        //{
        //    double error = 0;

        //    /* error checking */
        //    if (chromosome.InputSize != data.InputSize)
        //    {
        //        Console.WriteLine("Error: the number of chromosome inputs must match the number of inputs specified the dataSet.\n");
        //        Console.WriteLine("Terminating CGP-Library.\n");
        //        return 0;
        //    }

        //    if (chromosome.OutputSize != data.OutputSize)
        //    {
        //        Console.WriteLine("Error: the number of chromosome outputs must match the number of outputs specified the dataSet.\n");
        //        Console.WriteLine("Terminating CGP-Library.\n");
        //        return 0;
        //    }

        //    /* for each sample data */
        //    for (var i = 0 ; i < data.Count; i++)
        //    {
        //        /* calculate the chromosome outputs for the set of inputs  */
        //        chromosome.ExecuteChromosome(data.GetSampleInput(i));

        //        /* for each chromosome output */
        //        for (var j = 0; j < chromosome.OutputSize; j++)
        //        {

        //            error += Math.Abs(chromosome.GetChromosomeOutput(j) - data.GetSampleOutputItem(i, j));
        //        }
        //    }

        //    return error;
        //}
    }
}
