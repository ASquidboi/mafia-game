using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialog Trigger? Devil May Cry reference?

public class DialogTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Dialog Manager")]
    [SerializeField] private DialogManager dialogManager;

    private bool playerInRange;


    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
        {
            visualCue.SetActive(true);
            //check for interaction
            if (Input.GetButtonDown("Interact"))
            {
                //ink magic
                DialogManager.GetInstance().EnterDialogMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
