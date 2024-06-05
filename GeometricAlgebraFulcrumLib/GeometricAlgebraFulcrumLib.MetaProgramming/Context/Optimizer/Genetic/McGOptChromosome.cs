using System.Text;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;

public class McGOptChromosome : 
    IEquatable<McGOptChromosome>
{
    public static McGOptChromosome Create(MetaContextGeneticOptimizer geneticOptimizer, MetaContext context)
    {
        return new McGOptChromosome(geneticOptimizer, context);
    }

    public static McGOptChromosome CreateFromChromosome(McGOptChromosome chromosome)
    {
        var chromosomeNew = new McGOptChromosome(
            chromosome.GeneticOptimizer, 
            chromosome.Context.GetContextCopy()
        )
        {
            Cost = chromosome.Cost,
            Generation = chromosome.Generation
        };

        return chromosomeNew;
    }

    
    public static bool operator ==(McGOptChromosome left, McGOptChromosome right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(McGOptChromosome left, McGOptChromosome right)
    {
        return !Equals(left, right);
    }


    public MetaContextGeneticOptimizer GeneticOptimizer { get; }

    public McGOptParameters Parameters 
        => GeneticOptimizer.Parameters;
    
    public MetaContext Context { get; private set; }

    public int Generation { get; internal set; }

    public double Cost { get; private set; }


    private McGOptChromosome(MetaContextGeneticOptimizer geneticOptimizer, MetaContext context)
    {
        GeneticOptimizer = geneticOptimizer;
        Context = context;

        Cost = double.PositiveInfinity;
    }

    
    public void MutateChromosome()
    {
        Context = Parameters.MutationType.ApplyMutation(Parameters, Context);
    }

    public void UpdateCost()
    {
        Cost = Parameters.CostFunction.ComputeCost(Context);

        //Console.WriteLine($"Cost = {Cost:G}");
    }

    public void CopyToChromosome(McGOptChromosome dstChromosome)
    {
        dstChromosome.Context = Context.GetContextCopy();
        dstChromosome.Cost = Cost;
        dstChromosome.Generation = Generation;
    }


    public bool Equals(McGOptChromosome other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Context.ToString().Equals(other.ToString());
    }
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((McGOptChromosome)obj);
    }

    //public override int GetHashCode()
    //{
    //    return Context.ToString().GetHashCode();
    //}

    public string GetGraphVizCode()
    {
        var contextCodeComposer = Context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        return contextCodeComposer.GenerateGraphVizCode();
    }

    public bool TrySaveCSharpCode()
    {
        if (string.IsNullOrEmpty(Parameters.CodeFilePath) || !Directory.Exists(Parameters.CodeFilePath))
            return false;

        var contextCodeComposer = Context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

        var cSharpCode = contextCodeComposer.Generate();
        var filePath = Path.Combine(Parameters.CodeFilePath, $"CSharpCode.cs");

        File.WriteAllText(filePath, cSharpCode, Encoding.UTF8);

        return true;
    }

    public override string ToString()
    {
        return GetGraphVizCode();
    }
}