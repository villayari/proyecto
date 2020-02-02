(function (customer) {
    customer.success = successReload;
    customer.pages = 1;
    customer.rowSize = 50;
    customer.hub = {};
    customer.ids = [];
    customer.recordInUse = false;

    customer.addCustomer = addCustomerId;
    customer.removeHubCustomer = removeCustomerId;
    customer.validate = validate;

    $(function () {
        connectToHub();
        init();
    })

    return customer;

    function successReload(option) {
        init();
        cibertec.closeModal(option);
    }

    function init() {
        $.get('/Customer/CountPages/' + customer.rowSize,
            function (data) {
                customer.pages = data;
                getCustomers();
            });
    }

    function getCustomers() {
        var url = '/Customer/List/';
        $.get(url, function (data) {
            $('.content').html(data);
        });
    }

    function addCustomerId(id) {
        customer.hub.server.addCustomerId(id);
    }

    function removeCustomerId(id) {
        customer.hub.server.removeCustomerId(id);
    }

    function connectToHub() {
        customer.hub = $.connection.customerHub;
        customer.hub.client.customerStatus = customerStatus;
    }

    function customerStatus(customerIds) {
        console.log(customerIds);
        customer.ids = customerIds;
    }

    function validate(id) {
        customer.recordInUse = (customer.ids.indexOf(id) > -1);
        if (customer.recordInUse) {
            $('#inUse').removeClass('hidden');
            $('#btn_save').html("");
        }
    }
         
})(window.customer = window.customer || {});