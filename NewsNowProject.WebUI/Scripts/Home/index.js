var articleContainer = "#article-container";
var welcomeMessage = "#welcome-message-block";
var preloadItem = $("#preload-item");
var filterObject;

$(function () {
    sourceContent.push(advertisingContent);

    var notificationhub = $.connection.notificationHub;
    notificationhub.client.displayMessage = function (data) {
        if (data.Articles.length) {
            data.Articles.forEach(SetSourceData);

            if (!parameters.IsAllFiterSelected) {
                var filteredData = { Articles: [] };

                data.Articles.forEach(function (item) {
                    var result = $.grep(parameters.Filter, function (e) {
                        return e == item.SourceName;
                    });

                    if (result.length == 1) {
                        filteredData.Articles.push(item);
                    }
                });

                data.Articles = filteredData.Articles;
            }

            var newArticles = { Articles: [] };

            data.Articles.forEach(function (updateItem) {
                var item = $("#" + updateItem.ArticleID);
                if (item.length) {
                    item.attr("href", updateItem.Url);
                    item.find(".title").text(updateItem.Title);
                } else {
                    newArticles.Articles.push(updateItem);
                }
            });

            data.Articles = newArticles.Articles;

            $.tmpl("atml", data).prependTo(articleContainer);
        }

        //if (data.UpdateArticles.length) {
        //    data.UpdateArticles.forEach(function (updateItem) {
        //        var item = $("#" + updateItem.ArticleID);
        //        if (item.length)
        //        {
        //            item.attr("href", updateItem.Url);
        //            item.find(".title").text(updateItem.Title);
        //        }
        //    });
        //}
    };

    $.connection.hub.start();
    ReadCookiesValue()
    $("#articleTmpl").template("atml");
    LoadFilter();
    isAutoLoadEnabled = isAutoLoadEnabled == "null" ? false : true;
    if (!String.prototype.startsWith) {
        String.prototype.startsWith = function (searchString, position) {
            return this.substr(position || 0, searchString.length) === searchString;
        };
    }
});

function GetData() {
    if (isAutoLoadEnabled) {
        isAutoLoadEnabled = false;
        preloadItem.show();
        $.ajax({
            url: "/home/get",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(parameters),
            dataType: "json",
            success: function (data) {
                data.Articles.forEach(SetSourceData);
                $.tmpl("atml", data).appendTo(articleContainer);
                isAutoLoadEnabled = data.IsAutoLoadEnabled;
                parameters.NextArticleId = data.NextArticleId;
            },
            complete: function (data) {
                preloadItem.hide();
            }
        });
    } 
}

function SetSourceData(item) {
    var result = $.grep(sourceContent, function (e) {
        return e.Source == item.Source;
    });

    if (result.length == 1) {
        item.SourceTitle = result[0].Title;
        item.Icon = result[0].Icon;
        item.SourceName = result[0].SourceName;
    }
}

function GetDate(jsonDate) {
    var date = new Date(jsonDate.toString().startsWith("/Date") ? parseInt(jsonDate.substr(6)) : jsonDate);
    var options = {
        weekday: "long", month: "long", day: "numeric", hour: "2-digit", minute: "2-digit"
    };
    return date.toLocaleTimeString("uk-UA", options);
}

function ShowWelcomeMessage() {
    //if (filterObject.welcome)
    //{
    //    $(welcomeMessage).modal();
    //}
    SetCookies(null, false);
}

$(window).scroll(function () {
    if ($(window).scrollTop() + 120 > $(document).height() - $(window).height()) {
        GetData();
    }
});

$(document).on("mouseenter", '.article-item-active', function () {
    $(this).removeClass("article-item-active");
});

$(document).ready(function(){
    $(window).scroll(function(){
        if ($(this).scrollTop() > 100) {
            $('.scrollup').fadeIn();
        } else {
            $('.scrollup').fadeOut();
        }
    });
 
    $('.scrollup').click(function(){
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
});

/*-------------Filter-------------*/

function LoadFilter() {
    var filterHeader = $("#filter-header");
    var filterBody = $("#filter-body");
    var articleBox = $(articleContainer);
    var sourceIconItems =  $(".source-icon-item");

    $("#open-filter-btn-item").click(function () {
        filterBody.show();
        filterHeader.hide();
    });

    $("#accept-filter-btn-item").click(function () {
        var filterParametr = [];

        $("input[name=filter]:checked").each(function () {
            filterParametr.push($(this).val());
        });

        CheckFilter(filterParametr);

        filterBody.hide();
        filterHeader.show();
    });

    $("#close-filter-btn-item").click(function () {
        filterBody.hide();
        filterHeader.show();
    });

    function CheckFilter(filterParametr) {
        if (!ArraysEqual(parameters.Filter, filterParametr)) {
            parameters.Filter = filterParametr;
            sourceIconItems.addClass("display-none");
            parameters.Filter.forEach(setVisibleFilterIcon);
            SetCookies(parameters.Filter);
            CheckIsAllFiterSelected();
            articleBox.empty();
            parameters.NextArticleId = null;
            isAutoLoadEnabled = true;
            GetData();
        }
    }
}

function setVisibleFilterIcon(item) {
    $("#source-icon-item-" + item).removeClass("display-none");
}

function ArraysEqual(a, b) {
    if (a === b) return true;
    if (a == null || b == null) return false;
    if (a.length != b.length) return false;

    for (var i = 0; i < a.length; ++i) {
        if (a[i] !== b[i]) return false;
    }
    return true;
}

function ReadCookiesValue() {
    filterObject = JSON.parse($.cookie(filterCookiesName));
    parameters.Filter = filterObject.filter;
    CheckIsAllFiterSelected();
    ShowWelcomeMessage();
}

function SetCookies(filter, welcome) {
    if (filter != null) {
        filterObject.filter = filter;
    }

    if (welcome != null)
    {
        filterObject.welcome = welcome;
    }

    $.cookie(filterCookiesName, JSON.stringify(filterObject), { path: '/', expires: 365 });
}

function CheckIsAllFiterSelected()
{
    parameters.IsAllFiterSelected = parameters.Filter.length == sourceContentCount || parameters.Filter.length == 0 ? true : false;
    parameters.IsAllFiterSelected ? $("#filter-icon-panel").hide() : $("#filter-icon-panel").show();
}

/*----------End-filter-----------*/