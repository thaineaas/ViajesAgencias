var backgrounds = [
  "images/port1.png",
  "images/port1.png",
  "images/port1.png",
  "images/port1.png",
]; // Asegúrate de proporcionar las rutas correctas de tus imágenes
var loadedImages = [];
var index = 0;

// Precargar todas las imágenes
function preloadImages() {
  for (var i = 0; i < backgrounds.length; i++) {
    var img = new Image();
    img.src = backgrounds[i];
    loadedImages.push(img);
  }
}

// Cambiar el fondo de manera fluida
function changeBackground() {
  var container = document.getElementById("fondox");
  container.style.backgroundImage = "url(" + loadedImages[index].src + ")";
  index = (index + 1) % loadedImages.length; // Cambia al siguiente fondo de manera cíclica
}

// Inicia la precarga de imágenes y cambia el fondo cada 7 segundos
preloadImages();
setInterval(changeBackground, 7000);
