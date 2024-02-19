using System.Text;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Operations;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Samples.Geometry;

public sealed record SnelliusPothenotData2D
{
    /*
    def GetAngles(A, B, C, P):

       ua = [A[0] - P[0], A[1] - P[1]]
       ub = [B[0] - P[0], B[1] - P[1]]
       uc = [C[0] - P[0], C[1] - P[1]]

       theta_ua = atan2(ua[1], ua[0])
       theta_ub = atan2(ub[1], ub[0])
       theta_uc = atan2(uc[1], uc[0])


       print("Angles ----> A: %f, B: %f, C: %f\n"%(theta_ua*180/pi, theta_ub*180/pi, theta_uc*180/pi))

       alpha =  theta_ua - theta_ub
       beta =  theta_ub - theta_uc

       # If P is aligned with A and B or B and C, a new assignment of the points is necessary.
       if beta == 0:
           alpha =  theta_ub - theta_ua
           beta =  theta_ua - theta_uc
           print("The angle beta = 0 ---> The position of A and B will be interchanged.")
           A[:], B[:] = B[:], A[:]

       if alpha == 0:
           alpha =  theta_ua - theta_uc
           beta =  theta_uc - theta_ub

           print("The angle alpha = 0 ---> The position of C and B will be interchanged.")
           C[:], B[:] = B[:], C[:]

       # if we are working in 2 dimensions, gamma is not used.
       gamma = None
       if len(A) == 3:
           gamma = atan2(A[2] - P[2], sqrt(ua[0]**2 + ua[1]**2))


       return alpha, beta, gamma
    */
    public static SnelliusPothenotData2D Create(Float64Vector2D a, Float64Vector2D b, Float64Vector2D c, Float64Vector2D p)
    {
        var ua = a - p;
        var ub = b - p;
        var uc = c - p;

        var thetaUa = ua.GetPolarAngle();
        var thetaUb = ub.GetPolarAngle();
        var thetaUc = uc.GetPolarAngle();

        //Console.WriteLine(
        //    $"Angles ----> A: {thetaUa}, B: {thetaUb}, C: {thetaUc}"
        //);

        var alpha = thetaUa - thetaUb;
        var beta = thetaUb - thetaUc;

        // If P is aligned with A and B or B and C, a new assignment of
        // the points is necessary.
        if (beta.IsZeroOrFullRotation())
        {
            alpha = thetaUb - thetaUa;
            beta = thetaUa - thetaUc;

            //Console.WriteLine(
            //    "The angle beta = 0 ---> The position of A and B will be interchanged."
            //);

            (a, b) = (b, a);
        }

        if (alpha.IsZeroOrFullRotation())
        {
            alpha = thetaUa - thetaUc;
            beta = thetaUc - thetaUb;

            //Console.WriteLine(
            //    "The angle alpha = 0 ---> The position of C and B will be interchanged."
            //);

            (c, b) = (b, c);
        }

        return new SnelliusPothenotData2D(
            a, b, c, alpha, beta
        );
    }


    public Float64Vector2D A { get; }

    public Float64Vector2D B { get; }

    public Float64Vector2D C { get; }

    public Float64PlanarAngle Alpha { get; }

    public Float64PlanarAngle Beta { get; }


    private SnelliusPothenotData2D(Float64Vector2D pointA, Float64Vector2D pointB, Float64Vector2D pointC, Float64PlanarAngle alpha, Float64PlanarAngle beta)
    {
        A = pointA;
        B = pointB;
        C = pointC;
        Alpha = alpha;
        Beta = beta;
    }


    /*
    def VGA_Method_2D(A,B,C,alpha,beta):

       v1 = (A[0]-B[0])*e1 + (A[1]-B[1])*e2
       v2 = (C[0]-B[0])*e1 + (C[1]-B[1])*e2
           
       d1 = v1 + (v1/tan(alpha))*e12
       d2 = v2 - (v2/tan(beta))*e12
       d = d2-d1
       
       try:
           p = (d1^d)|d.inv()
           P = [B[0] + p.as_array()[1], B[1] + p.as_array()[2]]
           
       except Exception as err:
           print("Prohibited circle ---> ",err)
           
       return P
    */
    /// <summary>
    /// Prototype implementation, used for validation and debug, slow to use.
    /// </summary>
    /// <returns></returns>
    public Float64Vector2D SolveUsingVGa1()
    {
        var v1 = A - B;
        var v2 = C - B;

        var d1 = v1 + (v1 / Alpha.Tan()).Gp(Float64Bivector2D.E12);
        var d2 = v2 - (v2 / Beta.Tan()).Gp(Float64Bivector2D.E12);
        var d = d2 - d1;

        try
        {
            return B + d1.Op(d).Rcp(d.Inverse());
        }
        catch (Exception err)
        {
            Console.WriteLine($"Prohibited circle ---> {err}");

            return Float64Vector2D.Zero;
        }
    }

    /*
    def CGA_Cassini_Method(A,B,C,alpha,beta):
       
       a_v = A[0]*e1 + A[1]*e2 
       b_v = B[0]*e1 + B[1]*e2
       c_v = C[0]*e1 + C[1]*e2

       a = up(a_v)
       b = up(b_v)
       c = up(c_v)

       LAB = a ^ b ^ einf
       LBC = c ^ b ^ einf

       midAB = (a - b).dual().normal()
       midBC = (b - c).dual().normal()
       
       Ra = e**(-((alpha - pi/2)/2)*e12)
       Rb = e**(-((pi/2 - beta)/2)*e12)
       
       Ta = 1 + einf*(a_v)/2
       Tc = 1 + einf*(c_v)/2
       
       LAO = (Ta*Ra*~Ta)*LAB*~(Ta*Ra*~Ta)
       LCO = (Tc*Rb*~Tc)*LBC*~(Tc*Rb*~Tc)
       
       O1 = up(down(eo << (LAO.dual() ^ midAB.dual()).dual().normal()))
       O2 = up(down(eo << (LCO.dual() ^ midBC.dual()).dual().normal()))
       
       R1 = (sqrt(-2*(O1 << b)))
       R2 = (sqrt(-2*(O2 << b)))

       C1 = (O1 - 1/2*(R1**2)*einf).normal()
       C2 = (O2 - 1/2*(R2**2)*einf).normal()

       P = (C1 ^ C2).dual()
       
       P1 = (sqrt(P << P) + P)/(einf << P)
       P2 = (-sqrt(P << P) + P)/(einf << P)

       
       if down(P1) == down(b):
           return down(P2)

       else:
           return down(P1)
    */
    /// <summary>
    /// Prototype implementation, used for validation and debug, slow to use.
    /// </summary>
    /// <returns></returns>
    public Float64Vector2D SolveUsingCGaCassini1()
    {
        var cga = RGaConformalSpace4D.Instance;

        var a = cga.EncodeIpnsRoundPoint(A);
        var b = cga.EncodeIpnsRoundPoint(B);
        var c = cga.EncodeIpnsRoundPoint(C);

        //var basisSpecs = RGaGeometrySpaceBasisSpecs.CreateCGaOrthogonal(4);

        var lab = cga.EncodeOpnsFlatLineFromPoints(A, B);
        var lcb = cga.EncodeOpnsFlatLineFromPoints(C, B);

        var midAb = (b - a).CGaUnDual().DivideByNorm();
        var midCb = (b - c).CGaUnDual().DivideByNorm();

        var ra = cga.EncodeEGaRotation(
            Float64PlanarAngle.Angle90 - Alpha
        );

        var rc = cga.EncodeEGaRotation(
            Beta - Float64PlanarAngle.Angle90
        );

        //var Ta = cga.EncodeCGaTranslation(A);
        //var Tc = cga.EncodeCGaTranslation(C);

        var da = ra.TranslateBy(A); //Ta.MapVersor(Ra);
        var dc = rc.TranslateBy(C); //Tc.MapVersor(Rb);

        var lao = da.MapBlade(lab);
        var lco = dc.MapBlade(lcb);

        var o1 = cga.Eo.Lcp(
            lao.CGaDual().Op(midAb.CGaDual()).CGaUnDual().DivideByNorm()
        ).DecodeEGaVector2D();

        var o2 = cga.Eo.Lcp(
            lco.CGaDual().Op(midCb.CGaDual()).CGaUnDual().DivideByNorm()
        ).DecodeEGaVector2D();

        var r1 = o1.GetDistanceToPoint(B);
        var r2 = o2.GetDistanceToPoint(B);

        //Console.WriteLine(r1);
        //Console.WriteLine(r2);
        //Console.WriteLine(o1);
        //Console.WriteLine(o2);

        var c1 = cga.EncodeIpnsRealRoundCircle(r1, o1);
        var c2 = cga.EncodeIpnsRealRoundCircle(r2, o2);

        var (p1, p2) =
            c1.IntersectIpns(c2).DecodeIpnsPointPairEGaPointsAsVector2D();

        return B.GetDistanceToPoint(p1).IsNearZero() ? p2 : p1;
    }
    
