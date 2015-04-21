using System;

namespace LINQ_Joins
{
    public class Veiculo
    {
        public string Modelo { get; set }
        
        public string Marca { get; set; }

        public DateTime Ano { get ; set; }

        public double Preco { get; set; }

        //registro da concessionária
        public int ConcessionariaId { get; set; }
    }
}
