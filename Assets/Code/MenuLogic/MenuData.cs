using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Menu Data", menuName = "New Menu Data")]
public class MenuData : ScriptableObject
{
    public string NextScene { get => _nextScene; }
    public List<string> Links { get => _links;  }

    [SerializeField] private string _nextScene;
    [SerializeField] private List<string> _links;
}
