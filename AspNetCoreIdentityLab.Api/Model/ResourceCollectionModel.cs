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

            MountResources(principalResource.InnerResources, Resources.Resources);
        }

        private IEnumerable<ResourceModel> MountResources(IEnumerable<Resource> innerResources, List<ResourceModel> resourceModels)
        {
            foreach (var innerResource in innerResources)
            {
                var resourceModel = new ResourceModel(innerResource.Description, innerResource.Permissions);
                
                MountResources(innerResource.InnerResources, resourceModel.Resources);

                resourceModels.Add(resourceModel);
            }

            return resourceModels;
        }
    }
}
