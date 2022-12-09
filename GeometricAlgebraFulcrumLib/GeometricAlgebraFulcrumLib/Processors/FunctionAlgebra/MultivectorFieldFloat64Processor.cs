using System;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.FunctionAlgebra;

public class MultivectorFieldFloat64Processor :
    IMultivectorFieldProcessor<double>
{
    public double DerivativeEpsilon { get; set; } = 1e-12;

    public IGeometricAlgebraProcessor<double> GeometricProcessor { get; }

    public Pair<BasisVectorFrame<double>> BasisVectorFrame { get; }


    public MultivectorFieldFloat64Processor(IGeometricAlgebraProcessor<double> geometricProcessor)
    {
        GeometricProcessor = geometricProcessor;
        BasisVectorFrame = geometricProcessor.GetBasisVectorFrame();
    }


    public Func<GaVector<double>, GaMultivector<double>> Negative(Func<GaVector<double>, GaMultivector<double>> f1)
    {
        return v => -f1(v);
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(Func<GaVector<double>, GaMultivector<double>> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(Func<GaVector<double>, GaMultivector<double>> f1, GaVector<double> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(Func<GaVector<double>, GaMultivector<double>> f1, GaBivector<double> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(Func<GaVector<double>, GaMultivector<double>> f1, GaKVector<double> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(Func<GaVector<double>, GaMultivector<double>> f1, GaMultivector<double> f2)
    {
        return v => f1(v) + f2;
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(double f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(GaVector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(GaBivector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(GaKVector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(GaMultivector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        return v => f1 + f2(v);
    }

    public Func<GaVector<double>, GaMultivector<double>> Add(Func<GaVector<double>, GaMultivector<double>> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        return v => f1(v) + f2(v);
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(Func<GaVector<double>, GaMultivector<double>> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(Func<GaVector<double>, GaMultivector<double>> f1, GaVector<double> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(Func<GaVector<double>, GaMultivector<double>> f1, GaBivector<double> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(Func<GaVector<double>, GaMultivector<double>> f1, GaKVector<double> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(Func<GaVector<double>, GaMultivector<double>> f1, GaMultivector<double> f2)
    {
        return v => f1(v) + f2;
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(double f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(GaVector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(GaBivector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(GaKVector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(GaMultivector<double> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        return v => f1 + f2(v);
    }

    public Func<GaVector<double>, GaMultivector<double>> Subtract(Func<GaVector<double>, GaMultivector<double>> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        return v => f1(v) + f2(v);
    }

    public Func<GaVector<double>, GaMultivector<double>> Times(Func<GaVector<double>, GaMultivector<double>> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Times(double f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }
    
    public Func<GaVector<double>, GaMultivector<double>> Divide(Func<GaVector<double>, GaMultivector<double>> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, Scalar<double>> Sp(Func<GaVector<double>, GaMultivector<double>> f1,
        Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Op(Func<GaVector<double>, GaMultivector<double>> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Gp(Func<GaVector<double>, GaMultivector<double>> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Lcp(Func<GaVector<double>, GaMultivector<double>> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    public Func<GaVector<double>, GaMultivector<double>> Rcp(Func<GaVector<double>, GaMultivector<double>> f1, Func<GaVector<double>, GaMultivector<double>> f2)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Geometric_calculus
    /// </summary>
    /// <param name="multivectorFunction"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public GaMultivector<double> GetVectorDerivativeValue(Func<GaVector<double>, GaMultivector<double>> multivectorFunction, GaVector<double> v, GaVector<double> w)
    {
        return (multivectorFunction(v + DerivativeEpsilon * w) - multivectorFunction(v)) / DerivativeEpsilon;
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Geometric_calculus
    /// </summary>
    /// <param name="multivectorFunction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public GaMultivector<double> GetGeometricDerivativeValue(Func<GaVector<double>, GaMultivector<double>> multivectorFunction, GaVector<double> v)
    {
        var n = GeometricProcessor.VSpaceDimension;
        var mv = GeometricProcessor.CreateMultivectorSparseZero();

        for (var i = 0; i < n; i++)
        {
            var basisVector = BasisVectorFrame.Item1[i];
            var vd = GetVectorDerivativeValue(multivectorFunction, v, basisVector);
            
            var basisVectorReciprocal = BasisVectorFrame.Item2[i];
            mv += basisVectorReciprocal.Gp(vd);
        }

        return mv;
    }
}