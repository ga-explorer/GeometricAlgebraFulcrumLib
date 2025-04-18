﻿using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

public class GrPovRayIsoSurfaceBoxContainer :
    GrPovRayIsoSurfaceContainer
{
    public GrPovRayVector3Value Corner1 { get; }

    public GrPovRayVector3Value Corner2 { get; }


    internal GrPovRayIsoSurfaceBoxContainer(GrPovRayVector3Value corner1, GrPovRayVector3Value corner2)
    {
        Corner1 = corner1;
        Corner2 = corner2;
    }


    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("box {")
            .IncreaseIndentation()
            .AppendAtNewLine(
                Corner1.GetAttributeValueCode() + ", " + 
                Corner2.GetAttributeValueCode()
            )
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}