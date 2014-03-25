// Clicking flag, avoids carrusel crazy behaviours 
var clickedFlag = false;
function StartClick() {
    clickedFlag = true;
}
function EndClick() {
    clickedFlag = false;
}

//THIS OPEN DETAILS ISSUE
function OpenDialog(url) {
    var options = SP.UI.$create_DialogOptions();
    options.url = url;
    options.width = 900;
    options.height = 800;
    SP.UI.ModalDialog.showModalDialog(options);
}

function ResizeCarrusel() {

    var carruselWidth = ($('#DatesWrapper').width() / 7)-7;
    $('#DatesWrapper #DatesCarrusel #DatesTable th, #DatesWrapper #DatesCarrusel #DatesTable td.status').css('min-width', carruselWidth + 'px');
    $('#DatesWrapper').height($('#DatesCarrusel').height());

    $('#DatesTable tr').each(function () {
        var height = $(this).outerHeight();
        var productIndex = $(this).attr('productIndex');
        if (productIndex != undefined && productIndex != null) {
            var tr = $("#ProductNamesTable tr[productIndex='" + productIndex + "']");
            if (tr != undefined && tr != null) {
                tr.outerHeight(height);
            }
        }
    });
}

function BindNavigationControls() {
    var carruselWidth = $('#DatesWrapper').width()-43;

    $('.WeekPrev').click(function () {
        var carruselOffset = $('#DatesCarrusel').offset().left;
        var diff = Math.abs(carruselOffset) - $('#DatesCarrusel').offsetParent().offset().left;

        if (clickedFlag == false && diff > 200) {
            StartClick();
            $('#DatesCarrusel').animate({ right: '-=' + carruselWidth }, 'slow');
            setTimeout(EndClick, 500);
        }
    });

    $('.WeekNext').click(function () {
        var datesCarruselStyle = $('#DatesCarrusel').attr('style');

        if ((clickedFlag == false) && (datesCarruselStyle != undefined)) {
            if ((datesCarruselStyle.indexOf(':0px') == -1) && (datesCarruselStyle.indexOf(': 0px') == -1)) {
                StartClick();
                $('#DatesCarrusel').animate({ right: '+=' + carruselWidth }, '2000');
                setTimeout(EndClick, 500);
            }
        }
    });
}

function BindToolTips() {
    $(document).tooltip({
        items: "img",
        content: function () {
            var element = $(this);
            return element.attr("alt");
        }
    });
}

function BindImageToDialog() {
    $(".TwitterIssueImage").click(
       function () {
           var element = $(this);
           var id = element.attr("twittIssueId");
           var url = L_Menu_BaseUrl +'/Pages/TwitterIssueDetail.aspx?twitterIssueId=' + id;
           OpenDialog(url);
        }
    );
}


function AdjustTwitterFeed() {
    var height = $("#DashboardWrapper").height();
    $(".apiResponse").height(height);
}

$(document).ready(function () {
    ResizeCarrusel();
    BindNavigationControls();
    BindToolTips();
    BindImageToDialog();
    AdjustTwitterFeed();
});