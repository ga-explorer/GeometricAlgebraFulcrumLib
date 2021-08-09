% Get the term value of a GAPoT power biversor
function value = gapotGetTermValue(mvM, id1, id2)
    value = mvM.GetTermValue(int32(id1), int32(id2));
end
