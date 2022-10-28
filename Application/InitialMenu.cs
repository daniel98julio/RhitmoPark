using RhitmoPark.AntiCorruption;
using RhitmoPark.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhitmoPark.Application
{
    public static class InitialMenu
    {
        public static void Menu()
        {
            Console.WriteLine("******************** BEM VINDO AO RHITMOPARK ********************");
            Console.WriteLine("Para iniciar o sistema, por favor informe alguns dados:");

            Console.WriteLine("");
            Console.WriteLine("Quantas vagas para Motocicletas que o estacionamento possui:");
            var motorCycleParkingSpaces = ConsoleInputs.IntReadLine();

            Console.WriteLine("");
            Console.WriteLine("Quantas vagas para Carros que o estacionamento possui:");
            var carParkingSpaces = ConsoleInputs.IntReadLine();

            Console.WriteLine("");
            Console.WriteLine("Quantas vagas para Vans que o estacionamento possui:");
            var vanParkingSpaces = ConsoleInputs.IntReadLine();
            Parking parking = new(motorCycleParkingSpaces, carParkingSpaces, vanParkingSpaces);

            Console.WriteLine("");
            Console.WriteLine("O estacionamento possui {0} vagas, no total!", parking.GetTotalParkingSpaces());

            Console.WriteLine("");
            Console.WriteLine("Sistema iniciado, aperte qualquer tecla para ser redirecionado ao Menu Principal!");
            Console.ReadLine();

            MainMenu.Menu(parking);
        }
    }
}
