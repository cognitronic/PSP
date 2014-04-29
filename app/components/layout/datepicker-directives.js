/**
 * Created by Danny Schreiber on 4/28/14.
 */


app.directive( 'ccDatePicker', ['dateFilter', '$parse', 'datepickerConfig', '$log', function (dateFilter, $parse, datepickerConfig, $log) {
    return {
        restrict: 'EA',
        replace: true,
        templateUrl: 'template/datepicker/datepicker.html',
        scope: {
            dateDisabled: '&'
        },
        require: ['ccDatePicker', '?^ngModel'],
        controller: 'DatepickerController',
        link: function(scope, element, attrs, ctrls) {
            var datepickerCtrl = ctrls[0], ngModel = ctrls[1];

            if (!ngModel) {
                return; // do nothing if no ng-model
            }

            // Configuration parameters
            var mode = 0, selected = new Date(), showWeeks = datepickerConfig.showWeeks;

            if (attrs.showWeeks) {
                scope.$parent.$watch($parse(attrs.showWeeks), function(value) {
                    showWeeks = !! value;
                    updateShowWeekNumbers();
                });
            } else {
                updateShowWeekNumbers();
            }

            if (attrs.min) {
                scope.$parent.$watch($parse(attrs.min), function(value) {
                    datepickerCtrl.minDate = value ? new Date(value) : null;
                    refill();
                });
            }
            if (attrs.max) {
                scope.$parent.$watch($parse(attrs.max), function(value) {
                    datepickerCtrl.maxDate = value ? new Date(value) : null;
                    refill();
                });
            }

            function updateShowWeekNumbers() {
                scope.showWeekNumbers = mode === 0 && showWeeks;
            }

            // Split array into smaller arrays
            function split(arr, size) {
                var arrays = [];
                while (arr.length > 0) {
                    arrays.push(arr.splice(0, size));
                }
                return arrays;
            }

            function refill( updateSelected ) {
                var date = null, valid = true;

                if ( ngModel.$modelValue ) {
                    date = new Date( ngModel.$modelValue );

                    if ( isNaN(date) ) {
                        valid = false;
                        $log.error('Datepicker directive: "ng-model" value must be a Date object, a number of milliseconds since 01.01.1970 or a string representing an RFC2822 or ISO 8601 date.');
                    } else if ( updateSelected ) {
                        selected = date;
                    }
                }
                ngModel.$setValidity('date', valid);

                var currentMode = datepickerCtrl.modes[mode], data = currentMode.getVisibleDates(selected, date);
                angular.forEach(data.objects, function(obj) {
                    obj.disabled = datepickerCtrl.isDisabled(obj.date, mode);
                });

                ngModel.$setValidity('date-disabled', (!date || !datepickerCtrl.isDisabled(date)));

                scope.rows = split(data.objects, currentMode.split);
                scope.labels = data.labels || [];
                scope.title = data.title;
            }

            function setMode(value) {
                mode = value;
                updateShowWeekNumbers();
                refill();
            }

            ngModel.$render = function() {
                refill( true );
            };

            scope.select = function( date ) {
                if ( mode === 0 ) {
                    var dt = ngModel.$modelValue ? new Date( ngModel.$modelValue ) : new Date(0, 0, 0, 0, 0, 0, 0);
                    dt.setFullYear( date.getFullYear(), date.getMonth(), date.getDate() );
                    ngModel.$setViewValue( dt );
                    refill( true );
                } else {
                    selected = date;
                    setMode( mode - 1 );
                }
            };
            scope.move = function(direction) {
                var step = datepickerCtrl.modes[mode].step;
                selected.setMonth( selected.getMonth() + direction * (step.months || 0) );
                selected.setFullYear( selected.getFullYear() + direction * (step.years || 0) );
                refill();
            };
            scope.toggleMode = function() {
                setMode( (mode + 1) % datepickerCtrl.modes.length );
            };
            scope.getWeekNumber = function(row) {
                return ( mode === 0 && scope.showWeekNumbers && row.length === 7 ) ? getISO8601WeekNumber(row[0].date) : null;
            };

            function getISO8601WeekNumber(date) {
                var checkDate = new Date(date);
                checkDate.setDate(checkDate.getDate() + 4 - (checkDate.getDay() || 7)); // Thursday
                var time = checkDate.getTime();
                checkDate.setMonth(0); // Compare with Jan 1
                checkDate.setDate(1);
                return Math.floor(Math.round((time - checkDate) / 86400000) / 7) + 1;
            }
        }
    };
}]);

    app.constant('ccDatepickerPopupConfig', {
        dateFormat: 'yyyy-MM-dd',
        currentText: 'Today',
        toggleWeeksText: 'Weeks',
        clearText: 'Clear',
        closeText: 'Done',
        closeOnDateSelection: true,
        appendToBody: false,
        showButtonBar: true,
        enableOnFocus: false
    });

    app.directive('ccDatepickerPopup', ['$compile', '$parse', '$document', 'ccWidgetPositionService', 'dateFilter', 'ccDatepickerPopupConfig', 'datepickerConfig',
        function ($compile, $parse, $document, ccWidgetPositionService, dateFilter, ccDatepickerPopupConfig, datepickerConfig) {
            return {
                restrict: 'EA',
                require: 'ngModel',
                link: function(originalScope, element, attrs, ngModel) {
                    var scope = originalScope.$new(), // create a child scope so we are not polluting original one
                        dateFormat,
                        closeOnDateSelection = angular.isDefined(attrs.closeOnDateSelection) ? originalScope.$eval(attrs.closeOnDateSelection) : ccDatepickerPopupConfig.closeOnDateSelection,
                        appendToBody = angular.isDefined(attrs.datepickerAppendToBody) ? originalScope.$eval(attrs.datepickerAppendToBody) : ccDatepickerPopupConfig.appendToBody;

                    attrs.$observe('ccDatepickerPopup', function(value) {
                        dateFormat = value || ccDatepickerPopupConfig.dateFormat;
                        ngModel.$render();
                    });

                    scope.showButtonBar = angular.isDefined(attrs.showButtonBar) ? originalScope.$eval(attrs.showButtonBar) : ccDatepickerPopupConfig.showButtonBar;

                    scope.enableOnFocus = angular.isDefined(attrs.enableOnFocus) ? originalScope.$eval(attrs.enableOnFocus) : ccDatepickerPopupConfig.enableOnFocus;

                    originalScope.$on('$destroy', function() {
                        $popup.remove();
                        scope.$destroy();
                    });

                    attrs.$observe('currentText', function(text) {
                        scope.currentText = angular.isDefined(text) ? text : ccDatepickerPopupConfig.currentText;
                    });
                    attrs.$observe('toggleWeeksText', function(text) {
                        scope.toggleWeeksText = angular.isDefined(text) ? text : ccDatepickerPopupConfig.toggleWeeksText;
                    });
                    attrs.$observe('clearText', function(text) {
                        scope.clearText = angular.isDefined(text) ? text : ccDatepickerPopupConfig.clearText;
                    });
                    attrs.$observe('closeText', function(text) {
                        scope.closeText = angular.isDefined(text) ? text : ccDatepickerPopupConfig.closeText;
                    });

                    var getIsOpen, setIsOpen;
                    if ( attrs.isOpen ) {
                        getIsOpen = $parse(attrs.isOpen);
                        setIsOpen = getIsOpen.assign;

                        originalScope.$watch(getIsOpen, function updateOpen(value) {
                            scope.isOpen = !! value;
                        });
                    }
                    scope.isOpen = getIsOpen ? getIsOpen(originalScope) : false; // Initial state

                    function setOpen( value ) {
                        if (setIsOpen) {
                            setIsOpen(originalScope, !!value);
                        } else {
                            scope.isOpen = !!value;
                        }
                    }

                    var documentClickBind = function(event) {
                        if (scope.isOpen && event.target !== element[0]) {
                            scope.$apply(function() {
                                setOpen(false);
                            });
                        }
                    };

                    var elementFocusBind = function() {
                        scope.$apply(function() {
                            if(scope.enableOnFocus){
                                setOpen( true );
                            }
                        });
                    };

                    // popup element used to display calendar
                    var popupEl = angular.element('<div datepicker-popup-wrap><div datepicker></div></div>');
                    popupEl.attr({
                        'ng-model': 'date',
                        'ng-change': 'dateSelection()'
                    });
                    var datepickerEl = angular.element(popupEl.children()[0]),
                        datepickerOptions = {};
                    if (attrs.datepickerOptions) {
                        datepickerOptions = originalScope.$eval(attrs.datepickerOptions);
                        datepickerEl.attr(angular.extend({}, datepickerOptions));
                    }

                    // TODO: reverse from dateFilter string to Date object
                    function parseDate(viewValue) {
                        if (!viewValue) {
                            ngModel.$setValidity('date', true);
                            return null;
                        } else if (angular.isDate(viewValue)) {
                            ngModel.$setValidity('date', true);
                            return viewValue;
                        } else if (angular.isString(viewValue)) {
                            var date = new Date(viewValue);
                            if (isNaN(date)) {
                                ngModel.$setValidity('date', false);
                                return undefined;
                            } else {
                                ngModel.$setValidity('date', true);
                                return date;
                            }
                        } else {
                            ngModel.$setValidity('date', false);
                            return undefined;
                        }
                    }
                    ngModel.$parsers.unshift(parseDate);

                    // Inner change
                    scope.dateSelection = function(dt) {
                        if (angular.isDefined(dt)) {
                            scope.date = dt;
                        }
                        var newDay, newMonth;
                        if(scope.date){
                            scope.date.getDate() < 10 ? newDay = '0' + scope.date.getDate() : newDay = scope.date.getDate();
                            (scope.date.getMonth() + 1) < 10 ? newMonth = '0' + (scope.date.getMonth() + 1) : newMonth = scope.date.getMonth() + 1;
                            scope.date = new Date((newMonth + '/' + newDay + '/' + scope.date.getFullYear()));
                            ngModel.$setViewValue(scope.date);
                            ngModel.$render();
                        }

                        if (closeOnDateSelection) {
                            setOpen( false );
                        }
                    };

                    element.bind('input change keyup', function() {
                        scope.$apply(function() {
                            scope.date = ngModel.$modelValue;
                        });
                    });

                    // Outter change
                    ngModel.$render = function() {
                        var date = ngModel.$viewValue ? dateFilter(ngModel.$viewValue, dateFormat) : '';
                        if(date !== '12/31/1969'){
                            element.val(date);
                            scope.date = ngModel.$modelValue;
                        }
                    };

                    function addWatchableAttribute(attribute, scopeProperty, datepickerAttribute) {
                        if (attribute) {
                            originalScope.$watch($parse(attribute), function(value){
                                scope[scopeProperty] = value;
                            });
                            datepickerEl.attr(datepickerAttribute || scopeProperty, scopeProperty);
                        }
                    }
                    addWatchableAttribute(attrs.min, 'min');
                    addWatchableAttribute(attrs.max, 'max');
                    if (attrs.showWeeks) {
                        addWatchableAttribute(attrs.showWeeks, 'showWeeks', 'show-weeks');
                    } else {
                        scope.showWeeks = 'show-weeks' in datepickerOptions ? datepickerOptions['show-weeks'] : datepickerConfig.showWeeks;
                        datepickerEl.attr('show-weeks', 'showWeeks');
                    }
                    if (attrs.dateDisabled) {
                        datepickerEl.attr('date-disabled', attrs.dateDisabled);
                    }

                    function updatePosition() {
                        scope.position = appendToBody ? ccWidgetPositionService.offset(element) : ccWidgetPositionService.position(element);
                        scope.position.top = scope.position.top + element.prop('offsetHeight');
                    }

                    var documentBindingInitialized = false, elementFocusInitialized = false;
                    scope.$watch('isOpen', function(value) {
                        if (value) {
                            updatePosition();
                            $document.bind('click', documentClickBind);
                            if(elementFocusInitialized) {
                                element.unbind('focus', elementFocusBind);
                            }
                            element[0].focus();
                            documentBindingInitialized = true;
                        } else {
                            if(documentBindingInitialized) {
                                $document.unbind('click', documentClickBind);
                            }
                            element.bind('focus', elementFocusBind);
                            elementFocusInitialized = true;
                        }

                        if ( setIsOpen ) {
                            setIsOpen(originalScope, value);
                        }
                    });

                    scope.today = function() {
                        scope.dateSelection(new Date());
                    };
                    scope.clear = function() {
                        scope.dateSelection(null);
                    };

                    var $popup = $compile(popupEl)(scope);
                    if ( appendToBody ) {
                        $document.find('body').append($popup);
                    } else {
                        element.after($popup);
                    }
                }
            };
        }]);

    app.directive('ccDatepickerPopupWrap', function() {
        return {
            restrict:'EA',
            replace: true,
            transclude: true,
            templateUrl: 'template/datepicker/popup.html',
            link:function (scope, element, attrs) {
                element.bind('click', function(event) {
                    event.preventDefault();
                    event.stopPropagation();
                });
            }
        };
    });

