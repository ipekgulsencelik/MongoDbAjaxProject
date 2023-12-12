using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDbAjaxProject.DAL.Entities;
using MongoDbAjaxProject.DAL.Settings;

namespace MongoDbAjaxProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductController(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        }

        public async Task<IActionResult> Index()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductID == id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var value = await _productCollection.Find(x => x.ProductID == id).FirstOrDefaultAsync();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == product.ProductID, product);
            return RedirectToAction("Index");
        }
    }
}