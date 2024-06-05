namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Cost;

public class McGOptIntermediateVariablesCountCostFunction :
    McGOptCostFunction
{
    public override string Name 
        => "Intermediate Variables Count Cost Function";


    internal McGOptIntermediateVariablesCountCostFunction() 
    {
    }


    public override double ComputeCost(MetaContext context)
    {
        return context.GetIntermediateVariables().Count();
    }

    
}