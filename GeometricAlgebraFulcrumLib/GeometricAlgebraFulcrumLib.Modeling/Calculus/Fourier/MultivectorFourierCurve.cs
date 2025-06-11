using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Fourier;

public class XGaMultivectorFourierCurve<T>
{
    public static XGaMultivectorFourierCurve<T> Create(XGaProcessor<T> processor)
    {
        return new XGaMultivectorFourierCurve<T>(processor);
    }


    private readonly SortedDictionary<int, MultivectorFourierCurveTerm<T>> _termsDictionary;

    public XGaProcessor<T> GeometricProcessor { get; }


    private XGaMultivectorFourierCurve(XGaProcessor<T> processor, SortedDictionary<int, MultivectorFourierCurveTerm<T>> termsDictionary)
    {
        GeometricProcessor = processor;
        _termsDictionary = termsDictionary;
    }

    private XGaMultivectorFourierCurve(XGaProcessor<T> processor)
    {
        GeometricProcessor = processor;
        _termsDictionary = new SortedDictionary<int, MultivectorFourierCurveTerm<T>>();
    }


    public XGaMultivectorFourierCurve<T> AddTermMultivectors(int key, XGaMultivector<T> cosMultivector, XGaMultivector<T> sinMultivector)
    {
        if (!_termsDictionary.TryGetValue(key, out var term))
            throw new InvalidOperationException();

        term.AddMultivectors(cosMultivector, sinMultivector);

        return this;
    }

    public XGaMultivectorFourierCurve<T> AddTermMultivectors(int key, Scalar<T> frequency, XGaMultivector<T> cosMultivector, XGaMultivector<T> sinMultivector)
    {
        if (_termsDictionary.TryGetValue(key, out var term))
        {
            if (!(frequency - term.Frequency).IsNearZero())
                throw new InvalidOperationException();

            term.AddMultivectors(cosMultivector, sinMultivector);
        }
        else
        {
            term = new MultivectorFourierCurveTerm<T>(cosMultivector, sinMultivector, frequency);

            _termsDictionary.Add(key, term);
        }

        return this;
    }

    public XGaMultivectorFourierCurve<T> SetTerm(int key, Scalar<T> frequency, XGaMultivector<T> cosMultivector, XGaMultivector<T> sinMultivector)
    {
        var term = new MultivectorFourierCurveTerm<T>(cosMultivector, sinMultivector, frequency);

        if (_termsDictionary.ContainsKey(key))
            _termsDictionary[key] = term;
        else
            _termsDictionary.Add(key, term);

        return this;
    }
        
    public XGaMultivectorFourierCurve<T> SetTerm(int key, T frequency, XGaMultivector<T> cosMultivector, XGaMultivector<T> sinMultivector)
    {
        var term = new MultivectorFourierCurveTerm<T>(
            cosMultivector, 
            sinMultivector, 
            frequency.ScalarFromValue(cosMultivector.ScalarProcessor)
        );

        if (_termsDictionary.ContainsKey(key))
            _termsDictionary[key] = term;
        else
            _termsDictionary.Add(key, term);

        return this;
    }

    public XGaMultivectorFourierCurve<T> GetDerivative(int degree = 1)
    {
        var termsDictionary = new SortedDictionary<int, MultivectorFourierCurveTerm<T>>();

        foreach (var (key, term) in _termsDictionary)
            termsDictionary.Add(
                key, 
                term.GetDerivative(degree)
            );

        return new XGaMultivectorFourierCurve<T>(GeometricProcessor, termsDictionary);
    }

        
    public XGaMultivector<T> GetValue(Scalar<T> parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            (XGaMultivector<T>) GeometricProcessor.GradedMultivectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public XGaMultivector<T> GetValue(T parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            (XGaMultivector<T>) GeometricProcessor.GradedMultivectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public Pair<XGaVector<T>> GetLocalFrame2D(T parameterValue)
    {
        var vDt1 = GetDerivative(1).GetValue(parameterValue).GetVectorPart();
        var vDt2 = GetDerivative(2).GetValue(parameterValue).GetVectorPart();

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm();
        //var e1d = (e1.DifferentiateScalars("t") / vDt1Norm);
            
        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm();
        //var e2d = (e2.DifferentiateScalars("t") / vDt1Norm);

        return new Pair<XGaVector<T>>(e1, e2);
    }

    public Triplet<XGaVector<T>> GetLocalFrame3D(T parameterValue)
    {
        var vDt1 = GetDerivative(1).GetValue(parameterValue).GetVectorPart();
        var vDt2 = GetDerivative(2).GetValue(parameterValue).GetVectorPart();
        var vDt3 = GetDerivative(3).GetValue(parameterValue).GetVectorPart();

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm();
        //var e1d = (e1.DifferentiateScalars("t") / vDt1Norm);
            
        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm();
        //var e2d = (e2.DifferentiateScalars("t") / vDt1Norm);
                
        var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm();
        //var e3d = (e3.DifferentiateScalars("t") / vDt1Norm);

        return new Triplet<XGaVector<T>>(e1, e2, e3);
    }
        
    public Quad<XGaVector<T>> GetLocalFrame4D(T parameterValue)
    {
        var vDt1 = GetDerivative(1).GetValue(parameterValue).GetVectorPart();
        var vDt2 = GetDerivative(2).GetValue(parameterValue).GetVectorPart();
        var vDt3 = GetDerivative(3).GetValue(parameterValue).GetVectorPart();
        var vDt4 = GetDerivative(4).GetValue(parameterValue).GetVectorPart();

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm();
        //var e1d = (e1.DifferentiateScalars("t") / vDt1Norm);
            
        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm();
        //var e2d = (e2.DifferentiateScalars("t") / vDt1Norm);
                
        var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm();
        //var e3d = (e3.DifferentiateScalars("t") / vDt1Norm);
                
        var u4 = vDt4 - vDt4.ProjectOn(u1.ToSubspace()) - vDt4.ProjectOn(u2.ToSubspace()) - vDt4.ProjectOn(u3.ToSubspace());
        var e4 = u4.DivideByNorm();
        //var e4d = (e4.DifferentiateScalars("t") / vDt1Norm);

        return new Quad<XGaVector<T>>(e1, e2, e3, e4);
    }
}