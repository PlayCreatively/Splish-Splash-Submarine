using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerBooster : MonoBehaviour
{
    public bool Boosting => boosting;
    bool boosting = false;

    const float speedChangeDuration = .25f;
    const float boostMultiplier = 4f;
    float DefaultSpeed => GlobalSettings.Current.player.verticalSpeed;


    float elapsedSinceCheck = 0;
    private void Update()
    {
        elapsedSinceCheck += Time.deltaTime;
        if (boosting || elapsedSinceCheck > .6f)
        {
            Check();
            elapsedSinceCheck = 0;
        }
    }

    void Check()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float? closestEnemy;
        if (enemies.Length == 0)
            closestEnemy = null;
        else 
            closestEnemy = enemies.Select(e => e.transform.position.y).Min();

        bool EnemiesClose = !(closestEnemy == null || closestEnemy > 9f);

        if (boosting && EnemiesClose)
        {
            boosting = false;
            StopAllCoroutines();
            StartCoroutine(SpeedChangeRoutine(DefaultSpeed));
        }
        else if(!boosting && !EnemiesClose)
        {
            boosting = true;
            StopAllCoroutines();
            StartCoroutine(SpeedChangeRoutine(DefaultSpeed * boostMultiplier));
        }
    }

    IEnumerator SpeedChangeRoutine(float newSpeed)
    {
        float oldSpeed = GameState.Get.PlayerVerticalSpeedUnscaled;
        Timer speedChangeTimer = new (speedChangeDuration);

        while(!speedChangeTimer)
        {
            yield return null;
            GameState.Get.PlayerVerticalSpeed = Mathf.Lerp(oldSpeed, newSpeed, speedChangeTimer);
        }
    }
}
