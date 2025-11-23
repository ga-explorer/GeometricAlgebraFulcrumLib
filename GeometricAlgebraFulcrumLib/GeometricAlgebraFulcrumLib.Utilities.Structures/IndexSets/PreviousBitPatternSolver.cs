//using System.Diagnostics;
//using System.Numerics;

//namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

//public class PreviousBitPatternSolver
//{
//    /// <summary>
//    /// Finds the previous closest virtual bit pattern with the same number of set bits.
//    /// Uses pure array operations without bit manipulation shortcuts.
//    /// </summary>
//    /// <param name="indexArray">Sorted array of unique integers representing set bit positions</param>
//    /// <returns>Array representing the previous bit pattern with same number of set bits</returns>
//    // Reference implementation using proper bit manipulation
//    public static int[] GetPreviousBitPatternReference(int[] indexArray)
//    {
//        if (indexArray == null || indexArray.Length == 0)
//            throw new ArgumentException("Input array cannot be null or empty");
        
//        var n = indexArray.Length;
        
//        // Convert bit positions to numerical value
//        ulong currentValue = 0;
//        foreach (var pos in indexArray)
//        {
//            currentValue |= 1UL << pos;
//        }
        
//        // Find the previous number with the same popcount (number of set bits)
//        var previousValue = GetPreviousBitPattern_BitManipulation(currentValue);
        
//        // Convert back to bit positions
//        var result = new int[n];
//        var index = 0;
//        for (var i = 0; i < 64 && index < n; i++)
//        {
//            if ((previousValue & (1UL << i)) != 0)
//            {
//                result[index++] = i;
//            }
//        }
        
//        return result;
//    }
    
//    // Pure array-based implementation matching bit manipulation logic exactly
//    public static int[] GetPreviousBitPattern(int[] indexArray)
//    {
//        if (indexArray == null || indexArray.Length == 0)
//            throw new ArgumentException("Input array cannot be null or empty");
        
//        var k = indexArray.Length;
        
//        // Create a boolean array to represent which positions are set
//        var maxPos = indexArray.Max();
//        var positions = new bool[maxPos + 1];
//        foreach (var pos in indexArray)
//        {
//            positions[pos] = true;
//        }
        
//        // Find the rightmost set position that can be moved left (has gap to its left)
//        var rightmostMovable = -1;
        
//        for (var i = 1; i <= maxPos; i++)
//        {
//            // position i is set and position i-1 is unset
//            if (!positions[i] || positions[i - 1]) continue; 
            
//            // Take the first (rightmost) one we find
//            rightmostMovable = i;
//            break; 
//        }
        
//        if (rightmostMovable == -1)
//        {
//            // No movable position found - return max combination ending at maxPos - 1
//            var result = new int[k];

//            for (var i = 0; i < k; i++) 
//                result[i] = maxPos - 1 - (k - 1 - i);

//            return result;
//        }
        
//        // Count set positions to the right of rightmostMovable
//        var setBitsToRight = 0;
//        for (var i = 0; i < rightmostMovable; i++)
//            if (positions[i]) setBitsToRight++;
        
//        // Clear all positions at and to the right of rightmostMovable
//        for (var i = 0; i <= rightmostMovable; i++) 
//            positions[i] = false;
        
//        // Set position at rightmostMovable - 1
//        positions[rightmostMovable - 1] = true;
        
//        // Set the remaining setBitsToRight positions at their highest consecutive positions
//        // to the right of the moved position
//        for (var i = 0; i < setBitsToRight; i++) 
//            positions[rightmostMovable - 2 - i] = true;
        
//        // Convert back to array
//        var finalResult = new int[k];
//        var index = 0;
//        for (var i = 0; i <= maxPos && index < k; i++)
//            if (positions[i]) finalResult[index++] = i;
        
//        return finalResult;
//    }
    
//    // Correct previous number with same pop count algorithm
//    private static ulong GetPreviousBitPattern_BitManipulation(ulong num)
//    {
//        if (num == 0) return 0;
        
//        // Find the rightmost set bit that can be moved left (has a gap to its left)
//        var rightmostMovableBit = -1;
        
//        for (var i = 1; i < 64; i++) // Search from low to high
//        {
//            if ((num & (1UL << i)) != 0) // bit i is set
//            {
//                // Check if there's a gap (unset bit) to the left of position i
//                if ((num & (1UL << (i - 1))) == 0) // immediate left bit is 0
//                {
//                    rightmostMovableBit = i;
//                    break; // Take the first (rightmost) one we find
//                }
//            }
//        }
        
//        if (rightmostMovableBit == -1)
//        {
//            // No movable bit found - return max combination ending at highest_bit - 1
//            var highestBit = 0;
//            for (var i = 63; i >= 0; i--)
//            {
//                if ((num & (1UL << i)) != 0)
//                {
//                    highestBit = i;
//                    break;
//                }
//            }
            
//            // Count set bits
//            var popcount = 0;
//            for (var i = 0; i < 64; i++)
//            {
//                if ((num & (1UL << i)) != 0) popcount++;
//            }
            
//            // Create max combination ending at highestBit - 1
//            ulong result = 0;
//            for (var i = 0; i < popcount; i++)
//            {
//                result |= 1UL << (highestBit - 1 - i);
//            }
//            return result;
//        }
        
//        // Count set bits to the right of rightmostMovableBit
//        var setBitsToRight = 0;
//        for (var i = 0; i < rightmostMovableBit; i++)
//        {
//            if ((num & (1UL << i)) != 0) setBitsToRight++;
//        }
        
//        // Clear the rightmost movable bit
//        num &= ~(1UL << rightmostMovableBit);
        
//        // Clear all bits to the right of rightmostMovableBit
//        for (var i = 0; i < rightmostMovableBit; i++)
//        {
//            num &= ~(1UL << i);
//        }
        
