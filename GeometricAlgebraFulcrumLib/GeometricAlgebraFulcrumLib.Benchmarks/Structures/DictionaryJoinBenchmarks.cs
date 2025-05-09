using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Structures;

[SimpleJob]
public class DictionaryJoinBenchmarks
{
    public Dictionary<int, double> Dictionary1 { get; private set; }

    public Dictionary<int, double> Dictionary2 { get; private set; }


    [GlobalSetup]
    public void Setup()
    {
        Dictionary1 = new Dictionary<int, double>();
        Dictionary2 = new Dictionary<int, double>();

        var random = new Random();

        for (var i = 0; i < 7500; i++)
        {
            if (i < 5000)
                Dictionary1.Add(
                    i * 2,
                    random.NextDouble()
                );

            if (i >= 2500)
                Dictionary2.Add(
                    i * 2,
                    random.NextDouble()
                );
        }
    }

    /// <summary>
    /// This is significantly faster that the Linq Join method
    /// </summary>
    [Benchmark]
    public Dictionary<int, double> Join1()
    {
        var dict = new Dictionary<int, double>();
            
        foreach (var (key, value1) in Dictionary1)
        {
            if (Dictionary2.TryGetValue(key, out var value2))
                dict.Add(
                    key,
                    value1 + value2
                );
        }

        return dict;
    }
        
    [Benchmark]
    public Dictionary<int, double> Join2()
    {
        return Dictionary1.Join(
            Dictionary2,
            keyValuePair1 => keyValuePair1.Key,
            keyValuePair2 => keyValuePair2.Key,
            (keyValuePair1, keyValuePair2) =>
                new KeyValuePair<int, double>(
                    keyValuePair1.Key, 
                    keyValuePair1.Value + keyValuePair2.Value
                )
        ).ToDictionary(
            p => p.Key,
            p => p.Value
        );
    }
}