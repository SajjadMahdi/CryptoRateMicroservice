﻿using System;
using System.Collections.Generic;

namespace CryptoRate.Common.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;
}
