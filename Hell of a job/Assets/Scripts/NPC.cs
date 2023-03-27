using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // ссылка на менеджер диалогов
    DialogueManager dialogueManager;

    // имя, отображаемое в диалоговом окне
    [SerializeField] string displayName;
    // список диалогов
    [SerializeField] List<Dialogue> dialogues = new List<Dialogue>();
    // индекс текущего диалога
    int currentDialogue = 0;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // запуск диалога с данным NPC
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(displayName, dialogues[currentDialogue]);

        if (currentDialogue < dialogues.Count - 1)
            currentDialogue++;
    }
}

