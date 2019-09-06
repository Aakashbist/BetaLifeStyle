/// <reference path="jquery-3.1.1.js" />
/// <reference path="bootstrap.js" />
/// <reference path="JQMethod.js" />


var TotalPage = 0;
var result;
var page;
$(document).ready(function () {
    
    
    loadData();

    $('#TxtPageNumber').on('input',function () {
        debugger;
        var pageNumber = parseInt($('#TxtPageNumber').val());
        var reg = /^[0-9]\d*$/;
        if (reg.test(pageNumber) === true || isNaN(pageNumber)===false) {
            
            if (isNaN(pageNumber)) {
                $('#TxtPageNumber').val("1");
            }
            else {
                if (pageNumber > TotalPage || pageNumber <= 0) {
                    $('.tbody').html('<tr><td colspan=5> No Data To Show</td></tr>');
                }
                else {

                    loadData(pageNumber);
                    $('#TxtPageNumber').css('border-color','grey');
                }
            }
        }
        else {
            $('#TxtPageNumber').css('border-color','red');
        }

    });

    $('#btnPrevious').click(function () {
      
      //  var pageNumber = parseInt($('#TxtPageNumber').val());
        if (page <= 1) {
            page = 1;
            Pagination(result, page);
        } else {
            page--;
            Pagination(result, page);
        }

    })
 

    $('#btnNext').click(function () {
  
      //  var pageNumber = parseInt($('#TxtPageNumber').val());
        if (page >= TotalPage) {
            page = TotalPage;
            Pagination(result, page);
        } else {
            page++;
            Pagination(result,page)
        }

    })
})


// To load user for UserManagement

function loadData() {
    var dataa = {};
    AjaxCaller("/Private/UserManagement.aspx/UserDetail", JSON.stringify(dataa), function (data) {
        result = JSON.parse(data.d);
        page = 1;
        Pagination(result, page);
    });
}

// for pagination
function Pagination(result, page) {
  
    var limit = 4;
    var TotalCount = result.length;
    TotalPage = Math.ceil(TotalCount / limit);
    var lastcount = TotalCount-((TotalPage-1)* limit);
    var tbody = '';
    var startFrom = limit * (page - 1);
  
    if (page === TotalPage) {
        for (var i = startFrom; i < startFrom + lastcount; i++) {

            tbody += '<tr>';
            tbody += '<td>' + result[i].ID + '</td>';
            tbody += '<td>' + result[i].Email + '</td>';
            tbody += '<td>' + result[i].IsActive +'</td>';
            tbody += '<td>' + result[i].UserRole + '</td>';
            if (result[i].IsActive === true) {
                tbody += '<td><a class="btn" style="font-size:18px; background-color: #2874F0"" onclick="Isactivate(' + result[i].ID + ',' + parseInt($('#TxtPageNumber').val()) + ')"><span class="fa fa-lock" style="color: white"><span></a> </td>';
            } else {
                tbody += '<td><a class="btn" style="font-size:18px;  background-color: #2874F0" onclick="Isactivate(' + result[i].ID + ',' + parseInt($('#TxtPageNumber').val()) + ')"><span class="fa fa-unlock" style="color: white"><span></a> </td>';
            }
            tbody += '</tr>';
        }
    }
    else if (page > TotalPage) {

        tbody += '<tr><td colspan=5> No Data To Show</td></tr>'
    }
    else {

        for (var j = startFrom; j < startFrom + limit; j++) {
          
            tbody += '<tr>';
            tbody += '<td>' + result[j].ID + '</td>';
            tbody += '<td>' + result[j].Email + '</td>';
            tbody += '<td>' + result[j].IsActive + '</td>';
            tbody += '<td>' + result[j].UserRole + '</td>';
            if (result[i].IsActive === true) {
                tbody += '<td><a class="btn"  style="font-size:18px;  background-color: #2874F0" onclick="Isactivate(' + result[i].ID + ',' + parseInt($('#TxtPageNumber').val()) + ')"><span class="fa fa-lock" style="color: white"><span></a> </td>';
            } else {
                tbody += '<td><a class="btn"  style="font-size:18px;  background-color: #2874F0" onclick="Isactivate(' + result[i].ID + ',' + parseInt($('#TxtPageNumber').val()) + ')"><span class="fa fa-unlock" style="color: white"><span></a> </td>';
            }
            tbody += '</tr>';
        }
    }
    $('.tbody').html(tbody);
    $('#TxtPageNumber').val(page);
    $('#TxtPageNumber').css('border-color', 'grey');
    $('#lblTotalPage').html(TotalPage);
}
function Isactivate(ID, page) {
    if (isNaN(page)) {
        page = 1;
        $('#TxtPageNumber').css('border-color', 'grey');

    }
    var ans = confirm("Are you sure you want to Change user Status?");
    if (ans) {
        var dataa = { Id: ID }
        $.ajax({
            url: "/Private/UserManagement.aspx/IsActivate",
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            data:JSON.stringify(dataa),
            dataType: "json",
            success: function (result) {  
                loadData(page);
            }
        });

    }
}

function SearchUser() {
   
    var _searchItem = $('#searchtxtbox').val();
   var dataa =
        {
            searchterm: _searchItem
        }
        AjaxCaller("/Private/UserManagement.aspx/Search", JSON.stringify(dataa), function (data) {
            result = JSON.parse(data.d);
         
         page = 1;
            Pagination(result, page);
        })    
}
function Searchorder() {

    var _searchItem = $('#searchtxtbox').val();
    var dataa =
        {
            searchterm: _searchItem
        }
    AjaxCaller("/Private/OrderManagement.aspx/Search", JSON.stringify(dataa), function (data) {
        result = JSON.parse(data.d);

        page = 1;
        Pagination(result, page);
    })
}




