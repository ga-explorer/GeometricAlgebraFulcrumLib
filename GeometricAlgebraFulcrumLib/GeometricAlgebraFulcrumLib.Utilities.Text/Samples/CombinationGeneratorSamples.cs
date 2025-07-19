using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Samples
{
    public static class CombinationGeneratorSamples
    {
        private static string[] GetInputNames(int n)
        {
            return n.MapRange(i => "i" + (i + 1)).ToArray();
        }

        public static void Example1()
        {
            const int n = 2;

            // Input values
            var inputNames = GetInputNames(n);

            // Group combinations by subset size
            var grouped = new List<string>[n + 1]; // index = subset size
            for (var i = 0; i <= n; i++)
                grouped[i] = [];

            // Generate all non-empty subsets
            for (var mask = 1; mask < (1 << n); mask++)
            {
                var elements = new List<string>();
                for (var i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0) 
                        elements.Add(inputNames[i]);
                }

                grouped[elements.Count].Add($"new SmallIndexSet({string.Join(", ", elements)});");
            }

            var sb = new StringBuilder();

            // Append combinations in order from smallest to largest
            for (var size = 1; size <= n; size++)
            {
                sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
                sb.AppendLine($"private IEnumerable<SmallIndexSet> GetSubsets{n}{size}()");
                sb.AppendLine("{");

                foreach (var line in grouped[size]) 
                    sb.AppendLine("    yield return " + line);
                
                sb.AppendLine("}");
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
    
    }
}
