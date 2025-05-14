using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Fourier;

public class XGaVectorFourierCurve<T>
{
    public static XGaVectorFourierCurve<T> Create(XGaProcessor<T> processor)
    {
        return new XGaVectorFourierCurve<T>(processor);
    }


    private readonly SortedDictionary<int, VectorFourierCurveTerm<T>> _termsDictionary;

    public XGaProcessor<T> Processor { get; }


    private XGaVectorFourierCurve(XGaProcessor<T> processor, SortedDictionary<int, VectorFourierCurveTerm<T>> termsDictionary)
    {
        Processor = processor;
        _termsDictionary = termsDictionary;
    }

    private XGaVectorFourierCurve(XGaProcessor<T> processor)
    {
        Processor = processor;
        _termsDictionary = new SortedDictionary<int, VectorFourierCurveTerm<T>>();
    }


    public XGaVectorFourierCurve<T> AddTermVectors(int key, XGaVector<T> cosVector, XGaVector<T> sinVector)
    {
        if (!_termsDictionary.TryGetValue(key, out var term))
            throw new InvalidOperationException();

        term.AddVectors(cosVector, sinVector);

        return this;
    }

    public XGaVectorFourierCurve<T> AddTermVectors(int key, Scalar<T> frequency, XGaVector<T> cosVector, XGaVector<T> sinVector)
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

    public XGaVectorFourierCurve<T> SetTerm(int key, Scalar<T> frequency, XGaVector<T> cosVector, XGaVector<T> sinVector)
    {
        var term = new VectorFourierCurveTerm<T>(cosVector, sinVector, frequency);

        if (_termsDictionary.ContainsKey(key))
            _termsDictionary[key] = term;
        else
            _termsDictionary.Add(key, term);

        return this;
    }
        
    public XGaVectorFourierCurve<T> SetTerm(int key, T frequency, XGaVector<T> cosVector, XGaVector<T> sinVector)
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

    public XGaVectorFourierCurve<T> GetDerivative(int degree = 1)
    {
        var termsDictionary = new SortedDictionary<int, VectorFourierCurveTerm<T>>();

        foreach (var (key, term) in _termsDictionary)
            termsDictionary.Add(
                key, 
                term.GetTermDerivative(degree)
            );

        return new XGaVectorFourierCurve<T>(Processor, termsDictionary);
    }

        
    public XGaVector<T> GetValue(Scalar<T> parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            Processor.VectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public XGaVector<T> GetValue(T parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            Processor.VectorZero, 
            (current, term) => 
                current + term.GetValue(parameterValue)
        );
    }
        
    public Pair<XGaVector<T>> GetLocalFrame2D(T parameterValue)
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

        return new Pair<XGaVector<T>>(e1, e2);
    }

    public Triplet<XGaVector<T>> GetLocalFrame3D(T parameterValue)
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

        return new Triplet<XGaVector<T>>(e1, e2, e3);
    }
        
    public Quad<XGaVector<T>> GetLocalFrame4D(T parameterValue)
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

        return new Quad<XGaVector<T>>(e1, e2, e3, e4);
    }
}