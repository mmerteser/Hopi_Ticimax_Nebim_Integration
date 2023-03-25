using Hoppo.Common.Common;
using Hoppo.Common.Contracts;
using Hoppo.Models.DTOs.Product;
using Microsoft.Extensions.Logging;
using TicimaxProductService;
using Hoppo.Common.Helpers;

namespace Hoppo.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbContext _context;
        private TicimaxService _ticimaxService;
        private readonly IUrunServis _urunServis;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IDbContext context, IUrunServis urunServis, ILogger<ProductService> logger)
        {
            _context = context;
            _urunServis = urunServis;
            _ticimaxService = new TicimaxService(_urunServis);
            _logger = logger;
        }

        public async Task<GetManyResult<Item>> GetProductsAsync()
        {
            var result = new GetManyResult<Item>();

            try
            {
                _logger.LogWarning("Nebimden ürünler alınıyor...");

                var products = await _context.QueryAsync<Item>("EXEC usp_Hopi_UrunListesi");

                _logger.LogWarning($"{products.Count()} adet ürün veritabanından alındı.");

                if (!products.Any())
                    return result;

                _logger.LogWarning("Ticimaxten ürünler alınıyor...");

                var ticimaxProducts = await _ticimaxService.GetAllProductsAsync();

                _logger.LogWarning($"{ticimaxProducts.Count()} adet ürün Ticimaxten alındı.");

                foreach (var ticimaxProduct in ticimaxProducts)
                {
                    foreach (var item in ticimaxProduct.Varyasyonlar)
                    {
                        if (String.IsNullOrEmpty(item.Barkod))
                            continue;

                        var product = products.FirstOrDefault(i => i.Gtin == item.Barkod);

                        if (product is null)
                        {
                            _logger.LogError($"{item.Barkod} barkodlu ürün Nebimde bulunamadı!");
                            break;
                        }

                        if (String.IsNullOrEmpty(product.Gtin))
                        {
                            _logger.LogError($"{product.Id} kodlu ürünün Nebim barkodu bulunamadı!");
                            break;
                        }

                        product.Link = Configuration.WebSiteUrl + ticimaxProduct.UrunSayfaAdresi;

                        if (ticimaxProduct.Resimler is not null)
                            switch (ticimaxProduct.Resimler.Count)
                            {
                                case 1:
                                    product.Additional_Image1_Link = ticimaxProduct.Resimler[0];
                                    break;
                                case 2:
                                    product.Additional_Image1_Link = ticimaxProduct.Resimler[0];
                                    product.Additional_Image2_Link = ticimaxProduct.Resimler[1];
                                    break;
                                case 3:
                                    product.Additional_Image1_Link = ticimaxProduct.Resimler[0];
                                    product.Additional_Image2_Link = ticimaxProduct.Resimler[1];
                                    product.Additional_Image3_Link = ticimaxProduct.Resimler[2];
                                    break;
                                default:
                                    break;
                            }
                    }
                }

                #region Old
                /*UrunKarti ticimaxItem = new UrunKarti();

                foreach (var item in ticimaxProducts)
                {
                    var varyant = item.Varyasyonlar.FirstOrDefault(i => i.Barkod.Contains(product.Gtin));

                    if (varyant is null)
                        continue;

                    ticimaxItem = item;

                    //if (varyant is null)
                    //{
                    //    _logger.LogWarning($"{product.Gtin} barkodlu ürün Ticimaxte bulunamadı!");
                    //    break;
                    //}

                    product.Link = Configuration.WebSiteUrl + ticimaxItem.UrunSayfaAdresi;

                    if (ticimaxItem.Resimler is not null)
                        switch (ticimaxItem.Resimler.Count)
                        {
                            case 1:
                                product.AdditionalImage1Link = ticimaxItem.Resimler[0];
                                break;
                            case 2:
                                product.AdditionalImage1Link = ticimaxItem.Resimler[0];
                                product.AdditionalImage2Link = ticimaxItem.Resimler[1];
                                break;
                            case 3:
                                product.AdditionalImage1Link = ticimaxItem.Resimler[0];
                                product.AdditionalImage2Link = ticimaxItem.Resimler[1];
                                product.AdditionalImage3Link = ticimaxItem.Resimler[2];
                                break;
                            default:
                                break;
                        }

                    break;
                }
            }*/
                #endregion

                var productList = products.ToList();

                productList.RemoveAll(i => String.IsNullOrEmpty(i.Link));

                result.Data = productList;

                return result;
            }
            catch (Exception ex)
            {
                result.Data = Enumerable.Empty<Item>();
                result.Success = false;
                result.Message = ex.Message;

                _logger.LogCritical(ex.Message);

                return result;
            }
        }

        public async Task<bool> CreateItemXml(IEnumerable<Item> products)
        {
            CheckIfExistsXmlFolder();

            Product product = new() { Products = products.ToList() };

            var xmlString = await XmlHelper.ConvertListToXmlAsync(product);

            string path = Configuration.XmlPath + "arta-hopi.xml";


            await File.WriteAllTextAsync(path, xmlString);
            return true;
        }

        private void CheckIfExistsXmlFolder()
        {
            try
            {
                if (!Directory.Exists(Configuration.XmlPath))
                    Directory.CreateDirectory(Configuration.XmlPath);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Dosya oluşturulamadı {message}", ex.Message);
            }
        }
    }
}
