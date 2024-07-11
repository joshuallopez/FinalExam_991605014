namespace FinalExam_991605014.Models
{
    public class Income

    {

        public int Id { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public bool IsReceived { get; set; }

        public int UserId { get; set; }

    }
}
