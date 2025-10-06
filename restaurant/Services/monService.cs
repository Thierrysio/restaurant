using restaurant.Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant.Services
{
    public class monService
    {
        public ObservableCollection<PlatCommandeCountDto> CompterPlatsCommandes(Restaurant resto)

        {
            var res = new ObservableCollection<PlatCommandeCountDto>();

            foreach (var plat in resto.Menu)
            {
                int total = 0;
                foreach (var cmd in resto.Commandes)
                {
                    foreach (var ligne in cmd.Lignes)
                    {
                        if (ligne.Plat.Nom == plat.Nom) total += ligne.Quantite;
                    }
                }
                res.Add(new PlatCommandeCountDto
                {
                    NomPlat = plat.Nom,
                    TotalCommandes = total
                });

            }

            return res;


        }
        public ObservableCollection<TableCommandesCountDto> CompterCommandesParTable(Restaurant resto, DateTime debut, DateTime fin)
        {
            var res = new ObservableCollection<TableCommandesCountDto>();

            foreach (var table in resto.Tables)
            {
                int nb = 0;
                foreach (var cmd in resto.Commandes)
                {
                    if (cmd.DateCommande < debut || cmd.DateCommande > fin) continue;
                    if (cmd.Table.Numero == table.Numero) nb++;

                }
                res.Add(new TableCommandesCountDto
                {
                    TableNumero = table.Numero,
                    NbCommandes = nb
                });
            }
                return res;
        }
    }
}
