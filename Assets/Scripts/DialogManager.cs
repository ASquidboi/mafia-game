//Ink.Parsed?
//using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;

    [Header("Dialog UI")]

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    private Story currentStory;
    public bool dialogIsPlaying { get; private set; }

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;
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
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
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
            DisplayChoices();
        }
        else
        {
            dialogIsPlaying = false;
            ExitDialogMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than the UI can support");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
