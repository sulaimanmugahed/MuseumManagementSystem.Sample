using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NuGet.Packaging;
using System;
using System.Text;

namespace MuseumManagementSystem.Web.TagHelpers
{
    public class AppButtonTagHelper:TagHelper
    {
        public string? Type { get; set; }

        public string? Text { get; set; }
        public string? ClassName { get; set; }

        public bool IsLoading { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;


            var attributes = new TagHelperAttributeList();
            attributes.Add("class", "app-button "+ ClassName);
            attributes.Add("type", Type ?? "button");
            if (IsLoading)
            {
              
                output.Content.AppendHtml("<span class=\"loading-icon\"></span>");
            }
            
            output.Content.AppendHtml($"<span class=\"button-text\">{Text}</span>");

           
            output.Attributes.AddRange(attributes);
            

        }
    }
}
