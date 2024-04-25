using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;

    private Dictionary<string, IRule> _rules;

    private IRule _defaultRule = new NormalItemRule();

    public GildedRose(IList<Item> items)
    {
        _rules = new Dictionary<string, IRule>
        {
            { "Aged Brie", new AgedBrieRule() },
            { "Backstage passes to a TAFKAL80ETC concert", new BackstagePassesRule() },
            { "Sulfuras, Hand of Ragnaros", new SulfurasRule() },
            { "Conjured Mana Cake",  new ConjuredRule()}
        };
        _items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            _rules.GetValueOrDefault(item.Name, _defaultRule).Apply(item);
        }
        /*
        for (var i = 0; i < _items.Count; i++)
        {
            if (_items[i].Name == "Sulfuras, Hand of Ragnaros")
                continue;

            var isNormalItem = _items[i].Name != "Aged Brie" && _items[i].Name != "Backstage passes to a TAFKAL80ETC concert";
            
            if(isNormalItem)
            {
                if (_items[i].Quality > 0)
                {
                    _items[i].Quality = _items[i].Quality - 1;
                }
            }

            else
            {
                if (_items[i].Quality < 50)
                {
                    _items[i].Quality = _items[i].Quality + 1;

                    if (_items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (_items[i].SellIn < 11)
                        {
                            if (_items[i].Quality < 50)
                            {
                                _items[i].Quality = _items[i].Quality + 1;
                            }
                        }

                        if (_items[i].SellIn < 6)
                        {
                            if (_items[i].Quality < 50)
                            {
                                _items[i].Quality = _items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            _items[i].SellIn = _items[i].SellIn - 1;

            if (_items[i].SellIn < 0)
            {
                if (isNormalItem)
                {
                    if (_items[i].Quality > 0)
                    {
                        _items[i].Quality = _items[i].Quality - 1;
                    }
                }
                else if (_items[i].Name != "Aged Brie")
                {
                    _items[i].Quality = 0;
                }
                else
                {
                    if (_items[i].Quality < 50)
                    {
                        _items[i].Quality = _items[i].Quality + 1;
                    }
                }
            }*/
        }
    

    interface IRule
    {
        void Apply(Item item);
    }

    internal class NormalItemRule : IRule
    {
        public virtual int diff => 1;
        public void Apply(Item item)
        {
            int localDiff = diff;
            if(item.SellIn < 0)
            {
                localDiff *= 2;
            }

            item.Quality -= localDiff;
            if(item.Quality < 0)
            {
                item.Quality = 0;
            }
            item.SellIn--;
        }
    }
    internal class AgedBrieRule : IRule
    {
        public void Apply(Item item)
        {
            int diff = 1;
            if (item.SellIn <= 0)
            {
                diff = 2;
            }

            item.Quality += diff;
            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
            item.SellIn--;
        }
    }
    internal class BackstagePassesRule : IRule
    {
        public void Apply(Item item)
        {
            int diff = 1;
            if (item.SellIn <= 10)
            {
                diff = 2;
            }
            if(item.SellIn <= 5)
            {
                diff = 3;
            }

            item.Quality += diff;
            if (item.Quality > 50)
            {
                item.Quality = 50;
            }
            if(item.SellIn <= 0)
            {
                item.Quality = 0;
            }
            item.SellIn--;
        }
    }
    internal class SulfurasRule : IRule
    {
        public void Apply(Item item)
        {
            return;
        }
    }
    internal class ConjuredRule : NormalItemRule
    {
        public override int diff => 2;
    }
}

