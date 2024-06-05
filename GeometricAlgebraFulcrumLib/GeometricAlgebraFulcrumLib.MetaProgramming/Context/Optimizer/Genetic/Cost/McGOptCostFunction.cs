namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Cost
{
    public abstract class McGOptCostFunction
    {
        public static McGOptIntermediateVariablesCountCostFunction IntermediateVariablesCount { get; }
            = new McGOptIntermediateVariablesCountCostFunction();
        
        public static McGOptComputationsCountCostFunction ComputationsCount { get; }
            = new McGOptComputationsCountCostFunction();


        public abstract string Name { get; }

        public abstract double ComputeCost(MetaContext context);
    }
}
