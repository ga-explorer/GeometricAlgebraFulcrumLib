% Compute the GAPoT impedance biversors given the voltage and current
% vectors parts
function mvZ = gapotImpedance(mvU, mvI, partLengthsArray)
    mvZ = mvU.GetPartsImpedance(mvI, int32(partLengthsArray));
end
