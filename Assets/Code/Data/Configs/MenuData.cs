using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu Config", menuName = "Configs/MenuConfig")]
public class MenuData : ScriptableObject
{

    public List<string> Links { get => _links;  }
    
    [SerializeField] private List<string> _links;
}
