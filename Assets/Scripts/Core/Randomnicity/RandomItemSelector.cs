using System;
using System.Collections.Generic;
using Random = System.Random;

namespace DanPie.Framework.Randomnicity
{
    public class RandomItemSelector<T>
        where T : IRandomSelectableItem
    {
        private readonly Random _random = new Random();   
        private List<T> _selectableItems;

        public RandomItemSelector()
        {
            _selectableItems = new List<T>();
        }

        public RandomItemSelector(List<T> selectableItems)
        {
            SetItemsList(selectableItems);
        }

        public void SetItemsList(List<T> selectableItems)
        {
            if (selectableItems == null)
            {
                throw new ArgumentException($"{nameof(selectableItems)} list can't be null!");
            }

            _selectableItems = new List<T>(selectableItems);
        }

        public T GetRandomItem()
        {
            int sum = 0;
            _selectableItems.ForEach((x) => sum += x.SelectionChance);
            int pointer = (int)Math.Round((double)sum * _random.NextDouble());

            while (pointer > 0)
            {
                foreach (var item in _selectableItems)
                {
                    pointer -= item.SelectionChance;
                    if (pointer <= 0)
                    {
                        return item;
                    }
                } 
            }

            if (_selectableItems.Count == 0)
            {
                throw new Exception($"{nameof(RandomItemSelector<T>)} don't contain any selectable items. " +
                    $"To resolve this use the {nameof(SetItemsList)} method.");
            }
            else
            {
                return _selectableItems[0];
            }
        }
    }
}
