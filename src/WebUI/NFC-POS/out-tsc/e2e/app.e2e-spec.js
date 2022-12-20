"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const app_po_1 = require("./app.po");
describe('new App', () => {
    let page;
    beforeEach(() => {
        page = new app_po_1.AppPage();
    });
    it('should display welcome message', () => {
        page.navigateTo();
        expect(page.getPageTitle()).toContain('Tab 1');
    });
});
//# sourceMappingURL=app.e2e-spec.js.map