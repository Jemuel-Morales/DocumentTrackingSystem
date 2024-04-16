function docReady(fn) {
    // see if DOM is already available
    if (document.readyState === "complete" || document.readyState === "interactive") {
        // call on next available tick
        setTimeout(fn, 1);
    } else {
        document.addEventListener("DOMContentLoaded", fn);
    }
}

docReady(function () {
    function onScanSuccess(decodedText, decodedResult) {

        // Handle on success condition with the decoded text or result.
        console.log(decodedText)
        // ^ this will stop the scanner (video feed) and clear the scan area.
        html5QrcodeScanner.clear();
        window.location.href = `/Document?trckNum=${decodedText}`;

    }
    var qrboxFunction = function (viewfinderWidth, viewfinderHeight) {
        // Square QR Box, with size = 80% of the min edge.
        var minEdgeSizeThreshold = 250;
        var edgeSizePercentage = 0.75;
        var minEdgeSize = (viewfinderWidth > viewfinderHeight) ?
            viewfinderHeight : viewfinderWidth;
        var qrboxEdgeSize = Math.floor(minEdgeSize * edgeSizePercentage);
        if (qrboxEdgeSize < minEdgeSizeThreshold) {
            if (minEdgeSize < minEdgeSizeThreshold) {
                return { width: minEdgeSize, height: minEdgeSize };
            } else {
                return {
                    width: minEdgeSizeThreshold,
                    height: minEdgeSizeThreshold
                };
            }
        }
        return { width: qrboxEdgeSize, height: qrboxEdgeSize };
    }
    let html5QrcodeScanner = new Html5QrcodeScanner(
        "reader",
        {
            fps: 10,
            qrbox: qrboxFunction,
            rememberLastUsedCamera: true,
            showTorchButtonIfSupported: true,
            formatsToSupport: [Html5QrcodeSupportedFormats.QR_CODE]
        });
    html5QrcodeScanner.render(onScanSuccess);
});