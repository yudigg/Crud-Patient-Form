$(function () {
    $('form').on('submit', function () {
        var form = $(this);
        var fName = form.parent().parent().find("td:first").text();
        var cofirm = confirm('Are you sure you want to delete ' + fName +'?')
        return cofirm;
    });
});