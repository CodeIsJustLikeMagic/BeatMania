using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background instance;
    public SpriteRenderer[] renderers;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        SetSprites("yellow");
    }
    public void SetSprites(string color)
    {
        foreach(SpriteRenderer rend in renderers)
        {
            string str = "yellow" + rend.gameObject.name;
            Sprite sprt = Resources.Load<Sprite>("BackgroundTextures/yellow" + rend.gameObject.name);
            rend.sprite = sprt;
        }
    }
}
