
$(document).ready(function () {
    // do your magic here
    console.log("ready!");

    //var inferredData = [{ StartDate: 1525373817000, Count: 3 },
    //    { StartDate: 1525373818000, Count: 5 },
    //    { StartDate: 1525373819000, Count: 2 },
    //    { StartDate: 1525373820000, Count: -4 },
    //    { StartDate: 1525373821000, Count: -3 },
    //    { StartDate: 1525373822000, Count: 1 },
    //    { StartDate: 1525373823000, Count: 3 },
    //    { StartDate: 1525373824000, Count: -2 },
    //    { StartDate: 1525373825000, Count: 1 },
    //    { StartDate: 1525373826000, Count: 1 }
    //];

    //var inferredData2 = [{ StartDate: 1525373827000, Count: -2 },
    //{ StartDate: 1525373830000, Count: -5 },
    //{ StartDate: 1525373831000, Count: 3 },
    //{ StartDate: 1525373831500, Count: 6 },
    //{ StartDate: 1525373833000, Count: 2 },
    //{ StartDate: 1525373834000, Count: -1 },
    //{ StartDate: 1525373834200, Count: 2 },
    //{ StartDate: 1525373836000, Count: -3 },
    //{ StartDate: 1525373837000, Count: 2 },
    //{ StartDate: 1525373838500, Count: 1 }
    //];

    //var inferredData3 = [{ StartDate: 1525373840000, Count: 1 },
    //{ StartDate: 1525373842000, Count: 1 },
    //{ StartDate: 1525373847000, Count: 1 },
    //{ StartDate: 1525373851100, Count: -2 },
    //{ StartDate: 1525373852000, Count: 3 },
    //{ StartDate: 1525373855000, Count: -1 },
    //{ StartDate: 1525373855500, Count: 2 },
    //{ StartDate: 1525373857100, Count: 1 },
    //{ StartDate: 1525373858000, Count: 1 },
    //{ StartDate: 1525373860000, Count: -1 }
    //];

    var divName = "count-graph";
    var count = 0;
    var init_time = 0;
    var interval = 10000;
    var last_id = "";

    $.ajax({
        url: "http://webapiaccess20180420013135.azurewebsites.net/api/iot/GetInferredData?guid=", success: function (result) {
            console.log(result);
            init_time = result.Measurements[0].StartDate;
            last_time = result.Measurements[result.Measurements.length - 1].Id;
            initGraph(init_time);
            updateGraph(results.Measurements);
        }, error: function (err) {
            console.log("error");
            console.log(err);
        }
    });

    setInterval(retrieveData, 5000);

    function retrieveData() {
        $.ajax({
            url: "http://webapiaccess20180420013135.azurewebsites.net/api/iot/GetInferredData?guid="+Id, success: function (result) {
                console.log(result);
                last_time = result.Measurements[result.Measurements.length - 1].Id;
                updateGraph(result.Measurements);
            }, error: function (err) {
                console.log("error");
                console.log(err);
            }
        });
    }

    function initGraph(init_time) {

        var layout = {
            title: "Frequency Flow",
            height: 800,
            xaxis: {
                title: "Time",
                type: 'date',
                range: [init_time, init_time],
                rangeslider: { range: [init_time, init_time] },
            },
            yaxis: {
                title: "Count"
            }
        };
        var data = [
            {
                x: [init_time],
                y: [0],
                mode: 'lines',
                line: { color: '#80CAF6' }
            }
        ];
        Plotly.plot(divName, data, layout);
    }

    var updateGraph = function (data) {
        var end_time = data[data.length - 1].StartDate;
        var start_time = end_time - interval;

        var update = {
            x: [data.map(x => x.StartDate)],
            y: [data.map(x => count += parseInt(x.Count))]
        }

        var minuteView = {
            xaxis: {
                type: 'date',
                range: [start_time, end_time],
                rangeslider: { range: [init_time, end_time] },
            }
        };

        Plotly.relayout(divName, minuteView);
        Plotly.extendTraces(divName, update, [0])
    };

    //setTimeout(function () { updateGraph(inferredData); }, 1000);
    //setTimeout(function () { updateGraph(inferredData2); }, 2000);
    //setTimeout(function () { updateGraph(inferredData3); }, 3000);

});