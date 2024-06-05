namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic.Mutation
{
    public abstract class McGOptMutation
    {
        public static McGOptSimpleMutation Simple { get; }
            = new McGOptSimpleMutation();


        public abstract string Name { get; }

        public abstract MetaContext ApplyMutation(McGOptParameters parameters, MetaContext context);


        public override string ToString()
        {
            return Name;
        }
    }
}
