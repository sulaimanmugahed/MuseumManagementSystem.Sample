var ctx = document.getElementById('materialsChart').getContext('2d');
var chart;

function changeChartType() {
    var selectedType = document.getElementById('chartTypeSelector').value;

    if (chart) {
        chart.destroy(); 
    }

    $.ajax({
        url: '/materials/GetChartData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var dataset = {
                label: data.label,
                data: data.data,
                backgroundColor: getRandomColors(data.data.length, 0.2),
                borderColor: 'rgba(0,0,0,0)',
                borderWidth: 1
            };
            var options = {
                   legend: {
                    display: false,
                    
                    },
            };

            var excludedTypes = ['pie', 'doughnut', 'radar','polarArea'];
            if (!excludedTypes.includes(selectedType)) {
               
                options.scales = {

                    yAxes: [{
                        ticks: {
                            beginAtZero: true,

                        },
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            zeroLineWidth: 2
                        },
                    }
                    ],
                    xAxes: [{
                        ticks: {
                            autoSkip: false,
                            maxRotation: 90,
                            minRotation: 90,
                            fontSize: 11
                        },

                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            zeroLineWidth: 2
                        }
                    }],

                };
            }
            chart = new Chart(ctx, {
                type: selectedType,
                data: {
                    labels: data.labels,
                    datasets: [dataset]
                },
                options:options,
            });
        },
        error: function (xhr, status, error) {
            showErrorMessage();
        }
    });
}

changeChartType();

function getRandomColors(count, opacity) {
    var colors = [];
    for (var i = 0; i < count; i++) {
        var color = 'rgba(' + getRandomValue(0, 255) + ',' + getRandomValue(0, 255) + ',' + getRandomValue(0, 255) + ',' + opacity + ')';
        colors.push(color);
    }
    return colors;
}

function getRandomValue(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

//export
function exportExcel (e, dt, button, config) {
    var form = document.createElement('form');
    form.method = 'POST';
    form.action = '/materials/export';

    var idInput = document.createElement('input');
    idInput.type = 'hidden';
    idInput.name = 'type';
    idInput.value = "excel";

    form.appendChild(idInput);
    document.body.appendChild(form);

    form.submit();
}