//        // Set bit at rightmostMovableBit - 1
//        num |= 1UL << (rightmostMovableBit - 1);
        
//        // Set the remaining setBitsToRight bits at their highest positions
//        // consecutively to the right of the moved bit
//        for (var i = 0; i < setBitsToRight; i++)
//        {
//            num |= 1UL << (rightmostMovableBit - 2 - i);
//        }
        
//        return num;
//    }


//    public static void MainExamples()
//    {
//        // Test with examples from the file
//        RunTests();
        
//        // Run comprehensive tests with all 200 examples
//        Console.WriteLine("\n" + new string('=', 60));
//        RunAllExamplesTest();
        
//        // Run performance benchmarks
//        Console.WriteLine("\n" + new string('=', 60));
//        RunBenchmarks();
        
//        // Run enhanced benchmarks with BitOperations
//        Console.WriteLine("\n" + new string('=', 60));
//        RunEnhancedBenchmarks();
//    }
    
//    public static void RunAllExamplesTest()
//    {
//        Console.WriteLine("=== RUNNING ALL 200 EXAMPLES FROM Examples.txt ===\n");
        
//        var allTestCases = LoadAllExamples();
        
//        Console.WriteLine($"Loaded {allTestCases.Count} test cases from Examples.txt");
        
//        // Test reference implementation
//        Console.WriteLine("\n=== Testing Reference Implementation (All 200 Examples) ===");
//        var refPassed = 0;
//        var total = allTestCases.Count;
        
//        foreach (var (input, expected) in allTestCases)
//        {
//            try
//            {
//                var refResult = GetPreviousBitPatternReference(input);
//                var isCorrect = ArraysEqual(refResult, expected);
                
//                if (!isCorrect)
//                {
//                    Console.WriteLine($"FAIL: [{string.Join(", ", input)}] -> [{string.Join(", ", refResult)}] | Expected: [{string.Join(", ", expected)}]");
//                }
                
//                if (isCorrect) refPassed++;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"ERROR: [{string.Join(", ", input)}] -> Exception: {ex.Message}");
//            }
//        }
        
//        Console.WriteLine($"Reference Results: {refPassed}/{total} passed ({100.0 * refPassed / total:F1}%)");
        
//        // Test array implementation
//        Console.WriteLine("\n=== Testing Array Implementation (All 200 Examples) ===");
//        var arrPassed = 0;
        
//        foreach (var (input, expected) in allTestCases)
//        {
//            try
//            {
//                var arrResult = GetPreviousBitPattern(input);
//                var isCorrect = ArraysEqual(arrResult, expected);
                
//                if (!isCorrect)
//                {
//                    Console.WriteLine($"FAIL: [{string.Join(", ", input)}] -> [{string.Join(", ", arrResult)}] | Expected: [{string.Join(", ", expected)}]");
//                }
                
//                if (isCorrect) arrPassed++;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"ERROR: [{string.Join(", ", input)}] -> Exception: {ex.Message}");
//            }
//        }
        
//        Console.WriteLine($"Array Results: {arrPassed}/{total} passed ({100.0 * arrPassed / total:F1}%)");
        
//        // Test consistency
//        Console.WriteLine("\n=== Testing Implementation Consistency (All 200 Examples) ===");
//        var consistentCount = 0;
        
//        foreach (var (input, expected) in allTestCases)
//        {
//            try
//            {
//                var refResult = GetPreviousBitPatternReference(input);
//                var arrResult = GetPreviousBitPattern(input);
//                var isConsistent = ArraysEqual(refResult, arrResult);
                
//                if (!isConsistent)
//                {
//                    Console.WriteLine($"INCONSISTENT: [{string.Join(", ", input)}] -> Ref: [{string.Join(", ", refResult)}] | Array: [{string.Join(", ", arrResult)}]");
//                }
                
//                if (isConsistent) consistentCount++;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"ERROR: [{string.Join(", ", input)}] -> Exception: {ex.Message}");
//            }
//        }
        
//        Console.WriteLine($"Consistency: {consistentCount}/{total} matched ({100.0 * consistentCount / total:F1}%)");
        
//        // Final summary for all 200 examples
//        Console.WriteLine("\n=== FINAL SUMMARY (All 200 Examples) ===");
//        Console.WriteLine($"Reference Implementation: {refPassed}/{total} correct ({100.0 * refPassed / total:F1}%)");
//        Console.WriteLine($"Array Implementation: {arrPassed}/{total} correct ({100.0 * arrPassed / total:F1}%)");
//        Console.WriteLine($"Implementation Consistency: {consistentCount}/{total} matched ({100.0 * consistentCount / total:F1}%)");
        
//        if (refPassed == total && arrPassed == total && consistentCount == total)
//        {
//            Console.WriteLine("✅ All 200 examples passed! Both implementations are perfect and consistent.");
//        }
//        else
//        {
//            Console.WriteLine("❌ Some issues remain with the 200 examples. Check the detailed results above.");
//        }
//    }
    
//    private static List<(int[], int[])> LoadAllExamples()
//    {
//        var testCases = new List<(int[], int[])>();
        
//        try
//        {
//            var lines = System.IO.File.ReadAllLines("../Examples.txt");
            
//            foreach (var line in lines)
//            {
//                if (string.IsNullOrWhiteSpace(line)) continue;
                
//                // Parse line format: "Index set: <0, 1, 5>; Previous: <2, 3, 4>"
//                var parts = line.Split(';');
//                if (parts.Length != 2) continue;
                
//                var inputPart = parts[0].Trim();
//                var expectedPart = parts[1].Trim();
                
//                // Extract numbers from "Index set: <0, 1, 5>"
//                var inputMatch = inputPart.IndexOf('<');
//                var inputEndMatch = inputPart.IndexOf('>');
//                if (inputMatch == -1 || inputEndMatch == -1) continue;
                
