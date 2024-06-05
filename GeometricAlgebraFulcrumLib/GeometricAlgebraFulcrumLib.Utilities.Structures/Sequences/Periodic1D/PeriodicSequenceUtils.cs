using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

public static class PeriodicSequenceUtils
{
    public static PSeqConstant1D<T> CreateConstantSequence<T>(T value, int count)
    {
        return new PSeqConstant1D<T>(count, value);
    }

    public static PSeqLinearDouble1D CreateLinearDoubleSequence(int count)
    {
        return new PSeqLinearDouble1D(count);
    }

    public static PSeqLinearDouble1D CreateLinearDoubleSequence(double valueLimit1, double valueLimit2, int count)
    {
        return new PSeqLinearDouble1D(valueLimit1, valueLimit2, count);
    }

    public static PSeqLinearDouble1D CreateLinearDoubleSequence(double valueLimit1, double valueLimit2, int count, int skipValues1, int skipValues2)
    {
        return new PSeqLinearDouble1D(valueLimit1, valueLimit2, count, skipValues1, skipValues2);
    }

    public static PSeqArray1D<T> CreateStoredSequence<T>(params T[] data)
    {
        return new PSeqArray1D<T>(data);
    }

    public static PSeqArray1D<T> CreateStoredSequence<T>(this IEnumerable<T> data)
    {
        return new PSeqArray1D<T>(data);
    }

    public static PSeqPartial1D<T> CreateRangeSequence<T>(this IPeriodicSequence1D<T> baseSequence, int index1, int index2)
    {
        return new PSeqPartial1D<T>(baseSequence, index1, index2);
    }

    public static PSeqReverse1D<T> CreateReverseSequence<T>(this IPeriodicSequence1D<T> baseSequence)
    {
        return new PSeqReverse1D<T>(baseSequence);
    }

    public static PSeqJoined1D<T> CreateChainedSequence<T>(params IPeriodicSequence1D<T>[] sequenceArray)
    {
        return new PSeqJoined1D<T>(sequenceArray);
    }

    public static PSeqJoined1D<T> CreateChainedSequence<T>(this IEnumerable<IPeriodicSequence1D<T>> sequenceList)
    {
        return new PSeqJoined1D<T>(sequenceList);
    }

    public static PSeqPermuted1D<T> CreatePermutedSequence<T>(this IPeriodicSequence1D<T> baseSequence, IndexMapPermutation1D permutation)
    {
        return new PSeqPermuted1D<T>(baseSequence, permutation);
    }

    public static PSeqPermuted1D<T> CreatePermutedSequenceWithValidation<T>(this IPeriodicSequence1D<T> baseSequence, params int[] permutationValues)
    {
        return new PSeqPermuted1D<T>(
            baseSequence, 
            IndexMapPermutation1D.CreateWithValidation(permutationValues)
        );
    }

    public static PSeqPermuted1D<T> CreatePermutedSequenceWithoutValidation<T>(this IPeriodicSequence1D<T> baseSequence, params int[] permutationValues)
    {
        return new PSeqPermuted1D<T>(
            baseSequence, 
            IndexMapPermutation1D.CreateWithoutValidation(permutationValues)
        );
    }

    public static PSeqPermuted1D<T> CreateRandomPermutedSequence<T>(this IPeriodicSequence1D<T> baseSequence)
    {
        return new PSeqPermuted1D<T>(
            baseSequence, 
            IndexMapPermutation1D.CreateRandom(baseSequence.Count)
        );
    }

    public static PSeqPermuted1D<T> CreateRandomPermutedSequence<T>(this IPeriodicSequence1D<T> baseSequence, int seed)
    {
        return new PSeqPermuted1D<T>(
            baseSequence, 
            IndexMapPermutation1D.CreateRandom(baseSequence.Count, seed)
        );
    }

    public static PSeqMultiplexed1D<T> CreateMultiplexedSequence<T>(this IReadOnlyList<IPeriodicSequence1D<T>> baseSequenceList, params int[] sequenceSelectionArray)
    {
        return new PSeqMultiplexed1D<T>(
            baseSequenceList,
            sequenceSelectionArray
        );
    }

    public static PSeqMultiplexed1D<T> CreateMultiplexedSequence<T>(this IReadOnlyList<IPeriodicSequence1D<T>> baseSequenceList, IReadOnlyList<int> sequenceSelectionList)
    {
        return new PSeqMultiplexed1D<T>(
            baseSequenceList,
            sequenceSelectionList
        );
    }

    public static IEnumerable<Pair<T>> GetValuePairs<T>(this IPeriodicSequence1D<T> sequence)
    {
        var index = 0;
        var stepsLeft = sequence.Count - 1;
        var indexPair = new Pair<T>(sequence[0], sequence[0]);

        while (stepsLeft > 0)
        {
            yield return indexPair.NextPair(sequence[++index]);

            stepsLeft--;
        }
    }


}