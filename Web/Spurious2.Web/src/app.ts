import { Router, RouterConfiguration, RouteConfig } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';
import { client } from "shared";
import { Densities, DensityInfo } from "dtos";

export class App {
    router!: Router;

    async configureRouter(config: RouterConfiguration, router: Router) {
        let req: Densities = new Densities();
        const r: DensityInfo[] = await client.get(req);

        let routes: RouteConfig[] = r.map(rx => <RouteConfig>{
            route: rx.shortName,
            name: rx.shortName,
            moduleId: PLATFORM.moduleName('views/list-and-map'),
            nav: true,
            title: rx.title,
            activationStrategy: "replace",
            settings: { densityLink: rx.address }
        });
        routes = routes.concat([{ route: "about", name: "about", moduleId: PLATFORM.moduleName('views/about'), nav: true, title: "About" }])
        //console.log(routes);
        config.title = 'Spurious Alcohol Statistics';
        config.options.pushState = true;
        config.map(routes);

        config.mapUnknownRoutes(<RouteConfig>{ redirect: routes[0].route });

        this.router = router;
    }
}
