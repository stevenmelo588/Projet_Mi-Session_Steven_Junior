// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor.Build.Pipeline;
using UnityEngine;

// namespace SurviveTheRust.Assets.Scripts.ScriptableObjects.AlertMessages
// {
    [CreateAssetMenu(fileName = "AlertMessagesSO", menuName = "ScriptableObjects/AlertMessagesSO", order = 0)]
    public class AlertMessagesSO : ScriptableObject
    {
        [SerializeField] private string alertType;
        [SerializeField] private string alertMessageName;

        [TextAreaAttribute(10, 20)] 
        [SerializeField] private string alertMessageText; 
                
        public string AlertType { get => alertType; private set => value = alertType; }
        public string AlertMessageName { get => alertMessageName; private set => value = alertMessageName; }
        public string AlertMessageText { get => alertMessageText; private set => value = alertMessageText; }
    }
// }
