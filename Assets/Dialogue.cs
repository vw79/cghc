using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public DialogueLine[] dialogueLines;
    public float textSpeed;
    public List<SpeakerImage> speakerImages;

    private Image dialogueBoxImage;


    private int index;

    [System.Serializable]
    public struct DialogueLine
    {
        public string line;
        public Speaker speaker;
    }

    [System.Serializable]
    public class SpeakerImage
    {
        public Speaker speaker;
        public Image image;
    }

    public enum Speaker
    {
        Player,
        NPC1,
        NPC2,
        // Add more as needed
    }

    private void Start()
    {
        dialogueBoxImage = GetComponent<Image>();
        textComponent.text = string.Empty;
        HideAll();
    }

    void StartDialogue()
    {
        GameManager.Instance.DisableControl();
        dialogueBoxImage.enabled = true;
        index = 0;
        StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isCinematic)
        {
            if (index >= 0 && index < dialogueLines.Length)
            {
                if (textComponent.text == dialogueLines[index].line)
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = dialogueLines[index].line;
                    ShowSpeakerImage(dialogueLines[index].speaker);
                }
            }
        }
    }

    IEnumerator TypeLine()
    {
        ShowSpeakerImage(dialogueLines[index].speaker);

        foreach (char c in dialogueLines[index].line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            HideAll();
            GameManager.Instance.EnableControl();
        }
    }

    public void SetLines(DialogueLine[] newLines)
    {
        StopAllCoroutines();
        dialogueLines = newLines;
        textComponent.text = string.Empty;
        StartCoroutine(DelayedStartDialogue());
    }

    IEnumerator DelayedStartDialogue()
    {
        yield return null;
        StartDialogue();
    }

    private void HideAllSpeakerImages()
    {
        foreach (var speakerImage in speakerImages)
        {
            speakerImage.image.gameObject.SetActive(false);
        }
    }

    private void ShowSpeakerImage(Speaker speaker)
    {
        HideAllSpeakerImages();
        foreach (var speakerImage in speakerImages)
        {
            if (speakerImage.speaker == speaker)
            {
                speakerImage.image.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void HideAll()
    {
        dialogueBoxImage.enabled = false;
        dialogueLines = new DialogueLine[0];
        textComponent.text = string.Empty; 
        HideAllSpeakerImages(); 
    }
}
