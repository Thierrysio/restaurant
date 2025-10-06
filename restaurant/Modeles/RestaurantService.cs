using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant.Modeles
{
    public class RestaurantService
    {
    }

    public class Restaurant
    {
        public string Nom { get; set; } = "";
        public ObservableCollection<Plat> Menu { get; set; } = new();
        public ObservableCollection<Commande> Commandes { get; set; } = new();
    }

    public class Plat
    {
        public string Nom { get; set; } = "";
        public string Categorie { get; set; } = ""; // Entrée, Plat, Dessert
        public double Prix { get; set; }
    }

    public class Commande
    {
        public int Numero { get; set; }
        public string Client { get; set; } = "";
        public ObservableCollection<LigneCommande> Lignes { get; set; } = new();
    }

    public class LigneCommande
    {
        public Plat Plat { get; set; } = default!;
        public int Quantite { get; set; }
    }

    public class PlatCommandeCountDto
    {
        public string NomPlat { get; set; } = "";
        public int TotalCommandes { get; set; }
    }

}
