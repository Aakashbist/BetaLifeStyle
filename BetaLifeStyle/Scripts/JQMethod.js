/// <reference path="jquery-3.1.1.js" />
/// <reference path="bootstrap.js" />

$(document).ready(function () {
    //  var myBackup = $('#RegisterModal').clone(true);
    //$("#RegisterModal").on('hidden.bs.modal', function () {
    //      $("#RegisterModal").remove();
    //      var myClone = myBackup.clone();
    //      $('#modals').append(myClone);
    //  });

 $(".sidebarsubcategory").on('click', function () {
     var subcategory = $(this).html();
     console.log("Sucategory = " + subcategory);
        GetProductsBySubCategory(subcategory, 1, true);
    });

    $('#ImageGallery').on({
        mouseover: function () {
            debugger;
            $(this).css('border', 'blue')
            var imageURL = $(this).attr('src');
            $('#Image').fadeOut(200, function () {
                $(this).attr('src', imageURL);
            }).fadeIn(200);
        },

        mouseout: function () {
            $(this).css('border', 'white')
        }
    }, 'img');


    //Hide Forgot Password Body Body
    $("#ForgotPass").addClass("hidden");

    //Show Forgot Body
    $("#btnforgot").click(function () {

        $("#logins").addClass("hidden")
        $("#ForgotPass").removeClass("hidden");

    });

    //Show Login Body
    $("#btnlogin").click(function () {

        $("#logins").removeClass("hidden")
        $("#ForgotPass").addClass("hidden");

    });




    //validating text box in register modal).fadeIn(100);


    $("#TxtREmail").on("input", function () {
        emailvalidate("#TxtREmail", "#ems");
    });


    $("#TxtRPassword").on('input', function () {
        passwordvalidate("#TxtRPassword", "#ems2")
    });


    $("#TxtRCPassword").on('input', function () {
        var password = $("#TxtRCPassword").val();
        if ($("#TxtRPassword").val() != password) {
            $("#TxtRCPassword").css('border-color', 'darkred');
            $("#ems3").addClass("glyphicon-remove");
            $("#ems3").css('color', 'darkred');
            $("#ems3").removeClass("glyphicon-ok");
        }
        else {
            $("#TxtRCPassword").css('border-color', 'green');
            $("#ems3").addClass("glyphicon-ok");
            $("#ems3").css('color', 'green');
            $("#ems3").removeClass("glyphicon-remove");
        }
    });

    // to validate email in login modal
    $("#TxtEmail").on('input', function () {
        var email = $("#TxtEmail").val();
        if (!ValidateEmail(email)) {
            $("#TxtEmail").css('border-color', 'red');

        }
        else {
            $("#TxtEmail").css('border-color', 'green');
        }
    });


    $("#searchbtn").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#searchbtn").click();
        }
    });

    // to validate email in forget password
    $("#txtForgotEmail").on('input', function () {
        var email = $("#txtForgotEmail").val();
        if (!ValidateEmail(email)) {
            $("#txtForgotEmail").css('border-color', 'red');

        }
        else {
            $("#txtForgotEmail").css('border-color', 'green');
        }
    });

    // Login
    var json;
    $("#btnShowModal").click(function () {
        $("#loginModal").modal('show');
    });


    $("#btnHideModal").click(function () {
        $("#loginModal").modal('hide');

    });

    $("#btnsignin").click(function () {

        var dataa = {
            email: $("#TxtEmail").val(),
            password: $("#TxtPassword").val()
        };
        var json = new Array();
        AjaxCaller("/Services/MethodHandler.aspx/login", JSON.stringify(dataa), function (data) {
            var json = $.parseJSON(data.d);
            if (json["Error"] == "0") {
                window.location.href = json["RedirectUrl"];
            } else if (json["Error"] == "1") {
                $("#messagelogin").html(json["Message"]).css("color", "red").removeClass("hidden");
            }
        });
    });

    // Forget Password

    $("#btnHideForgetPasswordModal").click(function () {
        $("#loginModal").modal('hide');

    });

    $("#btnShowForgetModal").click(function () {
        $("#loginModal").modal('Hide');
        $("#ForgetPasswordModal").modal('show')
    });
    // forget password

    $('#BtnResetPassword').click(function () {
        debugger
        var pass = $('#TxtResetPassword').val();
        var cpass = $("#TxtRestConfirmaPassword").val();
        if (pass == cpass) {
            var dataa = {
                password: pass,
                uniqueId: getUrlVars()
            };
            debugger;
            AjaxCaller("/Services/MethodHandler.aspx/forgetpassword", JSON.stringify(dataa), function (data) {
                var vars = $.parseJSON(data.d);
                if (vars["Error"] == "0") {
                    $("#resetpasswordmsg").html(vars["Message"]).css("color", "green").removeClass("hidden");
                    $('#TxtResetPassword').val('');
                    $("#TxtRestConfirmaPassword").val('')
                }
                else {
                    $("#resetpasswordmsg").html(vars["Message"]).css("color", "red").removeClass("hidden");
                }
            });

        }
        else {
            $("#ltMessages").html("Password did not mstch").css("color", "red").removeClass("hidden");
        }

    });
    

    $('#BtnChangePassword').click(function () {

        var Currentpass = $('#TxtCurrentPassword').val();
        var Newpass = $("#TxtNewPassword").val();
        var Confirmnewpass = $("#TxtConformNewPassword").val();
        if (Newpass == Confirmnewpass) {
            var dataa = {
                password: Currentpass,
                newpassword: Newpass
            }

            AjaxCaller("/Services/MethodHandler.aspx/ChangePassword", JSON.stringify(dataa), function (data) {
                var vars = $.parseJSON(data.d);

                if (vars["Error"] == "0") {
                    $("#changepasswordmsg").html(vars["Message"]).css("color", "green").removeClass("hidden");
                     $("#TxtNewPassword").val('');
                     $("#TxtConformNewPassword").val('');
                    
                }
                else {
                    $("#changepasswordmsg").html(vars["Message"]).css("color", "red").removeClass("hidden");
                  
                }
            });

        }
        else {
            $("#changepasswordmsg").html("Password did not match").css("color", "red").removeClass("hidden");
        }


    })

    // Register

    $("#btnShowRegisterModal").click(function () {
        $("#RegisterModal").modal('show')
    });

    $("#btnHideRegisterModal").click(function () {
        $("#RegisterModal").modal('hide');

    });

    $("#btnregister").click(function () {
        var dataa = {
            email: $("#TxtREmail").val(),
            password: $("#TxtRPassword").val()
        };
        if (dataa.password == $("#TxtRCPassword").val()) {
            AjaxCaller("/Services/MethodHandler.aspx/register", JSON.stringify(dataa), function (data) {
                var json = $.parseJSON(data.d);
                if (json["Error"] == "0") {
                    $("#message").html(json["Message"]).css("color", "green").removeClass("hidden");
                   $("#TxtREmail").val(''),
                    $("#TxtRPassword").val('')
                } else {
                    $("#message").html(json["Message"]).css("color", "red").removeClass("hidden");
                }
            });
        }
        else {
            $("#message").html(json["Message"]).css("color", "red").removeClass("hidden");
        }
    });

    $('#forgetPassword').click(function () {
        var dataa = {
            email: $("#txtForgotEmail").val()
        };
        AjaxCaller("/Services/MethodHandler.aspx/SendForgetPasswordLink", JSON.stringify(dataa), function (data) {
            var json = $.parseJSON(data.d);
            if (json["Error"] == "0") {
                $("#messageforgetpassword").html(json["Message"]).css("color", "green").removeClass("hidden");
            }
            else {
                $("#messageforgetpassword").html(json["Message"]).css("color", "red").removeClass("hidden");
            }
        })
    })
});
function AjaxCaller(urll, ddata, callback) {
    var canreturn = false;
    var result;
    var fun = $.ajax({
        async: false,
        type: "POST",
        url: urll,
        data: ddata,
        datatype: "json",
        contentType: 'application/json; charset=utf-8'
    });
    fun.fail(function (xhr) {
        alert("Error Occured (AjaxCaller)");
        return '{Error:"1",Message:"Internal Error ' + xhr.reponseText + '"}';
    });
    fun.done(function (data) {
        callback(data);
    });
};

