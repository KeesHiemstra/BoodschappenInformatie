using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BoodschappenInformatie.TagHelpers
{
	#region GroupTagHelp
	// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
	[HtmlTargetElement("groups")]
	public class GroupTagHelper : TagHelper
	{
		#region Specific declerations
		/// <summary>
		/// Refer to IDicionary<string:Key><string:Value> Groups in the tag groups.
		/// Key is used to display in the page.
		/// Value is used the RegEx pattern.
		/// </summary>
		public IDictionary<string, string> Groups { get; set; }

		/// <summary>
		/// Refer to string CurrentGroup in the tag current-group.
		/// </summary>
		public string CurrentGroup { get; set; } = _FirstGroup;

		/// <summary>
		/// Refer to string RemainingGroup (optional) in the tag remainng-group.
		/// If the remaining group is mentions the name is shown and selection is based on not-matched last group RegEx.
		/// </summary>
		public string RemainingGroup { get; set; }

		private static string _FirstGroup { get; set; }
		#endregion

		#region Generic declarations
		private readonly IHtmlGenerator _Generator;
		
		/// <summary>
		/// The name of the page.
		/// </summary>
		/// <remarks>
		/// Can be <c>null</c> if refering to the current page.
		/// </remarks>
		public string AspPage { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Rendering.ViewContext"/> for the current request.
		/// </summary>
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }
		#endregion

		#region Construction
		public GroupTagHelper(IHtmlGenerator generator)
		{
			_Generator = generator;
		}
		#endregion

		#region Process
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "ul";

			//This is the initial group name to use
			_FirstGroup = Groups.First().Key;

			TagBuilder ul = new TagBuilder("ul");
			ul.AddCssClass("pagination");
			output.MergeAttributes(ul);

			foreach (var item in Groups)
			{
				output.Content.AppendHtml(LiName(item.Key, true));
			}
			if (!string.IsNullOrEmpty(RemainingGroup))
			{
				output.Content.AppendHtml(LiName(RemainingGroup, false));
			}

			await base.ProcessAsync(context, output);
			return;
		}
		#endregion

		#region Process helpers
		private TagBuilder LiName(string groupName, bool match)
		{
			if (!match) { groupName = $"{RemainingGroup}"; }

			TagBuilder li = new TagBuilder("li");
			li.AddCssClass("page-item");
			if (groupName == CurrentGroup)
			{
				li.AddCssClass("active");
				TagBuilder span1 = new TagBuilder("span");
				span1.AddCssClass("page-link");
				span1.InnerHtml.Append($"{groupName}");
				li.InnerHtml.AppendHtml(span1);
			}
			else
			{
				object route = new { GroupName = groupName };
				TagBuilder a;
				a = _Generator.GeneratePageLink(
					ViewContext,
					linkText: groupName,
					pageName: AspPage,
					pageHandler: string.Empty,
					protocol: string.Empty,
					hostname: string.Empty,
					fragment: string.Empty,
					routeValues: route,
					htmlAttributes: null
					);
				a.AddCssClass("page-link");

				li.InnerHtml.AppendHtml(a);
			}

			return li;
		}
		#endregion
	}
	#endregion
}
