import "bootstrap";
import "bootstrap/dist/css/bootstrap.css";
import "@fortawesome/fontawesome-free/css/all.min.css";
import "azure-maps-control/dist/atlas.css";
import "./app.css";

import { Aurelia } from "aurelia-framework";
import environment from "../config/environment.json";
import { PLATFORM } from "aurelia-pal";

export async function configure(aurelia: Aurelia) {
  aurelia.use.standardConfiguration().developmentLogging();

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName("aurelia-testing"));
  }

  aurelia.use.plugin(PLATFORM.moduleName("resources"));

  await aurelia.start();
  await aurelia.setRoot(PLATFORM.moduleName("app"));
}
