using UnityEngine;

public class JournalFeeder : MonoBehaviour
{
    [Header("To add " + "[Hover over the variables for help]")]
    [Tooltip("When inputing a text here make sure to put a space before you start or after you finish a sentance, depenting on the circumstance or order of the reciving of the text")]
    public string text;
    [Tooltip("There is only 1 and 2 here, for the left page put 1 and for the right put 2")]
    public int pageNumber;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponentInChildren<JournalLogic>().AddText(text, pageNumber);
            Destroy(gameObject);
        }
    }
}
