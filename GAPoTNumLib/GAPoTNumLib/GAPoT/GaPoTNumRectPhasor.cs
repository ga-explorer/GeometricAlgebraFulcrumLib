using System;
using System.Collections.Generic;
using System.Diagnostics;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumRectPhasor
    {
        public static GaPoTNumRectPhasor operator -(GaPoTNumRectPhasor p)
        {
            return new GaPoTNumRectPhasor(
                p.Id,
                -p.XValue,
                -p.YValue
            );
        }

        public static GaPoTNumRectPhasor operator +(GaPoTNumRectPhasor p1, GaPoTNumRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTNumRectPhasor(
                p1.Id,
                p1.XValue + p2.XValue,
                p1.YValue + p2.YValue
            );
        }

        public static GaPoTNumRectPhasor operator -(GaPoTNumRectPhasor p1, GaPoTNumRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GaPoTNumRectPhasor(
                p1.Id,
                p1.XValue - p2.XValue,
                p1.YValue - p2.YValue
            );
        }

        public static GaPoTNumRectPhasor operator *(GaPoTNumRectPhasor p, double s)
        {
            return new GaPoTNumRectPhasor(
                p.Id,
                s * p.XValue,
                s * p.YValue
            );
        }

        public static GaPoTNumRectPhasor operator *(double s, GaPoTNumRectPhasor p)
        {
            return new GaPoTNumRectPhasor(
                p.Id,
                s * p.XValue,
                s * p.YValue
            );
        }

        public static GaPoTNumRectPhasor operator /(GaPoTNumRectPhasor p, double s)
        {
            s = 1.0d / s;

            return new GaPoTNumRectPhasor(
                p.Id,
                s * p.XValue,
                s * p.YValue
            );
        }


        public int Id { get; }

        public double XValue { get; set; }

        public double YValue { get; set; }


        internal GaPoTNumRectPhasor(int id, double x, double y)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            XValue = x;
            YValue = y;
        }

        internal GaPoTNumRectPhasor(int id, double x)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            XValue = x;
            YValue = 0;
        }

        
        public bool IsZero()
        {
            return XValue == 0 && YValue == 0;
        }

        public IEnumerable<GaPoTNumVectorTerm> GetTerms()
        {
            if (XValue != 0)
                yield return new GaPoTNumVectorTerm(
                    Id,
                    XValue
                );

            if (YValue != 0)
                yield return new GaPoTNumVectorTerm(
                    Id + 1,
                    -YValue
                );
        }

        public double Norm()
        {
            return Math.Sqrt(XValue * XValue + YValue * YValue);
        }

        public double Norm2()
        {
            return XValue * XValue + YValue * YValue;
        }

        public GaPoTNumPolarPhasor ToPolarPhasor()
        {
            return new GaPoTNumPolarPhasor(
                Id,
                Math.Sqrt(XValue * XValue + YValue * YValue),
                Math.Atan2(YValue, XValue)
            );
        }
        
        public string ToText()
        {
            //if (XValue == 0 && YValue == 0)
            //    return "0";

            var i1 = Id;
            var i2 = Id + 1;

            return $"r({XValue:G}, {YValue:G}) <{i1},{i2}>";
        }

        public string ToLaTeX()
        {
            //if (XValue == 0 && YValue == 0)
            //    return "0";

            var i1 = Id;
            var i2 = Id + 1;

            var xValueText = XValue.GetLaTeXNumber();
            var yValueText = YValue.GetLaTeXNumber();
            var basisText1 = $"{i1},{i2}".GetLaTeXBasisName();
            var basisText2 = $"{i1}".GetLaTeXBasisName();

            //if (XValue == 0)
            //    return $@"\left( {yValueText} \right) {basisText1} {basisText2}";

            //if (YValue == 0)
            //    return $@"{xValueText} {basisText2}";

            return $@"\left[ {xValueText} + \left( {yValueText} \right) {basisText1} \right] {basisText2}";
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}