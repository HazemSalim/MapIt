using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MapIt.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                  "~/Scripts/jquery-1.11.1.min.js",
                  "~/Scripts/bootstrap.min.js"));
        }
    }
}