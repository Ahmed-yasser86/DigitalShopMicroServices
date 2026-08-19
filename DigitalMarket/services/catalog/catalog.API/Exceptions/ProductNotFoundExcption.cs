using BuildingBlocks;
using BuildingBlocks.Exceptions;

namespace catalog.API.Exceptions
{
    public class ProductNotFoundExcption : NotFoundException
    {

        public ProductNotFoundExcption(Guid id):base("product ", id)
        {

        }
    }
}
