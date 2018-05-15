
$(document).ready(function () {
    // do your magic here
    console.log("ready!");

    var divs = [];
    var init_times = [];
    var noOfDevices = 0;

    //var rawData = [{ Type: "abc", Title: "proximity", Measurements : [
    //        { StartDate: 1525373817000, EndDate: 1525373817020 },
    //        { StartDate: 1525373818000, EndDate: 1525373818030 },
    //        { StartDate: 1525373819000, EndDate: 1525373819010 },
    //        { StartDate: 1525373820000, EndDate: 1525373820120 },
    //        { StartDate: 1525373821000, EndDate: 1525373821020 },
    //        { StartDate: 1525373822000, EndDate: 1525373822050 },
    //        { StartDate: 1525373823000, EndDate: 1525373823020 },
    //        { StartDate: 1525373824000, EndDate: 1525373824220 },
    //        { StartDate: 1525373825000, EndDate: 1525373825020 },
    //        { StartDate: 1525373826000, EndDate: 1525373826080 },
    //        { StartDate: 1525373827000, EndDate: 1525373827090 },
    //        { StartDate: 1525373828000, EndDate: 1525373828000 }
    //    ]
    //}, {
    //    Type: "def", Title: "acc", Measurements: [
    //        { StartDate: 1525373817000, EndDate: 1525373817020 },
    //        { StartDate: 1525373818000, EndDate: 1525373818030 },
    //        { StartDate: 1525373819000, EndDate: 1525373819010 },
    //        { StartDate: 1525373820000, EndDate: 1525373820120 },
    //        { StartDate: 1525373821000, EndDate: 1525373821020 },
    //        { StartDate: 1525373822000, EndDate: 1525373822050 },
    //        { StartDate: 1525373823000, EndDate: 1525373823020 },
    //        { StartDate: 1525373824000, EndDate: 1525373824220 },
    //        { StartDate: 1525373825000, EndDate: 1525373825020 },
    //        { StartDate: 1525373826000, EndDate: 1525373826080 },
    //        { StartDate: 1525373827000, EndDate: 1525373827090 },
    //        { StartDate: 1525373828000, EndDate: 1525373828000 }
    //    ]
    //    }];

    //var rawData2 = [{
    //    Type: "def", Title: "acc", Measurements: [
    //        { StartDate: 1525373829000, EndDate: 1525373829020 },
    //        { StartDate: 1525373830000, EndDate: 1525373830120 },
    //        { StartDate: 1525373831000, EndDate: 1525373831220 },
    //        { StartDate: 1525373832000, EndDate: 1525373832320 }]
    //}
    //];

    //var rawData3 = [{
    //    Type: "def", Title: "acc", Measurements: [
    //        { StartDate: 1525373833000, EndDate: 1525373833100 },
    //        { StartDate: 1525373834000, EndDate: 1525373834020 }
    //    ]
    //}, {
    //    Type: "abc", Title: "proximity", Measurements: [
    //        { StartDate: 1525373833000, EndDate: 1525373833100 },
    //        { StartDate: 1525373834000, EndDate: 1525373834020 }
    //        ]
    //    }];

    var last_door = "";
    var last_floor = "";

    setInterval(retrieveData, 5000);

    function retrieveData() {
        $.ajax({ // Maybe only send timestamp and not ids (what if many devices? - not dynamic right now)
            url: "https://webapiaccess20180420013135.azurewebsites.net//api/iot/GetCopyData?doorGuid="+last_door+"&floorGuid="+last_floor, success: function (result) {
                console.log(result);
                //last_door = ;
                //last_floor = ;
                processResult(result);
            }, error: function (err) {
                console.log("error")
                console.log(err);
            }
        });
    }

    var count = 0;

    var createGraph = function (divName, title, init_time) {
        divs[divs.length] = divName;
        var div = document.createElement("div");
        div.setAttribute("id", divName);
        div.setAttribute("class", "col-lg-12 col-xl-6 col-sm-12 col-xs-12");
        $(".row").append(div);
        init_times[noOfDevices] = { Device: divName, StartDate: init_time };
        noOfDevices++;

        var layout = {
            title: title,
            height: 500,
            xaxis: {
                title: 'Time',
                type: 'date',
                range: [init_time, init_time],
                rangeslider: {range: [init_time, init_time] },
            },
            yaxis: {
                title: 'Event'
            }
        };
        var data = [
            {
                x: [init_time],
                y: [0],
                mode: 'lines',
                line: {color: '#80CAF6' }
            }
        ];
        Plotly.plot(divName, data, layout);
    }

    var updateGraph = function (data, div, interval) {
        var end_time = data[data.length - 1].EndDate;
        var start_time = end_time - interval;
        var init_time = init_times.find(x => x.Device == div).StartDate;
        var update = reshapeArray(data);

        var minuteView = {
                xaxis: {
                type: 'date',
                range: [start_time, end_time],
                rangeslider: {range: [init_time, end_time] },
            }
        };

        Plotly.relayout(div, minuteView);
        Plotly.extendTraces(div, update, [0])
    }

    var reshapeArray = function (array) {
        var time = [];
        var binValue = [];
        var index = 0;
        array.forEach(el => {
            time[index] = el.StartDate;
            binValue[index++] = 0;

            time[index] = el.StartDate;
            binValue[index++] = 1;

            time[index] = el.EndDate;
            binValue[index++] = 1;

            time[index] = el.EndDate;
            binValue[index++] = 0;
        });

        return { x: [time], y: [binValue] };
    }

    var processResult = function (data) {
        data.forEach(x => {
            if (divs.find(y => { x.Type == y }) == undefined) {
                var title = x.Title + " (" + x.Type + ")";
                createGraph(x.Type, title, x.Measurements[0].StartDate);
            }
            updateGraph(x.Measurements, x.Type, 10000);
        });
    }

    //processResult(rawData);

    //createGraph('myDiv', init_time);
    //createGraph('myDiv2', init_time);
    //createGraph('myDiv3', init_time);

    //setTimeout(function () {updateGraph(rawData, 'myDiv', 10000); }, 1000);
    //setTimeout(function () { processResult(rawData); }, 1000);
    //setTimeout(function () { processResult(rawData3); }, 2000);
    //setTimeout(function () { updateGraph(updatedinferred2, 'myDiv3', 10000); }, 3000);

});