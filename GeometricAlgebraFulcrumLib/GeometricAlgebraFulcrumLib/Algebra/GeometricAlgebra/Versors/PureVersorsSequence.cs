using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public sealed class PureVersorsSequence<T> : 
        VersorBase<T>, 
        IReadOnlyList<PureVersor<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> CreateIdentity(IGeometricAlgebraProcessor<T> processor)
        {
            return new PureVersorsSequence<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, params VectorStorage<T>[] vectorStorages)
        {
            return new PureVersorsSequence<T>(
                processor,
                vectorStorages.Select(v => 
                    new PureVersor<T>(processor, v)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, IEnumerable<VectorStorage<T>> vectorStorages)
        {
            return new PureVersorsSequence<T>(
                processor,
                vectorStorages.Select(v => 
                    new PureVersor<T>(processor, v)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, params PureVersor<T>[] versors)
        {
            return new PureVersorsSequence<T>(processor, versors);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, IEnumerable<PureVersor<T>> versors)
        {
            return new PureVersorsSequence<T>(processor, versors);
        }


        private readonly List<PureVersor<T>> _versorsList;
        
        
        public int Count 
            => _versorsList.Count;

        public PureVersor<T> this[int index]
        {
            get => _versorsList[index];
            set => _versorsList[index] = 
                value 
                ?? throw new ArgumentNullException(nameof(value));
        }


        private PureVersorsSequence([NotNull] IGeometricAlgebraProcessor<T> processor)
            : base(processor)
        {
            _versorsList = new List<PureVersor<T>>();
        }

        private PureVersorsSequence([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IEnumerable<PureVersor<T>> versorsList)
            : base(processor)
        {
            _versorsList = new List<PureVersor<T>>(versorsList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureVersor<T> GetVersor(int index)
        {
            return _versorsList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureVersorsSequence<T> AppendVersor([NotNull] PureVersor<T> versor)
        {
            _versorsList.Add(versor);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureVersorsSequence<T> PrependVersor([NotNull] PureVersor<T> versor)
        {
            _versorsList.Insert(0, versor);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureVersorsSequence<T> InsertVersor(int index, [NotNull] PureVersor<T> versor)
        {
            _versorsList.Insert(index, versor);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureVersorsSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new PureVersorsSequence<T>(
                GeometricProcessor,
                _versorsList.Skip(startIndex).Take(count)
            );
        }

        public IEnumerable<IMultivectorStorage<T>> GetReflections([NotNull] IMultivectorStorage<T> storage)
        {
            var v = storage;

            yield return v;

            foreach (var versor in _versorsList)
            {
                v = versor.MapMultivector(v);

                yield return v;
            }
        }


        public IEnumerable<T[,]> GetReflectionArrays(int rowsCount)
        {
            var f = 
                GeometricProcessor.CreateBasisFreeFrame((uint) rowsCount);

            yield return f.GetArray(rowsCount);

            foreach (var versor in _versorsList)
                yield return versor.OmMapFreeFrame(f).GetArray(rowsCount);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Versor<T> GetFinalVersor()
        {
            var storage = _versorsList
                .Skip(1)
                .Select(r => r.Vector)
                .Aggregate(
                    (IMultivectorStorage<T>) _versorsList[0].Vector, 
                    (current, rotor) => 
                        GeometricProcessor.Gp(rotor, current)
                );

            return new Versor<T>(GeometricProcessor, storage);
        }

        public PureRotorsSequence<T> CreatePureRotorsSequence()
        {
            if (_versorsList.Count % 2 != 0)
                throw new InvalidOperationException();

            var rotorsCount = _versorsList.Count / 2;

            var simpleRotorsArray = new PureRotor<T>[rotorsCount];

            for (var i = 0; i < rotorsCount; i++)
            {
                var v1 = _versorsList[2 * i + 1].Vector;
                var v2 = _versorsList[2 * i].Vector;

                var scalar = GeometricProcessor.Sp(v1, v2);
                var bivector = GeometricProcessor.Op(v1, v2);

                simpleRotorsArray[i] = new PureRotor<T>(GeometricProcessor, scalar, bivector);
            }

            return PureRotorsSequence<T>.Create(
                GeometricProcessor,
                simpleRotorsArray
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _versorsList.All(versor => versor.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapVector(VectorStorage<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMapVector(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMapBivector(bv)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMapKVector(kv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (current, rotor) => rotor.OmMapMultivector(current)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (current, rotor) => rotor.OmMapMultivector(current)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return new PureVersorsSequence<T>(
                GeometricProcessor,
                _versorsList
                    .Select(r => r.GetPureDualVersorInverse())
                    .Reverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return _versorsList
                .Select(r => r.Vector)
                .Aggregate(
                    (IMultivectorStorage<T>) GeometricProcessor.CreateKVectorBasisScalarStorage(),
                    GeometricProcessor.Gp
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorInverseStorage()
        {
            return _versorsList
                .Select(r => r.VectorInverse)
                .Reverse()
                .Aggregate(
                    (IMultivectorStorage<T>) GeometricProcessor.CreateKVectorBasisScalarStorage(),
                    GeometricProcessor.Gp
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<PureVersor<T>> GetEnumerator()
        {
            return _versorsList.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IOutermorphism<T>> GetOutermorphisms()
        {
            return _versorsList;
        }
    }
}