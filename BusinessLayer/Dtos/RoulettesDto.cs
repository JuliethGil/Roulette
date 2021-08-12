using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Dtos
{
    public class RoulettesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? WinningNumber { get; set; }
        public bool Status { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
