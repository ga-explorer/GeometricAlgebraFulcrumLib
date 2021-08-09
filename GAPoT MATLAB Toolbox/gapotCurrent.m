% Compute the GAPoT current vector given the power biversor and voltage vector
function mvI = gapotCurrent(mvM, mvU)
    mvI = gapotGp(gapotInverse(mvU), mvM);
end
