/// <reference path="jquery-3.1.1.js" />
/// <reference path="bootstrap.js" />
/// <reference path="JQMethod.js" />
/// <reference path="Pagination.js" />
$(document).ready(function () {
    loadData();
    $('#btnPrevious').click(function () {

        if (page <= 1) {
            page = 1;
            Pagination(result, page);
        } else {
            page--;
            Pagination(result, page);
        }

    })


    $('#btnNext').click(function () {

        if (page >= TotalPage) {
            page = TotalPage;
            Pagination(result, page);
        } else {
            page++;
            Pagination(result, page)
        }

    })
});
function loadData() {
    debugger;
    var dataa = {};
    AjaxCaller("/Private/OrderManagement.aspx/OrderDetail", JSON.stringify(dataa), function (data) {
        result = JSON.parse(data.d);
        page = 1;
        Pagination(result, page);
    });
}
function Pagination(result, page) {

    var limit = 2;
    var TotalCount = result.length;
    TotalPage = Math.ceil(TotalCount / limit);
    var lastcount = TotalCount - ((TotalPage - 1) * limit);
    var tbody = '';
    var startFrom = limit * (page - 1);

    if (page === TotalPage) {
        for (var i = startFrom; i < startFrom + lastcount; i++) {

            tbody += '<tr>';
            tbody += '<td>' + result[i].ProductId + '</td>';            
            tbody += '<td>' + result[i].Email + '</td>';
            tbody += '<td>' + result[i].InvoiceNo + '</td>';
            tbody += '<td>' + result[i].Quantity + '</td>';
            tbody += '<td>' + result[i].Price + '</td>';
            tbody += '<td>' + result[i].Status + '</td>';
            //tbody += '<td>' + result[i].Address.Address + '</td>';
            //tbody += '<td>' + result[i].Address.City + '</td>';
            //tbody += '<td>' + result[i].Address.Country + '</td>';
            tbody += '</tr>';
        }
    }
    else if (page > TotalPage) {

        tbody += '<tr><td colspan=5> No Data To Show</td></tr>'
    }
    else {

        for (var j = startFrom; j < startFrom + limit; j++) {

            tbody += '<tr>';
            tbody += '<td>' + result[j].ProductId + '</td>';
            tbody += '<td>' + result[j].Email + '</td>';
            tbody += '<td>' + result[j].InvoiceNo + '</td>';
            tbody += '<td>' + result[j].Quantity + '</td>';
            tbody += '<td>' + result[j].Price + '</td>';
            tbody += '<td>' + result[j].Status + '</td>';   
            tbody += '</tr>';
        }
    }
    $('.tbody').html(tbody);
    $('#TxtPageNumber').val(page);
    $('#TxtPageNumber').css('border-color', 'grey');
    $('#lblTotalPage').html(TotalPage);
}