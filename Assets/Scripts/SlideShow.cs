using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlideShow : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    
    int number = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject thing in objects)
        {
            thing.SetActive(false);
        }
    }

    public void NextScene()
    {
        objects[number].SetActive(true);
        number++;
        if(number == 6)
        {
            objects[3].GetComponent<Image>().color = Color.red;
            objects[3].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }


        if(number >= objects.Count)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
