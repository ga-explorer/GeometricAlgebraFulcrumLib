using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Records;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms
{
    public class RGaOutermorphismSequence<T> :
        RGaOutermorphismBase<T>,
        IRGaOutermorphismSequence<T>,
        IReadOnlyList<IRGaOutermorphism<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaOutermorphismSequence<T> CreateIdentity(RGaProcessor<T> processor)
        {
            return new RGaOutermorphismSequence<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaOutermorphismSequence<T> Create(RGaProcessor<T> processor, params IRGaOutermorphism<T>[] outermorphismsList)
        {
            return new RGaOutermorphismSequence<T>(processor, outermorphismsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaOutermorphismSequence<T> Create(RGaProcessor<T> processor, IEnumerable<IRGaOutermorphism<T>> outermorphismsList)
        {
            return new RGaOutermorphismSequence<T>(processor, outermorphismsList);
        }
        
        
        private readonly List<IRGaOutermorphism<T>> _outermorphismList;

        public override RGaProcessor<T> Processor { get; }
        
        public int Count 
            => _outermorphismList.Count;

        public IRGaOutermorphism<T> this[int index] 
            => _outermorphismList[index];
        

        private RGaOutermorphismSequence(RGaProcessor<T> processor)
        {
            _outermorphismList = new List<IRGaOutermorphism<T>>();

            Processor = processor;
        }

        private RGaOutermorphismSequence(RGaProcessor<T> processor, IEnumerable<IRGaOutermorphism<T>> versorsList)
        {
            _outermorphismList = new List<IRGaOutermorphism<T>>(versorsList);
            
            Processor = processor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaOutermorphism<T> GetOutermorphism(int index)
        {
            return _outermorphismList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaOutermorphismSequence<T> AppendOutermorphism(IRGaOutermorphism<T> om)
        {
            _outermorphismList.Add(om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaOutermorphismSequence<T> PrependOutermorphism(IRGaOutermorphism<T> om)
        {
            _outermorphismList.Insert(0, om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaOutermorphismSequence<T> InsertOutermorphism(int index, IRGaOutermorphism<T> om)
        {
            _outermorphismList.Insert(index, om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaOutermorphismSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new RGaOutermorphismSequence<T>(
                Processor,
                _outermorphismList.Skip(startIndex).Take(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IRGaOutermorphism<T>> GetOutermorphisms()
        {
            return _outermorphismList;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _outermorphismList.All(om => om.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaOutermorphism<T> GetOmAdjoint()
        {
            return new RGaOutermorphismSequence<T>(
                Processor,
                _outermorphismList.Select(om => om.GetOmAdjoint()).Reverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> OmMapBasisVector(int index)
        {
            var kVector = _outermorphismList[0].OmMapBasisVector(index);

            return _outermorphismList
                .Skip(1)
                .Aggregate(
                    kVector,
                    (v, om) => om.OmMap(v)
                );

            //return _outermorphismsList.OmMapBasisVector(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> OmMapBasisBivector(int index1, int index2)
        {
            var v1 = OmMapBasisVector(index1);
            var v2 = OmMapBasisVector(index2);

            return v1.Op(v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> OmMapBasisBlade(ulong id)
        {
            var kVector = _outermorphismList[0].OmMapBasisBlade(id);

            return _outermorphismList
                .Skip(1)
                .Aggregate(
                    kVector,
                    (v, om) => om.OmMap(v)
                );

            //return _outermorphismsList.OmMapBasisBlade(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> OmMap(RGaVector<T> vector)
        {
            return _outermorphismList.OmMap(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> OmMap(RGaBivector<T> bivector)
        {
            return _outermorphismList.OmMap(bivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
        {
            return _outermorphismList.OmMap(kVector).GetHigherKVectorPart(kVector.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaMultivector<T> OmMap(RGaMultivector<T> multivector)
        {
            return _outermorphismList.OmMap(multivector);
        }
        
        public override IEnumerable<RGaIdVectorRecord<T>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            if (_outermorphismList.Count == 0)
                throw new InvalidOperationException();

            var om1 = _outermorphismList[0];
            var omList = _outermorphismList.Skip(1);

            return om1
                .GetOmMappedBasisVectors(vSpaceDimensions)
                .Select(r => 
                    r with { Vector = omList.OmMap(r.Vector) }
                ).Where(r => !r.Vector.IsZero);
        }

        public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            if (_outermorphismList.Count == 0)
                throw new InvalidOperationException();

            var om1 = _outermorphismList[0];
            var omList = _outermorphismList.Skip(1);

            return om1
                .GetMappedBasisBlades(vSpaceDimensions)
                .Select(r => 
                    new KeyValuePair<ulong, RGaMultivector<T>>(
                        r.Key, 
                        omList.OmMap(r.Value)
                    )
                ).Where(r => !r.Value.IsZero);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IRGaOutermorphism<T>> GetLeafOutermorphisms()
        {
            foreach (var om in _outermorphismList)
            {
                if (om is IRGaOutermorphismSequence<T> omSeq)
                    foreach (var childOm in omSeq.GetLeafOutermorphisms())
                        yield return childOm;
                else
                    yield return om;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<IRGaOutermorphism<T>> GetEnumerator()
        {
            return _outermorphismList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}