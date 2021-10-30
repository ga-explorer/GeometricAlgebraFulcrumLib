namespace GraphicsComposerLib.GraphViz.Dot
{
    /// <summary>
    /// This abstract class represents a dot value stored internally as a string
    /// </summary>
    public abstract class DotStoredValue : DotValue
    {
        public override string Value { get; }


        protected DotStoredValue(string value)
        {
            Value = value;
        }
    }
}
