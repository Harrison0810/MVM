using MVM.Infrastructure.Contracts;
using MVM.Infrastructure.DB;
using MVM.Infrastructure.Entities;
using System;
using System.Linq;

namespace MVM.Infrastructure.Repositories
{
    public class DBMmvRepository : IDBMmvRepository
    {
        #region Properties

        private readonly MVMContext context;

        public IGenericRepository<CorrespondencesEntity, CorrespondencesContract, MVMContext> Correspondences { private set; get; }
        public IGenericRepository<CorrespondenceTypesEntity, CorrespondenceTypesContract, MVMContext> CorrespondenceTypes { private set; get; }
        public IGenericRepository<LogEntity, LogContract, MVMContext> Log { private set; get; }
        public IGenericRepository<RolesEntity, RolesContract, MVMContext> Roles { private set; get; }
        public IGenericRepository<UsersEntity, UsersContract, MVMContext> Users { private set; get; }

        #endregion

        #region Constructor

        public DBMmvRepository(
            IGenericRepository<CorrespondencesEntity, CorrespondencesContract, MVMContext> _Correspondences,
            IGenericRepository<CorrespondenceTypesEntity, CorrespondenceTypesContract, MVMContext> _CorrespondenceTypes,
            IGenericRepository<LogEntity, LogContract, MVMContext> _Log,
            IGenericRepository<RolesEntity, RolesContract, MVMContext> _Roles,
            IGenericRepository<UsersEntity, UsersContract, MVMContext> _Users,
            MVMContext _context
        )
        {
            Correspondences = _Correspondences;
            CorrespondenceTypes = _CorrespondenceTypes;
            Log = _Log;
            Roles = _Roles;
            Users = _Users;
            context = _context;
        }

        public void GetUser(string userName)
        {
            try
            {
                // Call Store Procedure

                //UsersEntity user = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
                //var a = context.Correspondences.ToList();
                //var aa = context.CorrespondenceTypes.ToList();
                //var ar = context.Log.ToList();
                //var ad = context.Roles.ToList();
                //var abv = context.Users.ToList();
            }
            catch (Exception Ex)
            {
                var a = Ex.Message;
            }
        }

        #endregion
    }
}
