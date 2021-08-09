% Always begin with this
gapotInit;

clc;

% Create voltage vector using MATLAB array
u = 5;
mvU = gapotTermsArrayToVector([100*sqrt(2)/u; 0]);

% Create current vector using text expressions
mvI = gapotParseVector("p(0.5,0.523598775598299) <1,2>, p(0.5,-0.523598775598299) <3,4>");

fprintf("Display the terms of GAPoT vectors\n");
gapotDisplayTerms(mvU)
gapotDisplayTerms(mvI)

fprintf("Display the polar phasors of GAPoT vectors\n");
gapotDisplayPhasors(mvU)
gapotDisplayPhasors(mvI)

% Compute the GAPoT power biversor
mvM = gapotPower(mvU, mvI);
% Also you can use this: mvM = gapotGp(mvU, mvI);

fprintf("Display the terms of the GAPoT power biversor\n");
gapotDisplayTerms(mvM)

% Compute the GAPoT impedance biversor
mvZ = gapotImpedance(mvU, mvI, [2, 2]);
% Also you can use this: mvZ = gapotGp(mvU, gapotInverse(mvI));

fprintf("Display the terms of the GAPoT impedance biversor\n");
gapotDisplayTerms(mvZ(1))
gapotDisplayTerms(mvZ(2))

% Compute the norm of voltage, current, power, and impedance multivectors
normU = gapotNorm(mvU);
normI = gapotNorm(mvI);
normM = gapotNorm(mvM);
normZ1 = gapotNorm(mvZ(1));
normZ2 = gapotNorm(mvZ(2));

fprintf("Display the squared norm of voltage, current, power, and impedance multivectors\n");
disp(normU);
disp(normI);
disp(normM);
disp(normZ1);
disp(normZ2);

fprintf("Display the reverse of voltage, current, power, and impedance multivectors\n");
gapotDisplayTerms(gapotReverse(mvU))
gapotDisplayTerms(gapotReverse(mvI))
gapotDisplayTerms(gapotReverse(mvM))
gapotDisplayTerms(gapotReverse(mvZ(1)))
gapotDisplayTerms(gapotReverse(mvZ(2)))

fprintf("Display the inverse of voltage and current multivectors\n");
gapotDisplayTerms(gapotInverse(mvU))
gapotDisplayTerms(gapotInverse(mvI))
