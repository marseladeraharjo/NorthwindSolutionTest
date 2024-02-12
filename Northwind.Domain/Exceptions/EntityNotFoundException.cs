﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(int id) : base($"Entity with identifier {id} not found.")
        {
        }

    }
}
