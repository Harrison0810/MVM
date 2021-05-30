export class UrlsModel {
    // Login
    public Login: () => string;
    // Correspondences
    public GetCorrespondences: () => string;
    public GetCorrespondence: (Id: number) => string;
    public AddCorrespondence: () => string;
    public EditCorrespondence: () => string;
    public DeleteCorrespondence: () => string;
    // Correspondences Types
    public GetCorrespondenceTypes: () => string;
}