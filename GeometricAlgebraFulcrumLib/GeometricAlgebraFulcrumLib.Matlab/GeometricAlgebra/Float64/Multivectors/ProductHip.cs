using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        
        public XGaFloat64Scalar EHip(XGaFloat64Scalar _)
        {
            return Processor.ScalarZero;
        }

        
        public virtual XGaFloat64Multivector EHip(XGaFloat64Vector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EHip(kv2),
                XGaFloat64Vector mv1 => mv1.EHip(kv2),
                XGaFloat64Bivector mv1 => mv1.EHip(kv2),
                XGaFloat64HigherKVector mv1 => mv1.EHip(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.EHip(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.EHip(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector EHip(XGaFloat64Bivector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EHip(kv2),
                XGaFloat64Vector mv1 => mv1.EHip(kv2),
                XGaFloat64Bivector mv1 => mv1.EHip(kv2),
                XGaFloat64HigherKVector mv1 => mv1.EHip(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.EHip(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.EHip(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector EHip(XGaFloat64HigherKVector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EHip(kv2),
                XGaFloat64Vector mv1 => mv1.EHip(kv2),
                XGaFloat64Bivector mv1 => mv1.EHip(kv2),
                XGaFloat64HigherKVector mv1 => mv1.EHip(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.EHip(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.EHip(kv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public virtual XGaFloat64Multivector EHip(XGaFloat64GradedMultivector mv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EHip(mv2),
                XGaFloat64Vector mv1 => mv1.EHip(mv2),
                XGaFloat64Bivector mv1 => mv1.EHip(mv2),
                XGaFloat64HigherKVector mv1 => mv1.EHip(mv2),
                XGaFloat64GradedMultivector mv1 => mv1.EHip(mv2),
                XGaFloat64UniformMultivector mv1 => mv1.EHip(mv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public XGaFloat64Multivector EHip(XGaFloat64UniformMultivector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }

        
        public virtual XGaFloat64Multivector EHip(XGaFloat64KVector kv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;

            return kv2 switch
            {
                XGaFloat64Scalar => Processor.ScalarZero,
                XGaFloat64Vector v2 => EHip(v2),
                XGaFloat64Bivector bv2 => EHip(bv2),
                XGaFloat64HigherKVector hkv2 => EHip(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector EHip(XGaFloat64Multivector mv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;

            return mv2 switch
            {
                XGaFloat64Scalar => Processor.ScalarZero,
                XGaFloat64Vector v2 => EHip(v2),
                XGaFloat64Bivector bv2 => EHip(bv2),
                XGaFloat64HigherKVector kv2 => EHip(kv2),
                XGaFloat64GradedMultivector gmv2 => EHip(gmv2),
                XGaFloat64UniformMultivector umv2 => EHip(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        
        public XGaFloat64Scalar Hip(XGaFloat64Scalar _)
        {
            return Processor.ScalarZero;
        }

        
        public virtual XGaFloat64Multivector Hip(XGaFloat64Vector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Hip(kv2),
                XGaFloat64Vector mv1 => mv1.Hip(kv2),
                XGaFloat64Bivector mv1 => mv1.Hip(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Hip(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Hip(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Hip(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector Hip(XGaFloat64Bivector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Hip(kv2),
                XGaFloat64Vector mv1 => mv1.Hip(kv2),
                XGaFloat64Bivector mv1 => mv1.Hip(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Hip(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Hip(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Hip(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector Hip(XGaFloat64HigherKVector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Hip(kv2),
                XGaFloat64Vector mv1 => mv1.Hip(kv2),
                XGaFloat64Bivector mv1 => mv1.Hip(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Hip(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Hip(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Hip(kv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public virtual XGaFloat64Multivector Hip(XGaFloat64GradedMultivector mv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Hip(mv2),
                XGaFloat64Vector mv1 => mv1.Hip(mv2),
                XGaFloat64Bivector mv1 => mv1.Hip(mv2),
                XGaFloat64HigherKVector mv1 => mv1.Hip(mv2),
                XGaFloat64GradedMultivector mv1 => mv1.Hip(mv2),
                XGaFloat64UniformMultivector mv1 => mv1.Hip(mv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public XGaFloat64Multivector Hip(XGaFloat64UniformMultivector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }

        
        public virtual XGaFloat64Multivector Hip(XGaFloat64KVector kv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;

            return kv2 switch
            {
                XGaFloat64Scalar => Processor.ScalarZero,
                XGaFloat64Vector v2 => Hip(v2),
                XGaFloat64Bivector bv2 => Hip(bv2),
                XGaFloat64HigherKVector hkv2 => Hip(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Hip(XGaFloat64Multivector mv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;

            return mv2 switch
            {
                XGaFloat64Scalar => Processor.ScalarZero,
                XGaFloat64Vector v2 => Hip(v2),
                XGaFloat64Bivector bv2 => Hip(bv2),
                XGaFloat64HigherKVector kv2 => Hip(kv2),
                XGaFloat64GradedMultivector gmv2 => Hip(gmv2),
                XGaFloat64UniformMultivector umv2 => Hip(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaFloat64KVector
    {
        
        public new XGaFloat64KVector EHip(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar =>
                    Processor.ScalarZero,

                XGaFloat64Vector v1 =>
                    Float64ScalarComposer
                        .Create()
                        .AddESpTerms(v1, kv2)
                        .GetXGaFloat64Scalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 1)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        
        public new XGaFloat64KVector EHip(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar =>
                    Processor.ScalarZero,

                XGaFloat64Vector kv1 =>
                    Processor
                        .CreateKVectorComposer(1)
                        .AddELcpTerms(kv1, kv2)
                        .GetVector(),

                XGaFloat64Bivector kv1 =>
                    Float64ScalarComposer
                        .Create()
                        .AddESpTerms(kv1, kv2)
                        .GetXGaFloat64Scalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 2)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        
        public new XGaFloat64KVector EHip(XGaFloat64HigherKVector kv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        
        public new XGaFloat64KVector EHip(XGaFloat64KVector kv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64GradedMultivector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }


        
        public new XGaFloat64KVector Hip(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar =>
                    Processor.ScalarZero,

                XGaFloat64Vector v1 =>
                    v1.Sp(kv2),

                _ => Processor
                    .CreateKVectorComposer(Grade - 1)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        
        public new XGaFloat64KVector Hip(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar =>
                    Processor.ScalarZero,

                XGaFloat64Vector kv1 =>
                    Processor
                        .CreateKVectorComposer(1)
                        .AddLcpTerms(kv1, kv2)
                        .GetVector(),

                XGaFloat64Bivector kv1 =>
                    Float64ScalarComposer
                        .Create()
                        .AddSpTerms(kv1, kv2)
                        .GetXGaFloat64Scalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 2)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        
        public new XGaFloat64KVector Hip(XGaFloat64HigherKVector kv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        
        public new XGaFloat64KVector Hip(XGaFloat64KVector kv2)
        {
            if (this is XGaFloat64Scalar)
                return Processor.ScalarZero;
            
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64GradedMultivector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }
    }

    public sealed partial class XGaFloat64Scalar
    {
        
        public new XGaFloat64Scalar EHip(XGaFloat64Vector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar EHip(XGaFloat64Bivector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar EHip(XGaFloat64HigherKVector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar EHip(XGaFloat64KVector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar EHip(XGaFloat64GradedMultivector kv2)
        {
            return Processor.ScalarZero;
        }
        

        
        public new XGaFloat64Scalar Hip(XGaFloat64Vector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar Hip(XGaFloat64Bivector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar Hip(XGaFloat64HigherKVector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar Hip(XGaFloat64KVector kv2)
        {
            return Processor.ScalarZero;
        }
        
        
        public new XGaFloat64Scalar Hip(XGaFloat64GradedMultivector kv2)
        {
            return Processor.ScalarZero;
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        
        public new XGaFloat64Scalar EHip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64Vector EHip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetVector();
        }
        
        
        
        public new XGaFloat64Scalar Hip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64Vector Hip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetVector();
        }
    }

    public sealed partial class XGaFloat64Bivector
    {
        
        public new XGaFloat64Vector EHip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddERcpTerms(this, kv2)
                    .GetVector();
        }

        
        public new XGaFloat64Scalar EHip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }
        
        
        
        public new XGaFloat64Vector Hip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddRcpTerms(this, kv2)
                    .GetVector();
        }

        
        public new XGaFloat64Scalar Hip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

    }

    public partial class XGaFloat64GradedMultivector
    {
        
        public override XGaFloat64Multivector EHip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64KVector kv2)
        {
            return kv2 is XGaFloat64Scalar || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }


        
        public override XGaFloat64Multivector Hip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64KVector kv2)
        {
            return kv2 is XGaFloat64Scalar || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }

    }

    public partial class XGaFloat64UniformMultivector
    {
        
        public override XGaFloat64Multivector EHip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64KVector kv2)
        {
            return kv2 is XGaFloat64Scalar || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector EHip(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEHipTerms(this, mv2)
                    .GetMultivector();
        }


        
        public override XGaFloat64Multivector Hip(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64KVector kv2)
        {
            return kv2 is XGaFloat64Scalar || IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, kv2)
                    .GetMultivector();
        }

        
        public override XGaFloat64Multivector Hip(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddHipTerms(this, mv2)
                    .GetMultivector();
        }

    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {
        
        public XGaFloat64KVectorComposer AddEHipTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        
        public XGaFloat64KVectorComposer AddEHipTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }


        
        public XGaFloat64KVectorComposer AddHipTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        
        public XGaFloat64KVectorComposer AddHipTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

    }

    public sealed partial class XGaFloat64UniformMultivectorComposer
    {
        
        public XGaFloat64UniformMultivectorComposer AddEHipTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        
        public XGaFloat64UniformMultivectorComposer AddEHipTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }


        
        public XGaFloat64UniformMultivectorComposer AddHipTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        
        public XGaFloat64UniformMultivectorComposer AddHipTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EHipIsNonZero
            );
        }

    }
    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        
        public XGaFloat64GradedMultivectorComposer AddEHipTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }
        
        
        public XGaFloat64GradedMultivectorComposer AddEHipTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (kv2.Grade == 0 || mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddEHipTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddEHipTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.Grade == 0 || kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddEHipTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddEHipTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddEHipTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddEHipTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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


        
        public XGaFloat64GradedMultivectorComposer AddHipTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == 0 || kv2.Grade == 0)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }
        
        
        public XGaFloat64GradedMultivectorComposer AddHipTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (kv2.Grade == 0 || mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddHipTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddHipTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.Grade == 0 || kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddHipTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddHipTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddHipTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddHipTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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
