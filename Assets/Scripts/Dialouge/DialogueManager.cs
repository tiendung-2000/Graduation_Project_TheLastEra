using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else instance = this;
    }

    public void StartDialogue(Dialogue dialogue) {
        sentences = new Queue<string>();
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0)
        {
            dialoguePanel.SetActive(false);
            return;
        }
        dialoguePanel.SetActive(true);
        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    
    IEnumerator TypeSentence(string _sentence) {
        dialogueText.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        
    }

}
