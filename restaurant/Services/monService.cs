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

        public ObservableCollection<ServeurCaDto> CalculerCaParServeur(Restaurant resto, DateTime debut, DateTime fin)
        {
            var res = new ObservableCollection<ServeurCaDto>();

            foreach (var serveur in resto.Serveurs)
            {
                double total = 0;
                foreach (var cmd in resto.Commandes)
                {
                    if (cmd.DateCommande < debut || cmd.DateCommande > fin) continue;
                    if (serveur.Id == cmd.Serveur.Id)
                    {
                        double MontantCommande = 0;
                        foreach (var lc in cmd.Lignes)
                        {
                            MontantCommande += lc.Quantite * lc.Plat.Prix;
                        }
                        total += MontantCommande;
                    }
                }
                res.Add(new ServeurCaDto
                {
                    ServeurId = serveur.Id,
                    ServeurNom = serveur.Nom ?? "",
                    Montant = total
                });
            }

            return res;
        }

        public ObservableCollection<PlatQuantiteDto> QuantitesVenduesParPlat(Restaurant resto, DateTime jour)
        {
            var res = new ObservableCollection<PlatQuantiteDto>();
            foreach (var plat in resto.Menu)
            {
                int qte = 0;
                foreach (var cmd in resto.Commandes)
                {
                    if (cmd.DateCommande == jour)
                    {
                        foreach (var ligne in cmd.Lignes)
                        {
                            qte += ligne.Quantite;
                        }
                    }
                }
                res.Add(new PlatQuantiteDto
                {

                    PlatNom = plat.Nom ?? "",
                    Quantite = qte
                });
            }
            return res;
        }

        public ObservableCollection<TableDelaiMoyenDto> CalculerDelaiMoyenServiceParTable(Restaurant resto, int tableNumero)
        {
            var res = new ObservableCollection<TableDelaiMoyenDto>();

            TimeSpan total = TimeSpan.Zero;
            int count = 0;

            foreach (var cmd in resto.Commandes)
            {
                if (cmd.Table.Numero != tableNumero) continue;
                if (cmd.Statut != "servi") continue;

                total = total.Add(cmd.DateServi - cmd.DateCommande);
                count++;
            }

            var dto = new TableDelaiMoyenDto
            {
                TableNumero = tableNumero,
                DelaiMoyen = TimeSpan.FromTicks(total.Ticks / count),
                NbCommandes = count
            };

            return res;
        }

        public ObservableCollection<AlerteAllergeneDto> ListerCommandesAvecAllergene(Restaurant resto, string allergene)
        {
            var result = new ObservableCollection<AlerteAllergeneDto>();

            foreach (var cmd in resto.Commandes)
            {
                bool contient = false;
                foreach (var lc in cmd.Lignes)
                {
                    foreach (var alg in lc.Plat.Allergenes)
                    {
                        if (alg.Nom == allergene)
                        {
                            contient = true;
                            break;
                        }
                    }

                    if (contient) break;
                }
                var dto = new AlerteAllergeneDto
                {
                    CommandeId = cmd.Numero
                };
            }

            return result;
        }

        public ObservableCollection<TableTauxNonServiDto> TauxNonServiParTable(Restaurant resto, DateTime debut, DateTime fin)
        {
            var result = new ObservableCollection<TableTauxNonServiDto>();

            foreach (var table in resto.Tables)
            {
                int totalPlats = 0;
                int totalNonServi = 0;
                foreach (var cmd in resto.Commandes)
                {
                    
                    if (cmd.Table.Numero != table.Numero) continue;
                    
                    foreach(var lc in cmd.Lignes)
                    {
                        totalPlats++;
                        if (!lc.EstServi) totalNonServi++;
                    }
                }

                var dto = new TableTauxNonServiDto
                {
                    TableNumero = table.Numero,
                    TotalLignes = totalPlats,
                    LignesNonServies = totalNonServi,
                    Taux = (double)totalNonServi / (double)totalPlats
                };
            }
                return result;
        }

        public ObservableCollection<PlatQuantiteDto> Top3PlatsParQuantite(Restaurant resto, DateTime debut, DateTime fin)
        {
            var result = new ObservableCollection<PlatQuantiteDto>();

            var dico = new Dictionary<Plat, int>();

            foreach (var plat in resto.Menu)
            {
                if (!dico.ContainsKey(plat)) dico.Add(plat, 0);
            }

            foreach (var cmd in resto.Commandes)
            {
                foreach (var lc in cmd.Lignes)
                {
                    if (!lc.EstServi) continue;
                    dico[lc.Plat] += lc.Quantite;

                }
            }

            int prises = 0;
            while (prises < 3)
            {
                Plat bestPlat = null;
                int bestQte = -1;

                foreach (var kvp in dico)
                {

                    var plat = kvp.Key;
                    var qte = kvp.Value;

                    if (qte > bestQte)
                    {
                        bestQte = qte;
                        bestPlat = plat;
                    }

                }
                result.Add(new PlatQuantiteDto
                {
                    PlatId = 0, // remplace par bestPlat.Id si tu ajoutes un Id au modèle
                    PlatNom = bestPlat.Nom ?? "",
                    Quantite = bestQte
                });
                dico.Remove(bestPlat);
                prises++;


            }

                return result;
        }

        }
}