function AjaxCallerGet(urll, ddata, callback) {
    var canreturn = false;
    var result;
    var fun = $.ajax({
        async: true,
        type: "GET",
        url: urll,
        data: ddata,
        datatype: "json",
        contentType: 'application/json; charset=utf-8'
    });
    fun.fail(function (xhr) {
        alert("Error Occured (AjaxCaller)");
        return '{Error:"1",Message:"Internal Error ' + xhr.reponseText + '"}';
    });
    fun.done(function (data) {
        callback(data);
    });
};

// to validate email
function ValidateEmail(email) {
    var expr = /^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/;
    return expr.test(email);
};

// validate Password
function ValidatePassword(password) {
    var expr = /(?=^[^\s]{8,10}$)(?=.*\d)(?=.*[a-zA-Z])/;
    return expr.test(password);
};

var emailvalidate = function (idofTextbox, idofEms) {
    var email = $(idofTextbox).val();

    if (!ValidateEmail(email)) {
        $(idofTextbox).css('border-color', 'darkred');
        $(idofEms).addClass("glyphicon-remove");
        $(idofEms).css('color', 'darkred');
        $(idofEms).removeClass("glyphicon-ok");
    }
    else {
        $(idofTextbox).css('border-color', 'green');
        $(idofEms).addClass("glyphicon-ok");
        $(idofEms).css('color', 'green');
        $(idofEms).removeClass("glyphicon-remove");
    }
}

