global gapotAssembly gapotAssemblyPath

if (isempty(gapotAssembly))
    gapotAssemblyPath = 'D:\Projects\Active\GAPoTNumLib\GAPoTNumLib.Framework\bin\x64\Debug\';
    
    if ~contains(path, gapotAssemblyPath, 'IgnoreCase', true)
        addpath(gapotAssemblyPath);
    end
    
    gapotAssembly = NET.addAssembly(strcat(gapotAssemblyPath, 'GAPoTNumLib.Framework.exe'));
end
