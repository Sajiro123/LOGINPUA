
var elem = document.documentElement;
function openFullscreen() {
    if (elem.requestFullscreen) {
        elem.requestFullscreen();
        document.getElementById('esconder').style.visibility = 'hidden';

    } else if (elem.mozRequestFullScreen) { /* Firefox */
        elem.mozRequestFullScreen();
        document.getElementById('esconder').style.visibility = 'visible';

    } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari & Opera */
        elem.webkitRequestFullscreen();
        document.getElementById('esconder').style.visibility = 'visible';

    } else if (elem.msRequestFullscreen) { /* IE/Edge */
        elem.msRequestFullscreen();
        document.getElementById('esconder').style.visibility = 'visible';

    }
}

function closeFullscreen() {
    if (document.exitFullscreen) {
        document.exitFullscreen();
        
        document.getElementById('esconder').style.visibility = 'visible';

    } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
        document.getElementById('esconder').style.visibility = 'visible';

    } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
        document.getElementById('esconder').style.visibility = 'visible';

    } else if (document.msExitFullscreen) {
        document.msExitFullscreen();
        document.getElementById('esconder').style.visibility = 'visible';

    }
}