    /*
    def CGA_Collins_Method(A,B,C,alpha,beta):
       
       a_v = A[0]*e1 + A[1]*e2 
       b_v = B[0]*e1 + B[1]*e2
       c_v = C[0]*e1 + C[1]*e2

       a = up(a_v)
       b = up(b_v)
       c = up(c_v)
       
       LAC = a ^ c ^ einf
       
       Rb = e**(-(beta/2)*e12)
       Ra = e**(-(-alpha/2)*e12)
       
       Ta = 1 + einf*(a_v)/2
       Tc = 1 + einf*(c_v)/2
       
       L1 = (Ta*Rb*~Ta)*LAC*~(Ta*Rb*~Ta)
       L2 = (Tc*Ra*~Tc)*LAC*~(Tc*Ra*~Tc)
       
       # L1 and L2 are parallel
       if (einf<<L1) == (einf<<L2):
       
           V = einf << L1
           LEB = b ^ V
           F = (LAC.dual() ^ LEB.dual()).dual()
           P = up(down(eo<<F))
           
           return P
       
       # L1 and L2 intersect
       E = up(down(eo << (L1.dual() ^ L2.dual()).dual()))
       C = a ^ c ^ E
       LEB = E ^ b ^ einf

       P = (C.dual() ^ LEB.dual()).dual().normal()
       
       P1 = (sqrt(P << P) + P)/(einf << P)
       P2 = (-sqrt(P << P) + P)/(einf << P)
       
       if down(P1) == down(E):
           return down(P2)
       else:
           return down(P1) 
    */
    /// <summary>
    /// Prototype implementation, used for validation and debug, slow to use.
    /// </summary>
    /// <returns></returns>
    private Float64Vector2D SolveUsingCGaCollinsParallel1()
    {
        var cga = RGaConformalSpace4D.Instance;

        var b = cga.EncodeIpnsRoundPoint(B);

        var lac = cga.EncodeOpnsFlatLineFromPoints(A, C);
        
        var rb = cga.EncodeEGaRotation(-Beta);
        var d1 = rb.TranslateBy(A);
        var l1 = d1.MapBlade(lac);

        var v = cga.Ei.Lcp(l1);
        var leb = b.Op(v);
        var f = lac.CGaDual().Op(leb.CGaDual()).CGaUnDual();
        
        return cga.Eo.Lcp(f).DecodeEGaVector2D();
    }

