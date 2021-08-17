namespace CounterStrikeWeb.Test.Pipeline
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FluentAssertions;
    using CounterStrikeWeb.Controllers;
    using CounterStrikeWeb.Services.Players.Models;

    using static Data.Players;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithData(TenPublicPlayers))
                .Calling(c => c.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestPlayersCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                        .WithValueOfType<List<PlayerServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<PlayerServiceModel>>()
                    .Passing(model => model.Should().HaveCount(5)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}
