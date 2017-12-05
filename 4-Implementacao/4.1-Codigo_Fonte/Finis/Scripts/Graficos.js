google.charts.load('current', { 'packages': ['bar'] });
google.charts.setOnLoadCallback(drawChartBarras);

function drawChartBarras() {
    var data = google.visualization.arrayToDataTable([
        ['Mês', 'Entradas', 'Saídas'],
        ['Novembro', 345, 198],
        ['Outubro', 1170, 460],
        ['Setembro', 660, 1120],
        ['Agosto', 1030, 540],
        ['Julho', 300, 890],
        ['Junho', 1030, 456],
        ['Maio', 1030, 670],
        ['Abril', 630, 240],
        ['Março', 980, 346],
        ['Fevereiro', 760, 945],
        ['Janeiro', 456, 740],
        ['Dezembro', 879, 440],
        ['Novembro', 1002, 640]
    ]);

    var options = {
        chart: {
            title: 'Transações nos últimos 12 meses',
            subtitle: 'Entradas e saídas de créditos',
        }
    };

    var chart = new google.charts.Bar(document.getElementById('columnchart_material'));

    chart.draw(data, google.charts.Bar.convertOptions(options));
}

google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(drawChartRosca);

function drawChartRosca() {
    var data = google.visualization.arrayToDataTable([
        ['Situação', 'Situação'],
        ['Aguardando avaliação', 3],
        ['Avaliado', 12],
        ['Aguardando cliente', 7],
        ['Concluído', 2],
        ['Canceladas', 3]
    ]);

    var options = {
        title: 'Situação das avaliações',
        pieHole: 0.4,
    };

    var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
    chart.draw(data, options);
}

google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChartPizza);

function drawChartPizza() {

    var data = google.visualization.arrayToDataTable([
        ['Situação', 'Pedidos'],
        ['Pendentes', 3],
        ['Realizados', 5],
        ['Aguardando cliente', 2],
        ['Concluído', 5]
    ]);

    var options = {
        title: 'Pedidos de Exemplares'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
}