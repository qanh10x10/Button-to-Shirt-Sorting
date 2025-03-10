using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Level Info")]
    public GameMode gameMode;
    public LevelModelSO level;
    public GameObject trGamelevel;
    public int timeRemaining = 100;
    public int buttonRemain = 10;

    [Header("Prefabs Ref")]
    [SerializeField] ButtonCtrl buttonPrefab;
    [SerializeField] ShirtSlot shirtSlotPrefab;

    [Header("ReadOnly")]
    [SerializeField] private SpriteRenderer spawnArea_Button;
    [SerializeField] private SpriteRenderer spawnArea_Slot;
    [SerializeField] List<ButtonCtrl> buttonList;
    [SerializeField] List<ShirtSlot> shirtSlotList;

    private float spacing = 1f;
    List<Vector3> spawns_ButtonPos = new List<Vector3>();
    List<Vector3> spawns_SlotPos = new List<Vector3>();
    public void Init()
    {
        this.gameMode = Module.GameMode;
        switch (gameMode)
        {
            case GameMode.Level:
                LoadLevel();
                break;
            case GameMode.Endless:
                LoadEndless();
                break;
            default:
                break;
        }
        if (ctTimeRemain != null)
            StopCoroutine(ctTimeRemain);
        ctTimeRemain = StartCoroutine(IeTimerCountdown());
    }
    public void LoadLevel()
    {
        Module.isLose = false;
        Module.isWin = false;
        level = Resources.Load<LevelModelSO>(string.Format("Levels/Lv{0}", Module.cr_Level));
        buttonRemain = level.buttonCount;
        UIManager.Instance.m_UIGamePlay.UpdateSlotLeft(buttonRemain);
        List<ButtonInfo> selectColors = ButtonModelSO.Instance.buttons.OrderBy(b => Random.Range(0, ButtonModelSO.Instance.buttons.Count)).Take(buttonRemain).ToList();

        if (!level.isRandom)
        {
            for (int i = 0; i < level.buttonCount; i++)
            {
                ButtonInfo _info = selectColors[i];
                ShirtSlot slot = SimplePool.Spawn(shirtSlotPrefab, level.slotsPos[i], Quaternion.identity);
                slot.SetSlotInfo(_info);
                shirtSlotList.Add(slot);

                ButtonCtrl button = SimplePool.Spawn(buttonPrefab, level.buttonsPos[i], Quaternion.identity);
                button.SetButtonInfo(_info);
                button.transform.SetParent(spawnArea_Button.transform);
                buttonList.Add(button);
            }
        }
        else
        {
            AutoGenLevel();
        }

    }
    public void AutoGenLevel()
    {
        buttonRemain = level.buttonCount;
        for (int i = 0; i < buttonRemain; i++)
        {
            ButtonInfo info = level.GetRandomColor();
            //Buttons Spawn
            Vector3 randomPos_Btn;
            int attempt = 0;
            do
            {
                randomPos_Btn = GetRandomPositionInSprite(ObjectType.Button);
                attempt++;
            } while (IsOverlapping(randomPos_Btn, ObjectType.Button) && attempt < 50);

            spawns_ButtonPos.Add(randomPos_Btn);
            ButtonCtrl button = SimplePool.Spawn(buttonPrefab, randomPos_Btn, Quaternion.identity);
            button.SetButtonInfo(info);
            button.transform.parent = spawnArea_Button.transform;
            buttonList.Add(button);


            //Slots Spawn
            Vector3 randomPos_Slot;
            int attempts = 0;
            do
            {
                randomPos_Slot = GetRandomPositionInSprite(ObjectType.Slot);
                attempts++;
            } while (IsOverlapping(randomPos_Slot, ObjectType.Slot) && attempts < 50);

            spawns_SlotPos.Add(randomPos_Slot);
            ShirtSlot slot = SimplePool.Spawn(shirtSlotPrefab, randomPos_Slot, Quaternion.identity);
            slot.SetSlotInfo(info);
            slot.transform.parent = spawnArea_Slot.transform;
            shirtSlotList.Add(slot);
        }
    }
    Vector3 GetRandomPositionInSprite(ObjectType type = ObjectType.Button)
    {
        Bounds bounds = new Bounds();
        float buttonRadius = 0;

        switch (type)
        {
            case ObjectType.Slot:
                bounds = spawnArea_Slot.bounds;
                buttonRadius = shirtSlotPrefab.imgSlot.bounds.extents.x;
                break;
            case ObjectType.Button:
                bounds = spawnArea_Button.bounds;
                buttonRadius = buttonPrefab.imgBtn.bounds.extents.x;
                break;
        }


        float x = Random.Range(bounds.min.x + buttonRadius, bounds.max.x - buttonRadius);
        float y = Random.Range(bounds.min.y + buttonRadius, bounds.max.y - buttonRadius);

        return new Vector3(x, y, -1);
    }
    bool IsOverlapping(Vector3 position, ObjectType type = ObjectType.Button)
    {
        switch (type)
        {
            case ObjectType.Slot:
                foreach (Vector3 existingPos in spawns_SlotPos)
                {
                    if (Vector3.Distance(existingPos, position) < spacing)
                    {
                        return true;
                    }
                }
                break;
            case ObjectType.Button:
                foreach (Vector3 existingPos in spawns_ButtonPos)
                {
                    if (Vector3.Distance(existingPos, position) < spacing)
                    {
                        return true;
                    }
                }
                break;
        }


        return false;
    }
    public void LoadEndless()
    {
        Module.isLose = false;
        buttonRemain = Random.Range(5, 10) + Module.cr_Level / 5;
        UIManager.Instance.m_UIGamePlay.UpdateSlotLeft(buttonRemain);
        List<ButtonInfo> selectColors = ButtonModelSO.Instance.buttons.OrderBy(b => Random.Range(0, ButtonModelSO.Instance.buttons.Count)).Take(buttonRemain).ToList();
        for (int i = 0; i < buttonRemain; i++)
        {
            ButtonInfo _info = selectColors[i];
            Vector3 randomPos_Btn;
            int attempt = 0;
            do
            {
                randomPos_Btn = GetRandomPositionInSprite(ObjectType.Button);
                attempt++;
            } while (IsOverlapping(randomPos_Btn, ObjectType.Button) && attempt < 50);

            spawns_ButtonPos.Add(randomPos_Btn);
            ButtonCtrl button = SimplePool.Spawn(buttonPrefab, randomPos_Btn, Quaternion.identity);
            button.SetButtonInfo(_info);
            button.transform.parent = spawnArea_Button.transform;
            buttonList.Add(button);


            //Slots Spawn
            Vector3 randomPos_Slot;
            int attempts = 0;
            do
            {
                randomPos_Slot = GetRandomPositionInSprite(ObjectType.Slot);
                attempts++;
            } while (IsOverlapping(randomPos_Slot, ObjectType.Slot) && attempts < 50);

            spawns_SlotPos.Add(randomPos_Slot);
            ShirtSlot slot = SimplePool.Spawn(shirtSlotPrefab, randomPos_Slot, Quaternion.identity);
            slot.SetSlotInfo(_info);
            slot.transform.parent = spawnArea_Slot.transform;
            shirtSlotList.Add(slot);
        }
    }
    public void DoWin()
    {
        if (Module.isWin) return;
        Module.isWin = true;
        UIManager.Instance.Show_PopUpWin();
    }

    public void DoLose()
    {
        if (Module.isLose) return;
        Module.isLose = true;
        UIManager.Instance.Show_PopUpLose();
        Module.GameMode = GameMode.None;
    }

    public void BackToHome()
    {
        UIManager.Instance.ShowUIHome();
    }
    public void ResetLevel()
    {
        Module.isLose = false;
        Module.isWin = false;
    }
    Coroutine ctTimeRemain;
    IEnumerator IeTimerCountdown()
    {
        UIManager.Instance.m_UIGamePlay.UpdateTime(timeRemaining);
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            UIManager.Instance.m_UIGamePlay.UpdateTime(timeRemaining);
        }

        if (timeRemaining <= 0)
        {
            DoLose();
        }
    }
    public void RemainChecking(ShirtSlot _slot, ButtonCtrl _btn)
    {
        //SoundManager.Instance.PlayOnCamera(clipCollect);
        //_slot.HideHintEffect();
        //_btn.HideHintEffect();
        shirtSlotList.Remove(_slot);
        buttonList.Remove(_btn);

        buttonRemain -= 1;
        UIManager.Instance.m_UIGamePlay.UpdateSlotLeft(buttonRemain);
        if (buttonRemain <= 0)
        {
            //Show Win
            if (ctTimeRemain != null)
                StopCoroutine(ctTimeRemain);

            //state = EGameState.GameOver;
            DoWin();
        }

        //AutoShowHint();
    }
}
public enum GameMode
{
    None = 0,
    Level = 1,
    Endless = 2,
}
public enum ObjectType
{
    None = 0,
    Button = 1,
    Slot = 2
}