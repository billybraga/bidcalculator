/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

class CalculatorViewModel {
    public bid = ko.observable("");
    public total = ko.observable(1000);
    public userFee = ko.observable("");
    public salesFee = ko.observable("");
    public assocationFee = ko.observable("");
    public storageFee = ko.observable("");

    public constructor() {
        this.total.subscribe(x => this.update());
        this.update();
    }

    private update() {
        if (!this.total()) {
            return;
        }

        $
            .post('/Home/Calculate', { total: this.total() })
            .fail(x => this.log(x))
            .then(x => this.updateData(x))
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