//                var inputStr = inputPart.Substring(inputMatch + 1, inputEndMatch - inputMatch - 1);
//                var inputNumbers = inputStr.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
                
//                // Extract numbers from "Previous: <2, 3, 4>"
//                var expectedMatch = expectedPart.IndexOf('<');
//                var expectedEndMatch = expectedPart.IndexOf('>');
//                if (expectedMatch == -1 || expectedEndMatch == -1) continue;
                
//                var expectedStr = expectedPart.Substring(expectedMatch + 1, expectedEndMatch - expectedMatch - 1);
//                var expectedNumbers = expectedStr.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
                
//                testCases.Add((inputNumbers, expectedNumbers));
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error loading examples: {ex.Message}");
//        }
        
//        return testCases;
//    }
    
//    public static void RunTests()
//    {
//        Console.WriteLine("Running comprehensive tests...\n");
        
//        // Test cases from Examples.txt
//        var testCases = new[]
//        {
//            ([0, 1, 5], [2, 3, 4]),
//            ([0, 2, 5], [0, 1, 5]),
//            ([1, 2, 5], [0, 2, 5]),
//            ([0, 3, 5], [1, 2, 5]),
//            ([1, 3, 5], [0, 3, 5]),
//            ([2, 3, 5], [1, 3, 5]),
//            ([0, 4, 5], [2, 3, 5]),
//            ([1, 4, 5], [0, 4, 5]),
//            ([2, 4, 5], [1, 4, 5]),
//            ([3, 4, 5], [2, 4, 5]),
//            ([0, 1, 6], [3, 4, 5]),
//            ([0, 1, 2, 68], [64, 65, 66, 67]),
//            ([0, 1, 3, 68], [0, 1, 2, 68]),
//            ([0, 2, 3, 68], [0, 1, 3, 68]),
//            (new int[] {1, 2, 3, 68}, new int[] {0, 2, 3, 68})
//        };
        
//        // Test reference implementation vs expected results
//        Console.WriteLine("=== Testing Reference Implementation (Bit Manipulation) ===");
//        var refPassed = 0;
//        var total = testCases.Length;
        
//        foreach (var (input, expected) in testCases)
//        {
//            var refResult = GetPreviousBitPatternReference(input);
//            var isCorrect = ArraysEqual(refResult, expected);
            
//            Console.WriteLine($"Input: [{string.Join(", ", input)}] -> Output: [{string.Join(", ", refResult)}] | Expected: [{string.Join(", ", expected)}] | {(isCorrect ? "PASS" : "FAIL")}");
            
//            if (isCorrect) refPassed++;
//        }
        
//        Console.WriteLine($"\nReference Results: {refPassed}/{total} passed ({100.0 * refPassed / total:F1}%)\n");
        
//        // Test array implementation vs expected results
//        Console.WriteLine("=== Testing Array Implementation ===");
//        var arrPassed = 0;
        
//        foreach (var (input, expected) in testCases)
//        {
//            var arrResult = GetPreviousBitPattern(input);
//            var isCorrect = ArraysEqual(arrResult, expected);
            
//            Console.WriteLine($"Input: [{string.Join(", ", input)}] -> Output: [{string.Join(", ", arrResult)}] | Expected: [{string.Join(", ", expected)}] | {(isCorrect ? "PASS" : "FAIL")}");
            
//            if (isCorrect) arrPassed++;
//        }
        
//        Console.WriteLine($"\nArray Results: {arrPassed}/{total} passed ({100.0 * arrPassed / total:F1}%)\n");
        
//        // Test consistency between implementations
//        Console.WriteLine("=== Testing Implementation Consistency ===");
//        var consistentCount = 0;
        
//        foreach (var (input, expected) in testCases)
//        {
//            var refResult = GetPreviousBitPatternReference(input);
//            var arrResult = GetPreviousBitPattern(input);
//            var isConsistent = ArraysEqual(refResult, arrResult);
            
//            Console.WriteLine($"Input: [{string.Join(", ", input)}] -> Ref: [{string.Join(", ", refResult)}] | Array: [{string.Join(", ", arrResult)}] | {(isConsistent ? "CONSISTENT" : "INCONSISTENT")}");
            
//            if (isConsistent) consistentCount++;
//        }
        
//        Console.WriteLine($"\nConsistency: {consistentCount}/{total} matched ({100.0 * consistentCount / total:F1}%)\n");
        
//        // Additional comprehensive tests
//        Console.WriteLine("=== Additional Comprehensive Tests ===");
//        RunAdditionalTests();
        
//        // Final summary
//        Console.WriteLine("\n=== FINAL SUMMARY ===");
//        Console.WriteLine($"Reference Implementation: {refPassed}/{total} correct ({100.0 * refPassed / total:F1}%)");
//        Console.WriteLine($"Array Implementation: {arrPassed}/{total} correct ({100.0 * arrPassed / total:F1}%)");
//        Console.WriteLine($"Implementation Consistency: {consistentCount}/{total} matched ({100.0 * consistentCount / total:F1}%)");
        
//        if (refPassed == total && arrPassed == total && consistentCount == total)
//        {
//            Console.WriteLine("✅ All tests passed! Both implementations are correct and consistent.");
//        }
//        else
//        {
//            Console.WriteLine("❌ Some issues remain. Check the detailed results above.");
//        }
//    }
    
//    private static void RunAdditionalTests()
//    {
//        // Test edge cases and various sizes
//        var additionalCases = new[]
//        {
//            new int[] {0, 1, 2},           // Small consecutive case
//            new int[] {1, 3, 7},           // Powers of 2 minus 1 pattern
//            new int[] {0, 10, 20},         // Larger gaps
//            new int[] {5, 15, 25, 35},     // 4-bit pattern with gaps
//            new int[] {0, 1, 2, 3, 4},     // 5 consecutive bits
//            new int[] {10, 11, 12},        // Consecutive starting from 10
//            new int[] {0, 63},             // Maximum range 2-bit pattern
//        };
        
