using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Stormbreaker.Models;
using Stormbreaker.Web.UI;

namespace Stormbreaker.Web.Mvc.Html {
    /// <summary>
    /// Extension methods for the <see cref="HtmlHelper"/> object.
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    public static class MenuHelper {
        /// <summary>
        /// Responsible for creating a navigation based on an unordered list
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="structureInfo">Hierarchical structure info</param>
        /// <param name="itemContent">Default content for links</param>
        /// <returns></returns>
        public static string Menu(this HtmlHelper html, IStructureInfo structureInfo, Func<IPageModel, MvcHtmlString> itemContent)
        {
            return Menu(html, null, structureInfo, itemContent);
        }
        /// <summary>
        /// Responsible for creating a navigation based on an unordered list
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="id">Id of the unordered list</param>
        /// <param name="structureInfo">Hierarchical structure info</param>
        /// <param name="itemContent">Default content for links</param>
        /// <returns></returns>
        public static string Menu(this HtmlHelper html, string id, IStructureInfo structureInfo, Func<IPageModel, MvcHtmlString> itemContent)
        {
            return Menu(html, id, null, structureInfo, itemContent);
        }
        /// <summary>
        /// Responsible for creating a navigation based on an unordered list
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="id">Id of the unordered list</param>
        /// <param name="currentModel">The current page in the current request</param>
        /// <param name="structureInfo">Hierarchical structure info</param>
        /// <param name="itemContent">Default content for links</param>
        /// <returns></returns>
        public static string Menu(this HtmlHelper html, string id, IPageModel currentModel, IStructureInfo structureInfo, Func<IPageModel, MvcHtmlString> itemContent)
        {
            return Menu(html, id, currentModel, structureInfo, itemContent, itemContent);
        }
        /// <summary>
        /// Responsible for creating a main navigation based on an unordered list
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <param name="id">The id of the unordered list</param>
        /// <param name="currentModel"></param>
        /// <param name="structureInfo"></param>
        /// <param name="itemContent">Default content for links</param>
        /// <param name="selectedItemContent">Content for selected links</param>
        /// <returns></returns>
        public static string Menu(this HtmlHelper html, string id, IPageModel currentModel, IStructureInfo structureInfo, Func<IPageModel, MvcHtmlString> itemContent, Func<IPageModel, MvcHtmlString> selectedItemContent) {
            // only render the top level items
            var items = structureInfo.HierarchicalStructure.Where(x => x.Depth == 1);

            var sb = new StringBuilder();
            if(string.IsNullOrEmpty(id)) {
                sb.AppendLine("<ul>");
            }
            else {
                sb.AppendFormat("<ul id=\"{0}\">", id);
            }

            foreach (var item in items)
            {
                RenderLi(sb, "<li>{0}</li>", item.Entity, item.Entity.Equals(currentModel) ? selectedItemContent : itemContent);
            }

            sb.AppendLine("</ul>");
            return sb.ToString();
        }
        /// <summary>
        /// Responsible for renderingen the li element with it's content
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="format">The format.</param>
        /// <param name="item">The item.</param>
        /// <param name="itemContent">Content of the item.</param>
        private static void RenderLi(StringBuilder sb, string format, IPageModel item, Func<IPageModel, MvcHtmlString> itemContent) {
            sb.AppendFormat(format, itemContent(item));
        }
    }
}
