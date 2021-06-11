﻿using System;
using System.Collections.Generic;
using DataStructuresLib.Extensions;

namespace CodeComposerLib.KaTeX.Expressions
{
    public sealed class KaTeXArrayArgFunction : IKaTeXExpression
    {
        private readonly IKaTeXExpression[,] _argsArray;


        public string TexCodeTemplate { get; }

        public string ItemsSeparator { get; set; } 
            = @" & ";

        public string RowsSeparator { get; set; } 
            = @"//" + Environment.NewLine;

        public int RowsCount 
            => _argsArray.GetLength(0);

        public int ColumnsCount 
            => _argsArray.GetLength(1);



        public IKaTeXExpression this[int rowIndex, int colIndex]
        {
            get
            {
                if (rowIndex < 0 || rowIndex >= RowsCount || colIndex < 0 || colIndex >= ColumnsCount) 
                    throw new IndexOutOfRangeException();

                return _argsArray[rowIndex, colIndex];
            }
            set
            {
                if (rowIndex < 0 || rowIndex >= RowsCount || colIndex < 0 || colIndex >= ColumnsCount) 
                    throw new IndexOutOfRangeException();

                _argsArray[rowIndex, colIndex] = value;
            }
        }

        public string TexCode
            => TexCodeTemplate;
            //=> TexCodeTemplate.Replace(
            //    "texArg1", 
            //    Argument1?.TexCode ?? string.Empty
            //).Replace(
            //    "texArg2",
            //    Argument2?.TexCode ?? string.Empty
            //); 

        public bool IsLeafExpression 
            => false;

        public bool IsFunctionExpression 
            => true;

        public int ChildExpressionsCount 
            => _argsArray.Length;

        public IEnumerable<IKaTeXExpression> ChildExpressions
            => _argsArray.GetItems();


        internal KaTeXArrayArgFunction(string texCodeTemplate, int rows, int cols)
        {
            _argsArray = new IKaTeXExpression[rows, cols];
            TexCodeTemplate = texCodeTemplate;
        }


        public KaTeXArrayArgFunction ClearArguments()
        {
            for (var i = 0; i < RowsCount; i++)
            for (var j = 0; j < ColumnsCount; j++)
                _argsArray[i, j] = null;

            return this;
        }


        public override string ToString()
        {
            return TexCode;
        }
    }
}