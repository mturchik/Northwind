$(function() {
    //Random Attention Getter
    const atnGets = [
        "bounce", "flash", "pulse", "rubberBand", "shake", "swing", "tada", "wobble", "jello", "heartBeat"
    ];
    const rando = Math.floor(Math.random() * atnGets.length);
    $("#bday").addClass("animated " + atnGets[rando]);

    //Event Handlers
    $(".toast-btn").on("click", toggleOn);
    $("#collapse-btn").on("click", toggleOff);
    $(document).on("keyup",
        function(e) {
            if (e.key === "Escape")
                $(".toast").toast("hide");
        });
    $(".form-check-input").on("change", checkBoxChange);
    $("#pop-btn").on("click", pop);
    $(".form-check-label").on("mouseenter", mouseOver);
    $(".form-check-label").on("mouseleave", mouseLeave);
    $("#reset-btn").on("click", resetCheck);

    //uncheck all balloon checks
    $(".form-check-input").each(checkBoxChange);
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
    const imgId = "#" + this.id.substring(0, this.id.indexOf("-")) + "-balloon";

    $(this).is(":checked")
        ? $(imgId).css("visibility", "visible").removeClass().addClass("animated bounceInUp")//is checked
        : $(imgId).removeClass().addClass("animated fadeOut");//not checked


    if ($(imgId).hasClass("animated fadeOut"))
        window.setTimeout(function() { $(imgId).css("visibility", "hidden") }, 800);
};

function pop() {
    var noCheck = true;
    $(".form-check-input").each(function() {
        if ($(this).is(":checked"))
            noCheck = false;
    });
    noCheck ? $(".toast:last").toast("show") : $(".toast:last").toast("hide");
};

function mouseOver() {
    const color = this.id.substring(0, this.id.indexOf("-"));
    $("#bday")
        .animate({ "color": color },
            { "easing": "easeInQuad" });
};

function mouseLeave() {
    $('#bday')
        .animate({ "color": "#aaaaaa" },
            { "easing": "easeOutQuad" });
};

function resetCheck() {
    $(".form-check-input").each(function() {
        $(this).prop("checked", false);
    });
    $(".form-check-input").each(checkBoxChange);
};