function gapotDefineBasisBlades(basisDimensions, basisName)
    if (basisDimensions < 2 || basisDimensions > 9)
        ME = MException('gapot:badDimensions', 'Illegal Basis Dimensions');
        
        throw(ME)
    end
	
	n = 2 ^ basisDimensions;
    for id = 0:(n-1)
        binStr = dec2bin(id, basisDimensions);
        onePosArray = fliplr(1 + basisDimensions - find(binStr == '1'));
        basisVariableName = strcat(basisName, num2str(onePosArray, '%i'));
        basisValue = eval(strcat('GAPoTNumLib.Framework.GaPoTNumMatlabUtils.CreateBasisBlade(', num2str(id), ')'));
        %basisValue.TermsToText()
        
        assignin('base', basisVariableName, basisValue);
    end
end
    