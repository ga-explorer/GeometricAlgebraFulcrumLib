// Copyright 2017-2018 Alexander Luzgarev

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.Matlab
{
    /// <summary>
    /// Matlab's character array.
    /// </summary>
    public interface ICharArray : IArrayOf<char>
    {
        /// <summary>
        /// Gets the contained string.
        /// </summary>
        string String { get; }
    }
}