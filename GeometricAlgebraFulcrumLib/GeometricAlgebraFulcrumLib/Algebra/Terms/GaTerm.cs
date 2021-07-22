using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Terms
{
    public sealed class GaTerm<T>
    {
        public static GaTerm<T> CreateVector(int basisVectorIndex, T scalar)
        {
            return new GaTerm<T>(
                basisVectorIndex.CreateBasisVector(),
                scalar
            );
        }

        public static GaTerm<T> CreateVector(uint basisVectorIndex, T scalar)
        {
            return new GaTerm<T>(
                GaBasisFactory.CreateBasisVector(basisVectorIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateVector(ulong basisVectorIndex, T scalar)
        {
            return new GaTerm<T>(
                basisVectorIndex.CreateBasisVector(),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(int basisBivectorIndex, T scalar)
        {
            return new GaTerm<T>(
                basisBivectorIndex.CreateBasisBivector(),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(ulong basisBivectorIndex, T scalar)
        {
            return new GaTerm<T>(
                basisBivectorIndex.CreateBasisBivector(),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            return new GaTerm<T>(
                GaBasisFactory.CreateBasisBivector(basisVectorIndex1, basisVectorIndex2),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            return new GaTerm<T>(
                GaBasisFactory.CreateBasisBivector(basisVectorIndex1, basisVectorIndex2),
                scalar
            );
        }

        public static GaTerm<T> CreateUniform(ulong basisBladeId, T scalar)
        {
            return new GaTerm<T>(
                basisBladeId.CreateUniformBasisBlade(),
                scalar
            );
        }

        public static GaTerm<T> CreateUniform(uint grade, ulong basisBladeIndex, T scalar)
        {
            return new GaTerm<T>(
                grade.CreateUniformBasisBlade(basisBladeIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateGraded(ulong basisBladeId, T scalar)
        {
            return new GaTerm<T>(
                basisBladeId.CreateGradedBasisBlade(),
                scalar
            );
        }

        public static GaTerm<T> CreateGraded(uint grade, ulong basisBladeIndex, T scalar)
        {
            return new GaTerm<T>(
                grade.CreateGradedBasisBlade(basisBladeIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateFull(ulong basisBladeId, T scalar)
        {
            return new GaTerm<T>(
                basisBladeId.CreateFullBasisBlade(),
                scalar
            );
        }

        public static GaTerm<T> CreateFull(uint grade, ulong basisBladeIndex, T scalar)
        {
            return new GaTerm<T>(
                grade.CreateFullBasisBlade(basisBladeIndex),
                scalar
            );
        }

        public static GaTerm<T> Create(IGaBasisBlade basisBlade, T scalar)
        {
            return new GaTerm<T>(
                basisBlade,
                scalar
            );
        }


        public IGaBasisBlade BasisBlade { get; }

        public T Scalar { get; }


        internal GaTerm(IGaBasisBlade basisBlade, T scalar)
        {
            BasisBlade = basisBlade;
            Scalar = scalar;
        }


        public GaTerm<T> GetCopy()
        {
            return new(BasisBlade, Scalar);
        }

        public GaTerm<T> GetCopy(T newScalar)
        {
            return new(BasisBlade, newScalar);
        }

        public KeyValuePair<ulong, T> GetIdScalarPair()
        {
            return new(BasisBlade.Id, Scalar);
        }

        public Tuple<ulong, T> GetIdScalarTuple()
        {
            return new(BasisBlade.Id, Scalar);
        }

        public Tuple<uint, ulong, T> GetGradeIndexScalarTuple()
        {
            BasisBlade.GetGradeIndex(out var grade, out var index);

            return new Tuple<uint, ulong, T>(grade, index, Scalar);
        }

        public Tuple<ulong, T> GetIndexScalarTuple()
        {
            return new(BasisBlade.Index, Scalar);
        }

        public Tuple<ulong, uint, ulong, T> GetIdGradeIndexScalarTuple()
        {
            BasisBlade.GetIdGradeIndex(out var id, out var grade, out var index);

            return new Tuple<ulong, uint, ulong, T>(id, grade, index, Scalar);
        }


        public override string ToString()
        {
            return $"'{Scalar}'{BasisBlade}";
        }
    }
}