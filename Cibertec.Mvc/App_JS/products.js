(function (products) {
    products.success = successReload;
    products.pages = 1;
    products.rowSize = 50;
    products.hub = {};
    products.ids = [];
    products.recordInUse = false;

    products.addProducts = addProductsId;
    products.removeHubProducts = removeProductsId;
    products.validate = validate;

    $(function () {
        connectToHub();
        init();
    })

    return products;

    function successReload(option) {
        init();
        closeModal(option);
    }

    function init() {
        $.get('/Products/Count/',
            function (data) {
                var total_activos = data.Value.activos;
                var total_inactivos = data.Value.inactivos;
                var p_total_activos = data.Value.p_activos;
                var p_total_inactivos = data.Value.p_inactivos;
                getProducts(total_activos, total_inactivos, p_total_activos, p_total_inactivos);
            });
    }

    function getProducts(total_activos, total_inactivos, p_total_activos, p_total_inactivos) {
        var url = '/Products/List/';
        $.get(url, function (data) {
            $('.content').html(data);
            $('#total_activos').prepend('#' + total_activos);
            $('#total_inactivos').prepend('#' + total_inactivos);
            $('#p_total_activos').prepend('+' +p_total_activos + '%');
            $('#p_total_inactivos').prepend('-' +p_total_inactivos + '%');
        });
    }

    function addProductsId(id) {
        products.hub.server.addProductsId(id);
    }

    function removeProductsId(id) {
        products.hub.server.removeProductsId(id);
    }

    function connectToHub() {
        products.hub = $.connection.productsHub;
        products.hub.client.productsStatus = productsStatus;
    }

    function productsStatus(productsIds) {
        console.log(productsIds);
        products.ids = productsIds;
    }

    function validate(id) {
        products.recordInUse = (products.ids.indexOf(id) > -1);
        if (products.recordInUse) {
            $('#inUse').removeClass('hidden');
            $('#btn_save').html("");
        }
    }

    function closeModal(option) {
        $("button[data-dismiss='modal']").click();
        $('.modal-body').html("");
        modifyAlertsClasses(option);
    }

    function modifyAlertsClasses(option) {
        if (option == 'create') {
            Swal.fire(
                {
                    position: "top-end",
                    type: "success",
                    title: "El registro fue creado correctamente.",
                    showConfirmButton: false,
                    timer: 3000
                });
        } else if (option == 'delete') {
            Swal.fire(
                {
                    position: "top-end",
                    type: "error",
                    title: "El registro fue eliminado correctamente.",
                    showConfirmButton: false,
                    timer: 3000
                });
        } else if (option == 'edit') {
            Swal.fire(
                {
                    position: "top-end",
                    type: "warning",
                    title: "El registro fue actualizado correctamente.",
                    showConfirmButton: false,
                    timer: 3000
                });
        }
    }

})(window.products = window.products || {});