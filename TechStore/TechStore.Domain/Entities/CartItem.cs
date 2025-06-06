﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public class CartItem:Entity
{ 
    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int CartId { get; set; }

    public Cart Cart { get; set; }

    public int Amount { get; set; }
}
