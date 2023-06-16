using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms
{
    /// <summary>
    /// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
    /// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
    /// mappings are computed as needed.
    /// </summary>
    public sealed class XGaFloat64DiagonalOutermorphism : 
        IXGaFloat64Automorphism,
        IReadOnlyDictionary<IIndexSet, double>
    {
        public XGaMetric Metric 
            => DiagonalVector.Processor;
        
        public XGaFloat64Processor Processor 
            => DiagonalVector.Processor;
        
        public XGaFloat64Vector DiagonalVector { get; }
        
        public int VSpaceDimensions 
            => DiagonalVector.VSpaceDimensions;

        public int Count 
            => DiagonalVector.Count;
        
        public double this[IIndexSet key] 
            => DiagonalVector[key];

        public IEnumerable<IIndexSet> Keys 
            => DiagonalVector.Keys;

        public IEnumerable<double> Values 
            => DiagonalVector.Values;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64DiagonalOutermorphism(XGaFloat64Vector diagonalVector)
        {
            DiagonalVector = diagonalVector;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(IIndexSet key)
        {
            return DiagonalVector.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(IIndexSet key, out double value)
        {
            return DiagonalVector.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaFloat64Outermorphism GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector OmMapBasisVector(int index)
        {
            var id = index.IndexToIndexSet();

            return DiagonalVector.TryGetTermScalar(id, out var scalar)
                ? Processor.CreateVector(index, scalar)
                : Processor.CreateZeroVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            var id1 = index1.IndexToIndexSet();

            if (!DiagonalVector.TryGetTermScalar(id1, out var scalar1))
                return Processor.CreateZeroBivector();

            var id2 = index2.IndexToIndexSet();

            return !DiagonalVector.TryGetTermScalar(id2, out var scalar2)
                ? Processor.CreateZeroBivector()
                : Processor.CreateBivector(
                    IndexSetUtils.IndexPairToIndexSet(index1, index2), 
                    scalar1 * scalar2
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector OmMapBasisBlade(IIndexSet id)
        {
            var scalar = 1d;

            if (id.IsEmptySet)
                return Processor.CreateScalar(scalar);

            if (id.IsSingleIndexSet)
                return OmMapBasisVector(id.FirstIndex);

            if (id.IsIndexPairSet)
                return OmMapBasisBivector(
                    id.FirstIndex,
                    id.LastIndex
                );

            foreach (var index in id)
            {
                if (!DiagonalVector.TryGetTermScalar(index.IndexToIndexSet(), out var s))
                    return Processor.CreateZeroScalar();

                scalar *= s;
            }
            
            return Processor.CreateKVector(id, scalar);
        }
        

        public XGaFloat64Vector OmMap(XGaFloat64Vector vector)
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

        public XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in bivector)
            {
                var bv = OmMapBasisBivector(
                    id.FirstIndex,
                    id.LastIndex
                );

                if (bv.IsZero)
                    continue;

                composer.AddMultivector(bv, scalar);
            }

            return composer.GetBivector();
        }

        public XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
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

        public XGaFloat64KVector OmMap(XGaFloat64KVector kVector)
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

        public XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
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
        public IXGaFloat64UnilinearMap GetAdjoint()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapBasisBlade(IIndexSet id)
        {
            return OmMapBasisBlade(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
        {
            return OmMap(multivector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return DiagonalVector
                .Ids
                .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaFloat64Multivector>(
                        id, 
                        OmMapBasisBlade(id)
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return DiagonalVector
                .Ids
                .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaFloat64Vector>(
                        id, 
                        OmMapBasisVector(id.FirstIndex)
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            var indexVectorDictionary =
                DiagonalVector
                    .Where(p => p.Key.FirstIndex < vSpaceDimensions)
                    .ToDictionary(
                        p => p.Key.FirstIndex,
                        p => p.Key.FirstIndex.CreateLinVector(p.Value)
                    );

            return indexVectorDictionary.ToLinUnilinearMap();
        }

        public IEnumerator<KeyValuePair<IIndexSet, double>> GetEnumerator()
        {
            return DiagonalVector.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}