
$('#BillingCountryID').change(function () {
	var selectedCountry = $("#BillingCountryID").val();
	var theDDL = $("#BillingProvinceID");
	theDDL.empty();
	var URL = "/Employees/GetProvinces/" + selectedCountry;
	$(function () {
		$.getJSON(URL, function (data) {
			if (data !== null && !jQuery.isEmptyObject(data)) {
				$.each(data, function (index, item) {
					theDDL.append($('<option/>', {
						value: item.value,
						text: item.text
					}));
				});
				theDDL.trigger("chosen:updated");
			} else {
				theDDL.append($('<option/>', {
					value: null,
					text: 'No Provinces in that Country'
				}));
			};
		});
	});
});


$('#ShippingCountryID').change(function () {
	var selectedCountry = $("#ShippingCountryID").val();
	var theDDL = $("#ShippingProvinceID");
	theDDL.empty();
	var URL = "/Companies/GetProvinces/" + selectedCountry;
	$(function () {
		$.getJSON(URL, function (data) {
			if (data !== null && !jQuery.isEmptyObject(data)) {
				$.each(data, function (index, item) {
					theDDL.append($('<option/>', {
						value: item.value,
						text: item.text
					}));
				});
				theDDL.trigger("chosen:updated");
			} else {
				theDDL.append($('<option/>', {
					value: null,
					text: 'No Provinces in that Country'
				}));
			};
		});
	});
});