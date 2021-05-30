import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CorrespondenceModel } from 'src/shared/models/correspondence.model';
import { MessageModel } from 'src/shared/models/message.model';
import { CorrespondenceService } from 'src/shared/services/correspondence.service';

@Component({
    selector: 'app-list-correspondence',
    templateUrl: 'list-correspondence.component.html',
    styleUrls: ['./list-correspondence.component.css']
})
export class ListCorrespondenceComponent implements OnInit {
    public correspondences: CorrespondenceModel[] = [];

    constructor(
        private _correspondenceService: CorrespondenceService,
        private router: Router
    ) {
    }

    private GetCorrespondences(): void {
        this._correspondenceService.GetCorrespondences().subscribe((result: MessageModel<CorrespondenceModel[]>) => {
            if (result.status) {
                this.correspondences = result.data;
            }
        });
    }

    public NewCorrespondence(): void {
        this.router.navigate(['/add-correspondence']);
    }

    public EditCorrespondence(model: CorrespondenceModel): void {
        this.router.navigate(['/add-correspondence', model.correspondenceId.toString()]);
    }

    public DeleteCorrespondece(model: CorrespondenceModel): void {
        this._correspondenceService.DeleteCorrespondece(model).subscribe((result: MessageModel<CorrespondenceModel[]>) => {
            if (result.status) {
                this.GetCorrespondences();
            }
        });
    }

    ngOnInit(): void {
        this.GetCorrespondences();
    }
}