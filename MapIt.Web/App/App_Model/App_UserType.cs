﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_UserType
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

        public App_UserType(UserType obj)
        {
            Id = obj.Id;
            TitleEN = obj.TitleEN;
            TitleAR = obj.TitleAR;
        }
    }
}