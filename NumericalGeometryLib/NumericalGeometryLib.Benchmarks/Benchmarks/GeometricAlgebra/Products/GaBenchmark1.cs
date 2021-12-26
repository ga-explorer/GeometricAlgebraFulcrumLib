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
    public class GaBenchmark1
    {
        public const uint VSpaceDimension = 12;

        private GaMultivectorSparseList _mvSparseList1;
        private GaMultivectorSparseList _mvSparseList2;

        private GaMultivectorBinaryTrie _mvBinaryTrie1;
        private GaMultivectorBinaryTrie _mvBinaryTrie2;
        
        private GaGbtMultivectorProductsBinaryTrieStack _binaryTrieStack;

        
        public BasisBladeSet BasisSet { get; private set; }

        public static IEnumerable<int> TermsCountSource 
            => Enumerable.Range(0, 1 + (int) VSpaceDimension).Select(n => 1 << n);

        [ParamsSource(nameof(TermsCountSource))]
        public int TermsCount { get; set; } = 1;
        

        [GlobalSetup]
        public void Setup()
        {
            var n = VSpaceDimension / 3;
            BasisSet = BasisBladeSet.Create(n, n, n);

            var randGen = new RandomGaMultivectorComposer(BasisSet, 10);

            var mv1 = randGen.GetSparseMultivector(TermsCount);
            var mv2 = randGen.GetSparseMultivector(TermsCount);

            _mvSparseList1 = mv1.GetSparseList();
            _mvBinaryTrie1 = mv1.GetBinaryTrie();

            _mvSparseList2 = mv2.GetSparseList();
            _mvBinaryTrie2 = mv2.GetBinaryTrie();

            _binaryTrieStack =
                BasisSet.CreateGbtProductsStack(
                    _mvBinaryTrie1, 
                    _mvBinaryTrie2
                );
        }

        
        [Benchmark(Baseline = true)]
        public int EGpSparseList()
        {
            return BasisSet.GetEGpIdScalarRecords(
                _mvSparseList1, 
                _mvSparseList2
            ).Count();
        }
        
        [Benchmark]
        public int EGpBinaryTrie()
        {
            return _binaryTrieStack.GetEGpIdScalarRecords().Count();
        }

        [Benchmark]
        public int GpSparseList()
        {
            return BasisSet.GetGpIdScalarRecords(
                _mvSparseList1, 
                _mvSparseList2
            ).Count();
        }
        
        [Benchmark]
        public int GpBinaryTrie()
        {
            return _binaryTrieStack.GetGpIdScalarRecords().Count();
        }
        
        [Benchmark]
        public int OpSparseList()
        {
            return BasisSet.GetOpIdScalarRecords(
                _mvSparseList1, 
                _mvSparseList2
            ).Count();
        }
        
        [Benchmark]
        public int OpBinaryTrie()
        {
            return _binaryTrieStack.GetOpIdScalarRecords().Count();
        }
        
        [Benchmark]
        public int ELcpSparseList()
        {
            return BasisSet.GetELcpIdScalarRecords(
                _mvSparseList1, 
                _mvSparseList2
            ).Count();
        }
        
        [Benchmark]
        public int ELcpBinaryTrie()
        {
            return _binaryTrieStack.GetELcpIdScalarRecords().Count();
        }

        [Benchmark]
        public int LcpSparseList()
        {
            return BasisSet.GetLcpIdScalarRecords(
                _mvSparseList1, 
                _mvSparseList2
            ).Count();
        }
        
        [Benchmark]
        public int LcpBinaryTrie()
        {
            return _binaryTrieStack.GetLcpIdScalarRecords().Count();
        }
        
        [Benchmark]
        public int ESpSparseList()
        {
            return BasisSet.GetESpScalars(
                _mvSparseList1, 
                _mvSparseList2
            ).Count();
        }
        
        [Benchmark]
        public int ESpBinaryTrie()
        {
            return _binaryTrieStack.GetESpScalars().Count();
        }

        [Benchmark]
        public int SpSparseList()
        {
            return BasisSet.GetSpScalars(
                _mvSparseList1, 
                _mvSparseList2
            ).Count();
        }
        
        [Benchmark]
        public int SpBinaryTrie()
        {
            return _binaryTrieStack.GetSpScalars().Count();
        }
    }
}