using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAndEnableTrigger : MonoBehaviour
{
    public enum DisableEnable { disableAndEnable, disableOnly, enableOnly , disable1XAndEnable2X, disable2XAndEnable1X , disable2X, enable2X, disable2XAndEnable2X }
    [Header("Basic")]
    public DisableEnable deType;
    public GameObject toDisable;
    public GameObject toEnable;

    [Header("Extra")]
    public GameObject toDisableX2;
    public GameObject toEnableX2;

    [Header("Delay")]
    public bool hasDelay;
    public float delayDuration;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Horse"))
        {
            if (hasDelay)
            {
                Invoke("Delay", delayDuration);
                return;
            }
            switch (deType)
            {
                case DisableEnable.disableAndEnable:
                    toDisable.SetActive(false);
                    toEnable.SetActive(true);
                    break;
                case DisableEnable.disableOnly:
                    toDisable.SetActive(false);
                    break;
                case DisableEnable.enableOnly:
                    toEnable.SetActive(true);
                    break;
                case DisableEnable.disable1XAndEnable2X:
                    toDisable.SetActive(false);
                    toEnable.SetActive(true);
                    toEnableX2.SetActive(true);
                    break;
                case DisableEnable.disable2XAndEnable1X:
                    toDisable.SetActive(false);
                    toDisableX2.SetActive(false);
                    toEnable.SetActive(true);
                    break;
                case DisableEnable.disable2X:
                    toDisable.SetActive(false);
                    toDisableX2.SetActive(false);
                    break;
                case DisableEnable.enable2X:
                    toEnable.SetActive(false);
                    toEnableX2.SetActive(false);
                    break;
                case DisableEnable.disable2XAndEnable2X:
                    toDisable.SetActive(false);
                    toDisableX2.SetActive(false);
                    toEnable.SetActive(true);
                    toEnableX2.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        Destroy(this);
    }

    void Delay()
    {
        switch (deType)
        {
            case DisableEnable.disableAndEnable:
                toDisable.SetActive(false);
                toEnable.SetActive(true);
                break;
            case DisableEnable.disableOnly:
                toDisable.SetActive(false);
                break;
            case DisableEnable.enableOnly:
                toEnable.SetActive(true);
                break;
            case DisableEnable.disable1XAndEnable2X:
                toDisable.SetActive(false);
                toEnable.SetActive(true);
                toEnableX2.SetActive(true);
                break;
            case DisableEnable.disable2XAndEnable1X:
                toDisable.SetActive(false);
                toDisableX2.SetActive(false);
                toEnable.SetActive(true);
                break;
            case DisableEnable.disable2X:
                toDisable.SetActive(false);
                toDisableX2.SetActive(false);
                break;
            case DisableEnable.enable2X:
                toEnable.SetActive(false);
                toEnableX2.SetActive(false);
                break;
            case DisableEnable.disable2XAndEnable2X:
                toDisable.SetActive(false);
                toDisableX2.SetActive(false);
                toEnable.SetActive(true);
                toEnableX2.SetActive(true);
                break;
            default:
                break;
        }
        Destroy(this);
    }
}