using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using MathNet.Numerics.LinearAlgebra;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.PowerSystems.GAPoT;

public static class SymmetricalComponentsSamples
{
    public static void ClarkeSkrRelationSample()
    {
        var n = 4;

        var scalarProcessor =
            ScalarProcessorOfWolframExpr.Instance;

        var processor =
            XGaProcessor<Expr>.CreateEuclidean(
                scalarProcessor
            );

        var latexComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

        latexComposer.BasisName = @"\boldsymbol{\mu}";

        var clarkeMatrix =
            scalarProcessor.CreateClarkeRotationMap(n).ToArray(n);

        Console.WriteLine("Clarke Matrix:");
        Console.WriteLine(latexComposer.GetArrayText(clarkeMatrix));
        Console.WriteLine();

        var skrMatrix =
            processor.CreateSimpleKirchhoffRotor(n).GetVectorMapArray(n);

        Console.WriteLine("SKR Matrix:");
        Console.WriteLine(latexComposer.GetArrayText(skrMatrix));
        Console.WriteLine();

        var m1 =
            scalarProcessor.Times(
                clarkeMatrix,
                skrMatrix
            ).GetSubArray(0, 0, 3, 3);

        var clarkeMatrix3 =
            scalarProcessor.CreateClarkeRotationArray(3);

        var skrMatrix3 =
            processor
                .CreateSimpleKirchhoffRotor(3)
                .GetVectorMapArray(3);

        var m2 = scalarProcessor.Times(skrMatrix3, m1);

        Console.WriteLine("SKR * Clarke:");
        Console.WriteLine(latexComposer.GetArrayText(m2));
        Console.WriteLine();

        //processor.CreateLinMatrixDenseStorage()
    }

