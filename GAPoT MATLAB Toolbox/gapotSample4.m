% Always begin with this
gapotInit;

clc;

% Combine 3-phase components into a single GAPoT vector
mvUp = gapotParseVector("2<1>, 3<2>");
mvUn = gapotParseVector("-2<1>, -1<2>");
mvU0 = gapotParseVector("1<1>");

mvU = mvUp + mvUn.OffsetTermIDs(2) + mvU0.OffsetTermIDs(4);

gapotDisplayTerms(mvU)

% Separate 3-phase components into GAPoT vectors
mvU123 = gapotGetParts(mvU, [2,2,2]);

gapotDisplayTerms(mvU123(1))
gapotDisplayTerms(mvU123(2))
gapotDisplayTerms(mvU123(3))

% Separate 3-phase components into a MATLAB array were each column has the 
% terms of a separate component of the 3-phase vector
full( gapotPartsTermsToArray(mvU, 2, [2,2,2]) )

% Another way to combine 3-phase components into a single GAPoT vector using
% terms array
% The second argument (2) is the number of terms per component
arUp = gapotVectorToTermsArray(mvUp, 2); 
arUn = gapotVectorToTermsArray(mvUn, 2);
arU0 = gapotVectorToTermsArray(mvU0, 2);

% Just use normal MATLAB array construction
arU = full([arUp; arUn; arU0]);

disp(arU);

% You can get back a GAPoT vector using this: 
mvU = gapotTermsArrayToVector(arU);

gapotDisplayTerms(mvU)

% Create bivector using text expressions
mvM = gapotParseBiversor("2 <>, -5 <1,2>, 5<3,4>");

fprintf("Display the terms of GAPoT multivectors\n");
gapotDisplayTerms(mvM)

% Compute the geometric product of GAPoT multivectors
mvI = gapotGp(gapotInverse(mvU), mvM);

fprintf("Display the geometric product of GAPoT multivectors\n");
gapotDisplayTerms(mvI)