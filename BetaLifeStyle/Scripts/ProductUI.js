/// <reference path="jquery-3.1.1.js" />
/// <reference path="bootstrap.js" />

$(function () {
    // On Ready page
    $("#searchtxtbox").change(function () {
        $("#searchbtn").click();
    });

    // category handling with no sub category
    $(".CategoryNoSubCategory").on('click', function () {
        var category = $(this).html();
        GetProductsByCategory(category, 1, true);
    });

    // load databased on subcategory
    $(".sidebarsubcategory").on('click', function () {
        var category = $(this).parent().parent().children(':first-child').html().trim();
        var subcategory = $(this).html();
        GetProductsBySubCategory(category, subcategory, 1, true);
    });

    // Used On Product Detail page
    $(".productimagessmall").mouseover(function () {
        var idd = this.id;
        var path = $("#" + idd).attr("src");
        $(".MainPic").removeAttr("src");
        $(".MainPic").attr("src", path);
    })

    // hide other opened subcategories
    $(".anchorcollapse").on('click', function () {
        $('.ctogg').collapse('hide');
    });


})

// Make Ajax Calls
function AjaxCaller(urll, ddata, callback) {
    var canreturn = false;
    var result;
    var fun = $.ajax({
        async: true,
        type: "POST",
        url: urll,
        data: ddata,
        datatype: "json",
        contentType: 'application/json; charset=utf-8'
    });
    fun.done(function (data) {
        callback(data);
        return;
    });
    fun.fail(function (xhr) {
        //   alert("Error Occured (AC)");
        return '{Error:"1",Message:"Internal Error ' + xhr.reponseText + '"}';
    });

};


function GetProducts(p, replace) {
    var dataa = {
        page: p
    };

    ChangeTitle("Our Products");

    AjaxCaller("/Public/ViewProducts.aspx/GetProducts", JSON.stringify(dataa), function (data) {
        ShowProducts(data, replace);
    });
}

function GetProductsByCategory(cat, p, replace) {
    var dataa = {
        category: cat,
        page: p
    };

    ChangeTitle("'" + cat + "'");

    AjaxCaller("/Public/ViewProducts.aspx/GetProductsByCategory", JSON.stringify(dataa), function (data) {
        ShowProducts(data, replace);
    });
}

function GetProductsBySubCategory(cat, subcat, p, replace) {
    var dataa = {
        category: cat,
        subcategory: subcat,
        page: p
    };

    ChangeTitle("'" + subcat + "'");

    AjaxCaller("/Public/ViewProducts.aspx/GetProductsBySubCategory", JSON.stringify(dataa), function (data) {
        ShowProducts(data, replace);
    });
}

function Search(p, replace) {
    var dataa = {
        searchterm: $("#searchtxtbox").val(),
        page: p
    };
    if (!dataa.searchterm) {
        return;
    }
    ChangeTitle("'" + dataa.searchterm + "'");
    AjaxCaller("/Public/ViewProducts.aspx/Search", JSON.stringify(dataa), function (data) {
        ShowProducts(data, replace);
    });
}

function ShowProducts(data, replace) {
    var products = $.parseJSON(data.d);
    var containerdiv = $(".productgrid");
    if (replace == true) {
        containerdiv.html("");
    }

    if (products.length == 0) {
        containerdiv.html(containerdiv.html() + EmptyData());
    } else {
        $.each(products, function (i, item) {
            containerdiv.html(containerdiv.html() + ProductView(products[i]));
        });
    }


}

function ChangeTitle(newtitle) {
    $("#ProductListTitle").html(newtitle);
}

function ProductView(item) {
    return '<div class="product"><hidden ID="ProHdn" Value="' + item.Id + '" />' +
        '<img ID="proImg" runat="server" Class="productimage" src="' + "/ProductImage/" + item.ImageUrl.ProductImagePath + '" />' +
        '<div class="productdescription">' +
        '<p class="h6 text-center"><strong>' + item.Name + '</strong></p>' +
        '<div style="padding-left: 10px; padding-right: 10px">' +
        '<span><small>' + item.BrandName + '</small></span><span class="pull-right"><b>$' + item.Price + '</b></span>' +
        '</div>' +
        '<br/>' +
        '<div class="text-center">' +
        '<a href="/Product/' + item.Id + '" class="buybtn">Buy Now</a>' +
        '</div>' +
        '</div>';
}

function EmptyData() {
    return '<div class="white" style="grid-column:1/5;padding:10px;"> No Data Found <a href="/Home" > Back To Home</a>' +
        '</div>';
}