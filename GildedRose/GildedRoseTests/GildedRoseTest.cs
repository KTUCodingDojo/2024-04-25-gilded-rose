using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [TestCase("foo", 0, -1)]
    [TestCase("foo", 1, 0)]
    [TestCase("foo", 2, 1)]
    [TestCase("foo", 3, 2)]
    [TestCase("foo", -1, -2)]
    public void SellInDecreaseByOne(string name, int sellIn, int expectedSellIn)
    {
        var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = 0 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(expectedSellIn));
    }

    [TestCase("item0", 0, 0)]
    [TestCase("item1", 5, 4)]
    [TestCase("item2", 10, 9)]
    [TestCase("item3", 15, 14)]
    [TestCase("item4", 20, 19)]
    [TestCase("item5", 25, 24)]
    public void StandardQualityDecrease(string name, int quality, int expectedQuality)
    {
        var items = new List<Item> { new Item { Name = name, SellIn = 2, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(expectedQuality));
    }

    [TestCase("item6", 0, 0)]
    [TestCase("item6", 1, 0)]
    [TestCase("item7", 5, 3)]
    [TestCase("item8", 10, 8)]
    [TestCase("item9", 15, 13)]
    [TestCase("item10", 20, 18)]
    [TestCase("item11", 25, 23)]
    public void QualityDecreaseDoubleWhenSellInBelowZero(string name, int quality, int expectedQuality)
    {
        var items = new List<Item> { new Item { Name = name, SellIn = -1, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(expectedQuality));
    }

    [TestCase("Aged Brie", -1, 0, 2)]
    [TestCase("Aged Brie", -1, 2, 4)]
    [TestCase("Aged Brie", -1, 5, 7)]
    [TestCase("Aged Brie", 1, 0, 1)]
    [TestCase("Aged Brie", 1, 2, 3)]
    [TestCase("Aged Brie", 1, 5, 6)]
    [TestCase("Aged Brie", 1, 49, 50)]
    [TestCase("Aged Brie", 1, 50, 50)]
    [TestCase("Aged Brie", -1, 48, 50)]
    [TestCase("Aged Brie", -1, 49, 50)]
    [TestCase("Aged Brie", -1, 50, 50)]
    public void QualityIncreasesAgedBrie(string name, int sellIn, int quality, int expectedQuality)
    {
        var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(expectedQuality));
    }
    //test "Sulfuras, Hand of Ragnaros" quality and sellIn never change
    [TestCase("Sulfuras, Hand of Ragnaros", 1, 80)]
    public void SulfurasQualityAndSellInNeverChange(string name, int sellIn, int quality)
    {
        var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(quality));
        Assert.That(items[0].SellIn, Is.EqualTo(sellIn));
    }
    [TestCase("Backstage passes to a TAFKAL80ETC concert", 15, 20, 21)]
    [TestCase("Backstage passes to a TAFKAL80ETC concert", 10, 20, 22)]
    [TestCase("Backstage passes to a TAFKAL80ETC concert", 5, 20, 23)]
    [TestCase("Backstage passes to a TAFKAL80ETC concert", 0, 20, 0)]
    [TestCase("Backstage passes to a TAFKAL80ETC concert", -1, 20, 0)]
    public void QualityIncreasesBackstagePasses(string name, int sellIn, int quality, int expectedQuality)
    {
        var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(expectedQuality));
    }
    
}
