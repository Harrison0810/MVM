import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { CorrespondenceTypesModel } from 'src/shared/models/correspondece-types.model';
import { CorrespondenceModel } from 'src/shared/models/correspondence.model';
import { MessageModel } from 'src/shared/models/message.model';
import { CorrespondenceService } from 'src/shared/services/correspondence.service';

@Component({
    selector: 'app-add-correspondence',
    templateUrl: './add-correspondence.component.html',
    styleUrls: ['./add-correspondence.component.css']
})
export class AddCorrespondenceComponent implements OnInit {
    public types: CorrespondenceTypesModel[] = [];
    public typeSelected: CorrespondenceTypesModel;
    private Id: number;
    public corresponse: CorrespondenceTypesModel;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private _correspondenceService: CorrespondenceService,
        private activatedRoute: ActivatedRoute
    ) { }

    public addForm: FormGroup;

    public onSubmit(): void {
        const model: CorrespondenceModel = {
            subject: this.addForm.value.subject,
            content: this.addForm.value.content,
            creationDate: new Date(),
            code: '',
            correspondenceId: 0,
            correspondenceTypeId: this.addForm.value.correspondenceTypeId,
            userId: 0
        }

        if (this.Id) {
            model.correspondenceId = this.Id;

            this._correspondenceService.EditCorrespondece(model).subscribe(result => {
                this.router.navigate(['list-correspondence']);
            });
        } else {
            this._correspondenceService.AddCorrespondence(model).subscribe(result => {
                this.router.navigate(['list-correspondence']);
            });
        }


    }

    private GetTypes(): void {
        this._correspondenceService.GetCorrespondeceTypes().subscribe((result: MessageModel<CorrespondenceTypesModel[]>) => {
            if (result.status) {
                this.types = result.data;
            }
        });
    }

    ngOnInit() {
        this.addForm = this.formBuilder.group({
            subject: ['', Validators.required],
            content: ['', Validators.required],
            correspondenceTypeId: ['', Validators.required],
            correspondenceId: [],
            code: [],
            userId: [],
            creationDate: []
        });

        this.activatedRoute.params.subscribe(params => {
            this.Id = +params['Id'];

            if (this.Id) {
                this._correspondenceService.GetCorrespondence(this.Id).subscribe((result: MessageModel<CorrespondenceModel>) => {
                    if (result.status) {
                        this.addForm.setValue(result.data);
                    }
                });
            }
        });

        this.GetTypes();
    }
}