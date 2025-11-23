// File: Program.cs
// Purpose: Compute the nearest lower/higher virtual bit-pattern (same popcount) using only array operations.
// Notes:
//  - Inputs are strictly increasing, non-negative integer indices (positions of set bits) in ascending order.
//  - No bitwise tricks; pure array index math.
//  - TryGetPreviousIndices finds the largest pattern < Q; TryGetNextIndices finds the smallest pattern > Q.

namespace GeometricAlgebraFulcrumLib.Samples.Utilities.Structures.IndexSets;

public static class VirtualBitPatterns
{
    /// <summary>
    /// Try to get the nearest *lower* pattern P (< Q) with the same number of set-bit indices.
    /// Q is ascending, length n, values >= 0. Returns false if Q is the minimal pattern [0,1,2,...,n-1].
    /// </summary>
    public static bool TryGetPreviousIndices(int[] q, out int[] p)
    {
        if (q == null)
            throw new ArgumentNullException(nameof(q));

        if (q.Length == 0)
            throw new ArgumentException("Q must be non-empty.", nameof(q));

        ValidateAscendingUniqueNonNegative(q);

        var n = q.Length;
        // Scan pivot from least-significant index upward to keep the result as large as possible.
        for (var t = 0; t < n; t++)
        {
            var pivot = q[t];
            var need = t + 1; // how many ones must live strictly below pivot after we turn pivot off

            if (pivot < need) continue;

            // Build P: pack 'need' ones directly below pivot at the highest possible positions,
            // keep all higher bits (q[t+1..]) unchanged.
            p = new int[n];

            // Fill the packed block ending at pivot-1: [pivot-need, ..., pivot-1]
            for (var i = 0; i < need; i++)
                p[i] = pivot - need + i;

            // Copy the suffix unchanged
            for (var i = t + 1; i < n; i++)
                p[need + (i - (t + 1))] = q[i];

            return true;
        }

        p = [];
        return false; // already minimal: [0,1,...,n-1]
    }

    public static bool TryGetNextIndices(int[] q, out int[] p)
    {
        var n = q.Length;

        if (n == 0)
        {
            p = [];
            return false;
        }
        
        //ValidateAscendingUniqueNonNegative(q);
        
        if (n == 1)
        {
            p = [q[0] + 1];
            return true;
        }
        
        if (n == 2)
        {
            var i1 = q[0];
            var i2 = q[1];
            i1++;
            if (i1 == i2)
            {
                i1 = 0;
                i2++;
            }
            p = [i1, i2];
            return true;
        }
        
        if (n == 3)
        {
            var i1 = q[0];
            var i2 = q[1];
            var i3 = q[2];
            i1++;
            if (i1 == i2)
            {
                i1 = 0;
                i2++;
                if (i2 == i3)
                {
                    i1 = 0;
                    i2 = 1;
                    i3++;
                }
            }
            p = [i1, i2, i3];
            return true;
        }

        p = new int[n];
        Array.Copy(q, p, n);
        
        // Find the leftmost position i with a gap to its right: q[i] + 1 < q[i+1].
        // If found, increment p[i], reset all lower indices to minimal [0..i-1], keep the rest.
        for (var i = 0; i < n - 1; i++)
        {
            if (p[i] + 1 >= p[i + 1]) continue;

            p[i] += 1;
            for (var j = 0; j < i; j++) 
                p[j] = j; // bottom-pack to minimize value
            
            return true;
        }
        
        // No gap: all adjacent are consecutive. Move the MSB up by one and pack the rest to bottom.
        for (var j = 0; j < n - 1; j++) 
            p[j] = j;

        p[n - 1] = q[n - 1] + 1;

        return true;
    }

    private static void ValidateAscendingUniqueNonNegative(int[] q)
    {
        if (q[0] < 0)
            throw new ArgumentOutOfRangeException(nameof(q), "Indices must be non-negative.");

        for (var i = 1; i < q.Length; i++)
        {
            if (q[i] <= q[i - 1])
                throw new ArgumentException("Indices must be strictly increasing.", nameof(q));
        }
    }
}