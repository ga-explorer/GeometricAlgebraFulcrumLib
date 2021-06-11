using System;
using System.IO;

namespace TextComposerLib.Text.Tabular
{
    public class ArrayPrinter
    {
        #region Declarations

        static bool _isLeftAligned = false;

        const string CellLeftTop = "┌";
        const string CellRightTop = "┐";
        const string CellLeftBottom = "└";
        const string CellRightBottom = "┘";
        //const string CellHorizontalJointTop = "┬";
        //const string CellHorizontalJointbottom = "┴";
        const string CellVerticalJointLeft = "├";
        const string CellTJoint = "┼";
        const string CellVerticalJointRight = "┤";
        //const string CellHorizontalLine = "─";
        const string CellVerticalLine = "│";

        #endregion

        #region Private Methods

        private static int GetMaxCellWidth(string[,] arrValues)
        {
            var maxWidth = 1;

            for (var i = 0; i < arrValues.GetLength(0); i++)
            {
                for (var j = 0; j < arrValues.GetLength(1); j++)
                {
                    var length = arrValues[i, j].Length;
                    if (length > maxWidth)
                    {
                        maxWidth = length;
                    }
                }
            }

            return maxWidth;
        }

        private static string GetDataInTableFormat(string[,] arrValues)
        {
            var formattedString = string.Empty;

            if (arrValues == null)
                return formattedString;

            var dimension1Length = arrValues.GetLength(0);
            var dimension2Length = arrValues.GetLength(1);

            var maxCellWidth = GetMaxCellWidth(arrValues);
            var indentLength = (dimension2Length * maxCellWidth) + (dimension2Length - 1);
            //printing top line;
            formattedString = $"{CellLeftTop}{Indent(indentLength)}{CellRightTop}{Environment.NewLine}";

            for (var i = 0; i < dimension1Length; i++)
            {
                var lineWithValues = CellVerticalLine;
                var line = CellVerticalJointLeft;
                for (var j = 0; j < dimension2Length; j++)
                {
                    var value = (_isLeftAligned) ? arrValues[i, j].PadRight(maxCellWidth, ' ') : arrValues[i, j].PadLeft(maxCellWidth, ' ');
                    lineWithValues += $"{value}{CellVerticalLine}";
                    line += Indent(maxCellWidth);
                    if (j < (dimension2Length - 1))
                    {
                        line += CellTJoint;
                    }
                }
                line += CellVerticalJointRight;
                formattedString += $"{lineWithValues}{Environment.NewLine}";
                if (i < (dimension1Length - 1))
                {
                    formattedString += $"{line}{Environment.NewLine}";
                }
            }

            //printing bottom line
            formattedString += $"{CellLeftBottom}{Indent(indentLength)}{CellRightBottom}{Environment.NewLine}";
            return formattedString;
        }

        private static string Indent(int count)
        {
            return string.Empty.PadLeft(count, '─');
        }

        #endregion

        #region Public Methods

        public static void PrintToStream(string[,] arrValues, StreamWriter writer)
        {
            if (arrValues == null)
                return;

            writer?.Write(GetDataInTableFormat(arrValues));
        }

        public static void PrintToConsole(string[,] arrValues)
        {
            if (arrValues == null)
                return;

            Console.WriteLine(GetDataInTableFormat(arrValues));
        }

        #endregion

        //static void Main(string[] args)
        //{
        //    int value = 997;
        //    string[,] arrValues = new string[5, 5];
        //    for (int i = 0; i < arrValues.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < arrValues.GetLength(1); j++)
        //        {
        //            value++;
        //            arrValues[i, j] = value.ToString();
        //        }
        //    }
        //    ArrayPrinter.PrintToConsole(arrValues);
        //    Console.ReadLine();
        //}
    }
}
