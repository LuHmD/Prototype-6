using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    //Changes color when the ball is moved
    public bool isColored = false;

    public void Colored(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;

        FindObjectOfType<GameManager>().CheckComplete();
    }

}
