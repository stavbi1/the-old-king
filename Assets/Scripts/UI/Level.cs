using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private Text levelText;
    private ParticleSystem levelUpParticles;

    private void Start()
    {
        levelText = GetComponent<Text>();
        levelUpParticles = GetComponent<ParticleSystem>();
    }

    public void SetLevel(int level)
    {
        levelText.text = "Lvl. " + level.ToString();
        levelUpParticles.Play();
    }
}
