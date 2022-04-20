/*

* Price Format jQuery Plugin
* Created By Eduardo Cuducos
* Currently maintained by Flavio Silveira flavio [at] gmail [dot] com
* Version: 2.0
* Release: 2014-01-26

*/

(function ($) {
    /****************
    * Main Function *
    *****************/
    $.fn.rgFormat = function (options) {
        let defaults =
        {
            id: ''
        };

        let option = $.extend(defaults, options);

        function rgFormat() {
            let input = document.getElementById(option.id);

            input.value = input.value.replace(/\D/g, "");
            input.value = input.value.replace(/(\d)(\d{4})$/, "$1.$2");
            input.value = input.value.replace(/(\d)(\d{4})$/, "$1.$2");
            input.value = input.value.replace(/(\d)(\d)$/, "$1-$2");
            input.value = input.value.replace(/(-\d{3})\d+?$/, '$1');

            return;
        }

        return rgFormat();
    }

    $.fn.cepFormat = function (options) {
        let defaults =
        {
            id: ''
        };

        let option = $.extend(defaults, options);

        function cepFormat() {
            let input = document.getElementById(option.id);

            input.value = input.value.replace(/\D/g, "");
            input.value = input.value.replace(/^(\d{2})(\d)/, "$1.$2");
            input.value = input.value.replace(/\.(\d{3})(\d)/, ".$1-$2");
            input.value = input.value.replace(/(-\d{3})\d+?$/, '$1');

            return;
        }

        return cepFormat();
    }

    $.fn.phoneFormat = function (options) {
        let defaults =
        {
            id: ''
        };

        let option = $.extend(defaults, options);

        function phoneFormat() {
            let input = document.getElementById(option.id);

            input.value = input.value.replace(/\D/g, "")
            input.value = input.value.replace(/^(\d)/, "($1")
            input.value = input.value.replace(/(.{3})(\d)/, "$1)$2")

            if (input.value.length == 9) {
                input.value = input.value.replace(/(.{1})$/, "-$1")
            } else if (input.value.length == 10) {
                input.value = input.value.replace(/(.{2})$/, "-$1")
            } else if (input.value.length == 11) {
                input.value = input.value.replace(/(.{3})$/, "-$1")
            } else if (input.value.length == 12) {
                input.value = input.value.replace(/(.{4})$/, "-$1")
            } else if (input.value.length > 12) {
                input.value = input.value.replace(/(.{4})$/, "-$1")
                input.value = input.value.replace(/(-\d{4})\d+?$/, '$1')
            }

            return;
        }

        //function mascaraCNPJ(i) {
        //	unieduk.mascaraCNPJ(i);
        //}

        //function fMasc(objeto, mascara) {
        //	obj = objeto
        //	masc = mascara
        //	setTimeout("pMascEx()", 1)
        //}

        //function fMascEx() {
        //	obj.value = masc(obj.value)
        //}

        return phoneFormat();
    }

    $.fn.cpfFormat = function (options) {
        let defaults =
        {
            value: '',
            id: ''
        };

        let option = $.extend(defaults, options);

        function formatCPF() {
            let input = document.getElementById(option.id);

            input.value = input.value
                .replace(/\D/g, '') // substitui qualquer caracter que nao seja numero por nada
                .replace(/(\d{3})(\d)/, '$1.$2') // captura 2 grupos de numero o primeiro de 3 e o segundo de 1, apos capturar o primeiro grupo ele adiciona um ponto antes do segundo grupo de numero
                .replace(/(\d{3})(\d)/, '$1.$2')
                .replace(/(\d{3})(\d{1,2})/, '$1-$2')
                .replace(/(-\d{2})\d+?$/, '$1') // captura 2 numeros seguidos de um tra�o e n�o deixa ser digitado mais nada
            return;
        };

        return formatCPF();
    }

    $.fn.priceFormat = function (options) {
        var defaults =
        {
            prefix: '',
            suffix: '',
            centsSeparator: ',',
            thousandsSeparator: '.',
            limit: false,
            centsLimit: 2,
            clearPrefix: true,
            clearSufix: true,
            allowNegative: true,
            insertPlusSign: false,
            clearOnEmpty: false,
            putNegative: false
        };

        var options = $.extend(defaults, options);

        return this.each(function () {
            // pre defined options
            var obj = $(this);
            var value = '';
            var is_number = /[0-9]/;

            // Check if is an input
            if (obj.is('input'))
                value = obj.val();
            else
                value = obj.html();

            // load the pluggings settings
            var prefix = options.prefix;
            var suffix = options.suffix;
            var centsSeparator = options.centsSeparator;
            var thousandsSeparator = options.thousandsSeparator;
            var limit = options.limit;
            var centsLimit = options.centsLimit;
            var clearPrefix = options.clearPrefix;
            var clearSuffix = options.clearSuffix;
            var allowNegative = options.allowNegative;
            var insertPlusSign = options.insertPlusSign;
            var clearOnEmpty = options.clearOnEmpty;
            var putNegative = options.putNegative;

            // If insertPlusSign is on, it automatic turns on allowNegative, to work with Signs
            if (insertPlusSign) allowNegative = true;

            function set(nvalue) {
                if (obj.is('input'))
                    obj.val(nvalue);
                else
                    obj.html(nvalue);
            }

            function get() {
                if (obj.is('input'))
                    value = obj.val();
                else
                    value = obj.html();

                return value;
            }

            // skip everything that isn't a number
            // and also skip the left zeroes
            function to_numbers(str) {
                var formatted = '';
                for (var i = 0; i < (str.length); i++) {
                    char_ = str.charAt(i);
                    if (formatted.length == 0 && char_ == 0) char_ = false;

                    if (char_ && char_.match(is_number)) {
                        if (limit) {
                            if (formatted.length < limit) formatted = formatted + char_;
                        }
                        else {
                            formatted = formatted + char_;
                        }
                    }
                }

                return formatted;
            }

            // format to fill with zeros to complete cents chars
            function fill_with_zeroes(str) {
                while (str.length < (centsLimit + 1)) str = '0' + str;
                return str;
            }

            // format as price
            function price_format(str, ignore) {
                if (!ignore && (str === '' || str == price_format('0', true)) && clearOnEmpty)
                    return '';

                // formatting settings
                var formatted = fill_with_zeroes(to_numbers(str));
                var thousandsFormatted = '';
                var thousandsCount = 0;

                // Checking CentsLimit
                if (centsLimit == 0) {
                    centsSeparator = "";
                    centsVal = "";
                }

                // split integer from cents
                var centsVal = formatted.substr(formatted.length - centsLimit, centsLimit);
                var integerVal = formatted.substr(0, formatted.length - centsLimit);

                // apply cents pontuation
                formatted = (centsLimit == 0) ? integerVal : integerVal + centsSeparator + centsVal;

                // apply thousands pontuation
                if (thousandsSeparator || $.trim(thousandsSeparator) != "") {
                    for (var j = integerVal.length; j > 0; j--) {
                        char_ = integerVal.substr(j - 1, 1);
                        thousandsCount++;
                        if (thousandsCount % 3 == 0) char_ = thousandsSeparator + char_;
                        thousandsFormatted = char_ + thousandsFormatted;
                    }

                    //
                    if (thousandsFormatted.substr(0, 1) == thousandsSeparator) thousandsFormatted = thousandsFormatted.substring(1, thousandsFormatted.length);
                    formatted = (centsLimit == 0) ? thousandsFormatted : thousandsFormatted + centsSeparator + centsVal;
                }

                // if the string contains a dash, it is negative - add it to the begining (except for zero)
                //if (allowNegative && (integerVal != null || centsVal != null)){}

                //lets start with " - "
                if (allowNegative && (integerVal != null || centsVal != null)) {
                    if (str.indexOf('-') != -1 && str.indexOf('+') < str.indexOf('-')) {
                        formatted = '-' + formatted;
                    }
                    else {
                        if (!insertPlusSign)
                            formatted = '' + formatted;
                        else
                            formatted = '+' + formatted;

                        if (putNegative) {
                            formatted = '-' + formatted;
                        }
                    }
                }

                // apply the prefix
                if (prefix) formatted = prefix + formatted;

                // apply the suffix
                if (suffix) formatted = formatted + suffix;

                return formatted;
            }

            // filter what user type (only numbers and functional keys)
            function key_check(e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                var typed = String.fromCharCode(code);
                var functional = false;
                var str = value;
                var newValue = price_format(str + typed);

                // allow key numbers, 0 to 9
                if ((code >= 48 && code <= 57) || (code >= 96 && code <= 105)) functional = true;

                // check Backspace, Tab, Enter, Delete, and left/right arrows
                if (code == 8) functional = true;
                if (code == 9) functional = true;
                if (code == 13) functional = true;
                if (code == 46) functional = true;
                if (code == 37) functional = true;
                if (code == 39) functional = true;
                if (code == 86) functional = true; // v: paste
                // Minus Sign, Plus Sign
                if (allowNegative && (code == 189 || code == 109 || code == 173)) functional = true;
                if (insertPlusSign && (code == 187 || code == 107 || code == 61)) functional = true;

                if (!functional) {
                    e.preventDefault();
                    e.stopPropagation();
                    if (str != newValue) set(newValue);
                }
            }

            // Formatted price as a value
            function price_it() {
                var str = get();
                var price = price_format(str);
                if (str != price) set(price);
                if (parseFloat(str) == 0.0 && clearOnEmpty) set('');
            }

            // Add prefix on focus
            function add_prefix() {
                obj.val(prefix + get());
            }

            function add_suffix() {
                obj.val(get() + suffix);
            }

            // Clear prefix on blur if is set to true
            function clear_prefix() {
                if ($.trim(prefix) != '' && clearPrefix) {
                    var array = get().split(prefix);
                    set(array[1]);
                }
            }

            // Clear suffix on blur if is set to true
            function clear_suffix() {
                if ($.trim(suffix) != '' && clearSuffix) {
                    var array = get().split(suffix);
                    set(array[0]);
                }
            }

            // bind the actions
            obj.bind('keydown.price_format', key_check);
            obj.bind('keyup.price_format', price_it);
            obj.bind('focusout.price_format', price_it);

            // Clear Prefix and Add Prefix
            if (clearPrefix) {
                obj.bind('focusout.price_format', function () {
                    clear_prefix();
                });

                obj.bind('focusin.price_format', function () {
                    add_prefix();
                });
            }

            // Clear Suffix and Add Suffix
            if (clearSuffix) {
                obj.bind('focusout.price_format', function () {
                    clear_suffix();
                });

                obj.bind('focusin.price_format', function () {
                    add_suffix();
                });
            }

            // If value has content
            if (get().length > 0) {
                price_it();
                clear_prefix();
                clear_suffix();
            }
        });
    };

    /**********************
    * Remove price format *
    ***********************/
    $.fn.unpriceFormat = function () {
        return $(this).unbind(".price_format");
    };

    /******************
    * Unmask Function *
    *******************/
    $.fn.unmask = function () {
        var field;
        var result = "";

        if ($(this).is('input'))
            field = $(this).val();
        else
            field = $(this).html();

        for (var f in field) {
            if (!isNaN(field[f]) || field[f] == "-") result += field[f];
        }

        return result;
    };
})(jQuery);