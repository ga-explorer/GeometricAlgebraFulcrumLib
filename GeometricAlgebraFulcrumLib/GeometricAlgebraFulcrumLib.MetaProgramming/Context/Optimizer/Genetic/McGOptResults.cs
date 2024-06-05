using System.Collections;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic;

public record McGOptResults :
    IReadOnlyList<McGOptChromosome>
{
    public static McGOptResults Create(int iterationCount)
    {
        return new McGOptResults(iterationCount);
    }

    
    private readonly McGOptChromosome[] _bestChromosomes;

    public McGOptParameters Parameters 
        => _bestChromosomes[0].Parameters;

    public int Count 
        => _bestChromosomes.Length;
    
    public McGOptChromosome this[int index]
    {
        get => McGOptChromosome.CreateFromChromosome(_bestChromosomes[index]);
        set => _bestChromosomes[index] = value;
    }


    private McGOptResults(int iterationCount)
    {
        if (iterationCount < 1)
            throw new ArgumentOutOfRangeException(nameof(iterationCount));

        _bestChromosomes = new McGOptChromosome[iterationCount];
    }

    //public void SaveResults(string fileName)
    //{
    //    FILE fp = fopen(fileName, "w");

    //    if (fp == null)
    //    {
    //        Console.WriteLine("Warning: cannot open '%s' and so cannot save results to that file. Results not saved.\n", fileName);
    //        return;
    //    }

    //    fprintf(fp, "Run,Cost,Generations,Active Nodes\n");

    //    for (var i = 0; i < ChromosomeCount; i++)
    //    {

    //        var chromoTemp = GetChromosome(i);

    //        fprintf(fp, "%d,%f,%d,%d\n", i, chromoTemp.Cost, chromoTemp.Generation, chromoTemp.ActiveNodeCount);

    //        chromoTemp.FreeChromosome();
    //    }

    //    fclose(fp);
    //}


    public double GetAverageCost()
    {
        double avgFit = 0;
        
        for (var i = 0; i < Count; i++) 
            avgFit += _bestChromosomes[i].Cost;

        avgFit /= Count;

        return avgFit;
    }

    public double GetMedianCost()
    {
        var array = new double[Count];

        for (var i = 0; i < Count; i++) 
            array[i] = _bestChromosomes[i].Cost;

        var med = array.GetMedian();

        return med;
    }

    public double GetAverageGenerations()
    {
        double avgGens = 0;

        for (var i = 0; i < Count; i++)
            avgGens += _bestChromosomes[i].Generation;

        avgGens /= Count;

        return avgGens;
    }

    public double GetMedianGenerations()
    {
        var array = new int[Count];

        for (var i = 0; i < Count; i++) 
            array[i] = _bestChromosomes[i].Generation;

        var med = array.GetMedian();

        return med;
    }


    public IEnumerator<McGOptChromosome> GetEnumerator()
    {
        return ((IEnumerable<McGOptChromosome>) _bestChromosomes).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}