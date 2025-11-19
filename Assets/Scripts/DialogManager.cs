//Ink.Parsed?
//using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;

    [Header("Dialog UI")]

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    private Story currentStory;
    public bool dialogIsPlaying { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There are multiple Dialog Managers. You fucked something up.");

        }
        instance = this;
    }
    void Start()
    {
        dialogIsPlaying = false;
        dialogPanel.SetActive(false);
        //ContinueStory();
    }

    public static DialogManager GetInstance()
    {
        return instance;
    }

    private void LateUpdate()
    {
        if(!dialogIsPlaying)
        {
            return;
        }
        
        if(Input.GetButtonDown("Interact"))
        {
            ContinueStory();
        }
    }
    public void EnterDialogMode(TextAsset inkJSON)
    {
        
        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;
        dialogPanel.SetActive(true);
        //dialogText.text = currentStory.Continue();

    }

    private IEnumerator ExitDialogMode()
    {
        //yield return new WaitForSeconds(0.2f);
        dialogPanel.SetActive(false);
        dialogIsPlaying = false;
        dialogText.text = "If you're reading this, something is wrong with the code.";

        return null;

    }
    
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogText.text = currentStory.Continue();
        }
        else
        {
            dialogIsPlaying = false;
            ExitDialogMode();
        }
    }
}
