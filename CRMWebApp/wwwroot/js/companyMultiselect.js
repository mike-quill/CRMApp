$(document).ready(function () {
    updateContractor();
    updateCustomer();
    updateVendor();
});

function updateContractor() {
    $("#contractor-type-title").html("Contractor Types (" + $('#selectedContractorOptions option').length + ")");
}

function updateCustomer() {
    $("#customer-type-title").html("Customer Types (" + $('#selectedCustomerOptions option').length + ")");
}

function updateVendor() {
    $("#vendor-type-title").html("Vendor Types (" + $('#selectedVendorOptions option').length + ")");
}

$('#btnContractorRight').click(function (e) {
    var selectedOpts = $('#selectedContractorOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availContractorOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();

    updateContractor();
});

$('#btnContractorLeft').click(function (e) {
    var selectedOpts = $('#availContractorOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedContractorOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();

    updateContractor();
});

$('#btnCustomerRight').click(function (e) {
    var selectedOpts = $('#selectedCustomerOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availCustomerOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();

    updateCustomer();
});

$('#btnCustomerLeft').click(function (e) {
    var selectedOpts = $('#availCustomerOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedCustomerOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();

    updateCustomer();
});

$('#btnVendorRight').click(function (e) {
    var selectedOpts = $('#selectedVendorOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availVendorOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();

    updateVendor();
});

$('#btnVendorLeft').click(function (e) {
    var selectedOpts = $('#availVendorOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedVendorOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();

    updateVendor();
});

$('#btnSubmit').click(function (e) {
    $('#selectedContractorOptions option').prop('selected', true);
    $('#selectedCustomerOptions option').prop('selected', true);
    $('#selectedVendorOptions option').prop('selected', true);
});

$('#btnSubmit2').click(function (e) {
    $('#selectedContractorOptions option').prop('selected', true);
    $('#selectedCustomerOptions option').prop('selected', true);
    $('#selectedVendorOptions option').prop('selected', true);
});