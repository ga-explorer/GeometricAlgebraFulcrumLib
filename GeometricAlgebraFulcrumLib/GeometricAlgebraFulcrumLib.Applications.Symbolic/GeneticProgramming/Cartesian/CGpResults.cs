using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian;

public record CGpResults :
    IReadOnlyList<CGpChromosome>
{
    public static CGpResults Create(int iterationCount)
    {
        return new CGpResults(iterationCount);
    }

    
    private readonly CGpChromosome[] _bestChromosomes;

    public CGpParameters Parameters 
        => _bestChromosomes[0].Parameters;

    public int Count 
        => _bestChromosomes.Length;
    
    public CGpChromosome this[int index]
    {
        get => CGpChromosome.CreateFromChromosome(_bestChromosomes[index]);
        set => _bestChromosomes[index] = value;
    }


    private CGpResults(int iterationCount)
    {
        if (iterationCount < 1)
            throw new ArgumentOutOfRangeException(nameof(iterationCount));

        _bestChromosomes = new CGpChromosome[iterationCount];
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

    public double GetAverageActiveNodes()
    {
        double avgActiveNodes = 0;

        for (var i = 0; i < Count; i++) 
            avgActiveNodes += _bestChromosomes[i].ActiveNodeCount;

        avgActiveNodes /= Count;

        return avgActiveNodes;
    }

    public double GetMedianActiveNodes()
    {
        var array = new int[Count];

        for (var i = 0; i < Count; i++) 
            array[i] = _bestChromosomes[i].ActiveNodeCount;

        var medActiveNodes = array.GetMedian();

        return medActiveNodes;
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


    public IEnumerator<CGpChromosome> GetEnumerator()
    {
        return ((IEnumerable<CGpChromosome>) _bestChromosomes).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}