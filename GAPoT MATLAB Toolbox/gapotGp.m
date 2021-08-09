% The geometric product of two GAPoT multivectors. When one is a vector and
% another is a biversor the result is the vector part of the actual
% geometric product; the scalar and trivector parts are omitted
function mv = gapotGp(mv1, mv2)
    mv = mv1.Gp(mv2);
end