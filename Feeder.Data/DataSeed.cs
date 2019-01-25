using Feeder.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feeder.Data
{
    internal class DataSeed
    {
        private readonly FeederContext _context;

        public DataSeed(FeederContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();
        }
    }
}
