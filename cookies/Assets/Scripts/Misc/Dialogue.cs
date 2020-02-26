using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    public float typingSpeed;

    public int index;
    public bool _canAdvance;

    private void Start()
    {
        StartCoroutine(Type());
        _canAdvance = false;
    }

    private void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            _canAdvance = true;
        }

        if (Input.GetButtonDown("Fire1") && _canAdvance == true)
        {
            NextSentence();
        }

    }

    private IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        _canAdvance = false;

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            _canAdvance = false;
        }
    }
}
