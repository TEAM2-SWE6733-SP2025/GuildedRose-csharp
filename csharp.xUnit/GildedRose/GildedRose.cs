using System;
using System.Collections.Generic;

    
namespace GildedRoseKata;

public class GildedRose
{
    public IList<Item> Items;

    public const string AgedBrie = "Aged Brie";
    public const string BackstagePass = "Backstage passes to a TAFKAL80ETC concert";
    public const string Sulfuras = "Sulfuras, Hand of Ragnaros";

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            var name = item.Name;
            switch (item.Name)
            {
                case Sulfuras:
                    continue;
                case AgedBrie:
                    UpdateAgedBrie(item);
                    break;
                case BackstagePass:
                    UpdateBackstagePass(item);
                    break;
                case string s when s.StartsWith("Conjured"):
                    UpdateConjuredItem(item);
                    break;
                default:
                    UpdateStandardItem(item);
                    break;
            }
        }
    }

    private void UpdateConjuredItem(Item item)
    {
        if (item.SellIn < 0 && item.Quality > 1)
        {
            item.Quality -= 2;
        }

        if (item.Quality > 1)
        {
            item.Quality -= 2;
        }
        else
        {
            item.Quality -= 1;
        }
        item.SellIn -= 1;

        
    }

    private void UpdateStandardItem(Item item)
    {
        if (item.Quality > 0)
        {
            item.Quality -= 1;
        }
        item.SellIn -= 1;

        if (item.SellIn < 0 && item.Quality > 0)
        {
            item.Quality -= 1;
        }
    }

    private void UpdateBackstagePass(Item item)
    {
        if (item.Quality < 50)
        {
            item.Quality += 1;

            if (item.SellIn < 11 && item.Quality < 50)
            {
                item.Quality += 1;
            }

            if (item.SellIn < 6 && item.Quality <50)
            {
                item.Quality += 1;
            }
        }

        item.SellIn -= 1;

        if (item.SellIn < 0)
        {
            item.Quality = 0;
        }
    }

    private void UpdateAgedBrie(Item item)
    {

        if (item.Quality < 50)
        {
            item.Quality += 1;
        }

        item.SellIn -= 1;

        if (item.SellIn < 0 && item.Quality < 50)
        {
            item.Quality += 1;
        }
    }
}