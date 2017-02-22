!function (e) {
    "use strict";
    var t = function () { };
    t.prototype.init = function () {
        e("#removeExclusion").on("click", function () {
            swal({
                title: "Are you sure?",
                text: "You want to remove this user from exclusion list!",
                type: "warning",
                showCancelButton: !0,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, remove user!",
                cancelButtonText: "No, cancel pls!",
                closeOnConfirm: !1,
                closeOnCancel: !1
            },
            function (e) {
                if (e) {
                    swal("Removed!", "User has been removed from exclusion list.", "success");
                    $(function () {
                        var selectedName = $('#selectedExcludedUser').val();
                        window.location.href = "RemoveUserExclusion?userName=" + selectedName;
                    });
                }
                else {
                    swal("Cancelled", "User is not removed from Exclusion list", "error");
                }   
            })
        }),
        e("#addExclusion").on("click", function () {
            swal({
                title: "Are you sure?",
                text: "You want to add this user to exclusion list!",
                type: "warning",
                showCancelButton: !0,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, add user!",
                cancelButtonText: "No, cancel pls!",
                closeOnConfirm: !1,
                closeOnCancel: !1
            },
            function (e) {
                if (e) {
                    swal("Added!", "User has been added to exclusion list.", "success");
                    $(function () {
                        var selectedName = $('#selectedunExcludedUser').val();
                        window.location.href = "AddUserExclusion?userName=" + selectedName;
                    });
                }
                else {
                    swal("Cancelled", "User is not added to Exclusion list", "error");
                }   
            })
        }),
        e("#help").on("click", function () {
            swal("HelpLine", "For feedback, contact: 1273158555 mail: demo@mail.com")
        }),
        e("#about").on("click",function(){
            swal({
                title:"Camperbot version 3.0",
                text:"camperbot",
                imageUrl: "../Images/logo_large.jpg"
            })
        }),
        e("#about2").on("click", function () {
            swal({
                title: "Camperbot version 3.0",
                text: "camperbot",
                imageUrl: "../Images/logo_large.jpg"
            })
        })
    }, e.SweetAlert = new t, e.SweetAlert.Constructor = t
}(window.jQuery), function (e) {
    "use strict"; e.SweetAlert.init()
}(window.jQuery);