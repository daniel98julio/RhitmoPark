using RhitmoPark.AntiCorruption;
using RhitmoPark.Application.Constants;
using RhitmoPark.Domain.Constants;
using RhitmoPark.Domain.Entities;
using RhitmoPark.Domain.Enums;

namespace RhitmoPark.Application
{
    public static class MainMenu
    {
        public static void Menu(Parking parking)
        {
            try
            {
                while (parking.IsOpen)
                {
                    Console.Clear();

                    Console.WriteLine("*********************  RHITMOPARK **********************");
                    Console.WriteLine("******************** MENU PRINCIPAL ********************");
                    Console.WriteLine("1 - Registrar Entrada De Veículo");
                    Console.WriteLine("2 - Registrar Saída de Veículo");
                    Console.WriteLine("3 - Menu de Consultas");
                    Console.WriteLine("4 - Encerrar o Sistema");

                    var menuOption = ConsoleInputs.IntReadLine();
                    switch (menuOption)
                    {
                        case 1:
                            Console.WriteLine("Informe a Placa do Veículo (Ex.: AAA-0A00 ou AAA-0000)");
                            var plate = ConsoleInputs.ReadLine();
                            Console.WriteLine("");

                            Console.WriteLine("Informe a Marca do Veículo");
                            var brand = ConsoleInputs.ReadLine();
                            Console.WriteLine("");

                            Console.WriteLine("Informe o Modelo do Veículo");
                            var model = ConsoleInputs.ReadLine();
                            Console.WriteLine("");

                            Console.WriteLine("Informe o Tipo do Veículo de acordo com o abaixo:");
                            Console.WriteLine("");
                            var vehicleType = VehicleTypeMenu.Menu();

                            Vehicle vehicle = new(plate, brand, model, vehicleType);

                            Console.WriteLine(parking.AddVehicle(vehicle));

                            WaitNextCommand();
                            break;

                        case 2:
                            Console.WriteLine("Informe a Placa do Veículo que deseja tirar do estacionamento:");
                            Console.WriteLine(parking.RemoveVehicle(ConsoleInputs.ReadLine()));

                            WaitNextCommand();
                            break;

                        case 3:
                            ResearchMenu.Menu(parking);
                            break;

                        case 4:
                            Console.WriteLine("Sistema Encerrado!");
                            parking.SetIsOpenFalse();
                            break;

                        default:
                            Console.WriteLine(ApplicationErrorMessagesConstants.InvalidOption);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                WaitNextCommand();
                Menu(parking);
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
