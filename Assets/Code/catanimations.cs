using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catanimations : MonoBehaviour
{

    public Animator m_animator;

    public InputHandler m_inputhandler;

    public Timer m_timer;

    void Update()
    {
        if (m_timer.Night == true)
        {
            m_animator.SetBool("night", true);
        }
        else
        {
            m_animator.SetBool("night", false);
        }
    }
}
