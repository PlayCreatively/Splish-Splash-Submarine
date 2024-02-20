using UnityEngine;

public class EFBLurker : MonoBehaviour
{
    [SerializeField]
    float heightOfHand = 4;

    bool isVisible;

    EnemyFromBehindSettings Settings => GlobalSettings.Current.enemyFromBehind;

    void Awake()
    {
        Settings.curDistanceFromPlayer = Settings.maxDistanceFromPlayer;
        Settings.curMoveSpeedOverPlayer = 0;
    }

    void Update()
    {
        // Update distance to player
        float VerticalDelta = Settings.curMoveSpeedOverPlayer != 0
            ? Settings.curMoveSpeedOverPlayer
            : -.35f;

        Settings.curDistanceFromPlayer -= VerticalDelta * Time.deltaTime;
        Settings.curDistanceFromPlayer = Mathf.Clamp(Settings.curDistanceFromPlayer, 0, Settings.maxDistanceFromPlayer);


        UpdatePosition();
    }
    float GetBottomOfScreen() 
        => Camera.main.transform.position.y - Camera.main.orthographicSize;

    void UpdatePosition()
    {
        float bottomOfScreen = GetBottomOfScreen();

        // Normal for how close the enemy is to the player from 0 to 1
        float normalFromPlayer = 1 - (Settings.curDistanceFromPlayer / Settings.maxDistanceFromPlayer);

        transform.position = new Vector3(GlobalSettings.Current.player.Ref.position.x, bottomOfScreen + heightOfHand * normalFromPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 bottom = new (transform.position.x, GetBottomOfScreen());
        Gizmos.DrawLine(bottom, bottom + Vector3.up * heightOfHand);
    }

}
