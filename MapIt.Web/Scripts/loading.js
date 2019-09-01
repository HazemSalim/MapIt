Sys.Application.add_init(AppInit);
function AppInit() {
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(showLoadingImage);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(hideLoadingImage);
} 

function showLoadingImage(sender, args) {
//    debugger;
    var div = document.getElementById("divLoadingImage");
    div.style.display = "block";
}

function hideLoadingImage(sender, args) {
    var div = document.getElementById("divLoadingImage");
    div.style.display = "none";
}