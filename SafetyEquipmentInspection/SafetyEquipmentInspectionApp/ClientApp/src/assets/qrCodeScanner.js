
function initializeScanner() {
  let scanner = new Instascan.Scanner({
    video: document.getElementById("camera")
  });

  let result = document.getElementById("qrcode");
  scanner.addListener("scan", function (content) {
    result.innerText = content;
    scanner.stop();
  });
  Instascan.Camera.getCameras()
    .then(function (cameras) {
      if (cameras.length > 0) {
        scanner.start(cameras[0]);
      } else {
        result.innerText = "No cameras found.";
      }
    })
    .catch(function (e) {
      result.innerText = e;
    });
}
