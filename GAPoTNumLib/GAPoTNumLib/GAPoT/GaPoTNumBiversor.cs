using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GAPoTNumLib.Interop.MATLAB;
using GAPoTNumLib.Structures;
using GAPoTNumLib.Text;

namespace GAPoTNumLib.GAPoT
{
    public sealed class GeoPoTNumBiversor : IEnumerable<GeoPoTNumBiversorTerm>
    {
        public static GeoPoTNumBiversor operator -(GeoPoTNumBiversor v)
        {
            return new GeoPoTNumBiversor(
                v._termsDictionary.Values.Select(t => -t)
            );
        }

        public static GeoPoTNumBiversor operator +(GeoPoTNumBiversor v1, GeoPoTNumBiversor v2)
        {
            var biversor = new GeoPoTNumBiversor();

            biversor.AddTerms(v1._termsDictionary.Values);
            biversor.AddTerms(v2._termsDictionary.Values);

            return biversor;
        }

        public static GeoPoTNumBiversor operator -(GeoPoTNumBiversor v1, GeoPoTNumBiversor v2)
        {
            var biversor = new GeoPoTNumBiversor();

            biversor.AddTerms(v1._termsDictionary.Values);
            biversor.AddTerms(v2._termsDictionary.Values.Select(t => -t));

            return biversor;
        }

        public static GeoPoTNumBiversor operator +(GeoPoTNumBiversor v, double s)
        {
            return new GeoPoTNumBiversor(
                v._termsDictionary.Values
            ).AddTerm(0,0, s);
        }

        public static GeoPoTNumBiversor operator +(double s, GeoPoTNumBiversor v)
        {
            return new GeoPoTNumBiversor(
                v._termsDictionary.Values
            ).AddTerm(0,0, s);
        }

        public static GeoPoTNumBiversor operator -(GeoPoTNumBiversor v, double s)
        {
            return new GeoPoTNumBiversor(
                v._termsDictionary.Values
            ).AddTerm(0,0, -s);
        }

        public static GeoPoTNumBiversor operator -(double s, GeoPoTNumBiversor v)
        {
            return new GeoPoTNumBiversor(
                v._termsDictionary.Values.Select(t => -t)
            ).AddTerm(0,0, s);
        }

