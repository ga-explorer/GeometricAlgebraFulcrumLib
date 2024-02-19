using DataStructuresLib.Extensions;

namespace WebComposerLib.LaTeX.KaTeX.Expressions;

public sealed class WclKaTeXArrayArgFunction : IWclKaTeXExpression
{
    private readonly IWclKaTeXExpression[,] _argsArray;


    public string TexCodeTemplate { get; }

    public string ItemsSeparator { get; set; } 
        = @" & ";

    public string RowsSeparator { get; set; } 
        = @"//" + Environment.NewLine;

    public int RowsCount 
        => _argsArray.GetLength(0);

    public int ColumnsCount 
        => _argsArray.GetLength(1);



    public IWclKaTeXExpression this[int rowIndex, int colIndex]
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

    public IEnumerable<IWclKaTeXExpression> ChildExpressions
        => _argsArray.GetItems();


    internal WclKaTeXArrayArgFunction(string texCodeTemplate, int rows, int cols)
    {
        _argsArray = new IWclKaTeXExpression[rows, cols];
        TexCodeTemplate = texCodeTemplate;
    }


    public WclKaTeXArrayArgFunction ClearArguments()
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