var passwordvalidate = function (idofTextbox, idofspan) {
    var password = $(idofTextbox).val();
    if (!ValidatePassword(password)) {
        $(idofTextbox).css('border-color', 'darkred');
        $(idofspan).addClass("glyphicon-remove");
        $(idofspan).css('color', 'darkred');
        $(idofspan).removeClass("glyphicon-ok");
    }
    else {
        $(idofTextbox).css('border-color', 'green');
        $(idofspan).addClass("glyphicon-ok");
        $(idofspan).css('color', 'green');
        $(idofspan).removeClass("glyphicon-remove");

    }
}

function GetProductsBySubCategory(subcat, p,replace) {
    var dataa = {
        subcategory: subcat,
        page: p
    };

    $("#ProductListTitle").html = "'" + dataa.subcategory + "'";

    AjaxCaller("/Public/ViewProducts.aspx/GetProductsBySubCategory", JSON.stringify(dataa), function (data) {
        ShowProducts(data, replace);
    });
}

function Search(p,replace)
{
    var dataa = {
        searchterm: $("#searchtxtbox").val(),
        page:p
    };

    $("#ProductListTitle").html = "'"+dataa.searchterm + "'";

    AjaxCaller("/Public/ViewProducts.aspx/Search", JSON.stringify(dataa), function (data) {
        ShowProducts(data, replace);
    });
}

function ShowProducts(data,replace) {
    var products = $.parseJSON(data.d);
    var containerdiv = $(".productgrid");
    if (replace == true)
    {
        containerdiv.html("");
    }

    $.each(products, function (i, item) {
        //alert(products[i].Name);
        console.log(products[i]);
        containerdiv.html(containerdiv.html() + ProductView(products[i]) );
    });
}

function ProductView(item)
{
    return '<div class="product"><hidden ID="ProHdn" Value="' + item.Id + '" />' +
        '<img ID="proImg" runat="server" Class="productimage" src="' + "/ProductImage/"+ item.ImageUrl.ProductImagePath + '" />' +
        '<div class="productdescription">' +
        '<p class="h6"><strong>' + item.Name + '</strong></p>' +
        '<p><small>' + item.BrandName + '</small></p>' +
        '<a href="/Product/'+item.Id+'" class="buybtn">Buy Now</button>' +
        '</div>' +
        '</div>';
}

function getUrlVars() {
    debugger;
    var vars = [];
    var pathname = window.location.pathname.split("/");
    var filename = pathname[pathname.length - 1];
    return filename;
}