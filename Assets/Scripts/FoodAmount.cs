using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodAmount : MonoBehaviour
{
    public Player player;
    public Text foodText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foodText.text = player.foodAmount.ToString();
    }
}
