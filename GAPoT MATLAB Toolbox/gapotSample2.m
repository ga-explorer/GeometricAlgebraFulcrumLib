% Always begin with this
gapotInit;

clc;

% Create voltage and current vectors
aU = [
    200 / sqrt(3), 0;
    100 / sqrt(3), 60 * pi / 180
];

mvU = gapotPolarPhasorsArrayToVector(aU);

aI = [
    193.18, -45 * pi / 180;
    100 * sqrt(2), -165 * pi / 180
];

mvI = gapotPolarPhasorsArrayToVector(aI);

fprintf("Display the polar phasors of GAPoT vectors\n");
gapotDisplayPhasors(mvU)
gapotDisplayPhasors(mvI)

% Compute the single-phase power multivector
mvM = gapotPower(mvU, mvI);
% Or use this: mvM = gapotGp(mvU, mvI);

fprintf("Display all terms of the GAPoT power bivector\n");
gapotDisplayTerms(mvM)

fprintf("Active part of power bivector\n");
gapotDisplayTerms(gapotGetActivePart(mvM))

fprintf("Reactive part of power bivector\n");
gapotDisplayTerms(gapotGetReactivePart(mvM))

fprintf("Reactive total of power bivector\n");
disp(gapotGetReactiveTotal(mvM))

fprintf("Non-active part of power bivector\n");
gapotDisplayTerms(gapotGetNonActivePart(mvM))

fprintf("Non-active total of power bivector\n");
disp(gapotGetNonActiveTotal(mvM))

fprintf("Reactive fundamental part of power bivector\n");
gapotDisplayTerms(gapotGetReactiveFundamentalPart(mvM))

fprintf("Harm part of power bivector\n");
gapotDisplayTerms(gapotGetHarmPart(mvM))

fprintf("Harm total of power bivector\n");
disp(gapotGetHarmTotal(mvM))

fprintf("Selected part of power bivector\n");
gapotDisplayTerms(gapotGetTermPart(mvM, 2, 3))

% Compute the squared norm of voltage, current, power, and impedance multivectors
normU = gapotNorm(mvU);
normI = gapotNorm(mvI);
normM = gapotNorm(mvM);

fprintf("Display the squared norm of voltage, current, and power multivectors\n");
disp(normU);
disp(normI);
disp(normM);
disp(normU * normI - normM);

% Get per-phase voltage and current parts; here we have two phases each
% containing two terms
mvUParts = gapotGetParts(mvU, [2, 2]);
mvIParts = gapotGetParts(mvI, [2, 2]);

% Compute per-phase impedance bivectors
mvZParts = gapotImpedance(mvU, mvI, [2, 2]);

fprintf("Display per-phase voltage, current, and impedance multivectors\n");
gapotDisplayTerms(mvUParts(1))
gapotDisplayTerms(mvUParts(2))

gapotDisplayTerms(mvIParts(1))
gapotDisplayTerms(mvIParts(2))

gapotDisplayTerms(mvZParts(1))
gapotDisplayTerms(mvZParts(2))