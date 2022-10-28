namespace RhitmoPark.Domain.Constants
{
    public static class DomainErrorMessagesConstants
    {
        public const string EmptyParkingLot = "O Estacionamento está vazio.";
        public const string FullParkingLot = "O Estacionamento está cheio.";
        public const string ParkingSpaceNotFound = "Não há vagas de estacionamento disponíveis para este veículo.";
        public const string PlateNotFound = "Não foi encontrado nenhum veículo com esta placa.";
        public const string VehicleParked = "Já existe um veículo com esta placa no estacionamento.";
        public const string InvalidPlate = "Insira uma placa nos padrões corretos (Ex.: AAA-0A00 ou AAA-0000).";
    }
}
