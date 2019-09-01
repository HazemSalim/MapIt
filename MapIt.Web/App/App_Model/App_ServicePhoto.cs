﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_ServicePhoto
    {
        public Int64 ServicePhotoId { get; set; }

        string _Path;
        public string Path
        {
            set
            {
                _Path = value;
            }
        }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Path) && !string.IsNullOrEmpty(_Photo))
                    return _Path + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public App_ServicePhoto()
        {

        }

        public App_ServicePhoto(ServicePhoto servicePhoto)
        {
            ServicePhotoId = servicePhoto.Id;
            Photo = servicePhoto.Photo;
        }
    }
}