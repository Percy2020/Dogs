using MiracleMileAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Calculations
{
    public class Handelsbolag
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
        // Utrakning Handelsbolag och Anstallning. Med schablonbeskattning, for att forenkla deklarationen.
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Calc_Handelsbolag(Double lon_a_skatt, Double redan_inbetald_skatt, Double firma_Utgifter, Double firma_intake, Double andel_av_firma)
        {

            Double sek = 0.25 * (firma_intake - firma_Utgifter); ;
            Double Overskott = andel_av_firma * (firma_intake - firma_Utgifter - sek);

            // Set
            Double prisbasbelopp = 46500;
            Double inkomstbasbelopp = 64400;
            Double kommunalskatt = 0.2988; // Stockholm 0.2998
            Double tak_statlig_skatt = 504400;
            Double tak_varnskatt = 703000;
            Double public_service = 1347;

            // Egenavgifter (Sociala avgifter)
            Double egenavgifter = Calc_Egenavgifter(Overskott);

            Double lon_till_utbetalning = Overskott + lon_a_skatt;
            Double gl = Overskott + lon_a_skatt;
            Double arbetsinkomst = Overskott + lon_a_skatt; // inkluderar ej bidrag

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // __MAIN__
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Double grundavdrag = Calc_Grundavdraget(gl, prisbasbelopp);
            Double kommunal_inkomstskatt = (lon_till_utbetalning - grundavdrag) * kommunalskatt;

            Double statlig_skatt = Math.Max(0.20 * ((lon_till_utbetalning - grundavdrag) - tak_statlig_skatt), 0) + Math.Max(0.05 * ((lon_till_utbetalning - grundavdrag) - tak_varnskatt), 0);

            Double Lone_skatt = kommunal_inkomstskatt + statlig_skatt;
            Double x = Calc_x(arbetsinkomst, prisbasbelopp, kommunalskatt, grundavdrag);
            Double y = Calc_y(gl, inkomstbasbelopp);

            if ((gl - grundavdrag - y) < x)
            {
                x = gl - grundavdrag - y;

            }

            Double Slutgiltlig_skatt = Lone_skatt + egenavgifter - x + public_service;

            Double Lon_efter_skatt = lon_till_utbetalning - Slutgiltlig_skatt + sek;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Out
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            var company = new Company();

            company.Ditt_overskott = Overskott;
            company.Lon_A_skatt = lon_a_skatt;

            company.Din_arsinkomst = Math.Round(gl);
            company.Din_Genomsnittliga_Manadsinkomst_Brutto = Math.Round(gl / 12);

            company.Beskattningsbar_Forvarvsinkomst = Math.Round(gl - grundavdrag);
            company.Kommunal_inkomstskatt = Math.Round(kommunal_inkomstskatt);
            company.Statlig_skatt = Math.Round(statlig_skatt);
            company.Kvar_till_statlig_skatt = -(lon_till_utbetalning - grundavdrag) + tak_statlig_skatt;
            // Inklusive begravningsavgift.

            company.Egenavgifter_tot = Math.Round(egenavgifter);
            company.Egenavgifter_innan_nedsattning = Math.Round(0.2897 * Overskott);
            company.Egenavgifter_nedsattning = Math.Min(0.075 * Overskott, 0.075 * 200000);
            company.Kvar_till_okning_av_egenavgiften = Math.Round(200000 - Overskott);

            company.Grundavdrag = Math.Round(grundavdrag);
            //
            company.Schablonavdrag_som_maste_tas_upp_som_inkomst_nasta_ar = Math.Round(sek);

            company.Slutgiltlig_skatt = Math.Round(Slutgiltlig_skatt);
            //vars.Total skatt att betala                  = Math.Round(gl - Lon_efter_skatt + sek);   

            company.Skatt_kvar_att_betala = Math.Round(Slutgiltlig_skatt - redan_inbetald_skatt);

            company.Lon_efter_skatt = Math.Round((Lon_efter_skatt) / 12);

            company.Genomsnittlig_skatt_per_manad = Math.Round(Slutgiltlig_skatt / 12);
            //vars. Varav egenavifter']                      = round(egenavgifter / 12);                  
            //vars. Varav vanlig skatt']                     = round((Lone_skatt - x)/12);
            //vars. Varav public service']                   = Math.Round(public_service/12);

            company.Effektiv_skatt_pct = Math.Round(100 * (1 - Lon_efter_skatt / (gl + sek)), 2);
            //vars.Effektiv skatt pct = round(100 * Slutgiltlig_skatt / (gl + sek) , 2);



            string json = JsonConvert.SerializeObject(company);

            return json;
        }

    }
}
