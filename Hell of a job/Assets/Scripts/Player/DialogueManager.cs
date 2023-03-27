using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // ссылка на аниматор диалогового окна
    [SerializeField] Animator animator;
    // ссылка на текст, отображающий имя NPC
    [SerializeField] Text nameText;
    // ссылка на текст, отображающий реплики диалога
    [SerializeField] Text dialogueText;

    // использовать ли эффект печатания символов
    [SerializeField] bool useTyping = true;
    // время появления очередного символа в секундах
    [SerializeField] float timeBetweenTypedCharacters = 0.05f;

    // ссылка на скрипт движения
    Person movementScript;
    // ссылка на менеджер взаимодействия
    InteractionManager interactionManager;

    // очередь предложений текущего диалога
    Queue<string> sentences = new Queue<string>();

    void Start()
    {
        movementScript = GetComponent<Person>();
        interactionManager = GetComponent<InteractionManager>();
    }

    void Update()
    {
        if (animator.GetBool("isOpen") && Input.GetButtonDown("Interact"))
            DisplayNextSentence();
    }

    // запуск диалога
    public void StartDialogue(string npcName, Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        movementScript.enabled = false;
        interactionManager.enabled = false;

        nameText.text = npcName;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    // отображение очередной реплики в диалоговом окне
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();

        if (useTyping)
            StartCoroutine(TypeSentence(sentences.Dequeue()));
        else
            dialogueText.text = sentences.Dequeue();
    }

    // корутина для эффекта печатания символов
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        
        foreach (char ch in sentence.ToCharArray())
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(timeBetweenTypedCharacters);
            // не используем "yield return null", потому что при таком подходе
            // скорость анимации печатания будет зависеть от fps
        }
    }

    // остановка диалога
    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        movementScript.enabled = true;
        interactionManager.enabled = true;
    }
}
