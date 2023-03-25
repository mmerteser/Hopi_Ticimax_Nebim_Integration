using Hoppo.Common.Common;
using TicimaxProductService;

namespace Hoppo.Business.Services
{
    public class TicimaxService
    {
        private readonly IUrunServis _ticimaxProductService;
        public TicimaxService(IUrunServis ticimaxProductService)
        {
            _ticimaxProductService = ticimaxProductService;
        }

        public async Task<List<UrunKarti>> GetAllProductsAsync()
        {
            var urunFiltre = new UrunFiltre
            {
                Aktif = -1,
                Firsat = -1,
                Indirimli = -1,
                Vitrin = -1,
                KategoriID = 0, // 0 gönderilirse filtre yapılmaz.
                MarkaID = 0, // 0 gönderilirse filtre yapılmaz.
                UrunKartiID = 0 //0 gönderilirse filtre yapılmaz.
            };

            var urunSayfalama = new UrunSayfalama
            {
                BaslangicIndex = 0, // Başlangıç değeri
                KayitSayisi = 0, // Bir sayfada görüntülenecek ürün sayısı
                SiralamaDegeri = "ID", // Hangi sütuna göre sıralanacağı
                SiralamaYonu = "ASC" // Artan "ASC", azalan "DESC"
            };

            return await _ticimaxProductService.SelectUrunAsync(Configuration.TicimaxUyeKodu, urunFiltre, urunSayfalama);
        }

        public async Task<List<UrunKarti>> GetProductAsync(string productId)
        {
            var urunFiltre = new UrunFiltre
            {
                Aktif = -1,
                Firsat = -1,
                Indirimli = -1,
                Vitrin = -1,
                KategoriID = 0, // 0 gönderilirse filtre yapılmaz.
                MarkaID = 0, // 0 gönderilirse filtre yapılmaz.
                UrunKartiID = 0, //0 gönderilirse filtre yapılmaz.
                Barkod = productId
            };

            var urunSayfalama = new UrunSayfalama
            {
                BaslangicIndex = 0, // Başlangıç değeri
                KayitSayisi = 100, // Bir sayfada görüntülenecek ürün sayısı
                SiralamaDegeri = "ID", // Hangi sütuna göre sıralanacağı
                SiralamaYonu = "ASC" // Artan "ASC", azalan "DESC"
            };
            return await _ticimaxProductService.SelectUrunAsync(Configuration.TicimaxUyeKodu, urunFiltre, urunSayfalama);
        }
    }
}
