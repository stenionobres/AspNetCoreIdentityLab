using System;
using System.Collections.Generic;

namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public class Resource
    {
        private readonly List<Resource> _resources = new List<Resource>();
        private readonly List<Permissions> _permissions = new List<Permissions>();

        public string Description { get; private set; }
        public IEnumerable<Resource> InnerResources => _resources;
        public IEnumerable<Permissions> Permissions => _permissions;

        public Resource(string description)
        {
            ValidateDescription(description);
            Description = description;
        }

        public Resource(string description, Permissions permission)
        {
            ValidateDescription(description);
            Description = description;
            _permissions.Add(permission);
        }

        public Resource Add(params Resource[] innerResources)
        {
            _resources.AddRange(innerResources);
            return this;
        }

        public Resource Add(params Permissions[] permissions)
        {
            _permissions.AddRange(permissions);
            return this;
        }

        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));
        }
    }
}
