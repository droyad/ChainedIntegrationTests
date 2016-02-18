using System.Data.Entity;
using System.Threading;

namespace Api.Domain
{
    public class DB : DbContext
    {
        public IDbSet<Widget> Widgets { get; set; }
        public IDbSet<Thingimabob> Thingimabobs { get; set; }
    }
}