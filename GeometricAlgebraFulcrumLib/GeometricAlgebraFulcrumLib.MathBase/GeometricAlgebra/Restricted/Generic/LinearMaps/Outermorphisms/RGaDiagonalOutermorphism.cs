using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms
{
    /// <summary>
    /// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
    /// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
    /// mappings are computed as needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RGaDiagonalOutermorphism<T> : 
        IRGaAutomorphism<T>,
        IReadOnlyDictionary<ulong, T>
    {
        public RGaProcessor<T> Processor 
            => DiagonalVector.Processor;

        public RGaMetric Metric 
            => DiagonalVector.Metric;

        public IScalarProcessor<T> ScalarProcessor 
            => DiagonalVector.ScalarProcessor;

        public RGaVector<T> DiagonalVector { get; }
        
        public int Count 
            => DiagonalVector.Count;
        
        public T this[ulong key] 
            => DiagonalVector[key];

        public IEnumerable<ulong> Keys 
            => DiagonalVector.Keys;

        public IEnumerable<T> Values 
            => DiagonalVector.Values;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaDiagonalOutermorphism(RGaVector<T> diagonalVector)
        {
            DiagonalVector = diagonalVector;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return DiagonalVector.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            return DiagonalVector.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaOutermorphism<T> GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaVector<T> OmMapBasisVector(int index)
        {
            var id = index.BasisVectorIndexToId();

            return DiagonalVector.TryGetTermScalar(id, out var scalar)
                ? Processor.CreateVector(index, scalar)
                : Processor.CreateZeroVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBivector<T> OmMapBasisBivector(int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            var id1 = index1.BasisVectorIndexToId();

            if (!DiagonalVector.TryGetTermScalar(id1, out var scalar1))
                return Processor.CreateZeroBivector();

            var id2 = index2.BasisVectorIndexToId();

            return !DiagonalVector.TryGetTermScalar(id2, out var scalar2)
                ? Processor.CreateZeroBivector()
                : Processor.CreateBivector(
                    BasisBivectorUtils.IndexPairToBivectorId(index1, index2), 
                    ScalarProcessor.Times(scalar1, scalar2)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKVector<T> OmMapBasisBlade(ulong id)
        {
            var scalar = ScalarProcessor.ScalarOne;

            if (id == 0UL)
                return Processor.CreateScalar(scalar);

            if (id.IsBasisVector())
                return OmMapBasisVector(id.FirstOneBitPosition());

            if (id.IsBasisBivector())
                return OmMapBasisBivector(
                    id.FirstOneBitPosition(),
                    id.LastOneBitPosition()
                );

            foreach (var index in id.PatternToPositions())
            {
                if (!DiagonalVector.TryGetTermScalar(index.BasisVectorIndexToId(), out var s))
                    return Processor.CreateZeroScalar();

                scalar = ScalarProcessor.Times(scalar, s);
            }
            
            return Processor.CreateKVector(id, scalar);
        }
        

        public RGaVector<T> OmMap(RGaVector<T> vector)
        {
            var composer = Processor.CreateComposer();

            if (Count <= vector.Count)
            {
                foreach (var (id, mv) in DiagonalVector)
                {
                    if (!vector.TryGetTermScalar(id, out var scalar))
                        continue;

                    composer.AddTerm(id, mv, scalar);
                }
            }
            else
            {
                foreach (var (id, scalar) in vector)
                {
                    if (!DiagonalVector.TryGetValue(id, out var mv))
                        continue;

                    composer.AddTerm(id, mv, scalar);
                }
            }

            return composer.GetVector();
        }

        public RGaBivector<T> OmMap(RGaBivector<T> bivector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in bivector)
            {
                var bv = OmMapBasisBivector(
                    id.FirstOneBitPosition(),
                    id.LastOneBitPosition()
                );

                if (bv.IsZero)
                    continue;

                composer.AddMultivector(bv, scalar);
            }

            return composer.GetBivector();
        }

        public RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in kVector)
            {
                var mv = OmMapBasisBlade(id);

                if (mv.IsZero)
                    continue;

                composer.AddMultivector(mv, scalar);
            }

            return composer.GetHigherKVector(kVector.Grade);
        }

        public RGaKVector<T> OmMap(RGaKVector<T> kVector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in kVector)
            {
                var mv = OmMapBasisBlade(id);

                if (mv.IsZero)
                    continue;

                composer.AddMultivector(mv, scalar);
            }

            return composer.GetKVector(kVector.Grade);
        }

        public RGaMultivector<T> OmMap(RGaMultivector<T> multivector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in multivector)
            {
                var mv = OmMapBasisBlade(id);

                if (mv.IsZero)
                    continue;

                composer.AddMultivector(mv, scalar);
            }

            return composer.GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaUnilinearMap<T> GetAdjoint()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaMultivector<T> Map(RGaMultivector<T> multivector)
        {
            return OmMap(multivector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return DiagonalVector
                .Ids
                .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaMultivector<T>>(
                        id, 
                        OmMapBasisBlade(id)
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaIdVectorRecord<T>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return DiagonalVector
                .Ids
                .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
                .Select(id => 
                    new RGaIdVectorRecord<T>(
                        id, 
                        OmMapBasisVector(id.FirstOneBitPosition())
                    )
                );
        }

        public LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            return DiagonalVector.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}