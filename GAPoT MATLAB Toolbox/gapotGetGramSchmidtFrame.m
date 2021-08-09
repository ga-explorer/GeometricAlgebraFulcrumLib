% Create an orthonormal frame of n-dimensions containing by Gram- 
% Schmidt orthonormalization of a Kirchhoff frame.
function frame = gapotGetGramSchmidtFrame(n)
    frame = GAPoTNumLib.Framework.GaPoTNumMatlabUtils.CreateGramSchmidtFrame(n);
end