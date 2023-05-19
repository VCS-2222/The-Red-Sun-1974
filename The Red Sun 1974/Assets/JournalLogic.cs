using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class JournalLogic : MonoBehaviour
{
    [Header("Content Table")]
    public TextMeshProUGUI content1, content2;

    [Header("Needed Components")]
    private PlayerBindings binds;
    public Canvas journalFace;

    private bool isInJournal;

    private void Awake()
    {
        binds = new PlayerBindings();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        binds.Enable();
    }

    private void OnDisable()
    {
        binds.Disable();
    }

    private void Update()
    {
        binds.UI.OpenJournal.performed += t => OpenAndCloseJournal();
    }

    void OpenAndCloseJournal()
    {
        isInJournal = !isInJournal;

        if (isInJournal)
        {
            journalFace.enabled = true;
        }
        else
        {
            journalFace.enabled = false;
        }

    }

    public void AddText(string textToAdd, int contentBox)
    {
        if(contentBox == 1)
        {
            content1.text += textToAdd;
        }
        else if(contentBox == 2)
        {
            content2.text += textToAdd;
        }
    }
}
