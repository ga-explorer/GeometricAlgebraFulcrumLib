% Separate the terms of a GAPoT vector into several vectors
% For example the vector mv defined as '1<1>, 2<2>, 3<3>, 4<4>, 5<5>, 6<6>'
% can be separated into 2 component vectors:
% v(1): '1<1>, 2<2>' and v(2): '3<1>, 4<2>, 5<3>, 6<4>'
% By using the command:
% v = gapotGetParts(mv, [2,4]);
function mvParts = gapotGetParts(mv, partLengthsArray)
    mvParts = mv.GetOffsetParts(int32(partLengthsArray));
end