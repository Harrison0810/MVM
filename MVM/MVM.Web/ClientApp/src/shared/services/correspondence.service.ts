import { Injectable } from '@angular/core';
import { ConstantsService } from '../constants.service';
import { HttpClient } from '@angular/common/http';
import { CorrespondenceModel } from '../models/correspondence.model';


@Injectable()
export class CorrespondenceService {
    constructor(
        private _constants: ConstantsService,
        private _http: HttpClient
    ) { }

    public GetCorrespondences() {
        return this._http.get(this._constants.URLS.GetCorrespondences());
    }

    public GetCorrespondence(Id: number) {
        return this._http.get(this._constants.URLS.GetCorrespondence(Id));
    }

    public AddCorrespondence(model: CorrespondenceModel) {
        return this._http.post(this._constants.URLS.AddCorrespondence(), model);
    }

    public EditCorrespondece(model: CorrespondenceModel) {
        return this._http.post(this._constants.URLS.EditCorrespondence(), model);
    }

    public DeleteCorrespondece(model: CorrespondenceModel) {
        return this._http.post(this._constants.URLS.DeleteCorrespondence(), model);
    }

    public GetCorrespondeceTypes() {
        return this._http.get(this._constants.URLS.GetCorrespondenceTypes());
    }
}