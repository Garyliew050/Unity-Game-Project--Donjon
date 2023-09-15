using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Color m_oldColor = Color.white;
    private int greenPads = 0;
    public GameObject Door; // Reference to the wall that should open

    private void OnTriggerEnter(Collider other)
    {
        Renderer render = GetComponent<Renderer>();

        m_oldColor = render.material.color;
        render.material.color = Color.green;
        greenPads++;

        if (greenPads >= 3)
        {
            // All pads are green, trigger wall opening animation here
            if (Door != null)
            {

                Door.GetComponent<Animator>().SetTrigger("Open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        render.material.color = m_oldColor;

        if (greenPads > 0)
        {
            greenPads--;
        }
    }
}