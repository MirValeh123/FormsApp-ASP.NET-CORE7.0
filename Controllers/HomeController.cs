using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FormsApp.Controllers;

public class HomeController : Controller
{


    public IActionResult Index(string q, string c)
    {
        var products = Repository.Products;
        if (!String.IsNullOrEmpty(q))
        {
            ViewBag.q = q;
            products = products.Where(p => p.Name.ToLower().Contains(q)).ToList();

        }
        if (!String.IsNullOrEmpty(c) && c != "0")
        {
            ViewBag.c = c;
            products = products.Where(p => p.CategoryId == int.Parse(c)).ToList();

        }
        // ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name",c);

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = c,
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var extension = "";
        if (imageFile != null)
        {
            var allowedExtension = new[] { ".jpg", ".jpeg", ".png" };
            extension = Path.GetExtension(imageFile.FileName);

            if (!allowedExtension.Contains(extension))
            {
                ModelState.AddModelError("", "Geçerli resim seçiniz");
            }
        }
        if (ModelState.IsValid)
        {
            if (imageFile != null)
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
                model.ProductId = Repository.Products.Count + 1;
                Repository.CreateProduct(model);
                return RedirectToAction("Index");
            }

        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

        return View(model);
    }

    [HttpGet]

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();

        }

        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {

            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;

            }
            Repository.UpdateProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

        return View(model);



    }
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();

        }
        return View("DeleteConfirm",entity);
    }
    [HttpPost]
    public IActionResult Delete(int id,int ProductId)
    {
        if (id != ProductId)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == ProductId);
        if (entity == null)
        {
            return NotFound();

        }
        Repository.DeleteProduct(entity);
        return RedirectToAction("Index");

    }

    [HttpPost]
    public IActionResult EditProducts(List<Product> Products)
    {
        foreach (var item in Products)
        {
            Repository.EditIsActive(item);
        }
        return RedirectToAction("Index");
    }
}
