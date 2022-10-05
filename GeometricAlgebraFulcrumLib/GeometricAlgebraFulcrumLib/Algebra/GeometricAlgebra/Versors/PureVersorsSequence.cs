using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
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
        IOutermorphismSequence<T>,
        IReadOnlyList<PureVersor<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> CreateIdentity(IGeometricAlgebraProcessor<T> processor)
        {
            return new PureVersorsSequence<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, params GaVector<T>[] vectorStorages)
        {
            return new PureVersorsSequence<T>(
                vectorStorages.Select(PureVersor<T>.Create).ToList()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(IEnumerable<GaVector<T>> vectorStorages)
        {
            return new PureVersorsSequence<T>(
                vectorStorages.Select(PureVersor<T>.Create).ToList()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(params PureVersor<T>[] versors)
        {
            return new PureVersorsSequence<T>(versors.ToList());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> Create(IEnumerable<PureVersor<T>> versors)
        {
            return new PureVersorsSequence<T>(versors.ToList());
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

        private PureVersorsSequence([NotNull] List<PureVersor<T>> versorsList)
            : base(versorsList.First().GeometricProcessor)
        {
            _versorsList = versorsList;
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
                _versorsList.Skip(startIndex).Take(count).ToList()
            );
        }

        public IEnumerable<IMultivectorStorage<T>> GetReflections([NotNull] IMultivectorStorage<T> storage)
        {
            var v = storage;

            yield return v;

            foreach (var versor in _versorsList)
            {
                v = versor.Map(v);

                yield return v;
            }
        }


        public IEnumerable<T[,]> GetReflectionArrays(int rowsCount)
        {
            var f = 
                GeometricProcessor.CreateFreeFrameOfBasis((uint) rowsCount);

            yield return f.GetArray(rowsCount);

            foreach (var versor in _versorsList)
                yield return versor.OmMap(f).GetArray(rowsCount);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Versor<T> GetFinalVersor()
        {
            var storage = 
                _versorsList
                    .Skip(1)
                    .Select(r => r.Vector.AsMultivector())
                    .Aggregate(
                        _versorsList[0].Vector.AsMultivector(), 
                        (current, rotor) => rotor.Gp(current)
                    );

            return Versor<T>.Create(storage);
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

                var scalar = v1.Sp(v2);
                var bivector = v1.Op(v2);

                simpleRotorsArray[i] = PureRotor<T>.Create(scalar, bivector);
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
        public override GaVector<T> OmMap(GaVector<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMap(GaBivector<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (bv, rotor) => rotor.OmMap(bv)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMap(GaKVector<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (kv, rotor) => rotor.OmMap(kv)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> OmMap(GaMultivector<T> mv)
        {
            return _versorsList
                .Aggregate(
                    mv, 
                    (current, rotor) => rotor.OmMap(current)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return new PureVersorsSequence<T>(
                _versorsList
                    .Select(r => r.GetPureDualVersorInverse())
                    .Reverse()
                    .ToList()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivector()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorage()
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorReverse()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorageReverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorInverse()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorageInverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return _versorsList
                .Select(r => r.Vector.VectorStorage)
                .Aggregate(
                    (IMultivectorStorage<T>) GeometricProcessor.CreateKVectorStorageBasisScalar(),
                    GeometricProcessor.Gp
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageReverse()
        {
            return _versorsList
                .Select(r => r.Vector.VectorStorage)
                .Reverse()
                .Aggregate(
                    (IMultivectorStorage<T>) GeometricProcessor.CreateKVectorStorageBasisScalar(),
                    GeometricProcessor.Gp
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageInverse()
        {
            return _versorsList
                .Select(r => r.VectorInverse.VectorStorage)
                .Reverse()
                .Aggregate(
                    (IMultivectorStorage<T>) GeometricProcessor.CreateKVectorStorageBasisScalar(),
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
        public IEnumerable<IOutermorphism<T>> GetLeafOutermorphisms()
        {
            return _versorsList;
        }
    }
}