using System.Collections;
using UnityEngine;
using TMPro;

public class PopUpHandler : MonoBehaviour
{
    public Animator anim;
    public TMP_Text tmpText;

    private void Awake()
    {
        StartCoroutine(DelayedSepuku());
    }

    private void  Start()
    {
        anim.SetTrigger("Fading");
    }


    public void SetText(int p_value)
    {
        if (tmpText) tmpText.text = "// +" + p_value;
    }

    private IEnumerator DelayedSepuku()
    {
        yield return new WaitForSeconds(5);

        Destroy(this);
    }
}
