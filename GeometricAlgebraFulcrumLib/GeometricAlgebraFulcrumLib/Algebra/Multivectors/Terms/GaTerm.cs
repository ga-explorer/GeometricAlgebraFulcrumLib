using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Terms
{
    public sealed class GaTerm<T>
    {
        public static GaTerm<T> CreateVector(int basisVectorIndex, T scalar)
        {
            return new(
                new GaBasisVector(basisVectorIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateVector(ulong basisVectorIndex, T scalar)
        {
            return new(
                new GaBasisVector(basisVectorIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(int basisBivectorIndex, T scalar)
        {
            return new(
                new GaBasisBivector((ulong) basisBivectorIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(ulong basisBivectorIndex, T scalar)
        {
            return new(
                new GaBasisBivector(basisBivectorIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            return new(
                new GaBasisBivector(basisVectorIndex1, basisVectorIndex2),
                scalar
            );
        }

        public static GaTerm<T> CreateBivector(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            return new(
                new GaBasisBivector(basisVectorIndex1, basisVectorIndex2),
                scalar
            );
        }

        public static GaTerm<T> CreateUniform(ulong basisBladeId, T scalar)
        {
            return new(
                new GaBasisUniform(basisBladeId),
                scalar
            );
        }

        public static GaTerm<T> CreateUniform(int basisBladeGrade, ulong basisBladeIndex, T scalar)
        {
            return new(
                new GaBasisUniform(basisBladeGrade, basisBladeIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateGraded(ulong basisBladeId, T scalar)
        {
            return new(
                new GaBasisGraded(basisBladeId),
                scalar
            );
        }

        public static GaTerm<T> CreateGraded(int basisBladeGrade, ulong basisBladeIndex, T scalar)
        {
            return new(
                new GaBasisGraded(basisBladeGrade, basisBladeIndex),
                scalar
            );
        }

        public static GaTerm<T> CreateFull(ulong basisBladeId, T scalar)
        {
            return new(
                new GaBasisFull(basisBladeId),
                scalar
            );
        }

        public static GaTerm<T> CreateFull(int basisBladeGrade, ulong basisBladeIndex, T scalar)
        {
            return new(
                new GaBasisFull(basisBladeGrade, basisBladeIndex),
                scalar
            );
        }

        public static GaTerm<T> Create(IGaBasisBlade basisBlade, T scalar)
        {
            return new(
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

        public Tuple<int, ulong, T> GetGradeIndexScalarTuple()
        {
            BasisBlade.GetGradeIndex(out var grade, out var index);

            return new Tuple<int, ulong, T>(grade, index, Scalar);
        }

        public Tuple<ulong, T> GetIndexScalarTuple()
        {
            return new(BasisBlade.Index, Scalar);
        }

        public Tuple<ulong, int, ulong, T> GetIdGradeIndexScalarTuple()
        {
            BasisBlade.GetIdGradeIndex(out var id, out var grade, out var index);

            return new Tuple<ulong, int, ulong, T>(id, grade, index, Scalar);
        }


        public override string ToString()
        {
            return $"'{Scalar}'{BasisBlade}";
        }
    }
}