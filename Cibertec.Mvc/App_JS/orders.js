(function (orders) {
    orders.success = successReload;
    orders.pages = 1;
    orders.rowSize = 50;
    orders.hub = {};
    orders.ids = [];
    orders.recordInUse = false;

    orders.addOrders = addOrdersId;
    orders.removeHubOrders = removeOrdersId;
    orders.validate = validate;

    $(function () {
        connectToHub();
        init();

    })

    return orders;

    function successReload(option) {
        // init();
        // report();
        closeModal(option);
    }

    function init() {
        $.get('/Orders/Count/',
            function (data) {
                var total = data.Value.counts;
                var freight = data.Value.total;
                console.log(total);
                getOrders(total, freight);

            });
    }

    function reportYears() {

        $.get('/Orders/ReportYears/',
            function (data) {
                var cantidades = new Array();
                var anios = new Array();
                
                $.each(data, function (index, value) {
                    
                    if (index === 'Value') {
                        $.each(value, function (indice, valor) {

                            cantidades.push(valor.counts);
                            anios.push(valor.years);
                        });
                        //for (var i = 0; i < value.length; i++) {
                        //    var cantidad = value[i].counts;
                        //    cantidades.push(cantidad);
                        //    var anio = value[i].years;
                        //    anios.push(anio);
                        //}
                     }
                });
                chart2(cantidades, anios);

            });
    }

    function reportMonth() {

        $.get('/Orders/ReportMonth/',
            function (data) {
                var cantidades = new Array();
                var canti = "";
                var cantidad = new Array();
                var meses = new Array();

                $.each(data, function (index, value) {

                    if (index === 'Value') {
                        $.each(value, function (indice, valor) {

                            cantidades.push(valor.counts);
                            canti += (valor.counts + ',');
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
                cantidad = canti.slice(0, -1);
                //$('#peity').val(cantidad);
                chart1(cantidades, meses);

            });
    }

    function getOrders(total, freight) {
        var url = '/Orders/List/';
        $.get(url, function (data) {
            $('.content').html(data);
            $('#total').prepend(total);
            $('#total_peity').append('#'+total);
            $('#freight').prepend(freight);
            reportYears();
            reportMonth();
        });
    }

    function addOrdersId(id) {
        orders.hub.server.addOrdersId(id);
    }

    function removeOrdersId(id) {
        orders.hub.server.removeOrdersId(id);
    }

    function connectToHub() {
        orders.hub = $.connection.ordersHub;
        orders.hub.client.ordersStatus = ordersStatus;
    }

    function ordersStatus(ordersIds) {
        console.log(ordersIds);
        orders.ids = ordersIds;
    }

    function validate(id) {
        orders.recordInUse = (orders.ids.indexOf(id) > -1);
        if (orders.recordInUse) {
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

    function chart2(cantidades, anios) {
        var barChartData = {
            labels: anios,
            datasets: [
                {
                    label: "Year",
                    backgroundColor: '#37e2d0',
                    borderColor: '#1dc9b7',
                    borderWidth: 1,
                    data: cantidades
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

})(window.orders = window.orders || {});