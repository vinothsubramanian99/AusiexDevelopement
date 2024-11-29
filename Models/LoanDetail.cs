using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiProject3.Models;

public partial class LoanDetail
{
    [Key]
    public int Lid { get; set; }

    public string? LoanNumber { get; set; }

    public string? UserName { get; set; }
}
