using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Unity.Interfaces;
using TMPro;


public class BranchChoice : MonoBehaviour
{
    private Branch _branch;
    private ArticyFlowPlayer _flowPlayer;
    [SerializeField] private TextMeshProUGUI _buttonText;
    
    
    public void AssignBranch(ArticyFlowPlayer aFlowPlayer, Branch aBranch)
    {
        _branch = aBranch;
        _flowPlayer = aFlowPlayer;
        IFlowObject target = aBranch.Target;
        _buttonText.text = string.Empty;

        var objectWithMenuText = target as IObjectWithMenuText;
        if (objectWithMenuText != null) _buttonText.text = objectWithMenuText.MenuText;

        if (string.IsNullOrEmpty(_buttonText.text)) _buttonText.text = "Continue >";
    }

    public void OnBranchSelected()
    {
        _flowPlayer.Play(_branch);
    }
}
