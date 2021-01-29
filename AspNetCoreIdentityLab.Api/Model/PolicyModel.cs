using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;

namespace AspNetCoreIdentityLab.Api.Model
{
    public class PolicyModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }

        public PolicyModel() { }

        public PolicyModel(Permissions permission)
        {
            Id = Convert.ToInt32(permission);
            Description = permission.ToString();
            
            var enumType = permission.GetType();
            var enumMember = enumType.GetMember(permission.ToString());
            var displayAttribute = enumMember[0].GetCustomAttribute<DisplayAttribute>();

            Label = displayAttribute.Description;
        }
    }
}
