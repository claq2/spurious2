/* Options:
Date: 2022-08-21 17:29:47
Version: 6.21
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:5000

//GlobalNamespace: 
MakePropertiesOptional: True
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
    public shortName?: string;
    public title?: string;
    public address?: string;

    public constructor(init?: Partial<DensityInfo>) { (Object as any).assign(this, init); }
}

export class Subdivision
{
    public name?: string;
    public population?: number;
    public density?: number;
    public boundaryLink?: string;
    public centreCoordinates?: string;
    public storesLink?: string;

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
    public alcoholType?: AlcoholType;
    public volume?: number;

    public constructor(init?: Partial<Inventory>) { (Object as any).assign(this, init); }
}

export class Store
{
    public id?: number;
    public locationCoordinates?: string;
    public name?: string;
    public city?: string;
    public inventories?: Inventory[];

    public constructor(init?: Partial<Store>) { (Object as any).assign(this, init); }
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
    public name?: string;

    public constructor(init?: Partial<DensitySubdivisions>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DensitySubdivisions'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new Array<Subdivision>(); }
}

// @Route("/subdivisions/{Id}/boundary")
export class BoundaryRequest implements IReturn<string>
{
    public id?: number;

    public constructor(init?: Partial<BoundaryRequest>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'BoundaryRequest'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return ''; }
}

// @Route("/subdivisions/{Id}/stores")
export class StoresInSubdivisionRequest implements IReturn<Store[]>
{
    public id?: number;

    public constructor(init?: Partial<StoresInSubdivisionRequest>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'StoresInSubdivisionRequest'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new Array<Store>(); }
}

