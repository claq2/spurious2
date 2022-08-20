/* Options:
Date: 2022-08-14 10:52:36
Version: 6.2
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: https://localhost:5001

//GlobalNamespace: 
//MakePropertiesOptional: False
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


export interface IReturn<T>
{
    createResponse(): T;
}

export interface IReturnVoid
{
    createResponse(): void;
}

export class DensityInfo
{
    public shortName: string;
    public title: string;
    public address: string;

    public constructor(init?: Partial<DensityInfo>) { (Object as any).assign(this, init); }
}

export class Subdivision
{
    public name: string;
    public population: number;
    public density: number;
    public boundaryLink: string;
    public centreCoordinates: string;
    public storesLink: string;

    public constructor(init?: Partial<Subdivision>) { (Object as any).assign(this, init); }
}

export enum AlcoholType
{
    All = 'All',
    Beer = 'Beer',
    Wine = 'Wine',
    Spirits = 'Spirits',
}

export class Inventory
{
    public alcoholType: AlcoholType;
    public volume: number;

    public constructor(init?: Partial<Inventory>) { (Object as any).assign(this, init); }
}

export class Store
{
    public id: number;
    public locationCoordinates: string;
    public name: string;
    public city: string;
    public inventories: Inventory[];

    public constructor(init?: Partial<Store>) { (Object as any).assign(this, init); }
}

export class ServerStats
{
    public redis: { [index: string]: number; };
    public serverEvents: { [index: string]: string; };
    public mqDescription: string;
    public mqWorkers: { [index: string]: number; };

    public constructor(init?: Partial<ServerStats>) { (Object as any).assign(this, init); }
}

// @DataContract
export class ResponseError
{
    // @DataMember(Order=1)
    public errorCode: string;

    // @DataMember(Order=2)
    public fieldName: string;

    // @DataMember(Order=3)
    public message: string;

    // @DataMember(Order=4)
    public meta: { [index: string]: string; };

    public constructor(init?: Partial<ResponseError>) { (Object as any).assign(this, init); }
}

// @DataContract
export class ResponseStatus
{
    // @DataMember(Order=1)
    public errorCode: string;

    // @DataMember(Order=2)
    public message: string;

    // @DataMember(Order=3)
    public stackTrace: string;

    // @DataMember(Order=4)
    public errors: ResponseError[];

    // @DataMember(Order=5)
    public meta: { [index: string]: string; };

    public constructor(init?: Partial<ResponseStatus>) { (Object as any).assign(this, init); }
}

export class HelloResponse
{
    public result: string;

    public constructor(init?: Partial<HelloResponse>) { (Object as any).assign(this, init); }
}

export class AdminDashboardResponse
{
    public serverStats: ServerStats;
    public responseStatus: ResponseStatus;

    public constructor(init?: Partial<AdminDashboardResponse>) { (Object as any).assign(this, init); }
}

// @Route("/hello")
// @Route("/hello/{Name}")
export class Hello implements IReturn<HelloResponse>
{
    public name: string;

    public constructor(init?: Partial<Hello>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'Hello'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new HelloResponse(); }
}

// @Route("/densities")
export class Densities implements IReturn<DensityInfo[]>
{

    public constructor(init?: Partial<Densities>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'Densities'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new Array<DensityInfo>(); }
}

// @Route("/densities/{Name}/subdivisions")
export class DensitySubdivisions implements IReturn<Subdivision[]>
{
    public name: string;

    public constructor(init?: Partial<DensitySubdivisions>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DensitySubdivisions'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new Array<Subdivision>(); }
}

// @Route("/subdivisions/{Id}/boundary")
export class BoundaryRequest implements IReturn<string>
{
    public id: number;

    public constructor(init?: Partial<BoundaryRequest>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'BoundaryRequest'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return ''; }
}

// @Route("/subdivisions/{Id}/stores")
export class StoresInSubdivisionRequest implements IReturn<Store[]>
{
    public id: number;

    public constructor(init?: Partial<StoresInSubdivisionRequest>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'StoresInSubdivisionRequest'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new Array<Store>(); }
}

export class AdminDashboard implements IReturn<AdminDashboardResponse>
{

    public constructor(init?: Partial<AdminDashboard>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'AdminDashboard'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new AdminDashboardResponse(); }
}

