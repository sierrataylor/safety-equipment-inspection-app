const qrcode = window.qrcode;
const video =document.createElement("video");

let scanning = false;

qrcode.callback = res => {
  if (res) {
    outputData.innerText = res;
    scanning = false;

    video.srcObject.getTracks().forEach(track => {
      track.stop();
    });

    qrResult.hidden = false;
    canvasElement.hidden = true;
    btnScanQR.hidden = false;
  }
};

function initializescanner() {
  const canvas = document.getElementById("qr-canvas");
  document.getElementById("hello").innerHTML = "Hello friends";
  navigator.mediaDevices
    .getUserMedia({ video: { facingMode: "environment" } })
    .then(function (stream) {
      document.getElementById("hello").innerHTML = "got to stream function";
      document.getElementById("scannerimg").style.display = "none";
      // canvas.style.display = block;
      video.setAttribute("playsinline", true); // required to tell iOS safari we don't want fullscreen
      video.srcObject = stream;
      video.play();
      tick();
      scan();
    });
  
}


function tick() {
  var canvas = document.getElementById("qr-canvas");
  document.getElementById("hello").innerHTML = "got to tick function";
  canvas.height = video.videoHeight;
  document.getElementById("hello").innerHTML = "got to point of height of canvas defined";
  canvas.width = video.videoWidth;
  document.getElementById("hello").innerHTML = "got to point of width of canvas defined";
  canvas.getContext("2d").drawImage(video, 0, 0, canvas.width, canvas.height);

   requestAnimationFrame(tick);
  document.getElementById("hello").innerHTML = "got to request animtion frame";
}

function scan() {
  document.getElementById("hello").innerHTML = "got to scan function";
  try {
    qrcode.decode();
  } catch (e) {
    setTimeout(scan, 300);
  }
}
