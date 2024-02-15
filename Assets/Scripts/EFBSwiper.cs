using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EFBSwiper : MonoBehaviour
{
    [SerializeField]
    float 
        heightOfHand = 4, 
        edgeOfScreenMargin = 1;

    bool isVisible, isSwiping;

    // The side of the screen the enemy is on
    float Side
    {
        get => _side;
        set
        {
            _side = value;
            GetComponent<SpriteRenderer>().flipX = _side > 0;
        }
    } float _side;

    void Update()
    {
        // Update distance to player
        GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer = Mathf.MoveTowards(GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer, 0, GlobalSettings.Current.enemyFromBehind.constMoveSpeed * Time.deltaTime);

        float distanceToVisibility = GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer - GlobalSettings.Current.enemyFromBehind.tipPosition;

        // If visible
        if (distanceToVisibility < 0)
        {
            UpdateHeight();

            // If visible for first time
            if (!isVisible)
                OnVisible();

            // If the enemy reached the player
            if(GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer <= 0 && !isSwiping)
                StartCoroutine(SwipeRoutine());
        }

        // Update visaibility
        isVisible = distanceToVisibility < 0;
    }

    void OnVisible()
    {
        Side = -GetPlayerSide();

        var pos = transform.position;
        pos.x = GetTargetX(Side);
        transform.position = pos;
    }

    // Which side of the screen the player is on
    float GetPlayerSide() 
        => Mathf.Sign(GlobalSettings.Current.player.Ref.position.x - Camera.main.transform.position.x);

    float GetTargetX(float side) 
        => Camera.main.transform.position.x + side * (Camera.main.orthographicSize * Camera.main.aspect - edgeOfScreenMargin);

    float GetBottomOfScreen() 
        => Camera.main.transform.position.y - Camera.main.orthographicSize;

    void UpdateHeight()
    {
        float bottomOfScreen = GetBottomOfScreen();

        var pos = transform.position;
        pos.y = Mathf.Lerp(bottomOfScreen + heightOfHand, bottomOfScreen, GlobalSettings.Current.enemyFromBehind.curDistanceFromPlayer / GlobalSettings.Current.enemyFromBehind.tipPosition);
        transform.position = pos;
    }

    IEnumerator SwipeRoutine()
    {
        Timer swipeTimer = new(GlobalSettings.Current.enemyFromBehind.swipeDuration);
        var startPos = transform.position.x;
        var endPos = GetTargetX(-Side);
        Vector3 tempPos;

        isSwiping = true;

            while (!swipeTimer)
            {
                yield return null;
                float swipeNormal = swipeTimer.ClampedNormal();

                tempPos = transform.position;
                tempPos.x = Mathf.Lerp(startPos, endPos, swipeNormal * swipeNormal * swipeNormal);
                transform.position = tempPos;
            } 

        isSwiping = false;

        Side = -Side;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Camera.main.transform.position, new Vector3(Camera.main.orthographicSize * Camera.main.aspect - edgeOfScreenMargin, Camera.main.orthographicSize) * 2);
    }
}
