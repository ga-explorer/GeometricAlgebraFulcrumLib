% Create a GAPoT vector from polar phasors in the first 2 columns of 
% a MATLAB array
function mv = gapotPolarPhasorsArrayToVector(array)
    [iArray, jArray, vArray] = find(array(:, 1:2));
    m = size(array, 1);
    n = size(array, 2);
    
    mv = GAPoTNumLib.Framework.GaPoTNumMatlabUtils.PolarPhasorsArrayToVector(m, n, iArray, jArray, vArray);
end