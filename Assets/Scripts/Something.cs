using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Something : MonoBehaviour
{
    [System.Serializable]
    public class DecisionChoice
    {
    }
    [System.Serializable]
        public class DecisionScenario
        {
        public string audioSound;
        public DecisionChoice choiceOne;
        public DecisionChoice choiceTwo;
    }
    [SerializeField] private List<DecisionScenario> scenarios;

}
