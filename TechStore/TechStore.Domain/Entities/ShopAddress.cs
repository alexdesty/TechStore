﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public class ShopAddress:Entity
{
    public string Address { get; set; } = string.Empty;

    public List<Order> Orders { get; set; } = [];
}
