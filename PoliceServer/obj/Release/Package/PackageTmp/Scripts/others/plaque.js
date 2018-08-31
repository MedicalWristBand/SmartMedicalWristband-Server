$(document).ready(function() {
    window.checkP1 = function(e) {
        if (document.getElementById('txtP1').value.length == 2 && ((e.keyCode <= 57 && e.keyCode >= 48) || (e.keyCode <= 111 && e.keyCode >= 97))) {
            document.getElementById('txtP2').select();
        }
    };
    window.checkP2 = function(e) {
        if (document.getElementById('txtP2').value.length == 3 && ((e.keyCode <= 57 && e.keyCode >= 48) || (e.keyCode <= 111 && e.keyCode >= 97))) {
            document.getElementById('txtP3').select();
        }
    };
    document.getElementById('txtP1').focus();
});
    
