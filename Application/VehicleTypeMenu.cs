using RhitmoPark.AntiCorruption;
using RhitmoPark.Application.Constants;
using RhitmoPark.Domain.Enums;

namespace RhitmoPark.Application
{
    public static class VehicleTypeMenu
    {
        public static VehicleTypeEnum Menu()
        {
            Console.WriteLine("****************************** MENU DE TIPOS DE VEÍCULOS ******************************");
            Console.WriteLine("Informe o tipo de veículo que deseja utilizar:");
            Console.WriteLine("1 - Motos");
            Console.WriteLine("2 - Carros");
            Console.WriteLine("3 - Vans");

            return VehicleTypeReadLine();
        }

        private static VehicleTypeEnum VehicleTypeReadLine()
        {
            var consoleInput = ConsoleInputs.IntReadLine();

            if (consoleInput == 1)
                return VehicleTypeEnum.Motos;

            else if (consoleInput == 2)
                return VehicleTypeEnum.Carros;

            else if (consoleInput == 3)
                return VehicleTypeEnum.Vans;

            else
            {
                Console.WriteLine(ApplicationErrorMessagesConstants.WrongInput);
                return VehicleTypeReadLine();
            }
        }
    }
}
