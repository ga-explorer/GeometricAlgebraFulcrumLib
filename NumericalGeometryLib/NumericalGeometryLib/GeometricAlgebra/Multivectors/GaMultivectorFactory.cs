using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.Multivectors
{
    public static class GaMultivectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector SumToMultivector(this GaBasisSet basisSet, IEnumerable<GaIdScalarRecord> idScalarRecords)
        {
            var scalarList = new GaMultivectorSparseList(basisSet.GaSpaceDimension);

            scalarList.AddTerms(idScalarRecords);

            return new GaMultivector(basisSet, scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector CreateZero(this GaBasisSet basisSet)
        {
            return new GaMultivector(basisSet);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateBasisScalar(this GaBasisSet basisSet)
        {
            return new GaTerm(basisSet, 0, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateBasisVector(this GaBasisSet basisSet, ulong index)
        {
            return new GaTerm(basisSet, 1UL << (int) index, 1d);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateBasisBivector(this GaBasisSet basisSet, ulong index1, ulong index2)
        {
            if (index1 == index2)
                throw new ArgumentException();

            var id = (1UL << (int) index1) | (1UL << (int) index2);

            return new GaTerm(basisSet, id, index1 < index2 ? 1d : -1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateBasisBlade(this GaBasisSet basisSet, ulong id)
        {
            return new GaTerm(basisSet, id, 1d);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateBasisBlade(this GaBasisSet basisSet, uint grade, ulong index)
        {
            return new GaTerm(basisSet, grade, index, 1d);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateScalar(this GaBasisSet basisSet, double scalar)
        {
            return new GaTerm(basisSet, 0, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateVectorTerm(this GaBasisSet basisSet, ulong index, double scalar)
        {
            return new GaTerm(basisSet, 1UL << (int) index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateBivectorTerm(this GaBasisSet basisSet, ulong index1, ulong index2, double scalar)
        {
            if (index1 == index2)
                throw new ArgumentException();

            var id = (1UL << (int) index1) | (1UL << (int) index2);

            return new GaTerm(basisSet, id, index1 < index2 ? scalar : -scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateTerm(this GaBasisSet basisSet, ulong id, double scalar)
        {
            return new GaTerm(basisSet, id, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreateTerm(this GaBasisSet basisSet, uint grade, ulong index, double scalar)
        {
            return new GaTerm(basisSet, grade, index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreatePseudoScalar(this GaBasisSet basisSet)
        {
            return new GaTerm(basisSet, basisSet.MaxBasisBladeId, 1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreatePseudoScalarReverse(this GaBasisSet basisSet)
        {
            var scalar = 
                basisSet.VSpaceDimension.GradeHasNegativeReverse() 
                    ? -1d : 1d;

            return new GaTerm(basisSet, basisSet.MaxBasisBladeId, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreatePseudoScalarEInverse(this GaBasisSet basisSet)
        {
            var scalar = basisSet.EGpSquaredSignature(basisSet.MaxBasisBladeId);

            return new GaTerm(basisSet, basisSet.MaxBasisBladeId, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaTerm CreatePseudoScalarInverse(this GaBasisSet basisSet)
        {
            var scalar = basisSet.GpSquaredSignature(basisSet.MaxBasisBladeId);

            if (scalar == 0) throw new InvalidOperationException();

            return new GaTerm(basisSet, basisSet.MaxBasisBladeId, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector CreateVector(this GaBasisSet basisSet, params double[] numbersList)
        {
            if ((ulong) numbersList.Length > basisSet.GaSpaceDimension)
                throw new InvalidOperationException();

            var mv = new GaMultivector(basisSet);

            for (var index = 0; index < numbersList.Length; index++)
                mv[1UL << index] = numbersList[index];

            return mv;
        }


    }
}