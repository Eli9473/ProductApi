using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.App.Contract.IServices
{
    public interface IUserContext
    {
        public string UserId { get; set; }
    }
}
