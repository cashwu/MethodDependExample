using System.Collections.Generic;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCaseSource(nameof(TestCase))]
        public void TestProcessOrder(PaymentInfo source, (Order, List<OrderDetail>) expectResult)
        {
            IClass1 class1 = new Class1();
            var actual = class1.ProcessOrder(source);
            Assert.AreEqual(expectResult.Item1.UserId, actual.order.UserId);
            Assert.AreEqual(expectResult.Item1.OrderAmount, actual.order.OrderAmount);
            Assert.AreEqual(expectResult.Item2.Count, actual.orderDetailList.Count);
            for (var i = 0; i < actual.orderDetailList.Count; i++)
            {
                Assert.AreEqual(expectResult.Item2[i].Amount, actual.orderDetailList[i].Amount);
            }
        }

        static readonly object[] TestCase =
        {
            new object[] { new PaymentInfo
                {
                    UserId = 123,
                    Detail = new List<PaymentDetail>
                    {
                        new PaymentDetail
                        {
                            ProdId = 1,
                            Count = 2
                        },
                        new PaymentDetail
                        {
                            ProdId = 2,
                            Count = 3
                        },
                        new PaymentDetail
                        {
                            ProdId = 4,
                            Count = 1
                        }
                    }
                },(
                new Order
                {
                    UserId = 123,
                    OrderAmount = 2298M
                },
                new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        ProductId = 1,
                        Amount = 246M
                    },
                    new OrderDetail
                    {
                        ProductId = 2,
                        Amount = 1902M
                    },
                    new OrderDetail
                    {
                        ProductId = 4,
                        Amount = 150M
                    },
                })
            },
            new object[] { new PaymentInfo
                {
                    UserId = 678,
                    Detail = new List<PaymentDetail>
                    {
                        new PaymentDetail
                        {
                            ProdId = 1,
                            Count = 5
                        },
                        new PaymentDetail
                        {
                            ProdId = 2,
                            Count = 4
                        },
                        new PaymentDetail
                        {
                            ProdId = 3,
                            Count = 7
                        },
                        new PaymentDetail
                        {
                            ProdId = 4,
                            Count = 3
                        }
                    }
                },(
                    new Order
                    {
                        UserId = 678,
                        OrderAmount = 5001M
                    },
                    new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductId = 1,
                            Amount = 615M
                        },
                        new OrderDetail
                        {
                            ProductId = 2,
                            Amount = 2536M
                        },
                        new OrderDetail
                        {
                            ProductId = 3,
                            Amount = 1400M
                        },
                        new OrderDetail
                        {
                            ProductId = 3,
                            Amount = 450M
                        },
                    })
            }
        };
    }
}