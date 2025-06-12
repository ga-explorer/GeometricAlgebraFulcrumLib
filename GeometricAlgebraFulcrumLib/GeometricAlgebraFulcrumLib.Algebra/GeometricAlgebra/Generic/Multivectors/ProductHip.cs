using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> EHip(XGaScalar<T> _)
        {
            return Processor.ScalarZero;
        }

        public abstract XGaMultivector<T> EHip(XGaVector<T> kv2);

        public abstract XGaMultivector<T> EHip(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> EHip(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> EHip(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EHip(XGaUniformMultivector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EHip(XGaKVector<T> kv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;

            return kv2 switch
            {
                XGaScalar<T> => Processor.ScalarZero,
                XGaVector<T> v2 => EHip(v2),
                XGaBivector<T> bv2 => EHip(bv2),
                XGaHigherKVector<T> hkv2 => EHip(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EHip(XGaMultivector<T> mv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;

            return mv2 switch
            {
                XGaScalar<T> => Processor.ScalarZero,
                XGaVector<T> v2 => EHip(v2),
                XGaBivector<T> bv2 => EHip(bv2),
                XGaHigherKVector<T> kv2 => EHip(kv2),
                XGaGradedMultivector<T> gmv2 => EHip(gmv2),
                XGaUniformMultivector<T> umv2 => EHip(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Hip(XGaScalar<T> _)
        {
            return Processor.ScalarZero;
        }

        public abstract XGaMultivector<T> Hip(XGaVector<T> kv2);

        public abstract XGaMultivector<T> Hip(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> Hip(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> Hip(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Hip(XGaUniformMultivector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Hip(XGaKVector<T> kv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;

            return kv2 switch
            {
                XGaScalar<T> s2 => Hip(s2),
                XGaVector<T> v2 => Hip(v2),
                XGaBivector<T> bv2 => Hip(bv2),
                XGaHigherKVector<T> hkv2 => Hip(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Hip(XGaMultivector<T> mv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;

            return mv2 switch
            {
                XGaScalar<T> s2 => Hip(s2),
                XGaVector<T> v2 => Hip(v2),
                XGaBivector<T> bv2 => Hip(bv2),
                XGaHigherKVector<T> kv2 => Hip(kv2),
                XGaGradedMultivector<T> gmv2 => Hip(gmv2),
                XGaUniformMultivector<T> umv2 => Hip(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EHip(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> =>
                    Processor.ScalarZero,

                XGaVector<T> v1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddESpTerms(v1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 1)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EHip(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> =>
                    Processor.ScalarZero,

                XGaVector<T> kv1 =>
                    Processor
                        .CreateKVectorComposer(1)
                        .AddELcpTerms(kv1, kv2)
                        .GetVector(),

                XGaBivector<T> kv1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddESpTerms(kv1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 2)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EHip(XGaHigherKVector<T> kv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EHip(XGaKVector<T> kv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaGradedMultivector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Hip(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> =>
                    Processor.ScalarZero,

                XGaVector<T> v1 =>
                    v1.Sp(kv2),

                _ => Processor
                    .CreateKVectorComposer(Grade - 1)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Hip(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> =>
                    Processor.ScalarZero,

                XGaVector<T> kv1 =>
                    Processor
                        .CreateKVectorComposer(1)
                        .AddLcpTerms(kv1, kv2)
                        .GetVector(),

                XGaBivector<T> kv1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddSpTerms(kv1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 2)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Hip(XGaHigherKVector<T> kv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Hip(XGaKVector<T> kv2)
        {
            if (this is XGaScalar<T>)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaGradedMultivector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }
    }

    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EHip(XGaVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EHip(XGaBivector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EHip(XGaHigherKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EHip(XGaKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EHip(XGaGradedMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Hip(XGaVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Hip(XGaBivector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Hip(XGaHigherKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Hip(XGaKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Hip(XGaGradedMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }

    }

    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EHip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EHip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetVector();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Hip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Hip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetVector();
        }
    }

    public sealed partial class XGaBivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EHip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddERcpTerms(this, kv2)
                    .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EHip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Hip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddRcpTerms(this, kv2)
                    .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Hip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

    }

    public partial class XGaGradedMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaKVector<T> kv2)
        {
            return kv2 is XGaScalar<T> || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaKVector<T> kv2)
        {
            return kv2 is XGaScalar<T> || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }

    }

    public partial class XGaUniformMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaKVector<T> kv2)
        {
            return kv2 is XGaScalar<T> || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EHip(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaKVector<T> kv2)
        {
            return kv2 is XGaScalar<T> || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Hip(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }

    }
    
    public sealed partial class XGaKVectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddEHipTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddEHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddHipTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

    }

    public sealed partial class XGaUniformMultivectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddEHipTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddEHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddHipTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

    }
    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEHipTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEHipTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (kv2.Grade == 0 || mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddEHipTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEHipTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.Grade == 0 || kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddEHipTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEHipTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddEHipTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var mv2KVectors = 
                mv2.GetKVectorParts().ToArray();

            foreach (var kv1 in mv1.GetKVectorParts())
            foreach (var kv2 in mv2KVectors)
                AddEHipTerms(kv1, kv2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddHipTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddHipTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (kv2.Grade == 0 || mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddHipTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddHipTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.Grade == 0 || kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddHipTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddHipTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddHipTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddHipTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var mv2KVectors = 
                mv2.GetKVectorParts().ToArray();

            foreach (var kv1 in mv1.GetKVectorParts())
            foreach (var kv2 in mv2KVectors)
                AddHipTerms(kv1, kv2);

            return this;
        }

    }
}
