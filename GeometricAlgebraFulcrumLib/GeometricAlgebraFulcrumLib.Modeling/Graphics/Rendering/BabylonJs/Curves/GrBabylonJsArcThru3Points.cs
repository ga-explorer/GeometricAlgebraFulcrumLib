﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Curves;

public sealed class GrBabylonJsArcThru3Points :
    GrBabylonJsCurve3Base
{
    protected override string ConstructorName 
        => "BABYLON.Curve3.ArcThru3Points";

    public GrBabylonJsVector3Value Point1 { get; set; }

    public GrBabylonJsVector3Value Point2 { get; set; }

    public GrBabylonJsVector3Value Point3 { get; set; }

    public GrBabylonJsInt32Value Steps { get; set; }

    public GrBabylonJsBooleanValue Closed { get; set; }

    public GrBabylonJsBooleanValue FullCircle { get; set; }


    public GrBabylonJsArcThru3Points(string constName) 
        : base(constName)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Point1.GetAttributeValueCode();
        yield return Point2.GetAttributeValueCode();
        yield return Point3.GetAttributeValueCode();

        if (Steps.IsNullOrEmpty()) yield break;
        yield return Steps.GetAttributeValueCode();

        if (Closed.IsNullOrEmpty()) yield break;
        yield return Closed.GetAttributeValueCode();

        if (FullCircle.IsNullOrEmpty()) yield break;
        yield return FullCircle.GetAttributeValueCode();
    }
}