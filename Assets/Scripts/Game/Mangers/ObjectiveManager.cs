using System;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType
{
    Photo,
    Trigger,
}

public static class ObjectiveManager
{
    private static Dictionary<string, Dictionary<string, int>> objectives = new();

    public static Dictionary<string, Dictionary<string, int>> Objectives => objectives;

    public static event Action<string, string, int> OnObjectiveAdded;
    public static event Action<string, int> OnObjectiveUpdated;
    public static event Action<string> OnObjectiveDeleted;

    public static void CreateObjective(string name, string description, int amount)
    {
        if (!objectives.ContainsKey(name))
        {
            Dictionary<string, int> properties = new()
            {
              {"Amount", 0},
              {"Target", amount}
            };

            objectives.Add(name, properties);

            OnObjectiveAdded?.Invoke(name, description, amount);
        }
    }

    public static void UpdateObjective(string name, int amount)
    {
        if (objectives.ContainsKey(name))
        {
            objectives[name]["Amount"] += amount;

            OnObjectiveUpdated?.Invoke(name, objectives[name]["Amount"]);
        }
    }

    public static void DeleteObjective(string name)
    {
        if (objectives.ContainsKey(name))
        {
            objectives.Remove(name);

            OnObjectiveDeleted?.Invoke(name);
        }
    }
}
