using AutoMapper;
using MVM.Domain.Helpers;
using MVM.Domain.Models;
using MVM.Infrastructure.Contracts;
using MVM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVM.Domain.Services
{
    public class CorrespondenceService : ICorrespondenceService
    {
        #region Properties

        private readonly IDBMmvRepository _dbMmvRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public CorrespondenceService(
            IDBMmvRepository dbMmvRepository,
            IMapper mapper
        )
        {
            _dbMmvRepository = dbMmvRepository;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        MessageModel<List<CorrespondencesModel>> ICorrespondenceService.GetCorresponces()
        {
            MessageModel<List<CorrespondencesModel>> message = new MessageModel<List<CorrespondencesModel>>();

            try
            {
                List<CorrespondencesContract> correspondencesContracts = _dbMmvRepository.Correspondences.GetAll("CorrespondenceType");

                message.Data = _mapper.Map<List<CorrespondencesContract>, List<CorrespondencesModel>>(correspondencesContracts);
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = Ex.Message;
                // Save Log
                _dbMmvRepository.Log.Add(new LogContract
                {
                    Action = Messages.Error,
                    CreationDate = DateTime.Now,
                    Description = Ex.Message
                });
            }

            return message;
        }

        MessageModel<CorrespondencesModel> ICorrespondenceService.GetCorresponce(int Id)
        {
            MessageModel<CorrespondencesModel> message = new MessageModel<CorrespondencesModel>();

            try
            {
                CorrespondencesContract correspondencesContracts = _dbMmvRepository.Correspondences.FindBy(x => x.CorrespondenceId == Id).FirstOrDefault();

                message.Data = _mapper.Map<CorrespondencesContract, CorrespondencesModel>(correspondencesContracts);
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = Ex.Message;
                // Save Log
                _dbMmvRepository.Log.Add(new LogContract
                {
                    Action = Messages.Error,
                    CreationDate = DateTime.Now,
                    Description = Ex.Message
                });
            }

            return message;
        }

        MessageModel<CorrespondencesModel> ICorrespondenceService.AddCorrespondence(CorrespondencesModel model)
        {
            MessageModel<CorrespondencesModel> message = new MessageModel<CorrespondencesModel>();

            try
            {
                CorrespondenceTypesContract type = _dbMmvRepository.CorrespondenceTypes.FindBy(x => x.CorrespondenceTypeId == model.CorrespondenceTypeId).FirstOrDefault();

                if (type is null)
                {
                    return message;
                }

                CorrespondencesContract lastCorresponce = _dbMmvRepository.Correspondences.GetAll().LastOrDefault();

                lastCorresponce ??= new CorrespondencesContract();

                if (type.Code == Messages.EXT)
                {
                    model.Code = $"CE{(lastCorresponce.CorrespondenceId + 1).ToString("D" + (8 - lastCorresponce.CorrespondenceId.ToString().Length))}";
                }
                else if (type.Code == Messages.INT)
                {
                    model.Code = $"CI{(lastCorresponce.CorrespondenceId + 1).ToString("D" + (8 - lastCorresponce.CorrespondenceId.ToString().Length))}";
                }

                CorrespondencesContract correspondenceContract = _mapper.Map<CorrespondencesModel, CorrespondencesContract>(model);

                correspondenceContract = _dbMmvRepository.Correspondences.Add(correspondenceContract);

                message.Data = _mapper.Map<CorrespondencesContract, CorrespondencesModel>(correspondenceContract);
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = Ex.Message;
                // Save Log
                _dbMmvRepository.Log.Add(new LogContract
                {
                    Action = Messages.Error,
                    CreationDate = DateTime.Now,
                    Description = Ex.Message
                });
            }

            return message;
        }

        MessageModel<string> ICorrespondenceService.EditCorrespondence(CorrespondencesModel model)
        {
            MessageModel<string> message = new MessageModel<string>();

            try
            {
                CorrespondencesContract correspondenceContract = _mapper.Map<CorrespondencesModel, CorrespondencesContract>(model);

                _dbMmvRepository.Correspondences.Edit(x => x.CorrespondenceId == correspondenceContract.CorrespondenceId, correspondenceContract,
                    (entity, contract) =>
                    {
                        entity.Subject = contract.Subject;
                        entity.Content = contract.Content;

                        return entity;
                    });

                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = Ex.Message;
                // Save Log
                _dbMmvRepository.Log.Add(new LogContract
                {
                    Action = Messages.Error,
                    CreationDate = DateTime.Now,
                    Description = Ex.Message
                });
            }

            return message;
        }

        MessageModel<string> ICorrespondenceService.DeleteCorrespondence(CorrespondencesModel model)
        {
            MessageModel<string> message = new MessageModel<string>();

            try
            {
                CorrespondencesContract correspondenceContract = _mapper.Map<CorrespondencesModel, CorrespondencesContract>(model);

                _dbMmvRepository.Correspondences.Delete(x => x.CorrespondenceId == correspondenceContract.CorrespondenceId);

                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = Ex.Message;
                // Save Log
                _dbMmvRepository.Log.Add(new LogContract
                {
                    Action = Messages.Error,
                    CreationDate = DateTime.Now,
                    Description = Ex.Message
                });
            }

            return message;
        }

        MessageModel<List<CorrespondenceTypesModel>> ICorrespondenceService.GetCorrespondenceTypes()
        {
            MessageModel<List<CorrespondenceTypesModel>> message = new MessageModel<List<CorrespondenceTypesModel>>();

            try
            {
                List<CorrespondenceTypesContract> correspondenceTypes = _dbMmvRepository.CorrespondenceTypes.GetAll();

                message.Data = _mapper.Map<List<CorrespondenceTypesContract>, List<CorrespondenceTypesModel>>(correspondenceTypes);
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = Ex.Message;
                // Save Log
                _dbMmvRepository.Log.Add(new LogContract
                {
                    Action = Messages.Error,
                    CreationDate = DateTime.Now,
                    Description = Ex.Message
                });
            }

            return message;
        }

        #endregion
    }
}
