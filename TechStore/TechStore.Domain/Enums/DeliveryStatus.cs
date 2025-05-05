using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Enums;

public enum DeliveryStatus
{
    InProcessing = 1,
    InStore = 2,
    InDeliveryService = 3,
    Canceled = 4,
    Received = 5
}
