% You must execute gafulInit.m before doing any computations with this
% toolbox.
%
% Put the path of the toolbox in the gapotAssemblyPath global variable. 
% 
% You can use the '\Debug\' or the '\Release\' versions of GA-FuL 
% depending on what you need: find errors in GA-FuL or use fast 
% implementation of GA-FuL for actual computations. 
% 
% If you want to switch between '\Debug\' and '\Release\', you need to 
% restart MATLAB before executing gafulInit.m

global gafulAssembly gapotAssemblyPath vga pga cga

gapotAssemblyPath = 'D:\Projects\Active\GeometricAlgebraFulcrumLib\GA-FuL MATLAB Toolbox\Debug\';
    
dotnetenv("framework");

if (isempty(gafulAssembly))
    if ~contains(path, gapotAssemblyPath, 'IgnoreCase', true)
        addpath(gapotAssemblyPath);
    end
    
    gafulAssembly = NET.addAssembly(strcat(gapotAssemblyPath, 'GeometricAlgebraFulcrumLib.Matlab.exe'));
end

vga = gafulGetProcessor(0, 0);
pga = gafulGetProcessor(0, 1);
cga = GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors.XGaFloat64Processor.Conformal; %gafulGetProcessor(1, 0);