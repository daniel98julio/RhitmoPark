using RhitmoPark.AntiCorruption;
using RhitmoPark.Application.Constants;
using RhitmoPark.Domain.Constants;
using RhitmoPark.Domain.Entities;

namespace RhitmoPark.Application
{
    public static class ResearchMenu
    {
        public static void Menu(Parking parking)
        {
            int menuOption = 0;
            while (!menuOption.Equals(6))
            {
                Console.Clear();

                Console.WriteLine("******************** MENU DE CONSULTAS ********************");
                Console.WriteLine("1 - Consultar todos os veículos no estacionamento");
                Console.WriteLine("2 - Consultar veículo por placa");
                Console.WriteLine("3 - Consultar o total de vagas no estacionamento");
                Console.WriteLine("4 - Consultar o total de vagas vazias no estacionamento");
                Console.WriteLine("5 - Consultar o total de vagas vazias por tipo de Veículo");
                Console.WriteLine("6 - Retornar ao menu anterior");

                menuOption = ConsoleInputs.IntReadLine();
                switch (menuOption)
                {
                    case 1:

                        var vehicleList = parking.GetAllParkedVehicle();

                        foreach (var vehicle in vehicleList)
                            Console.WriteLine(vehicle);

                        WaitNextCommand();
                        break;

                    case 2:
                        Console.WriteLine("Informe a placa que deseja buscar");
                        var plate = ConsoleInputs.ReadLine();

                        Console.WriteLine(parking.GetParkedVehicleByPlate(plate));
                        WaitNextCommand();
                        break;

                    case 3:
                        Console.WriteLine("O estacionamento possui {0} vagas, no total.", parking.GetTotalParkingSpaces());
                        WaitNextCommand();
                        break;

                    case 4:
                        Console.WriteLine("O estacionamento possui {0} vagas vazias, no momento.", parking.GetTotalParkingSpaces(true));
                        WaitNextCommand();
                        break;

                    case 5:
                        var VehicleType = VehicleTypeMenu.Menu();

                        Console.WriteLine("O estacionamento possui {0} vagas de {1} vazias, no momento.", parking.GetTotalFreeParkingSpacesByVehicleType(VehicleType), VehicleType);
                        WaitNextCommand();
                        break;

                    default:
                        Console.WriteLine(ApplicationErrorMessagesConstants.InvalidOption);
                        break;
                }
            }
        }

        private static void WaitNextCommand()
        {
            Console.WriteLine("");
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadLine();
        }
    }
}
