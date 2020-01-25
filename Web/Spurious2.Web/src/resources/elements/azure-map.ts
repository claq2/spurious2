import { bindable, bindingMode } from 'aurelia-framework';
import { Map, data, AuthenticationType, control, ControlPosition } from 'azure-maps-control';
import { RouteConfig } from 'aurelia-router';

export class AzureMapCustomElement {
    private container: HTMLElement;
    private map: Map;
    private viewChangeHandler: () => void = () => {
        this.location = this.map.getCamera().center;
        //console.log("map location: " + this.location);
    };

    @bindable apiKey: string = '';
    @bindable height: string = '600px';
    @bindable width: string = '';

    @bindable({ defaultBindingMode: bindingMode.twoWay }) location: data.Position | string;
    attached() {
        //console.log('attached');
        this.map = new Map(this.container, {
            authOptions: {
                authType: AuthenticationType.subscriptionKey, subscriptionKey: this.apiKey
            },
            // Start centered on all of Ontario
            center: new data.Position(-83.9355468749954, 48.25394114468216),
            zoom: 4,
        });

        this.map.events.addOnce("ready", () => {
            this.location = this.map.getCamera().center;
            //console.log("map location: " + this.location);
            this.map.events.add("moveend", this.viewChangeHandler);
            let controls: any[] = [];
            controls.push(new control.ZoomControl());
            this.map.controls.add(controls, { position: ControlPosition.TopRight });
        });
    }

    bind(a: any, b: any) {
        //console.log('bind');
        //console.log(a);
    }

    detached() {
        if (this.viewChangeHandler) {
            this.map.events.remove("moveend", this.viewChangeHandler);
        }

        if (this.map) {
            this.map.dispose();
            this.map = null;
        }
    }
}
