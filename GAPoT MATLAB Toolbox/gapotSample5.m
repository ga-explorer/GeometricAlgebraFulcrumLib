% Always begin with this
gapotInit;

clc;

% Define frame of basis vectors before rotation
u1 = gapotParseVector("1<1>");
u2 = gapotParseVector("1<2>");
u3 = gapotParseVector("1<3>");
u4 = gapotParseVector("1<4>");

u1234 = u1.Op(u2).Op(u3).Op(u4);

fprintf('u1 = %s\n', u1.TermsToText());
fprintf('u2 = %s\n', u2.TermsToText());
fprintf('u3 = %s\n', u3.TermsToText());
fprintf('u4 = %s\n', u4.TermsToText());
fprintf('\n');

fprintf('u1234 = %s\n', u1234.TermsToText());
fprintf('inverse(u1234) = %s\n', u1234.Reverse().TermsToText());
fprintf('\n');

% Define basis vectors after rotation
c1 = gapotParseVector("1<1>, -1<4>") / sqrt(2);
c2 = gapotParseVector("-1<1>, 2<2>, -1<4>") / sqrt(6);
c3 = gapotParseVector("-1<1>, -1<2>, 3<3>, -1<4>") / sqrt(12);
c4 = -c1.Op(c2).Op(c3).Gp(u1234.Inverse()).GetVectorPart();

fprintf('c1 = %s\n', c1.TermsToText());
fprintf('c2 = %s\n', c2.TermsToText());
fprintf('c3 = %s\n', c3.TermsToText());
fprintf('c4 = %s\n', c4.TermsToText());
fprintf('\n');

% Find a simple rotor to get c1 from u1
rotor1 = u1.GetRotorToVector(c1);

% Apply the rotor to u2,u3,u4; 
% if you apply it to u1 you should get c1
u2_1 = u2.ApplyRotor(rotor1);
u3_1 = u3.ApplyRotor(rotor1);
u4_1 = u4.ApplyRotor(rotor1);

% Find a simple rotor to get c2 from u2_1
rotor2 = u2_1.GetRotorToVector(c2);

% Apply the rotor to u3_1, u4_1; 
% if you apply it to u1 you should get c1; 
% if you apply it to u2_1 you should get c2
u3_2 = u3_1.ApplyRotor(rotor2);
u4_2 = u4_1.ApplyRotor(rotor2);

% Find a simple rotor to get c3 from u3_2
rotor3 = u3_2.GetRotorToVector(c3);

% If you apply it to u1 you should get c1
% if you apply it to u2_1 you should get c2
% if you apply it to u3_2 you should get c3
% if you apply it to u4_2 you should get c4 because both frames are
% orthonormal with same handidness

% Now construct final rotor
rotor = rotor3.Gp(rotor2).Gp(rotor1)

fprintf('rotor = %s\n', rotor.TermsToText());
fprintf('\n');

% Test rotor action on u1, u2, u3, u4
c1_1 = u1.ApplyRotor(rotor);
c2_1 = u2.ApplyRotor(rotor);
c3_1 = u3.ApplyRotor(rotor);
c4_1 = u4.ApplyRotor(rotor);

fprintf('c1_1 = %s\n', c1_1.TermsToText());
fprintf('c2_1 = %s\n', c2_1.TermsToText());
fprintf('c3_1 = %s\n', c3_1.TermsToText());
fprintf('c4_1 = %s\n', c4_1.TermsToText());
fprintf('\n');


% The following steps are net needed for the procedure
% Construct a rotation matrix from ui frame to ci frame
M1 = full(gapotVectorToTermsArray(c1, 4));
M2 = full(gapotVectorToTermsArray(c2, 4));
M3 = full(gapotVectorToTermsArray(c3, 4));
M4 = full(gapotVectorToTermsArray(c4, 4));

M = [M1, M2, M3, M4]

% Perform eigen-decomposition of rotation matrix
[V,D] = eig(M)

% Make sure this is a proper rotation by showing determinant = 1
det(M)
