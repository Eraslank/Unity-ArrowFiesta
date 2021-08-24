using System.Collections;
using System.Collections.Generic;
using System;
public struct RandomWeightItem<T>
{
    public double Weight;
    public T Item;
}

public class WeightedRandomBag<T>
{
    private List<RandomWeightItem<T>> entries = new List<RandomWeightItem<T>>();
    private double accumulatedWeight;
    private static Random rand = new Random();

    public void AddEntry(T item, double weight)
    {
        accumulatedWeight += weight;
        entries.Add(new RandomWeightItem<T> { Item = item, Weight = accumulatedWeight });
    }

    public T GetRandom()
    {
        double r = rand.NextDouble() * accumulatedWeight;
        foreach (RandomWeightItem<T> entry in entries)
        {
            if (entry.Weight >= r)
            {
                return entry.Item;
            }
        }
        return default(T); //should only happen when there are no entries
    }
}