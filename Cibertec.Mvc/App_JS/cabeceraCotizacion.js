(function (cabeceraCotizacions) {
    cabeceraCotizacions.success = successReload;
    cabeceraCotizacions.pages = 1;
    cabeceraCotizacions.rowSize = 50;
    cabeceraCotizacions.hub = {};
    cabeceraCotizacions.ids = [];
    cabeceraCotizacions.recordInUse = false;

    cabeceraCotizacions.addCabeceraCotizacions = addCabeceraCotizacionsId;
    cabeceraCotizacions.removeHubCabeceraCotizacions = removeCabeceraCotizacionsId;
    cabeceraCotizacions.validate = validate;

    $(function () {
        connectToHub();
        init();

    })

    return cabeceraCotizacions;

    function successReload(option) {
        init();
        // report();
        closeModal(option);
    }

    function init() {
        $.get('/CabeceraCotizacion/Count/',
            function (data) {
                var counts = data.Value.counts;
                var total = data.Value.total;
                var descuento = data.Value.descuento;
                var facturado = data.Value.facturado;
                console.log(total);
                getCabeceraCotizacions(counts, total, descuento, facturado);

            });
    }

    function reportVentaAsesor() {

        $.get('/CabeceraCotizacion/ReportVentaAsesor/',
            function (data) {
                var totales = new Array();
                var asesores = new Array();

                $.each(data, function (index, value) {

                    if (index === 'Value') {
                        $.each(value, function (indice, valor) {

                            totales.push(valor.total);
                            asesores.push(valor.asesor);
                        });
                        //for (var i = 0; i < value.length; i++) {
                        //    var cantidad = value[i].counts;
                        //    cantidades.push(cantidad);
                        //    var anio = value[i].years;
                        //    anios.push(anio);
                        //}
                    }
                });
                chart2(totales, asesores);

            });
    }

    function reportMonthCotizacion() {

        $.get('/CabeceraCotizacion/ReportMonthCotizacion/',
            function (data) {
                var totales = new Array();
                var meses = new Array();

                $.each(data, function (index, value) {

                    if (index === 'Value') {
                        $.each(value, function (indice, valor) {

                            totales.push(valor.total);
                            meses.push(valor.months);
                        });
                        //for (var i = 0; i < value.length; i++) {
                        //    var cantidad = value[i].counts;
                        //    cantidades.push(cantidad);
                        //    var anio = value[i].years;
                        //    anios.push(anio);
                        //}
                    }
                });
                chart1(totales, meses);

            });
    }

    function getCabeceraCotizacions(counts, total, descuento, facturado) {
        var url = '/CabeceraCotizacion/List/';
        $.get(url, function (data) {
            $('.content').html(data);
            $('#counts').prepend(counts);
            $('#total').prepend(total);
            $('#descuento').prepend(descuento);
            $('#facturado').prepend(facturado);
            $('#total_peity').append('#' + counts);
            reportVentaAsesor();
            reportMonthCotizacion();
        });
    }

    function addCabeceraCotizacionsId(id) {
        cabeceraCotizacions.hub.server.addCabeceraCotizacionsId(id);
    }

    function removeCabeceraCotizacionsId(id) {
        cabeceraCotizacions.hub.server.removeCabeceraCotizacionsId(id);
    }

    function connectToHub() {
        cabeceraCotizacions.hub = $.connection.cabeceraCotizacionsHub;
        cabeceraCotizacions.hub.client.cabeceraCotizacionsStatus = cabeceraCotizacionsStatus;
    }

    function cabeceraCotizacionsStatus(cabeceraCotizacionsIds) {
        console.log(cabeceraCotizacionsIds);
        cabeceraCotizacions.ids = cabeceraCotizacionsIds;
    }

    function validate(id) {
        cabeceraCotizacions.recordInUse = (cabeceraCotizacions.ids.indexOf(id) > -1);
        if (cabeceraCotizacions.recordInUse) {
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

    function chart2(totales, asesores) {
        var barChartData = {
            labels: asesores,
            datasets: [
                {
                    label: "Asesor",
                    backgroundColor: '#37e2d0',
                    borderColor: '#1dc9b7',
                    borderWidth: 1,
                    data: totales
                }]

        };
        var config = {
            type: 'bar',
            data: barChartData,
            options:
            {
                responsive: true,
                legend:
                {
                    position: 'top',
                },
                title:
                {
                    display: false,
                    text: 'Bar Chart'
                },
                scales:
                {
                    xAxes: [
                        {
                            display: true,
                            scaleLabel:
                            {
                                display: false,
                                labelString: '6 months forecast'
                            },
                            gridLines:
                            {
                                display: true,
                                color: "#f2f2f2"
                            },
                            ticks:
                            {
                                beginAtZero: true,
                                fontSize: 11
                            }
                        }],
                    yAxes: [
                        {
                            display: true,
                            scaleLabel:
                            {
                                display: false,
                                labelString: 'Profit margin (approx)'
                            },
                            gridLines:
                            {
                                display: true,
                                color: "#f2f2f2"
                            },
                            ticks:
                            {
                                beginAtZero: true,
                                fontSize: 11
                            }
                        }]
                }
            }
        }
        new Chart($("#barChart > canvas").get(0).getContext("2d"), config);
    }

    function chart1(cantidades, meses) {
        var config = {
            type: 'doughnut',
            data:
            {
                datasets: [
                    {
                        data: cantidades,
                        backgroundColor: [
                            '#4de5d5',
                            '#21dfcb',
                            '#ccbfdf',
                            '#a38cc6',
                            '#838383',
                            '#696969',
                            '#ffdb8e',
                            '#ffca5b',
                            '#82c4f8',
                            '#2196F3',
                            '#fe85be',
                            '#fd52a3'
                        ],
                        label: 'My dataset' // for legend


                    }],
                labels: meses
            },
            options:
            {
                responsive: true,
                legend:
                {
                    display: true,
                    position: 'bottom',
                }
            }
        };
        new Chart($("#doughnutChart > canvas").get(0).getContext("2d"), config);
    }

})(window.cabeceraCotizacions = window.cabeceraCotizacions || {});