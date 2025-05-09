using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Structures.SortingNetworks;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Samples
{
    public static class SortingNetworkCodeSamples
    {
        // TODO: Create a GA optimizer to create selection networks of given size

        public static string GetCode1(SortingNetwork network)
        {
            var template = new ParametricTextComposer(
                "#",
                "#",
                @"
/// <summary>
/// Sort #n# items in array in-place
/// </summary>
/// <typeparam name=""T""></typeparam>
/// <param name=""itemArray"">Array to be sorted</param>
/// <param name=""index"">Index of first item in array</param>
/// <returns></returns>
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static void Sort#n#Items<T>(this T[] itemArray, int index = 0)
    where T : IComparable
{
    #code#
}
".Trim()
            );

            var composer = new LinearTextComposer();

            foreach (var layer in network.Layers)
            {
                foreach (var (i1, i2) in layer)
                {
                    var i1Text = i1 == 0 ? "index" : $"index + {i1}";
                    var i2Text = i2 == 0 ? "index" : $"index + {i2}";

                    composer.AppendLineAtNewLine($@"Branchless.SwapIfGreaterThan(ref itemArray[{i1Text}], ref itemArray[{i2Text}]);");
                }

                composer.AppendLineAtNewLine();
            }

            var n = network.InputCount.ToString();
            var code = composer.ToString().Trim();

            return template.GenerateUsing(n, code);
        }

        public static string GetCode2(SortingNetwork network, string arrayType)
        {
            var template = new ParametricTextComposer(
                "#",
                "#",
                @"
/// <summary>
/// Sort #n# items in array in-place
/// </summary>
/// <param name=""itemArray"">Array to be sorted</param>
/// <param name=""index"">Index of first item in array</param>
/// <returns></returns>
public static void Sort#n#Items(this #type#[] itemArray, int index = 0)
{
    #code#
}
".Trim()
            );

            var composer = new LinearTextComposer();

            composer.AppendLineAtNewLine(
                network.InputCount.MapRange(
                    i =>
                    {
                        var iText = i == 0 ? "index" : $"index + {i}";

                        return $"ref var i{i} = ref itemArray[{iText}];";
                    }).ConcatenateText(Environment.NewLine)
            ).AppendLineAtNewLine();

            var layerIndex = 1;
            foreach (var layer in network.Layers)
            {
                var layerCode =
                    layer.Cast<IPair<int>>().MapTuples(
                        (i1, i2) =>
                            $"Branchless.SwapIfGreaterThan(ref i{i1}, ref i{i2});"
                    ).ConcatenateText(Environment.NewLine);

                composer
                    .AppendLineAtNewLine($"// Layer {layerIndex}")
                    .AppendLineAtNewLine(layerCode)
                    .AppendLineAtNewLine();

                layerIndex++;
            }

            var n = network.InputCount.ToString();
            var code = composer.ToString().Trim();

            return template.GenerateUsing(n, arrayType, code);
        }

        public static string GetCode3(SortingNetwork network)
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLine(@"/*")
                .IncreaseIndentation()
                .AppendLineAtNewLine(network.ToString())
                .DecreaseIndentation()
                .AppendLineAtNewLine(@"*/");

            foreach (var layer in network.Layers)
            {
                foreach (var (i1, i2) in layer)
                {
                    var i1Text = $"i{i1}";
                    var i2Text = $"i{i2}";

                    var layerCode =
                        $@"if ({i1Text} > {i2Text}) ({i1Text}, {i2Text}) = ({i2Text}, {i1Text});";

                    composer.AppendLineAtNewLine(layerCode);
                }

                composer.AppendLineAtNewLine();
            }

            return composer.ToString().Trim();
        }

        public static void CodeGenExample1()
        {
            var typeArray1 = new string[]
            {
                "byte",
                "sbyte",
                "ushort",
                "short",
                "uint",
                "int"
            };

            var typeArray2 = new string[]
            {
                "UInt8",
                "Int8",
                "UInt16",
                "Int16",
                "UInt32",
                "Int32"
            };

            for (var i = 0; i < typeArray1.Length; i++)
            {
                var typeName = typeArray1[i];
                var className = $"{typeArray2[i]}ArraySortUtils";
                var composer = new LinearTextComposer();

                composer
                    .AppendLine("namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SortingNetworks;")
                    .AppendLine()
                    .AppendLine($"public static class {className}")
                    .AppendLine("{")
                    .IncreaseIndentation();

                for (var n = 2; n <= 64; n++)
                {
                    var code = GetCode2(
                        SortingNetwork.Predefined(n), 
                        typeName
                    );

                    composer.AppendLineAtNewLine(code).AppendLineAtNewLine();
                }

                composer
                    .DecreaseIndentation()
                    .AppendLineAtNewLine("}");

                File.WriteAllText(
                    @$"D:\{className}.cs", 
                    composer.ToString()
                );
            }
        }

        public static void BenchmarkExample1()
        {
            const int inputCount = 64;
            const int n = 1024000;

            var network = SortingNetwork.BoseNelsen(inputCount);

            var arrayList1 = new List<int[]>(n);
            var arrayList2 = new List<int[]>(n);

            for (var i = 0; i < n; i++)
            {
                var array1 = inputCount.MapRange(k => k).Shuffled().ToArray();
                var array2 = array1.Select(v => (2 * v)).ToArray();

                arrayList1.Add(array1);
                arrayList2.Add(array2);
            }

            var t1 = DateTime.Now;
            for (var i = 0; i < n; i++)
            {
                Array.Sort(arrayList1[i]);

                Debug.Assert(arrayList1[i].IsSortedAscending());
            }
            var t2 = DateTime.Now;
            Console.WriteLine("Normal sort: " + (t2 - t1).TotalSeconds);
            Console.WriteLine();


            t1 = DateTime.Now;
            for (var i = 0; i < n; i++)
            {
                //arrayList2[i].Sort64Items(0);
                network.SortItemsAscending(arrayList2[i]);

                Debug.Assert(arrayList2[i].IsSortedAscending());
            }

            t2 = DateTime.Now;

            Console.WriteLine("Sort network: " + (t2 - t1).TotalSeconds);
            Console.WriteLine();
        }

        public static void BenchmarkExample2()
        {
            for (var n = 2; n <= 256; n++)
            {
                var network = 
                    n <= 64
                        ? SortingNetwork.Predefined(n)
                        : SortingNetwork.OddEvenMergesort(n);

                var size = network.SizeInBytes();

                Console.WriteLine($"Size of n: {n:N2} = {size:N} bytes");
            }
        }

        public static void Example3()
        {
            var network = SortingNetwork.BubbleInsert(6);

            Console.WriteLine(network.ToString());
            Console.WriteLine();
        }
    }
}
