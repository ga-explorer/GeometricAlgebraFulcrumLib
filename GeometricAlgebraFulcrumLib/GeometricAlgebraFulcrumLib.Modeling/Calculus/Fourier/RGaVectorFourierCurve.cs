using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Fourier;

public class RGaVectorFourierCurve<T>
{
    public static RGaVectorFourierCurve<T> Create(RGaProcessor<T> processor)
    {
        return new RGaVectorFourierCurve<T>(processor);
    }


    private readonly SortedDictionary<int, VectorFourierCurveTerm<T>> _termsDictionary;

    public RGaProcessor<T> Processor { get; }


    private RGaVectorFourierCurve(RGaProcessor<T> processor, SortedDictionary<int, VectorFourierCurveTerm<T>> termsDictionary)
    {
        Processor = processor;
        _termsDictionary = termsDictionary;
    }

    private RGaVectorFourierCurve(RGaProcessor<T> processor)
    {
        Processor = processor;
        _termsDictionary = new SortedDictionary<int, VectorFourierCurveTerm<T>>();
    }


    public RGaVectorFourierCurve<T> AddTermVectors(int key, RGaVector<T> cosVector, RGaVector<T> sinVector)
    {
        if (!_termsDictionary.TryGetValue(key, out var term))
            throw new InvalidOperationException();

        term.AddVectors(cosVector, sinVector);

        return this;
    }

    public RGaVectorFourierCurve<T> AddTermVectors(int key, Scalar<T> frequency, RGaVector<T> cosVector, RGaVector<T> sinVector)
    {
        if (_termsDictionary.TryGetValue(key, out var term))
        {
            if (!(frequency - term.Frequency).IsNearZero())
                throw new InvalidOperationException();

            term.AddVectors(cosVector, sinVector);
        }
        else
        {
            term = new VectorFourierCurveTerm<T>(cosVector, sinVector, frequency);

            _termsDictionary.Add(key, term);
        }

        return this;
    }

    public RGaVectorFourierCurve<T> SetTerm(int key, Scalar<T> frequency, RGaVector<T> cosVector, RGaVector<T> sinVector)
    {
        var term = new VectorFourierCurveTerm<T>(cosVector, sinVector, frequency);

        if (_termsDictionary.ContainsKey(key))
            _termsDictionary[key] = term;
        else
            _termsDictionary.Add(key, term);

        return this;
    }
        
    public RGaVectorFourierCurve<T> SetTerm(int key, T frequency, RGaVector<T> cosVector, RGaVector<T> sinVector)
    {
        var term = new VectorFourierCurveTerm<T>(
            cosVector, 
            sinVector, 
            frequency.ScalarFromValue(cosVector.ScalarProcessor)
        );

        if (_termsDictionary.ContainsKey(key))
            _termsDictionary[key] = term;
        else
            _termsDictionary.Add(key, term);

        return this;
    }

    public RGaVectorFourierCurve<T> GetDerivative(int degree = 1)
    {
        var termsDictionary = new SortedDictionary<int, VectorFourierCurveTerm<T>>();

        foreach (var (key, term) in _termsDictionary)
            termsDictionary.Add(
                key, 
                term.GetTermDerivative(degree)
            );

        return new RGaVectorFourierCurve<T>(Processor, termsDictionary);
    }

        
    public RGaVector<T> GetValue(Scalar<T> parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            Processor.VectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public RGaVector<T> GetValue(T parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            Processor.VectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public Pair<RGaVector<T>> GetLocalFrame2D(T parameterValue)
    {
        var vDt1 = GetDerivative(1).GetValue(parameterValue);
        var vDt2 = GetDerivative(2).GetValue(parameterValue);

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
        var vDt1 = GetDerivative(1).GetValue(parameterValue);
        var vDt2 = GetDerivative(2).GetValue(parameterValue);
        var vDt3 = GetDerivative(3).GetValue(parameterValue);

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
        var vDt1 = GetDerivative(1).GetValue(parameterValue);
        var vDt2 = GetDerivative(2).GetValue(parameterValue);
        var vDt3 = GetDerivative(3).GetValue(parameterValue);
        var vDt4 = GetDerivative(4).GetValue(parameterValue);

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