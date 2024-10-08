using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Button CloseListButton;

    [SerializeField] Button ListButton;
    private bool isListOpen = false;

    [SerializeField] Button chairListButton;
    private bool isChairListOpen = false; 

    private void Start()
    {
        ListButton.onClick.AddListener(ToggleList);
        CloseListButton.onClick.AddListener(CloseList);

        chairListButton.onClick.AddListener(ToggleChairList); 
    }

    private void ToggleList()
    {
        isListOpen = !isListOpen;

        if (isListOpen)
        {
            animator.Play("OpenList");
        }
        else
        {
            animator.Play("CloseList");
        }
    }

    private void CloseList()
    {
        animator.Play("CloseList");
    }

    private void ToggleChairList()
    {
        isChairListOpen = !isChairListOpen; 

        if (isChairListOpen)
        {
            animator.Play("ChairOpenList");
        }
        else
        {
            animator.Play("ChairCloseList"); 
        }
    }
}
