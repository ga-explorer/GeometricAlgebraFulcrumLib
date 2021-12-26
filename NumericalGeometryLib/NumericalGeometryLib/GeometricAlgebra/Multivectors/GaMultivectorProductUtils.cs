using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.GuidedBinaryTraversal;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.Multivectors
{
    public static class GaMultivectorProductUtils
    {
        /// <summary>
        /// True if the outer product of the given euclidean basis blades is zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroOp(ulong id1, ulong id2)
        {
            return (id1 & id2) != 0UL;
        }

        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroOp(ulong id1, ulong id2)
        {
            return (id1 & id2) == 0UL;
        }

        /// <summary>
        /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEGp(ulong id1, ulong id2)
        {
            return false;
        }

        /// <summary>
        /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEGp(ulong id1, ulong id2)
        {
            return true;
        }

        /// <summary>
        /// True if the scalar product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroESp(ulong id1, ulong id2)
        {
            return id1 != id2;
        }

        /// <summary>
        /// True if the scalar product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroESp(ulong id1, ulong id2)
        {
            return id1 == id2;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroELcp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) != 0UL;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroELcp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0UL;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroERcp(ulong id1, ulong id2)
        {
            return (id2 & ~id1) != 0UL;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroERcp(ulong id1, ulong id2)
        {
            return (id2 & ~id1) == 0UL;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEFdp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) != 0UL && (id2 & ~id1) != 0UL;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEFdp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0UL || (id2 & ~id1) == 0UL;
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEHip(ulong id1, ulong id2)
        {
            return id1 == 0UL || 
                   id2 == 0UL || 
                   ((id1 & ~id2) != 0UL && (id2 & ~id1) != 0UL);
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEHip(ulong id1, ulong id2)
        {
            return id1 != 0UL && 
                   id2 != 0UL && 
                   ((id1 & ~id2) == 0UL || (id2 & ~id1) == 0UL);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroEAcp(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return BasisBladeDataLookup.EGpIsNegative(id1, id2) != 
                   BasisBladeDataLookup.EGpIsNegative(id2, id1);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEAcp(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return BasisBladeDataLookup.EGpIsNegative(id1, id2) == 
                   BasisBladeDataLookup.EGpIsNegative(id2, id1);
        }

        /// <summary>
        /// True if the commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroECp(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return BasisBladeDataLookup.EGpIsNegative(id1, id2) == 
                   BasisBladeDataLookup.EGpIsNegative(id2, id1);
        }

        /// <summary>
        /// True if the commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroECp(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return BasisBladeDataLookup.EGpIsNegative(id1, id2) != 
                   BasisBladeDataLookup.EGpIsNegative(id2, id1);
        }


        public static IEnumerable<double> GetESpSquaredScalars(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1)
        {
            foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ESpSquaredSign(id);

                yield return signature * scalar1 * scalar1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetENormSquaredScalars(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1)
        {
            return mvList1.StoredNumbers.Select(scalar => scalar * scalar);
        }
        
        public static IEnumerable<double> GetESpScalars(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            if (mvList1.Count <= mvList2.Count)
            {
                foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
                {
                    var signature = basisSet.ESpSquaredSign(id);

                    yield return signature * scalar1 * mvList2[id];
                }
            }
            else
            {
                foreach (var (id, scalar2) in mvList2.StoredIdNumberPairs)
                {
                    var signature = basisSet.ESpSquaredSign(id);

                    yield return signature * mvList1[id] * scalar2;
                }
            }
        }

        public static IEnumerable<IdScalarRecord> GetOpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.OpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetOpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.OpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetOpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.OpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetELcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ELcpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetELcpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ELcpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetELcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ELcpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetERcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ERcpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetERcpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ERcpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetERcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ERcpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEFdpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EFdpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEFdpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EFdpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetEFdpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EFdpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEHipIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EHipSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEHipIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EHipSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetEHipIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EHipSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEAcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EAcpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEAcpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EAcpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetEAcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EAcpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetECpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.ECpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetECpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ECpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetECpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.ECpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEGpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEGpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetEGpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvSparseList1, GaMultivectorSparseList mvSparseList2)
        {
            foreach (var (id1, scalar1) in mvSparseList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvSparseList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpSign(id1, id2);

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEGpReverseIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpReverseSign(id1, mv2.Id);

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetEGpReverseIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpReverseSign(mv1.Id, id2);

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetEGpReverseIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.EGpReverseSign(id1, id2);

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }


        public static IEnumerable<double> GetSpSquaredScalars(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1)
        {
            foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.SpSquaredSign(id);

                if (signature == 0) continue;

                yield return signature * scalar1 * scalar1;
            }
        }
        
        public static IEnumerable<double> GetNormSquaredScalars(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1)
        {
            foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.NormSquaredSign(id);

                if (signature == 0) continue;

                yield return signature * scalar1 * scalar1;
            }
        }

        public static IEnumerable<double> GetSpScalars(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            if (mvList1.Count <= mvList2.Count)
            {
                foreach (var (id, scalar1) in mvList1.StoredIdNumberPairs)
                {
                    var signature = basisSet.SpSquaredSign(id);

                    if (signature == 0) continue;

                    yield return signature * scalar1 * mvList2[id];
                }
            }
            else
            {
                foreach (var (id, scalar2) in mvList2.StoredIdNumberPairs)
                {
                    var signature = basisSet.SpSquaredSign(id);

                    if (signature == 0) continue;

                    yield return signature * mvList1[id] * scalar2;
                }
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetLcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.LcpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetLcpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.LcpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetLcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.LcpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetRcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.RcpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetRcpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.RcpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetRcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.RcpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetFdpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.FdpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetFdpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.FdpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetFdpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.FdpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetHipIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.HipSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetHipIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.HipSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetHipIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.HipSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetAcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.AcpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetAcpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.AcpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetAcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.AcpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetCpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.CpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetCpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.CpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetCpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.CpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetGpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.GpSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetGpIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetGpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvSparseList1, GaMultivectorSparseList mvSparseList2)
        {
            foreach (var (id1, scalar1) in mvSparseList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvSparseList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetGpReverseIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaTerm mv2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            {
                var signature = basisSet.GpReverseSign(id1, mv2.Id);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ mv2.Id,
                    signature * scalar1 * mv2.Scalar
                );
            }
        }
        
        public static IEnumerable<IdScalarRecord> GetGpReverseIdScalarRecords(this BasisBladeSet basisSet, GaTerm mv1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpReverseSign(mv1.Id, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    mv1.Id ^ id2,
                    signature * mv1.Scalar * scalar2
                );
            }
        }

        public static IEnumerable<IdScalarRecord> GetGpReverseIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorSparseList mvList1, GaMultivectorSparseList mvList2)
        {
            foreach (var (id1, scalar1) in mvList1.StoredIdNumberPairs)
            foreach (var (id2, scalar2) in mvList2.StoredIdNumberPairs)
            {
                var signature = basisSet.GpReverseSign(id1, id2);

                if (signature == 0) continue;

                yield return new IdScalarRecord(
                    id1 ^ id2,
                    signature * scalar1 * scalar2
                );
            }
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetOpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetOpIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetESpScalars(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetESpScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetELcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetELcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetERcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetERcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetEFdpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEFdpIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetEHipIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEHipIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetEAcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEAcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetECpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetECpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetEGpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetEGpIdScalarRecords();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetSpScalars(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetSpScalars();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetLcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetLcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetRcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetRcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetFdpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetFdpIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetHipIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetHipIdScalarRecords();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetAcpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetAcpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetCpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetCpIdScalarRecords();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IdScalarRecord> GetGpIdScalarRecords(this BasisBladeSet basisSet, GaMultivectorBinaryTrie mvBinaryTrie1, GaMultivectorBinaryTrie mvBinaryTrie2)
        {
            return basisSet
                .CreateGbtProductsStack(mvBinaryTrie1, mvBinaryTrie2)
                .GetGpIdScalarRecords();
        }
    }
}