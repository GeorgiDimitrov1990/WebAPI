﻿using CoinMapWebAPI.DAL.Data;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
