﻿// Copyright 2017-2018 Alexander Luzgarev

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.Matlab
{
    /// <summary>
    /// Cell array.
    /// </summary>
    internal class MatCellArray : MatArray, ICellArray
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatCellArray"/> class.
        /// </summary>
        /// <param name="flags">Array properties.</param>
        /// <param name="dimensions">Dimensions of the array.</param>
        /// <param name="name">Array name.</param>
        /// <param name="elements">Array elements.</param>
        public MatCellArray(ArrayFlags flags, int[] dimensions, string name, IEnumerable<IArray> elements)
            : base(flags, dimensions, name)
        {
            Data = elements.ToArray();
        }

        /// <inheritdoc />
        public IArray[] Data { get; }

        /// <inheritdoc />
        public IArray this[params int[] indices]
        {
            get => Data[Dimensions.DimFlatten(indices)];
            set => Data[Dimensions.DimFlatten(indices)] = value;
        }
    }
}