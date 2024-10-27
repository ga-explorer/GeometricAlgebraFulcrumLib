using System.Collections;
using GeneticSharp;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Optimizer.Genetic2;

/// <summary>
/// Bit Array chromosome with binary values (0 and 1).
/// </summary>
public sealed class McOptGaChromosome : 
    BinaryChromosomeBase
{
    private readonly BitArray _bitArray;

    public int BitCount 
        => _bitArray.Count;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:McOptGaChromosome"/> class.
    /// </summary>
    /// <param name="bitCount"></param>
    public McOptGaChromosome(int bitCount) 
        : base(bitCount)
    {
        var maxVarCount = 2d;

        _bitArray = new BitArray(
            bitCount.MapRange(
                i => RandomizationProvider.Current.GetDouble(0, 1) < maxVarCount / (double)bitCount
            ).ToArray()
        );

        CreateGenes();
    }

    /// <summary>
    /// Generates the gene.
    /// </summary>
    /// <returns>The gene.</returns>
    /// <param name="geneIndex">Gene index.</param>
    public override Gene GenerateGene(int geneIndex)
    {
        var value = _bitArray[geneIndex];

        return new Gene(value);
    }

    /// <summary>
    /// Creates the new.
    /// </summary>
    /// <returns>The new.</returns>
    public override IChromosome CreateNew()
    {
        return new McOptGaChromosome(BitCount);
    }

    ///// <summary>
    ///// Converts the chromosome to its integer representation.
    ///// </summary>
    ///// <returns>The integer.</returns>
    //public int ToInteger()
    //{
    //    var array = new int[1];
    //    var genes = GetGenes().Select(g => (bool)g.Value).ToArray();
    //    var bitArray = new BitArray(genes);
    //    bitArray.CopyTo(array, 0);

    //    return array[0];
    //}

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:GeneticSharp.Domain.Chromosomes.FloatingPointChromosome"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:GeneticSharp.Domain.Chromosomes.FloatingPointChromosome"/>.</returns>
    public override string ToString()
    {
        return GetGenes()
            .SelectIndexWhere(g => (bool)g.Value)
            .Select(i => i.ToString())
            .Concatenate(", ", "[", "]");
    }

    /// <summary>
    /// Flips the gene.
    /// </summary>
    /// <remarks>>
    /// If gene's value is 0, it will flip to 1 and vice-versa.</remarks>
    /// <param name="index">The gene index.</param>
    public override void FlipGene(int index)
    {
        var value = (bool)GetGene(index).Value;

        ReplaceGene(index, new Gene(!value));
    }
}