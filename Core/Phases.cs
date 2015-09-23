// --------------------------------------------------------------------------
// <copyright file="Phases.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2015.9.23">Created</change>
// </history>

namespace DbDeployer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum Phases
    {
        PreRelease = 0,
        Test, 
        Release, 
        UAT1, 
        UAT2, 
        UAT3, 
        UAT4
    }
}
