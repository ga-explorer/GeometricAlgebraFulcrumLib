using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors
{
    public sealed partial class RGaFloat64Vector :
        RGaFloat64KVector
    {
        private readonly IReadOnlyDictionary<ulong, double> _idScalarDictionary;


        public override string MultivectorClassName
            => "Generic Vector";

        public override int Count
            => _idScalarDictionary.Count;

        public override IEnumerable<int> KVectorGrades
        {
            get
            {
                if (!IsZero) yield return 1;
            }
        }

        public override int Grade
            => 1;

        public override bool IsZero
            => _idScalarDictionary.Count == 0;

        public override IEnumerable<ulong> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<double> Scalars
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _idScalarDictionary.Values;
        }
        
        public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
            => _idScalarDictionary.Select(p => 
                new KeyValuePair<int, double>(p.Key.FirstOneBitPosition(), p.Value)
            );

        public override IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64Vector(RGaFloat64Processor metric)
            : base(metric)
        {
            _idScalarDictionary = new EmptyDictionary<ulong, double>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64Vector(RGaFloat64Processor metric, KeyValuePair<ulong, double> basisScalarPair)
            : base(metric)
        {
            _idScalarDictionary =
                new SingleItemDictionary<ulong, double>(basisScalarPair);

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64Vector(RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> idScalarDictionary)
            : base(metric)
        {
            _idScalarDictionary = idScalarDictionary;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _idScalarDictionary.IsValidVectorDictionary();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyDictionary<ulong, double> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(ulong key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar GetScalarPart()
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector GetVectorPart()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector GetHigherKVectorPart(int grade)
        {
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = 
                _idScalarDictionary.Where(term => 
                    filterFunc(term.Key.FirstOneBitPosition())
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetPart(Func<ulong, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetPart(Func<double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetPart(Func<ulong, double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }


        public override IEnumerable<RGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalarTermScalar()
        {
            return 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetTermScalar(ulong basisBladeId)
        {
            return basisBladeId.IsBasisVector() &&
                   _idScalarDictionary.TryGetValue(basisBladeId, out var scalar)
                ? scalar
                : 0d;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalarTermScalar(out double scalar)
        {
            scalar = 0d;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetTermScalar(ulong basisBladeId, out double scalar)
        {
            if (basisBladeId.IsBasisVector() && _idScalarDictionary.TryGetValue(basisBladeId, out scalar))
                return true;

            scalar = 0d;
            return false;
        }


        public override IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisScalarPairs
        {
            get
            {
                return _idScalarDictionary.Select(p =>
                    new KeyValuePair<RGaBasisBlade, double>(
                        Processor.CreateBasisBlade(p.Key),
                        p.Value
                    )
                );
            }
        }

        
        public IRGaSignedBasisBlade GetDominantBasisBlade()
        {
            if (_idScalarDictionary.Count == 0)
                return new RGaBasisBlade(Metric, 0);

            ulong dominantId = 0;
            var dominantScalar = 0d;

            foreach (var (id, scalar) in _idScalarDictionary)
            {
                var absScalar = scalar.Abs();

                if (absScalar <= dominantScalar) 
                    continue;

                //if (absScalar == dominantScalar && id.Grade() <= dominantId.Grade()) 
                //    continue;

                dominantId = id;
                dominantScalar = absScalar;
            }

            return new RGaBasisBlade(Metric, dominantId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetNormalVector()
        {
            return this.VectorToLinVector().GetUnitNormal().ToRGaFloat64Vector(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : this;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return BasisScalarPairs
                .OrderBy(p => p.Key.Id.Grade())
                .ThenBy(p => p.Key.Id)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}