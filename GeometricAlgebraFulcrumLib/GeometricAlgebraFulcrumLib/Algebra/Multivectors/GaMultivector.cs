using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors
{
    public sealed class GaMultivector<T> : IGaMultivector<T>
    {
        public static GaMultivector<T> operator -(GaMultivector<T> v1)
        {
            return new(
                v1.Storage.GetNegative()
            );
        }

        public static GaMultivector<T> operator +(GaMultivector<T> v1, T v2)
        {
            return new(
                v1.Storage.Add(v2)
            );
        }

        public static GaMultivector<T> operator +(T v1, GaMultivector<T> v2)
        {
            return new(
                v1.Add(v2.Storage)
            );
        }

        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            return new(
                v1.Storage.Add(v2.Storage)
            );
        }

        public static GaMultivector<T> operator -(GaMultivector<T> v1, T v2)
        {
            return new(
                v1.Storage.Subtract(v2)
            );
        }

        public static GaMultivector<T> operator -(T v1, GaMultivector<T> v2)
        {
            return new(
                v1.Subtract(v2.Storage)
            );
        }

        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            return new(
                v1.Storage.Subtract(v2.Storage)
            );
        }

        public static GaMultivector<T> operator *(GaMultivector<T> v1, T v2)
        {
            return new(
                v1.Storage.Times(v2)
            );
        }

        public static GaMultivector<T> operator *(T v1, GaMultivector<T> v2)
        {
            return new(
                v1.Times(v2.Storage)
            );

        }
        
        public static GaMultivector<T> operator /(GaMultivector<T> v1, T v2)
        {
            return new(
                v1.Storage.Divide(v2)
            );
        }


        public IGaScalarProcessor<T> ScalarProcessor 
            => Storage.ScalarProcessor;

        public IGaMultivectorStorage<T> Storage { get; }


        public GaMultivector([NotNull] IGaMultivectorStorage<T> storage)
        {
            Storage = storage;
        }


        public GaMultivector<T> Negative()
        {
            return new(
                Storage.GetNegative()
            );
        }

        public GaMultivector<T> Reverse()
        {
            return new(
                Storage.GetReverse()
            );
        }

        public GaMultivector<T> GradeInvolution()
        {
            return new(
                Storage.GetGradeInvolution()
            );
        }

        public GaMultivector<T> CliffordConjugate()
        {
            return new(
                Storage.GetCliffordConjugate()
            );
        }


        public T TermScalar(ulong id)
        {
            return Storage.TryGetTermScalar(id, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public T TermScalar(int grade, ulong index)
        {
            return Storage.TryGetTermScalar(grade, index, out var scalar) 
                ? scalar 
                : ScalarProcessor.ZeroScalar;
        }

        public GaMultivector<T> ScalarPart()
        {
            return new(Storage.GetScalarPart());
        }

        public GaMultivector<T> VectorPart()
        {
            return new(Storage.GetVectorPart());
        }

        public GaMultivector<T> BivectorPart()
        {
            return new(Storage.GetBivectorPart());
        }

        public GaMultivector<T> KVectorPart(int grade)
        {
            return new(Storage.GetKVectorPart(grade));
        }

        public GaMultivector<T> OddKVectorsPart()
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.AddTerms(
                Storage.GetTerms().Where(term => term.BasisBlade.Grade.IsOdd())
            );

            return new GaMultivector<T>(
                composer.GetCompactStorage()
            );
        }

        public GaMultivector<T> EvenKVectorsPart()
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(ScalarProcessor);

            composer.AddTerms(
                Storage.GetTerms().Where(term => term.BasisBlade.Grade.IsEven())
            );

            return new GaMultivector<T>(
                composer.GetCompactStorage()
            );
        }
    }
}