﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WannaWhat.Client.Pages
{
    public class BrowseBase : ComponentBase
    {
        [Parameter]
        public string Heading { get; set; }
    }
}
