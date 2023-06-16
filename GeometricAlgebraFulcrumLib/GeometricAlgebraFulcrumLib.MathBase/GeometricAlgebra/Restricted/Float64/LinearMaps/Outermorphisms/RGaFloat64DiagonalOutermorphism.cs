using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms
{
    /// <summary>
    /// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
    /// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
    /// mappings are computed as needed.
    /// </summary>
    public sealed class RGaFloat64DiagonalOutermorphism : 
        IRGaFloat64Automorphism,
        IReadOnlyDictionary<ulong, double>
    {
        public RGaFloat64Processor Processor 
            => DiagonalVector.Processor;
        
        public RGaMetric Metric 
            => DiagonalVector.Metric;
        
        public RGaFloat64Vector DiagonalVector { get; }
        
        public int Count 
            => DiagonalVector.Count;
        
        public double this[ulong key] 
            => DiagonalVector[key];

        public IEnumerable<ulong> Keys 
            => DiagonalVector.Keys;

        public IEnumerable<double> Values 
            => DiagonalVector.Values;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64DiagonalOutermorphism(RGaFloat64Vector diagonalVector)
        {
            DiagonalVector = diagonalVector;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return DiagonalVector.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out double value)
        {
            return DiagonalVector.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaFloat64Outermorphism GetOmAdjoint()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector OmMapBasisVector(int index)
        {
            var id = index.BasisVectorIndexToId();

            return DiagonalVector.TryGetTermScalar(id, out var scalar)
                ? Processor.CreateVector(index, scalar)
                : Processor.CreateZeroVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
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
                    scalar1 * scalar2
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector OmMapBasisBlade(ulong id)
        {
            var scalar = 1d;

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

                scalar *= s;
            }
            
            return Processor.CreateKVector(id, scalar);
        }
        

        public RGaFloat64Vector OmMap(RGaFloat64Vector vector)
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

        public RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
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

        public RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
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

        public RGaFloat64KVector OmMap(RGaFloat64KVector kVector)
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

        public RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
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
        public IRGaFloat64UnilinearMap GetAdjoint()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector Map(RGaFloat64Multivector multivector)
        {
            return OmMap(multivector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return DiagonalVector
                .Ids
                .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaFloat64Multivector>(
                        id, 
                        OmMapBasisBlade(id)
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return DiagonalVector
                .Ids
                .Where(id => id.VSpaceDimensions() <= vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaFloat64Vector>(
                        id, 
                        OmMapBasisVector(id.FirstOneBitPosition())
                    )
                );
        }

        public LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<ulong, double>> GetEnumerator()
        {
            return DiagonalVector.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}