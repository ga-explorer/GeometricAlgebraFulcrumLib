﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Curves;

public sealed class GrBabylonJsCatmullRomSpline :
    GrBabylonJsCurve3Base
{
    protected override string ConstructorName 
        => "BABYLON.Curve3.CreateCatmullRomSpline";

    public GrBabylonJsVector3ArrayValue Points { get; set; }

    public GrBabylonJsVector3Value PointNumber { get; set; }

    public GrBabylonJsBooleanValue Closed { get; set; }


    public GrBabylonJsCatmullRomSpline(string constName) 
        : base(constName)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Points.GetAttributeValueCode();
        yield return PointNumber.GetAttributeValueCode();

        if (Closed.IsNullOrEmpty()) yield break;
        yield return Closed.GetAttributeValueCode();
    }
}