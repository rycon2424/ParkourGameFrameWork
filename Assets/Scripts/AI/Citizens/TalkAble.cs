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

    [Header("Active")]
    public bool activateSomething;
    public GameObject Esomething;
    public float eDelay;
    public bool disableSomething;
    public GameObject Dsomething;
    public float dDelay;

    private bool inTalkRange;
    private DisplayText dt;
    private PlayerInput pi;

    void Start()
    {
        dt = GameObject.FindObjectOfType<DisplayText>();
        pi = GameObject.FindObjectOfType<PlayerInput>();
        if (convo)
        {
            questLogo.SetActive(false);
        }
    }

    void Update()
    {
        if (inTalkRange == true && Input.GetKeyDown(pi.action))
        {
            inTalkRange = false;
            dt.Conversation(charName, textToSay, updateQuest, newQuest);
            questLogo.SetActive(false);
            if (activateSomething)
            {
                Invoke("CheckEnable", eDelay);
            }
            if (disableSomething)
            {
                Invoke("CheckDisable", dDelay);
            }
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
            if (other.gameObject.CompareTag("Player"))
            {
                dt.Thought(textToSay, updateQuest, newQuest);
                if (activateSomething)
                {
                    Invoke("CheckEnable", eDelay);
                }
                if (disableSomething)
                {
                    Invoke("CheckDisable", dDelay);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && convo)
        {
            inTalkRange = false;
            questLogo.SetActive(false);
        }
    }

    void CheckEnable()
    {
        if (activateSomething == true)
        {
            Esomething.SetActive(true);
        }
        taskEnable = true;
        BothTasksDone();
    }

    void CheckDisable()
    {
        if (disableSomething == true)
        {
            Dsomething.SetActive(false);
        }
        taskDisable = true;
        BothTasksDone();
    }

    private bool taskDisable;
    private bool taskEnable;
    void BothTasksDone()
    {
        if (disableSomething && activateSomething)
        {
            if (taskDisable && taskEnable)
            {
                Destroy(this);
            }
        }
        else if (disableSomething)
        {
            if (taskDisable)
            {
                Destroy(this);
            }
        }
        else if (activateSomething)
        {
            if (taskEnable)
            {
                Destroy(this);
            }
        }
    }
}