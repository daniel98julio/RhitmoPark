using RhitmoPark.Domain.Constants;
using RhitmoPark.Domain.Enums;

namespace RhitmoPark.Domain.Entities
{
    public class Parking
    {
        public bool IsOpen { get; private set; }
        public int TotalMotorCycleParkingSpaces { get; private set; }
        public int TotalCarParkingSpaces { get; private set; }
        public int TotalVanParkingSpaces { get; private set; }
        public List<Vehicle> Vehicles { get; private set; }

        public Parking(int motorCycleParkingSpaceNumber, int carParkingSpaceNumber, int vanParkingSpaceNumber)
        {
            IsOpen = true;
            TotalMotorCycleParkingSpaces = motorCycleParkingSpaceNumber;
            TotalCarParkingSpaces = carParkingSpaceNumber;
            TotalVanParkingSpaces = vanParkingSpaceNumber;
            Vehicles = new List<Vehicle>();
        }

        public void SetIsOpenFalse()
        {
            IsOpen = false;
        }

        public int GetTotalParkingSpaces(bool onlyFreeParkingSpaces = false)
        {
            var totalParkingSpaces = TotalMotorCycleParkingSpaces + TotalCarParkingSpaces + TotalVanParkingSpaces;

            if (onlyFreeParkingSpaces)
            {
                var totalFreeParkingSpaces = GetTotalFreeParkingSpacesByVehicleType(VehicleTypeEnum.Motos);
                totalFreeParkingSpaces += GetTotalFreeParkingSpacesByVehicleType(VehicleTypeEnum.Carros);
                totalFreeParkingSpaces += GetTotalFreeParkingSpacesByVehicleType(VehicleTypeEnum.Vans);

                return totalFreeParkingSpaces;
            }

            return totalParkingSpaces;
        }

        public int GetTotalFreeParkingSpacesByVehicleType(VehicleTypeEnum vehicleType)
        {
            var fullParkingSpaceByVehicleType = Vehicles.Where(x => x.ParkedInVehicleTypeSpace == vehicleType).Count();

            if (vehicleType.Equals(VehicleTypeEnum.Motos))
                return TotalMotorCycleParkingSpaces - fullParkingSpaceByVehicleType;

            else if (vehicleType.Equals(VehicleTypeEnum.Carros))
            {
                fullParkingSpaceByVehicleType = Vehicles.Where(x => x.VehicleType != VehicleTypeEnum.Vans).Where(x => x.ParkedInVehicleTypeSpace == vehicleType).Count();
                var vanInCarParkingSpace = Vehicles.Where(x => x.VehicleType == VehicleTypeEnum.Vans).Where(x => x.ParkedInVehicleTypeSpace == vehicleType).Count() * 3;
                return TotalCarParkingSpaces - fullParkingSpaceByVehicleType - vanInCarParkingSpace;
            }

            else
                return TotalVanParkingSpaces - fullParkingSpaceByVehicleType;
        }

        public List<string> GetAllParkedVehicle()
        {
            if (!Vehicles.Any())
                throw new Exception(DomainErrorMessagesConstants.EmptyParkingLot);

            List<string> vehiclesList = new();

            foreach (var vehicle in Vehicles)
                vehiclesList.Add(GetParkedVehicleByPlate(vehicle.Plate));

            return vehiclesList;
        }

        public string GetParkedVehicleByPlate(string plate)
        {
            var vehicleDetail = Vehicles.FirstOrDefault(x => x.Plate.Equals(plate));

            if (vehicleDetail == null)
                throw new Exception(DomainErrorMessagesConstants.PlateNotFound);

            return vehicleDetail.ToString();
        }

        public string AddVehicle(Vehicle vehicle)
        {
            if (!vehicle.ValidatePlate(vehicle.Plate))
                throw new Exception(DomainErrorMessagesConstants.InvalidPlate);

            if (Vehicles.Exists(x => x.Plate == vehicle.Plate))
                throw new Exception(DomainErrorMessagesConstants.VehicleParked);

            if (GetTotalParkingSpaces() <= 0)
                throw new Exception(DomainErrorMessagesConstants.FullParkingLot);

            vehicle.SetParkedInVehicleTypeSpace(GetParkSpaceTypeToPark(vehicle.VehicleType));

            Vehicles.Add(vehicle);
            return vehicle.ToString();
        }

        public string RemoveVehicle(string plate)
        {
            if (!Vehicles.Any())
                throw new Exception(DomainErrorMessagesConstants.EmptyParkingLot);

            var vehicle = Vehicles.FirstOrDefault(x => x.Plate.Equals(plate)); 
            if (vehicle == null)
                throw new Exception(DomainErrorMessagesConstants.PlateNotFound);
            
            vehicle.SetExitTime();

            Vehicles.Remove(vehicle);
            return vehicle.ToString();
        }

        private VehicleTypeEnum GetParkSpaceTypeToPark(VehicleTypeEnum vehicleType)
        {
            var motorCycleFreeParkingSpaces = GetTotalFreeParkingSpacesByVehicleType(VehicleTypeEnum.Motos);
            var carFreeParkingSpaces = GetTotalFreeParkingSpacesByVehicleType(VehicleTypeEnum.Carros);
            var vanFreeParkingSpaces = GetTotalFreeParkingSpacesByVehicleType(VehicleTypeEnum.Vans);

            if (motorCycleFreeParkingSpaces > 0 && vehicleType.Equals(VehicleTypeEnum.Motos))
                return VehicleTypeEnum.Motos;

            else if (carFreeParkingSpaces > 0 && (vehicleType.Equals(VehicleTypeEnum.Motos) || vehicleType.Equals(VehicleTypeEnum.Carros)))
                return VehicleTypeEnum.Carros;

            else if (vanFreeParkingSpaces <= 0 && carFreeParkingSpaces >= 3 && vehicleType.Equals(VehicleTypeEnum.Vans))
                return VehicleTypeEnum.Carros;

            else if (vanFreeParkingSpaces > 0)
                return VehicleTypeEnum.Vans;

            else
                throw new Exception(DomainErrorMessagesConstants.ParkingSpaceNotFound);
        }
    }
}