//        var additionalPassed = 0;
//        foreach (var testCase in additionalCases)
//        {
//            var refResult = GetPreviousBitPatternReference(testCase);
//            var arrResult = GetPreviousBitPattern(testCase);
//            var isConsistent = ArraysEqual(refResult, arrResult);
            
//            Console.WriteLine($"Additional Test [{string.Join(", ", testCase)}] -> Ref: [{string.Join(", ", refResult)}] | Array: [{string.Join(", ", arrResult)}] | {(isConsistent ? "CONSISTENT" : "INCONSISTENT")}");
            
//            if (isConsistent) additionalPassed++;
//        }
        
//        Console.WriteLine($"Additional tests consistency: {additionalPassed}/{additionalCases.Length} matched");
//    }
    
//    private static bool ArraysEqual(int[] a, int[] b)
//    {
//        if (a.Length != b.Length) return false;
//        for (var i = 0; i < a.Length; i++)
//        {
//            if (a[i] != b[i]) return false;
//        }
//        return true;
//    }
    
//    // Optimized Reference Implementation with early exits and faster bit operations
//    public static int[] GetPreviousBitPatternReferenceOptimized(int[] indexArray)
//    {
//        if (indexArray == null || indexArray.Length == 0)
//            throw new ArgumentException("Input array cannot be null or empty");
        
//        var n = indexArray.Length;
        
//        // Fast path for single element
//        if (n == 1)
//        {
//            return indexArray[0] == 0 ? [63] : [indexArray[0] - 1];
//        }
        
//        // Convert bit positions to numerical value with bounds checking
//        ulong currentValue = 0;
//        var maxBit = indexArray[n - 1];
//        if (maxBit >= 64)
//        {
//            // Fallback to array implementation for large bit positions
//            return GetPreviousBitPatternOptimized(indexArray);
//        }
        
//        foreach (var pos in indexArray)
//        {
//            currentValue |= 1UL << pos;
//        }
        
//        // Find the previous number with the same popcount (optimized)
//        var previousValue = GetPreviousBitPattern_BitManipulationOptimized(currentValue);
        
//        // Convert back to bit positions
//        var result = new int[n];
//        var index = 0;
        
//        // Use bit scan operations for faster conversion
//        var temp = previousValue;
//        while (temp != 0 && index < n)
//        {
//            var bit = BitScanForward(temp);
//            result[index++] = bit;
//            temp &= temp - 1; // Clear lowest set bit
//        }
        
//        return result;
//    }
    
//    // Optimized Array Implementation with minimal allocations and early exits
//    public static int[] GetPreviousBitPatternOptimized(int[] indexArray)
//    {
//        if (indexArray == null || indexArray.Length == 0)
//            throw new ArgumentException("Input array cannot be null or empty");
        
//        var k = indexArray.Length;
        
//        // Fast path for single element
//        if (k == 1)
//            return indexArray[0] > 0 
//                ? [indexArray[0] - 1] 
//                : throw new InvalidOperationException();
        
//        // Fast paths for consecutive patterns
//        var isConsecutive = true;
//        for (var i = 1; i < k; i++)
//        {
//            if (indexArray[i] == indexArray[i - 1] + 1) continue;

//            isConsecutive = false;
//            break;
//        }
        
//        if (isConsecutive && indexArray[0] == 0)
//        {
//            // Consecutive starting from 0 - jump to max arrangement
//            // ending at indexArray[k-1] - 1
//            var result = new int[k];
//            var maxEnd = indexArray[k - 1] - 1;

//            for (var i = 0; i < k; i++) 
//                result[i] = maxEnd - (k - 1 - i);

//            return result;
//        }
        
//        // Optimized main algorithm with pre-allocated arrays
//        var maxPos = indexArray[k - 1];
        
//        // Use span for better performance when possible
//        var positions = new bool[maxPos + 1];
//        foreach (var pos in indexArray) 
//            positions[pos] = true;
        
//        // Find the rightmost movable bit with early termination
//        var rightmostMovable = -1;
        
//        for (var i = 1; i <= maxPos; i++)
//        {
//            if (!positions[i] || positions[i - 1]) continue;

//            rightmostMovable = i;
//            break;
//        }
        
//        if (rightmostMovable == -1)
//        {
//            // No movable position - return max combination
//            var result = new int[k];

//            for (var i = 0; i < k; i++) 
//                result[i] = maxPos - 1 - (k - 1 - i);

//            return result;
//        }
        
//        // Count bits to the right (optimized)
//        var setBitsToRight = 0;
//        for (var i = 0; i < rightmostMovable; i++)
//            if (positions[i]) setBitsToRight++;
        
//        // Clear and set positions efficiently
//        Array.Clear(positions, 0, rightmostMovable + 1);
//        positions[rightmostMovable - 1] = true;
        
//        // Set remaining bits consecutively
//        for (var i = 0; i < setBitsToRight; i++) 
//            positions[rightmostMovable - 2 - i] = true;
        
//        // Convert back to result array
//        var finalResult = new int[k];
//        var index = 0;
//        for (var i = 0; i <= maxPos && index < k; i++)
//            if (positions[i]) finalResult[index++] = i;
        
//        return finalResult;
//    }
    
//    // Optimized bit manipulation core
//    private static ulong GetPreviousBitPattern_BitManipulationOptimized(ulong num)
//    {
//        if (num == 0) return 0;
        
//        // Use bit operations for faster rightmost movable bit finding
//        var rightmostMovable = -1;
        
//        // Find rightmost non-trailing set bit more efficiently
//        var temp = num;
//        var bit = 0;
        
