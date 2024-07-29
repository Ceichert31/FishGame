using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    private Sequencer startingSequencer;
    private void Awake()
    {
        startingSequencer = GetComponent<Sequencer>();
    }
}
