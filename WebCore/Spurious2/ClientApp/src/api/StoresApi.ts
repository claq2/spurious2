/**
 * Spurious2
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-http-client';
import { Api } from './Api';
import { AuthStorage } from './AuthStorage';
import {
  Store,
} from './models';

/**
 * getSubdivisionStores - parameters interface
 */
export interface IGetSubdivisionStoresParams {
  id: number;
}

/**
 * StoresApi - API class
 */
@autoinject()
export class StoresApi extends Api {

  /**
   * Creates a new StoresApi class.
   *
   * @param httpClient The Aurelia HTTP client to be injected.
   * @param authStorage A storage for authentication data.
   */
  constructor(httpClient: HttpClient, authStorage: AuthStorage) {
    super(httpClient, authStorage);
  }

  /**
   * @param params.id 
   */
  async getSubdivisionStores(params: IGetSubdivisionStoresParams): Promise<Array<Store>> {
    // Verify required parameters are set
    this.ensureParamIsSet('getSubdivisionStores', params, 'id');

    // Create URL to call
    const url = `${this.basePath}/subdivisions/{id}/stores`
      .replace(`{${'id'}}`, encodeURIComponent(`${params['id']}`));

    const response = await this.httpClient.createRequest(url)
      // Set HTTP method
      .asGet()

      // Send the request
      .send();

    if (response.statusCode < 200 || response.statusCode >= 300) {
      throw new Error(response.content);
    }

    // Extract the content
    return response.content;
  }

}
