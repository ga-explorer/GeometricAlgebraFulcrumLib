% Always begin with this
gapotInit;

clc;

R = gapotParseBiversor("3.1009536313039 <>, -1.28445705037617 <1,3>, -0.985598559653489 <2,3>, 0.408248290463863 <1,2>");

R1 = R.ToMultivector();

R1.Gp(R1.Reverse()).TermsToText()

%n = 5;

%gapotDefineBasisBlades(n, 'e');

%mv = (3 * e12 - 2 * e123 + 4.6 * e245) / 5

%e123.TermsToText()

%mv.TermsToText()