// JavaScript source code
let request = new XMLHttpRequest();
let skaterName = 'Tony Hawk';
let skaterId = 1598;
let distance = 1000;
let season = 2018;
let requestLink = 'https://speedskatingresults.com/api/json/skater_results.php?skater=' + skaterId + '&distance=' + distance + '&season=' + season;

request.open('GET', requestLink, true);
request.onload = function() {
    var data = JSON.parse(this.response);
    if (request.status >= 200 && request.status < 400) {

        var generateTableHead = function (table, titles) {
            let thead = table.createTHead();
            let row = thead.insertRow();
            for (let key in titles) {
                let th = document.createElement("th");
                let text = document.createTextNode(titles[key]);
                th.appendChild(text);
                row.appendChild(th);
            }
        };

        var generateTable = function(table, data) {
            for (let index in data) {
                let element = data[index];
                var row = table.insertRow();
                for (let key in element) {
                    let cell = row.insertCell();
                    if (key === 'link') {
                        const anchor = document.createElement('a');
                        anchor.href = element[key];
                        anchor.innerText = 'go to result';
                        cell.appendChild(anchor);
                    } else {
                        let text = document.createTextNode(element[key]);
                        cell.appendChild(text);
                    }
                }
            }
        };

        var table = document.getElementById("results");
        var titles = Object.keys(data.results[0]);
        generateTableHead(table, titles);
        generateTable(table, data.results);

        var overview = document.getElementById("Overview");
        overview.innerHTML = 'Skiing results for ' + skaterName + ' in season ' + data.season;
    }
};

request.send();