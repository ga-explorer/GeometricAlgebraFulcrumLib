namespace WebComposerLib.ImageSharp.Processing.AutoCrop.Models;

public sealed class NullBorderAnalysis : IBorderAnalysis
{
    public bool Success => false;
    public float BucketRatio => 0;
    public Color Background => Color.Transparent;
    public int Colors => 0;
}