/*   SmartCheck License
*******************************************************************************
*                                                                             *
*    Copyright (c) 2021 Luciano Gorosito <lucianogorosito@hotmail.com>        *
*                                                                             *
*    This file is part of SmartCheck                                          *
*                                                                             *
*    SmartCheck is free software: you can redistribute it and/or modify       *
*    it under the terms of the GNU General Public License as published by     *
*    the Free Software Foundation, either version 3 of the License, or        *
*    (at your option) any later version.                                      *
*                                                                             *
*    SmartCheck is distributed in the hope that it will be useful,            *
*    but WITHOUT ANY WARRANTY; without even the implied warranty of           *
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the            *
*    GNU General Public License for more details.                             *
*                                                                             *
*    You should have received a copy of the GNU General Public License        *
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.   *
*                                                                             *
*******************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCheck
{
    public class Check
    {
        public string order { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool pass { get; set; }
        public string result { get; set; }
        public List<Item> items { get; set; }
        public int icon { get; set; }

        public Check()
        {
            this.order = string.Empty;
            this.code = string.Empty;
            this.name = string.Empty;
            this.description = string.Empty;
            this.pass = false;
            this.result = string.Empty;
            this.items = new List<Item>();
            this.icon = 0;
        }

        public Check(string order, string code, string name, string description, bool pass, string result, List<Item> items,
            int icon)
        {
            this.order = order;
            this.code = code;
            this.name = name;
            this.description = description;
            this.pass = pass;
            this.result = result;
            this.items = items;
            this.icon = icon;
        }

        public string ToReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.order + " - " + this.name.ToUpper());
            sb.AppendLine("");
            sb.AppendLine("DESCRIPTION:");
            sb.AppendLine(this.description);
            sb.AppendLine("");
            foreach (Item itm in this.items)
            {
                string line = "";
                if (itm.id != 0)
                {
                    line += "<" + itm.id.ToString() + ">";
                }
                if (itm.category != string.Empty)
                {
                    line += itm.category;
                }
                if (itm.name != string.Empty)
                {
                    line += " | " + itm.name;
                }
                sb.AppendLine(line);
            }
            sb.AppendLine("");
            sb.AppendLine("ID List");
            string ids = string.Empty;
            foreach (Item itm in this.items)
            {
                ids += itm.id.ToString() + ";";
            }
            sb.AppendLine(ids);
            return sb.ToString();
        }

        /// <summary> Creates an html report from the Check </summary>
        public string ToHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<hr>");
            sb.AppendLine("<style>");
            sb.AppendLine("table, th, td { border: 1px solid black; border-collapse: collapse; }");
            sb.AppendLine("th, td { padding: 5px; } th { text-align: left; }");
            sb.AppendLine("</style>");
            sb.AppendLine("<h2>" + this.order + " - " + this.name + "</h2>");
            sb.AppendLine("<p>" + this.description + "</p>");
            sb.AppendLine("<h3>Results:</h3>");
            sb.AppendLine("<table>");
            if (this.items.Count > 0)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine("<td class='item'><h4>" + "Category" + "</h4></td>");
                sb.AppendLine("<td class='item'><h4>" + "Name" + "</h4></td>");
                sb.AppendLine("<td class='item'><h4>" + "ID" + "</h4></td>");
                sb.AppendLine("<td class='item'><h4>" + "Comments" + "</h4></td>");
                sb.AppendLine("</tr>");
                foreach (Item itm in this.items)
                {
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td class='item'>" + itm.category + "</td>");
                    sb.AppendLine("<td class='item'>" + itm.name + "</td>");
                    sb.AppendLine("<td class='item'>" + itm.id + "</td>");
                    sb.AppendLine("<td class='item'>" + itm.result + "</td>");
                    sb.AppendLine("</tr>");
                }
            }
            else
            {
                sb.AppendLine("<td>No elements</td>");
            }
            sb.AppendLine("</table>");
            sb.AppendLine("<h3>ID List:</h3>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            string line = "<td>";
            foreach (Item itm in this.items)
            {
                line += itm.id + ";";
            }
            line += "</td>";
            sb.AppendLine(line);
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<p></p>");
            return sb.ToString();
        }
    }
}
