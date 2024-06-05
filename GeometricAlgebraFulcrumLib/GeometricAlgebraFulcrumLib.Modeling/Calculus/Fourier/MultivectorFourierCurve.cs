using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Fourier;

public class RGaMultivectorFourierCurve<T>
{
    public static RGaMultivectorFourierCurve<T> Create(RGaProcessor<T> processor)
    {
        return new RGaMultivectorFourierCurve<T>(processor);
    }


    private readonly SortedDictionary<int, MultivectorFourierCurveTerm<T>> _termsDictionary;

    public RGaProcessor<T> GeometricProcessor { get; }


    private RGaMultivectorFourierCurve(RGaProcessor<T> processor, SortedDictionary<int, MultivectorFourierCurveTerm<T>> termsDictionary)
    {
        GeometricProcessor = processor;
        _termsDictionary = termsDictionary;
    }

    private RGaMultivectorFourierCurve(RGaProcessor<T> processor)
    {
        GeometricProcessor = processor;
        _termsDictionary = new SortedDictionary<int, MultivectorFourierCurveTerm<T>>();
    }


    public RGaMultivectorFourierCurve<T> AddTermMultivectors(int key, RGaMultivector<T> cosMultivector, RGaMultivector<T> sinMultivector)
    {
        if (!_termsDictionary.TryGetValue(key, out var term))
            throw new InvalidOperationException();

        term.AddMultivectors(cosMultivector, sinMultivector);

        return this;
    }

    public RGaMultivectorFourierCurve<T> AddTermMultivectors(int key, Scalar<T> frequency, RGaMultivector<T> cosMultivector, RGaMultivector<T> sinMultivector)
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

    public RGaMultivectorFourierCurve<T> SetTerm(int key, Scalar<T> frequency, RGaMultivector<T> cosMultivector, RGaMultivector<T> sinMultivector)
    {
        var term = new MultivectorFourierCurveTerm<T>(cosMultivector, sinMultivector, frequency);

        if (_termsDictionary.ContainsKey(key))
            _termsDictionary[key] = term;
        else
            _termsDictionary.Add(key, term);

        return this;
    }
        
    public RGaMultivectorFourierCurve<T> SetTerm(int key, T frequency, RGaMultivector<T> cosMultivector, RGaMultivector<T> sinMultivector)
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

    public RGaMultivectorFourierCurve<T> GetDerivative(int degree = 1)
    {
        var termsDictionary = new SortedDictionary<int, MultivectorFourierCurveTerm<T>>();

        foreach (var (key, term) in _termsDictionary)
            termsDictionary.Add(
                key, 
                term.GetDerivative(degree)
            );

        return new RGaMultivectorFourierCurve<T>(GeometricProcessor, termsDictionary);
    }

        
    public RGaMultivector<T> GetValue(Scalar<T> parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            (RGaMultivector<T>) GeometricProcessor.MultivectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public RGaMultivector<T> GetValue(T parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            (RGaMultivector<T>) GeometricProcessor.MultivectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public Pair<RGaVector<T>> GetLocalFrame2D(T parameterValue)
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

        return new Pair<RGaVector<T>>(e1, e2);
    }

    public Triplet<RGaVector<T>> GetLocalFrame3D(T parameterValue)
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

        return new Triplet<RGaVector<T>>(e1, e2, e3);
    }
        
    public Quad<RGaVector<T>> GetLocalFrame4D(T parameterValue)
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

        return new Quad<RGaVector<T>>(e1, e2, e3, e4);
    }
}