using System;
using System.Collections.Generic;

namespace WsRest_UpWay.Models.EntityFramework;

public partial class Estrealise
{
    public int Idvelo { get; set; }

    public int Idinspection { get; set; }

    public int Idreparation { get; set; }

    public string? Dateinspection { get; set; }

    public string? Commentaireinspection { get; set; }

    public string? Historiqueinspection { get; set; }

    public virtual Rapportinspection IdinspectionNavigation { get; set; } = null!;

    public virtual Reparationvelo IdreparationNavigation { get; set; } = null!;

    public virtual Velo IdveloNavigation { get; set; } = null!;
}
