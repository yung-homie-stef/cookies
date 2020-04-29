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
    private bool hasEvent = false;
    private GameObject _speaker;
    private Speech_Sound_Control _speakersSpeechControl;

    private void Start()
    {
    }

    public void BeginDialogue(string[] phrases, GameObject speaker, bool eventExists)
    {
        if (!hasSentence)
        {
            sentences = phrases;
            _speaker = speaker;

            dialogueIndex = 0;
            StartCoroutine(Type());
            _canAdvance = false;
            hasSentence = true;
            hasEvent = eventExists;
            _speakersSpeechControl = _speaker.GetComponent<Speech_Sound_Control>();
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
                CutSpeakerOff();
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
        _speakersSpeechControl.Speak();
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
        
        if (hasEvent)
        {
            _speaker.GetComponent<Interactable>().ConversationEndEvent();
        }
    }

    private void CutSpeakerOff()
    {
        textDisplay.text = "";
        _canAdvance = false;
        dialogueIndex = 0;

        hasSentence = false;
        sentences = null; // empty array
    }

}
