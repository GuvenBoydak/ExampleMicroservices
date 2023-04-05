using System.Linq;

namespace ExampleMicroservice.Shared.Service
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get;}
    }
}