using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public float delay = 0.05f;
    private string currentText;

    [Header("Quest")]
    public Text quest;
    public AudioSource aso;

    [Header("Conversations")]
    public GameObject talkBar;
    public Text nameBar;
    public Text textConv;

    [Header("Thought")]
    public GameObject thoughtBar;
    public Text textThought;

    public void Conversation(string name ,string text, bool updateQuest, string newQuest)
    {
        nameBar.text = name;
        StartCoroutine(ShowText(text, textConv, talkBar, false, updateQuest, newQuest));
    }

    public void Thought(string text, bool updateQuest, string newQuest)
    {
        StartCoroutine(ShowText(text, textThought, thoughtBar, true, updateQuest, newQuest));
    }

    public void UpdateQuest(string new_quest_text)
    {
        quest.text = new_quest_text;
    }

    IEnumerator ShowText(string fullText, Text t, GameObject g, bool canWalk, bool updateQuest, string newQuest)
    {
        if (canWalk == false)
        {
            GameObject.FindObjectOfType<PlayerController>().enabled = false;
            GameObject.FindObjectOfType<PlayerController>().Anim.SetFloat("Speed", 0f);
            GameObject.FindObjectOfType<PlayerController>().Anim.Play("Idle");
        }
        g.SetActive(true);
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            t.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(3f);
        g.SetActive(false);
        if (updateQuest == true)
        {
            quest.text = newQuest;
            aso.Play();
        }
        GameObject.FindObjectOfType<PlayerController>().enabled = true;
    }
}