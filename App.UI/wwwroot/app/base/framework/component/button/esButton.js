var app = angular.module('esButton',[]);

app.directive('esBtn', function () {
    return {
        restrict: "A",
        replace: false,
        compile: function (element, attributes) {

            return {
                pre: function (scope, element, attrs, controller, transcludeFn) {
                    
                    var param = {};
                    param.btnColor = attrs.btnColor;
                    param.btnSize = attrs.btnSize;
                    param.btnIcon = attrs.btnIcon;
                    param.btnText = attrs.btnText;
                    param.btnImgAlt = attrs.btnImgAlt;
                    param.btnSrcImg = attrs.btnSrcImg;
                    
                    if (angular.isDefined(param.btnColor)) {
                        element.addClass("btn btn-" + param.btnColor);
                    } else {
                        element.addClass("btn btn-default");
                    }
                    if (angular.isDefined(param.btnSize)) {
                        element.addClass("btn-" + param.btnSize);
                    }
                    if (angular.isDefined(param.btnIcon)) {
                        var iconElem = angular.element('<i></i>');
                        iconElem.css("vertical-align", "middle");
                        if (element.text().trim()) {
                            iconElem.addClass("es-btn-margin");
                        }
                        iconElem.addClass(param.btnIcon);
                        iconElem.addClass("btn-content"); //in LTR theme
                        element.prepend(iconElem);
                    }
                    if (angular.isDefined(param.btnText)) {
                        var textElem = angular.element('<label></label>');
                        textElem.text("{{"+param.btnText+"}}");
                        //in LTR theme
                        element.append(textElem);
                    }
                    if (angular.isDefined(param.btnSrcImg)) { // image
                        var imgElem = angular.element('<img>');
                        imgElem.attr("src", param.btnSrcImg);
                        imgElem.css("vertical-align", "middle");
                        if (element.text().trim()) {
                            imgElem.addClass("es-btn-margin");
                        }

                        if (angular.isDefined(param.btnImgAlt)) {
                            imgElem.attr("alt", param.btnImgAlt);
                        }
                        var height = "21px";
                        if (param.btnSize == "lg") {
                            height = "25px";
                        }
                        else if (param.btnSize == "xs" || param.btnSize == "sm") {
                            height = "19px";
                        }
                        imgElem.css("height", height);

                        element.prepend(imgElem);
                    }
                },
                post: function (scope, element, attributes, controller, transcludeFn) {

                }
            }
        },
        link: function (scope, element, attrs) {

        }
    }
});