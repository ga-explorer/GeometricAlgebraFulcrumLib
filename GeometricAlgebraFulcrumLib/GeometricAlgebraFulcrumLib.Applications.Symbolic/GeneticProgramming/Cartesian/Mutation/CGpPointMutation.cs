namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian.Mutation;

public sealed class CGpPointMutation :
    CGpMutation
{
    private static void PointMutation(CGpChromosome chromosome)
    {
        var cgpParameters = chromosome.Parameters;

        /* get the number of each type of gene */
        var numFunctionGenes = cgpParameters.MaxNodeCount;
        var numInputGenes = cgpParameters.MaxNodeCount * cgpParameters.MaxArity;
        var numOutputGenes = cgpParameters.OutputSize;

        /* set the total number of chromosome genes */
        var numGenes = numFunctionGenes + numInputGenes + numOutputGenes;

        /* calculate the number of genes to mutate */
        var numGenesToMutate = (int)Math.Round(numGenes * cgpParameters.MutationRate);

        /* for the number of genes to mutate */
        for (var i = 0; i < numGenesToMutate; i++)
        {
            /* select a random gene */
            var geneToMutate = CGpParameters.GetRandomIndex(numGenes);

            /* mutate function gene */
            int nodeIndex;
            if (geneToMutate < numFunctionGenes)
            {
                nodeIndex = geneToMutate;

                var node = chromosome.Nodes[nodeIndex];

                node.SetRandomFunction();
            }
            /* mutate node input gene */
            else if (geneToMutate < numFunctionGenes + numInputGenes)
            {
                nodeIndex = (geneToMutate - numFunctionGenes) / chromosome.Parameters.MaxArity;
                
                var nodeInputIndex = (geneToMutate - numFunctionGenes) % chromosome.Parameters.MaxArity;

                chromosome.Nodes[nodeIndex].SetRandomInputIndex(nodeInputIndex);

                //chromosome.Nodes[nodeIndex].InputIndices[nodeInputIndex] = cgpParameters.GetRandomNodeInputIndex(nodeIndex);
            }
            /* mutate output gene */
            else
            {
                nodeIndex = geneToMutate - numFunctionGenes - numInputGenes;
                
                chromosome.SetOutputNodeIndexToRandom(nodeIndex);
            }
        }
    }

    //private static void FpPointMutation(CGpChromosome chromosome)
    //{
    //    var cgpParameters = chromosome.Parameters;

    //    var fpList = 
    //        chromosome.ParametricConstants.ToImmutableArray();

    //    var numGenes = fpList.Length;
    //    var numGenesToMutate = (int)Math.Round(numGenes * cgpParameters.MutationRate);

    //    var fpIndexList = 
    //        CGpParameters.GetRandomUniqueIndexList(
    //            numGenesToMutate, 
    //            numGenes
    //        );

    //    foreach (var fpIndex in fpIndexList)
    //        fpList[fpIndex].SetRandomValue();
    //}


    public bool Ann { get; }

    public override string Name 
        => "point mutation" + (Ann ? "" : " (ANN)");
    

    internal CGpPointMutation(bool ann)
    {
        Ann = ann;
    }


    public override void ApplyMutation(CGpChromosome chromosome, bool parametric)
    {
        //if (parametric)
        //{
        //    FpPointMutation(chromosome);
        //}
        //else
        //{
        //    PointMutation(chromosome);
        //}

        PointMutation(chromosome);
    }
}