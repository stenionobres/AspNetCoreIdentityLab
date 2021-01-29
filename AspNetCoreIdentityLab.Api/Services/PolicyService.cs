using AspNetCoreIdentityLab.Api.Model;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using AspNetCoreIdentityLab.Persistence.Mappers;

namespace AspNetCoreIdentityLab.Api.Services
{
    public class PolicyService
    {
        private readonly PolicyMapper _policyMapper;

        public PolicyService(PolicyMapper policyMapper)
        {
            _policyMapper = policyMapper;
        }

        public void Save(PolicyModel policyModel)
        {
            var policyFromDatabase = _policyMapper.GetById(policyModel.Id);
            var policy = new Policy() { Id = policyModel.Id, Name = policyModel.Description };

            if (policyFromDatabase != null)
            {
                policyFromDatabase.Name = policyModel.Description;
                _policyMapper.Update(policyFromDatabase);

                return;
            }

            _policyMapper.Save(policy);
        }
    }
}
