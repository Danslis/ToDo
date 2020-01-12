"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Card = /** @class */ (function () {
    function Card(data) {
        this.id = 0;
        this.date = new Date();
        this.id = data.id;
        this.date = data.date;
        this.name = data.name;
        this.timeOfCompletion = data.timeOfCompletion;
        this.priority = data.priority;
        this.position = data.position;
    }
    return Card;
}());
exports.Card = Card;
//# sourceMappingURL=card.js.map