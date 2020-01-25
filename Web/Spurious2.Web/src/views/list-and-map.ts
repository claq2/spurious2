import { bindable, bindingMode, inject } from 'aurelia-framework';
import { Map, data, AuthenticationType, control, ControlPosition, source, layer, MapMouseEvent, Shape, Popup } from 'azure-maps-control';
import { RouteConfig } from 'aurelia-router';
import { HttpClient } from 'aurelia-fetch-client';
import { client } from "shared";
import { Subdivision, Store, Inventory } from '../dtos';

@inject(HttpClient)
export class ListAndMap {
    private container: HTMLElement;
    private map: Map;
    private viewChangeHandler: () => void;
    private densityLink: string;
    private datasource: source.DataSource;

    subdivisions: Subdivision[] = [];
    @bindable apiKey: string = "UHo_yP7VRSrUF-ZA_GFnT7YOz1b-MoRMT90xMbDybzs";
    @bindable height: string = '400px';
    @bindable width: string = '';

    @bindable({ defaultBindingMode: bindingMode.twoWay }) location: data.Position | string;

    constructor(private http: HttpClient) {
        http.defaults = { headers: { "Accept": "application/json" } };
    }

    async activate(params: any, routeConfig: RouteConfig) {
        //console.log('activate');
        //console.log(routeConfig);
        this.densityLink = routeConfig.settings.densityLink;
        //console.log("done activate");
    }

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

        var alcoholTemplate = '<div>{alcoholType}: {volume} L</div>'
        var popupTemplate = '<div class="customInfobox"><div class="name">{name}</div>{alcoholTypes}</div>';

        this.map.events.addOnce("ready", async () => {
            this.datasource = new source.DataSource();
            this.map.sources.add(this.datasource);
            //Add a layer for rendering the outline of polygons.
            let lineLayer = new layer.LineLayer(this.datasource, null, { strokeColor: 'black', strokeWidth: 0.7 });
            let polygonLayer = new layer.PolygonLayer(this.datasource, null, {
                fillOpacity: 0.3,
                filter: ['any', ['==', ['geometry-type'], 'Polygon'], ['==', ['geometry-type'], 'MultiPolygon']]	//Only render Polygon or MultiPolygon in this layer.
            });
            let pointLayer = new layer.SymbolLayer(this.datasource, 'symbol', {
                filter: ['==', ['geometry-type'], 'Point'], iconOptions: { allowOverlap: true, image: 'pin-blue' }
            });
            this.map.layers.add(lineLayer);
            this.map.layers.add(polygonLayer);
            this.map.layers.add(pointLayer);

            this.location = this.map.getCamera().center;
            //console.log("map location: " + this.location);
            this.viewChangeHandler = () => {
                this.location = this.map.getCamera().center;
                //console.log("map location: " + this.location);
            };

            this.map.events.add("moveend", this.viewChangeHandler);
            let controls: any[] = [];
            controls.push(new control.ZoomControl());
            this.map.controls.add(controls, { position: ControlPosition.TopRight });

            let densities: Subdivision[] = await client.get<Subdivision[]>(this.densityLink);
            //console.log(densities[0].centreCoordinates);
            this.subdivisions = densities;

            await this.datasource.importDataFromUrl(densities[0].boundaryLink);
            let shapes = this.datasource.getShapes();
            let bounds = shapes[0].getBounds();
            let centre = data.BoundingBox.getCenter(bounds);
            this.map.setCamera({ bounds: bounds, center: centre });
            this.map.setCamera({ zoom: this.map.getCamera().zoom - 1 });
            //console.log('shapes: ', shapes);

            let stores = await client.get<Store[]>(densities[0].storesLink);
            //console.log("stores: ", stores);
            stores.forEach(s => {
                let f = new data.Feature(
                    new data.Point(
                        new data.Position(parseFloat(s.locationCoordinates.split(",")[0]),
                            parseFloat(s.locationCoordinates.split(",")[1]))), { name: s.name, inventories: s.inventories });
                //console.log('f');
                //console.log(f);
                this.datasource.add(f);
            });
            let points = this.datasource.getShapes();
            //console.log('points: ', points);

            let popup = new Popup({
                pixelOffset: [0, -18],
                //closeButton: false
            });

            this.map.events.add('click', pointLayer, (e: MapMouseEvent) => {
                //console.log('click');
                //Make sure that the point exists.
                if (e.shapes && e.shapes.length > 0) {
                    let content: string, coordinate;
                    //console.log(e.shapes[0]);
                    let x: data.Feature<data.Geometry, any> = e.shapes[0] as data.Feature<data.Geometry, any>;
                    let s = e.shapes[0] as Shape;
                    //x.properties;
                    // console.log(x);
                    // console.log(s);
                    //console.log('x.prop');
                    //console.log(x.properties['name']);
                    let storeName: string = s.getProperties()['name'] as string;
                    let inventories: Inventory[] = s.getProperties()['inventories'] as Inventory[];
                    let inventoryDiv: string = '';
                    inventories.forEach(i => inventoryDiv += alcoholTemplate.replace(/{alcoholType}/g, i.alcoholType).replace(/{volume}/g, i.volume.toString()));
                    content = popupTemplate.replace(/{name}/g, storeName).replace(/{alcoholTypes}/g, inventoryDiv);
                    //console.log('s.getProp');
                    //console.log(s.getProperties()['name']);
                    //console.log(s.getProperties()['inventories']);
                    //let properties = e.shapes[0].properties;// getProperties();
                    //content = popupTemplate.replace(/{name}/g, properties.name).replace(/{description}/g, properties.description);
                    coordinate = s.getCoordinates();

                    popup.setOptions({
                        //Update the content of the popup.
                        content: content,

                        //Update the popup's position with the symbol's coordinate.
                        position: coordinate

                    });
                    //Open the popup.
                    popup.open(this.map);
                }
            });

            //this.map.events.add('mouseleave', pointLayer, () => {
            //    popup.close();
            //});
        });

        //console.log('done attach');
    }

    async selectSubdiv(subdiv: Subdivision) {
        //console.log("Starting select subdiv");
        this.datasource.clear();

        await this.datasource.importDataFromUrl(subdiv.boundaryLink);

        let shapes = this.datasource.getShapes();
        let bounds = shapes[0].getBounds();
        let centre = data.BoundingBox.getCenter(bounds);
        this.map.setCamera({ bounds: bounds, center: centre });
        this.map.setCamera({ zoom: this.map.getCamera().zoom - 1 });

        let stores = await client.get<Store[]>(subdiv.storesLink);
        //console.log("stores: ", stores);
        stores.forEach(s => {
            let f = new data.Feature(
                new data.Point(
                    new data.Position(parseFloat(s.locationCoordinates.split(",")[0]),
                        parseFloat(s.locationCoordinates.split(",")[1]))), { name: s.name, inventories: s.inventories });
            //console.log('f');
            //console.log(f);
            this.datasource.add(f);
        });
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
