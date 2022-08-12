



namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg=>cfg.AddProfile(typeof(ProductShopProfile)));


            ProductShopContext productShopContext = new ProductShopContext();

            //productShopContext.Database.EnsureDeleted();

          //  productShopContext.Database.EnsureCreated();

          string inputJsonUsers = File.ReadAllText($"../../../Datasets/categories-products.json");

          //Console.WriteLine(ImportCategoryProducts(productShopContext, inputJsonUsers));

          Console.WriteLine(GetSoldProducts(productShopContext));

        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);

            StringBuilder output = new StringBuilder();

            ICollection<User> validUsers = new List<User>();

            foreach (var userDto in userDtos)
            {

                if (!IsValid(userDtos))
                {
                    continue;

                }
                
                User user = Mapper.Map<User>(userDto);

                validUsers.Add(user);
            }
            
            context.Users.AddRange(validUsers);

            context.SaveChanges();

            return output.AppendLine($"Successfully imported {validUsers.Count}").ToString().Trim();


        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {

            ImportProductDto[] productDtos = JsonConvert.DeserializeObject<ImportProductDto[]>(inputJson);

            StringBuilder output = new StringBuilder();

            ICollection<Product> validProducts = new List<Product>();

            foreach (var productDto in productDtos)
            {

                if (!IsValid(productDto))
                {
                    continue;
                }

                Product product = Mapper.Map<Product>(productDto);

                validProducts.Add(product);

            }

            context.Products.AddRange(validProducts);

            context.SaveChanges();

            return output.AppendLine($"Successfully imported {validProducts.Count}").ToString().Trim();

        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {

            ImportCategoryDto[] categoryDtos = JsonConvert.DeserializeObject<ImportCategoryDto[]>(inputJson);

            StringBuilder output = new StringBuilder();

            ICollection<Category> valCategories = new List<Category>();

            foreach (var categoryDto in categoryDtos)
            {

                if (!IsValid(categoryDto))
                {
                    continue;
                }

                Category category = Mapper.Map<Category>(categoryDto);

                valCategories.Add(category);


            }

            context.Categories.AddRange(valCategories);

            context.SaveChanges();

            return output.AppendLine($"Successfully imported {valCategories.Count}").ToString().Trim();

        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {

            ImportCategory_ProductDto[] categoryProductDto = JsonConvert.DeserializeObject<ImportCategory_ProductDto[]>(inputJson);

            StringBuilder output = new StringBuilder();

            ICollection<CategoryProduct> valCategories = new List<CategoryProduct>();

            foreach (var dtoCategoryProduct in categoryProductDto)
            {
                if (!IsValid(dtoCategoryProduct))
                {
                    continue;
                }

                CategoryProduct categoryProduct = Mapper.Map<CategoryProduct>(dtoCategoryProduct);

                valCategories.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(valCategories);

            context.SaveChanges();

            return output.AppendLine($"Successfully imported {valCategories.Count}").ToString().Trim();

        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            ExportProductDto[] exportProduct = context.Products.Where(x => x.Price > 500 && x.Price < 1000)
                .OrderBy(x => x.Price).ProjectTo<ExportProductDto>()
                .ToArray();

            return JsonConvert.SerializeObject(exportProduct,Formatting.Indented);

        }


        public static string GetSoldProducts(ProductShopContext context)
        {
            ExportSoldProductDto[] users = context.Users.Where(u => u.ProductsSold.Any(b => b.BuyerId.HasValue))
                .OrderBy(u => u.LastName).ThenBy(u => u.LastName).ProjectTo<ExportSoldProductDto>().ToArray();

            return JsonConvert.SerializeObject(users, Formatting.Indented);
        }

    }
}
