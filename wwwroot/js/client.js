$(function() {
    //Event Handlers
    $(".toast-btn").on("click", toggleOn);
    $("#collapse-btn").on("click", toggleOff);
    $(document).on("keyup",
        function(e) {
            if (e.key === "Escape")
                $(".toast").toast("hide");
        });
    $('.form-check-input').on('change', checkBoxChange);
    $('#pop-btn').on('click', pop);

    //uncheck all balloon checks
    $('.form-check-input').each(function() {
        $(this).prop('checked', false);
    });

    //Data
    $("#fishDiscount").data({ 'header': "Fishy Savings", 'code': 9001 });
    $("#breadDiscount").data({ 'header': "Best Bread Ever!", 'code': 4290 });
    $("#wineDiscount").data({ 'header': "Can't Whine Discount", 'code': 2264 });
});

function toggleOn() {
    var toastAudio = new Audio("/media/toast.wav");
    toastAudio.play();

    $(".toast-header:first:first-child").text($(this).data("header"));
    $(".toast-body:first").text("Discount Code: " + $(this).data("code"));

    $(".toast:first").toast("show");
    $("#discountCollapse").collapse();
};

function toggleOff() {
    $(".toast:first").toast("hide");
};

function checkBoxChange() {
    const imgId = '#' + this.id.substring(0, this.id.indexOf('-')) + "-balloon";
    $(imgId).css('visibility', 'visible');

    $(this).is(':checked') ? 
        $(imgId).removeClass().addClass('animated bounceInUp') : 
        $(imgId).removeClass().addClass('animated fadeOut');


    if ($(imgId).hasClass('animated fadeOut'))
        window.setTimeout(function () {$(imgId).css('visibility', 'hidden')}, 400);
};

function pop() {
    var noCheck = true;
    $('.form-check-input').each(function() {
        if($(this).is(':checked'))
            noCheck = false;
    });
    noCheck ? $('.toast:last').toast('show') : $('.toast:last').toast('hide');
};