using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectiveController : MonoBehaviour
{
    [SerializeField] private Transform objectiveList;
    [SerializeField] private TextMeshProUGUI objectiveTemplate;

    private Dictionary<string, TextMeshProUGUI> objectives = new();

    private IEnumerator DestroySlot(TextMeshProUGUI slot)
    {
        yield return new WaitForSeconds(2f);

        Destroy(slot.gameObject);
    }

    private void OnObjectiveAdded(string name, string description, int amount)
    {
        if (!objectives.ContainsKey(name))
        {
            TextMeshProUGUI objectiveSlot = Instantiate(objectiveTemplate, objectiveList);
            TextMeshProUGUI targetAmount = objectiveSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            objectiveSlot.text = description.ToString();
            targetAmount.text = "0/" + amount;

            objectives.Add(name, objectiveSlot);
        }
    }

    private void OnObjectiveUpdated(string name, int amount)
    {
        if (objectives.ContainsKey(name))
        {
            TextMeshProUGUI targetAmount = objectives[name].transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            targetAmount.text = amount + "/" + ObjectiveManager.Objectives[name]["Target"].ToString();
        }
    }

    private void OnObjectiveDeleted(string name)
    {
        if (objectives.ContainsKey(name))
        {
            TextMeshProUGUI objectiveSlot = objectives[name];

            objectiveSlot.text = "<s>" + objectiveSlot.text + "</s>";
            objectives.Remove(name);

            StartCoroutine(DestroySlot(objectiveSlot));
        }
    }

    private void OnEnable()
    {
        ObjectiveManager.OnObjectiveAdded += OnObjectiveAdded;
        ObjectiveManager.OnObjectiveUpdated += OnObjectiveUpdated;
        ObjectiveManager.OnObjectiveDeleted += OnObjectiveDeleted;
    }

    private void OnDisable()
    {
        ObjectiveManager.OnObjectiveAdded -= OnObjectiveAdded;
        ObjectiveManager.OnObjectiveUpdated -= OnObjectiveUpdated;
        ObjectiveManager.OnObjectiveDeleted -= OnObjectiveDeleted;
    }
}
