using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace MuseumManagementSystem.Web.ExtensionMethods
{
    public static class SelectListHelper
    {

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> items, object? selectedValue = null)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in items)
            {
                var value = item.GetType().GetProperty("Id")?.GetValue(item)?.ToString();
                var text = item.GetType().GetProperty("Name")?.GetValue(item)?.ToString();

                var selectListItem = new SelectListItem
                {
                    Value = value,
                    Text = text,
                    Selected = value == selectedValue?.ToString()
                };

                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }

        public static List<SelectListItem> ToSelectListWithLocalized<T>(this IEnumerable<T> items, string valueProperty, string textProperty,IStringLocalizer localizer, object? selectedValue = null)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in items)
            {
                var value = item.GetType().GetProperty(valueProperty)?.GetValue(item)?.ToString();
                var text = item.GetType().GetProperty(textProperty)?.GetValue(item)?.ToString();

                var selectListItem = new SelectListItem
                {
                    Value = value,
                    Text = localizer[$"nameOf{text}"].Value ,
                    Selected = value == selectedValue?.ToString()
                };

                selectListItems.Add(selectListItem);
            }

            return selectListItems;
        }

        public static List<SelectListItem> ToMultipleSelectList<T>(this IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector, IEnumerable<string> selectedValues)
        {
            var selectListItems = items.Select(item => new SelectListItem
            {
                Text = textSelector(item),
                Value = valueSelector(item),
                Selected = selectedValues.Contains(valueSelector(item))
            }).ToList();

            return selectListItems;

        }

    }
}
