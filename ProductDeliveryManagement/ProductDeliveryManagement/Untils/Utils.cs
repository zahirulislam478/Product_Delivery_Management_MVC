using ProductDeliveryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductDeliveryManagement.Untils
{
    public class Utils
    {
        public static OrderStatus GetStatus(OrderDelivery o)
        {
            if (o.IsDelivered)
                return OrderStatus.Delivered;
            else
                return OrderStatus.AssignedToDeliver;
        }
    }
}