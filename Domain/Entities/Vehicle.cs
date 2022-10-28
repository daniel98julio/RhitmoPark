using RhitmoPark.Domain.Enums;
using System.Text.RegularExpressions;

namespace RhitmoPark.Domain.Entities
{
    public class Vehicle
    {
        public string Plate { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public VehicleTypeEnum VehicleType { get; private set; }
        public VehicleTypeEnum ParkedInVehicleTypeSpace { get; private set; }
        public DateTime EntryTime { get; private set; }
        public DateTime? ExitTime { get; private set; }
        public TimeSpan? StayTime { get; private set; }

        public Vehicle(string placa, string marca, string modelo, VehicleTypeEnum vehicleType)
        {
            Plate = placa;
            Brand = marca;
            Model = modelo;
            VehicleType = vehicleType;
            EntryTime = DateTime.Now;
        }

        public void SetParkedInVehicleTypeSpace(VehicleTypeEnum vehicleType)
        {
            ParkedInVehicleTypeSpace = vehicleType;
        }

        public void SetExitTime()
        {
            StayTime = DateTime.Now - EntryTime;
            ExitTime = DateTime.Now;
        }

        public bool ValidatePlate(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate) || plate.Length != 8)
                return false;
            
            var regularPlate = new Regex("[A-Z]{3}-[0-9]{4}");
            var mercosulPlate = new Regex("[A-Z]{3}-[0-9]{1}[A-Z]{1}[0-9]{2}");

            if (!regularPlate.IsMatch(plate.ToUpper()) && !mercosulPlate.IsMatch(plate.ToUpper()))
                return false;

            return true;
        }

        public override string ToString()
        {
            TimeSpan stayTime = new();
            if (!StayTime.HasValue)
                stayTime = DateTime.Now - EntryTime;

            return String.Format("\nPlaca: {0} \nMarca: {1} \nModelo: {2} \nTipo de Veículo: {3} \nEstacionado em Vaga do Tipo: {4} " +
                "\nHora de Entrada: {5} \nHora de Saída: {6} \nPermanência: {7}",
                Plate, Brand, Model, VehicleType, ParkedInVehicleTypeSpace, EntryTime.ToString(), ExitTime.ToString(), StayTime?.ToString("hh\\:mm\\:ss") ?? stayTime.ToString("hh\\:mm\\:ss"));
        }
    }
} 