        public static GeoPoTNumBiversor operator *(GeoPoTNumBiversor v, double s)
        {
            return new GeoPoTNumBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GeoPoTNumBiversor operator *(double s, GeoPoTNumBiversor v)
        {
            return new GeoPoTNumBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GeoPoTNumBiversor operator /(GeoPoTNumBiversor v, double s)
        {
            s = 1.0d / s;

            return new GeoPoTNumBiversor(
                v._termsDictionary.Values.Select(t => s * t)
            );
        }

        public static GeoPoTNumBiversor operator /(double s, GeoPoTNumBiversor v)
        {
            return s * v.Inverse();
        }


        private readonly Dictionary2Keys<int, GeoPoTNumBiversorTerm> _termsDictionary
            = new Dictionary2Keys<int, GeoPoTNumBiversorTerm>();


        public double this[int id1, int id2]
        {
            get
            {
                (id1, id2) = GeoPoTNumUtils.ValidateBiversorTermIDs(id1, id2);

                return _termsDictionary
                    .Values
                    .Where(t => t.TermId1 == id1 && t.TermId2 == id2)
                    .Select(v => v.Value)
                    .Sum();
            }
        }


        internal GeoPoTNumBiversor()
        {
        }

        internal GeoPoTNumBiversor(IEnumerable<GeoPoTNumBiversorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);
        }


        public GeoPoTNumBiversor AddTerm(GeoPoTNumBiversorTerm term)
        {
            //The input term IDs is already validation by construction, no validation is needed here
            var id1 = term.TermId1;
            var id2 = term.TermId2;

            if (_termsDictionary.TryGetValue(id1, id2, out var oldTerm))
                _termsDictionary[id1, id2] = 
                    new GeoPoTNumBiversorTerm(id1, id2, oldTerm.Value + term.Value);
            else
                _termsDictionary.Add(id1, id2, term);

            return this;
        }

        public GeoPoTNumBiversor AddTerm(int id1, int id2, double value)
        {
            return AddTerm(
                new GeoPoTNumBiversorTerm(id1, id2, value)
            );
        }

        public GeoPoTNumBiversor AddTerms(IEnumerable<GeoPoTNumBiversorTerm> termsList)
        {
            foreach (var term in termsList)
                AddTerm(term);

            return this;
        }


        public IEnumerable<GeoPoTNumBiversorTerm> GetTerms()
        {
            return _termsDictionary.Values.Where(t => !t.Value.IsNearZero());
        }

        public double GetTermValuesSum()
        {
            return _termsDictionary
                .Values
                .Select(v => v.Value)
                .Sum();
        }


        public double GetTermValue(int id1, int id2)
        {
            (id1, id2) = GeoPoTNumUtils.ValidateBiversorTermIDs(id1, id2);

            return _termsDictionary
                .Values
                .Where(t => t.TermId1 == id1 && t.TermId2 == id2)
                .Select(v => v.Value)
                .Sum();
        }

        public GeoPoTNumBiversorTerm GetTerm(int id1, int id2)
        {
            var value = GetTermValue(id1, id2);

            return new GeoPoTNumBiversorTerm(id1, id2, value);
        }


        public double GetActiveTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsScalar)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetNonActiveTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsNonScalar)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetReactiveTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsPhasor)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetReactiveFundamentalTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.TermId1 == 1 && t.TermId2 == 2)
                .Select(v => v.Value)
                .Sum();
        }

        public double GetHarmTotal()
        {
            return _termsDictionary
                .Values
                .Where(t => t.IsNonScalar && (t.TermId1 != 1 || t.TermId2 != 2))
                .Select(v => v.Value)
                .Sum();
        }

        public double GetScalar()
        {
            return GetTermValue(0, 0);
        }


        public GeoPoTNumBiversor GetBivectorPart()
        {
            return new GeoPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar)
            );
        }

        public GeoPoTNumBiversor GetTermPart(int id1, int id2)
        {
            (id1, id2) = GeoPoTNumUtils.ValidateBiversorTermIDs(id1, id2);

            return new GeoPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.TermId1 == id1 && t.TermId2 == id2)
            );
        }

        public GeoPoTNumBiversor GetActivePart()
        {
            return new GeoPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsScalar)
            );
        }

        public GeoPoTNumBiversor GetNonActivePart()
        {
            return new GeoPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar)
            );
        }

        public GeoPoTNumBiversor GetReactivePart()
        {
            return new GeoPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsPhasor)
            );
        }

        public GeoPoTNumBiversor GetReactiveFundamentalPart()
        {
            return new GeoPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.TermId1 == 1 && t.TermId2 == 2)
            );
        }

        public GeoPoTNumBiversor GetHarmPart()
        {
            return new GeoPoTNumBiversor(
                _termsDictionary.Values.Where(t => t.IsNonScalar && (t.TermId1 != 1 || t.TermId2 != 2))
            );
        }


        public GeoPoTNumBiversor Reverse()
        {
            var result = new GeoPoTNumBiversor();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(pair.Value);
                else
                    result.AddTerm(-pair.Value);
            }

            return result;
        }

        public GeoPoTNumBiversor Negative()
        {
            var result = new GeoPoTNumBiversor();

            foreach (var pair in _termsDictionary) 
                result.AddTerm(-pair.Value);

            return result;
        }

        public GeoPoTNumBiversor ScaleBy(double s)
        {
            var result = new GeoPoTNumBiversor();

            foreach (var pair in _termsDictionary) 
                result.AddTerm(s * pair.Value);

            return result;
        }

        public GeoPoTNumBiversor NegativeReverse()
        {
            var result = new GeoPoTNumBiversor();

            foreach (var pair in _termsDictionary)
            {
                if (pair.Value.IsScalar)
                    result.AddTerm(-pair.Value);
                else
                    result.AddTerm(pair.Value);
            }

            return result;
        }

        public double Norm()
        {
            return Math.Sqrt(Norm2());
        }

        public double Norm2()
        {
            return _termsDictionary
                .Values
                .Select(t => t.Value * t.Value)
                .Sum();
        }

        public GeoPoTNumBiversor Inverse()
        {
            var norm2 = Norm2();

            if (norm2 == 0)
                throw new DivideByZeroException();

            var value = 1.0d / norm2;

            return new GeoPoTNumBiversor(
                _termsDictionary
                    .Values
                    .Select(t => t.ScaledReverse(value))
            );
        }

        public GeoPoTNumBiversor DivideByNorm()
        {
            return this / Norm();
        }

        public GeoPoTNumBiversor DivideByNorm2()
        {
            return this / Norm2();
        }

        public GeoPoTNumVector Gp(GeoPoTNumVector v)
        {
            return this * v;
        }


        public GeoNumMatlabSparseMatrixData TermsToMatlabArray(int rowsCount)
        {
            return GetTerms().TermsToMatlabArray(rowsCount);
        }

        public GeoPoTNumMultivector ToMultivector()
        {
            return new GeoPoTNumMultivector(
                GetTerms().Select(t => t.ToMultivectorTerm())
            );
        }

        public string ToText()
        {
            return TermsToText();
        }

        public string TermsToText()
        {
            var termsArray = GetTerms()
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.TermId1)
                .ThenBy(t => t.TermId2)
                .ToArray();

            return termsArray.Length == 0
                ? "0"
                : termsArray.Select(t => t.ToText()).Concatenate(", ", 80);
        }

        public string ToLaTeX()
        {
            return TermsToLaTeX();
        }

        public string TermsToLaTeX()
        {
            var termsArray = GetTerms()
                .Where(t => !t.Value.IsNearZero())
                .OrderBy(t => t.TermId1)
                .ThenBy(t => t.TermId2)
                .ToArray();

            return termsArray.Length == 0
                ? "0"
                : string.Join(" + ", termsArray.Select(t => t.ToLaTeX()));
        }

        public override string ToString()
        {
            return TermsToText();
        }

        public IEnumerator<GeoPoTNumBiversorTerm> GetEnumerator()
        {
            return _termsDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}