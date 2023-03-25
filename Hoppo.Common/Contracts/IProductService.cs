using Hoppo.Common.Common;
using Hoppo.Models.DTOs.Product;

namespace Hoppo.Common.Contracts
{
    public interface IProductService
    {
        Task<GetManyResult<Item>> GetProductsAsync();
        Task<bool> CreateItemXml(IEnumerable<Item> products);
    }
}
