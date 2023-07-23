using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace ListDetailsTest
{
    public static class HtmlHelperExtension
    {
        public static HtmlString CreateInputsFromEntity<T>(this IHtmlHelper html, T item, params string[] ignoredParams)
        {
            TagBuilder container = new TagBuilder("div");
            container.AddCssClass("inputs-container");
            container.Attributes.Add("data-id", $"{typeof(T).GetProperty("id")!.GetValue(item)}");

            List<string> props = new();
            Type type = typeof(T);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if(IsProper(prop.Name, ignoredParams))
                {

                    TagBuilder label = new TagBuilder("label");
                    label.Attributes.Add("for", $"input-{type.Name}-{prop.Name}");
                    label.InnerHtml.Append(FirstUpper(prop.Name));

                    TagBuilder input = new TagBuilder("input");

                    if(prop.PropertyType == typeof(DateOnly))
                    {
                        input.Attributes.Add("type", "date");
                        input.Attributes.Add("value", ((DateOnly)prop.GetValue(item)!).ToString("yyyy-MM-dd"));
                    }
                    else if(prop.PropertyType == typeof(Int32))
                    {
                        input.Attributes.Add("type", "number");
                        input.Attributes.Add("value", prop.GetValue(item)!.ToString());
                    }
                    else
                    {
                        input.Attributes.Add("type", "text");
                        input.Attributes.Add("value", prop.GetValue(item)!.ToString());
                    }


                    input.Attributes.Add("id", $"input-{type.Name}-{prop.Name}");

                    container.InnerHtml.AppendHtml(label);
                    container.InnerHtml.AppendHtml(input);
                }
            }

            using var writter = new StringWriter();
            container.WriteTo(writter, HtmlEncoder.Default);

            return new HtmlString(writter.ToString());
        }

        public static HtmlString CreateTableFromEntities<T>(this IHtmlHelper html, List<T> list, params string[] ignoredParams)
        {
            TagBuilder table = new TagBuilder("table");
            List<string> props = new();
            Type type = typeof(T);

            TagBuilder tableHeader = new TagBuilder("tr");

            foreach(PropertyInfo prop in type.GetProperties())
            {
                if(IsProper(prop.Name, ignoredParams))
                {
                    TagBuilder th = new TagBuilder("th");
                    props.Add(prop.Name);
                    th.InnerHtml.Append(FirstUpper(prop.Name));
                    tableHeader.InnerHtml.AppendHtml(th);
                }
            }

            table.InnerHtml.AppendHtml(tableHeader);

            foreach(T item in list)
            {
                TagBuilder row = new TagBuilder("tr");

                foreach(string propName in props)
                {
                    TagBuilder td = new TagBuilder("td");
                    td.InnerHtml.Append(type.GetProperty(propName)!.GetValue(item)!.ToString()!);
                    row.InnerHtml.AppendHtml(td);
                }
                table.InnerHtml.AppendHtml(row);
            }


            using var writer = new StringWriter();
            table.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        static bool IsProper(string inspected, params string[] values)
        {
            if (values.Length == 0)
                return true;

            foreach(string s in values)
            {
                if (inspected == s)
                    return false;
            }
            return true;
        }

        public static string FirstUpper(string str)
        {
            string[] s = Regex.Split(str, @"(?=\p{Lu})");

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 1)
                    s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1);
                else s[i] = s[i].ToUpper();
            }
            return string.Join(" ", s);
        }
    }
}
