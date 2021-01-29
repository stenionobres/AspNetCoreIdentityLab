using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using AspNetCoreIdentityLab.Persistence.EntityFrameworkContexts;

namespace AspNetCoreIdentityLab.Persistence.Mappers
{
    public class PolicyMapper
    {
        public Policy GetById(int id)
        {
            using (var context = new AuthenticationDbContext())
            {
                return context.Policy.Find(id);
            }
        }

        public void Save(Policy policy)
        {
            using (var context = new AuthenticationDbContext())
            {
                context.Policy.Add(policy);
                context.SaveChanges();
            }
        }

        public void Update(Policy policy)
        {
            using (var context = new AuthenticationDbContext())
            {
                context.Policy.Update(policy);
                context.SaveChanges();
            }
        }
    }
}
