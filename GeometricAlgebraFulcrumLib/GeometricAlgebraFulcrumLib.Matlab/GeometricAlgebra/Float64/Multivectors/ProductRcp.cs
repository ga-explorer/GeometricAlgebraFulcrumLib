using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        
        public XGaFloat64Multivector ERcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public virtual XGaFloat64Multivector ERcp(XGaFloat64Vector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.ERcp(kv2),
                XGaFloat64Vector mv1 => mv1.ERcp(kv2),
                XGaFloat64Bivector mv1 => mv1.ERcp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.ERcp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.ERcp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.ERcp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector ERcp(XGaFloat64Bivector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.ERcp(kv2),
                XGaFloat64Vector mv1 => mv1.ERcp(kv2),
                XGaFloat64Bivector mv1 => mv1.ERcp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.ERcp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.ERcp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.ERcp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector ERcp(XGaFloat64HigherKVector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.ERcp(kv2),
                XGaFloat64Vector mv1 => mv1.ERcp(kv2),
                XGaFloat64Bivector mv1 => mv1.ERcp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.ERcp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.ERcp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.ERcp(kv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public virtual XGaFloat64Multivector ERcp(XGaFloat64GradedMultivector mv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.ERcp(mv2),
                XGaFloat64Vector mv1 => mv1.ERcp(mv2),
                XGaFloat64Bivector mv1 => mv1.ERcp(mv2),
                XGaFloat64HigherKVector mv1 => mv1.ERcp(mv2),
                XGaFloat64GradedMultivector mv1 => mv1.ERcp(mv2),
                XGaFloat64UniformMultivector mv1 => mv1.ERcp(mv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public XGaFloat64Multivector ERcp(XGaFloat64UniformMultivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.TryGetScalarValue(out var s2)
                    ? Processor.Scalar(s1.ScalarValue * s2)
                    : Processor.ScalarZero;

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public virtual XGaFloat64Multivector ERcp(XGaFloat64KVector kv2)
        {
            return kv2 switch
            {
                XGaFloat64Scalar s2 => ERcp(s2),
                XGaFloat64Vector v2 => ERcp(v2),
                XGaFloat64Bivector bv2 => ERcp(bv2),
                XGaFloat64HigherKVector hkv2 => ERcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector ERcp(XGaFloat64Multivector mv2)
        {
            return mv2 switch
            {
                XGaFloat64Scalar s2 => ERcp(s2),
                XGaFloat64Vector v2 => ERcp(v2),
                XGaFloat64Bivector bv2 => ERcp(bv2),
                XGaFloat64HigherKVector kv2 => ERcp(kv2),
                XGaFloat64GradedMultivector gmv2 => ERcp(gmv2),
                XGaFloat64UniformMultivector umv2 => ERcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        
        public XGaFloat64Multivector Rcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public virtual XGaFloat64Multivector Rcp(XGaFloat64Vector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Rcp(kv2),
                XGaFloat64Vector mv1 => mv1.Rcp(kv2),
                XGaFloat64Bivector mv1 => mv1.Rcp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Rcp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Rcp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Rcp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector Rcp(XGaFloat64Bivector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Rcp(kv2),
                XGaFloat64Vector mv1 => mv1.Rcp(kv2),
                XGaFloat64Bivector mv1 => mv1.Rcp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Rcp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Rcp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Rcp(kv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public virtual XGaFloat64Multivector Rcp(XGaFloat64HigherKVector kv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Rcp(kv2),
                XGaFloat64Vector mv1 => mv1.Rcp(kv2),
                XGaFloat64Bivector mv1 => mv1.Rcp(kv2),
                XGaFloat64HigherKVector mv1 => mv1.Rcp(kv2),
                XGaFloat64GradedMultivector mv1 => mv1.Rcp(kv2),
                XGaFloat64UniformMultivector mv1 => mv1.Rcp(kv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public virtual XGaFloat64Multivector Rcp(XGaFloat64GradedMultivector mv2)
        {
            return this switch
            {
                XGaFloat64Scalar mv1 => mv1.Rcp(mv2),
                XGaFloat64Vector mv1 => mv1.Rcp(mv2),
                XGaFloat64Bivector mv1 => mv1.Rcp(mv2),
                XGaFloat64HigherKVector mv1 => mv1.Rcp(mv2),
                XGaFloat64GradedMultivector mv1 => mv1.Rcp(mv2),
                XGaFloat64UniformMultivector mv1 => mv1.Rcp(mv2),
                _ => throw new InvalidOperationException()
            };
        }
        
        
        public XGaFloat64Multivector Rcp(XGaFloat64UniformMultivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.TryGetScalarValue(out var s2)
                    ? Processor.Scalar(s1.ScalarValue * s2)
                    : Processor.ScalarZero;

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        
        public virtual XGaFloat64Multivector Rcp(XGaFloat64KVector kv2)
        {
            return kv2 switch
            {
                XGaFloat64Scalar s2 => Rcp(s2),
                XGaFloat64Vector v2 => Rcp(v2),
                XGaFloat64Bivector bv2 => Rcp(bv2),
                XGaFloat64HigherKVector hkv2 => Rcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        
        public XGaFloat64Multivector Rcp(XGaFloat64Multivector mv2)
        {
            return mv2 switch
            {
                XGaFloat64Scalar s2 => Rcp(s2),
                XGaFloat64Vector v2 => Rcp(v2),
                XGaFloat64Bivector bv2 => Rcp(bv2),
                XGaFloat64HigherKVector kv2 => Rcp(kv2),
                XGaFloat64GradedMultivector gmv2 => Rcp(gmv2),
                XGaFloat64UniformMultivector umv2 => Rcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaFloat64KVector
    {

        
        public new XGaFloat64KVector ERcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64KVector ERcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 1)
                return Processor.ScalarZero;

            if (this is XGaFloat64Vector v1)
                return Float64ScalarComposer
                    .Create()
                    .AddESpTerms(v1, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 1)
                .AddERcpTerms(this, kv2)
                .GetKVector();
        }

        
        public new XGaFloat64KVector ERcp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 2)
                return Processor.ScalarZero;

            if (this is XGaFloat64Bivector bv1)
                return Float64ScalarComposer
                    .Create()
                    .AddESpTerms(bv1, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 2)
                .AddERcpTerms(this, kv2)
                .GetKVector();
        }

        
        public new XGaFloat64KVector ERcp(XGaFloat64HigherKVector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return (Grade - kv2.Grade) switch
            {
                < 0 => Processor.ScalarZero,

                0 => Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - kv2.Grade)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        
        public new XGaFloat64KVector ERcp(XGaFloat64KVector kv2)
        {
            if (IsZero || kv2.IsZero || Grade < kv2.Grade)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2 is XGaFloat64Scalar s2
                    ? Processor.Scalar(s1.ScalarValue * s2.ScalarValue)
                    : Processor.ScalarZero;

            if (kv2 is XGaFloat64Scalar s3)
                return Times(s3.ScalarValue);

            if (Grade == kv2.Grade)
                return Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - kv2.Grade)
                .AddERcpTerms(this, kv2)
                .GetKVector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64GradedMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return Processor.Scalar(s1.ScalarValue * mv2.Scalar());

            return Processor
                .CreateMultivectorComposer()
                .AddERcpTerms(this, mv2)
                .GetSimpleMultivector();
        }


        
        public new XGaFloat64KVector Rcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64KVector Rcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 1)
                return Processor.ScalarZero;

            if (this is XGaFloat64Vector v1)
                return Float64ScalarComposer
                    .Create()
                    .AddSpTerms(v1, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 1)
                .AddRcpTerms(this, kv2)
                .GetKVector();
        }

        
        public new XGaFloat64KVector Rcp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 2)
                return Processor.ScalarZero;

            if (this is XGaFloat64Bivector bv1)
                return Float64ScalarComposer
                    .Create()
                    .AddSpTerms(bv1, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 2)
                .AddRcpTerms(this, kv2)
                .GetKVector();
        }

        
        public new XGaFloat64KVector Rcp(XGaFloat64HigherKVector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return (Grade - kv2.Grade) switch
            {
                < 0 => Processor.ScalarZero,

                0 => Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - kv2.Grade)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        
        public new XGaFloat64KVector Rcp(XGaFloat64KVector kv2)
        {
            if (IsZero || kv2.IsZero || Grade < kv2.Grade)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2 is XGaFloat64Scalar s2
                    ? Processor.Scalar(s1.ScalarValue * s2.ScalarValue)
                    : Processor.ScalarZero;

            if (kv2 is XGaFloat64Scalar s3)
                return Times(s3.ScalarValue);

            if (Grade == kv2.Grade)
                return Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - kv2.Grade)
                .AddRcpTerms(this, kv2)
                .GetKVector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64GradedMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return Processor.Scalar(s1.ScalarValue * mv2.Scalar());

            return Processor
                .CreateMultivectorComposer()
                .AddRcpTerms(this, mv2)
                .GetSimpleMultivector();
        }

    }

    public sealed partial class XGaFloat64Scalar
    {

        
        public new XGaFloat64Scalar ERcp(XGaFloat64Scalar kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64Vector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64Bivector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64HigherKVector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64KVector kv2)
        {
            return kv2.TryGetScalarValue(out var s2)
                ? Times(s2)
                : Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetScalarValue(out var s2)
                ? Times(s2)
                : Processor.ScalarZero;
        }


        
        public new XGaFloat64Scalar Rcp(XGaFloat64Scalar kv2)
        {
            return kv2.Times(ScalarValue);
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64Vector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64Bivector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64HigherKVector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64KVector kv2)
        {
            return kv2 is XGaFloat64Scalar kv
                ? kv.Times(ScalarValue)
                : Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((XGaFloat64Scalar)mv).ScalarValue)
                : Processor.ScalarZero;
        }

    }

    public sealed partial class XGaFloat64Vector
    {

        
        public new XGaFloat64Vector ERcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, kv2)
                .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64Bivector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64HigherKVector _)
        {
            return Processor.ScalarZero;
        }


        
        public new XGaFloat64Vector Rcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, kv2)
                .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64Bivector _)
        {
            return Processor.ScalarZero;
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64HigherKVector _)
        {
            return Processor.ScalarZero;
        }

    }

    public sealed partial class XGaFloat64Bivector
    {

        
        public new XGaFloat64Bivector ERcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64Vector ERcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.VectorZero;

            return Processor
                .CreateKVectorComposer(1)
                .AddERcpTerms(this, kv2)
                .GetVector();
        }

        
        public new XGaFloat64Scalar ERcp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, kv2)
                .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64KVector ERcp(XGaFloat64HigherKVector _)
        {
            return Processor.ScalarZero;
        }


        
        public new XGaFloat64Bivector Rcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        
        public new XGaFloat64Vector Rcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.VectorZero;

            return Processor
                .CreateKVectorComposer(1)
                .AddRcpTerms(this, kv2)
                .GetVector();
        }

        
        public new XGaFloat64Scalar Rcp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, kv2)
                .GetXGaFloat64Scalar(Processor);
        }

        
        public new XGaFloat64KVector Rcp(XGaFloat64HigherKVector _)
        {
            return Processor.ScalarZero;
        }

    }

    public sealed partial class XGaFloat64HigherKVector
    {

        
        public new XGaFloat64HigherKVector ERcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }


        
        public new XGaFloat64HigherKVector Rcp(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public partial class XGaFloat64GradedMultivector
    {
        
        public override XGaFloat64Multivector ERcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        
        public override XGaFloat64Multivector Rcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public partial class XGaFloat64UniformMultivector
    {
        
        public override XGaFloat64Multivector ERcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector ERcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        
        public override XGaFloat64Multivector Rcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        
        public override XGaFloat64Multivector Rcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public sealed partial class XGaFloat64UniformMultivectorComposer
    {

        public XGaFloat64UniformMultivectorComposer AddERcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return AddEuclideanProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaFloat64UniformMultivectorComposer AddERcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64UniformMultivectorComposer AddERcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64UniformMultivectorComposer AddERcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        
        public XGaFloat64UniformMultivectorComposer AddERcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }


        public XGaFloat64UniformMultivectorComposer AddRcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return AddMetricProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaFloat64UniformMultivectorComposer AddRcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64UniformMultivectorComposer AddRcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64UniformMultivectorComposer AddRcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        
        public XGaFloat64UniformMultivectorComposer AddRcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {

        public XGaFloat64KVectorComposer AddERcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return AddEuclideanProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaFloat64KVectorComposer AddERcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64KVectorComposer AddERcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64KVectorComposer AddERcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        
        public XGaFloat64KVectorComposer AddERcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }


        public XGaFloat64KVectorComposer AddRcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return AddMetricProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaFloat64KVectorComposer AddRcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64KVectorComposer AddRcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64KVectorComposer AddRcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        
        public XGaFloat64KVectorComposer AddRcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {

        public XGaFloat64GradedMultivectorComposer AddERcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);
            
            var composer = 
                Processor
                    .CreateKVectorComposer(kv1.Grade - kv2.Grade)
                    .AddERcpTerms(kv1, kv2);

            if (!composer.IsZero)
                AddKVectorTerms(
                    kv1.Grade - kv2.Grade, 
                    composer.IdScalarPairs
                );

            return this;
        }

        public XGaFloat64GradedMultivectorComposer AddERcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64GradedMultivectorComposer AddERcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64GradedMultivectorComposer AddERcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64GradedMultivectorComposer AddERcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.GetKVectorParts())
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.GetKVectorParts().Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }


        public XGaFloat64GradedMultivectorComposer AddRcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);
            
            var composer = 
                Processor
                    .CreateKVectorComposer(kv1.Grade - kv2.Grade)
                    .AddRcpTerms(kv1, kv2);

            if (!composer.IsZero)
                AddKVectorTerms(
                    kv1.Grade - kv2.Grade, 
                    composer.IdScalarPairs
                );

            return this;
        }

        public XGaFloat64GradedMultivectorComposer AddRcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64GradedMultivectorComposer AddRcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaFloat64GradedMultivectorComposer AddRcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        
        public XGaFloat64GradedMultivectorComposer AddRcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.GetKVectorParts())
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.GetKVectorParts().Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

    }
}
