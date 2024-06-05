namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Cost;

public class McGOptComputationsCountCostFunction :
    McGOptCostFunction
{
    public override string Name 
        => "Computations Count Cost Function";


    internal McGOptComputationsCountCostFunction() 
    {
    }


    public override double ComputeCost(MetaContext context)
    {
        return context.ComputationsCount;
    }
}