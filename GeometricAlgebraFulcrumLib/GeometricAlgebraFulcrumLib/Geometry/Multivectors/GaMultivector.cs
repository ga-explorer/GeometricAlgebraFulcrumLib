using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Geometry.Multivectors
{
    public sealed class GaMultivector<T> : 
        IGaGeometry<T>
    {
        public static GaMultivector<T> operator -(GaMultivector<T> v1)
        {
            return new GaMultivector<T>(
                v1.Processor,
                v1.Storage.GetNegative()
            );
        }

        public static GaMultivector<T> operator +(GaMultivector<T> v1, T v2)
        {
            return new GaMultivector<T>(
                v1.Processor,
                v1.Storage.Add(v2)
            );
        }

        public static GaMultivector<T> operator +(T v1, GaMultivector<T> v2)
        {
            return new GaMultivector<T>(
                v2.Processor,
                v1.Add(v2.Storage)
            );
        }

        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            return new GaMultivector<T>(
                v1.Processor,
                v1.Storage.Add(v2.Storage)
            );
        }

        public static GaMultivector<T> operator -(GaMultivector<T> v1, T v2)
        {
            return new GaMultivector<T>(
                v1.Processor,
                v1.Storage.Subtract(v2)
            );
        }

        public static GaMultivector<T> operator -(T v1, GaMultivector<T> v2)
        {
            return new GaMultivector<T>(
                v2.Processor,
                v1.Subtract(v2.Storage)
            );
        }

        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            return new GaMultivector<T>(
                v1.Processor,
                v1.Storage.Subtract(v2.Storage)
            );
        }

        public static GaMultivector<T> operator *(GaMultivector<T> v1, T v2)
        {
            return new GaMultivector<T>(
                v1.Processor,
                v1.Storage.Times(v2)
            );
        }

        public static GaMultivector<T> operator *(T v1, GaMultivector<T> v2)
        {
            return new GaMultivector<T>(
                v2.Processor,
                v1.Times(v2.Storage)
            );

        }
        
        public static GaMultivector<T> operator /(GaMultivector<T> v1, T v2)
        {
            return new GaMultivector<T>(
                v1.Processor,
                v1.Storage.Divide(v2)
            );
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGasMultivector<T> Storage { get; }

        public bool IsValid 
            => Storage
                .GetScalars()
                .All(Storage.ScalarProcessor.IsValid);

        public bool IsInvalid 
            => Storage
                .GetScalars()
                .Any(s => !Storage.ScalarProcessor.IsValid(s));


        internal GaMultivector([NotNull] IGaProcessor<T> processor, [NotNull] IGasMultivector<T> storage)
        {
            Processor = processor;
            Storage = storage;
        }


        public GaMultivector<T> Negative()
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetNegative()
            );
        }

        public GaMultivector<T> Reverse()
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetReverse()
            );
        }

        public GaMultivector<T> GradeInvolution()
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetGradeInvolution()
            );
        }

        public GaMultivector<T> CliffordConjugate()
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetCliffordConjugate()
            );
        }


        public T TermScalar(ulong id)
        {
            return Storage.TryGetTermScalar(id, out var scalar) 
                ? scalar 
                : Processor.ZeroScalar;
        }

        public T TermScalar(uint grade, ulong index)
        {
            return Storage.TryGetTermScalar(grade, index, out var scalar) 
                ? scalar 
                : Processor.ZeroScalar;
        }

        public GaMultivector<T> ScalarPart()
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetScalarPart()
            );
        }

        public GaMultivector<T> VectorPart()
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetVectorPart()
            );
        }

        public GaMultivector<T> BivectorPart()
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetBivectorPart()
            );
        }

        public GaMultivector<T> KVectorPart(uint grade)
        {
            return new GaMultivector<T>(
                Processor,
                Storage.GetKVectorPart(grade)
            );
        }

        public GaMultivector<T> OddKVectorsPart()
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(Processor);

            composer.AddTerms(
                Storage.GetTerms().Where(term => term.BasisBlade.Grade.IsOdd())
            );

            return new GaMultivector<T>(
                Processor,
                composer.GetCompactMultivector()
            );
        }

        public GaMultivector<T> EvenKVectorsPart()
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(Processor);

            composer.AddTerms(
                Storage.GetTerms().Where(term => term.BasisBlade.Grade.IsEven())
            );

            return new GaMultivector<T>(
                Processor,
                composer.GetCompactMultivector()
            );
        }


        public T Sp()
        {
            return Processor.Sp(Storage);
        }

        public T Sp(GaMultivector<T> mv2)
        {
            return Processor.Sp(Storage, mv2.Storage);
        }

        public T Norm()
        {
            return Processor.Norm(Storage);
        }

        public T NormSquared()
        {
            return Processor.NormSquared(Storage);
        }

        public T ESp()
        {
            return Storage.ESp();
        }

        public T ESp(GaMultivector<T> mv2)
        {
            return Storage.ESp(mv2.Storage);
        }

        public T ENorm()
        {
            return Storage.ENorm();
        }

        public T ENormSquared()
        {
            return Storage.ENormSquared();
        }

        public GaMultivector<T> Op(GaMultivector<T> mv2)
        {
            return Storage.Op(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Gp()
        {
            return Processor.Gp(Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Gp(GaMultivector<T> mv2)
        {
            return Processor.Gp(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> GpReverse()
        {
            return Processor.GpReverse(Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> GpReverse(GaMultivector<T> mv2)
        {
            return Processor.GpReverse(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Lcp(GaMultivector<T> mv2)
        {
            return Processor.Lcp(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Rcp(GaMultivector<T> mv2)
        {
            return Processor.Rcp(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Fdp(GaMultivector<T> mv2)
        {
            return Processor.Fdp(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Hip(GaMultivector<T> mv2)
        {
            return Processor.Hip(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Acp(GaMultivector<T> mv2)
        {
            return Processor.Acp(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Cp(GaMultivector<T> mv2)
        {
            return Processor.Cp(Storage, mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> Dual()
        {
            return Processor.Dual(Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> UnDual()
        {
            return Processor.UnDual(Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> BladeInverse()
        {
            return Processor.BladeInverse(Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> VersorInverse()
        {
            return Processor.VersorInverse(Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EGp()
        {
            return Storage.EGp().ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EGp(GaMultivector<T> mv2)
        {
            return Storage.EGp(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EGpReverse()
        {
            return Storage.EGpReverse().ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EGpReverse(GaMultivector<T> mv2)
        {
            return Storage.EGpReverse(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> ELcp(GaMultivector<T> mv2)
        {
            return Storage.ELcp(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> ERcp(GaMultivector<T> mv2)
        {
            return Storage.ELcp(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EFdp(GaMultivector<T> mv2)
        {
            return Storage.ELcp(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EHip(GaMultivector<T> mv2)
        {
            return Storage.ELcp(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EAcp(GaMultivector<T> mv2)
        {
            return Storage.ELcp(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> ECp(GaMultivector<T> mv2)
        {
            return Storage.ELcp(mv2.Storage).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EDual()
        {
            return Storage.EDual(VSpaceDimension).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EUnDual()
        {
            return Storage.EUnDual(VSpaceDimension).ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EBladeInverse()
        {
            return Storage.EBladeInverse().ToGenericMultivector(Processor);
        }

        public GaMultivector<T> EVersorInverse()
        {
            return Storage.EVersorInverse().ToGenericMultivector(Processor);
        }


        public override string ToString()
        {
            return Storage.ToString();
        }
    }
}