    public static void SymbolicSymmetricalComponentsSample1()
    {
        var n = 3;

        var scalarProcessor = ScalarProcessorOfWolframExpr.Instance;

        var processor =
            XGaProcessor<Expr>.CreateEuclidean(scalarProcessor);

        var latexComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

        latexComposer.BasisName = @"\boldsymbol{\mu}";

        var nSqrt = $"Sqrt[{n}]".ToExpr();
        var omega = @"\[Omega]".ToExpr();
        var t = @"t".ToExpr();
        var wt = Mfs.Times[omega, t];

        var aMagnitude = @$"Subscript[U,a]".ScalarFromText(scalarProcessor);
        var bMagnitude = @$"Subscript[U,b]".ScalarFromText(scalarProcessor);
        var cMagnitude = @$"Subscript[U,c]".ScalarFromText(scalarProcessor);

        //var aAngle = @$"{wt} + 2 * Pi * 0 / 3".CreateScalar(scalarProcessor);
        //var bAngle = @$"{wt} + 2 * Pi * 1 / 3".CreateScalar(scalarProcessor);
        //var cAngle = @$"{wt} + 2 * Pi * 2 / 3".CreateScalar(scalarProcessor);

        var aAngle = @$"{wt} + Subscript[\[Theta],a]".ScalarFromText(scalarProcessor);
        var bAngle = @$"{wt} + Subscript[\[Theta],b]".ScalarFromText(scalarProcessor);
        var cAngle = @$"{wt} + Subscript[\[Theta],c]".ScalarFromText(scalarProcessor);

        var assumeExpr1 =
            @$"Element[{omega} | {t} | {aMagnitude} | {bMagnitude} | {cMagnitude} | {aAngle} | {bAngle} | {cAngle}, Reals]";

        var assumeExpr =
            $@"And[{assumeExpr1}, {omega} > 0, {aMagnitude} > 0, {bMagnitude} > 0, {cMagnitude} > 0]";

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var e1 = processor.VectorTerm(0);
        var e2 = processor.VectorTerm(1);
        var e3 = processor.VectorTerm(2);

        var u = processor.Vector(
            aMagnitude * aAngle.Cos(),
            bMagnitude * bAngle.Cos(),
            cMagnitude * cAngle.Cos()
        );
        var uNorm = u.ENorm();
        var uUnit = u / uNorm;

        Console.WriteLine($@"\boldsymbol{{u}}\left(t\right) & = & {latexComposer.GetMultivectorText(u)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{u}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(uNorm)}");
        Console.WriteLine();

        var v1 = aMagnitude / nSqrt * processor.Vector($"Cos[{aAngle}]", $"Cos[{aAngle}]", $"Cos[{aAngle}]");
        var v2 = bMagnitude / nSqrt * processor.Vector($"Cos[{bAngle}]", $"Cos[{bAngle} + 2 * Pi / 3]", $"Cos[{bAngle} - 2 * Pi / 3]");
        var v3 = cMagnitude / nSqrt * processor.Vector($"Cos[{cAngle}]", $"Cos[{cAngle} - 2 * Pi / 3]", $"Cos[{cAngle} + 2 * Pi / 3]");

        var v1Norm = v1.Norm().FullSimplifyScalar();
        var v2Norm = v2.Norm().FullSimplifyScalar();
        var v3Norm = v3.Norm().FullSimplifyScalar();

        Console.WriteLine($@"\boldsymbol{{v}}_{{1}}\left(t\right) & = & {latexComposer.GetMultivectorText(v1)} \\");
        Console.WriteLine($@"\boldsymbol{{v}}_{{2}}\left(t\right) & = & {latexComposer.GetMultivectorText(v2)} \\");
        Console.WriteLine($@"\boldsymbol{{v}}_{{3}}\left(t\right) & = & {latexComposer.GetMultivectorText(v3)}");
        Console.WriteLine();

        Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{1}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(v1Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{2}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(v2Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{3}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(v3Norm)}");
        Console.WriteLine();

        var k =
            processor.VectorSymmetric(3).DivideByENorm();

        var skr =
            k.CreatePureRotorToAxis(LinBasisVector.Pz, true);

        var r2 = e2.Op(e1).GetEuclideanPureRotor(
            @$"2 * Pi / 24".RadiansToPolarAngle(processor.ScalarProcessor)
        );

        var rv1 = r2.OmMap(skr.OmMap(v1)).FullSimplifyScalars();
        var rv2 = r2.OmMap(skr.OmMap(v2)).FullSimplifyScalars();
        var rv3 = r2.OmMap(skr.OmMap(v3)).FullSimplifyScalars();

        var rv1Norm = rv1.Norm().FullSimplifyScalar();
        var rv2Norm = rv2.Norm().FullSimplifyScalar();
        var rv3Norm = rv3.Norm().FullSimplifyScalar();

        Console.WriteLine($@"\boldsymbol{{R}}_{{3}}\boldsymbol{{v}}_{{1}}\left(t\right)\boldsymbol{{R}}_{{3}}^{{\dagger}} & = & {latexComposer.GetMultivectorText(rv1)} \\");
        Console.WriteLine($@"\boldsymbol{{R}}_{{3}}\boldsymbol{{v}}_{{2}}\left(t\right)\boldsymbol{{R}}_{{3}}^{{\dagger}} & = & {latexComposer.GetMultivectorText(rv2)} \\");
        Console.WriteLine($@"\boldsymbol{{R}}_{{3}}\boldsymbol{{v}}_{{3}}\left(t\right)\boldsymbol{{R}}_{{3}}^{{\dagger}} & = & {latexComposer.GetMultivectorText(rv3)}");
        Console.WriteLine();

        Console.WriteLine($@"\left\Vert \boldsymbol{{R}}_{{3}}\boldsymbol{{v}}_{{1}}\left(t\right)\boldsymbol{{R}}_{{3}}^{{\dagger}} \right\Vert & = & {latexComposer.GetScalarText(rv1Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{R}}_{{3}}\boldsymbol{{v}}_{{1}}\left(t\right)\boldsymbol{{R}}_{{3}}^{{\dagger}} \right\Vert & = & {latexComposer.GetScalarText(rv2Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{R}}_{{3}}\boldsymbol{{v}}_{{1}}\left(t\right)\boldsymbol{{R}}_{{3}}^{{\dagger}} \right\Vert & = & {latexComposer.GetScalarText(rv3Norm)}");
        Console.WriteLine();

        var m2 = u.GetEuclideanPureRotorTo(rv2, false).Multivector;//.GetVectorOmMappingMatrix().ToArray();

        Console.WriteLine(@$"\\boldsymbol{{M}}_{{2}} = {latexComposer.GetMultivectorText(m2)}");
        Console.WriteLine();

        //var w0 = Expr.INT_ZERO;
        //var w1 = "2 * Pi * 1 / 3".ToExpr();
        //var w2 = "2 * Pi * 2 / 3".ToExpr();

        //var v1 = 
        //    processor.CreateXGaPhasor(aNorm, aAngle + w0, 0) +
        //    processor.CreateXGaPhasor(bNorm, bAngle + w0, 1) +
        //    processor.CreateXGaPhasor(cNorm, cAngle + w0, 2);

        //var v2 = 
        //    processor.CreateXGaPhasor(aNorm, aAngle + w0, 0) +
        //    processor.CreateXGaPhasor(bNorm, bAngle + w1, 1) +
        //    processor.CreateXGaPhasor(cNorm, cAngle + w2, 2);

        //var v3 = 
        //    processor.CreateXGaPhasor(aNorm, aAngle + w0, 0) +
        //    processor.CreateXGaPhasor(bNorm, bAngle + w2, 1) +
        //    processor.CreateXGaPhasor(cNorm, cAngle + w1, 2);

        //Console.WriteLine($@"\boldsymbol{{v}}_{{1}}\left(t\right) & = & {latexComposer.GetMultivectorText(v1)} \\");
        //Console.WriteLine($@"\boldsymbol{{v}}_{{2}}\left(t\right) & = & {latexComposer.GetMultivectorText(v2)} \\");
        //Console.WriteLine($@"\boldsymbol{{v}}_{{3}}\left(t\right) & = & {latexComposer.GetMultivectorText(v3)}");
        //Console.WriteLine();
    }


    //public static IGaOutermorphism<T> CreateClarkePhasorMap<T>(this IGeometricAlgebraEuclideanProcessor<T> processor)
    //{
    //    Debug.Assert(
    //        processor.VSpaceDimensions.IsEven()
    //    );

    //    var phasorCount = (int)processor.VSpaceDimensions / 2;

    //    var clarkeArray =
    //        processor.CreateClarkeArray(phasorCount);

    //    var clarkePhasorMapArray =
    //        processor.CreateArrayZero2D(phasorCount * 2);

    //    for (var i = 0; i < phasorCount; i++)
    //        for (var j = 0; j < phasorCount; j++)
    //        {
    //            clarkePhasorMapArray[i, j] = clarkeArray[i, j];
    //            clarkePhasorMapArray[i + phasorCount, j + phasorCount] = clarkeArray[i, j];
    //        }

    //    var basisVectorImagesDictionary =
    //        new Dictionary<ulong, VectorStorage<T>>();

    //    for (var i = 0; i < phasorCount * 2; i++)
    //        basisVectorImagesDictionary.Add(
    //            (ulong)i,
    //            clarkePhasorMapArray.ColumnToVectorStorage(i, processor)
    //        );

    //    return processor.CreateLinearMapOutermorphism(
    //        //(uint) vectorsCount,
    //        basisVectorImagesDictionary
    //    );
    //}

    public static IXGaOutermorphism<T> CreateClarkePhasorMap<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        Debug.Assert(
            vSpaceDimensions.IsEven()
        );

        var scalarProcessor = processor.ScalarProcessor;

        var phasorCount = vSpaceDimensions / 2;

        var clarkeArray =
            scalarProcessor.CreateClarkeRotationArray(phasorCount);

        var clarkePhasorMapArray =
            scalarProcessor.CreateArrayZero2D(phasorCount * 2);

        for (var i = 0; i < phasorCount; i++)
            for (var j = 0; j < phasorCount; j++)
            {
                clarkePhasorMapArray[i, j] = clarkeArray[i, j];
                clarkePhasorMapArray[i + phasorCount, j + phasorCount] = clarkeArray[i, j];
            }

        var basisVectorImagesDictionary =
            new Dictionary<int, LinVector<T>>();

        for (var i = 0; i < phasorCount * 2; i++)
            basisVectorImagesDictionary.Add(
                i,
                clarkePhasorMapArray.ColumnToLinVector(scalarProcessor, i)
            );

        return scalarProcessor.CreateLinUnilinearMap(
            basisVectorImagesDictionary
        ).ToOutermorphism(processor);
    }


