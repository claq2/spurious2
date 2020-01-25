/* Options:
Date: 2019-10-05 19:10:44
Version: 5.60
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:5000

//GlobalNamespace: 
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
    Spirits = 'Spirits',
    Wine = 'Wine',
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
    public inventories: Inventory[];

    public constructor(init?: Partial<Store>) { (Object as any).assign(this, init); }
}

// @Route("/densities")
export class Densities implements IReturn<DensityInfo[]>
{

    public constructor(init?: Partial<Densities>) { (Object as any).assign(this, init); }
    public createResponse() { return new Array<DensityInfo>(); }
    public getTypeName() { return 'Densities'; }
}

// @Route("/densities/{Name}/subdivisions")
export class DensitySubdivisions implements IReturn<Subdivision[]>
{
    public name: string;

    public constructor(init?: Partial<DensitySubdivisions>) { (Object as any).assign(this, init); }
    public createResponse() { return new Array<Subdivision>(); }
    public getTypeName() { return 'DensitySubdivisions'; }
}

// @Route("/subdivisions/{Id}/boundary")
export class BoundaryRequest implements IReturn<string>
{
    public id: number;

    public constructor(init?: Partial<BoundaryRequest>) { (Object as any).assign(this, init); }
    public createResponse() { return ''; }
    public getTypeName() { return 'BoundaryRequest'; }
}

// @Route("/subdivisions/{Id}/stores")
export class StoresInSubdivisionRequest implements IReturn<Store[]>
{
    public id: number;

    public constructor(init?: Partial<StoresInSubdivisionRequest>) { (Object as any).assign(this, init); }
    public createResponse() { return new Array<Store>(); }
    public getTypeName() { return 'StoresInSubdivisionRequest'; }
}

