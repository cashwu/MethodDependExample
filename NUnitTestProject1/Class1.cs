﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestProject1
{
    public class Class1 : IClass1
    {
        public virtual (Order order, List<OrderDetail> orderDetailList) ProcessOrder(PaymentInfo paymentInfo)
        {
            var order = new Order
            {
                UserId = paymentInfo.UserId,
                OrderId = Guid.NewGuid().ToString()
            };
            var orderDetail = paymentInfo.Detail.Select(item => ProcessOrderDetail(order.OrderId, item)).ToList();
            order.OrderAmount = orderDetail.Sum(x => x.Amount);
            return (order, orderDetail);
        }

        public virtual OrderDetail ProcessOrderDetail(string orderId, PaymentDetail paymentDetailInfo)
        {
            //Do many something
            var productInfo = GetProductInfo(paymentDetailInfo.ProdId);
            var orderDetail = new OrderDetail
            {
                OrderId = orderId,
                ProductId = productInfo.ProductId,
                Amount = productInfo.Price * paymentDetailInfo.Count
            };
            //Do many something
            return orderDetail;
        }

        protected virtual ProductInfo GetProductInfo(int productId)
        {
            return GetProductInfoData().Single(x => x.ProductId == productId);
        }

        protected virtual IEnumerable<ProductInfo> GetProductInfoData()
        {
            return new List<ProductInfo>
            {
                new ProductInfo
                {
                    ProductId = 1,
                    Price = 123
                },
                new ProductInfo()
                {
                    ProductId = 2,
                    Price = 634
                },
                new ProductInfo()
                {
                    ProductId = 3,
                    Price = 200
                },
                new ProductInfo()
                {
                    ProductId = 4,
                    Price = 150
                }
            };
        }
    }

    public interface IClass1
    {
        (Order order, List<OrderDetail> orderDetailList) ProcessOrder(PaymentInfo paymentInfo);
        OrderDetail ProcessOrderDetail(string orderId, PaymentDetail paymentDetailInfo);
    }

    public class PaymentInfo
    {
        public int UserId { get; set; }
        public List<PaymentDetail> Detail { get; set; }
    }

    public class PaymentDetail
    {
        public int ProdId { get; set; }
        public int Count { get; set; }
    }


    public class Order
    {
        public int UserId { get; set; }
        public string OrderId { get; set; }
        public decimal OrderAmount { get; set; }
    }

    public class OrderDetail
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
    }

    public class ProductInfo
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}