//        // Skip trailing zeros
//        if ((temp & 1) == 0)
//        {
//            temp = temp >> 1;
//            bit = 1;
//            while ((temp & 1) == 0 && bit < 64)
//            {
//                temp >>= 1;
//                bit++;
//            }
//        }
        
//        // Find first gap
//        while (bit < 63 && temp != 0)
//        {
//            if ((temp & 1) != 0 && (temp & 2) == 0) // bit set, next bit clear
//            {
//                rightmostMovable = bit;
//                break;
//            }
//            temp >>= 1;
//            bit++;
//        }
        
//        if (rightmostMovable == -1)
//        {
//            // Use bit-counting for faster pop count
//            var popCount = PopCount(num);
//            var highestBit = 63 - LeadingZeroCount(num);
            
//            ulong result = 0;
//            for (var i = 0; i < popCount; i++)
//            {
//                result |= 1UL << (highestBit - 1 - i);
//            }
//            return result;
//        }
        
//        // Count set bits to the right using bit manipulation
//        var mask = (1UL << rightmostMovable) - 1;
//        var setBitsToRight = PopCount(num & mask);
        
//        // Clear movable bit and bits to the right
//        num &= ~((1UL << (rightmostMovable + 1)) - 1);
        
//        // Set new positions
//        num |= 1UL << (rightmostMovable - 1);
        
//        // Set remaining bits consecutively
//        for (var i = 0; i < setBitsToRight; i++)
//        {
//            num |= 1UL << (rightmostMovable - 2 - i);
//        }
        
//        return num;
//    }
    
//    // Fast bit operations
//    private static int BitScanForward(ulong value)
//    {
//        if (value == 0) return -1;
        
//        var result = 0;
//        if ((value & 0xFFFFFFFF) == 0) { result += 32; value >>= 32; }
//        if ((value & 0xFFFF) == 0) { result += 16; value >>= 16; }
//        if ((value & 0xFF) == 0) { result += 8; value >>= 8; }
//        if ((value & 0xF) == 0) { result += 4; value >>= 4; }
//        if ((value & 0x3) == 0) { result += 2; value >>= 2; }
//        if ((value & 0x1) == 0) { result += 1; }
        
//        return result;
//    }
    
//    private static int PopCount(ulong value)
//    {
//        // Brian Kernighan's algorithm
//        var count = 0;
//        while (value != 0)
//        {
//            count++;
//            value &= value - 1;
//        }
//        return count;
//    }
    
//    private static int LeadingZeroCount(ulong value)
//    {
//        if (value == 0) return 64;
        
//        var count = 0;
//        if (value <= 0xFFFFFFFF) { count += 32; value <<= 32; }
//        if (value <= 0xFFFFFFFFFFFF) { count += 16; value <<= 16; }
//        if (value <= 0xFFFFFFFFFFFFFF) { count += 8; value <<= 8; }
//        if (value <= 0xFFFFFFFFFFFFFFF) { count += 4; value <<= 4; }
//        if (value <= 0x3FFFFFFFFFFFFFFF) { count += 2; value <<= 2; }
//        if (value <= 0x7FFFFFFFFFFFFFFF) { count += 1; }
        
//        return count;
//    }
    
//    public static void RunBenchmarks()
//    {
//        Console.WriteLine("=== PERFORMANCE BENCHMARKS ===\n");
        
//        const int warmupIterations = 1000;
//        const int benchmarkIterations = 10000;
        
//        // Test different pattern sizes
//        for (var patternSize = 1; patternSize <= 8; patternSize++)
//        {
//            Console.WriteLine($"--- Pattern Size: {patternSize} ---");
            
//            // Generate test cases for this pattern size
//            var testCases = GenerateTestCases(patternSize, 100); // 100 test cases per size
            
//            // Warm up
//            foreach (var testCase in testCases)
//            {
//                GetPreviousBitPatternReference(testCase);
//                GetPreviousBitPattern(testCase);
//                GetPreviousBitPatternReferenceOptimized(testCase);
//                GetPreviousBitPatternOptimized(testCase);
//            }
            
//            // Benchmark Reference Implementation
//            var sw = Stopwatch.StartNew();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternReference(testCase);
//                }
//            }
//            sw.Stop();
//            var referenceTime = sw.Elapsed.TotalMilliseconds;
            
//            // Benchmark Array Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPattern(testCase);
//                }
//            }
//            sw.Stop();
//            var arrayTime = sw.Elapsed.TotalMilliseconds;
            
//            // Benchmark Optimized Reference Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternReferenceOptimized(testCase);
//                }
//            }
//            sw.Stop();
//            var referenceOptimizedTime = sw.Elapsed.TotalMilliseconds;
            
//            // Benchmark Optimized Array Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternOptimized(testCase);
//                }
//            }
//            sw.Stop();
//            var arrayOptimizedTime = sw.Elapsed.TotalMilliseconds;
            
//            // Calculate operations per second
//            var totalOperations = (long)benchmarkIterations * testCases.Count;
            
//            Console.WriteLine($"Reference Implementation:           {referenceTime:F2}ms ({totalOperations / referenceTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Array Implementation:               {arrayTime:F2}ms ({totalOperations / arrayTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Optimized Reference Implementation: {referenceOptimizedTime:F2}ms ({totalOperations / referenceOptimizedTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Optimized Array Implementation:     {arrayOptimizedTime:F2}ms ({totalOperations / arrayOptimizedTime * 1000:F0} ops/sec)");
            
//            // Calculate performance improvements
//            var refImprovement = (referenceTime - referenceOptimizedTime) / referenceTime * 100;
//            var arrayImprovement = (arrayTime - arrayOptimizedTime) / arrayTime * 100;
            
