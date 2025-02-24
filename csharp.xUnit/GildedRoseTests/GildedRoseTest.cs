using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    /// <summary>
    /// Rule: All items have a quality value that decreases by 1 every day
    /// </summary>
    [Fact]
    public void DefaultQualityDecrease()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 1 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 0, Quality = 0 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 15 },
        };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(0, Items[1].Quality);
        Assert.Equal(14, Items[2].Quality);
    }

    [Fact]
    public void DefaultSellInDecrease()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Elixir of the Mongoose", SellIn = 1, Quality = 5 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 0, Quality = 6 }

        };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(0, Items[0].SellIn);
        Assert.Equal(-1, Items[1].SellIn);
    }


    /// <summary>
    /// Rule: when an item is past it's sell by date, it's decreases by 2 instead of 1.
    /// </summary>
    [Fact]
    public void DefaultQualityDecreaseTwiceAsFastWhenExpired()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Elixir of the Mongoose", SellIn = -1, Quality = 10 },
            new Item { Name = "Elixir of the Mongoose", SellIn = -1, Quality = 2 },
            new Item { Name = "Elixir of the Mongoose", SellIn = -1, Quality = 1 },
        };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(8, Items[0].Quality);
        Assert.Equal(0, Items[1].Quality);
        Assert.Equal(0, Items[2].Quality);
    }

    /// <summary>
    /// Rule: quality never goes below 0.
    /// </summary>
    [Fact]
    public void ItemQualityNeverNegative()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Elixir of the Mongoose", SellIn = 1, Quality = 0 },
            new Item { Name = "Elixir of the Mongoose", SellIn = -1, Quality = 1 }
        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(0, Items[1].Quality);
    }

    [Fact]
    public void AgedBrieIncreasesWithTime()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 1, Quality = 0 },
            new Item { Name = "Aged Brie", SellIn = -1, Quality = 0 }
        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(1, Items[0].Quality);
        Assert.Equal(2, Items[1].Quality);
    }

    [Fact]
    public void QualityOfItemNeverMoreThan50()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 1, Quality = 50 },
            new Item { Name = "Aged Brie", SellIn = -1, Quality = 49 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 50 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 2, Quality = 49 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 50 },

        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(50, Items[0].Quality);
        Assert.Equal(50, Items[1].Quality);
        Assert.Equal(50, Items[2].Quality);
        Assert.Equal(50, Items[3].Quality);
        Assert.Equal(50, Items[4].Quality);
    }

    [Fact]
    public void QualityOfSulfurusStaysAtEighty()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 15, Quality = 80 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 }

        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(80, Items[0].Quality);
    }

    [Fact]
    public void QualityOfBackstageWHenLessThan10MoreThan5()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 25 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 6, Quality = 25 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 8, Quality = 25 }

        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(27, Items[0].Quality);
        Assert.Equal(27, Items[1].Quality);
        Assert.Equal(27, Items[2].Quality);
    }

    [Fact]
    public void QualityOfBackstageWhenMoreThan10()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 20, Quality = 25 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 30, Quality = 25 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 25 }
        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(26, Items[0].Quality);
        Assert.Equal(26, Items[1].Quality);
        Assert.Equal(26, Items[2].Quality);
    }

    [Fact]
    public void QualityOfBackstageWhen5orLess()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 25 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 3, Quality = 25 }
        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(28, Items[0].Quality);
        Assert.Equal(28, Items[1].Quality);
    }

    [Fact]
    public void QualityOfBackstageWhenPastConcertDate()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 45 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 25 }

        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(0, Items[1].Quality);
    }

    [Fact]
    public void QualityOfBackstageDropToZero()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 25 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 25 }
        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(0, Items[1].Quality);
    }

    [Fact]
    public void QualityOfConjuredGoodsDecreasesByTwoTimes()
    {
        IList<Item> Items = new List<Item>
        {
            new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 30 },
            new Item { Name = "Conjured Flasks of Swiftness", SellIn = -1, Quality = 1 },
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6},
            new Item {Name = "Conjured Mana Cake", SellIn = -1, Quality = 6}


        };

        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();

        Assert.Equal(28, Items[0].Quality);
        Assert.Equal(0, Items[1].Quality);
        Assert.Equal(4, Items[2].Quality);
        Assert.Equal(2, Items[3].Quality);


    }
}