using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences = null;
    public float typingSpeed;
    public int dialogueIndex = 0;
    public bool _canAdvance;
    public float speakingDistance = 2.0f;

    private bool hasSentence = false;
    private GameObject _speaker;

    private void Start()
    {
    }

    public void BeginDialogue(string[] phrases, GameObject speaker)
    {
        if (!hasSentence)
        {
            sentences = phrases;
            _speaker = speaker;

            dialogueIndex = 0;
            StartCoroutine(Type());
            _canAdvance = false;
            hasSentence = true;
        }

    }

    private void Update()
    {
        if (hasSentence)
        {
            if (textDisplay.text == sentences[dialogueIndex])
            {
                _canAdvance = true;
            }

            if (Input.GetButtonDown("Fire1") && _canAdvance == true)
            {
                NextSentence();
            }
        }

        if (_speaker)
        {
            if (Vector3.Distance(transform.position, _speaker.transform.position) > speakingDistance)
            {
                EndConversation(); // if player moves too far end conversation as if speaker has said all their lines
                                   // returning to said speaker will restart said conversation
            }
        }

    }

    private IEnumerator Type()
    {
        foreach(char letter in sentences[dialogueIndex].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        _canAdvance = false;

        if (dialogueIndex < sentences.Length - 1)
        {
            dialogueIndex++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            EndConversation();
        }
    }

    public void EndConversation()
    {
        textDisplay.text = "";
        _canAdvance = false;
        dialogueIndex = 0;

        hasSentence = false;
        sentences = null; // empty array
    }

}
