// TODO: jel ovo potrebno?
const HTML_FILES_PATH = '/childweb/servlets';
const FILES_PATH = '../files';
const CSS_FILES_PATH = '../files/styles';
const JS_FILES_PATH = '../files/scripts';


function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}


function setCookie(cookieName, cookieValue, expirationTime){
    var expires = "";
    if (expirationTime !== undefined) {
        var date  = new Date();
        date.setTime(date.getTime() + (expirationTime*24*60*60*1000));
        expires += "expires=" + date.toUTCString();
    }
    document.cookie = cookieName + "=" + cookieValue + ";" + expires;
}

function deleteCookie (cookieName) {
    //if cookie exists, delete cookie
    if (getCookie(cookieName) != "") {
        var expires  = "Thu, 01 Jan 1970 00:00:00 UTC";
        document.cookie = cookieName + "=" + "" + ";" + expires;
    }
}