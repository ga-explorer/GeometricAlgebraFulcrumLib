using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        public abstract XGaScalar<T> ESp(XGaScalar<T> kv2);

        public abstract XGaScalar<T> ESp(XGaVector<T> kv2);

        public abstract XGaScalar<T> ESp(XGaBivector<T> kv2);

        public abstract XGaScalar<T> ESp(XGaHigherKVector<T> kv2);

        public abstract XGaScalar<T> ESp(XGaKVector<T> kv2);

        public abstract XGaScalar<T> ESp(XGaGradedMultivector<T> mv2);

        public abstract XGaScalar<T> ESp(XGaUniformMultivector<T> mv2);

        public abstract XGaScalar<T> ESp(XGaMultivector<T> mv2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Scalar<T> ESpSquared()
        {
            return IsZero
                ? ScalarProcessor.Zero
                : ScalarProcessor.Add(
                    IdScalarPairs.Select(p =>
                        ScalarProcessor.Times(
                            Metric.EGpSquaredSign(p.Key), 
                            ScalarProcessor.Square(p.Value).ScalarValue
                        ).ScalarValue
                    )
                );
        }


        public abstract XGaScalar<T> Sp(XGaScalar<T> kv2);

        public abstract XGaScalar<T> Sp(XGaVector<T> kv2);

        public abstract XGaScalar<T> Sp(XGaBivector<T> kv2);

        public abstract XGaScalar<T> Sp(XGaHigherKVector<T> kv2);

        public abstract XGaScalar<T> Sp(XGaKVector<T> kv2);

        public abstract XGaScalar<T> Sp(XGaGradedMultivector<T> mv2);

        public abstract XGaScalar<T> Sp(XGaUniformMultivector<T> mv2);

        public abstract XGaScalar<T> Sp(XGaMultivector<T> mv2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Scalar<T> SpSquared()
        {
            return IsZero
                ? ScalarProcessor.Zero
                : ScalarProcessor.Add(
                    IdScalarPairs.Select(p =>
                        ScalarProcessor.Times(
                            Metric.GpSquaredSign(p.Key), 
                            ScalarProcessor.Square(p.Value).ScalarValue
                        ).ScalarValue
                    )
                );
        }

    }

    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaScalar<T> kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            return this is XGaScalar<T> kv1
                ? Processor.Scalar(ScalarProcessor.Times(kv1.ScalarValue, kv2.ScalarValue))
                : Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaVector<T> kv1
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(kv1, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaBivector<T> kv1
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(kv1, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero || Grade != kv2.Grade
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2 is not XGaScalar<T> s2
                    ? Processor.ScalarZero
                    : Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue).ScalarValue);

            return Grade != kv2.Grade
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = ScalarComposer<T>.Create(ScalarProcessor);

            if (mv2 is XGaKVector<T> kv2)
                composer.AddESpTerms(this, kv2);

            else if (mv2 is XGaGradedMultivector<T> gmv2)
                composer.AddESpTerms(this, gmv2);

            else if (mv2 is XGaUniformMultivector<T> umv2)
                composer.AddESpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaScalar(Processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaScalar<T> kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            return this is XGaScalar<T> kv1
                ? Processor.Scalar(ScalarProcessor.Times(kv1.ScalarValue, kv2.ScalarValue))
                : Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaVector<T> kv1
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(kv1, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaBivector<T> kv1
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(kv1, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero || Grade != kv2.Grade
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2 is not XGaScalar<T> s2
                    ? Processor.ScalarZero
                    : Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue).ScalarValue);

            return Grade != kv2.Grade
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = ScalarComposer<T>.Create(ScalarProcessor);

            if (mv2 is XGaKVector<T> kv2)
                composer.AddSpTerms(this, kv2);

            else if (mv2 is XGaGradedMultivector<T> gmv2)
                composer.AddSpTerms(this, gmv2);

            else if (mv2 is XGaUniformMultivector<T> umv2)
                composer.AddSpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaScalar(Processor);
        }
    }

    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> ESpSquared()
        {
            return IsZero
                ? ScalarProcessor.Zero
                : ScalarProcessor.Square(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> SpSquared()
        {
            return IsZero
                ? ScalarProcessor.Zero
                : ScalarProcessor.Square(ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaScalar<T> kv2)
        {
            return IsZero || kv2.IsZero 
                ? Processor.ScalarZero 
                : Processor.Scalar(ScalarProcessor.Times(ScalarValue, kv2.ScalarValue));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaScalar<T> kv2)
        {
            return IsZero || kv2.IsZero 
                ? Processor.ScalarZero 
                : Processor.Scalar(ScalarProcessor.Times(ScalarValue, kv2.ScalarValue));
        }
    }

    public partial class XGaGradedMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaScalar<T> kv2)
        {
            return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
                ? Processor.Scalar(
                    ScalarProcessor.Times(
                        ((XGaScalar<T>)scalarPart).ScalarValue, 
                        kv2.ScalarValue
                    )
                )
                : Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = ScalarComposer<T>.Create(ScalarProcessor);

            if (mv2 is XGaKVector<T> kv2)
                composer.AddESpTerms(this, kv2);

            else if (mv2 is XGaGradedMultivector<T> gmv2)
                composer.AddESpTerms(this, gmv2);

            else if (mv2 is XGaUniformMultivector<T> umv2)
                composer.AddESpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaScalar(Processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaScalar<T> kv2)
        {
            return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
                ? Processor.Scalar(ScalarProcessor.Times(((XGaScalar<T>)scalarPart).ScalarValue, kv2.ScalarValue))
                : Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = ScalarComposer<T>.Create(ScalarProcessor);

            if (mv2 is XGaKVector<T> kv2)
                composer.AddSpTerms(this, kv2);

            else if (mv2 is XGaGradedMultivector<T> gmv2)
                composer.AddSpTerms(this, gmv2);

            else if (mv2 is XGaUniformMultivector<T> umv2)
                composer.AddSpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaScalar(Processor);
        }
    }

    public partial class XGaUniformMultivector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaScalar<T> kv2)
        {
            return TryGetScalarValue(out var s1) || kv2.IsZero
                ? Processor.ScalarZero
                : Processor.Scalar(ScalarProcessor.Times(s1, kv2.ScalarValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ESp(XGaMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaScalar<T> kv2)
        {
            return TryGetScalarValue(out var s1) || kv2.IsZero
                ? Processor.ScalarZero
                : Processor.Scalar(ScalarProcessor.Times(s1, kv2.ScalarValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Sp(XGaMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, mv2)
                    .GetXGaScalar(Processor);
        }

    }
    
    public sealed partial class XGaKVectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddESpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(kv1, kv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(mv1, kv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddESpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(kv1, mv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(mv1, mv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddESpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(mv1, mv2);
            
            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddSpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(kv1, kv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(mv1, kv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddSpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(kv1, mv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(mv1, mv2);
            
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddSpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(mv1, mv2);
            
            return this;
        }

    }
    
    public sealed partial class XGaUniformMultivectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(kv1, kv2).ScalarValue
            );

            //if (kv1.Grade != kv2.Grade || kv1.IsZero || kv2.IsZero)
            //    return this;

            //var spScalar = 0d;

            //if (kv1.Count <= kv2.Count)
            //{
            //    foreach (var (id, scalar1) in kv1.IdScalarPairs)
            //    {
            //        if (!kv2.TryGetBasisBladeScalarValue(id, out var scalar2))
            //            continue;
                    
            //        spScalar = Metric.EGpSquaredSign(id).IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}
            //else
            //{
            //    foreach (var (id, scalar2) in kv2.IdScalarPairs)
            //    {
            //        if (!kv1.TryGetBasisBladeScalarValue(id, out var scalar1))
            //            continue;
                    
            //        spScalar = Metric.EGpSquaredSign(id).IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}

            //return AddScalarTerm(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, kv2).ScalarValue
            );

            //if (mv1.IsZero || kv2.IsZero)
            //    return this;

            //return mv1.TryGetKVector(kv2.Grade, out var kv1)
            //    ? AddESpTerms(kv1, kv2)
            //    : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(kv1, mv2).ScalarValue
            );

            //if (kv1.IsZero || mv2.IsZero)
            //    return this;

            //return mv2.TryGetKVector(kv1.Grade, out var kv2)
            //    ? AddESpTerms(kv1, kv2)
            //    : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, mv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddESpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, mv2).ScalarValue
            );

            //if (mv1.IsZero || mv2.IsZero)
            //    return this;

            //var spScalar = 0d;

            //if (mv1.Count <= mv2.Count)
            //{
            //    foreach (var (id, scalar1) in mv1.IdScalarPairs)
            //    {
            //        if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
            //            continue;
                    
            //        spScalar = Metric.EGpSquaredSign(id).IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}
            //else
            //{
            //    foreach (var (id, scalar2) in mv2.IdScalarPairs)
            //    {
            //        if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
            //            continue;
                    
            //        spScalar = Metric.EGpSquaredSign(id).IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}

            //return AddScalarTerm(spScalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(kv1, kv2).ScalarValue
            );

            //if (kv1.Grade != kv2.Grade || kv1.IsZero || kv2.IsZero)
            //    return this;
            
            //var spScalar = 0d;

            //if (kv1.Count <= kv2.Count)
            //{
            //    foreach (var (id, scalar1) in kv1.IdScalarPairs)
            //    {
            //        if (!kv2.TryGetBasisBladeScalarValue(id, out var scalar2))
            //            continue;
                    
            //        var sign = Metric.GpSquaredSign(id);

            //        if (sign.IsZero) continue;

            //        spScalar = sign.IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}
            //else
            //{
            //    foreach (var (id, scalar2) in kv2.IdScalarPairs)
            //    {
            //        if (!kv1.TryGetBasisBladeScalarValue(id, out var scalar1))
            //            continue;
                    
            //        var sign = Metric.GpSquaredSign(id);

            //        if (sign.IsZero) continue;

            //        spScalar = sign.IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}

            //return AddScalarTerm(spScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, kv2).ScalarValue
            );

            //if (mv1.IsZero || kv2.IsZero)
            //    return this;

            //return mv1.TryGetKVector(kv2.Grade, out var kv1)
            //    ? AddSpTerms(kv1, kv2)
            //    : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(kv1, mv2).ScalarValue
            );

            //if (kv1.IsZero || mv2.IsZero)
            //    return this;

            //return mv2.TryGetKVector(kv1.Grade, out var kv2)
            //    ? AddSpTerms(kv1, kv2)
            //    : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, mv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddSpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, mv2).ScalarValue
            );

            //Debug.Assert(
            //    Metric.HasSameSignature(mv1.Metric) &&
            //    Metric.HasSameSignature(mv2.Metric)
            //);

            //if (mv1.IsZero || mv2.IsZero)
            //    return this;

            //var spScalar = 0d;

            //if (mv1.Count <= mv2.Count)
            //{
            //    foreach (var (id, scalar1) in mv1.IdScalarPairs)
            //    {
            //        if (!mv2.TryGetBasisBladeScalarValue(id, out var scalar2))
            //            continue;
                    
            //        var sign = Metric.GpSquaredSign(id);

            //        if (sign.IsZero) continue;

            //        spScalar = sign.IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}
            //else
            //{
            //    foreach (var (id, scalar2) in mv2.IdScalarPairs)
            //    {
            //        if (!mv1.TryGetBasisBladeScalarValue(id, out var scalar1))
            //            continue;
                    
            //        var sign = Metric.GpSquaredSign(id);

            //        if (sign.IsZero) continue;

            //        spScalar = sign.IsPositive
            //            ? spScalar + scalar1 * scalar2
            //            : spScalar - scalar1 * scalar2;
            //    }
            //}

            //return AddScalarTerm(spScalar);
        }
    }
    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddESpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(kv1, kv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, kv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddESpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(kv1, mv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddESpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, mv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddESpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, mv2).ScalarValue
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddSpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(kv1, kv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, kv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddSpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(kv1, mv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddSpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, mv2).ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddSpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, mv2).ScalarValue
            );
        }
    }

}
