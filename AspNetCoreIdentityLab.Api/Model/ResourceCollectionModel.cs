using System.Linq;
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
                
            MountResources(principalResource);
        }

        private ResourceModel MountResources(Resource resource)
        {
            if (resource.InnerResources.Any())
            {
                foreach (var innerResource in resource.InnerResources)
                {
                    var resourceModel = MountResources(innerResource);

                    Resources.Resources.Add(resourceModel);
                }
            }

            var innerResourceModel = new ResourceModel(resource.Description, resource.Permissions);

            return innerResourceModel;
        }
    }
}
