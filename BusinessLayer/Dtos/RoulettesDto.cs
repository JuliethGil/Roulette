using System;

namespace BusinessLayer.Dtos
{
    public class RoulettesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? WinningNumber { get; set; }
        public bool IsRoulettOpen { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
