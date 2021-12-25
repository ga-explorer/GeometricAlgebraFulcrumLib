using System;
using System.Collections.Generic;
using System.Linq;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;
using NumericalGeometryLib.GeometricAlgebra.Structures;
using NumericalGeometryLib.Random;
using NUnit.Framework;

namespace NumericalGeometryLib.Tests
{
    public class ProductBinaryTriesTests
    {
        private static readonly GaBasisSet BasisSet 
            = GaBasisSet.Create(2, 2, 2);

        private static readonly RandomGaMultivectorComposer RandomComposer
            = new RandomGaMultivectorComposer(BasisSet, 10);

        private readonly List<GaMultivectorSparseList> _sparseLists 
            = new List<GaMultivectorSparseList>();

        private readonly List<GaMultivectorBinaryTrie> _binaryTries
            = new List<GaMultivectorBinaryTrie>();


        private GaMultivector GetProductDifference(int i, int j, Func<GaMultivectorSparseList, GaMultivectorSparseList, IEnumerable<GaIdScalarRecord>> sparseListFunc, Func<GaMultivectorBinaryTrie, GaMultivectorBinaryTrie, IEnumerable<GaIdScalarRecord>> binaryTrieFunc)
        {
            var mv1 = BasisSet.SumToMultivector(
                sparseListFunc(_sparseLists[i], _sparseLists[j])
            );

            var mv2 = BasisSet.SumToMultivector(
                binaryTrieFunc(_binaryTries[i], _binaryTries[j])
            );

            return mv1 - mv2;
        }

        private GaMultivector GetOpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetOpIdScalarRecords, 
                BasisSet.GetOpIdScalarRecords
            );
        }

        private GaMultivector GetEGpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetEGpIdScalarRecords, 
                BasisSet.GetEGpIdScalarRecords
            );
        }
        
        private GaMultivector GetELcpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetELcpIdScalarRecords, 
                BasisSet.GetELcpIdScalarRecords
            );
        }
        
        private GaMultivector GetERcpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetERcpIdScalarRecords, 
                BasisSet.GetERcpIdScalarRecords
            );
        }
        
        private GaMultivector GetEFdpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetEFdpIdScalarRecords, 
                BasisSet.GetEFdpIdScalarRecords
            );
        }
        
        private GaMultivector GetEHipDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetEHipIdScalarRecords, 
                BasisSet.GetEHipIdScalarRecords
            );
        }

        private GaMultivector GetEAcpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetEAcpIdScalarRecords, 
                BasisSet.GetEAcpIdScalarRecords
            );
        }

        private GaMultivector GetECpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetECpIdScalarRecords, 
                BasisSet.GetECpIdScalarRecords
            );
        }
        
        private GaMultivector GetGpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetGpIdScalarRecords, 
                BasisSet.GetGpIdScalarRecords
            );
        }
        
        private GaMultivector GetLcpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetLcpIdScalarRecords, 
                BasisSet.GetLcpIdScalarRecords
            );
        }
        
        private GaMultivector GetRcpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetRcpIdScalarRecords, 
                BasisSet.GetRcpIdScalarRecords
            );
        }
        
        private GaMultivector GetFdpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetFdpIdScalarRecords, 
                BasisSet.GetFdpIdScalarRecords
            );
        }
        
        private GaMultivector GetHipDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetHipIdScalarRecords, 
                BasisSet.GetHipIdScalarRecords
            );
        }

        private GaMultivector GetAcpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetAcpIdScalarRecords, 
                BasisSet.GetAcpIdScalarRecords
            );
        }

        private GaMultivector GetCpDifference(int i, int j)
        {
            return GetProductDifference(
                i, 
                j, 
                BasisSet.GetCpIdScalarRecords, 
                BasisSet.GetCpIdScalarRecords
            );
        }
        

        [SetUp]
        public void Setup()
        {
            var mvList = new List<GaMultivector>();

            for (var grade = 0U; grade <= BasisSet.VSpaceDimension; grade++)
            {
                mvList.Add(RandomComposer.GetKVector(grade));
                mvList.Add(RandomComposer.GetSparseKVector(grade));
            }

            mvList.Add(RandomComposer.GetMultivector());
            mvList.Add(RandomComposer.GetSparseMultivector());

            _sparseLists.AddRange(mvList.Select(mv => mv.GetSparseList()));
            _binaryTries.AddRange(mvList.Select(mv => mv.GetBinaryTrie()));
        }
        
        [Test]
        public void TestBinaryTriesConstruction()
        {
            var n = _sparseLists.Count;

            for (var i = 0; i < n; i++)
            {
                var mvSparseList1 = _sparseLists[i];
                var mvBinaryTrie1 = _binaryTries[i];

                var mv1 = BasisSet.SumToMultivector(
                    mvSparseList1.GetIdScalarRecords()
                );

                var mv2 = BasisSet.SumToMultivector(
                    mvBinaryTrie1.GetIdScalarRecords()
                );

                Assert.IsTrue((mv1 - mv2).IsNearZero());
            }
        }

        [Test]
        public void TestProducts()
        {
            var n = _sparseLists.Count;

            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
            {
                Assert.IsTrue(GetOpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetEGpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetELcpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetERcpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetEFdpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetEHipDifference(i, j).IsNearZero());
                Assert.IsTrue(GetEAcpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetECpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetGpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetLcpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetRcpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetFdpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetHipDifference(i, j).IsNearZero());
                Assert.IsTrue(GetAcpDifference(i, j).IsNearZero());
                Assert.IsTrue(GetCpDifference(i, j).IsNearZero());
            }
        }
    }
}