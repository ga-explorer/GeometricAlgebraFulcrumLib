namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Mutation;

public sealed class CGpSingleMutation :
    CGpMutation
{
    private static void SingleMutation(CGpChromosome chromosome)
    {
        var cgpParameters = chromosome.Parameters;

        var mutatedActive = false;

        /* get the number of each type of gene */
        var numFunctionGenes = cgpParameters.MaxNodeCount;
        var numInputGenes = cgpParameters.MaxNodeCount * cgpParameters.MaxArity;
        var numOutputGenes = cgpParameters.OutputSize;

        /* set the total number of chromosome genes */
        var numGenes = numFunctionGenes + numInputGenes + numOutputGenes;

        /* while active gene not mutated */
        while (mutatedActive)
        {
            /* select a random gene */
            var geneToMutate = CGpParameters.GetRandomIndex(numGenes);

            /* mutate function gene */
            int nodeIndex;
            string previousGeneValue;
            string newGeneValue;
            if (geneToMutate < numFunctionGenes)
            {

                nodeIndex = geneToMutate;

                previousGeneValue = chromosome.Nodes[nodeIndex].Function.Name;

                var node = chromosome.Nodes[nodeIndex];

                node.SetRandomFunction();

                //node.FunctionIndex = chromosome.Parameters.GetRandomFunctionIndex();

                newGeneValue = node.Function.Name;

                if (previousGeneValue != newGeneValue && node.IsActive)
                {
                    mutatedActive = true;
                }

            }

            /* mutate node input gene */
            else if (geneToMutate < numFunctionGenes + numInputGenes)
            {

                nodeIndex = (geneToMutate - numFunctionGenes) / chromosome.Parameters.MaxArity;
                var nodeInputIndex = (geneToMutate - numFunctionGenes) % chromosome.Parameters.MaxArity;

                previousGeneValue = chromosome.Nodes[nodeIndex].InputIndices[nodeInputIndex].ToString();

                chromosome.Nodes[nodeIndex].SetRandomInputIndex(nodeInputIndex);
                //chromosome.Nodes[nodeIndex].InputIndices[nodeInputIndex] = cgpParameters.GetRandomNodeInputIndex(nodeIndex);
                
                newGeneValue = chromosome.Nodes[nodeIndex].InputIndices[nodeInputIndex].ToString();

                if (previousGeneValue != newGeneValue && chromosome.Nodes[nodeIndex].IsActive)
                {
                    mutatedActive = true;
                }
            }

            /* mutate output gene */
            else
            {
                nodeIndex = geneToMutate - numFunctionGenes - numInputGenes;

                previousGeneValue = chromosome.OutputNodeIndices[nodeIndex].ToString();

                chromosome.SetOutputNodeIndexToRandom(nodeIndex);

                newGeneValue = chromosome.OutputNodeIndices[nodeIndex].ToString();

                if (previousGeneValue != newGeneValue)
                {
                    mutatedActive = true;
                }
            }
        }
    }

    public override string Name 
        => "single mutation";


    internal CGpSingleMutation()
    {
    }


    public override void ApplyMutation(CGpChromosome chromosome, bool parametric)
    {
        SingleMutation(chromosome);
    }
}