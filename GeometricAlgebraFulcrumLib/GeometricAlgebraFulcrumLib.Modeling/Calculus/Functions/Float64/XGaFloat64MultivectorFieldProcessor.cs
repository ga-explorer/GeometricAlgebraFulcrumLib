using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Spaces;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

public class XGaFloat64MultivectorFieldProcessor :
    IXGaFloat64MultivectorFieldProcessor
{
    public double DerivativeEpsilon { get; set; } = 1e-12;

    public XGaFloat64Space Space { get; }

    public Pair<XGaFloat64BasisVectorFrame> XGaBasisVectorFrame { get; }


    public XGaFloat64MultivectorFieldProcessor(XGaFloat64Space space)
    {
        Space = space;

        var frame1 = space.Processor.CreateBasisVectorFrame(space.VSpaceDimensions);
        var frame2 = frame1.GetReciprocalVectorFrame();

        XGaBasisVectorFrame = new Pair<XGaFloat64BasisVectorFrame>(
            frame1,
            frame2
        );
    }


    public Func<XGaFloat64Vector, XGaFloat64Multivector> Negative(Func<XGaFloat64Vector, XGaFloat64Multivector> f1)
    {
        return v => -f1(v);
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Vector f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Bivector f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64KVector f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Multivector f2)
    {
        return v => f1(v) + f2;
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(double f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64Vector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64Bivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64KVector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(XGaFloat64Multivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        return v => f1 + f2(v);
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Add(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        return v => f1(v) + f2(v);
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Vector f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Bivector f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64KVector f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, XGaFloat64Multivector f2)
    {
        return v => f1(v) + f2;
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(double f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64Vector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64Bivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64KVector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(XGaFloat64Multivector f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        return v => f1 + f2(v);
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Subtract(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        return v => f1(v) + f2(v);
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Times(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Times(double f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Divide(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, double f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Scalar> Sp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1,
        Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Op(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Gp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Lcp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
    {
        throw new NotImplementedException();
    }

    public Func<XGaFloat64Vector, XGaFloat64Multivector> Rcp(Func<XGaFloat64Vector, XGaFloat64Multivector> f1, Func<XGaFloat64Vector, XGaFloat64Multivector> f2)
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
    public XGaFloat64Multivector GetVectorDerivativeValue(Func<XGaFloat64Vector, XGaFloat64Multivector> multivectorFunction, XGaFloat64Vector v, XGaFloat64Vector w)
    {
        return (multivectorFunction(v + DerivativeEpsilon * w) - multivectorFunction(v)) / DerivativeEpsilon;
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Geometric_calculus
    /// </summary>
    /// <param name="multivectorFunction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public XGaFloat64Multivector GetGeometricDerivativeValue(Func<XGaFloat64Vector, XGaFloat64Multivector> multivectorFunction, XGaFloat64Vector v)
    {
        var n = Space.VSpaceDimensions;
        var mv = (XGaFloat64Multivector)Space.Processor.MultivectorZero;

        for (var i = 0; i < n; i++)
        {
            var basisVector = XGaBasisVectorFrame.Item1[i];
            var vd = GetVectorDerivativeValue(multivectorFunction, v, basisVector);

            var basisVectorReciprocal = XGaBasisVectorFrame.Item2[i];
            mv += basisVectorReciprocal.Gp(vd);
        }

        return mv;
    }
}