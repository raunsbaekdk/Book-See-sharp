function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}



function errorAdd(theClass, text, sdiv) {
    sdiv = typeof sdiv !== "undefined" ? "nth-child(" + sdiv + ")" : "first";

    if (jQuery("#" + theClass + " div:" + sdiv + " .help-block").length == 0) {
        jQuery("#" + theClass + "").attr("class", "form-group has-error");
        if (text)
            jQuery("#" + theClass + " div:" + sdiv).append("<span class=\"help-block\" id=\"" + theClass + "Help\">" + text + "</span>");
    }
}

function errorRemove(theClass) {
    jQuery("#" + theClass + "").attr("class", "form-group");
    jQuery("#" + theClass + " .help-block").remove();
}