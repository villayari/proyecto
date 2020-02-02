(function (cibertec) {
    cibertec.getModal = getModalContent;
    cibertec.closeModal = closeModal;

    return cibertec;

    function getModalContent(url) {
        $.get(url, function (data) {
            $('.modal-body').html(data);
        });
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

})(window.cibertec = window.cibertec || {});