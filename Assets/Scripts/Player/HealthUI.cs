using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Material health1;

    public Material health2;

    public Material health3;

    public Color green;

    public Color yellow;

    public Color gray;

    public Color red;
    // Start is called before the first frame update
    private CharacterController healthComponent;
    void Start()
    {
        healthComponent = GetComponent<CharacterController>();
    }

    void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (healthComponent.life >= 10)
        {
            health1.color = green;
            health2.color = green;
            health3.color = green;
        }else if (healthComponent.life >= 9)
        {
            health1.color = yellow;
            health2.color = green;
            health3.color = green;
        }else if (healthComponent.life >= 8)
        {
            health1.color = yellow;
            health2.color = green;
            health3.color = green;
        }else if (healthComponent.life >= 7)
        {
            health1.color = gray;
            health2.color = green;
            health3.color = green;
        }else if (healthComponent.life >= 6)
        {
            health1.color = gray;
            health2.color = yellow;
            health3.color = green;
        }else if (healthComponent.life >= 5)
        {
            health1.color = gray;
            health2.color = yellow;
            health3.color = green;
        }else if (healthComponent.life >= 4)
        {
            health1.color = gray;
            health2.color = gray;
            health3.color = green;
        }else if (healthComponent.life >= 3)
        {
            health1.color = gray;
            health2.color = gray;
            health3.color = yellow;
        }else if (healthComponent.life >= 2)
        {
            health1.color = gray;
            health2.color = gray;
            health3.color = red;
        }else if (healthComponent.life >= 1)
        {
            health1.color = gray;
            health2.color = gray;
            health3.color = red;
        }else if (healthComponent.life >= 0)
        {
            health1.color = gray;
            health2.color = gray;
            health3.color = gray;
        }
    }
    // health 10, green, green, green
    // health 9, yellow, green, green
    // health 8, yellow, green, green
    
    // health 7, gray, green, green
    // health 6, gray, yellow, green
    // helath 5, gray, yellow, green 
    
    // helath 4, gray, gray, green
    // health 3, gray, gray, yellow
    // health 2, gray, gray, red
    // health 1, gray, gray, red blinking
    
    // health 0, is dead. no display
}
