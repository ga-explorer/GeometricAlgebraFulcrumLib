using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;
using MathNet.Numerics;
using NUnit.Framework;

namespace GeometricAlgebraLib.UnitTests.Storages
{
    [TestFixture]
    public sealed class GaBladeRelationsTests
    {
        private static readonly Random RandomGenerator 
            = new Random(10);

        private readonly IGaScalarProcessor<double> _scalarsDomain
            = GaScalarProcessorFloat64.DefaultProcessor;

        private readonly List<GaVectorStorage<double>> _vectorsList
            = new List<GaVectorStorage<double>>();

        private readonly List<IGaKVectorStorage<double>> _bladesList
            = new List<IGaKVectorStorage<double>>();

        private readonly double _scalar
            = RandomGenerator.NextDouble();

        private GaScalarTermStorage<double> _scalarStorage;


        public int VSpaceDimension { get; }
            = 8;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        private Dictionary<ulong, double> GetRandomKVectorDictionary(int grade)
        {
            return Enumerable
                .Range(0, (int)GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade))
                .ToDictionary(
                    index => (ulong)index, 
                    _ => RandomGenerator.NextDouble()
                );
        }

        private IGaKVectorStorage<double> GetRandomBlade(int grade)
        {
            if (grade == 0)
                return GaScalarTermStorage<double>.Create(
                    _scalarsDomain, 
                    RandomGenerator.NextDouble()
                );

            var vectors =
                _vectorsList
                    .SelectPermutation(RandomGenerator)
                    .Take(grade);

            return _scalarsDomain.Op(vectors);
        }

        [OneTimeSetUp]
        public void ClassInit()
        {
            for (var i = 0; i < 20; i++)
                _vectorsList.Add(
                    GaVectorStorage<double>.Create(
                        _scalarsDomain, 
                        GetRandomKVectorDictionary(1)
                    )
                );

            for (var grade = 0; grade <= VSpaceDimension; grade++)
                _bladesList.Add(GetRandomBlade(grade));

            _scalarStorage
                = GaScalarTermStorage<double>.Create(_scalarsDomain, _scalar);
        }

        [Test]
        public void AssertScaling()
        {
            IGaMultivectorStorage<double> blade2;
            IGaMultivectorStorage<double> diff;

            foreach (var blade1 in _bladesList)
            {
                blade2 = blade1.Times(_scalar).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = _scalar.Times(blade1).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = _scalarStorage.Op(blade1).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = blade1.Op(_scalarStorage).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = _scalarStorage.EGp(blade1).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = blade1.EGp(_scalarStorage).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());
            }
        }
    }
}