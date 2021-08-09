% Create a GAPoT vector from rectangular phasors in the first 2 columns of 
% a MATLAB array
function mv = gapotRectPhasorsArrayToVector(array)
    [iArray, jArray, vArray] = find(array(:, 1:2));
    m = size(array, 1);
    n = size(array, 2);
    
    mv = GAPoTNumLib.Framework.GaPoTNumMatlabUtils.RectPhasorsArrayToVector(m, n, iArray, jArray, vArray);
end