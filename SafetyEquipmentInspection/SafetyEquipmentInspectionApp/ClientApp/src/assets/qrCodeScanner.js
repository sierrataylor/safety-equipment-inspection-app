var expression = /[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)?/gi;
var regex;

function initializeScanner() {
  console.log("Worked!");
  let scanner = new Instascan.Scanner({
    video: document.getElementById("camera")
  });

  regex = new RegExp(expression);

  let result = document.getElementById("qrcode");
  scanner.addListener("scan", function (content) {
    if (content.match(regex)) {
      window.location.replace(content);
    }
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

function submit() {
  let inputval = document.getElementById("code").value;
  if (inputval == "") {
    alert("You need to an Equipment Id in order to continue");
  }
  else
  {
    window.location.href = '/inspection-form';
  }
}

