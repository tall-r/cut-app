using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cut.Web.Models
{
    public class FileModel
    {
        [Required]
        public IFormFile  File { get; set; }
    }

    public class SheetFileModel
    {
        public SheetFileModel(bool allowRotation, int width, int height) 
        {
            this.AllowRotation = allowRotation;
            this.SheetHeight = height;
            this.SheetWidth = width;
        }

        public SheetFileModel(): this(false, 1250, 2500) {

        }

//        [DefaultValue(2500)]
        [Range(100,5000)]
        public int SheetHeight { get; set; }
        
//        [DefaultValue(1250)]
        [Range(100,5000)]
        public int SheetWidth { get; set; }
        
        public Cut.Data.Size SheetSize { 
            get { return new Cut.Data.Size(this.SheetWidth, this.SheetHeight); }  
        }

        [DefaultValue(false)]
        public bool AllowRotation { get; set; }

        [Required]
        public IFormFile  File { get; set; }
    }
}