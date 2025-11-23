using System.Numerics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Samples.Utilities.Structures.IndexSets;

public static class Samples1
{
    public static void Example1()
    {
        const int size = 4;
        const int steps = 256;
        var randGen = new System.Random(10);

        //var bitPatternIn = randGen.GetIndexSetOfSize(size, 7).ToInt32();
        var bitPatternIn = IndexSet.CreateDense(size).ToUInt64();
        
        for (var i = 0; i < steps; i++)
        {
            // Input index set
            var indexSetIn = bitPatternIn.GetSetBitPositions().ToArray();

            if (indexSetIn[^1] > 60)
                break;

            // Correct next index set
            var indexSetNextCorrect = GetNextRefImp(bitPatternIn).GetSetBitPositions().ToArray();

            // Computed next index set
            VirtualBitPatterns.TryGetNextIndices(indexSetIn, out var indexSetNextComputed);

            var is1Text = $"<{string.Join(", ", indexSetIn)}>";
            var is2Text = $"<{string.Join(", ", indexSetNextComputed)}>";
            var isRefText = $"<{string.Join(", ", indexSetNextCorrect)}>";

            var isSameText = is2Text == isRefText ? "correct" : $"incorrect, should be {isRefText}";
            
            if (is2Text != isRefText)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ResetColor();

            Console.WriteLine($"Next of {is1Text} => {is2Text}; ({isSameText})");

            bitPatternIn = UInt64BitUtils.PositionsToPattern(indexSetNextCorrect);
        }

        return;
        
        ulong GetNextRefImp(ulong pattern)
        {
            var k = BitOperations.PopCount(pattern);
            var pattern2 = pattern + 1;

            while (BitOperations.PopCount(pattern2) != k)
                pattern2++;

            return pattern2;
        }
    }

    public static void GetNextSameSizeExample()
    {
        const int size = 10;
        const int steps = 256;
        var randGen = new System.Random(10);

        //var bitPatternIn = randGen.GetIndexSetOfSize(size, 7).ToInt32();
        var bitPatternIn = IndexSet.CreateDense(size).ToUInt64();
        
        for (var i = 0; i < steps; i++)
        {
            // Input index set
            var indexSetIn = 
                IndexSet.CreateFromUInt64Pattern(bitPatternIn);

            if (indexSetIn.LastIndex > 60)
                break;

            // Correct next index set
            var indexSetNextCorrect = 
                IndexSet.CreateFromUInt64Pattern(
                    GetNextSameSizeRefImp(bitPatternIn)
                );

            // Computed next index set
            var indexSetNextComputed =
                indexSetIn.GetNextSameSize();

            var is1Text = $"<{string.Join(", ", indexSetIn)}>";
            var is2Text = $"<{string.Join(", ", indexSetNextComputed)}>";
            var isRefText = $"<{string.Join(", ", indexSetNextCorrect)}>";

            var isSameText = is2Text == isRefText ? "correct" : $"incorrect, should be {isRefText}";
            
            if (is2Text != isRefText)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ResetColor();

            Console.WriteLine($"Next of {is1Text} => {is2Text}; ({isSameText})");

            bitPatternIn = UInt64BitUtils.PositionsToPattern(indexSetNextCorrect);
        }

        return;
        
        ulong GetNextSameSizeRefImp(ulong pattern)
        {
            var k = BitOperations.PopCount(pattern);
            var pattern2 = pattern + 1;

            while (BitOperations.PopCount(pattern2) != k)
                pattern2++;

            return pattern2;
        }
    }
    
    public static void GetPrevSameSizeExample()
    {
        
        const int size = 3;
        const int steps = 256;
        var randGen = new System.Random(10);
        // TODO: Test for array index set
        //var bitPatternIn = randGen.GetIndexSetOfSize(size, 7).ToInt32();
        var bitPatternIn = IndexSet.CreateDense(50 - size + 1, size).ToUInt128();
        
        for (var i = 0; i < steps; i++)
        {
            // Input index set
            var indexSetIn = 
                IndexSet.CreateFromUInt128Pattern(bitPatternIn);

            if (indexSetIn.LastIndex > 126)
                break;

            // Correct next index set
            var indexSetNextCorrect = 
                IndexSet.CreateFromUInt128Pattern(
                    GetPrevSameSizeRefImp(bitPatternIn)
                );

            // Computed next index set
            var indexSetNextComputed =
                indexSetIn.GetPrevSameSize();

            var is1Text = $"<{string.Join(", ", indexSetIn)}>";
            var is2Text = $"<{string.Join(", ", indexSetNextComputed)}>";
            var isRefText = $"<{string.Join(", ", indexSetNextCorrect)}>";

            var isSameText = is2Text == isRefText ? "correct" : $"incorrect, should be {isRefText}";
            
            if (is2Text != isRefText)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ResetColor();

            Console.WriteLine($"Prev of {is1Text} => {is2Text}; ({isSameText})");

            bitPatternIn = UInt64BitUtils.PositionsToPattern(indexSetNextCorrect);
        }

        return;
        
        UInt128 GetPrevSameSizeRefImp(UInt128 pattern)
        {
            var k = UInt128.PopCount(pattern);
            var pattern2 = pattern - 1;

            while (UInt128.PopCount(pattern2) != k)
                pattern2--;

            return pattern2;
        }
    }

    public static void Example3()
    {
        // Quick self-checks against provided examples.
        CheckPrev([0, 1, 5], [2, 3, 4]);
        CheckPrev([0, 2, 5], [0, 1, 5]);
        CheckPrev([1, 2, 5], [0, 2, 5]);
        CheckPrev([0, 3, 5], [1, 2, 5]);
        CheckPrev([0, 1, 6], [3, 4, 5]);
        CheckPrev([0, 2, 7], [0, 1, 7]);
        CheckPrev([6, 7, 8], [5, 7, 8]);

        // Demonstrate next-higher as well (useful for iteration): [3,4,5] -> [0,1,6]
        var q = new[] { 3, 4, 5 };
        VirtualBitPatterns.TryGetNextIndices(q, out var next);
        Console.WriteLine($"Next of <{string.Join(", ", q)}> => <{string.Join(", ", next)}>");
    }

    private static void CheckPrev(int[] q, int[] expected)
    {
        var ok = VirtualBitPatterns.TryGetPreviousIndices(q, out var p);
        
        if (!ok) 
            throw new Exception("Expected previous to exist.");

        if (!p.SequenceEqual(expected))
            throw new Exception($"Prev(<{string.Join(", ", q)}>) = <{string.Join(", ", p)}>, expected <{string.Join(", ", expected)}>");
        
        Console.WriteLine($"Prev(<{string.Join(", ", q)}>) = <{string.Join(", ", p)}>");
    }
}