    public static void SymbolicSymmetricalComponentsSample2()
    {
        var n = 8;

        var scalarProcessor =
            ScalarProcessorOfWolframExpr.Instance;

        var processor =
            scalarProcessor.CreateEuclideanXGaProcessor();

        var latexComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

        latexComposer.BasisName = @"\boldsymbol{\mu}";

        var nSqrt = $"Sqrt[{n}]".ScalarFromText(scalarProcessor);
        var nHalfSqrt = $"Sqrt[{n / 2}]".ScalarFromText(scalarProcessor);
        var sAngle = @$"2 * Pi / {n / 2}".ScalarFromText(scalarProcessor).RadiansToPolarAngle();
        var omega = @"\[Omega]".ScalarFromText(scalarProcessor);
        var t = @"t".ScalarFromText(scalarProcessor);
        var wt = omega * t;

        var u1Magnitude = @$"Subscript[U,1]".ScalarFromText(scalarProcessor);
        var u2Magnitude = @$"Subscript[U,2]".ScalarFromText(scalarProcessor);
        var u3Magnitude = @$"Subscript[U,3]".ScalarFromText(scalarProcessor);
        var u4Magnitude = @$"Subscript[U,4]".ScalarFromText(scalarProcessor);

        var u1Angle = @$"{wt} + Subscript[\[Theta],1]".ScalarFromText(scalarProcessor).RadiansToPolarAngle();
        var u2Angle = @$"{wt} + Subscript[\[Theta],2]".ScalarFromText(scalarProcessor).RadiansToPolarAngle();
        var u3Angle = @$"{wt} + Subscript[\[Theta],3]".ScalarFromText(scalarProcessor).RadiansToPolarAngle();
        var u4Angle = @$"{wt} + Subscript[\[Theta],4]".ScalarFromText(scalarProcessor).RadiansToPolarAngle();

        var assumeExpr1 =
            @$"Element[{omega} | {t} | {u1Magnitude} | {u2Magnitude} | {u3Magnitude} | {u4Magnitude} | {u1Angle} | {u2Angle} | {u3Angle} | {u4Angle}, Reals]";

        var assumeExpr =
            $@"And[{assumeExpr1}, {omega} > 0, {u1Magnitude} > 0, {u2Magnitude} > 0, {u3Magnitude} > 0, {u4Magnitude} > 0]";

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        //var e1 = processor.Vector(0);
        //var e2 = processor.Vector(1);
        //var e3 = processor.Vector(2);
        //var e4 = processor.Vector(3);
        //var e5 = processor.Vector(4);
        //var e6 = processor.Vector(5);
        //var e7 = processor.Vector(6);
        //var e8 = processor.Vector(7);

        var u1 = processor.VectorPhasor(u1Magnitude, u1Angle, 0, 4);
        var u2 = processor.VectorPhasor(u2Magnitude, u2Angle, 1, 5);
        var u3 = processor.VectorPhasor(u3Magnitude, u3Angle, 2, 6);
        var u4 = processor.VectorPhasor(u4Magnitude, u4Angle, 3, 7);

        Console.WriteLine($@"\boldsymbol{{u}}_{{1}}\left(t\right) & = & {latexComposer.GetMultivectorText(u1)} \\");
        Console.WriteLine($@"\boldsymbol{{u}}_{{2}}\left(t\right) & = & {latexComposer.GetMultivectorText(u2)} \\");
        Console.WriteLine($@"\boldsymbol{{u}}_{{3}}\left(t\right) & = & {latexComposer.GetMultivectorText(u3)} \\");
        Console.WriteLine($@"\boldsymbol{{u}}_{{4}}\left(t\right) & = & {latexComposer.GetMultivectorText(u4)}");
        Console.WriteLine();

        var u = u1 + u2 + u3 + u4;
        var uNorm = u.ENorm();

        Console.WriteLine($@"\boldsymbol{{u}}\left(t\right) & = & {latexComposer.GetMultivectorText(u)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{u}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(uNorm)}");
        Console.WriteLine();

        var v1 =
            processor.VectorPhasor(u1Magnitude / nHalfSqrt, u1Angle + 0 * sAngle, 0, 4) +
            processor.VectorPhasor(u1Magnitude / nHalfSqrt, u1Angle + 0 * sAngle, 1, 5) +
            processor.VectorPhasor(u1Magnitude / nHalfSqrt, u1Angle + 0 * sAngle, 2, 6) +
            processor.VectorPhasor(u1Magnitude / nHalfSqrt, u1Angle + 0 * sAngle, 3, 7);

        var v2 =
            processor.VectorPhasor(u2Magnitude / nHalfSqrt, u2Angle + 0 * sAngle, 0, 4) +
            processor.VectorPhasor(u2Magnitude / nHalfSqrt, u2Angle + 1 * sAngle, 1, 5) +
            processor.VectorPhasor(u2Magnitude / nHalfSqrt, u2Angle + 2 * sAngle, 2, 6) +
            processor.VectorPhasor(u2Magnitude / nHalfSqrt, u2Angle + 3 * sAngle, 3, 7);

        var v3 =
            processor.VectorPhasor(u3Magnitude / nHalfSqrt, u3Angle + 0 * sAngle, 0, 4) +
            processor.VectorPhasor(u3Magnitude / nHalfSqrt, u3Angle + 2 * sAngle, 1, 5) +
            processor.VectorPhasor(u3Magnitude / nHalfSqrt, u3Angle + 4 * sAngle, 2, 6) +
            processor.VectorPhasor(u3Magnitude / nHalfSqrt, u3Angle + 6 * sAngle, 3, 7);

        var v4 =
            processor.VectorPhasor(u4Magnitude / nHalfSqrt, u4Angle + 0 * sAngle, 0, 4) +
            processor.VectorPhasor(u4Magnitude / nHalfSqrt, u4Angle + 3 * sAngle, 1, 5) +
            processor.VectorPhasor(u4Magnitude / nHalfSqrt, u4Angle + 6 * sAngle, 2, 6) +
            processor.VectorPhasor(u4Magnitude / nHalfSqrt, u4Angle + 9 * sAngle, 3, 7);

        var v1Norm = v1.Norm().FullSimplifyScalar();
        var v2Norm = v2.Norm().FullSimplifyScalar();
        var v3Norm = v3.Norm().FullSimplifyScalar();
        var v4Norm = v4.Norm().FullSimplifyScalar();

        Console.WriteLine($@"\boldsymbol{{v}}_{{1}}\left(t\right) & = & {latexComposer.GetMultivectorText(v1)} \\");
        Console.WriteLine($@"\boldsymbol{{v}}_{{2}}\left(t\right) & = & {latexComposer.GetMultivectorText(v2)} \\");
        Console.WriteLine($@"\boldsymbol{{v}}_{{3}}\left(t\right) & = & {latexComposer.GetMultivectorText(v3)} \\");
        Console.WriteLine($@"\boldsymbol{{v}}_{{4}}\left(t\right) & = & {latexComposer.GetMultivectorText(v4)}");
        Console.WriteLine();

        Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{1}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(v1Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{2}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(v2Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{3}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(v3Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{4}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(v4Norm)}");
        Console.WriteLine();

        //var k1 = 
        //    processor.Vector(1, 1, 1, 0, 0, 0).DivideByENorm();

        //var k2 = 
        //    processor.Vector(0, 0, 0, 1, 1, 1).DivideByENorm();

        //var skr1 =
        //    k1.CreatePureRotorToAxis(new Axis(2), true);

        //var skr2 =
        //    k2.CreatePureRotorToAxis(new Axis(5), true);

        //var srAngle = @$"2 * Pi / 24".CreateScalar(processor);

        //var sr1 = processor.CreatePureRotor(
        //    srAngle,
        //    e2.Op(e1)
        //);

        //var sr2 = processor.CreatePureRotor(
        //    srAngle,
        //    e5.Op(e4)
        //);

        //var rv1 = sr2.OmMap(skr2.OmMap(sr1.OmMap(skr1.OmMap(v1)))).FullSimplifyScalars();
        //var rv2 = sr2.OmMap(skr2.OmMap(sr1.OmMap(skr1.OmMap(v2)))).FullSimplifyScalars();
        //var rv3 = sr2.OmMap(skr2.OmMap(sr1.OmMap(skr1.OmMap(v3)))).FullSimplifyScalars();

        var clarkeMap =
            processor.CreateClarkePhasorMap(n);

        var clarkeMatrix =
            clarkeMap.GetVectorMapArray(n);

        Console.WriteLine(@$"$\boldsymbol{{C}} = {latexComposer.GetArrayText(clarkeMatrix)}$");
        Console.WriteLine();

        var rv1 = clarkeMap.OmMap(v1);
        var rv2 = clarkeMap.OmMap(v2);
        var rv3 = clarkeMap.OmMap(v3);
        var rv4 = clarkeMap.OmMap(v4);

        var rv1Norm = rv1.Norm().FullSimplifyScalar();
        var rv2Norm = rv2.Norm().FullSimplifyScalar();
        var rv3Norm = rv3.Norm().FullSimplifyScalar();
        var rv4Norm = rv4.Norm().FullSimplifyScalar();

        Console.WriteLine($@"\boldsymbol{{C}}\boldsymbol{{v}}_{{1}}\left(t\right) & = & {latexComposer.GetMultivectorText(rv1)} \\");
        Console.WriteLine($@"\boldsymbol{{C}}\boldsymbol{{v}}_{{2}}\left(t\right) & = & {latexComposer.GetMultivectorText(rv2)} \\");
        Console.WriteLine($@"\boldsymbol{{C}}\boldsymbol{{v}}_{{3}}\left(t\right) & = & {latexComposer.GetMultivectorText(rv3)} \\");
        Console.WriteLine($@"\boldsymbol{{C}}\boldsymbol{{v}}_{{4}}\left(t\right) & = & {latexComposer.GetMultivectorText(rv4)}");
        Console.WriteLine();

        Console.WriteLine($@"\left\Vert \boldsymbol{{C}}\boldsymbol{{v}}_{{1}}\left(t\right) \right\Vert & = & {latexComposer.GetScalarText(rv1Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{C}}\boldsymbol{{v}}_{{2}}\left(t\right) \right\Vert & = & {latexComposer.GetScalarText(rv2Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{C}}\boldsymbol{{v}}_{{3}}\left(t\right) \right\Vert & = & {latexComposer.GetScalarText(rv3Norm)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{C}}\boldsymbol{{v}}_{{4}}\left(t\right) \right\Vert & = & {latexComposer.GetScalarText(rv4Norm)}");
        Console.WriteLine();

        //var p13 = processor.CreatePureRotorSequence(
        //    e1, e4,
        //    e3, e6,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var p21 = processor.CreatePureRotorSequence(
        //    e2, e5,
        //    e1, e4,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var n25 = processor.CreatePureRotorSequence(
        //    e2, e5,
        //    e5, -e2,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var p31 = processor.CreatePureRotorSequence(
        //    e3, e6,
        //    e1, e4,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var n35 = processor.CreatePureRotorSequence(
        //    e3, e6,
        //    e5, -e2,
        //    true
        //).GetVectorOmMappingMatrix().ToArray().ArrayToMatrixExprScalar(processor);

        //var t1 = p13;//.ScalarValue.MatrixExprToArray();
        //var t2 = (p21 + n25);//.ScalarValue.MatrixExprToArray();
        //var t3 = (p31 - n35);//.ScalarValue.MatrixExprToArray();

        //Console.WriteLine($@"\boldsymbol{{T}}_{{1}} = {latexComposer.GetArrayText(t1)}");
        //Console.WriteLine($@"\boldsymbol{{T}}_{{2}} = {latexComposer.GetArrayText(t2)}");
        //Console.WriteLine($@"\boldsymbol{{T}}_{{3}} = {latexComposer.GetArrayText(t3)}");
        //Console.WriteLine();
    }

    public static T[,] GetPMatrix<T>(this XGaProcessor<T> processor, int vSpaceDimensions, int a, int b, int c, int d)
    {
        var scalarProcessor = processor.ScalarProcessor;

        var ea = processor.VectorTerm(a);
        var eb = processor.VectorTerm(b);
        var ec = processor.VectorTerm(c);
        var ed = processor.VectorTerm(d);

        var pArray = ea.CreatePureRotorSequence(
            eb,
            ec,
            ed,
            true
        ).GetVectorMapArray(vSpaceDimensions, vSpaceDimensions);

        for (var j = 0; j < pArray.GetLength(1); j++)
        {
            if (j == a || j == b)
                continue;

            for (var i = 0; i < pArray.GetLength(0); i++)
                pArray[i, j] = scalarProcessor.ZeroValue;
        }

        return pArray;
    }

    public static T[,] GetNMatrix<T>(this XGaProcessor<T> processor, int vSpaceDimensions, int a, int b, int c, int d)
    {
        var scalarProcessor = processor.ScalarProcessor;

        var ea = processor.VectorTerm(a);
        var eb = processor.VectorTerm(b);
        var ec = processor.VectorTerm(c);
        var ed = processor.VectorTerm(d);

        var nArray = ea.CreatePureRotorSequence(
            eb,
            ec,
            -ed,
            true
        ).GetVectorMapArray(vSpaceDimensions, vSpaceDimensions);

        for (var j = 0; j < nArray.GetLength(1); j++)
        {
            if (j == a || j == b)
                continue;

            for (var i = 0; i < nArray.GetLength(0); i++)
                nArray[i, j] = scalarProcessor.ZeroValue;
        }

        return nArray;
    }

    public static T[,] GetTMatrix<T>(this XGaProcessor<T> processor, int vSpaceDimensions, int k)
    {
        var n = vSpaceDimensions / 2;

        if (k == 0)
            return processor.GetPMatrix(vSpaceDimensions, 0, n, n - 1, 2 * n - 1);

        if (n.IsEven() && k == n / 2)
            return processor.GetPMatrix(vSpaceDimensions, n / 2, n / 2 + n, n - 2, 2 * n - 2);

        var a = k;
        var b = k + n;
        var cp = 0;
        var dp = 0;
        var cn = 0;
        var dn = 0;
        var nNegative = false;

        if (n.IsOdd())
        {
            if (k <= (n - 1) / 2)
            {
                k = k - 1;

                cn = k * 2;
                dn = k * 2 + 1;

                cp = dn + n;
                dp = cn + n;
            }
            else
            {
                k = n - k - 1;
                cp = k * 2;
                dp = k * 2 + 1;

                cn = dp + n;
                dn = cp + n;

                nNegative = true;
            }
        }
        else
        {
            if (k < n / 2)
            {
                k = k - 1;

                cn = k * 2;
                dn = k * 2 + 1;

                cp = dn + n;
                dp = cn + n;
            }
            else
            {
                k = n - k - 1;
                cp = k * 2;
                dp = k * 2 + 1;

                cn = dp + n;
                dn = cp + n;

                nNegative = true;
            }
        }

        var pMatrix = processor.GetPMatrix(vSpaceDimensions, a, b, cp, dp);
        var nMatrix = processor.GetNMatrix(vSpaceDimensions, a, b, cn, dn);

        var scalarProcessor = processor.ScalarProcessor;

        var sqrt2 = scalarProcessor.Sqrt(scalarProcessor.TwoValue).ScalarValue;

        var tMatrix = nNegative
            ? scalarProcessor.Subtract(pMatrix, nMatrix)
            : scalarProcessor.Add(pMatrix, nMatrix);

        return scalarProcessor.Divide(tMatrix, sqrt2);
    }

    public static void SymbolicSymmetricalComponentsSample(int phaseCount)
    {
        var n = 2 * phaseCount;

        var scalarProcessor =
            ScalarProcessorOfWolframExpr.Instance;

        var processor =
            scalarProcessor.CreateEuclideanXGaProcessor();

        var latexComposer =
            LaTeXComposerOfWolframExpr.DefaultComposer;

        latexComposer.BasisName = @"\boldsymbol{\mu}";

        var phaseCountSqrt = $"Sqrt[{phaseCount}]".ScalarFromText(scalarProcessor);
        var sAngle = @$"2 * Pi / {phaseCount}".ScalarFromText(scalarProcessor).RadiansToPolarAngle();
        var omega = @"\[Omega]".ScalarFromText(scalarProcessor);
        var t = @"t".ScalarFromText(scalarProcessor);
        var wt = omega * t;

        var ukMagnitudes =
            phaseCount
                .GetRange()
                .Select(i => @$"Subscript[U,{i + 1}]".ScalarFromText(scalarProcessor))
                .ToImmutableArray();

        var ukAngles =
            phaseCount
                .GetRange()
                .Select(i => @$"{wt} + Subscript[\[Theta],{i + 1}]".ScalarFromText(scalarProcessor).RadiansToPolarAngle())
                .ToImmutableArray();

        var assumeExpr1 =
            ukMagnitudes.Select(s => s.ToString()).Concatenate(" | ");

        var assumeExpr2 =
            ukAngles.Select(s => s.ToString()).Concatenate(" | ");

        var assumeExpr3 =
            @$"Element[{omega} | {t} | {assumeExpr1} | {assumeExpr2}, Reals]";

        var assumeExpr4 =
            ukMagnitudes.Select(s => s.ToString() + " > 0").Concatenate(", ");

        var assumeExpr =
            $@"And[{assumeExpr3}, {omega} > 0, {assumeExpr4}]";

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var ukPhasors =
            phaseCount
                .GetRange()
                .Select(k =>
                    processor.VectorPhasor(
                        ukMagnitudes[k],
                        ukAngles[k],
                        k,
                        k + phaseCount
                    )
                ).ToImmutableArray();

        var u = processor.VectorZero;

        for (var k = 0; k < phaseCount; k++)
        {
            u += ukPhasors[k];

            var newLine = k < phaseCount - 1 ? @" \\" : string.Empty;
            Console.WriteLine($@"\boldsymbol{{u}}_{{{k + 1}}}\left(t\right) & = & {latexComposer.GetMultivectorText(ukPhasors[k])}{newLine}");
        }

        Console.WriteLine();

        var uNorm = u.ENorm();

        Console.WriteLine($@"\boldsymbol{{u}}\left(t\right) & = & {latexComposer.GetMultivectorText(u)} \\");
        Console.WriteLine($@"\left\Vert \boldsymbol{{u}}\left(t\right)\right\Vert  & = & {latexComposer.GetScalarText(uNorm)}");
        Console.WriteLine();

        var vkPhasors = new XGaVector<Expr>[phaseCount];

        for (var k = 0; k < phaseCount; k++)
        {
            var vk = processor.VectorZero;

            var magnitude = ukMagnitudes[k] / phaseCountSqrt;

            for (var j = 0; j < phaseCount; j++)
            {
                var angle = ukAngles[k] + j * k * sAngle;

                vk += processor.VectorPhasor(magnitude, angle, j, j + phaseCount);
            }

            vkPhasors[k] = vk.FullSimplifyScalars();

            var newLine = k < phaseCount - 1 ? @" \\" : string.Empty;
            Console.WriteLine($@"\boldsymbol{{v}}_{{{k + 1}}}\left(t\right) & = & {latexComposer.GetMultivectorText(vkPhasors[k])}{newLine}");
        }

        Console.WriteLine();

        var vkPhasorNorms =
            vkPhasors
                .Select(v => v.ENorm().FullSimplifyScalar())
                .ToImmutableArray();

        for (var k = 0; k < phaseCount; k++)
        {
            var newLine = k < phaseCount - 1 ? @" \\" : string.Empty;
            Console.WriteLine($@"\left\Vert \boldsymbol{{v}}_{{{k + 1}}}\left(t\right) \right\Vert  & = & {latexComposer.GetScalarText(vkPhasorNorms[k])}{newLine}");
        }

        Console.WriteLine();

        var clarkeMap =
            processor.CreateClarkePhasorMap(n);

        var clarkeMatrix =
            clarkeMap.GetVectorMapArray(n);

        Console.WriteLine(@$"$\boldsymbol{{C}} = {latexComposer.GetArrayText(clarkeMatrix)}$");
        Console.WriteLine();

        var wkPhasors =
            vkPhasors
                .Select(v => clarkeMap.OmMap(v).FullSimplifyScalars())
                .ToImmutableArray();

        var wkPhasorNorms =
            wkPhasors
                .Select(v => v.ENorm().FullSimplifyScalar())
                .ToImmutableArray();

        for (var k = 0; k < phaseCount; k++)
        {
            var newLine = k < phaseCount - 1 ? @" \\" : string.Empty;
            Console.WriteLine($@"\boldsymbol{{w}}_{{{k + 1}}}\left(t\right)  & = & {latexComposer.GetMultivectorText(wkPhasors[k])}{newLine}");
        }

        Console.WriteLine();

        for (var k = 0; k < phaseCount; k++)
        {
            var newLine = k < phaseCount - 1 ? @" \\" : string.Empty;
            Console.WriteLine($@"\left\Vert \boldsymbol{{w}}_{{{k + 1}}}\left(t\right) \right\Vert  & = & {latexComposer.GetScalarText(wkPhasorNorms[k])}{newLine}");
        }

        Console.WriteLine();

        var tMatrices =
            phaseCount
                .GetRange()
                .Select(k => processor.GetTMatrix(n, k))
                .ToImmutableArray();

        for (var k = 0; k < phaseCount; k++)
        {
            Console.WriteLine($@"$\boldsymbol{{T}}_{{{k + 1}}} = {latexComposer.GetArrayText(tMatrices[k])}$");
            Console.WriteLine();
        }

        //var p13 = processor.CreatePureRotorSequence(
        //    e1, e4,
        //    e3, e6,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var p21 = processor.CreatePureRotorSequence(
        //    e2, e5,
        //    e1, e4,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var n25 = processor.CreatePureRotorSequence(
        //    e2, e5,
        //    e5, -e2,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var p31 = processor.CreatePureRotorSequence(
        //    e3, e6,
        //    e1, e4,
        //    true
        //).GetVectorOmMappingMatrix();//.ToArray().ArrayToMatrixExprScalar(processor);

        //var n35 = processor.CreatePureRotorSequence(
        //    e3, e6,
        //    e5, -e2,
        //    true
        //).GetVectorOmMappingMatrix().ToArray().ArrayToMatrixExprScalar(processor);

        //var t1 = p13;//.ScalarValue.MatrixExprToArray();
        //var t2 = (p21 + n25);//.ScalarValue.MatrixExprToArray();
        //var t3 = (p31 - n35);//.ScalarValue.MatrixExprToArray();

        //Console.WriteLine($@"\boldsymbol{{T}}_{{1}} = {latexComposer.GetArrayText(t1)}");
        //Console.WriteLine($@"\boldsymbol{{T}}_{{2}} = {latexComposer.GetArrayText(t2)}");
        //Console.WriteLine($@"\boldsymbol{{T}}_{{3}} = {latexComposer.GetArrayText(t3)}");
        //Console.WriteLine();
    }

    public static void EigenDecompositionSample()
    {
        const int n = 9;

        var metric = XGaFloat64Processor.Euclidean;
        var matrix = n.CreateUnitaryDftMatrix();
        var matrixInv = n.CreateUnitaryDftMatrix(true);

        Debug.Assert(
            (matrixInv * matrix - n.CreateDenseIdentityMatrix().ToComplex()).L2Norm().IsNearZero()
        );

        var eigenPairList =
            matrix
                .GetComplexEigenPairs()
                .OrderByDescending(p => p.Item1.Magnitude);

        foreach (var (value, vector) in eigenPairList)
        {
            var angle = value.GetPhaseAsPolarAngle();
            var blade = metric.Vector(
                vector.Imaginary().ToArray().MapItems(d => d.Round(6))).Op(
                metric.Vector(vector.Real().ToArray().MapItems(d => d.Round(6)))
            );

            blade = blade.Divide(blade.ENorm().ScalarValue);

            //Console.WriteLine($" Eigen Value {i}: {value}");
            //Console.WriteLine($"Eigen Vector {i}: {vector}");
            Console.WriteLine($"           Angle: {angle}");
            Console.WriteLine($"     Eigen Blade: {blade}");
            Console.WriteLine();
        }
    }
}