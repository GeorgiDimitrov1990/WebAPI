using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist
{
    public class CategoryAlreadyExistException : AlreadyExistsException
    {
        public CategoryAlreadyExistException(string name) : base($"Category with category name '{name}' already exists.")
        {

        }
    }
}
