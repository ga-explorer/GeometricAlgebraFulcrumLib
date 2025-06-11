using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        public abstract XGaFloat64Scalar ESp(XGaFloat64Scalar kv2);

        public abstract XGaFloat64Scalar ESp(XGaFloat64Vector kv2);

        public abstract XGaFloat64Scalar ESp(XGaFloat64Bivector kv2);

        public abstract XGaFloat64Scalar ESp(XGaFloat64HigherKVector kv2);

        public abstract XGaFloat64Scalar ESp(XGaFloat64KVector kv2);

        public abstract XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2);

        public abstract XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2);

        public abstract XGaFloat64Scalar ESp(XGaFloat64Multivector mv2);

        
        public virtual double ESpSquared()
        {
            return IsZero
                ? 0d
                : IdScalarPairs.Select(p =>
                    Metric.EGpSquaredSign(p.Key) * p.Value * p.Value
                ).Sum();
        }


        public abstract XGaFloat64Scalar Sp(XGaFloat64Scalar kv2);

        public abstract XGaFloat64Scalar Sp(XGaFloat64Vector kv2);

        public abstract XGaFloat64Scalar Sp(XGaFloat64Bivector kv2);

        public abstract XGaFloat64Scalar Sp(XGaFloat64HigherKVector kv2);

        public abstract XGaFloat64Scalar Sp(XGaFloat64KVector kv2);

        public abstract XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2);

        public abstract XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2);

        public abstract XGaFloat64Scalar Sp(XGaFloat64Multivector mv2);

        
        public virtual double SpSquared()
        {
            return IsZero
                ? 0d
                : IdScalarPairs.Select(p =>
                    Metric.GpSquaredSign(p.Key) * p.Value * p.Value
                ).Sum();
        }

    }

    public abstract partial class XGaFloat64KVector
    {
        
        public override XGaFloat64Scalar ESp(XGaFloat64Scalar kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            return this is XGaFloat64Scalar kv1
                ? Processor.Scalar(kv1.ScalarValue * kv2.ScalarValue)
                : Processor.ScalarZero;
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaFloat64Vector kv1
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(kv1, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaFloat64Bivector kv1
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(kv1, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero || Grade != kv2.Grade
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64KVector kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2 is not XGaFloat64Scalar s2
                    ? Processor.ScalarZero
                    : Processor.Scalar(s1.ScalarValue * s2.ScalarValue);

            return Grade != kv2.Grade
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = Float64ScalarComposer.Create();

            if (mv2 is XGaFloat64KVector kv2)
                composer.AddESpTerms(this, kv2);

            else if (mv2 is XGaFloat64GradedMultivector gmv2)
                composer.AddESpTerms(this, gmv2);

            else if (mv2 is XGaFloat64UniformMultivector umv2)
                composer.AddESpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaFloat64Scalar(Processor);
        }


        
        public override XGaFloat64Scalar Sp(XGaFloat64Scalar kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            return this is XGaFloat64Scalar kv1
                ? Processor.Scalar(kv1.ScalarValue * kv2.ScalarValue)
                : Processor.ScalarZero;
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaFloat64Vector kv1
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(kv1, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero || this is not XGaFloat64Bivector kv1
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(kv1, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero || Grade != kv2.Grade
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64KVector kv2)
        {
            if (IsZero || kv2.IsZero) 
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2 is not XGaFloat64Scalar s2
                    ? Processor.ScalarZero
                    : Processor.Scalar(s1.ScalarValue * s2.ScalarValue);

            return Grade != kv2.Grade
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = Float64ScalarComposer.Create();

            if (mv2 is XGaFloat64KVector kv2)
                composer.AddSpTerms(this, kv2);

            else if (mv2 is XGaFloat64GradedMultivector gmv2)
                composer.AddSpTerms(this, gmv2);

            else if (mv2 is XGaFloat64UniformMultivector umv2)
                composer.AddSpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaFloat64Scalar(Processor);
        }
    }

    public sealed partial class XGaFloat64Scalar
    {
        
        public override double ESpSquared()
        {
            return IsZero
                ? 0d
                : ScalarValue.Square();
        }

        
        public override double SpSquared()
        {
            return IsZero
                ? 0d
                : ScalarValue.Square();
        }


        
        public override XGaFloat64Scalar ESp(XGaFloat64Scalar kv2)
        {
            return IsZero || kv2.IsZero 
                ? Processor.ScalarZero 
                : Processor.Scalar(ScalarValue * kv2.ScalarValue);
        }
        
        
        public override XGaFloat64Scalar Sp(XGaFloat64Scalar kv2)
        {
            return IsZero || kv2.IsZero 
                ? Processor.ScalarZero 
                : Processor.Scalar(ScalarValue * kv2.ScalarValue);
        }
    }

    public partial class XGaFloat64GradedMultivector
    {
        
        public override XGaFloat64Scalar ESp(XGaFloat64Scalar kv2)
        {
            return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
                ? Processor.Scalar(((XGaFloat64Scalar)scalarPart).ScalarValue * kv2.ScalarValue)
                : Processor.ScalarZero;
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = Float64ScalarComposer.Create();

            if (mv2 is XGaFloat64KVector kv2)
                composer.AddESpTerms(this, kv2);

            else if (mv2 is XGaFloat64GradedMultivector gmv2)
                composer.AddESpTerms(this, gmv2);

            else if (mv2 is XGaFloat64UniformMultivector umv2)
                composer.AddESpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaFloat64Scalar(Processor);
        }

        
        
        public override XGaFloat64Scalar Sp(XGaFloat64Scalar kv2)
        {
            return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
                ? Processor.Scalar(((XGaFloat64Scalar)scalarPart).ScalarValue * kv2.ScalarValue)
                : Processor.ScalarZero;
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Multivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            var composer = Float64ScalarComposer.Create();

            if (mv2 is XGaFloat64KVector kv2)
                composer.AddSpTerms(this, kv2);

            else if (mv2 is XGaFloat64GradedMultivector gmv2)
                composer.AddSpTerms(this, gmv2);

            else if (mv2 is XGaFloat64UniformMultivector umv2)
                composer.AddSpTerms(this, umv2);

            else
                throw new InvalidOperationException();

            return composer.GetXGaFloat64Scalar(Processor);
        }
    }

    public partial class XGaFloat64UniformMultivector
    {

        
        public override XGaFloat64Scalar ESp(XGaFloat64Scalar kv2)
        {
            return TryGetScalarValue(out var s1) || kv2.IsZero
                ? Processor.ScalarZero
                : Processor.Scalar(s1 * kv2.ScalarValue);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar ESp(XGaFloat64Multivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }


        
        public override XGaFloat64Scalar Sp(XGaFloat64Scalar kv2)
        {
            return TryGetScalarValue(out var s1) || kv2.IsZero
                ? Processor.ScalarZero
                : Processor.Scalar(s1 * kv2.ScalarValue);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        
        public override XGaFloat64Scalar Sp(XGaFloat64Multivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, mv2)
                    .GetXGaFloat64Scalar(Processor);
        }

    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {
        
        public XGaFloat64KVectorComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(kv1, kv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(mv1, kv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(kv1, mv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(mv1, mv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddESpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddESpTerms(mv1, mv2);
            
            return this;
        }


        
        public XGaFloat64KVectorComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(kv1, kv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(mv1, kv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(kv1, mv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(mv1, mv2);
            
            return this;
        }

        
        public XGaFloat64KVectorComposer AddSpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            if (!IsScalar)
                throw new InvalidOperationException();

            _scalarComposer.AddSpTerms(mv1, mv2);
            
            return this;
        }

    }
    
    public sealed partial class XGaFloat64UniformMultivectorComposer
    {
        
        public XGaFloat64UniformMultivectorComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        
        public XGaFloat64UniformMultivectorComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        
        public XGaFloat64UniformMultivectorComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        
        public XGaFloat64UniformMultivectorComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, mv2).ScalarValue
            );
        }

        
        public XGaFloat64UniformMultivectorComposer AddESpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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


        
        public XGaFloat64UniformMultivectorComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        
        public XGaFloat64UniformMultivectorComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        
        public XGaFloat64UniformMultivectorComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        
        public XGaFloat64UniformMultivectorComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, mv2).ScalarValue
            );
        }

        
        public XGaFloat64UniformMultivectorComposer AddSpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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
    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        
        public XGaFloat64GradedMultivectorComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(kv1, kv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, kv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddESpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(kv1, mv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddESpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, mv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddESpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddESpTerms(mv1, mv2).ScalarValue
            );
        }


        
        public XGaFloat64GradedMultivectorComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(kv1, kv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, kv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddSpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(kv1, mv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddSpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, mv2).ScalarValue
            );
        }

        
        public XGaFloat64GradedMultivectorComposer AddSpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddScalarTerm(
                _scalarComposer.Clear().AddSpTerms(mv1, mv2).ScalarValue
            );
        }
    }

}
