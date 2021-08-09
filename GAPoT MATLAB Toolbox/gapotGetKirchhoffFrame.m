% Create a Kirchhoff frame of n-dimensions containing n-1 vectors where
% e_i = u_{i + 1} - u_i and u_i is the ith standard basis vector
% This frame is not orthogonal
function frame = gapotGetKirchhoffFrame(n)
    frame = GAPoTNumLib.Framework.GaPoTNumMatlabUtils.CreateKirchhoffFrame(n);
end