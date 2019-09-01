using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapIt.Web.App.App_Model
{
    public class App_PropertyVideo
    {
        public Int64 PropertyVideoId { get; set; }

        string _Path;
        public string Path
        {
            set
            {
                _Path = value;
            }
        }

        string _Video;
        public string Video
        {
            get
            {
                if (!string.IsNullOrEmpty(_Path) && !string.IsNullOrEmpty(_Video))
                    return _Path + _Video;

                return string.Empty;
            }
            set { _Video = value; }
        }
    }
}