//            Console.WriteLine($"Reference Optimization Improvement: {refImprovement:F1}%");
//            Console.WriteLine($"Array Optimization Improvement:     {arrayImprovement:F1}%");
//            Console.WriteLine();
//        }
        
//        // Validate correctness of optimized implementations
//        Console.WriteLine("=== CORRECTNESS VALIDATION ===");
//        ValidateOptimizedImplementations();
//    }
    
//    private static List<int[]> GenerateTestCases(int patternSize, int count)
//    {
//        var testCases = new List<int[]>();
//        var random = new System.Random(42); // Fixed seed for reproducibility
        
//        for (var i = 0; i < count; i++)
//        {
//            var pattern = new int[patternSize];
//            var usedPositions = new HashSet<int>();
            
//            for (var j = 0; j < patternSize; j++)
//            {
//                int pos;
//                do
//                {
//                    pos = random.Next(0, Math.Min(64, 20 + patternSize * 5)); // Reasonable range
//                } while (usedPositions.Contains(pos));
                
//                usedPositions.Add(pos);
//                pattern[j] = pos;
//            }
            
//            Array.Sort(pattern);
//            testCases.Add(pattern);
//        }
        
//        return testCases;
//    }
    
//    private static void ValidateOptimizedImplementations()
//    {
//        var testCases = new[]
//        {
//            new int[] { 5 },
//            new int[] { 0, 1 },
//            new int[] { 0, 1, 5 },
//            new int[] { 0, 2, 5 },
//            new int[] { 1, 2, 5 },
//            new int[] { 0, 1, 6 },
//            new int[] { 1, 3, 7 },
//            new int[] { 0, 10, 20, 30 },
//            new int[] { 5, 15, 25, 35, 45 }
//        };
        
//        var passed = 0;
//        var total = testCases.Length;
        
//        foreach (var testCase in testCases)
//        {
//            var original = GetPreviousBitPattern(testCase);
//            var optimized = GetPreviousBitPatternOptimized(testCase);
            
//            var matches = ArraysEqual(original, optimized);
//            if (matches)
//            {
//                passed++;
//            }
//            else
//            {
//                Console.WriteLine($"MISMATCH: [{string.Join(", ", testCase)}] -> Original: [{string.Join(", ", original)}] | Optimized: [{string.Join(", ", optimized)}]");
//            }
//        }
        
//        Console.WriteLine($"Optimized Implementation Validation: {passed}/{total} passed ({100.0 * passed / total:F1}%)");
        
//        if (passed == total)
//        {
//            Console.WriteLine("✅ All optimized implementations are correct!");
//        }
//        else
//        {
//            Console.WriteLine("❌ Some optimized implementations have issues.");
//        }
//    }
        
//    // Ultra-Optimized Reference Implementation using BitOperations for hardware acceleration
//    public static int[] GetPreviousBitPatternReferenceUltraOptimized(int[] indexArray)
//    {
//        if (indexArray == null || indexArray.Length == 0)
//            throw new ArgumentException("Input array cannot be null or empty");
            
//        var n = indexArray.Length;
            
//        // Fast path for single element
//        if (n == 1)
//            return indexArray[0] > 0 
//                ? [indexArray[0] - 1]
//                : throw new InvalidOperationException();
            
//        // Convert bit positions to numerical value with bounds checking
//        ulong currentValue = 0;
//        var maxBit = indexArray[n - 1];
        
//        // Fallback to array implementation for large bit positions
//        if (maxBit >= 64)
//            return GetPreviousBitPatternUltraOptimized(indexArray);
            
//        foreach (var pos in indexArray) 
//            currentValue |= 1UL << pos;
            
//        // Find the previous number with the same pop count (ultra-optimized)
//        var previousValue = GetPreviousBitPattern_BitOperationsOptimized(currentValue);
            
//        // Handle special case where BitOperations couldn't create valid bit pattern
//        // Fall back to array implementation for edge cases
//        if (previousValue == 0 && currentValue != 0)
//            return GetPreviousBitPatternUltraOptimized(indexArray);
            
//        // Convert back to bit positions using hardware-accelerated operations
//        var result = new int[n];
//        var index = 0;
            
//        var temp = previousValue;
//        while (temp != 0 && index < n)
//        {
//            var bit = BitOperations.TrailingZeroCount(temp);
//            result[index++] = bit;
//            temp &= temp - 1; // Clear lowest set bit
//        }
            
//        return result;
//    }
        
//    // Ultra-Optimized Array Implementation with hardware-accelerated bit operations
//    public static int[] GetPreviousBitPatternUltraOptimized(int[] indexArray)
//    {
//        if (indexArray == null || indexArray.Length == 0)
//            throw new ArgumentException("Input array cannot be null or empty");
            
//        var k = indexArray.Length;
            
//        // Fast path for single element
//        if (k == 1)
//        {
//            return indexArray[0] == 0 ? [-1] : [indexArray[0] - 1];
//        }
            
//        // Enhanced fast paths for consecutive patterns
//        var isConsecutive = true;
//        for (var i = 1; i < k; i++)
//        {
//            if (indexArray[i] != indexArray[i-1] + 1)
//            {
//                isConsecutive = false;
//                break;
//            }
//        }
            
//        if (isConsecutive && indexArray[0] == 0)
//        {
//            // Consecutive starting from 0 - jump to max arrangement ending at indexArray[k-1] - 1
//            var result = new int[k];
//            var maxEnd = indexArray[k - 1] - 1;
//            for (var i = 0; i < k; i++)
//            {
//                result[i] = maxEnd - (k - 1 - i);
//            }
//            return result;
//        }
            
//        // Ultra-optimized main algorithm
//        var maxPos = indexArray[k - 1];
            
//        // Use bit operations when possible for small ranges
//        if (maxPos <= 63)
//        {
//            // Convert to bit representation
//            ulong bitMask = 0;
//            foreach (var pos in indexArray)
//            {
//                bitMask |= 1UL << pos;
//            }
                
