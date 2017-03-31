/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

class CalculatorViewModel {
    public bid = ko.observable("");
    public total = ko.observable(1000);
    public userFee = ko.observable("");
    public salesFee = ko.observable("");
    public assocationFee = ko.observable("");
    public storageFee = ko.observable("");
    private lastUpdateQuery: JQueryXHR; 

    public constructor() {
        this.total.subscribe(x => this.update());
        this.update();
    }

    private update() {
        if (!this.total()) {
            return;
        }

        if (this.lastUpdateQuery) {
            this.lastUpdateQuery.abort("cancelUpdate");
        }

        var total = this.total();
        (this.lastUpdateQuery = $.post('/Home/Calculate', { total: total }))
            .fail(x => {
                if (x.statusText == "cancelUpdate") {
                    console.log("Canceling request for " + total);
                    return;
                }
                this.log(x);
            })
            .then(x => {
                this.lastUpdateQuery = null;
                this.updateData(x);
            });
    }

    private log(result) {
        console.error("update", result);
    }

    private updateData(data) {
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
    }
}