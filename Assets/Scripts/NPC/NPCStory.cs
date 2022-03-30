using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStory : Collidable
{
    public Dialogue dialogue;
    public Dialogue firstDialogue;

    public bool _dialogueFirst;

    public bool typeTextNow;

    DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = DialogueManager.instance;
        if (_dialogueFirst)
        {
            dialogueManager.StartDialogue(firstDialogue);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && typeTextNow)
        {
            dialogueManager.StartDialogue(dialogue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            typeTextNow = false;
        }
    }

    public override void Interacable()
    {
        base.Interacable();
        typeTextNow = true;
    }
}
