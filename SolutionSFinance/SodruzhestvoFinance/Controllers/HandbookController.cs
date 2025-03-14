using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SFinance.Data;
using SFinance.Data.Services;
using SodruzhestvoFinance.Models;
using SodruzhestvoFinance.Models.Enum;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SodruzhestvoFinance.Controllers;

public class HandbookController : Controller
{
    private const int SizePage = 15;

    private readonly IManagerHandbook Service;

    public HandbookController(IManagerHandbook handbookManager)
    {
        Service = handbookManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetHandbookHTML(int idHandbook, bool isReturnSelectValue)
    {
        var model = GetModelHandbook(idHandbook);

        model.IsReturnSelectValue = isReturnSelectValue;

        return PartialView("Handbook", model);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMainBodyHandbookHTML(int idHandbook, int page = 0, string? inputField = null, Dictionary<string, string>? selectValue = null)
    {
        var model = GetModelHandbook(idHandbook, page, inputField, selectValue);

        return PartialView("MainBodyHandbook", model);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetInsertBodyHTML(int idHandbook)
    {
        var model = GetInsertModelHandbook(idHandbook);

        return PartialView("InsertBodyHandbook", model);
    }

    [HttpGet]
    public async Task<IActionResult> GetUpdateBodyHTML(int idHandbook, Dictionary<string, string> selectValue)
    {
        var model = GetInsertModelHandbook(idHandbook, selectValue);

        return PartialView("UpdateBodyHandbook", model);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(HandbookModel formModel)
    {
        HandbookModel handbookModel = GetInsertModelHandbook(formModel.Handbook.IdHandbook);

        try
        {
            Service.InsertValue(formModel.Handbook.IdHandbook, formModel.FieldsInsert);
            handbookModel.StatusInser.StatusType = TypeStatus.Success;
            handbookModel.StatusInser.MassageText = "Запись добавлена!";
        }
        catch (Exception ex)
        {
            handbookModel.StatusInser.StatusType = TypeStatus.Failed;
            handbookModel.StatusInser.MassageText = ex.Message;
        }

        return PartialView("StatusOperation", handbookModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(HandbookModel formModel)
    {
        HandbookModel handbookModel = GetInsertModelHandbook(formModel.Handbook.IdHandbook);

        try
        {
            Service.UpdateValue(formModel.Handbook.IdHandbook, formModel.KeyFieldValue.ToString(), formModel.FieldsInsert);
            handbookModel.StatusInser.StatusType = TypeStatus.Success;
            handbookModel.StatusInser.MassageText = "Запись обновлена!";
        }
        catch (Exception ex)
        {
            handbookModel.StatusInser.StatusType = TypeStatus.Failed;
            handbookModel.StatusInser.MassageText = ex.Message;
        }

        return PartialView("StatusOperation", handbookModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(HandbookModel formModel)
    {
        HandbookModel handbookModel = GetModelHandbook(formModel.Handbook.IdHandbook);

        try
        {
            Service.DeleteValue(formModel.Handbook.IdHandbook, formModel.KeyFieldValue.ToString());
            handbookModel.StatusInser.StatusType = TypeStatus.Success;
            handbookModel.StatusInser.MassageText = "Запись удалена!";
        }
        catch (Exception ex)
        {
            handbookModel.StatusInser.StatusType = TypeStatus.Failed;
            handbookModel.StatusInser.MassageText = ex.Message;
        }

        return PartialView("StatusOperation", handbookModel);
    }

    private HandbookModel GetModelHandbook(int idHandbook, int pageNum = 0, string? inputField = null, Dictionary<string, string>? selectValue = null)
    {
        Handbook handbook = Service.GenerateHandbook(idHandbook);

        if (inputField != null)
        {
            List<Dictionary<string, object>> fieldsResult = new List<Dictionary<string, object>>();

            string fieldVisible = handbook.VisibleField;

            fieldsResult = handbook.FieldsValue
                .Where(dict => dict.ContainsKey(fieldVisible) &&
                dict[fieldVisible] is string value &&
                value.ToLower().Contains(inputField.ToLower()))
            .ToList();

            handbook.FieldsValue = fieldsResult;
        }

        int pageCount = Convert.ToInt32(Math.Ceiling((decimal)handbook.FieldsValue.Count / SizePage));

        handbook.FieldsValue = handbook.FieldsValue.Skip(SizePage * pageNum).Take(SizePage).ToList();
        
        int key = 0;

        string? value = null;

        if (selectValue != null)
        {
            key = Convert.ToInt32(selectValue.Where(x => x.Key == "key").FirstOrDefault().Value);
        
            value = selectValue.Where(x => x.Key == "value").FirstOrDefault().Value;
        }

        var model = new HandbookModel()
        {
            IdHandbookToHtml = "Handbook" + idHandbook,
            Handbook = handbook,
            PageCount = pageCount,
            PageNum = pageNum,
            InputField = inputField,
            KeyFieldValue = key,
            VisibleFieldValue = value
        };

        return model;
    }

    private HandbookModel GetInsertModelHandbook(int idHandbook, Dictionary<string, string>? selectValue = null)
    {
        var handbook = Service.GenerateHandbook(idHandbook);

        var fieldKey = handbook.Fields.Where(f => f.NameFieldToQuery == handbook.KeyField).FirstOrDefault();

        if (fieldKey != null)
        {
            handbook.Fields.Remove(fieldKey);
        }

        int key = 0;

        string? value = null;

        if (selectValue != null)
        {
            key = Convert.ToInt32(selectValue.Where(x => x.Key == "key").FirstOrDefault().Value);

            value = selectValue.Where(x => x.Key == "value").FirstOrDefault().Value;
        }

        var model = new HandbookModel()
        {
            IdHandbookToHtml = "Handbook" + idHandbook,
            Handbook = handbook,
            KeyFieldValue = key,
            VisibleFieldValue = value
        };

        return model;
    }
}