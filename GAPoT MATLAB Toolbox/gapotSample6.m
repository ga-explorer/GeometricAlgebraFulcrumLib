% Always begin with this
gapotInit;

clc;

n = 3;

% The standard basis frame
frame1 = gapotGetBasisFrame(n);
matrix1 = double(frame1.GetMatrix(n));

disp(frame1.VectorsToText());
disp(matrix1);

% The standard Clarke frame
frame2 = gapotGetClarkeFrame(n);
matrix2 = double(frame2.GetMatrix(n));

disp(frame2.VectorsToText());
disp(matrix2);

% The Gram-Schmidt frame
frame3 = gapotGetGramSchmidtFrame(n);
matrix3 = double(frame3.GetMatrix(n));

disp(frame3.VectorsToText());
disp(matrix3);

% Find the sequence of rotors to transform frame1 to frame2
rotors12 = gapotGetRotors(frame1, frame2);

% Get a single rotor from the geometric product of the rotors sequence
finalRotor12 = rotors12.GetFinalRotor();

% This is the inverse rotor that transforms frame 2 to frame 1
finalRotor21 = finalRotor12.Reverse();

disp(rotors12.RotorsToText());
disp(finalRotor12.TermsToText());
disp(finalRotor21.TermsToText());

% Define a vector and rotate it using finalRotor12
v1 = gapotParseVector('3<1>, -2.4<2>, -0.6<3>');
v2 = v1.ApplyRotor(finalRotor12);

disp(v2.TermsToText());

% Apply the inverse rotor to get v1 from v2 again
v3 = v2.ApplyRotor(finalRotor21);

disp(v3.TermsToText());

% We can also rotate a whole frame using a rotor
invFrame2 = frame2.ApplyRotor(finalRotor21);

disp(invFrame2.VectorsToText());