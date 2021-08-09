% Create the standard Clarke frame of n-dimensions
% See paper: "Generalized Clarke Components for Polyphase Networks", 1969
function frame = gapotGetClarkeFrame(n)
    frame = GAPoTNumLib.Framework.GaPoTNumMatlabUtils.CreateClarkeFrame(n);
end