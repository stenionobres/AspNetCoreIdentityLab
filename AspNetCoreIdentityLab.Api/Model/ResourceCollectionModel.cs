using System.Collections.Generic;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;

namespace AspNetCoreIdentityLab.Api.Model
{
    public class ResourceCollectionModel
    {
        public ResourceModel Resources { get; set; }

        public ResourceCollectionModel()
        {
            var principalResource = ResourceCollection.Get();

            Resources = new ResourceModel(principalResource.Description, principalResource.Permissions);

            Resources.Resources.AddRange(MountResources(principalResource.InnerResources));
        }

        private IEnumerable<ResourceModel> MountResources(IEnumerable<Resource> innerResources)
        {
            var resourceModels = new List<ResourceModel>();

            foreach (var innerResource in innerResources)
            {
                var resourceModel = new ResourceModel(innerResource.Description, innerResource.Permissions);

                resourceModel.Resources.AddRange(MountResources(innerResource.InnerResources));

                resourceModels.Add(resourceModel);
            }

            return resourceModels;
        }
    }
}
