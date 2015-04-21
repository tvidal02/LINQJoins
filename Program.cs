using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Joins
{
    class Program
    {
        static void Main(string[] args) 
        {
            Concessionaria csBmw = new Concessionaria() { ConcessionariaId = 1, Localizacao = "Maceió" };
            Concessionaria csBmw2 = new Concessionaria() { ConcessionariaId = 2, Localizacao = "Rio de Janeiro" };
            Concessionaria csToyota = new Concessionaria() { ConcessionariaId = 3, Localizacao = "São Paulo" };
            Concessionaria csFiat = new Concessionaria() { ConcessionariaId = 4, Localizacao = "Curitiba" };
            Concessionaria csCitroen = new Concessionaria() { ConcessionariaId = 5, Localizacao = "Salvador" };

            var lstVeiculos = new List<Veiculo>() 
            {
                new Veiculo(){ Marca = "Fiat", Modelo = "Palio Fire", Ano = DateTime.Today.AddYears(-3), Preco = 17900.0 },
                new Veiculo(){ Marca = "BMW", Modelo = "528i M Sport", Ano = DateTime.Today.AddYears(-1), Preco = 270750.0, ConcessionariaId = 2 },
                new Veiculo(){ Marca = "BMW", Modelo = "Série 2 Coupé", Ano = DateTime.Today.AddYears(1), Preco = 190000.0, ConcessionariaId  = 1 },
                new Veiculo(){ Marca = "Toyota", Modelo = "Etios", Ano = DateTime.Today, Preco = 39950.0, ConcessionariaId = 3 },
                new Veiculo(){ Marca = "Fiat", Modelo = "Siena", Ano = DateTime.Today.AddYears(-2), Preco = 29990.0 },
                new Veiculo(){ Marca = "Citroen", Modelo = "Citroen C3", Ano = DateTime.Today.AddYears(1), Preco = 41990.0 },
                new Veiculo(){ Marca = "BMW", Modelo = "Série 3 Gran Turismo", Ano = DateTime.Today.AddYears(-6), Preco = 310000.0, ConcessionariaId = 4 },
                new Veiculo(){ Marca = "Fiat", Modelo = "Mille", Ano = DateTime.Today.AddYears(-9), Preco = 17990.0, ConcessionariaId = 5 },
                new Veiculo(){ Marca = "Toyota", Modelo = "Hilux", Ano = DateTime.Today.AddYears(-11), Preco = 57900.0, ConcessionariaId = 3 },
                new Veiculo(){ Marca = "Citroen", Modelo = "C4 Picasso", Ano = DateTime.Today.AddYears(-1), Preco = 63990.0 },
                new Veiculo(){ Marca = "Toyota", Modelo = "Novo Corolla", Ano = DateTime.Today.AddYears(1), Preco = 46900.0, ConcessionariaId = 1 }
            };

            var lstConcessionarias = new List<Concessionaria>() { csBmw, csBmw2, csToyota, csFiat, csCitroen };

            //seleciona os veículos agrupando-os por marca e preço em ordem decrescente
            var queryVeiculos = from v in lstVeiculos
                                group v by new { v.Marca, v.Modelo, Preço = v.Preco } into vGrouped
                                orderby vGrouped.Key.Marca, vGrouped.Key.Preço descending
                                let Marca = vGrouped.Key.Marca
                                let Modelo = vGrouped.Key.Modelo
                                let Preço = vGrouped.Key.Preço
                                select new
                                {
                                    Marca,
                                    Modelo,
                                    Preço
                                };

            foreach (var veiculo in queryVeiculos)
            {
                Console.WriteLine("Marca: {0} - Modelo: {1} - Preço: R${2},00", veiculo.Marca, veiculo.Modelo, veiculo.Preço);
            }

            //selecionar os veículos que possuem registro na concessionária (inner join)
            //cada carro pode ter zero ou um registro na concessionária
            var queryVeiculosEmConcessionarias = from veiculo in lstVeiculos
                                                 join veiculosConcessionaria in lstConcessionarias
                                                 on veiculo.ConcessionariaId equals veiculosConcessionaria.ConcessionariaId
                                                 select veiculo;

            foreach (var veiculo in queryVeiculosEmConcessionarias)
            {
                Console.WriteLine(veiculo.Modelo);   
            }

            //left join: selecionar os veículos que possuem registro ou não na concessionária
            var queryVeiculosConcessionarias = from veiculo in lstVeiculos
                                               join concessionaria in lstConcessionarias
                                               on veiculo.ConcessionariaId equals concessionaria.ConcessionariaId into veiculosConcessionaria
                                               from carros in veiculosConcessionaria.DefaultIfEmpty()
                                               select new 
                                               {
                                                   Modelo = carros == null ? string.Concat(veiculo.Modelo, " não possui registro na concessionária")
                                                        : veiculo.Modelo
                                               };

            foreach (var veiculo in queryVeiculosConcessionarias)
            {
                Console.WriteLine(veiculo.Modelo);
            }
          
            Console.ReadLine();
        }
    }
}
