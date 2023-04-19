using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors
{
    public sealed partial class XGaFloat64Vector :
        XGaFloat64KVector
    {
        private readonly IReadOnlyDictionary<IIndexSet, double> _idScalarDictionary;


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

        public override IEnumerable<IIndexSet> Ids
            => _idScalarDictionary.Keys;

        public override IEnumerable<double> Scalars
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _idScalarDictionary.Values;
        }
        
        public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
            => _idScalarDictionary.Select(p => 
                new KeyValuePair<int, double>(p.Key.FirstIndex, p.Value)
            );

        public override IEnumerable<KeyValuePair<IIndexSet, double>> IdScalarPairs
            => _idScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Vector(XGaFloat64Processor metric)
            : base(metric)
        {
            _idScalarDictionary = new EmptyDictionary<IIndexSet, double>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Vector(XGaFloat64Processor metric, KeyValuePair<IIndexSet, double> basisScalarPair)
            : base(metric)
        {
            _idScalarDictionary =
                new SingleItemDictionary<IIndexSet, double>(basisScalarPair);

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Vector(XGaFloat64Processor metric, IReadOnlyDictionary<IIndexSet, double> idScalarDictionary)
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
        public override IReadOnlyDictionary<IIndexSet, double> GetIdScalarDictionary()
        {
            return _idScalarDictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(IIndexSet key)
        {
            return !IsZero && _idScalarDictionary.ContainsKey(key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar GetScalarPart()
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector GetVectorPart()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector GetBivectorPart()
        {
            return Processor.CreateZeroBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
        {
            return Processor.CreateZeroHigherKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = 
                _idScalarDictionary.Where(term => 
                    filterFunc(term.Key.FirstIndex)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetPart(Func<IIndexSet, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetPart(Func<double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetPart(Func<IIndexSet, double, bool> filterFunc)
        {
            if (IsZero) return this;

            var idScalarDictionary = _idScalarDictionary
                .Where(
                    p => filterFunc(p.Key, p.Value)
                ).ToDictionary();

            return Processor.CreateVector(idScalarDictionary);
        }


        public override IEnumerable<XGaBasisBlade> BasisBlades
            => _idScalarDictionary.Keys.Select(Metric.CreateBasisBlade);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalarTermScalar()
        {
            return 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetTermScalar(IIndexSet basisBladeId)
        {
            return basisBladeId.IsSingleIndexSet &&
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
        public override bool TryGetTermScalar(IIndexSet basisBladeId, out double scalar)
        {
            if (basisBladeId.IsSingleIndexSet && _idScalarDictionary.TryGetValue(basisBladeId, out scalar))
                return true;

            scalar = 0d;
            return false;
        }


        public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
        {
            get
            {
                return _idScalarDictionary.Select(p =>
                    new KeyValuePair<XGaBasisBlade, double>(
                        Metric.CreateBasisBlade(p.Key),
                        p.Value
                    )
                );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : this;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return BasisScalarPairs
                .OrderBy(p => p.Key.Id.Count)
                .ThenBy(p => p.Key.Id)
                .Select(p => $"'{p.Value:G}'{p.Key}")
                .Concatenate(" + ");
        }
    }
}