using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.FourierAlgebra
{
    public class MultivectorFourierCurve<T>
    {
        public static MultivectorFourierCurve<T> Create(IGeometricAlgebraProcessor<T> processor)
        {
            return new MultivectorFourierCurve<T>(processor);
        }


        private readonly SortedDictionary<int, MultivectorFourierCurveTerm<T>> _termsDictionary;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }


        private MultivectorFourierCurve([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] SortedDictionary<int, MultivectorFourierCurveTerm<T>> termsDictionary)
        {
            GeometricProcessor = processor;
            _termsDictionary = termsDictionary;
        }

        private MultivectorFourierCurve([NotNull] IGeometricAlgebraProcessor<T> processor)
        {
            GeometricProcessor = processor;
            _termsDictionary = new SortedDictionary<int, MultivectorFourierCurveTerm<T>>();
        }


        public MultivectorFourierCurve<T> AddTermMultivectors(int key, GaMultivector<T> cosMultivector, GaMultivector<T> sinMultivector)
        {
            if (!_termsDictionary.TryGetValue(key, out var term))
                throw new InvalidOperationException();

            term.AddMultivectors(cosMultivector, sinMultivector);

            return this;
        }

        public MultivectorFourierCurve<T> AddTermMultivectors(int key, Scalar<T> frequency, GaMultivector<T> cosMultivector, GaMultivector<T> sinMultivector)
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

        public MultivectorFourierCurve<T> SetTerm(int key, Scalar<T> frequency, GaMultivector<T> cosMultivector, GaMultivector<T> sinMultivector)
        {
            var term = new MultivectorFourierCurveTerm<T>(cosMultivector, sinMultivector, frequency);

            if (_termsDictionary.ContainsKey(key))
                _termsDictionary[key] = term;
            else
                _termsDictionary.Add(key, term);

            return this;
        }
        
        public MultivectorFourierCurve<T> SetTerm(int key, T frequency, GaMultivector<T> cosMultivector, GaMultivector<T> sinMultivector)
        {
            var term = new MultivectorFourierCurveTerm<T>(
                cosMultivector, 
                sinMultivector, 
                frequency.CreateScalar(cosMultivector.GeometricProcessor)
            );

            if (_termsDictionary.ContainsKey(key))
                _termsDictionary[key] = term;
            else
                _termsDictionary.Add(key, term);

            return this;
        }

        public MultivectorFourierCurve<T> GetDerivative(int degree = 1)
        {
            var termsDictionary = new SortedDictionary<int, MultivectorFourierCurveTerm<T>>();

            foreach (var (key, term) in _termsDictionary)
                termsDictionary.Add(
                    key, 
                    term.GetDerivative(degree)
                );

            return new MultivectorFourierCurve<T>(GeometricProcessor, termsDictionary);
        }

        
        public GaMultivector<T> GetValue(Scalar<T> parameterValue)
        {
            return _termsDictionary.Values.Aggregate(
                GeometricProcessor.CreateMultivectorSparseZero(), 
                (current, term) => 
                    current + term.GetValue(parameterValue)
                );
        }
        
        public GaMultivector<T> GetValue(T parameterValue)
        {
            return _termsDictionary.Values.Aggregate(
                GeometricProcessor.CreateMultivectorSparseZero(), 
                (current, term) => 
                    current + term.GetValue(parameterValue)
            );
        }
        
        public Pair<GaVector<T>> GetLocalFrame2D(T parameterValue)
        {
            var vDt1 = GetDerivative(1).GetValue(parameterValue).GetVectorPart();
            var vDt2 = GetDerivative(2).GetValue(parameterValue).GetVectorPart();

            // Apply GS process
            var u1 = vDt1;
            var e1 = u1.DivideByNorm();
            //var e1d = (e1.DifferentiateScalars("t") / vDt1Norm);
            
            var u2 = vDt2 - vDt2.ProjectOn(u1.GetSubspace());
            var e2 = u2.DivideByNorm();
            //var e2d = (e2.DifferentiateScalars("t") / vDt1Norm);

            return new Pair<GaVector<T>>(e1, e2);
        }

        public Triplet<GaVector<T>> GetLocalFrame3D(T parameterValue)
        {
            var vDt1 = GetDerivative(1).GetValue(parameterValue).GetVectorPart();
            var vDt2 = GetDerivative(2).GetValue(parameterValue).GetVectorPart();
            var vDt3 = GetDerivative(3).GetValue(parameterValue).GetVectorPart();

            // Apply GS process
            var u1 = vDt1;
            var e1 = u1.DivideByNorm();
            //var e1d = (e1.DifferentiateScalars("t") / vDt1Norm);
            
            var u2 = vDt2 - vDt2.ProjectOn(u1.GetSubspace());
            var e2 = u2.DivideByNorm();
            //var e2d = (e2.DifferentiateScalars("t") / vDt1Norm);
                
            var u3 = vDt3 - vDt3.ProjectOn(u1.GetSubspace()) - vDt3.ProjectOn(u2.GetSubspace());
            var e3 = u3.DivideByNorm();
            //var e3d = (e3.DifferentiateScalars("t") / vDt1Norm);

            return new Triplet<GaVector<T>>(e1, e2, e3);
        }
        
        public Quad<GaVector<T>> GetLocalFrame4D(T parameterValue)
        {
            var vDt1 = GetDerivative(1).GetValue(parameterValue).GetVectorPart();
            var vDt2 = GetDerivative(2).GetValue(parameterValue).GetVectorPart();
            var vDt3 = GetDerivative(3).GetValue(parameterValue).GetVectorPart();
            var vDt4 = GetDerivative(4).GetValue(parameterValue).GetVectorPart();

            // Apply GS process
            var u1 = vDt1;
            var e1 = u1.DivideByNorm();
            //var e1d = (e1.DifferentiateScalars("t") / vDt1Norm);
            
            var u2 = vDt2 - vDt2.ProjectOn(u1.GetSubspace());
            var e2 = u2.DivideByNorm();
            //var e2d = (e2.DifferentiateScalars("t") / vDt1Norm);
                
            var u3 = vDt3 - vDt3.ProjectOn(u1.GetSubspace()) - vDt3.ProjectOn(u2.GetSubspace());
            var e3 = u3.DivideByNorm();
            //var e3d = (e3.DifferentiateScalars("t") / vDt1Norm);
                
            var u4 = vDt4 - vDt4.ProjectOn(u1.GetSubspace()) - vDt4.ProjectOn(u2.GetSubspace()) - vDt4.ProjectOn(u3.GetSubspace());
            var e4 = u4.DivideByNorm();
            //var e4d = (e4.DifferentiateScalars("t") / vDt1Norm);

            return new Quad<GaVector<T>>(e1, e2, e3, e4);
        }
    }
}
