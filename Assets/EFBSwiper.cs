using System.Collections;
using System.Collections.Generic;
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

    // Which side of the screen the player is on
    float GetPlayerSide()
    {
        return Mathf.Sign(GlobalSettings.Current.player.Ref.position.x - Camera.main.transform.position.x);
    }

    void OnVisible()
    {
        Side = -GetPlayerSide();

        var pos = transform.position;
        pos.x = GetTargetX(Side);
        transform.position = pos;
    }

    float GetTargetX(float side)
    {
        return Camera.main.transform.position.x + side * (Camera.main.orthographicSize * Camera.main.aspect - edgeOfScreenMargin);
    }

    float GetBottomOfScreen()
    {
        return Camera.main.transform.position.y - Camera.main.orthographicSize;
    }

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
        
        isSwiping = true;

            while(!swipeTimer)
            {
                var pos = transform.position;
                pos.x = Mathf.Lerp(startPos, endPos, swipeTimer);
                transform.position = pos;
                yield return null;
            }

        isSwiping = false;

        Side = -Side;
    }
}
