using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    private Dialogue dialogue;
    [SerializeField] private Dialogue.DialogueLine[] lines;

    private void Awake()
    {
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.SetLines(lines);
            Destroy(this.gameObject);
        }
    }
}
