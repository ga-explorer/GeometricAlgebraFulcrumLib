﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GAPoTNumLib.Text.LaTeX;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GaPoTNumPolarPhasor
    {
        public static GaPoTNumPolarPhasor operator -(GaPoTNumPolarPhasor p)
        {
            return new GaPoTNumPolarPhasor(p.Id, -p.Magnitude, p.Phase);
        }

        public static GaPoTNumPolarPhasor operator +(GaPoTNumPolarPhasor p1, GaPoTNumPolarPhasor p2)
        {
            var rp = 
                p1.ToRectPhasor() + p2.ToRectPhasor();

            return rp.ToPolarPhasor();
        }

        public static GaPoTNumPolarPhasor operator -(GaPoTNumPolarPhasor p1, GaPoTNumPolarPhasor p2)
        {
            var rp = 
                p1.ToRectPhasor() - p2.ToRectPhasor();

            return rp.ToPolarPhasor();
        }

        public static GaPoTNumPolarPhasor operator *(GaPoTNumPolarPhasor p, double s)
        {
            return new GaPoTNumPolarPhasor(p.Id, s * p.Magnitude, p.Phase);
        }

        public static GaPoTNumPolarPhasor operator *(double s, GaPoTNumPolarPhasor p)
        {
            return new GaPoTNumPolarPhasor(p.Id, s * p.Magnitude, p.Phase);
        }

        public static GaPoTNumPolarPhasor operator /(GaPoTNumPolarPhasor p, double s)
        {
            s = 1.0d / s;

            return new GaPoTNumPolarPhasor(p.Id, s * p.Magnitude, p.Phase);
        }



        public int Id { get; }

        public double Magnitude { get; }

        public double Phase { get; }

        public double PhaseInDegrees 
            => Phase.RadiansToDegrees();


        internal GaPoTNumPolarPhasor(int id, double magnitude, double phase)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            Magnitude = magnitude;
            Phase = phase;
        }

        internal GaPoTNumPolarPhasor(int id, double magnitude)
        {
            Debug.Assert(id > 0 && id % 2 == 1);

            Id = id;
            Magnitude = magnitude;
            Phase = 0;
        }


        public bool IsZero()
        {
            return Magnitude == 0;
        }

        public IEnumerable<GaPoTNumVectorTerm> GetTerms()
        {
            return Magnitude == 0
                ? Enumerable.Empty<GaPoTNumVectorTerm>()
                : ToRectPhasor().GetTerms();
        }

        public double Norm()
        {
            return Math.Abs(Magnitude);
        }

        public double Norm2()
        {
            return Magnitude * Magnitude;
        }

        public GaPoTNumRectPhasor ToRectPhasor()
        {
            return new GaPoTNumRectPhasor(
                Id,
                Magnitude * Math.Cos(Phase),
                Magnitude * Math.Sin(Phase)
            );
        }

        public string ToText()
        {
            //if (Magnitude == 0)
            //    return "0";

            var i1 = Id;
            var i2 = Id + 1;

            return $"p({Magnitude:G}, {PhaseInDegrees:G}) <{i1},{i2}>";
        }

        public string ToLaTeX()
        {
            //if (Magnitude == 0)
            //    return "0";

            var i1 = Id;
            var i2 = Id + 1;

            var magnitudeText = Magnitude.GetLaTeXNumber();
            var phaseText = Phase.GetLaTeXAngleInDegrees();
            var basisText1 = $"{i1},{i2}".GetLaTeXBasisName();
            var basisText2 = $"{i1}".GetLaTeXBasisName();

            //if (Phase == 0)
            //    return $@"{magnitudeText} {basisText2}";

            return $@"{magnitudeText} e^{{ {phaseText} {basisText1} }} {basisText2}";
        }


        public override string ToString()
        {
            return ToText();
        }
    }
}