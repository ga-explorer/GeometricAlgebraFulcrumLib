using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        
        public XGaFloat64Multivector EFdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public virtual XGaFloat64Multivector EFdp(XGaFloat64Vector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EFdp(kv2),
                XGaFloat64Vector mv1 => mv1.EFdp(kv2),
                XGaFloat64Bivector mv1 => mv1.EFdp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.EFdp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.EFdp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.EFdp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector EFdp(XGaFloat64Bivector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EFdp(kv2),
                XGaFloat64Vector mv1 => mv1.EFdp(kv2),
                XGaFloat64Bivector mv1 => mv1.EFdp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.EFdp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.EFdp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.EFdp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector EFdp(XGaFloat64HigherKVector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EFdp(kv2),
                XGaFloat64Vector mv1 => mv1.EFdp(kv2),
                XGaFloat64Bivector mv1 => mv1.EFdp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.EFdp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.EFdp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.EFdp(kv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public virtual XGaFloat64Multivector EFdp(XGaFloat64GradedMultivector mv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.EFdp(mv2),
                XGaFloat64Vector mv1 => mv1.EFdp(mv2),
                XGaFloat64Bivector mv1 => mv1.EFdp(mv2),
                XGaFloat64HigherKVector mv1 => mv1.EFdp(mv2),
                XGaFloat64GradedMultivector mv1 => mv1.EFdp(mv2),
                XGaFloat64UniformMultivector mv1 => mv1.EFdp(mv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public XGaFloat64Multivector EFdp(XGaFloat64UniformMultivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public virtual XGaFloat64Multivector EFdp(XGaFloat64KVector kv2)
        {
            return kv2 switch
            {
                XGaFloat64Scalar s2 => EFdp(s2),
                XGaFloat64Vector v2 => EFdp(v2),
                XGaFloat64Bivector bv2 => EFdp(bv2),
                XGaFloat64HigherKVector hkv2 => EFdp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector EFdp(XGaFloat64Multivector mv2)
        {
            return mv2 switch
            {
                XGaFloat64Scalar s2 => EFdp(s2),
                XGaFloat64Vector v2 => EFdp(v2),
                XGaFloat64Bivector bv2 => EFdp(bv2),
                XGaFloat64HigherKVector kv2 => EFdp(kv2),
                XGaFloat64GradedMultivector gmv2 => EFdp(gmv2),
                XGaFloat64UniformMultivector umv2 => EFdp(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        
        public XGaFloat64Multivector Fdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public virtual XGaFloat64Multivector Fdp(XGaFloat64Vector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Fdp(kv2),
                XGaFloat64Vector mv1 => mv1.Fdp(kv2),
                XGaFloat64Bivector mv1 => mv1.Fdp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Fdp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Fdp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Fdp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector Fdp(XGaFloat64Bivector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Fdp(kv2),
                XGaFloat64Vector mv1 => mv1.Fdp(kv2),
                XGaFloat64Bivector mv1 => mv1.Fdp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Fdp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Fdp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Fdp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector Fdp(XGaFloat64HigherKVector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Fdp(kv2),
                XGaFloat64Vector mv1 => mv1.Fdp(kv2),
                XGaFloat64Bivector mv1 => mv1.Fdp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Fdp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Fdp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Fdp(kv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public virtual XGaFloat64Multivector Fdp(XGaFloat64GradedMultivector mv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Fdp(mv2),
                XGaFloat64Vector mv1 => mv1.Fdp(mv2),
                XGaFloat64Bivector mv1 => mv1.Fdp(mv2),
                XGaFloat64HigherKVector mv1 => mv1.Fdp(mv2),
                XGaFloat64GradedMultivector mv1 => mv1.Fdp(mv2),
                XGaFloat64UniformMultivector mv1 => mv1.Fdp(mv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public XGaFloat64Multivector Fdp(XGaFloat64UniformMultivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public virtual XGaFloat64Multivector Fdp(XGaFloat64KVector kv2)
        {
            return kv2 switch
            {
                XGaFloat64Scalar s2 => Fdp(s2),
                XGaFloat64Vector v2 => Fdp(v2),
                XGaFloat64Bivector bv2 => Fdp(bv2),
                XGaFloat64HigherKVector hkv2 => Fdp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Fdp(XGaFloat64Multivector mv2)
        {
            return mv2 switch
            {
                XGaFloat64Scalar s2 => Fdp(s2),
                XGaFloat64Vector v2 => Fdp(v2),
                XGaFloat64Bivector bv2 => Fdp(bv2),
                XGaFloat64HigherKVector kv2 => Fdp(kv2),
                XGaFloat64GradedMultivector gmv2 => Fdp(gmv2),
                XGaFloat64UniformMultivector umv2 => Fdp(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaFloat64KVector
    {

        
        public new XGaFloat64KVector EFdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64KVector EFdp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar s1 =>
                    kv2.Times(s1.ScalarValue),

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

        
        public new XGaFloat64KVector EFdp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar kv1 =>
                    kv2.Times(kv1.ScalarValue),

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

        
        public new XGaFloat64KVector EFdp(XGaFloat64HigherKVector kv2)
        {
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        
        public new XGaFloat64KVector EFdp(XGaFloat64KVector kv2)
        {
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64GradedMultivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        
        public new XGaFloat64KVector Fdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64KVector Fdp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaFloat64Vector v1 =>
                    v1.Sp(kv2),

                _ => Processor
                    .CreateKVectorComposer(Grade - 1)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        
        public new XGaFloat64KVector Fdp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar kv1 =>
                    kv2.Times(kv1.ScalarValue),

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

        
        public new XGaFloat64KVector Fdp(XGaFloat64HigherKVector kv2)
        {
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        
        public new XGaFloat64KVector Fdp(XGaFloat64KVector kv2)
        {
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64GradedMultivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public sealed partial class XGaFloat64Scalar
    {
        
        public new XGaFloat64Scalar EFdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64Vector EFdp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public new XGaFloat64Bivector EFdp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public new XGaFloat64HigherKVector EFdp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public new XGaFloat64KVector EFdp(XGaFloat64KVector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public override XGaFloat64Multivector EFdp(XGaFloat64GradedMultivector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        

        
        public new XGaFloat64Scalar Fdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64Vector Fdp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public new XGaFloat64Bivector Fdp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public new XGaFloat64HigherKVector Fdp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public new XGaFloat64KVector Fdp(XGaFloat64KVector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        
        public override XGaFloat64Multivector Fdp(XGaFloat64GradedMultivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        
        public new XGaFloat64Vector EFdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64Scalar EFdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64Vector EFdp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetVector();
        }
        
        
        
        public new XGaFloat64Vector Fdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        
        public new XGaFloat64Scalar Fdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64Vector Fdp(XGaFloat64Bivector kv2)
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
        
        public new XGaFloat64Bivector EFdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        
        public new XGaFloat64Vector EFdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddERcpTerms(this, kv2)
                    .GetVector();
        }

        
        public new XGaFloat64Scalar EFdp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }
        
        
        
        public new XGaFloat64Bivector Fdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        
        public new XGaFloat64Vector Fdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddRcpTerms(this, kv2)
                    .GetVector();
        }

        
        public new XGaFloat64Scalar Fdp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

    }

    public sealed partial class XGaFloat64HigherKVector
    {
        
        public new XGaFloat64HigherKVector EFdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        
        
        public new XGaFloat64HigherKVector Fdp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public partial class XGaFloat64GradedMultivector
    {
        
        public override XGaFloat64Multivector EFdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64KVector kv2)
        {
            if (kv2 is XGaFloat64Scalar scalar)
                return Times(scalar.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        
        public override XGaFloat64Multivector Fdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64KVector kv2)
        {
            if (kv2 is XGaFloat64Scalar scalar)
                return Times(scalar.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public partial class XGaFloat64UniformMultivector
    {
        
        public override XGaFloat64Multivector EFdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector EFdp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        
        public override XGaFloat64Multivector Fdp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Fdp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {
        
        public XGaFloat64KVectorComposer AddEFdpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        
        public XGaFloat64KVectorComposer AddEFdpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }


        
        public XGaFloat64KVectorComposer AddFdpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        
        public XGaFloat64KVectorComposer AddFdpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

    }

    public sealed partial class XGaFloat64UniformMultivectorComposer
    {
        
        public XGaFloat64UniformMultivectorComposer AddEFdpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        
        public XGaFloat64UniformMultivectorComposer AddEFdpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }


        
        public XGaFloat64UniformMultivectorComposer AddFdpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        
        public XGaFloat64UniformMultivectorComposer AddFdpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        
        public XGaFloat64GradedMultivectorComposer AddEFdpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }
        
        
        public XGaFloat64GradedMultivectorComposer AddEFdpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddEFdpTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddEFdpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddEFdpTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddEFdpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddEFdpTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddEFdpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var mv2KVectors = 
                mv2.GetKVectorParts().ToArray();

            foreach (var kv1 in mv1.GetKVectorParts())
            foreach (var kv2 in mv2KVectors)
                AddEFdpTerms(kv1, kv2);

            return this;
        }


        
        public XGaFloat64GradedMultivectorComposer AddFdpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }
        
        
        public XGaFloat64GradedMultivectorComposer AddFdpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddFdpTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddFdpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddFdpTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddFdpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddFdpTerms(kv1, kv2);

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddFdpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var mv2KVectors = 
                mv2.GetKVectorParts().ToArray();

            foreach (var kv1 in mv1.GetKVectorParts())
            foreach (var kv2 in mv2KVectors)
                AddFdpTerms(kv1, kv2);

            return this;
        }

    }
}
