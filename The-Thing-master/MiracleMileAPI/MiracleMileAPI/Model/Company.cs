using System;

namespace MiracleMileAPI.Model
{
    public class Company
    {
        public Double Ditt_overskott { get; set; }
        public Double Lon_A_skatt { get; set; }
        public Double Din_arsinkomst { get; set; }
        public Double Din_Genomsnittliga_Manadsinkomst_Brutto { get; set; }
        public Double Beskattningsbar_Forvarvsinkomst { get; set; }
        public Double Kommunal_inkomstskatt { get; set; }
        public Double Statlig_skatt { get; set; }
        public Double Kvar_till_statlig_skatt { get; set; }
        public Double Egenavgifter_tot { get; set; }
        public Double Egenavgifter_innan_nedsattning { get; set; }
        public Double Egenavgifter_nedsattning { get; set; }
        public Double Kvar_till_okning_av_egenavgiften { get; set; }
        public Double Grundavdrag { get; set; }
        public Double Schablonavdrag_som_maste_tas_upp_som_inkomst_nasta_ar { get; set; }
        public Double Slutgiltlig_skatt { get; set; }
        public Double Skatt_kvar_att_betala { get; set; }
        public Double Lon_efter_skatt { get; set; }
        public Double Genomsnittlig_skatt_per_manad { get; set; }
        public Double Effektiv_skatt_pct { get; set; }

        //Aktiebolag

        public Double Arsinkomst { get; set; }
        public Double Manadsinkomst { get; set; }

        public Double Pensionsavgiften { get; set; }
        public Double Jobbskatteavdraget { get; set; }
        public Double Lon_efter_skatt_per_manad { get; set; }

        public Double Gransbelopp_Huvudregeln { get; set; }
        public Double Gransbelopp_forenklingsregeln { get; set; }
        public Double Utdelning_efter_skatt { get; set; }
        public Double Totalt_i_handen { get; set; }
        public Double Pengar_i_foretag_post_max_utd { get; set; }
        public Double Utdelningsutrymme_nex_yr { get; set; }
        public Double Total_lonerel_skatt_att_betala { get; set; }
        public Double Total_lonerel_skatt_att_betala_per_mon { get; set; }
        public Double Summa_skatter_bolag { get; set; }
        public Double Summa_skatter_agare { get; set; }
        public Double Tot_Utdel_rel_skatt_att_betala { get; set; }
        public Double Total_skatt { get; set; }

    }
}
