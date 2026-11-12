using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private TMP_Text tipText;
    void Start()
    {
        tipText.maxVisibleCharacters =0;
    }
    IEnumerator ShowFullTip()
    {
        while(tipText.maxVisibleCharacters< tipText.text.Length)
        {
            yield return new WaitForSecondsRealtime(0.03f);
            tipText.maxVisibleCharacters++;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }
        StartCoroutine(ShowFullTip());
    }
}
