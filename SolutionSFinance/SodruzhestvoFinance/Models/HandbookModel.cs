using SFinance.Data;

namespace SodruzhestvoFinance.Models;

public class HandbookModel
{
    public string IdHandbookToHtml { get; set; }

    public Handbook Handbook { get; set; }

    public Dictionary<string, string> FieldsInsert { get; set; }

    public StatusInser StatusInser { get; set; }

    public int PageNum { get; set; }

    public int PageCount { get; set; }

    public string? InputField { get; set; }

    public int KeyFieldValue { get; set; }

    public string? VisibleFieldValue { get; set; }

    public bool IsReturnSelectValue { get; set; }

    public HandbookModel()
    {
        IdHandbookToHtml = "";
        Handbook = new Handbook(0, "", "", "");
        FieldsInsert = new Dictionary<string, string>();
        StatusInser = new StatusInser();
    }
}
