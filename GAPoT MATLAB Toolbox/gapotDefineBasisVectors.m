function gapotDefineBasisVectors(basisDimensions, basisName)
    if (basisDimensions < 2 || basisDimensions > 9)
        ME = MException('gapot:badDimensions', 'Illegal Basis Dimensions');
        
        throw(ME)
    end
	
    for id = 1:basisDimensions
        %binStr = dec2bin(id, basisDimensions);
        %onePosArray = fliplr(1 + basisDimensions - find(binStr == '1'));
        basisVariableName = strcat(basisName, num2str(id, '%i'))
        basisValue = eval(strcat('GAPoTNumLib.Framework.GaPoTNumMatlabUtils.CreateBasisVector(', num2str(id), ')'));
        basisValue.TermsToText()
        
        assignin('base', basisVariableName, basisValue);
    end
end