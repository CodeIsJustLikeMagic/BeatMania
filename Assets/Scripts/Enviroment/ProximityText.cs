using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityText : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float showUpDistance = 10;
    [SerializeField]
    private GameObject textObject;
    // Start is called before the first frame update

    public void SetText(string n_text)
    {
        GetComponentInChildren<TextMesh>().text = n_text;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist < showUpDistance)
        {
            textObject.SetActive(true);
        }
        else
        {
            textObject.SetActive(false);
        }
    }
}
