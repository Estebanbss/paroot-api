using System;
using System.Collections.Generic;

namespace paroot_api.Data.Models;

public partial class Url
{
    public int Id { get; set; }

    public string OriginalUrl { get; set; } = null!;

    public string ShortUrl { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? Clicks { get; set; }

    public DateTime? LastClickedAt { get; set; }

    public string? LastClickedCountry { get; set; }
}
