import { } from "aurelia-framework";

export class Home {
    name!: string;
    densityLink!: string;
    activate(params: any, routeConfig: any, $navigationInstruction: any) {
        this.name = routeConfig.settings.name;
        this.densityLink = routeConfig.settings.densitylink;
    }
}
