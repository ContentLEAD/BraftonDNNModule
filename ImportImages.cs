﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security;
using System.Net;

using Brafton.Modules.Globals;

// The AllowPartiallyTrustedCallersAttribute requires the assembly to be signed with a strong name key.
// This attribute is necessary since the control is called by either an intranet or Internet
// Web page that should be running under restricted permissions.
//[assembly: AllowPartiallyTrustedCallers]

namespace ImportImages
{
    public class ImageDownload
    {
        public string urlDirectory;
        /// <summary>
        /// Function to download Image from website
        /// </summary>
        /// <param name="_URL">URL address to download image</param>
        /// <returns>Image</returns>
        /// 

        public ImageDownload()
        {

        }

        public void saveImage(string photoURL, string imageName, int contentId)
        {
            string contentstring = contentId.ToString();
            //Used for importing the images
            string physicalDir = HttpRuntime.BinDirectory;
            //Get current directory for style sheets and images
            string appPath = HttpRuntime.AppDomainAppVirtualPath == "/" ? appPath = "" : appPath = HttpRuntime.AppDomainAppVirtualPath;
            int? portal = MyGlobals.currentPortal;
            int? blog = MyGlobals.CurrentBlog;
            physicalDir = physicalDir.Substring(0, physicalDir.Length - 4) + "Portals/" + portal + "/blog/files/" + blog + "/"+contentstring;
            urlDirectory = appPath + "Portals/" + portal + "/blog/files/" + blog + "/" + contentstring;
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(photoURL);
            Bitmap workingBitmap = new Bitmap(stream);
            stream.Flush();
            stream.Close();

            Image _Image = null;
            _Image = workingBitmap;

            // check for valid image
            if (_Image != null)
            {
                Directory.CreateDirectory(physicalDir);
                MyGlobals.LogMessage("file should exists at : " + physicalDir + "\\" + imageName + ".jpg");
                // lets save image to disk                            
                _Image.Save(@"" + physicalDir + "\\" + imageName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
