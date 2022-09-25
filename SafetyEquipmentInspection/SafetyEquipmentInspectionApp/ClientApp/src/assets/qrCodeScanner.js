const qrcode = window.qrcode;

const video =document.createElement("video");
const canvasElement = document.getElementById("qr-canvas");
const canvas = canvasElement.getContext("2d");

const qrResult = document.getElementById("qr-result");
const outputData = document.getElementById("outputData");
const btnScanQR = document.getElementById("btn-scan-qr");
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
  document.getElementById("hello").innerHTML = "Hello friends";
  navigator.mediaDevices
    .getUserMedia({ video: { facingMode: "environment" } })
  stream();
}

function stream()
{
  document.getElementById("hello").innerHTML = "got to stream function";
  //scanning = true;
  document.getElementById("qr-result").hidden= "false";
  document.getElementById("scannerimg").style.visibility = "hidden";
  // document.getElementById("qr-canvas").hidden = false;
  video.setAttribute("playsinline", true);
  //video.srcObject = stream;
  video.play();
  tick();
  scan();
}
function tick() {
  document.getElementById("hello").innerHTML = "got to tick function";
  document.getElementById("qr-canvas").height = window.innerHeight;
  document.getElementById("hello").innerHTML = "got to point of height of canvas defined";
  document.getElementById("qr-canvas").width = window.innerWidth;
  document.getElementById("qr-canvas").getContext("2d").drawImage(video, 0, 0, window.innerWidth, window.innerHeight);

  //scanning && requestAnimationFrame(tick);
}

function scan() {
  document.getElementById("hello").innerHTML = "got to scan function";
  try {
    qrcode.decode();
  } catch (e) {
    setTimeout(scan, 300);
  }
}
