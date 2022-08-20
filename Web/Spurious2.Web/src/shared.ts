import { JsonServiceClient } from "@servicestack/client";

declare let global: { BaseUrl: string; }; //populated from package.json/jest

export const client = new JsonServiceClient(global.BaseUrl || '/');