angular.module("template/datepicker/datepicker.html", []).run(["$templateCache", function($templateCache) {
    $templateCache.put("template/datepicker/datepicker.html",
        "<table>\n" +
            "  <thead>\n" +
            "    <tr>\n" +
            "      <th><button type=\"button\" class=\"btn btn-default btn-sm pull-left\" ng-click=\"move(-1)\"><i class=\"glyphicon glyphicon-chevron-left\"></i></button></th>\n" +
            "      <th colspan=\"{{rows[0].length - 2 + showWeekNumbers}}\"><button type=\"button\" class=\"btn btn-default btn-sm btn-block\" ng-click=\"toggleMode()\"><strong>{{title}}</strong></button></th>\n" +
            "      <th><button type=\"button\" class=\"btn btn-default btn-sm pull-right\" ng-click=\"move(1)\"><i class=\"glyphicon glyphicon-chevron-right\"></i></button></th>\n" +
            "    </tr>\n" +
            "    <tr ng-show=\"labels.length > 0\" class=\"h6\">\n" +
            "      <th ng-show=\"showWeekNumbers\" class=\"text-center\">#</th>\n" +
            "      <th ng-repeat=\"label in labels\" class=\"text-center\">{{label}}</th>\n" +
            "    </tr>\n" +
            "  </thead>\n" +
            "  <tbody>\n" +
            "    <tr ng-repeat=\"row in rows\">\n" +
            "      <td ng-show=\"showWeekNumbers\" class=\"text-center\"><em>{{ getWeekNumber(row) }}</em></td>\n" +
            "      <td ng-repeat=\"dt in row\" class=\"text-center\">\n" +
            "        <button type=\"button\" style=\"width:100%;\" class=\"btn btn-default btn-sm\" ng-class=\"{'btn-info': dt.selected}\" ng-click=\"select(dt.date)\" ng-disabled=\"dt.disabled\"><span ng-class=\"{'text-muted': dt.secondary}\">{{dt.label}}</span></button>\n" +
            "      </td>\n" +
            "    </tr>\n" +
            "  </tbody>\n" +
            "</table>\n" +
            "");
}]);

