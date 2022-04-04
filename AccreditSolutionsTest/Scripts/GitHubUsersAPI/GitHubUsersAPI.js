$(function () {
    GitHubUsersAPI.on_load();
});

var GitHubUsersAPI = {
    on_load: function () {
        $("#getUserUserNameInput").on("input", function () {
            if ($(this).val().length > 0)
                $("#getUserSubmitbtn").attr("disabled", false);
            else
                $("#getUserSubmitbtn").attr("disabled", true);

            if ($(this).val().length > 0 || $("#userResultTable tbody tr").length > 0)
                $("#getUserClearbtn").attr("disabled", false);
            else
                $("#getUserClearbtn").attr("disabled", true);

        }),
            $("#getUserClearbtn").on("click", function () {
                $("#getUserUserNameInput").val("")
                GitHubUsersAPI.InitPage();
                $("#getUserClearbtn").attr("disabled", true);
                $("#getUserSubmitbtn").attr("disabled", true);
            });
        $("#getUserSubmitbtn").on("click", function () {
            GitHubUsersAPI.InitPage();
            $.ajax({
                url: "../../GitHubUsersAPI/GetUserDetails",
                type: "POST",
                async: true,
                data: { username: $("#getUserUserNameInput").val()},
                success: function (response) {
                    if (response.IsSuccess) {
                        $("#resultUserName").text(response.userDetailsModel.login);
                        $("#resultLocation").text(((response.userDetailsModel.location == null || response.userDetailsModel.location.trim() == "") ? "Not Found" : response.userDetailsModel.location));
                        $("#resultavatarImage").attr("src", response.userDetailsModel.avatar_url);
                        $("#resultavatarImage").css("display", "")
                        $("#userResultSection").css("display", "")
                        if (response.userDetailsModel.userReposList != null) {
                            $("#userResultWarningMessage").css("display", "none")
                            for (var x = 0; x < response.userDetailsModel.userReposList.length; x++) {
                                $("#userResultTable tbody").append("<tr> <th>" + response.userDetailsModel.userReposList[x].name + "</th><th>"
                                    + response.userDetailsModel.userReposList[x].html_url + "</th> <th>"
                                    + response.userDetailsModel.userReposList[x].description + "</th>" +
                                    "<th>" + response.userDetailsModel.userReposList[x].stargazers_count + "</th>" + "</tr>")
                            }
                        } else {
                            $("#userResultWarningMessage").css("display","")
                        }
                    } else {
                        $("#UserApiErrorMessages").text(response.message)
                        $("#UserApiErrorMessages").css("display", "")
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        });

    },
       InitPage: function () {
        $("#userResultTable tbody").html("")
        $("#resultUserName").text("")
        $("#resultLocation").text("")
        $("#resultavatarImage").attr("src", "");
        $("#resultavatarImage").css("display", "none");
        $("#UserApiErrorMessages").css("display", "none")
        $("#userResultSection").css("display", "none")
    },
}