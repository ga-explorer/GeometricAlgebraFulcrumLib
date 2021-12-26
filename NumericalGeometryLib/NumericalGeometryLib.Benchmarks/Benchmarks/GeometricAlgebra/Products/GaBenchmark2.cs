using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.GuidedBinaryTraversal;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;
using NumericalGeometryLib.GeometricAlgebra.Structures;
using NumericalGeometryLib.Random;

namespace NumericalGeometryLib.Benchmarks.Benchmarks.GeometricAlgebra.Products
{
    public class GaBenchmark2
    {
        private readonly List<GaMultivectorSparseList> _sparseLists
            = new List<GaMultivectorSparseList>();

        private readonly List<GaMultivectorBinaryTrie> _binaryTries
            = new List<GaMultivectorBinaryTrie>();
        
        private GaGbtMultivectorProductsBinaryTrieStack[,] _binaryTrieStacksArray;

        
        public BasisBladeSet BasisSet { get; private set; }

        [Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        public uint VSpaceDimension { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            BasisSet = BasisBladeSet.CreateEuclidean(VSpaceDimension);

            var randGen = new RandomGaMultivectorComposer(BasisSet, 10);

            var mv = randGen.GetMultivector();

            _sparseLists.Add(mv.GetSparseList());
            _binaryTries.Add(mv.GetBinaryTrie());

            _binaryTrieStacksArray =
                new GaGbtMultivectorProductsBinaryTrieStack[_binaryTries.Count, _binaryTries.Count];

            for (var i = 0; i < _binaryTries.Count; i++)
            for (var j = 0; j < _binaryTries.Count; j++)
                _binaryTrieStacksArray[i, j] = 
                    BasisSet.CreateGbtProductsStack(
                        _binaryTries[i], 
                        _binaryTries[j]
                    );
        }

        [Benchmark(Baseline = true)]
        public int EGpSparseList()
        {
            var count = 0;

            foreach (var mv1 in _sparseLists)
            foreach (var mv2 in _sparseLists)
            {
                count += BasisSet.GetEGpIdScalarRecords(mv1, mv2).Count();
            }

            return count;
        }
        
        [Benchmark]
        public int EGpBinaryTrie()
        {
            var count = 0;

            for (var i = 0; i < _binaryTries.Count; i++)
            {
                //var mv1 = _binaryTries[i];

                for (var j = 0; j < _binaryTries.Count; j++)
                {
                    //var mv2 = _binaryTries[j];

                    var stack = _binaryTrieStacksArray[i, j];

                    count += stack.GetEGpIdScalarRecords().Count();
                }
            }

            return count;
        }
    }
}