    /// <summary>
    /// Prototype implementation, used for validation and debug, slow to use.
    /// </summary>
    /// <returns></returns>
    public Float64Vector2D SolveUsingCGaCollins1()
    {
        if ((Alpha.Radians.Value + Beta.Radians.Value - Math.PI).IsNearZero())
            return SolveUsingCGaCollinsParallel1();

        var cga = RGaConformalSpace4D.Instance;

        var a = cga.EncodeIpnsRoundPoint(A);
        var b = cga.EncodeIpnsRoundPoint(B);
        var c = cga.EncodeIpnsRoundPoint(C);

        var lac = cga.EncodeOpnsFlatLineFromPoints(A, C);

        var ra = cga.EncodeEGaRotation(Alpha);
        var rb = cga.EncodeEGaRotation(-Beta);

        var d1 = rb.TranslateBy(A);
        var d2 = ra.TranslateBy(C);

        var l1 = d1.MapBlade(lac);
        var l2 = d2.MapBlade(lac);

        var pvE = -cga.Eo.Lcp(
            l1.CGaDual().Op(l2.CGaDual()).CGaUnDual().DivideByNorm()
        ).DecodeEGaVector2D();

        var e = cga.EncodeIpnsRoundPoint(pvE);

        var c1 = a.Op(c).Op(e);
        var leb = e.Op(b).Op(cga.Ei);

        var p =
            c1.CGaDual().Op(leb.CGaDual()).DivideByNorm();

        var (p1, p2) =
            p.DecodeIpnsPointPairEGaPointsAsVector2D();

        return pvE.GetDistanceToPoint(p1).IsNearZero() ? p2 : p1;
    }

    
    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public Float64Vector2D SolveUsingVGa()
    {
        //Begin GA-FuL MetaContext Code Generation, 2024-02-18T19:42:05.0554634+02:00

        var temp0 = -B.X.Value;
        var temp1 = A.X.Value + temp0;
        var temp2 = -B.Y.Value;
        var temp3 = A.Y.Value + temp2;
        var temp4 = Math.Tan(Alpha.Radians.Value);
        temp4 = 1 / temp4;
        var temp5 = temp3 * temp4;
        temp5 = -temp5;
        temp5 = temp1 + temp5;
        var temp6 = -temp5;
        temp0 = C.X.Value + temp0;
        temp2 = C.Y.Value + temp2;
        var temp7 = Math.Tan(Beta.Radians.Value);
        temp7 = 1 / temp7;
        var temp8 = temp2 * temp7;
        temp8 = temp0 + temp8;
        temp6 += temp8;
        temp8 = temp6 * temp6;
        temp1 *= temp4;
        temp1 = temp3 + temp1;
        temp1 = -temp1;
        temp0 *= temp7;
        temp0 = -temp0;
        temp0 = temp2 + temp0;
        temp0 = temp1 + temp0;
        temp2 = temp0 * temp0;
        var dSig = temp8 + temp2;

        if (dSig.IsNearZero())
        {
            Console.WriteLine("Prohibited circle ---> Division By Zero");

            return Float64Vector2D.Zero;
        }

        temp2 = 1 / dSig;
        temp3 = temp6 * temp2;
        temp3 = -temp3;
        temp4 = temp5 * temp0;
        temp1 = temp6 * temp1;
        temp1 = temp4 + temp1;
        temp3 *= temp1;
        var pY = B.Y.Value + temp3;

        temp0 *= temp2;
        temp0 = temp1 * temp0;
        var pX = B.X.Value + temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-02-18T19:42:05.1448437+02:00

        return Float64Vector2D.Create(pX, pY);
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public Float64Vector2D SolveUsingCGaCassini()
    {
        //Begin GA-FuL MetaContext Code Generation, 2024-02-18T20:01:23.8302569+02:00

        var temp0 = -Alpha.Radians.Value;
        temp0 = 1.5707963267948966 + temp0;
        temp0 = 0.5 * temp0;
        var temp1 = Math.Cos(temp0);
        var temp2 = 0.5 * A.X.Value;
        var temp3 = temp1 * temp2;
        temp0 = Math.Sin(temp0);
        var temp4 = -temp0;
        var temp5 = 0.5 * A.Y.Value;
        var temp6 = temp4 * temp5;
        temp3 += temp6;
        temp6 = -temp3;
        var temp7 = -temp5;
        var temp8 = temp6 * temp7;
        var temp9 = temp2 * temp0;
        temp5 = temp1 * temp5;
        temp5 = temp9 + temp5;
        temp2 = -temp2;
        temp9 = temp5 * temp2;
        var temp10 = -temp5;
        var temp11 = temp2 * temp10;
        var temp12 = temp3 * temp7;
        var temp13 = temp11 + temp12;
        temp13 = temp9 + temp13;
        temp13 = temp8 + temp13;
        temp13 = temp0 + temp13;
        var temp14 = -temp13;
        var temp15 = A.X.Value * B.Y.Value;
        var temp16 = -A.Y.Value;
        var temp17 = B.X.Value * temp16;
        temp15 += temp17;
        temp17 = temp14 * temp15;
        temp4 *= temp2;
        temp4 = temp5 + temp4;
        var temp18 = temp1 * temp7;
        temp4 += temp18;
        temp18 = -temp4;
        var temp19 = A.X.Value * A.X.Value;
        var temp20 = A.Y.Value * A.Y.Value;
        temp19 += temp20;
        temp19 = 0.5 * temp19;
        temp20 = 0.5 + temp19;
        var temp21 = B.Y.Value * temp20;
        var temp22 = B.X.Value * B.X.Value;
        var temp23 = B.Y.Value * B.Y.Value;
        temp22 += temp23;
        temp22 = 0.5 * temp22;
        temp23 = 0.5 + temp22;
        var temp24 = -temp23;
        var temp25 = A.Y.Value * temp24;
        temp21 += temp25;
        temp21 = -temp21;
        temp19 = -0.5 + temp19;
        temp25 = B.Y.Value * temp19;
        temp22 = -0.5 + temp22;
        var temp26 = -temp22;
        temp26 = A.Y.Value * temp26;
        temp25 += temp26;
        temp21 += temp25;
        temp25 = temp18 * temp21;
        temp17 += temp25;
        temp0 *= temp7;
        temp0 = temp3 + temp0;
        temp25 = temp1 * temp2;
        temp0 += temp25;
        temp25 = -temp0;
        temp26 = B.X.Value * temp20;
        var temp27 = -A.X.Value;
        var temp28 = temp23 * temp27;
        temp26 += temp28;
        temp26 = -temp26;
        temp28 = B.X.Value * temp19;
        var temp29 = temp22 * temp27;
        temp28 += temp29;
        temp26 += temp28;
        temp28 = temp25 * temp26;
        temp17 += temp28;
        temp8 += temp9;
        temp8 = temp11 + temp8;
        temp8 = temp12 + temp8;
        temp9 = -temp8;
        temp11 = temp15 * temp9;
        temp11 = temp17 + temp11;
        temp12 = -temp11;
        temp17 = -temp0;
        temp28 = temp12 * temp17;
        temp0 = temp15 * temp0;
        temp14 *= temp26;
        temp29 = temp15 * temp25;
        temp14 += temp29;
        temp14 = temp0 + temp14;
        temp6 *= temp2;
        temp10 = temp7 * temp10;
        temp5 = temp7 * temp5;
        temp2 = temp3 * temp2;
        temp3 = temp5 + temp2;
        temp3 = temp10 + temp3;
        temp3 = temp6 + temp3;
        temp1 += temp3;
        temp3 = temp21 * temp1;
        temp3 = temp14 + temp3;
        temp7 = -temp3;
        temp14 = -temp13;
        var temp30 = temp7 * temp14;
        temp18 = temp15 * temp18;
        temp13 *= temp21;
        var temp31 = temp15 * temp4;
        temp13 += temp31;
        temp13 = temp18 + temp13;
        var temp32 = temp26 * temp1;
        temp13 += temp32;
        temp32 = temp1 * temp13;
        temp30 += temp32;
        temp6 += temp10;
        temp5 += temp6;
        temp2 += temp5;
        temp5 = temp15 * temp2;
        temp6 = temp4 * temp26;
        temp5 += temp6;
        temp6 = temp21 * temp25;
        temp5 += temp6;
        temp6 = temp15 * temp1;
        temp5 += temp6;
        temp6 = -temp5;
        temp4 = -temp4;
        temp10 = temp6 * temp4;
        temp15 = temp30 + temp10;
        temp25 = temp5 * temp4;
        temp15 += temp25;
        temp30 = temp11 * temp17;
        temp15 += temp30;
        temp15 = temp28 + temp15;
        temp32 = temp26 * temp2;
        temp18 += temp32;
        temp18 = temp31 + temp18;
        temp31 = temp21 * temp8;
        temp18 += temp31;
        temp31 = -temp2;
        temp32 = temp18 * temp31;
        temp15 += temp32;
        temp2 = temp21 * temp2;
        temp0 += temp2;
        temp0 = temp29 + temp0;
        temp2 = temp26 * temp9;
        temp0 += temp2;
        temp2 = temp9 * temp0;
        temp2 = temp15 + temp2;
        temp15 = -temp19;
        temp15 += temp22;
        temp15 = -temp15;
        temp19 = temp15 * temp15;
        temp19 = -temp19;
        temp20 = -temp20;
        temp20 += temp23;
        temp20 = -temp20;
        temp21 = temp20 * temp20;
        temp19 += temp21;
        temp21 = B.X.Value + temp27;
        temp26 = temp21 * temp21;
        temp26 = -temp26;
        temp19 += temp26;
        temp16 = B.Y.Value + temp16;
        temp16 = -temp16;
        temp26 = temp16 * temp16;
        temp26 = -temp26;
        temp19 += temp26;
        temp19 = Math.Abs(temp19);
        temp19 = Math.Sqrt(temp19);
        temp19 = 1 / temp19;
        temp15 *= temp19;
        temp15 = -temp15;
        temp26 = temp15 * temp2;
        temp27 = temp17 * temp3;
        temp29 = -temp13;
        temp32 = temp29 * temp4;
        temp27 += temp32;
        temp32 = temp6 * temp31;
        var temp33 = temp27 + temp32;
        var temp34 = temp1 * temp5;
        temp33 += temp34;
        var temp35 = temp11 * temp14;
        temp33 += temp35;
        var temp36 = temp9 * temp11;
        temp33 += temp36;
        var temp37 = -temp18;
        var temp38 = temp37 * temp4;
        temp33 += temp38;
        var temp39 = temp17 * temp0;
        temp33 += temp39;
        temp33 = -temp33;
        temp16 *= temp19;
        temp16 = -temp16;
        var temp40 = temp33 * temp16;
        temp26 += temp40;
        temp20 *= temp19;
        temp20 = -temp20;
        temp40 = temp20 * temp2;
        temp27 += temp34;
        temp27 = temp32 + temp27;
        temp27 = temp36 + temp27;
        temp27 = temp35 + temp27;
        temp27 = temp38 + temp27;
        temp27 = temp39 + temp27;
        temp27 = -temp27;
        temp32 = temp16 * temp27;
        temp32 = temp40 + temp32;
        temp34 = temp32 * temp32;
        temp35 = temp26 * temp26;
        temp35 = -temp35;
        temp34 += temp35;
        temp19 = temp21 * temp19;
        temp2 = temp19 * temp2;
        temp21 = temp12 * temp4;
        temp35 = temp1 * temp3;
        temp36 = temp14 * temp13;
        temp35 += temp36;
        temp36 = temp17 * temp5;
        temp35 += temp36;
        temp38 = temp17 * temp6;
        temp35 += temp38;
        temp39 = temp11 * temp4;
        temp35 += temp39;
        temp35 = temp21 + temp35;
        temp40 = temp8 * temp18;
        temp35 += temp40;
        temp40 = temp31 * temp0;
        temp35 += temp40;
        temp40 = temp16 * temp35;
        temp2 += temp40;
        temp2 = -temp2;
        temp2 *= temp2;
        temp2 = -temp2;
        temp2 = temp34 + temp2;
        temp34 = temp15 * temp27;
        temp40 = -temp33;
        temp40 = temp20 * temp40;
        temp34 += temp40;
        temp34 *= temp34;
        temp2 += temp34;
        temp27 *= temp19;
        temp34 = -temp35;
        temp35 = temp20 * temp34;
        temp27 += temp35;
        temp27 = -temp27;
        temp35 = temp27 * temp27;
        temp2 += temp35;
        temp33 *= temp19;
        temp34 = temp15 * temp34;
        temp33 += temp34;
        temp33 = -temp33;
        temp34 = temp33 * temp33;
        temp34 = -temp34;
        temp2 += temp34;
        temp34 = temp3 * temp31;
        temp35 = temp8 * temp13;
        temp34 += temp35;
        temp34 = temp38 + temp34;
        temp34 = temp36 + temp34;
        temp21 += temp34;
        temp21 = temp39 + temp21;
        temp34 = temp14 * temp18;
        temp21 += temp34;
        temp34 = temp1 * temp0;
        temp21 += temp34;
        temp16 *= temp21;
        temp7 *= temp4;
        temp21 = temp29 * temp17;
        temp7 += temp21;
        temp6 = temp14 * temp6;
        temp21 = temp7 + temp6;
        temp5 = temp8 * temp5;
        temp8 = temp21 + temp5;
        temp12 *= temp31;
        temp8 += temp12;
        temp11 *= temp1;
        temp8 += temp11;
        temp17 = temp37 * temp17;
        temp8 += temp17;
        temp0 = -temp0;
        temp4 = temp0 * temp4;
        temp8 += temp4;
        temp8 = temp15 * temp8;
        temp8 = temp16 + temp8;
        temp5 = temp7 + temp5;
        temp5 = temp6 + temp5;
        temp5 = temp11 + temp5;
        temp5 = temp12 + temp5;
        temp5 = temp17 + temp5;
        temp4 += temp5;
        temp4 = -temp4;
        temp4 = temp20 * temp4;
        temp4 = temp8 + temp4;
        temp3 = temp9 * temp3;
        temp5 = temp13 * temp31;
        temp3 += temp5;
        temp3 += temp25;
        temp3 += temp10;
        temp3 += temp28;
        temp3 += temp30;
        temp1 *= temp18;
        temp1 = temp3 + temp1;
        temp0 *= temp14;
        temp0 = temp1 + temp0;
        temp0 = temp19 * temp0;
        temp0 = temp4 + temp0;
        temp0 = -temp0;
        temp0 *= temp0;
        temp0 = temp2 + temp0;
        temp0 = Math.Abs(temp0);
        temp0 = Math.Sqrt(temp0);
        temp0 = 1 / temp0;
        temp1 = temp26 * temp0;
        temp1 = -0.5 * temp1;
        temp2 = temp32 * temp0;
        temp2 = -0.5 * temp2;
        temp1 += temp2;
        temp2 = temp1 * temp1;
        temp3 = temp33 * temp0;
        temp3 = -0.5 * temp3;
        temp0 = temp27 * temp0;
        temp0 = -0.5 * temp0;
        temp0 = temp3 + temp0;
        temp3 = temp0 * temp0;
        temp4 = temp2 + temp3;
        temp4 = 0.5 * temp4;
        temp5 = 0.5 + temp4;
        temp6 = temp5 * temp24;
        temp4 = -0.5 + temp4;
        temp7 = temp4 * temp22;
        temp6 += temp7;
        temp7 = B.X.Value * temp1;
        temp6 += temp7;
        temp7 = B.Y.Value * temp0;
        temp6 += temp7;
        temp6 = -2 * temp6;
        temp6 = Math.Sqrt(temp6);
        temp6 *= temp6;
        temp6 = 0.5 * temp6;
        temp6 = -temp6;
        temp5 += temp6;
        temp7 = temp5 * temp5;
        temp7 = -temp7;
        temp4 += temp6;
        temp6 = temp4 * temp4;
        temp6 = temp7 + temp6;
        temp2 += temp6;
        temp2 = temp3 + temp2;
        temp2 = Math.Abs(temp2);
        temp2 = Math.Sqrt(temp2);
        temp2 = 1 / temp2;
        temp3 = temp5 * temp2;
        temp5 = -1.5707963267948966 + Beta.Radians.Value;
        temp5 = 0.5 * temp5;
        temp6 = Math.Cos(temp5);
        temp7 = 0.5 * C.X.Value;
        temp8 = temp6 * temp7;
        temp5 = Math.Sin(temp5);
        temp9 = -temp5;
        temp10 = 0.5 * C.Y.Value;
        temp11 = temp9 * temp10;
        temp8 += temp11;
        temp11 = -temp8;
        temp12 = -temp10;
        temp13 = temp11 * temp12;
        temp14 = temp7 * temp5;
        temp10 = temp6 * temp10;
        temp10 = temp14 + temp10;
        temp7 = -temp7;
        temp14 = temp10 * temp7;
        temp15 = -temp10;
        temp16 = temp7 * temp15;
        temp17 = temp8 * temp12;
        temp18 = temp16 + temp17;
        temp18 = temp14 + temp18;
        temp18 = temp13 + temp18;
        temp18 = temp5 + temp18;
        temp19 = -temp18;
        temp20 = B.Y.Value * C.X.Value;
        temp21 = -C.Y.Value;
        temp25 = B.X.Value * temp21;
        temp20 += temp25;
        temp25 = temp19 * temp20;
        temp9 *= temp7;
        temp9 = temp10 + temp9;
        temp26 = temp6 * temp12;
        temp9 += temp26;
        temp26 = -temp9;
        temp27 = C.X.Value * C.X.Value;
        temp28 = C.Y.Value * C.Y.Value;
        temp27 += temp28;
        temp27 = 0.5 * temp27;
        temp28 = 0.5 + temp27;
        temp29 = B.Y.Value * temp28;
        temp30 = temp21 * temp23;
        temp29 += temp30;
        temp29 = -temp29;
        temp27 = -0.5 + temp27;
        temp30 = B.Y.Value * temp27;
        temp31 = temp21 * temp22;
        temp30 += temp31;
        temp29 += temp30;
        temp30 = temp26 * temp29;
        temp25 += temp30;
        temp5 *= temp12;
        temp5 = temp8 + temp5;
        temp30 = temp6 * temp7;
        temp5 += temp30;
        temp30 = -temp5;
        temp31 = B.X.Value * temp28;
        temp32 = -C.X.Value;
        temp33 = temp32 * temp23;
        temp31 += temp33;
        temp31 = -temp31;
        temp33 = B.X.Value * temp27;
        temp34 = temp32 * temp22;
        temp33 += temp34;
        temp31 += temp33;
        temp33 = temp30 * temp31;
        temp25 += temp33;
        temp13 += temp14;
        temp13 = temp16 + temp13;
        temp13 = temp17 + temp13;
        temp14 = -temp13;
        temp16 = temp20 * temp14;
        temp16 = temp25 + temp16;
        temp17 = -temp16;
        temp25 = -temp5;
        temp33 = temp17 * temp25;
        temp5 = temp20 * temp5;
        temp19 *= temp31;
        temp34 = temp20 * temp30;
        temp19 += temp34;
        temp19 = temp5 + temp19;
        temp11 *= temp7;
        temp15 = temp12 * temp15;
        temp10 = temp12 * temp10;
        temp7 = temp8 * temp7;
        temp8 = temp10 + temp7;
        temp8 = temp15 + temp8;
        temp8 = temp11 + temp8;
        temp6 += temp8;
        temp8 = temp29 * temp6;
        temp8 = temp19 + temp8;
        temp12 = -temp8;
        temp19 = -temp18;
        temp35 = temp12 * temp19;
        temp26 = temp20 * temp26;
        temp18 *= temp29;
        temp36 = temp20 * temp9;
        temp18 += temp36;
        temp18 = temp26 + temp18;
        temp37 = temp31 * temp6;
        temp18 += temp37;
        temp37 = temp6 * temp18;
        temp35 += temp37;
        temp11 += temp15;
        temp10 += temp11;
        temp7 += temp10;
        temp10 = temp20 * temp7;
        temp11 = temp9 * temp31;
        temp10 += temp11;
        temp11 = temp29 * temp30;
        temp10 += temp11;
        temp11 = temp20 * temp6;
        temp10 += temp11;
        temp11 = -temp10;
        temp9 = -temp9;
        temp15 = temp11 * temp9;
        temp20 = temp35 + temp15;
        temp30 = temp10 * temp9;
        temp20 += temp30;
        temp35 = temp16 * temp25;
        temp20 += temp35;
        temp20 = temp33 + temp20;
        temp37 = temp31 * temp7;
        temp26 += temp37;
        temp26 = temp36 + temp26;
        temp36 = temp29 * temp13;
        temp26 += temp36;
        temp36 = -temp7;
        temp37 = temp26 * temp36;
        temp20 += temp37;
        temp7 = temp29 * temp7;
        temp5 += temp7;
        temp5 = temp34 + temp5;
        temp7 = temp31 * temp14;
        temp5 += temp7;
        temp7 = temp14 * temp5;
        temp7 = temp20 + temp7;
        temp20 = -temp27;
        temp20 += temp22;
        temp20 = -temp20;
        temp27 = -temp28;
        temp23 = temp27 + temp23;
        temp23 = -temp23;
        temp27 = temp23 * temp23;
        temp28 = temp20 * temp20;
        temp28 = -temp28;
        temp27 += temp28;
        temp28 = B.X.Value + temp32;
        temp29 = temp28 * temp28;
        temp29 = -temp29;
        temp27 += temp29;
        temp21 = B.Y.Value + temp21;
        temp21 = -temp21;
        temp29 = temp21 * temp21;
        temp29 = -temp29;
        temp27 += temp29;
        temp27 = Math.Abs(temp27);
        temp27 = Math.Sqrt(temp27);
        temp27 = 1 / temp27;
        temp20 *= temp27;
        temp20 = -temp20;
        temp29 = temp7 * temp20;
        temp31 = temp25 * temp8;
        temp32 = -temp18;
        temp34 = temp9 * temp32;
        temp31 += temp34;
        temp34 = temp11 * temp36;
        temp37 = temp31 + temp34;
        temp38 = temp6 * temp10;
        temp37 += temp38;
        temp39 = temp16 * temp19;
        temp37 += temp39;
        temp40 = temp14 * temp16;
        temp37 += temp40;
        var temp41 = -temp26;
        var temp42 = temp9 * temp41;
        temp37 += temp42;
        var temp43 = temp25 * temp5;
        temp37 += temp43;
        temp37 = -temp37;
        temp21 *= temp27;
        temp21 = -temp21;
        var temp44 = temp37 * temp21;
        temp29 += temp44;
        temp23 *= temp27;
        temp23 = -temp23;
        temp44 = temp7 * temp23;
        temp31 += temp38;
        temp31 = temp34 + temp31;
        temp31 = temp40 + temp31;
        temp31 = temp39 + temp31;
        temp31 = temp42 + temp31;
        temp31 = temp43 + temp31;
        temp31 = -temp31;
        temp34 = temp21 * temp31;
        temp34 = temp44 + temp34;
        temp38 = temp34 * temp34;
        temp39 = temp29 * temp29;
        temp39 = -temp39;
        temp38 += temp39;
        temp27 = temp28 * temp27;
        temp7 *= temp27;
        temp28 = temp17 * temp9;
        temp39 = temp6 * temp8;
        temp40 = temp19 * temp18;
        temp39 += temp40;
        temp40 = temp25 * temp10;
        temp39 += temp40;
        temp42 = temp25 * temp11;
        temp39 += temp42;
        temp43 = temp16 * temp9;
        temp39 += temp43;
        temp39 = temp28 + temp39;
        temp44 = temp13 * temp26;
        temp39 += temp44;
        temp44 = temp36 * temp5;
        temp39 += temp44;
        temp44 = temp21 * temp39;
        temp7 += temp44;
        temp7 = -temp7;
        temp7 *= temp7;
        temp7 = -temp7;
        temp7 = temp38 + temp7;
        temp38 = temp20 * temp31;
        temp44 = -temp37;
        temp44 = temp23 * temp44;
        temp38 += temp44;
        temp38 *= temp38;
        temp7 += temp38;
        temp31 *= temp27;
        temp38 = -temp39;
        temp39 = temp23 * temp38;
        temp31 += temp39;
        temp31 = -temp31;
        temp39 = temp31 * temp31;
        temp7 += temp39;
        temp37 *= temp27;
        temp38 = temp20 * temp38;
        temp37 += temp38;
        temp37 = -temp37;
        temp38 = temp37 * temp37;
        temp38 = -temp38;
        temp7 += temp38;
        temp38 = temp8 * temp36;
        temp39 = temp13 * temp18;
        temp38 += temp39;
        temp38 = temp42 + temp38;
        temp38 = temp40 + temp38;
        temp28 += temp38;
        temp28 = temp43 + temp28;
        temp38 = temp19 * temp26;
        temp28 += temp38;
        temp38 = temp6 * temp5;
        temp28 += temp38;
        temp21 *= temp28;
        temp12 *= temp9;
        temp28 = temp25 * temp32;
        temp12 += temp28;
        temp11 = temp19 * temp11;
        temp28 = temp12 + temp11;
        temp10 = temp13 * temp10;
        temp13 = temp28 + temp10;
        temp17 *= temp36;
        temp13 += temp17;
        temp16 *= temp6;
        temp13 += temp16;
        temp25 *= temp41;
        temp13 += temp25;
        temp5 = -temp5;
        temp9 *= temp5;
        temp13 += temp9;
        temp13 = temp20 * temp13;
        temp13 = temp21 + temp13;
        temp10 = temp12 + temp10;
        temp10 = temp11 + temp10;
        temp10 = temp16 + temp10;
        temp10 = temp17 + temp10;
        temp10 = temp25 + temp10;
        temp9 += temp10;
        temp9 = -temp9;
        temp9 = temp23 * temp9;
        temp9 = temp13 + temp9;
        temp8 = temp14 * temp8;
        temp10 = temp18 * temp36;
        temp8 += temp10;
        temp8 = temp30 + temp8;
        temp8 = temp15 + temp8;
        temp8 = temp33 + temp8;
        temp8 = temp35 + temp8;
        temp6 *= temp26;
        temp6 = temp8 + temp6;
        temp5 = temp19 * temp5;
        temp5 = temp6 + temp5;
        temp5 = temp27 * temp5;
        temp5 = temp9 + temp5;
        temp5 = -temp5;
        temp5 *= temp5;
        temp5 = temp7 + temp5;
        temp5 = Math.Abs(temp5);
        temp5 = Math.Sqrt(temp5);
        temp5 = 1 / temp5;
        temp6 = temp29 * temp5;
        temp6 = -0.5 * temp6;
        temp7 = temp34 * temp5;
        temp7 = -0.5 * temp7;
        temp6 += temp7;
        temp7 = temp6 * temp6;
        temp8 = temp37 * temp5;
        temp8 = -0.5 * temp8;
        temp5 = temp31 * temp5;
        temp5 = -0.5 * temp5;
        temp5 = temp8 + temp5;
        temp8 = temp5 * temp5;
        temp9 = temp7 + temp8;
        temp9 = 0.5 * temp9;
        temp10 = -0.5 + temp9;
        temp9 = 0.5 + temp9;
        temp11 = temp9 * temp24;
        temp12 = temp10 * temp22;
        temp11 += temp12;
        temp12 = B.X.Value * temp6;
        temp11 += temp12;
        temp12 = B.Y.Value * temp5;
        temp11 += temp12;
        temp11 = -2 * temp11;
        temp11 = Math.Sqrt(temp11);
        temp11 *= temp11;
        temp11 = 0.5 * temp11;
        temp11 = -temp11;
        temp10 += temp11;
        temp9 += temp11;
        temp11 = temp9 * temp9;
        temp11 = -temp11;
        temp12 = temp10 * temp10;
        temp11 += temp12;
        temp7 += temp11;
        temp7 = temp8 + temp7;
        temp7 = Math.Abs(temp7);
        temp7 = Math.Sqrt(temp7);
        temp7 = 1 / temp7;
        temp8 = temp10 * temp7;
        temp10 = temp3 * temp8;
        temp4 *= temp2;
        temp11 = -temp4;
        temp9 *= temp7;
        temp11 *= temp9;
        temp10 += temp11;
        temp6 *= temp7;
        temp11 = temp4 * temp6;
        temp1 *= temp2;
        temp12 = -temp1;
        temp13 = temp8 * temp12;
        temp11 += temp13;
        temp11 = -temp11;
        temp13 = -temp11;
        temp14 = temp3 * temp6;
        temp12 = temp9 * temp12;
        temp12 = temp14 + temp12;
        temp12 = -temp12;
        temp13 += temp12;
        temp14 = temp13 * temp13;
        temp5 *= temp7;
        temp4 *= temp5;
        temp0 *= temp2;
        temp0 = -temp0;
        temp2 = temp8 * temp0;
        temp2 = temp4 + temp2;
        temp4 = -temp2;
        temp3 *= temp5;
        temp7 = temp9 * temp0;
        temp3 += temp7;
        temp4 += temp3;
        temp7 = temp4 * temp4;
        temp7 = temp14 + temp7;
        temp1 *= temp5;
        temp0 = temp6 * temp0;
        temp0 = temp1 + temp0;
        temp0 = -temp0;
        temp1 = -temp0;
        temp5 = temp1 * temp1;
        temp6 = temp7 + temp5;
        temp5 = -temp5;
        temp5 = temp6 + temp5;
        temp5 = 1 / temp5;
        temp6 = temp13 * temp5;
        temp7 = temp10 * temp6;
        temp8 = -temp3;
        temp1 *= temp5;
        temp8 *= temp1;
        temp7 += temp8;
        temp8 = temp2 * temp1;
        temp7 += temp8;
        temp8 = temp10 * temp10;
        temp8 = -temp8;
        temp9 = temp12 * temp12;
        temp9 = -temp9;
        temp8 += temp9;
        temp3 *= temp3;
        temp3 = -temp3;
        temp3 = temp8 + temp3;
        temp8 = temp11 * temp11;
        temp3 += temp8;
        temp2 *= temp2;
        temp2 = temp3 + temp2;
        temp0 *= temp0;
        temp0 = temp2 + temp0;
        temp0 = Math.Sqrt(temp0);
        temp2 = temp4 * temp5;
        temp3 = temp0 * temp2;
        temp3 = temp7 + temp3;
        var p1X = -temp3;

        temp3 = -temp10;
        temp3 = temp2 * temp3;
        temp4 = -temp12;
        temp4 = temp1 * temp4;
        temp3 += temp4;
        temp1 = temp11 * temp1;
        temp1 = temp3 + temp1;
        temp3 = temp6 * temp0;
        temp3 = temp1 + temp3;
        var p1Y = -temp3;

        temp0 = -temp0;
        temp2 *= temp0;
        temp2 = temp7 + temp2;
        var p2X = -temp2;

        temp0 = temp6 * temp0;
        temp0 = temp1 + temp0;
        var p2Y = -temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-02-18T20:01:23.9493960+02:00

        var d =
            Math.Abs(p1X - B.X.Value) +
            Math.Abs(p1Y - B.Y.Value);

        return d.IsNearZero()
            ? Float64Vector2D.Create(p2X, p2Y)
            : Float64Vector2D.Create(p1X, p1Y);
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    private Float64Vector2D SolveUsingCGaCollinsParallel()
    {
        //Begin GA-FuL MetaContext Code Generation, 2024-02-19T02:06:26.6564955+02:00

        var temp0 = A.X.Value * A.X.Value;
        var temp1 = A.Y.Value * A.Y.Value;
        temp0 += temp1;
        temp0 = 0.5 * temp0;
        temp1 = 0.5 + temp0;
        var temp2 = C.X.Value * temp1;
        var temp3 = -A.X.Value;
        var temp4 = C.X.Value * C.X.Value;
        var temp5 = C.Y.Value * C.Y.Value;
        temp4 += temp5;
        temp4 = 0.5 * temp4;
        temp5 = 0.5 + temp4;
        var temp6 = temp3 * temp5;
        temp2 += temp6;
        temp2 = -temp2;
        temp0 = -0.5 + temp0;
        temp6 = C.X.Value * temp0;
        temp4 = -0.5 + temp4;
        temp3 *= temp4;
        temp3 = temp6 + temp3;
        temp2 += temp3;
        temp3 = -temp2;
        temp3 = -temp3;
        temp6 = -Beta.Radians.Value;
        temp6 = 0.5 * temp6;
        var temp7 = Math.Sin(temp6);
        var temp8 = 0.5 * A.Y.Value;
        var temp9 = -temp8;
        var temp10 = temp7 * temp9;
        temp6 = Math.Cos(temp6);
        var temp11 = 0.5 * A.X.Value;
        var temp12 = temp6 * temp11;
        var temp13 = -temp7;
        var temp14 = temp8 * temp13;
        temp12 += temp14;
        temp10 += temp12;
        temp14 = -temp11;
        var temp15 = temp6 * temp14;
        temp10 += temp15;
        temp15 = A.X.Value * C.Y.Value;
        var temp16 = -C.X.Value;
        temp16 = A.Y.Value * temp16;
        temp15 += temp16;
        temp16 = temp10 * temp15;
        temp11 = temp7 * temp11;
        temp8 *= temp6;
        temp8 = temp11 + temp8;
        temp11 = temp14 * temp8;
        var temp17 = -temp8;
        var temp18 = temp14 * temp17;
        var temp19 = temp9 * temp12;
        var temp20 = temp18 + temp19;
        temp20 = temp11 + temp20;
        var temp21 = -temp12;
        var temp22 = temp9 * temp21;
        temp20 += temp22;
        temp7 += temp20;
        temp20 = -temp7;
        var temp23 = temp2 * temp20;
        var temp24 = -temp10;
        var temp25 = temp15 * temp24;
        temp23 += temp25;
        temp23 = temp16 + temp23;
        temp17 = temp9 * temp17;
        var temp26 = temp9 * temp8;
        temp12 *= temp14;
        var temp27 = temp26 + temp12;
        temp27 = temp17 + temp27;
        temp21 = temp14 * temp21;
        temp27 += temp21;
        temp27 = temp6 + temp27;
        temp1 = C.Y.Value * temp1;
        temp5 = -temp5;
        temp5 = A.Y.Value * temp5;
        temp1 += temp5;
        temp1 = -temp1;
        temp0 = C.Y.Value * temp0;
        temp4 = -temp4;
        temp4 = A.Y.Value * temp4;
        temp0 += temp4;
        temp0 = temp1 + temp0;
        temp1 = temp27 * temp0;
        temp1 = temp23 + temp1;
        temp4 = -temp10;
        temp5 = temp1 * temp4;
        temp10 = temp13 * temp14;
        temp8 += temp10;
        temp6 = temp9 * temp6;
        temp6 = temp8 + temp6;
        temp8 = -temp6;
        temp9 = temp15 * temp8;
        temp10 = temp7 * temp0;
        temp13 = temp15 * temp6;
        temp10 += temp13;
        temp10 = temp9 + temp10;
        temp14 = temp2 * temp27;
        temp10 += temp14;
        temp14 = -temp10;
        temp23 = -temp6;
        var temp28 = temp14 * temp23;
        temp5 += temp28;
        temp17 += temp21;
        temp17 = temp26 + temp17;
        temp12 += temp17;
        temp17 = temp15 * temp12;
        temp6 = temp2 * temp6;
        temp6 = temp17 + temp6;
        temp17 = temp24 * temp0;
        temp6 += temp17;
        temp17 = temp15 * temp27;
        temp6 += temp17;
        temp17 = -temp6;
        temp21 = -temp12;
        temp26 = temp17 * temp21;
        temp28 = temp5 + temp26;
        var temp29 = temp27 * temp6;
        temp28 += temp29;
        temp20 = temp15 * temp20;
        temp8 = temp0 * temp8;
        temp8 = temp20 + temp8;
        temp20 = temp2 * temp24;
        temp8 += temp20;
        temp11 += temp22;
        temp11 = temp18 + temp11;
        temp11 = temp19 + temp11;
        temp18 = -temp11;
        temp19 = temp15 * temp18;
        temp8 += temp19;
        temp7 = -temp7;
        temp19 = temp8 * temp7;
        temp20 = temp28 + temp19;
        temp22 = temp18 * temp8;
        temp20 += temp22;
        temp24 = temp2 * temp12;
        temp9 += temp24;
        temp9 = temp13 + temp9;
        temp13 = temp0 * temp11;
        temp9 += temp13;
        temp13 = -temp9;
        temp24 = temp23 * temp13;
        temp20 += temp24;
        temp12 = temp0 * temp12;
        temp12 = temp16 + temp12;
        temp12 = temp25 + temp12;
        temp2 *= temp18;
        temp2 = temp12 + temp2;
        temp12 = temp4 * temp2;
        temp16 = temp20 + temp12;
        temp16 = -temp16;
        temp5 += temp29;
        temp5 = temp26 + temp5;
        temp5 = temp22 + temp5;
        temp5 = temp19 + temp5;
        temp5 = temp24 + temp5;
        temp5 = temp12 + temp5;
        temp5 = temp16 + temp5;
        temp12 = B.X.Value * B.X.Value;
        temp16 = B.Y.Value * B.Y.Value;
        temp12 += temp16;
        temp12 = 0.5 * temp12;
        temp16 = 0.5 + temp12;
        temp19 = temp5 * temp16;
        temp20 = -temp8;
        temp22 = temp23 * temp20;
        temp24 = temp27 * temp1;
        temp25 = temp10 * temp7;
        temp24 += temp25;
        temp25 = temp4 * temp6;
        temp24 += temp25;
        temp25 = temp4 * temp17;
        temp24 += temp25;
        temp25 = temp23 * temp8;
        temp24 += temp25;
        temp22 += temp24;
        temp24 = temp11 * temp9;
        temp22 += temp24;
        temp24 = temp21 * temp2;
        temp22 += temp24;
        temp22 = -temp22;
        temp24 = -temp22;
        temp25 = B.X.Value * temp24;
        temp19 += temp25;
        temp26 = temp4 * temp20;
        temp1 = -temp1;
        temp28 = temp7 * temp1;
        temp10 = temp27 * temp10;
        temp10 = temp28 + temp10;
        temp28 = temp23 * temp17;
        temp10 += temp28;
        temp28 = temp23 * temp6;
        temp10 += temp28;
        temp28 = temp4 * temp8;
        temp10 += temp28;
        temp10 = temp26 + temp10;
        temp9 = temp21 * temp9;
        temp9 = temp10 + temp9;
        temp10 = temp18 * temp2;
        temp9 += temp10;
        temp9 = -temp9;
        temp10 = B.Y.Value * temp9;
        temp18 = temp19 + temp10;
        temp18 = -temp18;
        temp19 = temp3 * temp18;
        temp15 = -temp15;
        temp26 = temp16 * temp9;
        temp9 = -temp9;
        temp12 = -0.5 + temp12;
        temp9 *= temp12;
        temp9 = temp26 + temp9;
        temp9 = -temp9;
        temp26 = temp15 * temp9;
        temp19 += temp26;
        temp28 = temp19 * temp19;
        temp28 = -temp28;
        temp22 = temp16 * temp22;
        temp24 *= temp12;
        temp22 += temp24;
        temp24 = temp3 * temp22;
        temp9 = temp0 * temp9;
        temp9 = temp24 + temp9;
        temp9 = -temp9;
        temp9 *= temp9;
        temp9 = -temp9;
        temp9 = temp28 + temp9;
        temp5 *= temp12;
        temp5 = temp25 + temp5;
        temp5 = temp10 + temp5;
        temp5 = -temp5;
        temp10 = temp3 * temp5;
        temp10 = temp26 + temp10;
        temp24 = temp10 * temp10;
        temp9 += temp24;
        temp24 = -temp0;
        temp25 = temp18 * temp24;
        temp22 = temp15 * temp22;
        temp25 += temp22;
        temp25 = -temp25;
        temp26 = temp25 * temp25;
        temp26 = -temp26;
        temp9 += temp26;
        temp24 = temp5 * temp24;
        temp22 += temp24;
        temp22 = -temp22;
        temp24 = temp22 * temp22;
        temp9 += temp24;
        temp24 = -temp15;
        temp5 *= temp24;
        temp18 *= temp15;
        temp5 += temp18;
        temp5 *= temp5;
        temp5 = temp9 + temp5;
        temp9 = temp21 * temp20;
        temp7 = temp17 * temp7;
        temp1 = temp23 * temp1;
        temp14 = temp4 * temp14;
        temp1 += temp14;
        temp6 *= temp11;
        temp11 = temp1 + temp6;
        temp11 = temp7 + temp11;
        temp8 = temp27 * temp8;
        temp11 += temp8;
        temp11 = temp9 + temp11;
        temp4 *= temp13;
        temp11 += temp4;
        temp2 = -temp2;
        temp2 = temp23 * temp2;
        temp11 += temp2;
        temp11 = -temp11;
        temp1 = temp7 + temp1;
        temp1 = temp6 + temp1;
        temp1 = temp9 + temp1;
        temp1 = temp8 + temp1;
        temp1 = temp4 + temp1;
        temp1 = temp2 + temp1;
        temp1 = temp11 + temp1;
        temp2 = B.Y.Value * temp1;
        temp2 = temp3 * temp2;
        temp3 = B.X.Value * temp1;
        temp3 = -temp3;
        temp0 *= temp3;
        temp0 = temp2 + temp0;
        temp2 = temp12 * temp1;
        temp2 = temp24 * temp2;
        temp0 += temp2;
        temp1 = temp16 * temp1;
        temp1 = temp15 * temp1;
        temp0 += temp1;
        temp0 = -temp0;
        temp0 *= temp0;
        temp0 = temp5 + temp0;
        temp0 = Math.Abs(temp0);
        temp0 = Math.Sqrt(temp0);
        temp0 = 1 / temp0;
        temp1 = temp19 * temp0;
        temp1 = -0.5 * temp1;
        temp2 = temp10 * temp0;
        temp2 = -0.5 * temp2;
        var pX = temp1 + temp2;

        temp1 = temp25 * temp0;
        temp1 = -0.5 * temp1;
        temp0 = temp22 * temp0;
        temp0 = -0.5 * temp0;
        var pY = temp1 + temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-02-19T02:06:26.7628688+02:00

        return Float64Vector2D.Create(pX, pY);
    }

    /// <summary>
    /// Code-generated implementation, very fast to use
    /// </summary>
    /// <returns></returns>
    public Float64Vector2D SolveUsingCGaCollins()
    {
        if ((Alpha.Radians.Value + Beta.Radians.Value - Math.PI).IsNearZero())
            return SolveUsingCGaCollinsParallel();

        //Begin GA-FuL MetaContext Code Generation, 2024-02-19T02:19:53.1646747+02:00
        var temp0 = -Beta.Radians.Value;
        temp0 = 0.5 * temp0;
        var temp1 = Math.Cos(temp0);
        var temp2 = 0.5 * A.X.Value;
        var temp3 = temp1 * temp2;
        temp0 = Math.Sin(temp0);
        var temp4 = -temp0;
        var temp5 = 0.5 * A.Y.Value;
        var temp6 = temp4 * temp5;
        temp3 += temp6;
        temp6 = -temp3;
        var temp7 = -temp5;
        var temp8 = temp6 * temp7;
        var temp9 = temp3 * temp7;
        var temp10 = temp2 * temp0;
        temp5 = temp1 * temp5;
        temp5 = temp10 + temp5;
        temp10 = -temp5;
        temp2 = -temp2;
        var temp11 = temp10 * temp2;
        var temp12 = temp9 + temp11;
        var temp13 = temp5 * temp2;
        temp12 += temp13;
        temp12 = temp8 + temp12;
        temp12 = temp0 + temp12;
        var temp14 = -temp12;
        var temp15 = A.X.Value * C.Y.Value;
        var temp16 = -C.X.Value;
        temp16 = A.Y.Value * temp16;
        temp15 += temp16;
        temp16 = temp14 * temp15;
        temp4 *= temp2;
        temp4 = temp5 + temp4;
        var temp17 = temp1 * temp7;
        temp4 += temp17;
        temp17 = -temp4;
        var temp18 = A.X.Value * A.X.Value;
        var temp19 = A.Y.Value * A.Y.Value;
        temp18 += temp19;
        temp18 = 0.5 * temp18;
        temp19 = 0.5 + temp18;
        var temp20 = C.Y.Value * temp19;
        var temp21 = C.X.Value * C.X.Value;
        var temp22 = C.Y.Value * C.Y.Value;
        temp21 += temp22;
        temp21 = 0.5 * temp21;
        temp22 = 0.5 + temp21;
        var temp23 = -temp22;
        temp23 = A.Y.Value * temp23;
        temp20 += temp23;
        temp23 = -temp20;
        temp18 = -0.5 + temp18;
        var temp24 = C.Y.Value * temp18;
        temp21 = -0.5 + temp21;
        var temp25 = -temp21;
        temp25 = A.Y.Value * temp25;
        temp24 += temp25;
        temp23 += temp24;
        temp25 = temp17 * temp23;
        temp16 += temp25;
        temp0 *= temp7;
        temp0 = temp3 + temp0;
        temp25 = temp1 * temp2;
        temp0 += temp25;
        temp25 = -temp0;
        var temp26 = C.X.Value * temp19;
        var temp27 = -A.X.Value;
        var temp28 = temp22 * temp27;
        temp26 += temp28;
        temp28 = -temp26;
        var temp29 = C.X.Value * temp18;
        temp27 = temp21 * temp27;
        temp27 = temp29 + temp27;
        temp28 += temp27;
        temp29 = temp25 * temp28;
        temp16 += temp29;
        temp8 += temp13;
        temp8 = temp11 + temp8;
        temp8 = temp9 + temp8;
        temp9 = -temp8;
        temp11 = temp15 * temp9;
        temp11 = temp16 + temp11;
        temp13 = -temp11;
        temp16 = -temp0;
        temp29 = temp13 * temp16;
        temp0 = temp15 * temp0;
        temp14 *= temp28;
        var temp30 = temp15 * temp25;
        temp14 += temp30;
        temp14 = temp0 + temp14;
        temp6 *= temp2;
        temp5 = temp7 * temp5;
        temp2 = temp3 * temp2;
        temp3 = temp5 + temp2;
        temp7 *= temp10;
        temp3 += temp7;
        temp3 = temp6 + temp3;
        temp1 += temp3;
        temp3 = temp23 * temp1;
        temp3 = temp14 + temp3;
        temp10 = -temp3;
        temp14 = -temp12;
        var temp31 = temp10 * temp14;
        temp17 = temp15 * temp17;
        temp12 *= temp23;
        var temp32 = temp15 * temp4;
        temp12 += temp32;
        temp12 = temp17 + temp12;
        var temp33 = temp28 * temp1;
        temp12 += temp33;
        temp33 = temp1 * temp12;
        temp31 += temp33;
        temp6 += temp7;
        temp5 += temp6;
        temp2 += temp5;
        temp5 = temp15 * temp2;
        temp6 = temp4 * temp28;
        temp5 += temp6;
        temp6 = temp23 * temp25;
        temp5 += temp6;
        temp6 = temp15 * temp1;
        temp5 += temp6;
        temp6 = -temp5;
        temp4 = -temp4;
        temp7 = temp6 * temp4;
        temp25 = temp31 + temp7;
        temp31 = temp5 * temp4;
        temp25 += temp31;
        temp33 = temp11 * temp16;
        temp25 += temp33;
        temp25 = temp29 + temp25;
        var temp34 = temp28 * temp2;
        temp17 += temp34;
        temp17 = temp32 + temp17;
        temp32 = temp23 * temp8;
        temp17 += temp32;
        temp32 = -temp2;
        temp34 = temp17 * temp32;
        temp25 += temp34;
        temp2 = temp23 * temp2;
        temp0 += temp2;
        temp0 = temp30 + temp0;
        temp2 = temp28 * temp9;
        temp0 += temp2;
        temp2 = temp9 * temp0;
        temp2 = temp25 + temp2;
        temp2 = -temp2;
        temp2 = -temp2;
        temp25 = 0.5 * Alpha.Radians.Value;
        temp30 = Math.Cos(temp25);
        temp34 = 0.5 * C.X.Value;
        var temp35 = temp30 * temp34;
        temp25 = Math.Sin(temp25);
        var temp36 = -temp25;
        var temp37 = 0.5 * C.Y.Value;
        var temp38 = temp36 * temp37;
        temp35 += temp38;
        temp38 = -temp37;
        var temp39 = temp25 * temp38;
        temp39 = temp35 + temp39;
        var temp40 = -temp34;
        var temp41 = temp30 * temp40;
        temp39 += temp41;
        temp41 = temp15 * temp39;
        var temp42 = -temp35;
        var temp43 = temp38 * temp42;
        temp34 *= temp25;
        temp37 = temp30 * temp37;
        temp34 += temp37;
        temp37 = temp40 * temp34;
        var temp44 = -temp34;
        var temp45 = temp40 * temp44;
        var temp46 = temp35 * temp38;
        var temp47 = temp45 + temp46;
        temp47 = temp37 + temp47;
        temp47 = temp43 + temp47;
        temp25 += temp47;
        temp47 = -temp25;
        var temp48 = temp28 * temp47;
        var temp49 = -temp39;
        var temp50 = temp15 * temp49;
        temp48 += temp50;
        temp48 = temp41 + temp48;
        temp42 = temp40 * temp42;
        temp44 = temp38 * temp44;
        var temp51 = temp38 * temp34;
        temp35 *= temp40;
        var temp52 = temp51 + temp35;
        temp52 = temp44 + temp52;
        temp52 = temp42 + temp52;
        temp52 = temp30 + temp52;
        var temp53 = temp23 * temp52;
        temp48 += temp53;
        temp39 = -temp39;
        temp53 = temp48 * temp39;
        temp36 *= temp40;
        temp34 += temp36;
        temp30 *= temp38;
        temp30 = temp34 + temp30;
        temp34 = -temp30;
        temp36 = temp15 * temp34;
        temp38 = temp23 * temp25;
        temp40 = temp15 * temp30;
        temp38 += temp40;
        temp38 = temp36 + temp38;
        var temp54 = temp28 * temp52;
        temp38 += temp54;
        temp54 = -temp38;
        var temp55 = -temp30;
        var temp56 = temp54 * temp55;
        temp53 += temp56;
        temp42 += temp44;
        temp42 = temp51 + temp42;
        temp35 += temp42;
        temp42 = temp15 * temp35;
        temp30 = temp28 * temp30;
        temp30 = temp42 + temp30;
        temp42 = temp23 * temp49;
        temp30 += temp42;
        temp42 = temp15 * temp52;
        temp30 += temp42;
        temp42 = -temp30;
        temp44 = -temp35;
        temp51 = temp42 * temp44;
        temp56 = temp53 + temp51;
        var temp57 = temp52 * temp30;
        temp56 += temp57;
        temp47 = temp15 * temp47;
        temp34 = temp23 * temp34;
        temp34 = temp47 + temp34;
        temp47 = temp28 * temp49;
        temp34 += temp47;
        temp37 = temp43 + temp37;
        temp37 = temp45 + temp37;
        temp37 = temp46 + temp37;
        temp43 = -temp37;
        temp45 = temp15 * temp43;
        temp34 += temp45;
        temp25 = -temp25;
        temp45 = temp34 * temp25;
        temp46 = temp56 + temp45;
        temp47 = temp43 * temp34;
        temp46 += temp47;
        temp49 = temp28 * temp35;
        temp36 += temp49;
        temp36 = temp40 + temp36;
        temp40 = temp23 * temp37;
        temp36 += temp40;
        temp40 = -temp36;
        temp49 = temp55 * temp40;
        temp46 += temp49;
        temp23 *= temp35;
        temp23 = temp41 + temp23;
        temp23 = temp50 + temp23;
        temp28 *= temp43;
        temp23 += temp28;
        temp28 = temp39 * temp23;
        temp35 = temp46 + temp28;
        temp35 = -temp35;
        temp41 = temp2 * temp35;
        temp46 = temp16 * temp3;
        temp50 = -temp12;
        temp56 = temp4 * temp50;
        temp46 += temp56;
        temp56 = temp6 * temp32;
        var temp58 = temp46 + temp56;
        var temp59 = temp1 * temp5;
        temp58 += temp59;
        var temp60 = temp11 * temp14;
        temp58 += temp60;
        var temp61 = temp9 * temp11;
        temp58 += temp61;
        var temp62 = -temp17;
        var temp63 = temp4 * temp62;
        temp58 += temp63;
        var temp64 = temp16 * temp0;
        temp58 += temp64;
        temp58 = -temp58;
        var temp65 = -temp34;
        var temp66 = temp65 * temp39;
        var temp67 = -temp48;
        var temp68 = temp67 * temp25;
        var temp69 = temp52 * temp38;
        temp68 += temp69;
        temp69 = temp55 * temp42;
        temp68 += temp69;
        var temp70 = temp55 * temp30;
        temp68 += temp70;
        var temp71 = temp39 * temp34;
        temp68 += temp71;
        temp68 = temp66 + temp68;
        var temp72 = temp44 * temp36;
        temp68 += temp72;
        temp72 = temp43 * temp23;
        temp68 += temp72;
        temp68 = -temp68;
        temp72 = temp58 * temp68;
        temp41 = temp72 + temp41;
        temp53 += temp57;
        temp51 = temp53 + temp51;
        temp47 = temp51 + temp47;
        temp45 = temp47 + temp45;
        temp45 += temp49;
        temp28 = temp45 + temp28;
        temp28 = -temp28;
        temp45 = temp28 * temp2;
        temp46 += temp59;
        temp46 += temp56;
        temp46 += temp61;
        temp46 += temp60;
        temp46 += temp63;
        temp46 += temp64;
        temp46 = -temp46;
        temp47 = temp68 * temp46;
        temp45 += temp47;
        temp47 = temp45 * temp45;
        temp49 = temp41 * temp41;
        temp49 = -temp49;
        temp47 += temp49;
        temp49 = temp65 * temp55;
        temp51 = temp52 * temp48;
        temp53 = temp38 * temp25;
        temp51 += temp53;
        temp53 = temp39 * temp30;
        temp51 += temp53;
        temp56 = temp39 * temp42;
        temp51 += temp56;
        temp57 = temp55 * temp34;
        temp51 += temp57;
        temp51 = temp49 + temp51;
        temp59 = temp37 * temp36;
        temp51 += temp59;
        temp59 = temp44 * temp23;
        temp51 += temp59;
        temp59 = temp51 * temp2;
        temp60 = temp13 * temp4;
        temp61 = temp1 * temp3;
        temp63 = temp14 * temp12;
        temp61 += temp63;
        temp63 = temp16 * temp5;
        temp61 += temp63;
        temp64 = temp16 * temp6;
        temp61 += temp64;
        temp72 = temp11 * temp4;
        temp61 += temp72;
        temp61 = temp60 + temp61;
        var temp73 = temp8 * temp17;
        temp61 += temp73;
        temp73 = temp32 * temp0;
        temp61 += temp73;
        temp73 = temp68 * temp61;
        temp59 += temp73;
        temp59 = -temp59;
        temp59 *= temp59;
        temp59 = -temp59;
        temp47 += temp59;
        temp59 = temp46 * temp35;
        temp73 = -temp58;
        var temp74 = temp28 * temp73;
        temp59 += temp74;
        temp59 *= temp59;
        temp47 += temp59;
        temp59 = temp46 * temp51;
        temp74 = -temp61;
        var temp75 = temp28 * temp74;
        temp59 += temp75;
        temp59 = -temp59;
        temp75 = temp59 * temp59;
        temp47 += temp75;
        temp58 *= temp51;
        temp74 *= temp35;
        temp58 += temp74;
        temp58 = -temp58;
        temp74 = temp58 * temp58;
        temp74 = -temp74;
        temp47 += temp74;
        temp74 = temp48 * temp44;
        temp75 = temp38 * temp37;
        temp74 += temp75;
        temp56 += temp74;
        temp53 += temp56;
        temp49 += temp53;
        temp49 = temp57 + temp49;
        temp53 = temp25 * temp36;
        temp49 += temp53;
        temp53 = temp52 * temp23;
        temp49 += temp53;
        temp2 = temp49 * temp2;
        temp49 = temp65 * temp44;
        temp42 *= temp25;
        temp53 = temp67 * temp55;
        temp54 = temp39 * temp54;
        temp53 += temp54;
        temp30 *= temp37;
        temp37 = temp53 + temp30;
        temp37 = temp42 + temp37;
        temp34 = temp52 * temp34;
        temp37 += temp34;
        temp37 = temp49 + temp37;
        temp39 *= temp40;
        temp37 += temp39;
        temp23 = -temp23;
        temp40 = temp23 * temp55;
        temp37 += temp40;
        temp37 = temp46 * temp37;
        temp2 += temp37;
        temp37 = temp42 + temp53;
        temp30 += temp37;
        temp30 = temp49 + temp30;
        temp30 = temp34 + temp30;
        temp30 = temp39 + temp30;
        temp30 = temp40 + temp30;
        temp30 = temp73 * temp30;
        temp2 += temp30;
        temp30 = temp48 * temp43;
        temp34 = temp38 * temp44;
        temp30 += temp34;
        temp30 = temp70 + temp30;
        temp30 = temp69 + temp30;
        temp30 = temp66 + temp30;
        temp30 = temp71 + temp30;
        temp34 = temp52 * temp36;
        temp30 += temp34;
        temp23 *= temp25;
        temp23 = temp30 + temp23;
        temp23 = -temp23;
        temp23 = temp61 * temp23;
        temp2 += temp23;
        temp23 = temp3 * temp32;
        temp25 = temp8 * temp12;
        temp23 += temp25;
        temp23 = temp64 + temp23;
        temp23 = temp63 + temp23;
        temp23 = temp60 + temp23;
        temp23 = temp72 + temp23;
        temp25 = temp14 * temp17;
        temp23 += temp25;
        temp25 = temp1 * temp0;
        temp23 += temp25;
        temp23 = temp68 * temp23;
        temp2 += temp23;
        temp10 *= temp4;
        temp23 = temp16 * temp50;
        temp10 += temp23;
        temp6 = temp14 * temp6;
        temp23 = temp10 + temp6;
        temp5 = temp8 * temp5;
        temp8 = temp23 + temp5;
        temp13 *= temp32;
        temp8 += temp13;
        temp11 *= temp1;
        temp8 += temp11;
        temp16 *= temp62;
        temp8 += temp16;
        temp0 = -temp0;
        temp4 = temp0 * temp4;
        temp8 += temp4;
        temp8 *= temp35;
        temp2 += temp8;
        temp5 = temp10 + temp5;
        temp5 = temp6 + temp5;
        temp5 = temp11 + temp5;
        temp5 = temp13 + temp5;
        temp5 = temp16 + temp5;
        temp4 += temp5;
        temp4 = -temp4;
        temp4 = temp28 * temp4;
        temp2 += temp4;
        temp3 = temp9 * temp3;
        temp4 = temp12 * temp32;
        temp3 += temp4;
        temp3 += temp31;
        temp3 += temp7;
        temp3 += temp29;
        temp3 += temp33;
        temp1 *= temp17;
        temp1 = temp3 + temp1;
        temp0 *= temp14;
        temp0 = temp1 + temp0;
        temp0 = -temp0;
        temp0 = -temp0;
        temp0 = temp51 * temp0;
        temp0 = temp2 + temp0;
        temp0 = -temp0;
        temp0 *= temp0;
        temp0 = temp47 + temp0;
        temp0 = Math.Abs(temp0);
        temp0 = Math.Sqrt(temp0);
        temp0 = 1 / temp0;
        temp1 = temp41 * temp0;
        temp1 = -0.5 * temp1;
        temp2 = temp45 * temp0;
        temp2 = -0.5 * temp2;
        var eX = temp1 + temp2;

        temp1 = temp58 * temp0;
        temp1 = -0.5 * temp1;
        temp0 = temp59 * temp0;
        temp0 = -0.5 * temp0;
        var eY = temp1 + temp0;

        temp0 = temp19 * temp21;
        temp1 = -temp18;
        temp1 *= temp22;
        temp0 += temp1;
        temp1 = temp0 * eX;
        temp2 = -temp26;
        temp3 = eX * eX;
        temp4 = eY * eY;
        temp3 += temp4;
        temp3 = 0.5 * temp3;
        temp4 = -0.5 + temp3;
        temp2 *= temp4;
        temp1 += temp2;
        temp2 = 0.5 + temp3;
        temp3 = temp2 * temp27;
        temp1 += temp3;
        temp1 = -temp1;
        temp1 = -temp1;
        temp3 = B.Y.Value * eX;
        temp5 = -eY;
        temp5 = B.X.Value * temp5;
        temp3 += temp5;
        temp3 = -temp3;
        temp5 = temp1 * temp3;
        temp6 = eY * temp26;
        temp7 = -eX;
        temp8 = temp7 * temp20;
        temp6 += temp8;
        temp8 = temp2 * temp15;
        temp6 += temp8;
        temp6 = -temp6;
        temp8 = B.X.Value * temp2;
        temp9 = B.X.Value * B.X.Value;
        temp10 = B.Y.Value * B.Y.Value;
        temp9 += temp10;
        temp9 = 0.5 * temp9;
        temp10 = 0.5 + temp9;
        temp10 = -temp10;
        temp11 = temp10 * eX;
        temp8 += temp11;
        temp8 = -temp8;
        temp11 = B.X.Value * temp4;
        temp9 = -0.5 + temp9;
        temp9 = -temp9;
        temp12 = temp9 * eX;
        temp11 += temp12;
        temp8 += temp11;
        temp8 = -temp8;
        temp11 = temp6 * temp8;
        temp11 = temp5 + temp11;
        temp12 = B.Y.Value * temp2;
        temp10 *= eY;
        temp10 = temp12 + temp10;
        temp10 = -temp10;
        temp12 = B.Y.Value * temp4;
        temp9 *= eY;
        temp9 = temp12 + temp9;
        temp9 = temp10 + temp9;
        temp1 *= temp9;
        temp0 *= eY;
        temp10 = -temp20;
        temp10 = temp4 * temp10;
        temp0 += temp10;
        temp2 *= temp24;
        temp0 += temp2;
        temp2 = temp8 * temp0;
        temp1 += temp2;
        temp1 = -temp1;
        temp2 = temp1 * temp1;
        temp2 = -temp2;
        temp10 = temp11 * temp11;
        temp10 = -temp10;
        temp2 += temp10;
        temp10 = eY * temp27;
        temp7 *= temp24;
        temp7 = temp10 + temp7;
        temp4 *= temp15;
        temp4 = temp7 + temp4;
        temp4 = -temp4;
        temp7 = temp8 * temp4;
        temp5 += temp7;
        temp7 = temp5 * temp5;
        temp2 += temp7;
        temp0 = -temp0;
        temp0 = temp3 * temp0;
        temp7 = temp6 * temp9;
        temp7 = temp0 + temp7;
        temp7 = -temp7;
        temp8 = temp7 * temp7;
        temp8 = -temp8;
        temp2 += temp8;
        temp8 = temp9 * temp4;
        temp0 += temp8;
        temp0 = -temp0;
        temp8 = temp0 * temp0;
        temp2 += temp8;
        temp6 = -temp6;
        temp6 = temp3 * temp6;
        temp3 *= temp4;
        temp3 = temp6 + temp3;
        temp4 = temp3 * temp3;
        temp2 += temp4;
        temp2 = Math.Abs(temp2);
        temp2 = Math.Sqrt(temp2);
        temp2 = 1 / temp2;
        temp4 = temp11 * temp2;
        temp1 *= temp2;
        temp6 = -temp1;
        temp8 = temp6 * temp6;
        temp9 = -temp4;
        temp5 *= temp2;
        temp9 += temp5;
        temp10 = temp9 * temp9;
        temp10 = temp8 + temp10;
        temp7 *= temp2;
        temp11 = -temp7;
        temp0 *= temp2;
        temp11 += temp0;
        temp12 = temp11 * temp11;
        temp10 += temp12;
        temp8 = -temp8;
        temp8 = temp10 + temp8;
        temp8 = 1 / temp8;
        temp6 *= temp8;
        temp10 = temp4 * temp6;
        temp12 = -temp5;
        temp12 = temp6 * temp12;
        temp10 += temp12;
        temp2 = temp3 * temp2;
        temp3 = temp11 * temp8;
        temp11 = temp2 * temp3;
        temp10 += temp11;
        temp1 *= temp1;
        temp4 *= temp4;
        temp1 += temp4;
        temp4 = temp5 * temp5;
        temp4 = -temp4;
        temp1 += temp4;
        temp4 = temp7 * temp7;
        temp1 += temp4;
        temp4 = temp0 * temp0;
        temp4 = -temp4;
        temp1 += temp4;
        temp4 = temp2 * temp2;
        temp4 = -temp4;
        temp1 += temp4;
        temp1 = Math.Sqrt(temp1);
        temp4 = temp9 * temp8;
        temp5 = temp1 * temp4;
        temp5 = temp10 + temp5;
        var p1X = -temp5;

        temp5 = temp7 * temp6;
        temp0 = -temp0;
        temp0 = temp6 * temp0;
        temp0 = temp5 + temp0;
        temp2 = -temp2;
        temp2 = temp4 * temp2;
        temp0 += temp2;
        temp2 = temp3 * temp1;
        temp2 = temp0 + temp2;
        var p1Y = -temp2;

        temp1 = -temp1;
        temp2 = temp4 * temp1;
        temp2 = temp10 + temp2;
        var p2X = -temp2;

        temp1 = temp3 * temp1;
        temp0 += temp1;
        var p2Y = -temp0;

        //Finish GA-FuL MetaContext Code Generation, 2024-02-19T02:19:53.2948969+02:00

        var d =
            Math.Abs(p1X - eX) +
            Math.Abs(p1Y - eY);

        return d.IsNearZero()
            ? Float64Vector2D.Create(p2X, p2Y)
            : Float64Vector2D.Create(p1X, p1Y);
    }


    public override string ToString()
    {
        return new StringBuilder()
            .AppendLine("2D Snellius-Pothenot Problem Data")
            .AppendLine($"   A    : {A.ToTupleString()}")
            .AppendLine($"   B    : {B.ToTupleString()}")
            .AppendLine($"   C    : {C.ToTupleString()}")
            .AppendLine($"   alpha: {Alpha}")
            .AppendLine($"   beta : {Beta}")
            .ToString();
    }
}