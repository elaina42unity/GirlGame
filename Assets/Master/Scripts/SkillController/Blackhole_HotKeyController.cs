using TMPro;
using UnityEngine;

public class Blackhole_HotKeyController : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private BlackholeSkillController blackHole;

    public void SetupHotKey(KeyCode _myNewHotKey, Transform _myEnemy, BlackholeSkillController _myBlackHole)
    {
        sr = GetComponent<SpriteRenderer>();

        myEnemy = _myEnemy;

        blackHole = _myBlackHole;

        SetupHotKey(_myNewHotKey);

    }

    public void SetupHotKey(KeyCode _myNewHotKey)
    {
        
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myHotKey = _myNewHotKey;


        myText.text = _myNewHotKey.ToString();

        myText.text = "B";
    }

    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            blackHole.AddEnemyToList(myEnemy);

            myText.color = Color.clear;

            sr.color = Color.clear;
        }
    }
}
