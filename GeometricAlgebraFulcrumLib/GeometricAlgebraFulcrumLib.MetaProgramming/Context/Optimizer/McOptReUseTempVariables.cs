namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer;

internal sealed class McOptReUseTempVariables : 
    MetaContextProcessorBase
{
    internal static void Process(MetaContext context)
    {
        var processor = new McOptReUseTempVariables(context);

        processor.BeginProcessing();
    }


    private McOptReUseTempVariables(MetaContext context)
        : base(context)
    {
            
    }


    private void BeginProcessingWithNoReUse()
    {
        var nameIndex = 0;
        var tempVarList = 
            Context.GetIntermediateVariables();
        
        foreach (var tempVar in tempVarList)
        {
            tempVar.SetReuseInfo(false, nameIndex);

            nameIndex++;
        }
    }

    //private void BeginProcessingWithReUse()
    //{
    //    var finalNameIndexList = new List<int>();
        
    //    //Go through all temp variables
    //    foreach (var tempVar in Context.GetIntermediateVariables())
    //    {
    //        var nameIndex = 0;
    //        var isReused = false;

    //        //Find the first available index for re-use; if any
    //        for (var i = 0; i < finalNameIndexList.Count; i++)
    //        {
    //            if (finalNameIndexList[i] > tempVar.ComputationOrder)
    //                continue;

    //            nameIndex = i + 1;
    //            isReused = true;
    //            break;
    //        }

    //        //Find the computation order of the last variable using this temp in its RHS
    //        var lastUseEvalOrder = tempVar.LastDependingVariableComputationOrder;

    //        if (nameIndex > 0)
    //            //Re-use the found name index and update the re-use list
    //            finalNameIndexList[nameIndex - 1] = lastUseEvalOrder;

    //        else
    //        {
    //            //No temp can be re-used. 
    //            finalNameIndexList.Add(lastUseEvalOrder);

    //            //This LHS variable will have a new use index
    //            nameIndex = finalNameIndexList.Count;
    //        }

    //        //Modify the reuse information of this temp variable
    //        tempVar.SetReuseInfo(isReused, nameIndex - 1);
    //    }
    //}

    protected override void BeginProcessing()
    {
        BeginProcessingWithNoReUse();

        //if (Context.ContextOptions.ReUseIntermediateVariables)
        //    BeginProcessingWithReUse();
        //else
        //    BeginProcessingWithNoReUse();
    }
}