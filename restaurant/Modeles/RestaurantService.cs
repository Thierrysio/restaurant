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

    public class Serveur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
    }

    public class Commande
    {
        public int Numero { get; set; }
        public string Client { get; set; } = "";

        public DateTime DateCommande { get; set; };
        public ObservableCollection<LigneCommande> Lignes { get; set; } = new();
        public Serveur Serveur { get; set; } = default!; // 🔹 Lien vers le serveur

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

    public class TableCommandesCountDto
    {
        public int TableNumero { get; set; }
        public int NbCommandes { get; set; }
    }

    public class ServeurCaDto
    {
        public int ServeurId { get; set; }
        public string ServeurNom { get; set; } = "";
        public decimal Montant { get; set; }
    }

    public class PlatQuantiteDto
    {
        public int PlatId { get; set; }
        public string PlatNom { get; set; } = "";
        public int Quantite { get; set; }
    }

    public class TableDelaiMoyenDto
    {
        public int TableNumero { get; set; }
        public TimeSpan DelaiMoyen { get; set; }
        public int NbCommandes { get; set; }
    }

    public class AlerteAllergeneDto
    {
        public int CommandeId { get; set; }
        public DateTime DateHeure { get; set; }
        public int TableNumero { get; set; }
        public string ServeurNom { get; set; } = "";
        public string PlatNom { get; set; } = "";
        public int Quantite { get; set; }
    }

    public class TableTauxNonServiDto
    {
        public int TableNumero { get; set; }
        public int TotalLignes { get; set; }
        public int LignesNonServies { get; set; }
        public double Taux { get; set; } // 0..1
    }

    public class ServeurTicketMoyenDto
    {
        public int ServeurId { get; set; }
        public string ServeurNom { get; set; } = "";
        public decimal Total { get; set; }
        public int NbCommandes { get; set; }
        public decimal TicketMoyen { get; set; }
    }

    public class TableDto
    {
        public int Numero { get; set; }
        public int Capacite { get; set; }
        public string Zone { get; set; } = "";
    }

    public class IncoherencePrixDto
    {
        public int CommandeId { get; set; }
        public string PlatNom { get; set; } = "";
        public decimal PrixPlat { get; set; }
        public decimal PrixLigne { get; set; }
        public int Quantite { get; set; }
    }

}
