using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Cut.Web.Models
{
    public class FileModel
    {
        [Required]
        public IFormFile  File { get; set; }
    }

    public class SheetFileModel: FileModel
    {
        public SheetFileModel(bool allowRotation) 
        {
            this.AllowRotation = allowRotation;
        }

        public SheetFileModel(): this(false) {

        }

        [Default(false)]
        public bool AllowRotation { get; set; }
    }
}