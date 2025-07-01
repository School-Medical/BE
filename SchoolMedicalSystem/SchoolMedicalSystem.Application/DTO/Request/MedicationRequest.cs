namespace SchoolMedicalSystem.Application.DTO.Response
{
    public class MedicationRequest
    {
        public string MedicineName { get; set; }

        public int Quantity { get; set; }

        public string? Unit { get; set; }

        public string? Type { get; set; }

        public string? Message { get; set; }

        public int GivenDoseId { get; set; }
    }
}