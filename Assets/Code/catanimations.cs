using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catanimations : MonoBehaviour
{

    public Animator m_animator;

    public InputHandler m_inputhandler;

    public Timer m_timer;

    
    // Start is called before the first frame update
    void Start()
    {

        m_animator.SetBool("typing", false);//only for RN
    }

    // Update is called once per frame
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
