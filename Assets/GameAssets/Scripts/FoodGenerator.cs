using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FoodGenerator : NetworkBehaviour {

    [SerializeField] GameObject foodPrefab;
    [SerializeField] int minFoodPerRound;
    [SerializeField] int maxFoodPerRound;
    [SerializeField] float timePerRound;

    [SerializeField] Rect levelLimits;

    public override void OnStartServer()
    {
        StartCoroutine(CreateFoodCoroutine() );
    }

    IEnumerator CreateFoodCoroutine()
    {
        while (true)
        {
            int foods = Random.Range(minFoodPerRound, maxFoodPerRound);
            for(int i=0; i<foods; ++i)
            {
                GameObject newFood = Instantiate(foodPrefab);
                float randomX = Random.Range(levelLimits.xMin, levelLimits.xMax);
                float randomY = Random.Range(levelLimits.yMin, levelLimits.yMax);
                newFood.transform.position = new Vector3(randomX, randomY, 0);

                NetworkServer.Spawn(newFood);
            }
            yield return new WaitForSeconds(timePerRound);

        }
    }

}
