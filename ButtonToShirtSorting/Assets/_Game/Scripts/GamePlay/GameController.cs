using DG.Tweening;
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
    public int timeRemaining = 99;
    public int buttonRemain = 10;

    [Header("Prefabs Ref")]
    [SerializeField] ButtonCtrl buttonPrefab;
    [SerializeField] ShirtSlot shirtSlotPrefab;
    [Header("ReadOnly")]
    [SerializeField] private SpriteRenderer spawnArea_Button;
    [SerializeField] private SpriteRenderer spawnArea_Slot;
    [SerializeField] List<ButtonCtrl> buttonList;
    [SerializeField] List<ShirtSlot> shirtSlotList;
    [SerializeField] List<ButtonCtrl> removeButtons;
    [SerializeField] List<ShirtSlot> removeSlots;

    private float spacing = 1f;
    List<Vector3> spawns_ButtonPos = new List<Vector3>();
    List<Vector3> spawns_SlotPos = new List<Vector3>();
    public void Init()
    {
        trGamelevel.SetActive(true);
        this.gameMode = Module.GameMode;
        switch (gameMode)
        {
            case GameMode.Level:
                LoadLevel();
                break;
            case GameMode.Endless:
                LoadEndless();
                timeRemaining = 999;
                break;
            default:
                break;
        }
        if (crTimeRemain != null)
            StopCoroutine(crTimeRemain);
        crTimeRemain = StartCoroutine(IeTimerCountdown());
        ShowHint();
    }
    public void LoadLevel()
    {
        ResetLevel();
        timeRemaining = 99;
        Module.isLose = false;
        Module.isWin = false;
        level = Resources.Load<LevelModelSO>(string.Format("Levels/Lv{0}", Module.cr_Level));
        buttonRemain = level.buttonCount;
        UIManager.Instance.m_UIGamePlay.UpdateSlotLeft(buttonRemain);
        //List<ButtonInfo> selectColors = ButtonModelSO.Instance.buttons.OrderBy(b => Random.Range(0, ButtonModelSO.Instance.buttons.Count)).Take(buttonRemain).ToList();

        List<ButtonInfo> selectColors = new List<ButtonInfo>();
        for (int i = 0; i < buttonRemain; i++)
        {
            selectColors.Add(ButtonModelSO.Instance.buttons[Random.Range(0, ButtonModelSO.Instance.buttons.Count)]);
        }
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
            ButtonInfo info = ButtonModelSO.Instance.GetRandomColor();
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
        ResetLevel();
        Module.isLose = false;
        buttonRemain = Random.Range(5, 10) + Module.cr_EndlessLevel / 5;
        UIManager.Instance.m_UIGamePlay.UpdateSlotLeft(buttonRemain);
        List<ButtonInfo> selectColors = new List<ButtonInfo>();// = ButtonModelSO.Instance.buttons.OrderBy(b => Random.Range(0, ButtonModelSO.Instance.buttons.Count)).Take(buttonRemain).ToList();
        for (int i = 0; i < buttonRemain; i++)
        {
            selectColors.Add(ButtonModelSO.Instance.buttons[Random.Range(0, ButtonModelSO.Instance.buttons.Count)]);
        }
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
    public void DoWinLevel()
    {
        if (Module.isWin) return;
        Module.isWin = true;
        Module.cr_Level++;
        UIManager.Instance.Show_PopUpWin();
        if (crTimeRemain != null)
            StopCoroutine(crTimeRemain);
        crTimeRemain = StartCoroutine(IeTimerCountdown());
    }
    public void DoWinEndless()
    {
        if (Module.isWin) return;
        Module.isWin = true;
        Module.cr_EndlessLevel++;
        LoadEndless();
        if (crTimeRemain != null)
            StopCoroutine(crTimeRemain);
        crTimeRemain = StartCoroutine(IeTimerCountdown());
    }

    public void DoLose()
    {
        if (Module.isLose) return;
        Module.isLose = true;
        UIManager.Instance.Show_PopUpLose();
        Module.GameMode = GameMode.None;
        if (crTimeRemain != null)
            StopCoroutine(crTimeRemain);
    }
    public void ResetLevel()
    {
        Module.isLose = false;
        Module.isWin = false;
        RemoveAllObject();
    }
    public void RemoveAllObject()
    {
        foreach (var item in removeButtons)
        {
            item.DespawnObj();
        }
        removeButtons.Clear();
        foreach (var item in removeSlots)
        {
            item.DespawnObj();
        }
        removeSlots.Clear();
        foreach (var item in buttonList)
        {
            item.DespawnObj();
        }
        buttonList.Clear();
        foreach (var item in shirtSlotList)
        {
            item.DespawnObj();
        }
        shirtSlotList.Clear();
    }
    Coroutine crTimeRemain;
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
        _slot.HideHint();
        _btn.HideHint();
        removeButtons.Add(_btn);
        removeSlots.Add(_slot);
        shirtSlotList.Remove(_slot);
        buttonList.Remove(_btn);

        buttonRemain -= 1;
        UIManager.Instance.m_UIGamePlay.UpdateSlotLeft(buttonRemain);
        if (buttonRemain <= 0)
        {
            //Show Win
            if (crTimeRemain != null)
                StopCoroutine(crTimeRemain);

            switch (gameMode)
            {
                case GameMode.Level:
                    DoWinLevel();
                    break;
                case GameMode.Endless:
                    DoWinEndless();
                    break;
                default:
                    break;
            }
        }

        ShowHint();
    }
    #region Hint
    Coroutine crHint;
    public void ShowHint()
    {
        if (crHint != null) StopCoroutine(crHint);
        crHint = StartCoroutine(IShowHint());
    }
    public IEnumerator IShowHint()
    {
        int time = 10;
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
        }
        foreach (var button in buttonList)
        {
            if (!button.IsPlaced)
            {
                foreach (var slot in shirtSlotList)
                {
                    if (button.buttonInfo == slot.slotInfo)
                    {
                        button.ShowHint();
                        slot.ShowHint();
                        break;
                    }
                }
            }
            break;
        }
    }
    bool isRunningHint = false;
    public void OnClickButtonHint()
    {
        if (isRunningHint) return;
        foreach (var button in buttonList)
        {
            if (!button.IsPlaced)
            {
                foreach (var slot in shirtSlotList)
                {
                    if (button.buttonInfo == slot.slotInfo)
                    {
                        isRunningHint = true;
                        button.transform.DOMove(slot.transform.position, 1f).OnComplete(() =>
                        {
                            button.SetAuto(slot);
                            isRunningHint = false;
                        });
                        break;
                    }
                }
            }
            break;
        }
    }
    #endregion
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