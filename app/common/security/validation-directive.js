/**
 * Created by Danny Schreiber on 4/29/14.
 */

ramAngularApp.module.directive('fieldValidate',['Constants',function(Constants) {
    return {
        // restrict to an attribute type.
        restrict: 'A',

        // element must have ng-model attribute.
        require: 'ngModel',

        // scope = the parent scope
        // elem = the element the directive is on
        // attr = a dictionary of attributes on the element
        // ctrl = the controller for ngModel.
        link: function (scope, elem, attr, ctrl, ngModel) {




            var markedRequired = false;

            if(attr.isrequired === "true"){
                markedRequired=true;
            }

            var controlGroup = true;

            if(attr.controlgroup === "false"){
                controlGroup=false;
            }

            // type of validation
            var type = attr.fieldValidate;

            var valid;

            if(markedRequired==true){
                valid = false;
            }else{
                valid = true;
            }

            var errorText = "";

            var invalidateBlanks = attr.invalidBlank;

            /*
             the maskRegExs is used to evaluate where delimiters should be placed. Since some masking has irregular delimit spots (not every three or every two sequences of numbers) there are multiple regexs applied.
             delimiter is the delimiter that is used within masking function
             occurrences is the number of delimits that should be placed within field input

             The validityFunction is the function called to run the validity checks for each type. This allows types to have more than one type of check. The validity function
             should return a boolean if the field is valid and also set the errorText variable to the message to be displayed.
             */

            if (type === "phone") {
                var validityFunction = function(value){
                    //var validityRegEx = new RegExp(/^[2-9]\d{2}-\d{3}-\d{4}$/);
                    var validityRegEx = new RegExp(/^\(\d{3}\)\s\d{3}-\d{4}$/);
                    //test for formatting
                    valid = validityRegEx.test(value);
                    if(!valid){
                        errorText = Constants.ERR_TXT.PHONE;
                        return false;
                    }
                    return true;
                };
                var maskRegExs = new Array(/\d{3}/g);
                var occurences = 2;
                var delimiter = null;
                var maskLead = '(';
                var delimiters = new Array(') ','-');
            } else if (type === "ssn") {
                var maskRegExs = new Array(/\d{3}/g, /\d{2}/g);
                var occurences = 2;
                var delimiter = '-';
                var validityFunction = function(value){
                    var validityRegEx = new RegExp(/^\d{3}-\d{2}-\d{4}$/);
                    //test for formatting
                    valid = validityRegEx.test(value);

                    if(!valid){
                        errorText = Constants.ERR_TXT.SSN;
                        return false;
                    }
                    return true;
                };
            } else if (type === "email"){
                var validityFunction = function(value){
                    var validityRegEx = new RegExp(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/);
                    //test for formatting
                    valid = validityRegEx.test(value);

                    if(!valid){
                        errorText = Constants.ERR_TXT.EMAIL;
                        return false;
                    }
                    //console.log('**** in email validityFunction VALUE: '+value)
                    //console.log('**** in email validityFunction VALID: '+valid)
                    return true;
                };
            } else if (type === "url"){
                var validityFunction = function(value){
                    var validityRegEx = new RegExp(/((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/);
                    //test for formatting
                    valid = validityRegEx.test(value);

                    if(!valid){
                        errorText = Constants.ERR_TXT.URL;
                        return false;
                    }

                    return true;
                };
            } else if (type === "zip"){
                var validityFunction = function(value){
                    var validityRegEx = new RegExp(/^\d{5}(?:[-\s]\d{4})?$/);
                    //test for formatting
                    valid = validityRegEx.test(value);

                    if(!valid){
                        errorText = Constants.ERR_TXT.ZIP;
                        return false;
                    }

                    return true;
                };
            } else if(type==="date"){
                var maskRegExs = new Array(/(0[1-9]|1[0-2])/g,/(0[1-9]|1\d|2\d|3[01])/g);
                var occurences = 2;
                var delimiter = '/';
                var validityFunction = function(value){
                    var validityRegEx = new RegExp(/^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/);
                    //test for formatting
                    valid = validityRegEx.test(value);

                    if(!valid){
                        errorText = Constants.ERR_TXT.DATE;
                        return false;
                    }
                    return true;
                };
            } else if(type === "dob"){
                var maskRegExs = new Array(/(0[1-9]|1[0-2])/g,/(0[1-9]|1\d|2\d|3[01])/g);
                var occurences = 2;
                var delimiter = '/';
                var validityFunction = function(value){
                    var validityRegEx = new RegExp(/^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/);
                    //test for formatting
                    valid = validityRegEx.test(value);

                    if(!valid){
                        errorText = Constants.ERR_TXT.DATE;
                        return false;
                    }

                    //test for a date in the future
                    var today = new Date();
                    var date = Date.parse(value);

                    if(date > today){
                        errorText = Constants.ERR_TXT.FUTURE_DATE;
                        return false;
                    }
                    return true;
                };
            } else if(type === "password"){
                var validityFunction = function(value){
                    //test for minimum length
                    if(value.length < 7){
                        errorText = Constants.ERR_TXT.PASSWORD.MIN_LENGTH;
                        return false;
                    }
                    //test for maximum length
                    if(value.length > 50){
                        errorText = Constants.ERR_TXT.PASSWORD.MAX_LENGTH;
                        return false;
                    }
                    //test for illegal characters
                    var validityRegex = new RegExp(/.*[^0-9a-zA-Z].*/);
                    if(validityRegex.test(value)){
                        errorText = Constants.ERR_TXT.PASSWORD.ILLEGAL_CHARS;
                        return false;
                    }
                    //test to make sure there is at least one digit
                    validityRegex = new RegExp(/.*[0-9]+.*$/);
                    if(!validityRegex.test(value)){
                        errorText = Constants.ERR_TXT.PASSWORD.CONTAINS_DIGIT;
                        return false;
                    }
                    //test to make sure there is at least one letter
                    validityRegex = new RegExp(/.*[a-zA-Z]+.*$/);
                    if(!validityRegex.test(value)){
                        errorText = Constants.ERR_TXT.PASSWORD.CONTAINS_LETTER;
                        return false;
                    }

                    return true;
                };
            } else {
                var validityFunction = function(value){
                    if (value) return true;
                };
            }


            // Searches recursively up the chain to find the DIV with the control-group class
            function findControlGroupElement(e) {
                if (e.hasClass("control-group"))
                    return e;
                else
                    return findControlGroupElement(e.parent());
            };

            function requiredRoutine(){
                $(elem).attr('title', Constants.ERR_TXT.REQUIRED);
                if(controlGroup){
                    findControlGroupElement(elem.parent()).addClass('has-error');
                }
                else{
                    elem.addClass('error');
                }
            };

            function invalidRoutine(){
                $(elem).attr('title', errorText);
                if(controlGroup){
                    findControlGroupElement(elem.parent()).addClass('has-error');
                }
                else{
                    elem.addClass('has-error');
                }
            };

            function cleanupRoutine(){
                $(elem).attr('title',"");
                if(controlGroup){
                    findControlGroupElement(elem.parent()).removeClass('has-error');
                }
                else{
                    elem.removeClass('has-error');
                }
            };


            //handle displaying of current validity state
            function displayApprError(){
                if(markedRequired && !ctrl.$viewValue){
                    //console.log('displayApprError 1')
                    cleanupRoutine();
                    requiredRoutine();
                }
                else if(valid==false && ctrl.$viewValue && type!==""){
                    cleanupRoutine();
                    invalidRoutine();
                }
                else{
                    //console.log('displayApprError 4')
                    cleanupRoutine();
                }
            }

            //handle setting appr. validtiy for current state
            function setApprValidity(){
                ctrl.$setValidity('regexValidate',valid);
                scope[modelString] = valid;
            }

            //things that should happen on blur
            elem.bind('blur', function () {
                setApprValidity();
                displayApprError();
            });



            var modelName = attr.ngModel;

            //creates validity flag in parent scope with model name hanging off of the scope and the flag hanging off of that object
            function createValidFlagInParentScope(modelName){

                var wordsInModelName = modelName.split(".");

                var modelString = "";

                for(var i =0; i<wordsInModelName.length;i++){
                    modelString+=wordsInModelName[i];
                }

                modelString += "validityFlag";

                scope.modelString = true;

                return modelString;

            }

            var modelString = createValidFlagInParentScope(modelName);
            var cachedValue="";
            //GOTTA WATCH THE MODEL so that we can reset the flags :)
            scope.$watch(attr.ngModel, function () {
                if(markedRequired==true && !ctrl.$viewValue){
                    if(invalidateBlanks == 'true'){
                        ctrl.$setValidity('regexValidate', false);
                        scope[modelString] = false;
                        cleanupRoutine();
                        invalidRoutine();
                    }
                    else {
                        ctrl.$setValidity('regexValidate', false);
                        scope[modelString] = false;
                        cleanupRoutine();

                    }
                }
                else if (ctrl.$viewValue){
                    valid = validityFunction(ctrl.$viewValue);
                    setApprValidity();
                    displayApprError();
                }
            });


            var cachedValue="";
            // add a parser that will process each time the value is
            // parsed into the model when the user updates it.
            ctrl.$parsers.unshift(function (value) {
                //test and set the validity after update.


                //    cleanupRoutine();
                if(!ctrl.$viewValue && !markedRequired){
                    valid = true;
                }
                else if(type!==""){
                    //send validation check to the function setup by the type definition
                    valid = validityFunction(value);
                }else if(markedRequired==true && !ctrl.$viewValue){
                    valid = false;
                }

                setApprValidity();

                if(maskRegExs && value && (cachedValue.length<value.length)){
                    newValue = maskValue(value);
                }

                //sets the cached value
                cachedValue = value;


                if(valid || (type==="" && markedRequired && ctrl.$viewValue)){
                    return value;

                }

            });




            //function to add mask to data
            function maskValue(value) {
                // if (delimiter) var delimitExp = new RegExp(delimiter, "g");
                //    else if (delimiters) var delimitExp = new RegExp(delimiters, "g");

                value = value.replace(/[^\w\s]/gi, '')
                //value = value.replace(delimitExp, "");

                var noMatches = false;

                var newValue = "";

                var regExCount = 0;
                var loopCount = 0;

                //throughout loop
                var workValue = value;

                for (var i = 0; i < occurences; i++) {
                    var matches = workValue.match(maskRegExs[regExCount]);
                    if (!matches) {
                        break;
                    }

                    if (delimiter) newValue += matches[0] + delimiter;
                    else if (delimiters){
                        newValue += matches[0] + delimiters[i];
                    }
                    if ((regExCount + 1) < maskRegExs.length) {
                        regExCount++;
                    }
                    loopCount++;
                    workValue = workValue.substring(matches[0].length);
                    if (workValue === matches[0]) {
                        break;
                    }
                }


                newValue += value.substring(newValue.length - loopCount, value.length);
                if (maskLead) newValue = maskLead + newValue;
                //  console.log(newValue.length)
                //  console.log(newValue.substr(0,14))
                // if (newValue.length >= 14) newValue = newValue.substr(0,14)
                newValue = newValue.substr(0,14)
                ctrl.$viewValue = newValue;
                ctrl.$render();

                return newValue;
            }
        }
    };
}]);