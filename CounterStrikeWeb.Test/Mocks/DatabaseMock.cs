namespace CounterStrikeWeb.Test.Mocks
{
    using CounterStrikeWeb.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class DatabaseMock
    {
        public static CounterStrikeDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<CounterStrikeDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new CounterStrikeDbContext(dbContextOptions);
            }
        }
    }
}
