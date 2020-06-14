using System.Collections.Generic;
using ExpectedObjects;
using NUnit.Framework;
using NUnitTestProject1;

namespace NUnitTestProject1
{
    [TestFixture]
    public class CashTests
    {
        [Test]
        public void TestProcessOrder()
        {
            var fakeClass = new FakeClass();

            fakeClass.SetProductionInfo(new ProductInfo { ProductId = 1, Price = 123 },
                                        new ProductInfo { ProductId = 2, Price = 634 },
                                        new ProductInfo { ProductId = 3, Price = 200 },
                                        new ProductInfo { ProductId = 4, Price = 150 });

            var processOrder = fakeClass.ProcessOrder(new PaymentInfo
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
            });

            new { UserId = 123, OrderAmount = 2298M }.ToExpectedObject().ShouldMatch(processOrder.order);

            new[]
            {
                new { ProductId = 1, Amount = 246M },
                new { ProductId = 2, Amount = 1902M },
                new { ProductId = 4, Amount = 150M },
            }.ToExpectedObject().ShouldMatch(processOrder.orderDetailList);
        }
    }
}

public class FakeClass : Class1
{
    private IEnumerable<ProductInfo> _productInfos;

    public void SetProductionInfo(params ProductInfo[] productInfos)
    {
        _productInfos = productInfos;
    }

    protected override IEnumerable<ProductInfo> GetProductInfoData()
    {
        return _productInfos;
    }
}