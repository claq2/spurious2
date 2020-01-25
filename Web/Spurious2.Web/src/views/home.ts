import {} from "aurelia-framework";

export class Home {
    name: string;
    densityLink: string;
    activate(params, routeConfig, $navigationInstruction) {
        this.name = routeConfig.settings.name;
        this.densityLink = routeConfig.settings.densitylink;
    }
}