//            // Use hardware-accelerated bit operations
//            var previous = GetPreviousBitPattern_BitOperationsOptimized(bitMask);
                
//            // Convert back using hardware operations
//            var result = new int[k];
//            var bitIndex = 0;
                
//            var temp = previous;
//            while (temp != 0 && bitIndex < k)
//            {
//                var bit = BitOperations.TrailingZeroCount(temp);
//                result[bitIndex++] = bit;
//                temp &= temp - 1;
//            }
                
//            return result;
//        }
            
//        // Fallback to array approach for larger bit positions
//        var positions = new bool[maxPos + 1];
//        foreach (var pos in indexArray)
//        {
//            positions[pos] = true;
//        }
            
//        // Find the rightmost movable bit with early termination
//        var rightmostMovable = -1;
            
//        for (var i = 1; i <= maxPos; i++)
//        {
//            if (positions[i] && !positions[i-1])
//            {
//                rightmostMovable = i;
//                break;
//            }
//        }
            
//        if (rightmostMovable == -1)
//        {
//            // No movable position - return max combination
//            var result = new int[k];
//            for (var i = 0; i < k; i++)
//            {
//                result[i] = maxPos - 1 - (k - 1 - i);
//            }
//            return result;
//        }
            
//        // Count bits to the right (optimized)
//        var setBitsToRight = 0;
//        for (var i = 0; i < rightmostMovable; i++)
//        {
//            if (positions[i]) setBitsToRight++;
//        }
            
//        // Clear and set positions efficiently
//        Array.Clear(positions, 0, rightmostMovable + 1);
//        positions[rightmostMovable - 1] = true;
            
//        // Set remaining bits consecutively
//        for (var i = 0; i < setBitsToRight; i++)
//        {
//            positions[rightmostMovable - 2 - i] = true;
//        }
            
//        // Convert back to result array
//        var finalResult = new int[k];
//        var index = 0;
//        for (var i = 0; i <= maxPos && index < k; i++)
//        {
//            if (positions[i])
//            {
//                finalResult[index++] = i;
//            }
//        }
            
//        return finalResult;
//    }
        
//    // Ultra-optimized bit manipulation core using BitOperations
//    private static ulong GetPreviousBitPattern_BitOperationsOptimized(ulong num)
//    {
//        if (num == 0) return 0;
            
//        // Use hardware-accelerated operations for faster bit manipulation
//        var rightmostMovable = -1;
            
//        // Find the rightmost set bit that can be moved left (has a gap to its left)
//        // This matches the array implementation logic exactly
//        for (var i = 1; i < 64; i++)
//        {
//            if ((num & (1UL << i)) != 0) // bit i is set
//            {
//                // Check if there's a gap (unset bit) to the left of position i
//                if ((num & (1UL << (i - 1))) == 0) // immediate left bit is 0
//                {
//                    rightmostMovable = i;
//                    break; // Take the first (rightmost) one we find
//                }
//            }
//        }
            
//        if (rightmostMovable == -1)
//        {
//            // No movable bit found - return max combination ending at highest_bit - 1
//            // Use hardware popcount and leading zero count for efficiency
//            var popcount = BitOperations.PopCount(num);
//            var leadingZeros = BitOperations.LeadingZeroCount(num);
//            var highestBit = 63 - leadingZeros;
                
//            // Check if we can create a valid max combination
//            if (highestBit - 1 - (popcount - 1) < 0)
//            {
//                // Would require negative bit positions - fallback to array implementation approach
//                // This handles edge cases like [0, 1] -> [-1, 0]
//                return 0; // Return 0 to indicate special case that needs array conversion
//            }
                
//            // Create max combination ending at highestBit - 1
//            ulong result = 0;
//            for (var i = 0; i < popcount; i++)
//            {
//                var bitPos = highestBit - 1 - i;
//                if (bitPos >= 0)
//                {
//                    result |= 1UL << bitPos;
//                }
//            }
//            return result;
//        }
            
//        // Count set bits to the right of rightmostMovable using hardware popcount
//        var mask = (1UL << rightmostMovable) - 1;
//        var setBitsToRight = BitOperations.PopCount(num & mask);
            
//        // Clear the rightmost movable bit
//        num &= ~(1UL << rightmostMovable);
            
//        // Clear all bits to the right of rightmostMovable
//        num &= ~mask;
            
//        // Set bit at rightmostMovable - 1
//        num |= 1UL << (rightmostMovable - 1);
            
//        // Set the remaining setBitsToRight bits at their highest positions
//        // consecutively to the right of the moved bit
//        for (var i = 0; i < setBitsToRight; i++)
//        {
//            num |= 1UL << (rightmostMovable - 2 - i);
//        }
            
//        return num;
//    }
        
//    // Enhanced benchmarking with BitOperations implementations
//    public static void RunEnhancedBenchmarks()
//    {
//        Console.WriteLine("=== ENHANCED PERFORMANCE BENCHMARKS (Including BitOperations) ===\n");
            
//        const int benchmarkIterations = 10000;
            
//        // Test different pattern sizes
//        for (var patternSize = 1; patternSize <= 8; patternSize++)
//        {
//            Console.WriteLine($"--- Pattern Size: {patternSize} ---");
                
//            // Generate test cases for this pattern size
//            var testCases = GenerateTestCases(patternSize, 100);
                
//            // Warm up all implementations
//            foreach (var testCase in testCases)
//            {
//                GetPreviousBitPatternReference(testCase);
//                GetPreviousBitPattern(testCase);
//                GetPreviousBitPatternReferenceOptimized(testCase);
//                GetPreviousBitPatternOptimized(testCase);
//                GetPreviousBitPatternReferenceUltraOptimized(testCase);
//                GetPreviousBitPatternUltraOptimized(testCase);
//            }
                
