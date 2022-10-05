using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.BabylonJs;

public sealed class GrBabylonJsSnapshotSpecs
{
	public bool Enabled { get; set; } = false;

    public GrBabylonJsInt32Value Delay { get; set; } = 750;

    public GrBabylonJsInt32Value Width { get; set; } = 1280;

    public GrBabylonJsInt32Value Height { get; set; } = 720;

    public GrBabylonJsFloat32Value Precision { get; set; } = 1;

    public bool UsePrecision { get; set; } = true;

    public string FileName { get; set; } = "Snapshot.png";

	
    public string GetSnapshotCode(string sceneName)
    {
        if (!Enabled)
            return string.Empty;

		var sizeCode = UsePrecision
            ? $"{{ precision: {Precision.GetCode()} }}"
            : $"{{ width: {Width.GetCode()}, height: {Height.GetCode()} }}";

        var composer = new LinearTextComposer();

        composer
            .AppendLine(@"
function base64toBlob(base64Data, contentType) {
	contentType = contentType || '';
	var sliceSize = 1024;
	var byteCharacters = atob(base64Data);
	var bytesLength = byteCharacters.length;
	var slicesCount = Math.ceil(bytesLength / sliceSize);
	var byteArrays = new Array(slicesCount);

	for (var sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
		var begin = sliceIndex * sliceSize;
		var end = Math.min(begin + sliceSize, bytesLength);

		var bytes = new Array(end - begin);
		for (var offset = begin, i = 0; offset < end; ++i, ++offset) {
			bytes[i] = byteCharacters[offset].charCodeAt(0);
		}
		byteArrays[sliceIndex] = new Uint8Array(bytes);
	}
	return new Blob(byteArrays, { type: contentType });
}

function saveScreenShot(scene, delay, fileName, size) {
	scene.onAfterRenderCameraObservable.clear();
	
	BABYLON.Tools.DelayAsync(delay).then(() => { 
		BABYLON.Tools.CreateScreenshot(
			scene.getEngine(), 
			scene.activeCamera, 
			size,
			function (data) {
				const blob = base64toBlob(data.substring(22), 'image/png');

				BABYLON.Tools.DownloadBlob(blob, fileName);
				
				//html2canvas(document.body).then(function(canvas) {
				//    BABYLON.Tools.toBlob(canvas, function(blob) {
				//        BABYLON.Tools.DownloadBlob(blob, fileName);
				//    }, 'image/png', 1);
				//});
			}
		);
	});
}".Trim())
            .AppendLine()
            .AppendLine(@$"{sceneName}.onAfterRenderCameraObservable.add(() => {{")
            .AppendLine(@$"    saveScreenShot({sceneName}, {Delay.GetCode()}, '{FileName}', {sizeCode});")
            .AppendLine(@"});")
            .AppendLine();
		
		return composer.ToString();
    }
}