using DistributedCash.Model;

namespace DistributedCash.Data;

public class ProductList
{
    public List<Product> products()
    { 
     List<Product> productDatas = new List<Product>();

        productDatas = new List<Product>()
        {
            new Product(){  ProductId=1, ProductName="Lux", ProductDescription="Beauty", Stock=52},
            new Product(){  ProductId=2, ProductName="Sunsilk", ProductDescription="Shampo", Stock=10},
            new Product(){  ProductId=3, ProductName="Pen", ProductDescription="Pen", Stock=01},
            new Product(){  ProductId=4, ProductName="Mobile", ProductDescription="Electronics", Stock=56},
            new Product(){  ProductId=5, ProductName="Laptop", ProductDescription="Electronics", Stock=20},
            new Product(){  ProductId=6, ProductName="Plate", ProductDescription="Plate", Stock=31},
            new Product(){  ProductId=7, ProductName="MoneyBag", ProductDescription="Money", Stock=25}
        };

        return productDatas;
    }
}
