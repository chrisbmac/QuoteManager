// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showFileName(){
    let btnUpload = document.getElementById("btnUpload").value;
    let mySString = btnUpload.substring(12);
    let mySQLImage = document.getElementById("forImageSQL");
    mySQLImage.value= mySString;
   
    console.log("mysql" + mySQLImage.value);
    $("#span_fileName").text("This is your file: " + mySString);
   
}
