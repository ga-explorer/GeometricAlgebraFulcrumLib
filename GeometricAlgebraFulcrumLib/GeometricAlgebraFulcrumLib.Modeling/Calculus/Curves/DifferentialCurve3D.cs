using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Quaternions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Curves;

public class DifferentialCurve3D :
    IParametricC2Curve3D,
    ITriplet<DifferentialFunction>,
    IReadOnlyList<DifferentialFunction>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurve3D Create(Triplet<DifferentialFunction> components)
    {
        return new DifferentialCurve3D(components);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurve3D Create(Triplet<DifferentialFunction> components, DifferentialFunction tangentNorm)
    {
        return new DifferentialCurve3D(components, tangentNorm);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurve3D Create(DifferentialFunction component1, DifferentialFunction component2, DifferentialFunction component3)
    {
        return new DifferentialCurve3D(
            new Triplet<DifferentialFunction>(
                component1,
                component2, 
                component3
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialCurve3D Create(DifferentialFunction component1, DifferentialFunction component2, DifferentialFunction component3, DifferentialFunction tangentNorm)
    {
        return new DifferentialCurve3D(
            new Triplet<DifferentialFunction>(
                component1,
                component2, 
                component3
            ),
            tangentNorm
        );
    }


    public int Count 
        => 3;
        
    public Float64ScalarRange ParameterRange 
        => Float64ScalarRange.Infinite;

    public DifferentialFunction Item1 
        => ComponentFunctions.Item1;

    public DifferentialFunction Item2 
        => ComponentFunctions.Item2;

    public DifferentialFunction Item3 
        => ComponentFunctions.Item3;

    public Triplet<DifferentialFunction> ComponentFunctions { get; }

    public DifferentialFunction TangentNormFunction { get; }
    
    public Triplet<Triplet<DifferentialFunction>> ComponentDerivatives { get; }

    public Triplet<DifferentialFunction> ComponentsDerivative1
        => new Triplet<DifferentialFunction>(
            ComponentDerivatives.Item1.Item1,
            ComponentDerivatives.Item2.Item1,
            ComponentDerivatives.Item3.Item1
        );

    public Triplet<DifferentialFunction> ComponentsDerivative2
        => new Triplet<DifferentialFunction>(
            ComponentDerivatives.Item1.Item2,
            ComponentDerivatives.Item2.Item2,
            ComponentDerivatives.Item3.Item2
        );

    public Triplet<DifferentialFunction> ComponentsDerivative3
        => new Triplet<DifferentialFunction>(
            ComponentDerivatives.Item1.Item3,
            ComponentDerivatives.Item2.Item3,
            ComponentDerivatives.Item3.Item3
        );

    public DifferentialFunction XFunction 
        => ComponentFunctions.Item1;

    public DifferentialFunction YFunction 
        => ComponentFunctions.Item2;

    public DifferentialFunction ZFunction 
        => ComponentFunctions.Item3;
    
    public DifferentialFunction XDerivative1 
        => ComponentDerivatives.Item1.Item1;

    public  DifferentialFunction XDerivative2 
        => ComponentDerivatives.Item1.Item2;

    public  DifferentialFunction XDerivative3
        => ComponentDerivatives.Item1.Item3;

    public  DifferentialFunction YDerivative1 
        => ComponentDerivatives.Item2.Item1;

    public  DifferentialFunction YDerivative2 
        => ComponentDerivatives.Item2.Item2;

    public  DifferentialFunction YDerivative3 
        => ComponentDerivatives.Item2.Item3;

    public  DifferentialFunction ZDerivative1 
        => ComponentDerivatives.Item3.Item1;

    public  DifferentialFunction ZDerivative2 
        => ComponentDerivatives.Item3.Item2;

    public  DifferentialFunction ZDerivative3 
        => ComponentDerivatives.Item3.Item3;

    
    public DifferentialFunction this[int index] 
        => ComponentFunctions.GetItem(index);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DifferentialCurve3D(ITriplet<DifferentialFunction> components)
    {
        ComponentFunctions = components.ToTriplet();
        
        ComponentDerivatives = ComponentFunctions.MapItems(
            component => component.GetDerivatives3()
        );

        TangentNormFunction =
            DfPlus.Create(
                ComponentDerivatives.Item1.Item1.Square(),
                ComponentDerivatives.Item2.Item1.Square(), 
                ComponentDerivatives.Item3.Item1.Square()
            ).SquareRoot();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DifferentialCurve3D(ITriplet<DifferentialFunction> components, DifferentialFunction tangentNorm)
    {
        ComponentFunctions = components.ToTriplet();
        
        ComponentDerivatives = ComponentFunctions.MapItems(
            component => component.GetDerivatives3()
        );

        TangentNormFunction = tangentNorm;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual bool IsValid()
    {
        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double t)
    {
        return LinFloat64Vector3D.Create(ComponentFunctions.Item1.GetValue(t),
            ComponentFunctions.Item2.GetValue(t),
            ComponentFunctions.Item3.GetValue(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Point(double t)
    {
        var vDt1 = ComponentsDerivative1;

        var xDt = vDt1.Item1.GetValue(t);
        var yDt = vDt1.Item2.GetValue(t);
        var zDt = vDt1.Item3.GetValue(t);

        return LinFloat64Vector3D.Create(xDt, yDt, zDt);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative2Point(double t)
    {
        var vDt2 = ComponentsDerivative2;

        var xDt = vDt2.Item1.GetValue(t);
        var yDt = vDt2.Item2.GetValue(t);
        var zDt = vDt2.Item3.GetValue(t);

        return LinFloat64Vector3D.Create(xDt, yDt, zDt);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative3Point(double t)
    {
        var vDt3 = ComponentsDerivative3;

        var xDt = vDt3.Item1.GetValue(t);
        var yDt = vDt3.Item2.GetValue(t);
        var zDt = vDt3.Item3.GetValue(t);

        return LinFloat64Vector3D.Create(xDt, yDt, zDt);
    }
        
    public LinFloat64Vector3D GetArcLengthDerivative1Point(double t)
    {
        var vDt1 = GetDerivative1Point(t);

        var sDt1 = GetTangentNormValue(t);

        var vDs1 = vDt1 / sDt1;

        return vDs1;
    }

    public LinFloat64Vector3D GetArcLengthDerivative2Point(double t)
    {
        var vDt1 = GetDerivative1Point(t);
        var vDt2 = GetDerivative2Point(t);

        var sDt1 = GetTangentNormValue(t);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();

        return vDs2 - vDs2.ProjectOnUnitVector(vDs1);
    }
        
    public LinFloat64Vector3D GetArcLengthDerivative3Point(double t)
    {
        var vDt1 = GetDerivative1Point(t);
        var vDt2 = GetDerivative2Point(t);
        var vDt3 = GetDerivative3Point(t);

        var sDt1 = GetTangentNormValue(t);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorESp(vDt2) + vDt1.VectorESp(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);
            
        return vDs3 - vDs3.ProjectOnUnitVector(vDs2) - vDs3.ProjectOnUnitVector(vDs1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTangentNormValue(double t)
    {
        return TangentNormFunction.GetValue(t);
    }

    public Triplet<IParametricCurve3D> GetComponentCurves()
    {
        var curve1 = ComputedParametricCurve3D.Create(
            t => LinFloat64Vector3D.Create(
                ComponentFunctions.Item1.GetValue(t),
                0,
                0
            ),
            t => LinFloat64Vector3D.Create(
                ComponentsDerivative1.Item1.GetValue(t),
                0,
                0
            )
        );

        var curve2 = ComputedParametricCurve3D.Create(
            t => LinFloat64Vector3D.Create(
                0,
                ComponentFunctions.Item2.GetValue(t),
                0
            ),
            t => LinFloat64Vector3D.Create(
                0,
                ComponentsDerivative1.Item2.GetValue(t),
                0
            )
        );

        var curve3 = ComputedParametricCurve3D.Create(
            t => LinFloat64Vector3D.Create(
                0, 
                0,
                ComponentFunctions.Item3.GetValue(t)
            ),
            t => LinFloat64Vector3D.Create(
                0,
                0,
                ComponentsDerivative1.Item3.GetValue(t)
            )
        );

        return new Triplet<IParametricCurve3D>(curve1, curve2, curve3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DifferentialFunction GetArcLengthVariableDerivative1()
    {
        return TangentNormFunction;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DifferentialFunction GetArcLengthVariableDerivative2()
    {
        return TangentNormFunction.GetDerivative1();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DifferentialFunction GetArcLengthVariableDerivative3()
    {
        return TangentNormFunction.GetDerivative2();
    }
        
    public DifferentialCurveFrame3D GetFrenetFrame2Vectors(double parameterValue, bool orthogonalFrame = false)
    {
        var origin = GetPoint(parameterValue);

        var vDt1 = GetDerivative1Point(parameterValue);
        var vDt2 = GetDerivative2Point(parameterValue);
            
        var sDt1 = GetTangentNormValue(parameterValue);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
            
        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
            
        if (vDs2.IsNearZero())
            return DifferentialCurveFrame3D.Create(
                parameterValue,
                origin,
                vDs1,
                vDs1.GetUnitNormalPair()
            );

        if (!orthogonalFrame)
            return DifferentialCurveFrame3D.Create(
                parameterValue, 
                origin, 
                vDs1, 
                vDs2, 
                vDs1.VectorCross(vDs2)
            );
        
        var u1 = vDs1;
        var u2 = vDs2 - vDs2.ProjectOnVector(u1);
            
        return DifferentialCurveFrame3D.Create(
            parameterValue, 
            origin, 
            u1, 
            u2, 
            u1.VectorCross(u2)
        );
    }
        
    public DifferentialCurveFrame3D GetFrenetFrame(double parameterValue, bool orthogonalFrame = false)
    {
        var origin = GetPoint(parameterValue);

        var vDt1 = GetDerivative1Point(parameterValue);
        var vDt2 = GetDerivative2Point(parameterValue);
        var vDt3 = GetDerivative3Point(parameterValue);

        var sDt1 = GetTangentNormValue(parameterValue);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorESp(vDt2) + vDt1.VectorESp(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);
            
        if (vDs2.IsNearZero())
            return DifferentialCurveFrame3D.Create(
                parameterValue,
                origin,
                vDs1,
                vDs1.GetUnitNormalPair()
            );

        if (!orthogonalFrame)
            return DifferentialCurveFrame3D.Create(
                parameterValue, 
                origin, 
                vDs1, 
                vDs2, 
                vDs3
            );
        
        var u1 = vDs1;
        var u2 = vDs2 - vDs2.ProjectOnVector(u1);
        var u3 = vDs3 - vDs3.ProjectOnVector(u2) - vDs3.ProjectOnVector(u1);

        //var vDsMatrix = Matrix.Build.DenseOfArray(
        //    new [,]
        //    {
        //        { vDs1.X, vDs2.X, vDs3.X }, 
        //        { vDs1.Y, vDs2.Y, vDs3.Y }, 
        //        { vDs1.Z, vDs2.Z, vDs3.Z }
        //    }
        //);

        //var gramSchmidt = vDsMatrix.GramSchmidt();
        //var eMatrix = gramSchmidt.Q;
        ////var qDet = eMatrix.Determinant();

        //var e1 = new Tuple3D(eMatrix[0, 0], eMatrix[1, 0], eMatrix[2, 0]);
        //var e2 = new Tuple3D(eMatrix[0, 1], eMatrix[1, 1], eMatrix[2, 1]);
        //var e3 = e1.VectorUnitCross(e2);

        //if (eMatrix[2, 2].IsNearZero())
        //    throw new InvalidOperationException();

        ////if (t == 1.94)
        ////{
        ////    Console.WriteLine(vDsMatrix);
        ////    Console.WriteLine(gramSchmidt.Q);
        ////    Console.WriteLine(qDet);
        ////    Console.WriteLine(gramSchmidt.R);
        ////    Console.WriteLine();
        ////}

        return DifferentialCurveFrame3D.Create(
            parameterValue, 
            origin, 
            u1, 
            u2, 
            u3
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedParametricQuaternion GetFrenetFrameRotationQuaternionsCurve()
    {
        return ComputedParametricQuaternion.Create(time =>
            {
                var frame = GetFrenetFrame2Vectors(time, false);

                return frame
                    .Direction1
                    .GetOrthogonalizingRotation(frame.Direction2)
                    .GetQuaternion();
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return ParametricCurveLocalFrame3D.CreateFromAffineFrame(
            parameterValue,
            GetAffineFrame(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Triplet<LinFloat64Vector3D> GetComponentVectors(double t)
    {
        var x = LinFloat64Vector3D.Create(XFunction.GetValue(t), 0, 0);
        var y = LinFloat64Vector3D.Create(0, YFunction.GetValue(t), 0);
        var z = LinFloat64Vector3D.Create(0, 0, ZFunction.GetValue(t));

        return new Triplet<LinFloat64Vector3D>(x, y, z);
    }
        
    public LinFloat64AffineFrame3D GetArcLengthFrame(double t)
    {
        var origin = GetPoint(t);

        var vDt1 = GetDerivative1Point(t);
        var vDt2 = GetDerivative2Point(t);
        var vDt3 = GetDerivative3Point(t);

        var sDt1 = GetTangentNormValue(t);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorESp(vDt2) + vDt1.VectorESp(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        //return AffineFrame3D.Create(
        //    origin, 
        //    vDs1.ToUnitVector(), 
        //    vDs2.ToUnitVector(), 
        //    vDs3.ToUnitVector()
        //);

        var u1 = vDs1;
        var u2 = vDs2 - vDs2.ProjectOnUnitVector(vDs1);
        var u3 = vDs3 - vDs3.ProjectOnUnitVector(vDs2) - vDs3.ProjectOnUnitVector(vDs1);

        //var vDsMatrix = Matrix.Build.DenseOfArray(
        //    new [,]
        //    {
        //        { vDs1.X, vDs2.X, vDs3.X }, 
        //        { vDs1.Y, vDs2.Y, vDs3.Y }, 
        //        { vDs1.Z, vDs2.Z, vDs3.Z }
        //    }
        //);

        //var gramSchmidt = vDsMatrix.GramSchmidt();
        //var eMatrix = gramSchmidt.Q;
        ////var qDet = eMatrix.Determinant();

        //var e1 = new Tuple3D(eMatrix[0, 0], eMatrix[1, 0], eMatrix[2, 0]);
        //var e2 = new Tuple3D(eMatrix[0, 1], eMatrix[1, 1], eMatrix[2, 1]);
        //var e3 = e1.VectorUnitCross(e2);

        //if (eMatrix[2, 2].IsNearZero())
        //    throw new InvalidOperationException();

        ////if (t == 1.94)
        ////{
        ////    Console.WriteLine(vDsMatrix);
        ////    Console.WriteLine(gramSchmidt.Q);
        ////    Console.WriteLine(qDet);
        ////    Console.WriteLine(gramSchmidt.R);
        ////    Console.WriteLine();
        ////}

        return LinFloat64AffineFrame3D.Create(origin, u1, u2, u3);
    }
    
    public LinFloat64AffineFrame3D GetAffineFrame(double t)
    {
        var origin = GetPoint(t);

        var vDt1 = GetDerivative1Point(t);
        var vDt2 = GetDerivative2Point(t);
            
        var sDt1 = GetTangentNormValue(t);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
            
        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
            
        var e1 = vDs1;
        var e2 = (vDs2 - vDs2.ProjectOnUnitVector(vDs1)).ToUnitLinVector3D();
        var e3 = e1.VectorUnitCross(e2);
            
        return LinFloat64AffineFrame3D.Create(origin, e1, e2, e3);
    }
    
    public Pair<double> GetCurvatures(double t)
    {
        var vDt1 = GetDerivative1Point(t);
        var vDt2 = GetDerivative2Point(t);
        var vDt3 = GetDerivative3Point(t);

        var sDt1 = GetTangentNormValue(t);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorESp(vDt2) + vDt1.VectorESp(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        var e1 = vDs1;

        var u2 = vDs2 - vDs2.ProjectOnUnitVector(e1);
        var e2 = u2.ToUnitLinVector3D();

        var u3 = vDs3 - vDs3.ProjectOnUnitVector(e2) - vDs3.ProjectOnUnitVector(e1);
        //var e3 = u3.ToUnitVector();

        var kappa1 = (sDt1 * u2.VectorENorm()).ScalarValue.NaNInfinityToZero();
        var kappa2 = (sDt1 * u3.VectorENorm() / u2.VectorENorm()).NaNInfinityToZero();

        return new Pair<double>(kappa1, kappa2);
    }
    
    public Triplet<LinFloat64Bivector3D> GetDarbouxBlades(double t)
    {
        var vDt1 = GetDerivative1Point(t);
        var vDt2 = GetDerivative2Point(t);
        var vDt3 = GetDerivative3Point(t);

        var sDt1 = GetTangentNormValue(t);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorESp(vDt2) + vDt1.VectorESp(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        var e1 = vDs1.ToLinVector3D();

        var u2 = vDs2 - vDs2.ProjectOnUnitVector(e1);
        var e2 = u2.ToUnitLinVector3D().ToLinVector3D();

        var u3 = vDs3 - vDs3.ProjectOnUnitVector(e2) - vDs3.ProjectOnUnitVector(e1);
        var e3 = u3.ToUnitLinVector3D().ToLinVector3D();

        var kappa1 = (sDt1 * u2.VectorENorm()).NaNInfinityToZero();
        var kappa2 = (sDt1 * u3.VectorENorm() / u2.VectorENorm()).NaNInfinityToZero();

        var omega1 = kappa1 * e1.Op(e2);
        var omega3 = kappa2 * e2.Op(e3);
        var omega2 = omega1 + omega3;

        return new Triplet<LinFloat64Bivector3D>(omega1, omega2, omega3);
    }

    public LinFloat64Bivector3D GetDarbouxBivector(double t)
    {
        var vDt1 = GetDerivative1Point(t);
        var vDt2 = GetDerivative2Point(t);
        var vDt3 = GetDerivative3Point(t);

        var sDt1 = GetTangentNormValue(t);
        var sDt2 = vDt1.VectorESp(vDt2) / sDt1;
        var sDt3 = (vDt2.VectorESp(vDt2) + vDt1.VectorESp(vDt3) - sDt2.Square()) / sDt1;

        var vDs1 = vDt1 / sDt1;
        var vDs2 = (sDt1 * vDt2 - sDt2 * vDt1) / sDt1.Cube();
        var vDs3 = (sDt1.Square() * vDt3 - 3 * sDt1 * sDt2 * vDt2 + (3 * sDt2.Square() - sDt1 * sDt3) * vDt1) / sDt1.Power(5);

        var e1 = vDs1.ToLinVector3D();

        var u2 = vDs2 - vDs2.ProjectOnUnitVector(e1);
        var e2 = u2.ToUnitLinVector3D().ToLinVector3D();

        var u3 = vDs3 - vDs3.ProjectOnUnitVector(e2) - vDs3.ProjectOnUnitVector(e1);
        var e3 = u3.ToUnitLinVector3D().ToLinVector3D();

        var kappa1 = (sDt1 * u2.VectorENorm()).NaNInfinityToZero();
        var kappa2 = (sDt1 * u3.VectorENorm() / u2.VectorENorm()).NaNInfinityToZero();

        return kappa1 * e1.Op(e2) + kappa2 * e2.Op(e3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFrequency(double t)
    {
        return GetDarbouxBivector(t).Norm();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFrequencyHz(double t)
    {
        return GetFrequency(t) / (2d * Math.PI);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<DifferentialFunction> GetComponentsDerivativeN(int order)
    {
        return ComponentFunctions.MapItems(f => 
            f.GetDerivativeN(order)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<DifferentialFunction> GetComponentsArcLengthDerivative1()
    {
        return ComponentFunctions.MapItems(f => 
            f.GetDerivative1() / TangentNormFunction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<DifferentialFunction> GetComponentsArcLengthDerivative2()
    {
        return GetComponentsArcLengthDerivative1().MapItems(f => 
            f.GetDerivative1() / TangentNormFunction
        );
    } 

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<DifferentialFunction> GetComponentsArcLengthDerivative3()
    {
        return GetComponentsArcLengthDerivative2().MapItems(f => 
            f.GetDerivative1() / TangentNormFunction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<Triplet<DifferentialFunction>> GetComponentsArcLengthDerivatives3()
    {
        var ds0 = ComponentFunctions;

        var ds1 = ds0.MapItems(f =>
            f.GetDerivative1() / TangentNormFunction
        );

        var ds2 = ds1.MapItems(f =>
            f.GetDerivative1() / TangentNormFunction
        );

        var ds3 = ds2.MapItems(f =>
            f.GetDerivative1() / TangentNormFunction
        );

        return new Triplet<Triplet<DifferentialFunction>>(ds1, ds2, ds3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<DifferentialFunction> GetEnumerator()
    {
        return ComponentFunctions.GetItems().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}