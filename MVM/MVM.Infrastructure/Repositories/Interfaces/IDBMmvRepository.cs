using MVM.Infrastructure.Contracts;
using MVM.Infrastructure.DB;
using MVM.Infrastructure.Entities;

namespace MVM.Infrastructure.Repositories
{
    public interface IDBMmvRepository
    {
        IGenericRepository<CorrespondencesEntity, CorrespondencesContract, MVMContext> Correspondences { get; }
        IGenericRepository<CorrespondenceTypesEntity, CorrespondenceTypesContract, MVMContext> CorrespondenceTypes { get; }
        IGenericRepository<LogEntity, LogContract, MVMContext> Log { get; }
        IGenericRepository<RolesEntity, RolesContract, MVMContext> Roles { get; }
        IGenericRepository<UsersEntity, UsersContract, MVMContext> Users { get; }

        void GetUser(string userName);
    }
}
