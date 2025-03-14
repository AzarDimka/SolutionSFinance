using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SodruzhestvoFinance.Helpers
{
    public static class HandbookHelpers
    {
        public static IHtmlContent ButtonHandbook(this IHtmlHelper htmlHelper, object bodyButton, string cssClass, object htmlAttributes)
        {
            var button = CreateButton(bodyButton, cssClass, "buttonShowHandbook(this)");

            button.SetAttributes(htmlAttributes);

            return button;
        }

        private static TagBuilder CreateButton(object bodyButton, string cssClass, string onclick)
        {
            TagBuilder button = new TagBuilder("button");

            if (bodyButton is string buttonText)
            {
                button.InnerHtml.Append(buttonText);
            }
            else if (bodyButton is TagBuilder buttonTag)
            {
                button.InnerHtml.AppendHtml(buttonTag);
            }

            button.AddCssClass(cssClass);

            button.Attributes.Add("type", "button");

            button.Attributes.Add("onclick", onclick);

            return button;
        }

        private static void SetAttributes(this TagBuilder tagBuilder, object htmlAttributes)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (attributes.Any())
            {
                foreach (var attribute in attributes)
                {
                    if (attribute.Value != null)
                    {
                        tagBuilder.Attributes.Add(attribute.Key, attribute.Value.ToString() ?? null);
                    }
                }
            }
        }

        public static IHtmlContent ButtonInsertValueHandbook(this IHtmlHelper htmlHelper, object bodyButton, string cssClass,
            object htmlAttributes, string? dataBsTitle = null, string? dataBsToggle = null)
        {
            var button = CreateButton(bodyButton, cssClass, "buttonInsertValuesHandbook(this)");

            button.SetAttributes(htmlAttributes);

            button.SetToltip(dataBsTitle, dataBsToggle);

            return button;
        }

        public static void SetToltip(this TagBuilder builder, string? dataBsTitle = null, string? dataBsToggle = null)
        {
            if (dataBsTitle != null && dataBsToggle != null)
            {
                builder.Attributes.Add("data-bs-title", dataBsTitle);
                builder.Attributes.Add("data-bs-toggle", dataBsToggle);
                builder.Attributes.Add("data-bs-placement", "top");
            }
        }

        public static IHtmlContent ButtonMainBodyHandbook(this IHtmlHelper htmlHelper, object bodyButton, string cssClass, object htmlAttributes)
        {
            var button = CreateButton(bodyButton, cssClass, "buttonMainBodyHandbook(this, 0, null, null)");

            button.SetAttributes(htmlAttributes);

            return button;
        }

        public static IHtmlContent ActionMainBodyHandbook(this IHtmlHelper htmlHelper, object bodyAction, string cssClass, object htmlAttributes, int page, string inputField, string selectValue)
        {
            string function = "buttonMainBodyHandbook(this, " + page + ", '" + inputField + "', " + selectValue + ")";

            var button = CreateAction(bodyAction, cssClass, function);

            button.SetAttributes(htmlAttributes);

            return button;
        }

        private static TagBuilder CreateAction(object bodyAction, string cssClass, string onclick)
        {
            TagBuilder button = new TagBuilder("a");

            if (bodyAction is string actionText)
            {
                button.InnerHtml.Append(actionText);
            }
            else if (bodyAction is TagBuilder actionTag)
            {
                button.InnerHtml.AppendHtml(actionTag);
            }

            button.AddCssClass(cssClass);

            button.Attributes.Add("onclick", onclick);

            return button;
        }

        public static IHtmlContent ButtonSaveValuesInsertHandbook(this IHtmlHelper htmlHelper, object bodyButton, string cssClass, object htmlAttributes)
        {
            var button = CreateButton(bodyButton, cssClass, "buttonSaveValuesInsertHandbook(this, 'insert')");

            button.SetAttributes(htmlAttributes);

            return button;
        }
        public static IHtmlContent ButtonSaveValuesUpdateHandbook(this IHtmlHelper htmlHelper, object bodyButton, string cssClass, object htmlAttributes)
        {
            var button = CreateButton(bodyButton, cssClass, "buttonSaveValuesInsertHandbook(this, 'update')");

            button.SetAttributes(htmlAttributes);

            return button;
        }

        public static IHtmlContent ButtonRetrievalValuesHandbook(this IHtmlHelper htmlHelper, object bodyButton, string cssClass,
            object htmlAttributes, string selectValue, string? dataBsTitle = null, string? dataBsToggle = null)
        {
            var button = CreateButton(bodyButton, cssClass, $"buttonRetrievalValuesHandbook(this, {selectValue})");

            button.SetAttributes(htmlAttributes);

            button.SetToltip(dataBsTitle, dataBsToggle);

            return button;
        }

        public static IHtmlContent ButtonUpdateValues(this IHtmlHelper htmlHelper, object bodyButton, string cssClass,
            object htmlAttributes, string selectValue, string? dataBsTitle = null, string? dataBsToggle = null)
        {
            var button = CreateButton(bodyButton, cssClass, $"buttonUpdateValue(this, {selectValue})");

            button.SetAttributes(htmlAttributes);

            button.SetToltip(dataBsTitle, dataBsToggle);

            return button;
        }

        public static IHtmlContent ButtonDeleteValues(this IHtmlHelper htmlHelper, object bodyButton, string cssClass,
            object htmlAttributes, string selectValue, string? dataBsTitle = null, string? dataBsToggle = null)
        {
            var button = CreateButton(bodyButton, cssClass, $"buttonDeleteValue(this, {selectValue})");

            button.SetAttributes(htmlAttributes);

            button.SetToltip(dataBsTitle, dataBsToggle);

            return button;
        }

        public static IHtmlContent ButtonReturnSelectValue(this IHtmlHelper htmlHelper, object bodyButton, string cssClass, object htmlAttributes)
        {
			var button = CreateButton(bodyButton, cssClass, $"buttonReturnSelectValue(this)");

			button.SetAttributes(htmlAttributes);

			return button;
		}

        public static IHtmlContent InputHandbook(this IHtmlHelper htmlHelper, string idInput, object htmlAttributesButton)
        {
			TagBuilder div = new TagBuilder("div");

            div.AddCssClass("input-group");

            var inputHidden = CreateInput(null, new { type = "hidden", id = idInput, name = idInput });

            var inputText = CreateInput("form-control", new { type = "text" });

            var span = new TagBuilder("span");

			span.AddCssClass("fa fa-search");

			IHtmlContent buttonHelper = null;

			buttonHelper = htmlHelper.ButtonHandbook(span, "btn btn-outline-primary", htmlAttributesButton);

			var spanClear = new TagBuilder("span");

			spanClear.AddCssClass("fa fa-trash");

			TagBuilder buttonClearValue = CreateButton(spanClear, "btn btn-outline-danger", $"buttonClearSelectValue(this)");

			div.InnerHtml.AppendHtml(inputText);
            div.InnerHtml.AppendHtml(inputHidden);
			div.InnerHtml.AppendHtml(buttonHelper);
			div.InnerHtml.AppendHtml(buttonClearValue);

            return div;
		}

        private static TagBuilder CreateInput(string cssClass, object htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("input");

            tag.AddCssClass(cssClass);

            tag.SetAttributes(htmlAttributes);

			return tag;
        }

		public static IHtmlContent TegByName(this IHtmlHelper htmlHelper, string nameTag, object htmlAttributes)
        {
            TagBuilder tag = new TagBuilder(nameTag);

            tag.SetAttributes(htmlAttributes);

            return tag;
        }
    }
}