angular.module("template/datepicker/popup.html", []).run(["$templateCache", function($templateCache) {
    $templateCache.put("template/datepicker/popup.html",
        "<ul class=\"dropdown-menu\" ng-style=\"{display: (isOpen && 'block') || 'none', top: position.top+'px', left: position.left+'px'}\">\n" +
            "	<li ng-transclude></li>\n" +
            "	<li ng-show=\"showButtonBar\" style=\"padding:10px 9px 2px\">\n" +
            "		<span class=\"btn-group\">\n" +
            "			<button type=\"button\" class=\"btn btn-sm btn-info\" ng-click=\"today()\">{{currentText}}</button>\n" +
            "			<button type=\"button\" class=\"btn btn-sm btn-default\" ng-click=\"showWeeks = ! showWeeks\" ng-class=\"{active: showWeeks}\">{{toggleWeeksText}}</button>\n" +
            "			<button type=\"button\" class=\"btn btn-sm btn-danger\" ng-click=\"clear()\">{{clearText}}</button>\n" +
            "		</span>\n" +
            "		<button type=\"button\" class=\"btn btn-sm btn-success pull-right\" ng-click=\"isOpen = false\">{{closeText}}</button>\n" +
            "	</li>\n" +
            "</ul>\n" +
            "");
}]);