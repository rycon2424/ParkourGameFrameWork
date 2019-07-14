using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkAble : MonoBehaviour
{
    public bool convo;
    public string charName;
    public string textToSay;

    [Header("Quest")]
    public GameObject questLogo;
    public bool updateQuest;
    public string newQuest;

    private bool inTalkRange;
    private DisplayText dt;
    private PlayerInput pi;

    void Start()
    {
        dt = GameObject.FindObjectOfType<DisplayText>();
        pi = GameObject.FindObjectOfType<PlayerInput>();
        questLogo.SetActive(false);
    }

    void Update()
    {
        if (inTalkRange == true && Input.GetKeyDown(pi.action))
        {
            inTalkRange = false;
            dt.Conversation(charName, textToSay, updateQuest, newQuest);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (convo)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                inTalkRange = true;
                questLogo.SetActive(true);
            }
        }
        else
        {
            dt.Thought(textToSay, updateQuest, newQuest);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTalkRange = false;
            questLogo.SetActive(false);
        }
    }

}