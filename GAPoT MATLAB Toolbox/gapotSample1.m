% Always begin with this
gapotInit;

clc;

u1 = 5;
u2 = 10;

% Create GAPoT vector using MATLAB array of terms
disp('Create GAPoT vector using MATLAB array of terms');
mv1 = gapotTermsArrayToVector([
    100 * sqrt(2) / u1; 
    0; 
    200 * sqrt(3) / u2 * cos(60 * pi / 180); 
    -200 * sqrt(3) / u2 * sin(60 * pi / 180)
]);
gapotDisplayPhasors(mv1)

% Scale vector by given number
disp('Scale vector by given number')
mv1s = mv1 * 3;

gapotDisplayPhasors(mv1s)

% Create GAPoT vector using MATLAB array of polar phasors
disp('Create GAPoT vector using MATLAB array of polar phasors');
mv2 = gapotPolarPhasorsArrayToVector([
    100 * sqrt(2) / u1, 30 * pi / 180; 
    200 * sqrt(3) / u2, 60 * pi / 180; 
]);
gapotDisplayPhasors(mv2)

% Create GAPoT vector using MATLAB array of rectangular phasors
disp('Create GAPoT vector using MATLAB array of rectangular phasors');
mv3 = gapotRectPhasorsArrayToVector([
    10, -30; 
    40, 20; 
    -20, 20; 
    0, 50; 
]);
gapotDisplayRectPhasors(mv3)


% Create MATLAB array of terms from GAPoT vector
disp('Create MATLAB array of terms from GAPoT vector');
full( gapotVectorToTermsArray(mv1, 4) )

% Create MATLAB array of polar phasors from GAPoT vector
disp('Create MATLAB array of polar phasors from GAPoT vector');
full( gapotVectorToPolarPhasorsArray(mv2, 2) )

% Create MATLAB array of rectangular phasors from GAPoT vector
disp('Create MATLAB array of rectangular phasors from GAPoT vector');
full( gapotVectorToPolarPhasorsArray(mv3, 4) )


% Read the scalar coefficient of the first term in the vector
disp('Read the scalar coefficient of the first term in the vector');
mv1.Item(1)

% Add two vectors
disp('Add two vectors');
mv3 = gapotAdd(mv1, mv2);

gapotDisplayTerms(mv3)

% Convert GAPoT vector terms to MATLAB array
disp('Convert GAPoT vector terms to MATLAB array');
a1 = full( gapotVectorToTermsArray(mv3, 4) )

%Convert GAPoT vector polar phasors to MATLAB array
disp('Convert GAPoT vector polar phasors to MATLAB array')
a2 = full( gapotVectorToPolarPhasorsArray(mv3, 2) )

% Compute power biversor
disp('Compute power biversor')
mvM = gapotGp(mv1, mv2);

gapotDisplayTerms(mvM)

% Convert GAPoT bivector terms into MATLAB array
disp('Convert GAPoT bivector terms into MATLAB array');
a3 = full( gapotBiversorToTermsArray(mvM, 5) )


% Scale biversor by given number
disp('Scale biversor by given number')
mvP = mvM * 3;

gapotDisplayTerms(mvP)