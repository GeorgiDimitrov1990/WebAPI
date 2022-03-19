using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Exceptions.NotFound
{
    class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int id) : base ($"Category with Id '{id}' was not found.")
        {

        }
    }
}
