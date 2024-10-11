using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] public Animator animator;

    [SerializeField] Button CloseListButton;
    [SerializeField] Button ListButton;
    public bool isListOpen = false;

    [SerializeField] Button chairListButton;
    [SerializeField] Button bedListButton;
    [SerializeField] Button deskListButton;
    [SerializeField] Button drawerListButton;
    [SerializeField] Button lightListButton;
    [SerializeField] Button mirrorListButton;
    [SerializeField] Button sofaListButton;

    [SerializeField] GameObject chairListObjects;
    [SerializeField] GameObject bedListObjects;
    [SerializeField] GameObject deskListObjects;
    [SerializeField] GameObject drawerListObjects;
    [SerializeField] GameObject lightListObjects;
    [SerializeField] GameObject mirrorListObjects;
    [SerializeField] GameObject sofaListObjects;

    public GameObject curTarget = null;
    public GameObject CurTarget { get { return curTarget; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ListButton.onClick.AddListener(ToggleList);
        CloseListButton.onClick.AddListener(CloseList);

        chairListButton.onClick.AddListener(() => ToggleUIList(chairListObjects));
        bedListButton.onClick.AddListener(() => ToggleUIList(bedListObjects));
        deskListButton.onClick.AddListener(() => ToggleUIList(deskListObjects));
        drawerListButton.onClick.AddListener(() => ToggleUIList(drawerListObjects));
        lightListButton.onClick.AddListener(() => ToggleUIList(lightListObjects));
        mirrorListButton.onClick.AddListener(() => ToggleUIList(mirrorListObjects));
        sofaListButton.onClick.AddListener(() => ToggleUIList(sofaListObjects));
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

    public void CloseList()
    {
        animator.Play("CloseList");
        HideAllFurniture();
        curTarget = null; 
    }

    private void ToggleUIList(GameObject targetList)
    {
        if (curTarget == targetList)
        {
            StartCoroutine(CloseListRoutine(curTarget));
            animator.Play("CloseUIList");
            curTarget = null; 
        }
        else
        {
            if (curTarget != null)
            {
                animator.Play("OpenUIList"); 
                curTarget.SetActive(false);
            }

            targetList.SetActive(true);
            animator.Play("OpenUIList");
            curTarget = targetList;
        }
    }

    private void HideAllFurniture()
    {
        chairListObjects.SetActive(false);
        bedListObjects.SetActive(false);
        deskListObjects.SetActive(false);
        drawerListObjects.SetActive(false);
        lightListObjects.SetActive(false);
        mirrorListObjects.SetActive(false);
        sofaListObjects.SetActive(false);
    }

    IEnumerator CloseListRoutine(GameObject targetList)
    {
        yield return new WaitForSeconds(0.2f);
        targetList.SetActive(false);
    }

    public void Listinterct(bool a)
    {
        ListButton.interactable = a;
    }
}
