﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharedClasses
{
    public class SharedFollower
    {
        public Guid PK_FollowerID { get; set; }
        public Guid FK_CompanyID { get; set; }
        public Guid FK_UserID { get; set; }
    }
}
