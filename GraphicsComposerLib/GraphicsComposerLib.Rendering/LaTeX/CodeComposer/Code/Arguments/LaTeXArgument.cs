using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Arguments
{
    /// <summary>
    /// This class represents a single argument to a LaTeX command; required or optional.
    /// </summary>
    public sealed class LaTeXArgument : ILaTeXCodeElement
    {
        /// <summary>
        /// Create a copy of the given argument
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static LaTeXArgument Create(LaTeXArgument arg)
        {
            return new LaTeXArgument(arg.IsOptional, arg.Value);
        }

        /// <summary>
        /// Create a required argument with null value
        /// </summary>
        /// <returns></returns>
        public static LaTeXArgument Create()
        {
            return new LaTeXArgument();
        }

        /// <summary>
        /// Create a required argument with the given value
        /// </summary>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static LaTeXArgument Create(ILaTeXCodeElement argValue)
        {
            return new LaTeXArgument(argValue);
        }

        /// <summary>
        /// Create a required or optional argument with null value
        /// </summary>
        /// <param name="isOptional"></param>
        /// <returns></returns>
        public static LaTeXArgument Create(bool isOptional)
        {
            return new LaTeXArgument(isOptional);
        }

        /// <summary>
        /// Create a required or optional argument with the given value
        /// </summary>
        /// <param name="isOptional"></param>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static LaTeXArgument Create(bool isOptional, ILaTeXCodeElement argValue)
        {
            return new LaTeXArgument(isOptional, argValue);
        }


        /// <summary>
        /// The value assigned to this command argument
        /// </summary>
        public ILaTeXCodeElement Value { get; set; }

        /// <summary>
        /// Indicates if this argument is optional
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Indicates if this argument is required
        /// </summary>
        public bool IsRequired
        {
            get => !IsOptional;
            set => IsOptional = !value;
        }


        private LaTeXArgument()
        {
            Value = null;
            IsOptional = false;
        }

        private LaTeXArgument(ILaTeXCodeElement argValue)
        {
            Value = argValue;
            IsOptional = false;
        }

        private LaTeXArgument(bool isOptional)
        {
            Value = null;
            IsOptional = isOptional;
        }

        private LaTeXArgument(bool isOptional, ILaTeXCodeElement argValue)
        {
            Value = argValue;
            IsOptional = isOptional;
        }


        public LaTeXArgument Reset()
        {
            Value = null;
            IsOptional = false;

            return this;
        }

        public LaTeXArgument Reset(ILaTeXCodeElement argValue)
        {
            Value = argValue;
            IsOptional = false;

            return this;
        }

        public LaTeXArgument Reset(bool isOptional)
        {
            Value = null;
            IsOptional = isOptional;

            return this;
        }

        public LaTeXArgument Reset(bool isOptional, ILaTeXCodeElement argValue)
        {
            Value = argValue;
            IsOptional = isOptional;

            return this;
        }


        public bool IsEmpty()
        {
            return Value.IsNullOrEmpty();
        }

        public void ToText(LinearTextComposer composer)
        {
            composer
                .Append(IsOptional ? "[" : "{");

            if (!Value.IsNullOrEmpty())
                Value.ToText(composer);

            composer
                .Append(IsOptional ? "]" : "}");
        }

        public override string ToString()
        {
            var composer = new LinearTextComposer() {IndentationDefault = "  "};

            composer
                .Append(IsOptional ? "[" : "{");

            if (!Value.IsNullOrEmpty())
                Value.ToText(composer);

            composer
                .Append(IsOptional ? "]" : "}");

            return composer.ToString();
        }
    }
}
