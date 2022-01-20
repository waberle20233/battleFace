using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleFaceDataAccess.Models
{
    public static class QuoteRepo
    {
        public static Quote SaveQuote(ApplicationDBContext db, Quote quote)
        {
            db.Add(quote);
            db.SaveChanges();
            return quote;
        }
    }
}