//            // Benchmark Original Reference Implementation
//            var sw = Stopwatch.StartNew();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternReference(testCase);
//                }
//            }
//            sw.Stop();
//            var referenceTime = sw.Elapsed.TotalMilliseconds;
                
//            // Benchmark Original Array Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPattern(testCase);
//                }
//            }
//            sw.Stop();
//            var arrayTime = sw.Elapsed.TotalMilliseconds;
                
//            // Benchmark Optimized Reference Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternReferenceOptimized(testCase);
//                }
//            }
//            sw.Stop();
//            var referenceOptimizedTime = sw.Elapsed.TotalMilliseconds;
                
//            // Benchmark Optimized Array Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternOptimized(testCase);
//                }
//            }
//            sw.Stop();
//            var arrayOptimizedTime = sw.Elapsed.TotalMilliseconds;
                
//            // Benchmark Ultra-Optimized Reference Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternReferenceUltraOptimized(testCase);
//                }
//            }
//            sw.Stop();
//            var referenceUltraTime = sw.Elapsed.TotalMilliseconds;
                
//            // Benchmark Ultra-Optimized Array Implementation
//            sw.Restart();
//            for (var i = 0; i < benchmarkIterations; i++)
//            {
//                foreach (var testCase in testCases)
//                {
//                    GetPreviousBitPatternUltraOptimized(testCase);
//                }
//            }
//            sw.Stop();
//            var arrayUltraTime = sw.Elapsed.TotalMilliseconds;
                
//            // Calculate operations per second
//            var totalOperations = (long)benchmarkIterations * testCases.Count;
                
//            Console.WriteLine($"Reference (Original):              {referenceTime:F2}ms ({totalOperations / referenceTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Array (Original):                  {arrayTime:F2}ms ({totalOperations / arrayTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Reference (Optimized):             {referenceOptimizedTime:F2}ms ({totalOperations / referenceOptimizedTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Array (Optimized):                 {arrayOptimizedTime:F2}ms ({totalOperations / arrayOptimizedTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Reference (BitOperations Ultra):   {referenceUltraTime:F2}ms ({totalOperations / referenceUltraTime * 1000:F0} ops/sec)");
//            Console.WriteLine($"Array (BitOperations Ultra):       {arrayUltraTime:F2}ms ({totalOperations / arrayUltraTime * 1000:F0} ops/sec)");
                
//            // Calculate performance improvements
//            var refUltraImprovement = (referenceTime - referenceUltraTime) / referenceTime * 100;
//            var arrayUltraImprovement = (arrayTime - arrayUltraTime) / arrayTime * 100;
//            var refUltraVsOptimized = (referenceOptimizedTime - referenceUltraTime) / referenceOptimizedTime * 100;
//            var arrayUltraVsOptimized = (arrayOptimizedTime - arrayUltraTime) / arrayOptimizedTime * 100;
                
//            Console.WriteLine($"Reference BitOperations vs Original:  {refUltraImprovement:F1}%");
//            Console.WriteLine($"Array BitOperations vs Original:      {arrayUltraImprovement:F1}%");
//            Console.WriteLine($"Reference BitOperations vs Optimized: {refUltraVsOptimized:F1}%");
//            Console.WriteLine($"Array BitOperations vs Optimized:     {arrayUltraVsOptimized:F1}%");
//            Console.WriteLine();
//        }
            
//        // Validate correctness of ultra-optimized implementations
//        Console.WriteLine("=== BITOPERATIONS CORRECTNESS VALIDATION ===");
//        ValidateUltraOptimizedImplementations();
//    }
        
//    private static void ValidateUltraOptimizedImplementations()
//    {
//        var testCases = new[]
//        {
//            new int[] { 5 },
//            new int[] { 0, 1 },
//            new int[] { 0, 1, 5 },
//            new int[] { 0, 2, 5 },
//            new int[] { 1, 2, 5 },
//            new int[] { 0, 1, 6 },
//            new int[] { 1, 3, 7 },
//            new int[] { 0, 10, 20, 30 },
//            new int[] { 5, 15, 25, 35, 45 },
//            new int[] { 2, 4, 8, 16 },
//            new int[] { 1, 3, 9, 27 },
//            new int[] { 0, 7, 14, 21 }
//        };
            
//        var passed = 0;
//        var total = testCases.Length;
            
//        foreach (var testCase in testCases)
//        {
//            var original = GetPreviousBitPattern(testCase);
//            var ultraRef = GetPreviousBitPatternReferenceUltraOptimized(testCase);
//            var ultraArray = GetPreviousBitPatternUltraOptimized(testCase);
                
//            var refMatches = ArraysEqual(original, ultraRef);
//            var arrayMatches = ArraysEqual(original, ultraArray);
                
//            if (refMatches && arrayMatches)
//            {
//                passed++;
//            }
//            else
//            {
//                Console.WriteLine($"MISMATCH: [{string.Join(", ", testCase)}]");
//                Console.WriteLine($"  Original: [{string.Join(", ", original)}]");
//                Console.WriteLine($"  Ultra Ref: [{string.Join(", ", ultraRef)}] {(refMatches ? "✓" : "✗")}");
//                Console.WriteLine($"  Ultra Array: [{string.Join(", ", ultraArray)}] {(arrayMatches ? "✓" : "✗")}");
//            }
//        }
            
//        Console.WriteLine($"BitOperations Ultra-Optimized Validation: {passed}/{total} passed ({100.0 * passed / total:F1}%)");
            
//        if (passed == total)
//        {
//            Console.WriteLine("✅ All BitOperations ultra-optimized implementations are correct!");
//        }
//        else
//        {
//            Console.WriteLine("❌ Some BitOperations ultra-optimized implementations have issues.");
//        }
//    }
//}