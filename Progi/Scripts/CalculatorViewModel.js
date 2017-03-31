/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var CalculatorViewModel = (function () {
    function CalculatorViewModel() {
        var _this = this;
        this.bid = ko.observable("");
        this.total = ko.observable(1000);
        this.userFee = ko.observable("");
        this.salesFee = ko.observable("");
        this.assocationFee = ko.observable("");
        this.storageFee = ko.observable("");
        this.total.subscribe(function (x) { return _this.update(); });
        this.update();
    }
    CalculatorViewModel.prototype.update = function () {
        var _this = this;
        if (!this.total()) {
            return;
        }
        if (this.lastUpdateQuery) {
            this.lastUpdateQuery.abort("cancelUpdate");
        }
        var total = this.total();
        (this.lastUpdateQuery = $.post('/Home/Calculate', { total: total }))
            .fail(function (x) {
            if (x.statusText == "cancelUpdate") {
                console.log("Canceling request for " + total);
                return;
            }
            _this.log(x);
        })
            .then(function (x) {
            _this.lastUpdateQuery = null;
            _this.updateData(x);
        });
    };
    CalculatorViewModel.prototype.log = function (result) {
        console.error("update", result);
    };
    CalculatorViewModel.prototype.updateData = function (data) {
        if (data == null) {
            this.bid("");
            this.userFee("");
            this.salesFee("");
            this.assocationFee("");
            this.storageFee("");
            return;
        }
        for (var key in data) {
            this[key](data[key]);
        }
    };
    return CalculatorViewModel;
}());
//# sourceMappingURL=CalculatorViewModel.js.map