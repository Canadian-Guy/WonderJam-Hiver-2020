using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpHandler : MonoBehaviour
{
    public Animator anim;
    public TMP_Text tmpText;

    private void  Start()
    {
        anim.SetTrigger("Fading");
    }


    public void SetText(int p_value)
    {
        if (tmpText) tmpText.text = "// +" + p_value;
    }
}
