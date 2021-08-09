% Create a GAPoT vector from terms in the first column of a MATLAB array
function mv = gapotTermsArrayToVector(array)
    [iArray, jArray, vArray] = find(array(:, 1));
    m = size(array, 1);
    n = size(array, 2);
    
    mv = GAPoTNumLib.Framework.GaPoTNumMatlabUtils.TermsArrayToVector(m, n, iArray, jArray, vArray);
end