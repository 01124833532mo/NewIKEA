using Link.Dev.IKEA.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entites
{
	public class Email : ModelBase
	{
        public string Subject { get; set; }

        public string Body { get; set; }

        public string Reciepints { get; set; }

    }
}
