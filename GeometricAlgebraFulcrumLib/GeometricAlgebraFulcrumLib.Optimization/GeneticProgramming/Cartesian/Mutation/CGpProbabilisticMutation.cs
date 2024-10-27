namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian.Mutation;

public sealed class CGpProbabilisticMutation :
    CGpMutation
{
    //private static void FpProbabilisticMutationAllNodes(CGpChromosome chromosome)
    //{
    //    var cgpParameters = chromosome.Parameters;

    //    // for every parameter in the node's function
    //    foreach (var parameter in chromosome.NodeFunctionParameters)
    //    {
    //        // mutate the function parameter
    //        if (cgpParameters.CanMutate())
    //            parameter.SetRandomValue();
    //    }
    //}
    
    //private static void FpProbabilisticMutationActiveNodes(CGpChromosome chromosome)
    //{
    //    var cgpParameters = chromosome.Parameters;

    //    // for every parameter in the node's function
    //    foreach (var parameter in chromosome.ActiveNodeFunctionParameters)
    //    {
    //        // mutate the function parameter
    //        if (cgpParameters.CanMutate())
    //            parameter.SetRandomValue();
    //    }
    //}

    private static void ProbabilisticMutationAllNodes(CGpChromosome chromosome)
    {
        var cgpParameters = chromosome.Parameters;

        for (var nodeIndex = 0; nodeIndex < chromosome.NodeCount; nodeIndex++)
        {
            var node = chromosome.Nodes[nodeIndex];

            // mutate the function gene
            if (cgpParameters.CanMutate()) 
                node.SetRandomFunction();

            // for every input to each chromosome
            for (var j = 0; j < cgpParameters.MaxArity; j++)
            {
                // mutate the node input
                if (cgpParameters.CanMutate()) 
                    node.SetRandomInputIndex(j);

                // mutate the node input weight
                if (node.Weights[j].IsParametric && cgpParameters.CanMutate()) 
                    node.Weights[j].SetRandomValue();
            }
        }

        // for every chromosome output
        for (var i = 0; i < cgpParameters.OutputSize; i++)
        {
            // mutate the chromosome output
            if (cgpParameters.CanMutate()) 
                chromosome.SetOutputNodeIndexToRandom(i);
        }
    }

    private static void ProbabilisticMutationActiveNodes(CGpChromosome chromosome)
    {
        var cgpParameters = chromosome.Parameters;

        for (var i = 0; i < chromosome.ActiveNodeCount; i++)
        {
            var nodeIndex = chromosome.ActiveNodeIndices[i];
            var node = chromosome.Nodes[nodeIndex];

            // mutate the function gene
            if (cgpParameters.CanMutate()) 
                node.SetRandomFunction();

            // for every input to each chromosome
            for (var j = 0; j < node.ActualArity; j++)
            {
                // mutate the node input
                if (cgpParameters.CanMutate())
                    node.SetRandomInputIndex(j);
                
                // mutate the node input weight
                if (node.Weights[j].IsParametric && cgpParameters.CanMutate()) 
                    node.Weights[j].SetRandomValue();
            }
        }

        // for every chromosome output
        for (var i = 0; i < cgpParameters.OutputSize; i++)
        {
            // mutate the chromosome output
            if (cgpParameters.CanMutate()) 
                chromosome.SetOutputNodeIndexToRandom(i);
        }
    }


    public bool ActiveNodesOnly { get; }

    public override string Name 
        => "probabilistic mutation " + (ActiveNodesOnly ? "(active nodes only)" : "(all nodes)");
    

    internal CGpProbabilisticMutation(bool activeNodesOnly)
    {
        ActiveNodesOnly = activeNodesOnly;
    }


    public override void ApplyMutation(CGpChromosome chromosome, bool parametric)
    {
        if (ActiveNodesOnly)
            ProbabilisticMutationActiveNodes(chromosome);
        else
            ProbabilisticMutationAllNodes(chromosome);
    }
}