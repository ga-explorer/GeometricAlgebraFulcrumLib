﻿namespace WebComposerLib.ImageSharp.Processing.AutoCrop.Models;

public sealed class CropAnalysis : ICropAnalysis
{
    public Rectangle BoundingBox { get; set; }

    public Color Background { get; set; }

    public bool Success { get; set; }
}