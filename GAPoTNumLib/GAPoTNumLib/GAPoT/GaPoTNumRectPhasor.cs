using System;
using System.Collections.Generic;
using System.Diagnostics;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumRectPhasor
    {
        public static GeoPoTNumRectPhasor operator -(GeoPoTNumRectPhasor p)
        {
            return new GeoPoTNumRectPhasor(
                p.Id,
                -p.XValue,
                -p.YValue
            );
        }

        public static GeoPoTNumRectPhasor operator +(GeoPoTNumRectPhasor p1, GeoPoTNumRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GeoPoTNumRectPhasor(
                p1.Id,
                p1.XValue + p2.XValue,
                p1.YValue + p2.YValue
            );
        }

        public static GeoPoTNumRectPhasor operator -(GeoPoTNumRectPhasor p1, GeoPoTNumRectPhasor p2)
        {
            if (p1.Id != p2.Id)
                throw new InvalidOperationException();

            return new GeoPoTNumRectPhasor(
                p1.Id,
                p1.XValue - p2.XValue,
                p1.YValue - p2.YValue
            );
        }

        public static GeoPoTNumRectPhasor operator *(GeoPoTNumRectPhasor p, double s)
        {
            return new GeoPoTNumRectPhasor(
                p.Id,
                s * p.XValue,
                s * p.YValue
            );
        }

        public static GeoPoTNumRectPhasor operator *(double s, GeoPoTNumRectPhasor p)
        {
            return new GeoPoTNumRectPhasor(
                p.Id,
                s * p.XValue,
                s * p.YValue
            );
        }

        public static GeoPoTNumRectPhasor operator /(GeoPoTNumRectPhasor p, double s)
        {
            s = 1.0d / s;

            return new GeoPoTNumRectPhasor(
                p.Id,
                s * p.XValue,
                s * p.YValue
            );
        }


        public int Id { get; }

        public double XValue { get; set; }

        public double YValue { get; set; }


        internal GeoPoTNumRectPhasor(int id, double x, double y)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            XValue = x;
            YValue = y;
        }

        internal GeoPoTNumRectPhasor(int id, double x)
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

        public IEnumerable<GeoPoTNumVectorTerm> GetTerms()
        {
            if (XValue != 0)
                yield return new GeoPoTNumVectorTerm(
                    Id,
                    XValue
                );

            if (YValue != 0)
                yield return new GeoPoTNumVectorTerm(
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

        public GeoPoTNumPolarPhasor ToPolarPhasor()
        {
            return new GeoPoTNumPolarPhasor(
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