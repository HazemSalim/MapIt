using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapIt.Web.App.App_Model
{
    public class App_PropertyFeature
    {
        public Int64 PropertyFeatureId { get; set; }

        public int FeatureId { get; set; }
        public string FeatureEN { get; set; }
        public string FeatureAR { get; set; }
    }
}