import { CorrespondenceTypesModel } from "./correspondece-types.model";

export class CorrespondenceModel {
    public correspondenceId: number;
    public code: string;
    public subject: string;
    public content: string;
    public creationDate: Date;
    public userId: number;
    public correspondenceTypeId: number;

    public CorrespondenceType?: CorrespondenceTypesModel;
}
