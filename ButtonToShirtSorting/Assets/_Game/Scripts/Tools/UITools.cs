using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITools : MonoBehaviour
{
    public LevelModelSO level;
    public List<LevelModelSO> levels = new List<LevelModelSO>();

    [Header("HUD")]
    [SerializeField] private TMP_Dropdown ddLevel;
    [SerializeField] private Toggle togIsRandom;
    [SerializeField] private TMP_InputField inF_Remain;
    [SerializeField] private TMP_InputField inF_TimeCD;

    [SerializeField] private UIButton btnAddNew;
    [SerializeField] private UIButton btnAutoGen;
    [SerializeField] private UIButton btnSave;
    [SerializeField] private UIButton btnAddPoint;
    [SerializeField] private UIButton btnRmvPoint;


    [Header("Prefabs Ref")]
    [SerializeField] private ShirtSlot shirtSlotPrefab;
    [SerializeField] private ButtonCtrl buttonPrefab;

    [Header("ReadOnly")]
    [SerializeField] private SpriteRenderer spawnArea_Button;
    [SerializeField] private SpriteRenderer spawnArea_Slot;
    [SerializeField] private List<ButtonCtrl> buttonCtrls;
    [SerializeField] private List<ShirtSlot> shirtSlots;
    [SerializeField] private int buttonRemain;
    private List<ButtonInfo> buttonColors => ButtonModelSO.Instance.buttons;
    private int lvCurrent => level.level;
    private void Start()
    {
        levels = Resources.LoadAll<LevelModelSO>("Levels").ToList();
        ddLevel.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (var k in levels)
        {
            options.Add(new TMP_Dropdown.OptionData(k.name));
        }
        ddLevel.AddOptions(options);

        btnAddNew.SetUpEvent(AddNewLevel);
        btnAutoGen.SetUpEvent(AutoGenLevel);
        btnSave.SetUpEvent(SaveLevel);
        btnAddPoint.SetUpEvent(AddPoint);
        btnRmvPoint.SetUpEvent(RemovePoint);

        ddLevel.onValueChanged.AddListener(x => LoadLevel(x));
        togIsRandom.onValueChanged.AddListener(x => ChangeRandom(x));
        inF_Remain.onValueChanged.AddListener(x => ChangeRemain(int.Parse(x)));
        inF_TimeCD.onValueChanged.AddListener(x => ChangeTimeCD(int.Parse(x)));

        ddLevel.value = 0;
        LoadLevel(0);
    }

    public void ChangeRandom(bool _isRandom)
    {
        level.isRandom = _isRandom;
    }

    public void ChangeRemain(int _remain)
    {
        level.buttonCount = _remain;
    }

    public void ChangeTimeCD(int _time)
    {
        level.timeCountdown = _time;
    }

    public void Refresh()
    {
        ddLevel.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (var k in levels)
        {
            options.Add(new TMP_Dropdown.OptionData(k.name));
        }

        options.Sort();
        ddLevel.AddOptions(options);

    }


    public void LoadLevel(int _lv)
    {

        level = levels[_lv];
        togIsRandom.isOn = level.isRandom;
        inF_Remain.text = level.buttonCount.ToString("00");
        inF_TimeCD.text = level.timeCountdown.ToString("00");
        if (!level.isRandom)
        {
            foreach (var k in buttonCtrls)
                SimplePool.Despawn(k.gameObject);

            foreach (var k in shirtSlots)
                SimplePool.Despawn(k.gameObject);

            spawns_ButtonPos.Clear();
            spawns_SlotPos.Clear();
            buttonCtrls.Clear();
            shirtSlots.Clear();

            buttonRemain = level.buttonCount;
            for (int i = 0; i < level.buttonCount; i++)
            {
                ShirtSlot slot = SimplePool.Spawn(shirtSlotPrefab, level.slotsPos[i], Quaternion.identity);
                slot.SetSlotInfo(level.buttonInfos[i]);
                slot.transform.SetParent(spawnArea_Slot.transform);
                shirtSlots.Add(slot);

                ButtonCtrl button = SimplePool.Spawn(buttonPrefab, level.buttonsPos[i], Quaternion.identity);
                button.SetButtonInfo(level.buttonInfos[i]);
                button.transform.SetParent(spawnArea_Button.transform);
                buttonCtrls.Add(button);
            }
        }
        else
        {
            AutoGenLevel();
        }

    }


    public void AddNewLevel()
    {
        int _lv = levels.Count + 1;
        LevelModelSO newLevel = new LevelModelSO().CreateNewLevel(_lv);
        newLevel.buttonInfos = buttonColors;
        levels.Add(newLevel);
        level = newLevel;


        Refresh();

        ddLevel.value = _lv - 1;

    }

    public void AutoGenLevel()
    {
        foreach (var k in buttonCtrls)
        {
            SimplePool.Despawn(k.gameObject);
        }

        foreach (var k in shirtSlots)
        {
            SimplePool.Despawn(k.gameObject);
        }
        spawns_ButtonPos.Clear();
        spawns_SlotPos.Clear();
        buttonCtrls.Clear();
        shirtSlots.Clear();

        buttonRemain = level.buttonCount;

        for (int i = 0; i < buttonRemain; i++)
        {
            ButtonInfo _info = level.GetRandomColor();
            Vector3 randomPos_Btn;
            int attempt = 0;
            do
            {
                randomPos_Btn = GetRandomPositionInSprite(ETypeObject.Button);
                attempt++;
            } while (IsOverlapping(randomPos_Btn, ETypeObject.Button) && attempt < 50);

            spawns_ButtonPos.Add(randomPos_Btn);
            ButtonCtrl button = SimplePool.Spawn(buttonPrefab, randomPos_Btn, Quaternion.identity);
            button.SetButtonInfo(_info);
            button.transform.parent = spawnArea_Button.transform;
            buttonCtrls.Add(button);

            Vector3 randomPos_Slot;
            int attempts = 0;
            do
            {
                randomPos_Slot = GetRandomPositionInSprite(ETypeObject.Slot);
                attempts++;
            } while (IsOverlapping(randomPos_Slot, ETypeObject.Slot) && attempts < 50);

            spawns_SlotPos.Add(randomPos_Slot);
            ShirtSlot slot = SimplePool.Spawn(shirtSlotPrefab, randomPos_Slot, Quaternion.identity);
            slot.SetSlotInfo(_info);
            slot.transform.parent = spawnArea_Slot.transform;
            shirtSlots.Add(slot);
        }
    }

    public void AddPoint()
    {
        buttonRemain++;
        inF_Remain.text = buttonRemain.ToString("00");

        ButtonInfo _info = level.GetRandomColor();
        ShirtSlot slot = SimplePool.Spawn(shirtSlotPrefab, GetRandomPositionInSprite(ETypeObject.Slot), Quaternion.identity);
        slot.SetSlotInfo(_info);
        slot.transform.SetParent(spawnArea_Slot.transform);
        shirtSlots.Add(slot);

        ButtonCtrl button = SimplePool.Spawn(buttonPrefab, GetRandomPositionInSprite(ETypeObject.Button), Quaternion.identity);
        button.SetButtonInfo(_info);
        button.transform.SetParent(spawnArea_Button.transform);
        buttonCtrls.Add(button);
    }

    public void RemovePoint()
    {
        if (buttonRemain <= 0)
            return;

        buttonRemain--;
        inF_Remain.text = buttonRemain.ToString("00");

        ShirtSlot slot = shirtSlots[buttonRemain];
        ButtonCtrl button = buttonCtrls[buttonRemain];

        shirtSlots.Remove(slot);
        buttonCtrls.Remove(button);

        SimplePool.Despawn(slot.gameObject);
        SimplePool.Despawn(button.gameObject);
    }

    public void SaveLevel()
    {
        level.timeCountdown = int.Parse(inF_TimeCD.text);
        level.buttonCount = int.Parse(inF_Remain.text);
        level.isRandom = togIsRandom.isOn;

        if (!level.isRandom)
        {
            level.slotsPos.Clear();
            foreach (var slot in shirtSlots)
            {
                level.slotsPos.Add(slot.transform.position);
            }

            level.buttonsPos.Clear();
            foreach (var button in buttonCtrls)
            {
                level.buttonsPos.Add(button.transform.position);
            }
        }

        UnityEditor.EditorUtility.SetDirty(level);

        Debug.LogError(string.Format("Save level {0} done", lvCurrent));
    }


    #region coppy random in gameplayCtrl
    float spacing = 1f;
    List<Vector3> spawns_ButtonPos = new List<Vector3>();
    List<Vector3> spawns_SlotPos = new List<Vector3>();
    private enum ETypeObject
    {
        Slot,
        Button
    }

    Vector3 GetRandomPositionInSprite(ETypeObject type = ETypeObject.Button)
    {
        Bounds bounds = new Bounds();
        float buttonRadius = 0;

        switch (type)
        {
            case ETypeObject.Slot:
                bounds = spawnArea_Slot.bounds;
                buttonRadius = shirtSlotPrefab.imgSlot.bounds.extents.x;
                break;
            case ETypeObject.Button:
                bounds = spawnArea_Button.bounds;
                buttonRadius = buttonPrefab.imgBtn.bounds.extents.x;
                break;
        }


        float x = Random.Range(bounds.min.x + buttonRadius, bounds.max.x - buttonRadius);
        float y = Random.Range(bounds.min.y + buttonRadius, bounds.max.y - buttonRadius);

        return new Vector3(x, y, -1);
    }

    bool IsOverlapping(Vector3 position, ETypeObject type = ETypeObject.Button)
    {
        switch (type)
        {
            case ETypeObject.Slot:
                foreach (Vector3 existingPos in spawns_SlotPos)
                {
                    if (Vector3.Distance(existingPos, position) < spacing)
                    {
                        return true;
                    }
                }
                break;
            case ETypeObject.Button:
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

    #endregion

    [ContextMenu("Gen100Level")]
    public void Gen100Level()
    {
        int lvmin = 8;
        int lvMax = 100;
        for (int i = lvmin; i <= lvMax; i++)
        {
            LevelModelSO newLevel = new LevelModelSO().CreateNewLevel(i);
            newLevel.buttonInfos = buttonColors;

        }
    }
}
