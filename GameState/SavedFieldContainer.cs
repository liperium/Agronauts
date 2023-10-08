using System;
using System.Collections.Generic;

[Serializable]
public class SavedField
{
    public Pos2D field;
    public Pos2D tile;
}

[Serializable]
public class SavedFieldContainer
{
    public List<SavedField> boughtFieldsHistory;
    public void AddBoughtFieldToHistory(SavedField savedField)
    {
        boughtFieldsHistory.Add(savedField);
    }

    public void Init()
    {
        boughtFieldsHistory = new List<SavedField>();
    }
}