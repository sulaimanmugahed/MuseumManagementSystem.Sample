using Microsoft.AspNetCore.Razor.TagHelpers;
using NuGet.Packaging;

namespace MuseumManagementSystem.Web.TagHelpers
{
    public class BackButtonTagHelper:TagHelper
    {

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "a";
            var attributes = new TagHelperAttributeList();
            attributes.Add("id", "back-button");
            attributes.Add("type", "a");

            output.Content.AppendHtml("<div class=\"icon \"><i class=\"mdi mdi-arrow-left\" style=\"color: black; font-size: 25px;\"></i></div>");

            output.Attributes.AddRange(attributes);

        }
    }
}
