function keypress(e) {
//    console.log(e.keyCode);
    if (isNumber(e)) {
        e.returnValue = true;
        return true;
    } else {
        e.returnValue = false;
        e.preventDefault();
        return false;
    }
};

// check if input variable is number or not 
function isNumber(e) {
    // if ([e.keyCode || e.which] == 8)
    //document.getElementById("MainContent_btnSubmit").click();
    if ([e.keyCode || e.which] == 8) //this is to allow backspace
        return true;
    if ([e.keyCode || e.which] == 46) //this is to allow backspace
        return true;
    if ([e.keyCode || e.which] == 9) //this is to allow tab
        return true;
    if ([e.keyCode || e.which] == 86) // this is to allow control + V (past)
        return true;
    if ([e.keyCode || e.which] == 67) // this is to allow control + C (cut)
        return true;
    if ([e.keyCode || e.which] == 88) // this is to allow control + X (past)
        return true;
    if ([e.keyCode || e.which] == 90) // this is to allow control + Z (undo)
        return true;
    if ([e.keyCode || e.which] == 89) // this is to allow control + Y (redo)
        return true;
    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.which >= 48 && e.which <= 57) || (e.keyCode >= 96 &&
            e.keyCode <= 105) || (e.which >= 96 && e.which <= 105)) {
        return true;
    } //this is to allow type number
    else {
        return false;
    }
};