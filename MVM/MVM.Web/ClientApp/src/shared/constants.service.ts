import { Injectable, Inject } from '@angular/core';
import { UrlsModel } from './models/urls.model';

@Injectable()
export class ConstantsService {
    private BASE_URL: string = '';
    private API_VERSION: string = '';

    public URLS: UrlsModel = {
        // Login
        Login: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Auth/Login`,
        // Correspondences
        GetCorrespondences: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Correspondence/GetCorresponces`,
        GetCorrespondence: (Id: number): string => `${this.BASE_URL}api/${this.API_VERSION}/Correspondence/GetCorresponce/${Id}`,
        AddCorrespondence: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Correspondence/AddCorrespondence`,
        EditCorrespondence: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Correspondence/EditCorrespondence`,
        DeleteCorrespondence: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Correspondence/DeleteCorrespondence`,
        // Correspondences Types
        GetCorrespondenceTypes: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Correspondence/GetCorrespondenceTypes`
    }

    constructor(
        @Inject('BASE_URL') baseUrl: string,
        @Inject('API_VERSION') apiVersion: string
    ) {
        this.BASE_URL = baseUrl;
        this.API_VERSION = apiVersion;
    }
}