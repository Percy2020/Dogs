using MiracleMileAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Calculations
{
    public class Aktiebolag
    {

        // Definera funktioner
        public Double Calc_Egenavgifter(Double Overskott)
        {
            Double egenavgifter;
            if (Overskott > 200000)
            {
                egenavgifter = 200000 * (0.2897 - 0.075) + (Overskott - 200000) * 0.2897;
            }
            else if (Overskott > 40000)

            {
                egenavgifter = Overskott * (0.2897 - 0.075);
            }
            else if (Overskott > 1000)

            {
                egenavgifter = Overskott * 0.2897;
            }
            else
            {
                egenavgifter = 0;

            }
            return egenavgifter;
        }

        public Double Calc_Grundavdraget(Double gl, Double prisbasbelopp)
        {
            Double grundavdrag = 0;

            if (gl <= 0.99 * prisbasbelopp)
            {
                grundavdrag = 0.423 * prisbasbelopp;
            }
            else if (gl < 2.72 * prisbasbelopp)
            {
                grundavdrag = 0.225 * prisbasbelopp + 0.2 * gl;
            }
            else if (gl < 3.11 * prisbasbelopp)
            {
                grundavdrag = 0.770 * prisbasbelopp;
            }
            else if (gl < 7.88 * prisbasbelopp)
            {
                grundavdrag = 1.081 * prisbasbelopp - 0.1 * gl;
            }
            else if (gl >= 7.88 * prisbasbelopp)
            {
                grundavdrag = 0.293 * prisbasbelopp;

            }

            return grundavdrag;

        }

        public Double Calc_y(Double gl, Double inkomstbasbelopp)
        {
            Double y = 0;
            if (gl < 0.423 * inkomstbasbelopp)
            {
                y = 0;
            }
            else if (gl < 8.07 * inkomstbasbelopp)
            {
                y = 0.07 * gl;
            }
            else if (gl >= 8.07 * inkomstbasbelopp)
            {
                y = 0.07 * 8.07 * inkomstbasbelopp;

            }
            return y;
        }

        public Double Calc_x(Double arbetsinkomst, Double prisbasbelopp, Double kommunalskatt, Double grundavdrag)
        {
            Double x = 0;
            if (arbetsinkomst < 0.91 * prisbasbelopp)
            {
                x = (arbetsinkomst - grundavdrag) * kommunalskatt;
            }
            else if (arbetsinkomst < 3.24 * prisbasbelopp)
            {
                x = (0.91 * prisbasbelopp + 0.3405 * (arbetsinkomst - 0.91 * prisbasbelopp) - grundavdrag) * kommunalskatt;
            }
            else if (arbetsinkomst < 8.08 * prisbasbelopp)
            {
                x = (1.703 * prisbasbelopp + 0.128 * (arbetsinkomst - 3.24 * prisbasbelopp) - grundavdrag) * kommunalskatt;
            }
            else if (arbetsinkomst < 13.54 * prisbasbelopp)
            {
                x = (2.323 * prisbasbelopp - grundavdrag) * kommunalskatt;
            }
            else if (arbetsinkomst >= 13.54 * prisbasbelopp)
            {
                x = (2.323 * prisbasbelopp - 0.03 * (arbetsinkomst - 13.54 * prisbasbelopp) - grundavdrag) * kommunalskatt;

            }
            return x;
        }
        public Double Calc_Huvudregeln(Double lon_till_utbetalning, Double Anskafningsutgiften, Double utdelningsutrymme_ifran_tidigare_ar)
        {
            Double lonebaserat_utrymme;

            if (Math.Min(590400, 369000 + 0.05 * lon_till_utbetalning) < lon_till_utbetalning)
            {
                lonebaserat_utrymme = 0.5 * lon_till_utbetalning;
            }
            else
            {
                lonebaserat_utrymme = 0;
            }
            var Gransbelopp_Huvudregeln = lonebaserat_utrymme + Anskafningsutgiften * 0.0949 + utdelningsutrymme_ifran_tidigare_ar * 1.0349;
            return Gransbelopp_Huvudregeln;


        }

        public Double Calc_Forenklingsregeln(Double utdelningsutrymme_ifran_tidigare_ar)
        {
            Double Gransbelopp_forenklingsregeln;
            Gransbelopp_forenklingsregeln = 169125 + utdelningsutrymme_ifran_tidigare_ar * 1.0349;
            return Gransbelopp_forenklingsregeln;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Utrakning Aktiebolag. Med schablonbeskattning, for att forenkla deklarationen.
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Calc_Aktiebolag(Double Anskafningsutgiften, Double Kostnader_exklusive_loner, Double Intakter, Double utdelningsutrymme_ifrån_tidigare_ar)
        {

            Double sek = 0.25 * (Intakter - Kostnader_exklusive_loner);
            Double Overskott = (Intakter - Kostnader_exklusive_loner - sek);

            // Set
            Double prisbasbelopp = 46500;
            Double inkomstbasbelopp = 64400;
            Double kommunalskatt = 0.2988; // Stockholm 0.2998
            Double tak_statlig_skatt = 504400;
            Double tak_varnskatt = 703000;

            Double arbetsgivaravgift_aktiebolag_pct = 0.3142;
            Double lon_till_utbetalning = Overskott + Intakter;
            Double gl = Overskott + Intakter;
            Double arbetsinkomst = Overskott + Intakter; // inkluderar ej bidrag
            Double bolagsskattd = 0.214;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // __MAIN__
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Double grundavdrag = Calc_Grundavdraget(gl, prisbasbelopp);
            Double Arbetsgivaravgift = lon_till_utbetalning * arbetsgivaravgift_aktiebolag_pct;
            Double kommunal_inkomstskatt = (lon_till_utbetalning - grundavdrag) * kommunalskatt;

            Double statlig_skatt = Math.Max(0.20 * ((lon_till_utbetalning - grundavdrag) - tak_statlig_skatt), 0) + Math.Max(0.05 * ((lon_till_utbetalning - grundavdrag) - tak_varnskatt), 0);

            Double Lone_skatt = kommunal_inkomstskatt + statlig_skatt;
            Double x = Calc_x(arbetsinkomst, prisbasbelopp, kommunalskatt, grundavdrag);
            Double y = Calc_y(gl, inkomstbasbelopp);

            if ((gl - grundavdrag - y) < x)
            {
                x = gl - grundavdrag - y;

            }

            Double Lon_efter_skatt = lon_till_utbetalning - (Lone_skatt - x);

            Double Foretagets_vinst = Intakter - Kostnader_exklusive_loner - lon_till_utbetalning - Arbetsgivaravgift;

            Double Pengar_till_utdelning = Foretagets_vinst;

            Double Bolagsskatt = Foretagets_vinst * bolagsskattd;

            Double Foretagets_Vinst_efter_bolagsskatt = Foretagets_vinst - Bolagsskatt;

            Double Gransbelopp_Huvudregeln = Calc_Huvudregeln(lon_till_utbetalning, Anskafningsutgiften, utdelningsutrymme_ifrån_tidigare_ar);

            Double Gransbelopp_förenklingsregeln = Calc_Forenklingsregeln(utdelningsutrymme_ifrån_tidigare_ar);

            Double utdelningsutrymme = Math.Max(Gransbelopp_Huvudregeln, Gransbelopp_förenklingsregeln);

            Double maximal_mojlig_utdelning = Math.Min(Foretagets_Vinst_efter_bolagsskatt, utdelningsutrymme);

            Double pengar_i_AB_efter_skatt_utdelning = Foretagets_Vinst_efter_bolagsskatt - maximal_mojlig_utdelning;

            Double Skatt_100_pct_utdelning = maximal_mojlig_utdelning * 2 / 3 * 0.3;

            Double Utdelning_efter_skatt = maximal_mojlig_utdelning - Skatt_100_pct_utdelning;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Out
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            var company = new Company();

            company.Lon_A_skatt = Lone_skatt;
            

            company.Arsinkomst = Math.Round(gl);
            company.Manadsinkomst = Math.Round(gl / 12);

            company.Grundavdrag = Math.Round(gl - grundavdrag);
            company.Pensionsavgiften = Math.Round(kommunal_inkomstskatt);
            company.Jobbskatteavdraget = Math.Round(statlig_skatt);
            company.Lon_efter_skatt_per_manad = -(lon_till_utbetalning - grundavdrag) + tak_statlig_skatt;

            company.Statlig_skatt = Math.Round(statlig_skatt);
            company.Gransbelopp_Huvudregeln = Math.Round(0.2897 * Overskott);
            company.Gransbelopp_forenklingsregeln = Math.Min(0.075 * Overskott, 0.075 * 200000);
            company.Utdelning_efter_skatt = Math.Round(Utdelning_efter_skatt);

            company.Totalt_i_handen = Math.Round(grundavdrag);

            company.Pengar_i_foretag_post_max_utd = Math.Round(sek);

            company.Utdelningsutrymme_nex_yr = Math.Round(gl - sek);
            company.Total_lonerel_skatt_att_betala = Math.Round(gl - Lon_efter_skatt + sek);

            company.Total_lonerel_skatt_att_betala_per_mon = Math.Round(Skatt_100_pct_utdelning);

            company.Summa_skatter_bolag = Math.Round((Lon_efter_skatt));
            company.Summa_skatter_agare = Math.Round((Skatt_100_pct_utdelning) / 12);

            company.Tot_Utdel_rel_skatt_att_betala = Math.Round(utdelningsutrymme / 12);
            company.Total_skatt = Math.Round(gl - sek / 12);

            company.Effektiv_skatt_pct = Math.Round(100 * (1 - Lon_efter_skatt / (gl + sek)), 2);


            string json = JsonConvert.SerializeObject(company);

            return json;
        }

    }
}
