using MVM.Domain.Models;
using System.Collections.Generic;

namespace MVM.Domain.Services
{
    public interface ICorrespondenceService
    {
        MessageModel<List<CorrespondencesModel>> GetCorresponces();
        MessageModel<CorrespondencesModel> GetCorresponce(int Id);
        MessageModel<CorrespondencesModel> AddCorrespondence(CorrespondencesModel model);
        MessageModel<string> EditCorrespondence(CorrespondencesModel model);
        MessageModel<string> DeleteCorrespondence(CorrespondencesModel model);
        MessageModel<List<CorrespondenceTypesModel>> GetCorrespondenceTypes();
    }
}
